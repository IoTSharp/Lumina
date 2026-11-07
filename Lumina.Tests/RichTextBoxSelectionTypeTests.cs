using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxSelectionTypeTests
{
    private const int EmSelectionType = 0x0442;
    private const int EmExSetSel = 0x0437;
    private const int EmGetSel = 0x00B0;
    private const int EmGetModify = 0x00B8;
    private const int EmCanUndo = 0x00C6;
    private const int EmGetLimitText = 0x00D5;

    [Fact]
    public void SelectionTypeHasTheStandardDeclaredReadOnlyContract()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.SelectionType),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(RichTextBoxSelectionTypes), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.False(property.GetMethod.IsVirtual);
        Assert.Null(property.SetMethod);
        Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
    }

    [Fact]
    public void SelectionTypesRetainsTheFiveStandardFlagsAndTheirIntIdentity()
    {
        Assert.Equal(typeof(int), Enum.GetUnderlyingType(typeof(RichTextBoxSelectionTypes)));
        Assert.NotNull(typeof(RichTextBoxSelectionTypes).GetCustomAttribute<FlagsAttribute>());
        Assert.Equal(new[] { "Empty", "Text", "Object", "MultiChar", "MultiObject" },
            Enum.GetNames<RichTextBoxSelectionTypes>());
        Assert.Equal(new[] { 0, 1, 2, 4, 8 },
            Enum.GetValues<RichTextBoxSelectionTypes>().Select(value => (int)value));
    }

    [Fact]
    public void EmptyGetterCreatesTheHandleBeforeReadingTheVirtualLength()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { ObserveLength = true };
            Assert.False(rich.IsHandleCreated);

            Assert.Equal(RichTextBoxSelectionTypes.Empty, rich.SelectionType);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.True(rich.HandleExistedAtLastLengthRead);
            Assert.Equal(0, rich.TypeMessages.Count);
        });

    [Fact]
    public void GetterMaterializesTheCachedTextBeforeReadingTheEmptySelection()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "cached text" };
            Assert.False(rich.IsHandleCreated);

            Assert.Equal(RichTextBoxSelectionTypes.Empty, rich.SelectionType);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal("cached text", rich.Text);
            Assert.Equal(0, rich.SelectionStart);
            Assert.Equal(0, rich.SelectionLength);
            Assert.Empty(rich.TypeMessages);
        });

    [Fact]
    public void CreationFailurePrecedesTheLengthReadAndPreservesItsOriginalExceptionForRetry()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { FailCreation = true, ObserveLength = true };

            Assert.Same(rich.CreationFailure,
                Assert.Throws<InvalidOperationException>(() => _ = rich.SelectionType));
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.ObservedLengthReads);
            Assert.Empty(rich.TypeMessages);

            rich.FailCreation = false;
            Assert.Equal(RichTextBoxSelectionTypes.Empty, rich.SelectionType);
            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
            Assert.True(rich.HandleExistedAtLastLengthRead);
        });

    [Fact]
    public void LengthFailurePropagatesTheOriginalExceptionWithoutIssuingATypeQuery()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            _ = rich.Handle;
            rich.ObserveLength = true;
            rich.FailLength = true;

            Assert.Same(rich.LengthFailure,
                Assert.Throws<InvalidOperationException>(() => _ = rich.SelectionType));
            Assert.Equal(1, rich.ObservedLengthReads);
            Assert.Empty(rich.TypeMessages);

            rich.FailLength = false;
            rich.ReturnedLength = 2;
            rich.InterceptType = true;
            rich.ReturnedType = 5;
            Assert.Equal((RichTextBoxSelectionTypes)5, rich.SelectionType);
            Assert.Single(rich.TypeMessages);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void NonPositiveVirtualLengthReturnsEmptyWithoutTheNativeSelectionTypeMessage(int length)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "nonempty" };
            _ = rich.Handle;
            rich.SelectAll();
            rich.ObserveLength = true;
            rich.ReturnedLength = length;
            rich.InterceptType = true;
            rich.ReturnedType = -1;

            Assert.Equal(RichTextBoxSelectionTypes.Empty, rich.SelectionType);

            Assert.Equal(1, rich.ObservedLengthReads);
            Assert.True(rich.HandleExistedAtLastLengthRead);
            Assert.Empty(rich.TypeMessages);
        });

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(int.MaxValue)]
    public void PositiveVirtualLengthSendsExactlyOneZeroParameterTypeQuery(int length)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            _ = rich.Handle;
            rich.ObserveLength = true;
            rich.ReturnedLength = length;
            rich.InterceptType = true;
            rich.ReturnedType = 9;

            Assert.Equal((RichTextBoxSelectionTypes)9, rich.SelectionType);

            Assert.Equal(1, rich.ObservedLengthReads);
            TypeMessage message = Assert.Single(rich.TypeMessages);
            Assert.Equal((nint)0, message.WParam);
            Assert.Equal((nint)0, message.LParam);
            Assert.Equal(1, message.LengthReadsBeforeQuery);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(8)]
    [InlineData(15)]
    [InlineData(128)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void NativeReturnIsCastDirectlyWithoutFilteringUnknownOrNegativeValues(int value)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            _ = rich.Handle;
            rich.ReturnedLength = 1;
            rich.InterceptType = true;
            rich.ReturnedType = value;

            Assert.Equal((RichTextBoxSelectionTypes)value, rich.SelectionType);
            Assert.Single(rich.TypeMessages);
        });

    [Fact]
    public void RepeatedGetterReadsTheLatestVirtualLengthAndNativeTypeWithoutCaching()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            _ = rich.Handle;
            rich.ObserveLength = true;
            rich.ReturnedLength = 2;
            rich.InterceptType = true;
            rich.ReturnedType = 1;
            Assert.Equal((RichTextBoxSelectionTypes)1, rich.SelectionType);
            rich.ReturnedType = 10;
            Assert.Equal((RichTextBoxSelectionTypes)10, rich.SelectionType);
            rich.ReturnedLength = 0;
            Assert.Equal(RichTextBoxSelectionTypes.Empty, rich.SelectionType);
            Assert.Equal(3, rich.ObservedLengthReads);
            Assert.Equal(2, rich.TypeMessages.Count);
        });

    [Fact]
    public void APositiveLengthGetterMayDestroyTheHandleAndTheTypeQueryCreatesItsReplacement()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "rebuild" };
            _ = rich.Handle;
            rich.ReturnedLength = 1;
            rich.DestroyAtNextLengthRead = true;
            rich.InterceptType = true;
            rich.ReturnedType = 9;

            Assert.Equal((RichTextBoxSelectionTypes)9, rich.SelectionType);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(2, rich.CreatedCount);
            Assert.Single(rich.TypeMessages);
            Assert.Equal("rebuild", rich.Text);
        });

    [Theory]
    [InlineData("", 0, 0, 0)]
    [InlineData("abcdef", 3, 0, 0)]
    [InlineData("abcdef", 1, 1, 1)]
    [InlineData("abcdef", 1, 3, 5)]
    [InlineData("中文内容", 1, 1, 1)]
    [InlineData("中文内容", 1, 2, 5)]
    public void RealRichEditReportsEmptySingleAndMultipleTextSelections(
        string text, int start, int length, int expected)
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = text };
            _ = rich.Handle;
            rich.Select(start, length);
            rich.TypeMessages.Clear();

            Assert.Equal((RichTextBoxSelectionTypes)expected, rich.SelectionType);

            Assert.Equal(start, rich.SelectionStart);
            Assert.Equal(length, rich.SelectionLength);
            Assert.Equal(length > 0 ? 1 : 0, rich.TypeMessages.Count);
        });

    [Fact]
    public void MixedCharacterAndParagraphFormattingStillReportsATextSelection()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox();
            rich.Rtf = @"{\rtf1\ansi plain\b bold\b0\par\pard\qc centered}";
            rich.SelectAll();
            string text = rich.Text;
            string selected = rich.SelectedText;
            rich.TypeMessages.Clear();

            Assert.Equal(RichTextBoxSelectionTypes.Text | RichTextBoxSelectionTypes.MultiChar,
                rich.SelectionType);

            Assert.Equal(text, rich.Text);
            Assert.Equal(selected, rich.SelectedText);
            Assert.Single(rich.TypeMessages);
        });

    [Fact]
    public void DirectNativeSelectionChangesAreObservedByTheNextGetter()
        => OnSta(() =>
        {
            using var rich = new SelectionRichTextBox { Text = "native current range" };
            nint handle = rich.Handle;

            SetNativeSelection(handle, 1, 2);
            Assert.Equal(RichTextBoxSelectionTypes.Text, rich.SelectionType);
            SetNativeSelection(handle, 2, 7);
            Assert.Equal(RichTextBoxSelectionTypes.Text | RichTextBoxSelectionTypes.MultiChar,
                rich.SelectionType);
            SetNativeSelection(handle, 3, 3);
            Assert.Equal(RichTextBoxSelectionTypes.Empty, rich.SelectionType);
            Assert.Equal(2, rich.TypeMessages.Count);
        });

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1, 5, 5)]
    public void ASeparateSystemRichEditAgreesForTheSameNonemptyTextRange(
        int start, int length, int expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new SelectionRichTextBox { Text = "independent text" };
            CreateHostedHandle(form, rich);
            nint native = CreateWindowExW(0, "RICHEDIT50W", "independent text",
                0x40000004u /* WS_CHILD | ES_MULTILINE */,
                0, 0, 180, 100, form.Handle, 0, GetModuleHandleW(null), 0);
            Assert.NotEqual((nint)0, native);
            try
            {
                SetNativeSelection(native, start, start + length);
                rich.Select(start, length);

                Assert.Equal((nint)expected, SendMessageW(native, EmSelectionType, 0, 0));
                Assert.Equal((RichTextBoxSelectionTypes)expected, rich.SelectionType);
                Assert.Null(Control.FromHandle(native));
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GetterPreservesTheDocumentNativeStateFormattingAndNotifications(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new SelectionRichTextBox { Text = "first\nsecond", MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            rich.SelectionColor = Color.Blue;
            rich.SelectionBullet = true;
            rich.SelectionAlignment = HorizontalAlignment.Center;
            rich.Modified = modified;
            NativeState before = ReadState(rich.Handle);
            int effects = ReadCharacterEffects(rich.Handle);
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            Assert.Equal((RichTextBoxSelectionTypes)5, rich.SelectionType);
            Assert.Equal((RichTextBoxSelectionTypes)5, rich.SelectionType);

            Assert.Equal(before, ReadState(rich.Handle));
            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(Color.Blue.ToArgb(), rich.SelectionColor.ToArgb());
            Assert.True(rich.SelectionBullet);
            Assert.Equal(HorizontalAlignment.Center, rich.SelectionAlignment);
            Assert.Equal(effects, ReadCharacterEffects(rich.Handle));
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void ReadingTheTypeDoesNotConsumeThePendingNativeUndo()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new SelectionRichTextBox { Text = "before" };
            CreateHostedHandle(form, rich);
            rich.Select(1, 2);
            rich.SelectedText = "XY";
            rich.Select(1, 2);
            Assert.True(rich.CanUndo);

            Assert.Equal((RichTextBoxSelectionTypes)5, rich.SelectionType);
            rich.Undo();

            Assert.Equal("before", rich.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RtfRecreationPreservesTheSelectionAndItsComputedType(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new SelectionRichTextBox { MaxLength = 3 };
            CreateHostedHandle(form, rich);
            rich.Rtf = @"{\rtf1\ansi\b formatted\b0\par second}";
            rich.Select(1, 3);
            rich.Modified = modified;
            string before = rich.Text;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal((RichTextBoxSelectionTypes)5, rich.SelectionType);
            Assert.Equal(before, rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    private static void CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        _ = rich.Handle;
    }

    private static void SetNativeSelection(nint handle, int start, int end)
    {
        nint range = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(range, 0, start);
            Marshal.WriteInt32(range, 4, end);
            _ = SendMessageW(handle, EmExSetSel, 0, range);
        }
        finally { Marshal.FreeHGlobal(range); }
    }

    private static NativeState ReadState(nint handle)
    {
        _ = SendMessageW(handle, EmGetSel, out int start, out int end);
        return new NativeState(start, end, SendMessageW(handle, EmGetModify, 0, 0) != 0,
            SendMessageW(handle, EmCanUndo, 0, 0) != 0);
    }

    private static int ReadCharacterEffects(nint handle)
    {
        nint format = Marshal.AllocHGlobal(116);
        try
        {
            Marshal.Copy(new byte[116], 0, format, 116);
            Marshal.WriteInt32(format, 116);
            _ = SendMessageW(handle, 0x043A, 1, format);
            return Marshal.ReadInt32(format, 8);
        }
        finally { Marshal.FreeHGlobal(format); }
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox selection type test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct TypeMessage(nint WParam, nint LParam, int LengthReadsBeforeQuery);
    private readonly record struct NativeState(int Start, int End, bool Modified, bool CanUndo);

    private sealed class SelectionRichTextBox : RichTextBox
    {
        internal bool ObserveLength { get; set; }
        internal int ObservedLengthReads { get; private set; }
        internal bool HandleExistedAtLastLengthRead { get; private set; }
        internal int? ReturnedLength { get; set; }
        internal bool FailLength { get; set; }
        internal bool DestroyAtNextLengthRead { get; set; }
        internal bool FailCreation { get; set; }
        internal int CreatedCount { get; private set; }
        internal bool InterceptType { get; set; }
        internal int ReturnedType { get; set; }
        internal List<TypeMessage> TypeMessages { get; } = [];
        internal InvalidOperationException CreationFailure { get; } = new("Selection type creation failure");
        internal InvalidOperationException LengthFailure { get; } = new("Selection type length failure");

        internal void RecreateNow() => RecreateHandle();

        public override int SelectionLength
        {
            get
            {
                if (ObserveLength)
                {
                    ObservedLengthReads++;
                    HandleExistedAtLastLengthRead = IsHandleCreated;
                }
                if (FailLength) { throw LengthFailure; }
                if (DestroyAtNextLengthRead)
                {
                    DestroyAtNextLengthRead = false;
                    DestroyHandle();
                }
                return ReturnedLength ?? base.SelectionLength;
            }
            set => base.SelectionLength = value;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                if (FailCreation) { throw CreationFailure; }
                return base.CreateParams;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == EmSelectionType)
            {
                TypeMessages.Add(new TypeMessage(message.WParam, message.LParam, ObservedLengthReads));
                if (InterceptType)
                {
                    message.Result = ReturnedType;
                    return;
                }
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, out int wParam, out int lParam);

    [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint GetModuleHandleW(string? moduleName);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);
}
