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
public sealed class TextBoxTextLengthTests
{
    private const int WmGetTextLength = 0x000E;
    private const int EmGetSel = 0x00B0;
    private const int EmSetSel = 0x00B1;
    private const int EmGetModify = 0x00B8;
    private const int EmReplaceSel = 0x00C2;
    private const int EmCanUndo = 0x00C6;

    [Fact]
    public void TextLengthKeepsTheStandardVirtualReadOnlyBaseDeclarationAndVisibility()
    {
        PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBoxBase.TextLength))!;
        Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
        Assert.Equal(typeof(int), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.True(property.GetMethod.IsVirtual);
        Assert.False(property.GetMethod.IsFinal || property.GetMethod.IsStatic);
        Assert.Equal(typeof(TextBoxBase), property.GetMethod.GetBaseDefinition().DeclaringType);
        Assert.Null(property.SetMethod);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>(false)!.Browsable);
        Assert.False(TypeDescriptor.GetProperties(typeof(TextBox))[nameof(TextBoxBase.TextLength)]!.IsBrowsable);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("abc", 3)]
    [InlineData("中😀\r\n", 5)]
    [InlineData("x\0y", 3)]
    public void PendingHandleReadsTheVirtualTextExactlyOnceWithoutCreatingAnEdit(string text, int expected)
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { TextOverrideEnabled = true, ReportedText = text };
            textBox.TextReads = 0;

            Assert.Equal(expected, textBox.TextLength);

            Assert.Equal(1, textBox.TextReads);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.CreatedCount);
        });

    [Fact]
    public void PendingHandlePropagatesTheOriginalVirtualGetterExceptionAndAllowsAnotherRead()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox();
            var failure = new InvalidOperationException("pending text length");
            textBox.TextReads = 0;
            textBox.TextFailure = failure;
            try
            {
                Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.TextLength));
                Assert.Equal(1, textBox.TextReads);
                Assert.False(textBox.IsHandleCreated);
            }
            finally { textBox.TextFailure = null; }

            textBox.TextOverrideEnabled = true;
            textBox.ReportedText = "recovered";
            Assert.Equal(9, textBox.TextLength);
            Assert.Equal(2, textBox.TextReads);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PendingHandleDoesNotNormalizeAnInvalidNullFromAVirtualTextGetter()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { TextOverrideEnabled = true, ReportedText = null };
            textBox.TextReads = 0;

            Assert.Throws<NullReferenceException>(() => textBox.TextLength);

            Assert.Equal(1, textBox.TextReads);
            Assert.False(textBox.IsHandleCreated);
            textBox.ReportedText = "ok";
            Assert.Equal(2, textBox.TextLength);
        });

    [Fact]
    public void ConsumersStillDispatchToADerivedTextLengthOverride()
        => OnSta(() =>
        {
            using var textBox = new OverrideTextBox();
            TextBoxBase editing = textBox;

            Assert.Equal(317, editing.TextLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void DisposedControlWithoutAHandleStillUsesTheVirtualTextBranch()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox();
            textBox.Dispose();
            textBox.TextOverrideEnabled = true;
            textBox.ReportedText = "after dispose";
            textBox.TextReads = 0;

            Assert.Equal(13, textBox.TextLength);

            Assert.Equal(1, textBox.TextReads);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.CreatedCount);
        });

    [Theory]
    [InlineData("", 0)]
    [InlineData("alpha", 5)]
    [InlineData("中😀", 3)]
    [InlineData("a\r\nb\r\n", 6)]
    [InlineData("e\u0301", 2)]
    [InlineData("abc\0tail", 3)]
    public void CreatedEditReportsItsNativeUtf16LengthInsteadOfTheVirtualText(string text, int expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Multiline = true, WordWrap = false };
            nint handle = CreateHostedEdit(form, textBox);
            Assert.True(SetWindowTextW(handle, text));
            textBox.TextOverrideEnabled = true;
            textBox.ReportedText = "a deliberately unrelated virtual value";
            textBox.TextReads = 0;
            textBox.CaptureLengthQueries = true;

            Assert.Equal(expected, textBox.TextLength);

            Assert.Equal(0, textBox.TextReads);
            Assert.Equal(new[] { ((nint)0, (nint)0) }, textBox.LengthQueries);
            Assert.Equal(expected, GetWindowTextLengthW(handle));
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(1, textBox.CreatedCount);
        });

    [Fact]
    public void CreatedEditBypassesAThrowingVirtualTextGetter()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Text = "native" };
            nint handle = CreateHostedEdit(form, textBox);
            var failure = new InvalidOperationException("must not read managed Text");
            textBox.TextFailure = failure;
            textBox.TextReads = 0;
            try
            {
                Assert.Equal(6, textBox.TextLength);
                Assert.Equal(6, textBox.TextLength);
                Assert.Equal(0, textBox.TextReads);
                Assert.Equal(handle, textBox.Handle);
            }
            finally { textBox.TextFailure = null; }
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void LengthQueriesPreserveNativeSelectionModifiedUndoAndManagedNotifications(bool readOnly)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Text = "abcdef", ReadOnly = readOnly };
            nint handle = CreateHostedEdit(form, textBox);
            _ = SendMessageW(handle, EmSetSel, 1, 3);
            _ = SendMessageW(handle, EmReplaceSel, 1, "中😀");
            Assert.Equal("a中😀def", ReadNativeText(handle));
            NativeState before = ReadNativeState(handle);
            Assert.True(before.Modified);
            Assert.True(before.CanUndo);
            int textChanges = 0;
            int modifiedChanges = 0;
            int selectionChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.ModifiedChanged += (_, _) => modifiedChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;
            textBox.TextFailure = new InvalidOperationException("length must not fetch Text");
            textBox.TextReads = 0;
            try
            {
                Assert.Equal(7, textBox.TextLength);
                Assert.Equal(7, textBox.TextLength);

                Assert.Equal(before, ReadNativeState(handle));
                Assert.Equal("a中😀def", ReadNativeText(handle));
                Assert.Equal(0, textBox.TextReads);
                Assert.Equal(0, textChanges);
                Assert.Equal(0, modifiedChanges);
                Assert.Equal(0, selectionChanges);
                Assert.Equal(handle, textBox.Handle);
                Assert.Equal(readOnly, textBox.ReadOnly);
            }
            finally { textBox.TextFailure = null; }
        });

    [Fact]
    public void NativeTextChangesAreObservedOnEveryReadWithoutCaching()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Multiline = true, WordWrap = false };
            nint handle = CreateHostedEdit(form, textBox);
            Assert.True(SetWindowTextW(handle, "old"));
            textBox.TextOverrideEnabled = true;
            textBox.ReportedText = "unrelated";
            textBox.TextReads = 0;

            Assert.Equal(3, textBox.TextLength);
            Assert.True(SetWindowTextW(handle, "中😀\r\nx"));
            Assert.Equal(6, textBox.TextLength);
            _ = SendMessageW(handle, EmSetSel, 0, 1);
            _ = SendMessageW(handle, EmReplaceSel, 1, "AB");
            Assert.Equal(7, textBox.TextLength);
            Assert.True(SetWindowTextW(handle, string.Empty));
            Assert.Equal(0, textBox.TextLength);
            Assert.Equal(0, textBox.TextReads);
            Assert.Equal(handle, textBox.Handle);
        });

    [Fact]
    public void RecreatedEditUsesTheNewNativeHandleAndDestroyedEditReturnsToVirtualText()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Text = "before" };
            _ = CreateHostedEdit(form, textBox);
            Assert.Equal(6, textBox.TextLength);

            textBox.RecreateNow();
            nint recreatedHandle = textBox.Handle;
            Assert.Equal(2, textBox.CreatedCount);
            Assert.True(SetWindowTextW(recreatedHandle, "重建😀"));
            textBox.TextOverrideEnabled = true;
            textBox.ReportedText = "pending virtual state";
            textBox.TextReads = 0;
            Assert.Equal(4, textBox.TextLength);
            Assert.Equal(0, textBox.TextReads);

            textBox.TextOverrideEnabled = false;
            textBox.DestroyNow();
            Assert.False(textBox.IsHandleCreated);
            textBox.TextOverrideEnabled = true;
            textBox.TextReads = 0;
            Assert.Equal(21, textBox.TextLength);
            Assert.Equal(1, textBox.TextReads);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(2, textBox.CreatedCount);
        });

    [Fact]
    public void FiniteNativeReentryKeepsBothQueriesOnTheNativeBranch()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ObservingTextBox { Text = "entry" };
            _ = CreateHostedEdit(form, textBox);
            textBox.CaptureLengthQueries = true;
            textBox.ReenterLengthOnce = true;
            textBox.TextFailure = new InvalidOperationException("reentry must not fetch Text");
            textBox.TextReads = 0;
            try
            {
                Assert.Equal(5, textBox.TextLength);

                Assert.Equal(5, textBox.ReentrantLength);
                Assert.Equal(new[] { ((nint)0, (nint)0), ((nint)0, (nint)0) }, textBox.LengthQueries);
                Assert.Equal(0, textBox.TextReads);
            }
            finally { textBox.TextFailure = null; }
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

    private static NativeState ReadNativeState(nint handle)
    {
        _ = SendMessageW(handle, EmGetSel, out int start, out int end);
        return new(start, end,
            SendMessageW(handle, EmGetModify, 0, 0) != 0,
            SendMessageW(handle, EmCanUndo, 0, 0) != 0);
    }

    private static string ReadNativeText(nint handle)
    {
        var text = new StringBuilder(64);
        int length = GetWindowTextW(handle, text, text.Capacity);
        Assert.InRange(length, 0, text.Capacity - 2);
        return text.ToString();
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox TextLength test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeState(int Start, int End, bool Modified, bool CanUndo);

    private sealed class ObservingTextBox : TextBox
    {
        internal int TextReads { get; set; }
        internal bool TextOverrideEnabled { get; set; }
        internal string? ReportedText { get; set; }
        internal Exception? TextFailure { get; set; }
        internal int CreatedCount { get; private set; }
        internal bool CaptureLengthQueries { get; set; }
        internal bool ReenterLengthOnce { get; set; }
        internal int? ReentrantLength { get; private set; }
        internal List<(nint WParam, nint LParam)> LengthQueries { get; } = new();

        [AllowNull]
        public override string Text
        {
            get
            {
                TextReads++;
                if (TextFailure is not null) { throw TextFailure; }
                return TextOverrideEnabled ? ReportedText! : base.Text;
            }
            set => base.Text = value;
        }

        internal void RecreateNow() => RecreateHandle();
        internal void DestroyNow() => DestroyHandle();

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void WndProc(ref Message message)
        {
            if (CaptureLengthQueries && message.Msg == WmGetTextLength)
            {
                LengthQueries.Add((message.WParam, message.LParam));
                if (ReenterLengthOnce)
                {
                    ReenterLengthOnce = false;
                    ReentrantLength = TextLength;
                }
            }
            base.WndProc(ref message);
        }
    }

    private sealed class OverrideTextBox : TextBox
    {
        public override int TextLength => 317;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowTextLengthW(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetWindowTextW(nint hWnd, string text);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint hWnd, StringBuilder text, int maxCount);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, string lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, out int wParam, out int lParam);
}
