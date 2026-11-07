using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxSelectionMethodsTests
{
    private const int EmGetSel = 0x00B0;
    private const int EmSetSel = 0x00B1;

    [Fact]
    public void SelectionMethodsAreDeclaredOnStandardBaseWithNonvirtualPublicSignatures()
    {
        MethodInfo selectAll = typeof(TextBoxBase).GetMethod(nameof(TextBoxBase.SelectAll), Type.EmptyTypes)!;
        MethodInfo deselectAll = typeof(TextBoxBase).GetMethod(nameof(TextBoxBase.DeselectAll), Type.EmptyTypes)!;
        Assert.Equal(typeof(TextBoxBase), selectAll.DeclaringType);
        Assert.Equal(typeof(TextBoxBase), deselectAll.DeclaringType);
        Assert.Equal(typeof(void), selectAll.ReturnType);
        Assert.Equal(typeof(void), deselectAll.ReturnType);
        Assert.True(selectAll.IsPublic);
        Assert.True(deselectAll.IsPublic);
        Assert.False(selectAll.IsVirtual);
        Assert.False(deselectAll.IsVirtual);
        MethodInfo inheritedSelectAll = typeof(TextBox).GetMethod(nameof(TextBox.SelectAll), Type.EmptyTypes)!;
        Assert.Equal(typeof(TextBoxBase), inheritedSelectAll.DeclaringType);
        Assert.Equal(selectAll.Module, inheritedSelectAll.Module);
        Assert.Equal(selectAll.MetadataToken, inheritedSelectAll.MetadataToken);
        Assert.Equal(selectAll.Name, inheritedSelectAll.Name);
        Assert.Equal(selectAll.ReturnType, inheritedSelectAll.ReturnType);
        Assert.Empty(inheritedSelectAll.GetParameters());
        MethodInfo inheritedDeselectAll = typeof(TextBox).GetMethod(nameof(TextBox.DeselectAll), Type.EmptyTypes)!;
        Assert.Equal(typeof(TextBoxBase), inheritedDeselectAll.DeclaringType);
        Assert.Equal(deselectAll.Module, inheritedDeselectAll.Module);
        Assert.Equal(deselectAll.MetadataToken, inheritedDeselectAll.MetadataToken);
        Assert.Equal(deselectAll.Name, inheritedDeselectAll.Name);
        Assert.Equal(deselectAll.ReturnType, inheritedDeselectAll.ReturnType);
        Assert.Empty(inheritedDeselectAll.GetParameters());

        MethodInfo selectInternal = typeof(TextBoxBase).GetMethod("SelectInternal", BindingFlags.Instance | BindingFlags.NonPublic)!;
        Assert.True(selectInternal.IsFamilyAndAssembly);
        Assert.True(selectInternal.IsVirtual);
        Assert.Equal(typeof(void), selectInternal.ReturnType);
        Assert.Equal(new[] { "selectionStart", "selectionLength", "textLength" },
            selectInternal.GetParameters().Select(parameter => parameter.Name));
        Assert.All(selectInternal.GetParameters(), parameter => Assert.Equal(typeof(int), parameter.ParameterType));
    }

    [Fact]
    public void SelectAllReadsVirtualTextLengthOnceAndUsesThatRange()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdefgh", ReportedTextLength = 4 };
            TextBoxBase editing = textBox;
            textBox.TextLengthReads = 0;

            editing.SelectAll();

            Assert.Equal(1, textBox.TextLengthReads);
            Assert.False(editing.IsHandleCreated);
            Assert.Equal(0, editing.SelectionStart);
            Assert.Equal(4, editing.SelectionLength);
            Assert.Equal(0, textBox.SelectionLengthWrites);
        });

    [Fact]
    public void SelectAllPropagatesOriginalVirtualTextLengthFailureBeforeChangingSelection()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdefgh" };
            textBox.Select(2, 3);
            var failure = new InvalidOperationException("text length");
            textBox.TextLengthFailure = failure;
            textBox.TextLengthReads = 0;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.SelectAll()));

            Assert.Equal(1, textBox.TextLengthReads);
            Assert.False(textBox.IsHandleCreated);
            textBox.TextLengthFailure = null;
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
        });

    [Fact]
    public void DeselectAllCallsVirtualSelectionLengthSetterEvenForRepeatedZero()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdefgh", BypassSelectionLengthSetter = true };
            textBox.Select(2, 3);
            TextBoxBase editing = textBox;

            editing.DeselectAll();
            editing.DeselectAll();

            Assert.Equal(2, textBox.SelectionLengthWrites);
            Assert.Equal(new[] { 0, 0 }, textBox.AssignedSelectionLengths);
            Assert.Equal(2, editing.SelectionStart);
            Assert.Equal(3, editing.SelectionLength);
            Assert.False(editing.IsHandleCreated);
        });

    [Fact]
    public void DeselectAllPreservesOriginalVirtualSetterFailureAndPendingSelection()
        => OnSta(() =>
        {
            using var textBox = new ObservingTextBox { Text = "abcdefgh" };
            textBox.Select(2, 3);
            var failure = new InvalidOperationException("selection length");
            textBox.SelectionLengthFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.DeselectAll()));

            Assert.Equal(1, textBox.SelectionLengthWrites);
            Assert.Equal(0, Assert.Single(textBox.AssignedSelectionLengths));
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData("")]
    [InlineData("abcdef")]
    [InlineData("甲\r\n乙")]
    [InlineData("ab\U0001F600cd")]
    public void PendingSelectAllAndDeselectAllUseUtf16RangeWithoutCreatingHandle(string text)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = text };
            TextBoxBase editing = textBox;
            int textChanges = 0;
            int selectionChanges = 0;
            editing.TextChanged += (_, _) => textChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;

            editing.SelectAll();

            Assert.Equal(0, editing.SelectionStart);
            Assert.Equal(text.Length, editing.SelectionLength);
            editing.DeselectAll();
            Assert.Equal(0, editing.SelectionStart);
            Assert.Equal(0, editing.SelectionLength);
            Assert.Equal(text, editing.Text);
            Assert.False(editing.IsHandleCreated);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, selectionChanges);
        });

    [Fact]
    public void PendingDeselectAllKeepsSelectionStartWhenItCollapsesRange()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "abcdefgh" };
            textBox.Select(2, 4);

            textBox.DeselectAll();

            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(0, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PendingSelectAllRetainsCapturedRangeAcrossTemporaryTextShortening()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "abcdefgh" };
            textBox.SelectAll();
            textBox.Text = "ab";
            Assert.Equal(0, textBox.SelectionStart);
            Assert.Equal(2, textBox.SelectionLength);

            textBox.Text = "abcdefgh";

            Assert.Equal(0, textBox.SelectionStart);
            Assert.Equal(8, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, "")]
    [InlineData(false, "abcdef")]
    [InlineData(true, "甲\r\n乙")]
    [InlineData(true, "ab\U0001F600cd")]
    public void RealHostedEditConsumesPendingFullSelectionAndKeepsItAcrossRecreation(bool multiline, string text)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Multiline = multiline, Text = text };
            textBox.SelectAll();
            Assert.False(textBox.IsHandleCreated);
            nint first = CreateHostedEdit(form, textBox);
            AssertNativeSelection(first, 0, text.Length);

            textBox.HideSelection = false;

            nint recreated = textBox.Handle;
            Assert.NotEqual(first, recreated);
            Assert.False(IsWindow(first));
            AssertHostedParent(form, recreated);
            AssertNativeSelection(recreated, 0, text.Length);
            Assert.Equal(text, textBox.Text);
            textBox.DeselectAll();
            AssertNativeSelection(recreated, 0, 0);
            Assert.Equal(0, textBox.SelectionLength);
        });

    [Fact]
    public void RealDeselectAllUsesCurrentNativeSelectionStartInsteadOfCachedRange()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdefgh" };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Select(0, 1);
            _ = SendMessageW(handle, EmSetSel, 2, 6);

            textBox.DeselectAll();

            AssertNativeSelection(handle, 2, 2);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(0, textBox.SelectionLength);
        });

    [Fact]
    public void RealSelectionMethodsPreserveTextModifiedFlagAndReadOnlyState()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdefgh", ReadOnly = true };
            nint handle = CreateHostedEdit(form, textBox);
            textBox.Modified = true;
            int textChanges = 0;
            int modifiedChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.ModifiedChanged += (_, _) => modifiedChanges++;

            textBox.SelectAll();
            AssertNativeSelection(handle, 0, 8);
            textBox.DeselectAll();

            AssertNativeSelection(handle, 0, 0);
            Assert.Equal("abcdefgh", textBox.Text);
            Assert.True(textBox.Modified);
            Assert.True(textBox.ReadOnly);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, modifiedChanges);
        });

    [Fact]
    public void RealSelectionMethodsKeepExistingConcreteExtensionAndSuppressSameRangeDuplicates()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdefgh" };
            nint handle = CreateHostedEdit(form, textBox);
            var ranges = new List<(int Start, int End)>();
            textBox.SelectionChanged += (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                ranges.Add(GetNativeSelection(handle));
            };

            textBox.SelectAll();
            textBox.SelectAll();
            textBox.DeselectAll();
            textBox.DeselectAll();

            Assert.Equal(new[] { (0, 8), (0, 0) }, ranges);
        });

    [Fact]
    public void RealSelectAllCommitsSelectionBeforePropagatingExistingExtensionFailure()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdefgh" };
            nint handle = CreateHostedEdit(form, textBox);
            var failure = new InvalidOperationException("selection handler");
            EventHandler handler = (_, _) => throw failure;
            textBox.SelectionChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.SelectAll()));

            AssertNativeSelection(handle, 0, 8);
            Assert.Equal(8, textBox.SelectionLength);
            textBox.SelectionChanged -= handler;
            textBox.SelectAll();
            AssertNativeSelection(handle, 0, 8);
        });

    [Fact]
    public void RealSelectionExtensionCanReenterDeselectAllAfterFullSelection()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcdefgh" };
            nint handle = CreateHostedEdit(form, textBox);
            var ranges = new List<(int Start, int End)>();
            textBox.SelectionChanged += (_, _) =>
            {
                (int start, int end) = GetNativeSelection(handle);
                ranges.Add((start, end));
                if (end == 8) { textBox.DeselectAll(); }
            };

            textBox.SelectAll();

            Assert.Equal(new[] { (0, 8), (0, 0) }, ranges);
            AssertNativeSelection(handle, 0, 0);
            Assert.Equal(0, textBox.SelectionLength);
        });

    private static nint CreateHostedEdit(Form form, TextBox textBox)
    {
        form.Controls.Add(textBox);
        _ = form.Handle;
        nint handle = textBox.Handle;
        AssertHostedParent(form, handle);
        return handle;
    }

    private static void AssertHostedParent(Form form, nint handle)
    {
        nint parent = GetParent(handle);
        Assert.Equal(form.Handle, parent);
        Assert.Same(form, Control.FromHandle(parent));
    }

    private static (int Start, int End) GetNativeSelection(nint handle)
    {
        _ = SendMessageW(handle, EmGetSel, out int start, out int end);
        return (start, end);
    }

    private static void AssertNativeSelection(nint handle, int start, int end)
        => Assert.Equal((start, end), GetNativeSelection(handle));

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox selection methods timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ObservingTextBox : TextBox
    {
        internal int TextLengthReads { get; set; }
        internal int? ReportedTextLength { get; set; }
        internal Exception? TextLengthFailure { get; set; }
        internal int SelectionLengthWrites { get; private set; }
        internal bool BypassSelectionLengthSetter { get; set; }
        internal Exception? SelectionLengthFailure { get; set; }
        internal List<int> AssignedSelectionLengths { get; } = new();

        public override int TextLength
        {
            get
            {
                TextLengthReads++;
                if (TextLengthFailure is not null) { throw TextLengthFailure; }
                return ReportedTextLength ?? base.TextLength;
            }
        }

        public override int SelectionLength
        {
            get => base.SelectionLength;
            set
            {
                SelectionLengthWrites++;
                AssignedSelectionLengths.Add(value);
                if (SelectionLengthFailure is not null) { throw SelectionLengthFailure; }
                if (!BypassSelectionLengthSetter) { base.SelectionLength = value; }
            }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, out int wParam, out int lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);
}
