using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxMaxLengthTests
{
    private const int EmLimitText = 0x00C5;
    private const int EmGetLimitText = 0x00D5;
    private const int WmChar = 0x0102;

    [Fact]
    public void StandardMaxLengthIsVirtualAndDeclaredOnTextBoxBase()
    {
        PropertyInfo property = typeof(TextBoxBase).GetProperty("MaxLength")!;
        Assert.NotNull(property);
        Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
        Assert.Equal(typeof(int), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic && property.GetMethod.IsVirtual);
        Assert.True(property.SetMethod!.IsPublic && property.SetMethod.IsVirtual);
        Assert.Equal(32767, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        MethodInfo update = typeof(TextBoxBase).GetMethod("UpdateMaxLength", BindingFlags.Instance | BindingFlags.NonPublic)!;
        Assert.True(update.IsAssembly && update.IsVirtual);
    }

    [Fact]
    public void DefaultDoesNotCreateHandle()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            Assert.Equal(32767, textBox.MaxLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(32767)]
    [InlineData(int.MaxValue)]
    public void ValidPendingValuesPreserveTextAndSelectionWithoutCreatingHandle(int value)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "abcdef" };
            textBox.Select(2, 3);
            textBox.MaxLength = value;
            Assert.Equal(value, textBox.MaxLength);
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void NegativeValueThrowsWithoutChangingStateOrCreatingHandle(int value)
        => OnSta(() =>
        {
            using var textBox = new TextBox { MaxLength = 7 };
            Assert.Equal("value", Assert.Throws<ArgumentOutOfRangeException>(() => textBox.MaxLength = value).ParamName);
            Assert.Equal(7, textBox.MaxLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CreationAndRecreationApplyConfiguredLimitWithoutTruncatingExistingText(bool multiline)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Multiline = multiline, Text = "abcdef", MaxLength = 3 };
            nint first = CreateHostedEdit(form, textBox);
            Assert.Equal((nint)3, SendMessageW(first, EmGetLimitText, 0, 0));
            Assert.Equal("abcdef", textBox.Text);
            textBox.HideSelection = false;
            Assert.NotEqual(first, textBox.Handle);
            Assert.Equal((nint)3, SendMessageW(textBox.Handle, EmGetLimitText, 0, 0));
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(form.Handle, GetParent(textBox.Handle));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RuntimeLimitChangesKeepHandleAndStateAndRepeatedValueSendsNoMessage(bool multiline)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Multiline = multiline, Text = "abcdef" };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(1, 3);
            textBox.Modified = true;
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;
            textBox.CaptureLimits = true;
            textBox.MaxLength = 2;
            textBox.MaxLength = 2;
            Assert.Equal(new[] { (nint)2 }, textBox.Limits);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal((nint)2, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.Equal("abcdef", textBox.Text);
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.Equal(0, changes);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void NativeTypingHonorsLimitWhileProgrammaticTextCanExceedIt(bool multiline)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Multiline = multiline, MaxLength = 3 };
            nint handle = CreateHostedEdit(form, textBox);
            foreach (char character in "abcd")
            {
                _ = SendMessageW(handle, WmChar, character, 0);
            }
            Assert.Equal("abc", textBox.Text);
            textBox.Text = "longer than three";
            Assert.Equal("longer than three", textBox.Text);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
        });

    [Fact]
    public void ZeroDisablesTheConfiguredSmallLimit()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { MaxLength = 1 };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.MaxLength = 0;
            foreach (char character in "abcd")
            {
                _ = SendMessageW(handle, WmChar, character, 0);
            }
            Assert.Equal("abcd", textBox.Text);
            Assert.Equal(0, textBox.MaxLength);
            Assert.True(SendMessageW(handle, EmGetLimitText, 0, 0) > 4);
        });

    [Fact]
    public void NativeHookFailurePreservesCommittedValueAndOriginalException()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox();
            nint handle = CreateHostedEdit(form, textBox);
            // A managed WndProc failure is reported at the native callback boundary.
            // The public setter itself retains the configured state and handle.
            var failure = new InvalidOperationException("limit hook");
            Exception? observed = null;
            ThreadExceptionEventHandler handler = (_, args) => observed = args.Exception;
            Application.ThreadException += handler;
            try
            {
                textBox.LimitFailure = failure;
                textBox.MaxLength = 9;
                Assert.Equal(9, textBox.MaxLength);
                Assert.Equal(handle, textBox.Handle);
                Assert.Same(failure, observed);
            }
            finally { Application.ThreadException -= handler; }
        });

    private static nint CreateHostedEdit(Form form, TextBox textBox)
    {
        form.Controls.Add(textBox);
        _ = form.Handle;
        nint handle = textBox.Handle;
        Assert.Equal(form.Handle, GetParent(handle));
        Assert.Same(form, Control.FromHandle(GetParent(handle)));
        return handle;
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try
                {
                    Application.ExitThread();
                    typeof(Application).GetMethod("DisposeThemedCommonControlsActivation", BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                }
                catch (Exception exception) { failure = failure is null ? exception : new AggregateException(failure, exception); }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox MaxLength test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ObservingTextBox : TextBox
    {
        internal bool CaptureLimits { get; set; }
        internal List<nint> Limits { get; } = new();
        internal Exception? LimitFailure { get; set; }
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == EmLimitText)
            {
                if (CaptureLimits) { Limits.Add(message.WParam); }
                if (LimitFailure is not null) { throw LimitFailure; }
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);
}
