using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxLinesTests
{
    public static TheoryData<string, string[]> LineCases => new()
    {
        { string.Empty, [] },
        { "alpha", ["alpha"] },
        { "\r", ["", ""] },
        { "\n", ["", ""] },
        { "\r\n", ["", ""] },
        { "alpha\rbeta", ["alpha", "beta"] },
        { "alpha\nbeta", ["alpha", "beta"] },
        { "alpha\r\nbeta", ["alpha", "beta"] },
        { "alpha\r\r\nbeta\n", ["alpha", "", "beta", ""] },
        { "\n\ralpha\r", ["", "", "alpha", ""] },
        { "\r\n\r\n", ["", "", ""] },
        { "甲\r乙\n丙\r\n\U0001F600", ["甲", "乙", "丙", "\U0001F600"] }
    };

    public static TheoryData<string[]?, string> AssignmentCases => new()
    {
        { null, string.Empty },
        { [], string.Empty },
        { ["alpha"], "alpha" },
        { ["alpha", "beta", ""], "alpha" + Environment.NewLine + "beta" + Environment.NewLine },
        { ["alpha", null!, "beta"], "alpha" + Environment.NewLine + Environment.NewLine + "beta" },
        { ["甲\r乙", "\U0001F600"], "甲\r乙" + Environment.NewLine + "\U0001F600" }
    };

    [Fact]
    public void LinesHasStandardNonvirtualPropertyAndSerializationMetadata()
    {
        PropertyInfo property = typeof(TextBoxBase).GetProperty(nameof(TextBoxBase.Lines))!;
        Assert.Equal(typeof(string[]), property.PropertyType);
        Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.False(property.SetMethod!.IsVirtual);
        Assert.Equal("Appearance", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.False(property.GetCustomAttribute<MergablePropertyAttribute>()!.AllowMerge);
        Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        Assert.NotNull(property.SetMethod!.GetParameters()[0].GetCustomAttribute<AllowNullAttribute>());
        NullabilityInfo nullability = new NullabilityInfoContext().Create(property);
        Assert.Equal(NullabilityState.NotNull, nullability.ReadState);
        Assert.Equal(NullabilityState.Nullable, nullability.WriteState);
        Assert.Equal(typeof(TextBoxBase), typeof(TextBox).GetProperty(nameof(TextBox.Lines))!.DeclaringType);
    }

    [Theory]
    [MemberData(nameof(LineCases))]
    public void PendingLinesUseCrLfAndCrLfPairsWithoutCreatingHandle(string text, string[] expected)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = text };
            int notifications = 0;
            textBox.TextChanged += (_, _) => notifications++;

            Assert.Equal(expected, textBox.Lines);

            Assert.Equal(text, textBox.Text);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, notifications);
        });

    [Theory]
    [MemberData(nameof(AssignmentCases))]
    public void AssigningLinesUsesPlatformNewlinesAndNormalizesNullWithoutCreatingHandle(string[]? lines, string expected)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "before" };
            int notifications = 0;
            textBox.TextChanged += (_, _) => notifications++;

            textBox.Lines = lines;

            Assert.Equal(expected, textBox.Text);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(1, notifications);
        });

    [Fact]
    public void LinesGetterCapturesVirtualTextOnceAndReturnsAnIndependentArray()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { ReportedText = "alpha\rbeta\n" };
            textBox.TextReads = 0;

            string[] first = textBox.Lines;

            Assert.Equal(1, textBox.TextReads);
            Assert.Equal(new[] { "alpha", "beta", "" }, first);
            first[0] = "changed";
            Assert.Equal(new[] { "alpha", "beta", "" }, textBox.Lines);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void LinesGetterPreservesOriginalVirtualTextFailure()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox();
            var failure = new InvalidOperationException("lines text getter");
            textBox.TextReadFailure = failure;
            textBox.TextReads = 0;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => _ = textBox.Lines));

            Assert.Equal(1, textBox.TextReads);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void LinesSetterCallsVirtualTextOnceEvenForRepeatedEmptyAssignments()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { BypassTextSetter = true };
            textBox.TextWrites = 0;

            textBox.Lines = ["alpha", "beta"];

            Assert.Equal(1, textBox.TextWrites);
            Assert.Equal("alpha" + Environment.NewLine + "beta", textBox.LastAssignedText);
            textBox.Lines = null;
            Assert.Equal(2, textBox.TextWrites);
            Assert.Equal(string.Empty, textBox.LastAssignedText);
            textBox.Lines = [];
            Assert.Equal(3, textBox.TextWrites);
            Assert.Equal(string.Empty, textBox.LastAssignedText);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void LinesSetterPreservesOriginalVirtualTextFailureAndPreviousState()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "before" };
            var failure = new InvalidOperationException("lines text setter");
            textBox.TextWriteFailure = failure;
            textBox.TextWrites = 0;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.Lines = ["alpha", "beta"]));

            Assert.Equal(1, textBox.TextWrites);
            Assert.Equal("before", textBox.Text);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void RepeatedEquivalentLinesAssignmentsDoNotRepeatTextChanged()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            int notifications = 0;
            textBox.TextChanged += (_, _) => notifications++;

            textBox.Lines = ["alpha", "beta"];
            textBox.Lines = ["alpha", "beta"];

            Assert.Equal(1, notifications);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void RealHostedMultilineEditReceivesLinesOnceAndKeepsThemAcrossRecreation()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Multiline = true, Text = "before", ReadOnly = true };
            form.Controls.Add(textBox);
            _ = form.Handle;
            nint handle = textBox.Handle;
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.Same(form, Control.FromHandle(GetParent(handle)));
            textBox.Modified = true;
            int notifications = 0;
            textBox.TextChanged += (_, _) => notifications++;

            textBox.Lines = ["甲", "\U0001F600", ""];

            string expected = "甲" + Environment.NewLine + "\U0001F600" + Environment.NewLine;
            Assert.Equal(expected, NativeText(handle));
            Assert.Equal(new[] { "甲", "\U0001F600", "" }, textBox.Lines);
            Assert.False(textBox.Modified);
            Assert.True(textBox.ReadOnly);
            Assert.Equal(1, notifications);
            textBox.HideSelection = false;
            Assert.Equal(expected, NativeText(textBox.Handle));
            Assert.Equal(new[] { "甲", "\U0001F600", "" }, textBox.Lines);
            Assert.Equal(form.Handle, GetParent(textBox.Handle));
            Assert.Equal(1, notifications);
        });

    [Fact]
    public void RealLinesGetterReadsExternallyUpdatedNativeTextWithoutChangingIt()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Multiline = true };
            form.Controls.Add(textBox);
            _ = form.Handle;
            nint handle = textBox.Handle;
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.Same(form, Control.FromHandle(GetParent(handle)));
            Assert.True(SetWindowTextW(handle, "甲\r\n乙\r\n"));
            int notifications = 0;
            textBox.TextChanged += (_, _) => notifications++;

            Assert.Equal(new[] { "甲", "乙", "" }, textBox.Lines);

            Assert.Equal("甲\r\n乙\r\n", NativeText(handle));
            Assert.Equal(0, notifications);
        });

    private static string NativeText(nint handle)
    {
        var buffer = new StringBuilder(128);
        int copied = GetWindowTextW(handle, buffer, buffer.Capacity);
        Assert.InRange(copied, 0, buffer.Capacity - 1);
        return buffer.ToString();
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
                    typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                        BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox Lines test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ObservingTextBox : TextBox
    {
        internal int TextReads { get; set; }
        internal int TextWrites { get; set; }
        internal string? ReportedText { get; set; }
        internal string? LastAssignedText { get; private set; }
        internal Exception? TextReadFailure { get; set; }
        internal Exception? TextWriteFailure { get; set; }
        internal bool BypassTextSetter { get; set; }

        [AllowNull]
        public override string Text
        {
            get
            {
                TextReads++;
                if (TextReadFailure is not null) { throw TextReadFailure; }
                return ReportedText ?? base.Text;
            }
            set
            {
                TextWrites++;
                LastAssignedText = value;
                if (TextWriteFailure is not null) { throw TextWriteFailure; }
                if (!BypassTextSetter) { base.Text = value; }
            }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint handle, StringBuilder text, int maxCount);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetWindowTextW(nint handle, string text);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);
}
