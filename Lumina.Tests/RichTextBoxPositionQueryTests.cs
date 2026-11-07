using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxPositionQueryTests
{
    private const int EmCharFromPos = 0x00D7;
    private const int EmPosFromChar = 0x00D6;
    private const int EmExLineFromChar = 0x0436;

    [Theory]
    [InlineData(nameof(RichTextBox.GetCharIndexFromPosition), typeof(int), typeof(Point), "pt")]
    [InlineData(nameof(RichTextBox.GetPositionFromCharIndex), typeof(Point), typeof(int), "index")]
    [InlineData(nameof(RichTextBox.GetLineFromCharIndex), typeof(int), typeof(int), "index")]
    public void QueriesOverrideTheStandardVirtualBaseSlots(
        string name, Type resultType, Type parameterType, string parameterName)
    {
        MethodInfo method = typeof(RichTextBox).GetMethod(name,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly,
            null, [parameterType], null)!;
        Assert.NotNull(method);
        Assert.Equal(resultType, method.ReturnType);
        Assert.True(method.IsVirtual && !method.IsFinal);
        Assert.Equal(typeof(TextBoxBase), method.GetBaseDefinition().DeclaringType);
        Assert.Equal(parameterName, Assert.Single(method.GetParameters()).Name);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, -2)]
    [InlineData(65537, 131075)]
    [InlineData(int.MinValue, int.MaxValue)]
    public void CharacterIndexPassesAFullSignedPointAndKeepsMoreThanSixteenBits(int x, int y)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => new string('x', 70002);
            rich.CharacterResult = 70000;
            rich.Arm();

            Assert.Equal(70000, rich.GetCharIndexFromPosition(new Point(x, y)));

            Query query = Assert.Single(rich.Queries);
            Assert.Equal(EmCharFromPos, query.Message);
            Assert.Equal((nint)0, query.WParam);
            Assert.NotEqual((nint)0, query.LParam);
            Assert.Equal(new Point(x, y), query.Point);
            Assert.Equal(new[] { "char-message", "text" }, rich.Trace);
        });

    [Theory]
    [InlineData(int.MinValue, -2147483648)]
    [InlineData(-7, -7)]
    [InlineData(-1, -1)]
    [InlineData(0, 0)]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    [InlineData(int.MaxValue, 3)]
    public void CharacterIndexClampsOnlyResultsBeyondTheCurrentText(int result, int expected)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => "abcd";
            rich.CharacterResult = result;
            rich.Arm();

            Assert.Equal(expected, rich.GetCharIndexFromPosition(Point.Empty));
            Assert.Equal(new[] { "char-message", "text" }, rich.Trace);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void CharacterIndexForEmptyTextClampsToZero(int result)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => string.Empty;
            rich.CharacterResult = result;
            rich.Arm();

            Assert.Equal(0, rich.GetCharIndexFromPosition(new Point(100, 200)));
            Assert.Single(rich.Queries);
        });

    [Fact]
    public void CharacterIndexReadsTextAfterTheNativeMessageAndOnlyOnce()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            string text = "abcdef";
            int reads = 0;
            rich.TextGetter = () => { reads++; return text; };
            rich.CharacterResult = 4;
            rich.OnQuery = () => text = "x";
            rich.Arm();

            Assert.Equal(0, rich.GetCharIndexFromPosition(Point.Empty));
            Assert.Equal(1, reads);
            Assert.Equal(new[] { "char-message", "text" }, rich.Trace);
        });

    [Fact]
    public void CharacterIndexDoesNotCacheItsNativeResult()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => "abcdef";
            rich.CharacterResult = 1;
            rich.Arm();

            Assert.Equal(1, rich.GetCharIndexFromPosition(Point.Empty));
            rich.CharacterResult = 4;
            Assert.Equal(4, rich.GetCharIndexFromPosition(Point.Empty));
            Assert.Equal(2, rich.Queries.Count);
            Assert.Equal(new[] { "char-message", "text", "char-message", "text" }, rich.Trace);
        });

    [Fact]
    public void CharacterIndexPropagatesVirtualTextFailureAfterTheNativeQuery()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            var failure = new InvalidOperationException("rich character text");
            rich.TextGetter = () => throw failure;
            rich.Arm();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => rich.GetCharIndexFromPosition(Point.Empty)));
            Assert.Equal(new[] { "char-message", "text" }, rich.Trace);
        });

    [Fact]
    public void CharacterIndexDoesNotNormalizeAnInvalidNullVirtualText()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => null!;
            rich.CharacterResult = -1;
            rich.Arm();

            Assert.Throws<NullReferenceException>(() => rich.GetCharIndexFromPosition(Point.Empty));
            Assert.Equal(new[] { "char-message", "text" }, rich.Trace);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CharacterAndLineQueriesPropagateCreationFailureBeforeReadingText(bool line)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox { FailCreation = true };
            rich.TextGetter = () => throw new InvalidOperationException("must not read Text");
            rich.Arm();

            Assert.Same(rich.CreationFailure, Assert.Throws<InvalidOperationException>(() =>
            {
                if (line) { _ = rich.GetLineFromCharIndex(2); }
                else { _ = rich.GetCharIndexFromPosition(Point.Empty); }
            }));
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Queries);
            Assert.DoesNotContain("text", rich.Trace);

            rich.FailCreation = false;
            rich.TextGetter = null;
            rich.Disarm();
            if (line) { Assert.Equal(0, rich.GetLineFromCharIndex(0)); }
            else { Assert.Equal(0, rich.GetCharIndexFromPosition(Point.Empty)); }
            Assert.True(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
        });

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-7)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(70000)]
    [InlineData(int.MaxValue)]
    public void LineQueryUsesExtendedMessageAndSignedLParamWithoutReadingText(int index)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => throw new InvalidOperationException("line must not read Text");
            rich.LineResult = -19;
            rich.Arm();

            TextBoxBase editing = rich;
            Assert.Equal(-19, editing.GetLineFromCharIndex(index));

            Query query = Assert.Single(rich.Queries);
            Assert.Equal(EmExLineFromChar, query.Message);
            Assert.Equal((nint)0, query.WParam);
            Assert.Equal((nint)index, query.LParam);
            Assert.Equal(new[] { "line-message" }, rich.Trace);
        });

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(65537)]
    [InlineData(int.MaxValue)]
    public void LineQueryReturnsTheEntireNativeIntWithoutNormalization(int result)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.LineResult = result;
            rich.Arm();

            Assert.Equal(result, rich.GetLineFromCharIndex(3));
            Assert.Single(rich.Queries);
        });

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    public void NegativePositionIndexShortCircuitsBeforeTextAndHandle(int index)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox { FailCreation = true };
            rich.TextGetter = () => throw new InvalidOperationException("negative must not read Text");
            rich.Arm();

            Assert.Equal(Point.Empty, rich.GetPositionFromCharIndex(index));
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Trace);
            Assert.Empty(rich.Queries);
        });

    [Theory]
    [InlineData(4)]
    [InlineData(int.MaxValue)]
    public void PositionIndexAboveTheEndReadsTextButDoesNotCreateHandle(int index)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox { FailCreation = true };
            rich.TextGetter = () => "abc";
            rich.Arm();

            Assert.Equal(Point.Empty, rich.GetPositionFromCharIndex(index));
            Assert.Equal(new[] { "text" }, rich.Trace);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Queries);
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, -2)]
    [InlineData(-32769, 70001)]
    [InlineData(int.MinValue, int.MaxValue)]
    public void PositionAcceptsEndOfTextAndReturnsTheFullWrittenPoint(int x, int y)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => "abc";
            rich.PositionResult = new Point(x, y);
            rich.MessageResult = (nint)0x7FFF0001;
            rich.Arm();

            TextBoxBase editing = rich;
            Assert.Equal(new Point(x, y), editing.GetPositionFromCharIndex(3));

            Query query = Assert.Single(rich.Queries);
            Assert.Equal(EmPosFromChar, query.Message);
            Assert.NotEqual((nint)0, query.WParam);
            Assert.Equal((nint)3, query.LParam);
            Assert.Equal(Point.Empty, query.Point);
            Assert.Equal(new[] { "text", "position-message" }, rich.Trace);
        });

    [Fact]
    public void PositionOfEmptyTextStillSendsTheZeroIndex()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => string.Empty;
            rich.PositionResult = new Point(23, 47);
            rich.Arm();

            Assert.Equal(new Point(23, 47), rich.GetPositionFromCharIndex(0));
            Assert.Equal((nint)0, Assert.Single(rich.Queries).LParam);
            Assert.Equal(new[] { "text", "position-message" }, rich.Trace);
        });

    [Fact]
    public void PositionReadsVirtualTextExactlyOnceAndDoesNotUseVirtualTextLength()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            int reads = 0;
            rich.TextGetter = () => { reads++; return "abc"; };
            rich.PositionResult = new Point(4, 5);
            rich.Arm();

            Assert.Equal(new Point(4, 5), rich.GetPositionFromCharIndex(2));
            Assert.Equal(1, reads);
            Assert.Equal(new[] { "text", "position-message" }, rich.Trace);
        });

    [Fact]
    public void PositionPropagatesTextFailureBeforeHandleCreation()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox { FailCreation = true };
            var failure = new InvalidOperationException("position text");
            rich.TextGetter = () => throw failure;
            rich.Arm();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.GetPositionFromCharIndex(0)));
            Assert.Equal(new[] { "text" }, rich.Trace);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Queries);
        });

    [Fact]
    public void PositionDoesNotNormalizeNullVirtualText()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox { FailCreation = true };
            rich.TextGetter = () => null!;
            rich.Arm();

            Assert.Throws<NullReferenceException>(() => rich.GetPositionFromCharIndex(0));
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Queries);
        });

    [Fact]
    public void PositionPropagatesCreationFailureAfterValidatingTextAndCanRetry()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox { FailCreation = true };
            rich.TextGetter = () => "abc";
            rich.Arm();

            Assert.Same(rich.CreationFailure, Assert.Throws<InvalidOperationException>(
                () => rich.GetPositionFromCharIndex(3)));
            Assert.Equal(new[] { "text" }, rich.Trace);
            Assert.Empty(rich.Queries);
            Assert.False(rich.IsHandleCreated);

            rich.FailCreation = false;
            rich.TextGetter = null;
            rich.Disarm();
            _ = rich.GetPositionFromCharIndex(0);
            Assert.True(rich.IsHandleCreated);
        });

    [Fact]
    public void InheritedCharacterLookupDispatchesToTheRichIndexOverride()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox();
            _ = rich.Handle;
            rich.TextGetter = () => "A\uD83D\uDE00Z";
            rich.CharacterResult = 3;
            rich.Arm();

            TextBoxBase editing = rich;
            Assert.Equal('Z', editing.GetCharFromPosition(new Point(7, 9)));
            Assert.Equal(new[] { "text", "char-message", "text" }, rich.Trace);
            Assert.Equal(new Point(7, 9), Assert.Single(rich.Queries).Point);
        });

    [Theory]
    [InlineData(0, 97)]
    [InlineData(1, 98)]
    [InlineData(3, 0x4E2D)]
    [InlineData(6, 90)]
    [InlineData(8, 120)]
    public void RealRichEditRoundTripsLogicalUtf16IndexesAndMatchesNativeQueries(int index, int character)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new QueryRichTextBox
            {
                Size = new Size(360, 160), WordWrap = false, Text = "ab\n中\uD83D\uDE00Z\nxy\n"
            };
            CreateHostedHandle(form, rich);

            Point position = rich.GetPositionFromCharIndex(index);

            Assert.Equal(ReadPosition(rich.Handle, index), position);
            Assert.Equal(index, rich.GetCharIndexFromPosition(position));
            Assert.Equal(index, ReadIndex(rich.Handle, position));
            Assert.Equal((char)character, rich.GetCharFromPosition(position));
            Assert.Equal((int)SendMessageW(rich.Handle, EmExLineFromChar, 0, index),
                rich.GetLineFromCharIndex(index));
        });

    [Fact]
    public void RealRichEditQueriesIndexesAbove65535()
        => OnSta(() =>
        {
            string text = new string('x', 70000) + "\nlast";
            using var rich = new QueryRichTextBox
            {
                Size = new Size(360, 160), WordWrap = false, Text = text
            };
            nint handle = rich.Handle;

            Point position = rich.GetPositionFromCharIndex(70001);
            int nativeLine = (int)SendMessageW(handle, EmExLineFromChar, 0, 70001);

            Assert.Equal(ReadPosition(handle, 70001), position);
            Assert.Equal(70001, ReadIndex(handle, position));
            Assert.Equal(70001, rich.GetCharIndexFromPosition(position));
            // RichEdit can split very long paragraphs into physical lines even
            // without word wrapping. The logical second paragraph is therefore
            // not necessarily physical line 1; query the real native boundary.
            Assert.True(nativeLine > 1);
            Assert.Equal(nativeLine, rich.GetLineFromCharIndex(70001));
            Assert.Equal('l', rich.GetCharFromPosition(position));

            nint native = CreateWindowExW(0, "RICHEDIT50W", string.Empty,
                0x80000084, 0, 0, 360, 160, 0, 0, 0, 0);
            Assert.NotEqual((nint)0, native);
            try
            {
                _ = SendMessageW(native, 0x0030, SendMessageW(handle, 0x0031, 0, 0), 0);
                SendText(native, 0x000C, 0, text);
                // Physical boundaries also depend on each native window's
                // formatting and initialization. Compare the wrapper with its
                // own HWND, and retain both windows as explicit counterexamples
                // to treating a long second paragraph as physical line 1.
                int independentLine = (int)SendMessageW(native, EmExLineFromChar, 0, 70001);
                Assert.True(independentLine > 1);
                Assert.Equal(70001, ReadIndex(native, ReadPosition(native, 70001)));
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    [Fact]
    public void RealRichEditLineQueryReportsPhysicalWrappedLines()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox
            {
                Size = new Size(90, 180), WordWrap = true, Text = new string('x', 120)
            };
            nint handle = rich.Handle;
            int nativeLine = (int)SendMessageW(handle, EmExLineFromChar, 0, 100);

            Assert.True(nativeLine > 0);
            Assert.Equal(nativeLine, rich.GetLineFromCharIndex(100));
            Assert.Equal(0, rich.GetLineFromCharIndex(0));
        });

    [Theory]
    [InlineData("")]
    [InlineData("one\n")]
    public void RealRichEditEmptyAndTrailingNewlineQueriesKeepTheManagedEndBoundary(string text)
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox { Size = new Size(300, 120), Text = text };
            nint handle = rich.Handle;
            var point = new Point(1000, 1000);
            int native = ReadIndex(handle, point);
            int expected = native >= text.Length ? Math.Max(text.Length - 1, 0) : native;

            Assert.Equal(expected, rich.GetCharIndexFromPosition(point));
            Assert.Equal(ReadPosition(handle, text.Length), rich.GetPositionFromCharIndex(text.Length));
            Assert.Equal(Point.Empty, rich.GetPositionFromCharIndex(text.Length + 1));
            Assert.Equal(text, rich.Text);
        });

    [Fact]
    public void QueriesUseTheLatestDirectNativeTextAndSelection()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox { Size = new Size(300, 120), Text = "old" };
            nint handle = rich.Handle;
            SendText(handle, 0x000C, 0, "new\ntext");
            _ = SendMessageW(handle, 0x00B1, 5, 7);

            Point position = rich.GetPositionFromCharIndex(5);

            Assert.Equal("new\ntext", rich.Text);
            Assert.Equal(5, rich.GetCharIndexFromPosition(position));
            Assert.Equal(1, rich.GetLineFromCharIndex(5));
            Assert.Equal(ReadPosition(handle, 5), position);
            Assert.Equal(5, rich.SelectionStart);
            Assert.Equal(2, rich.SelectionLength);
        });

    [Fact]
    public void QueriesPreserveTextSelectionFormattingModifiedUndoAndNotifications()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new QueryRichTextBox
            {
                Size = new Size(350, 160), Text = "first\nsecond", MaxLength = 1024
            };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            rich.SelectionColor = Color.Blue;
            rich.SelectionIndent = 24;
            rich.Select(2, 0);
            SendText(rich.Handle, 0x00C2, 1, "Q");
            Assert.True(rich.CanUndo);
            rich.Select(1, 3);
            rich.Modified = true;
            string text = rich.Text;
            string rtf = rich.Rtf;
            int changes = 0;
            int modifiedChanges = 0;
            rich.TextChanged += (_, _) => changes++;
            rich.ModifiedChanged += (_, _) => modifiedChanges++;
            nint handle = rich.Handle;

            Point point = rich.GetPositionFromCharIndex(1);
            Assert.Equal(1, rich.GetCharIndexFromPosition(point));
            Assert.Equal(0, rich.GetLineFromCharIndex(1));

            Assert.Equal(text, rich.Text);
            Assert.Equal(rtf, rich.Rtf);
            Assert.Equal(handle, rich.Handle);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.True(rich.Modified);
            Assert.True(rich.CanUndo);
            Assert.Equal((nint)1024, SendMessageW(handle, 0x00D5, 0, 0));
            Assert.Equal(0, changes);
            Assert.Equal(0, modifiedChanges);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void QueriesRemainValidAfterRtfHandleRecreation(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new QueryRichTextBox
            {
                Size = new Size(350, 160), Text = "first\nsecond", MaxLength = 3
            };
            CreateHostedHandle(form, rich);
            rich.Select(1, 3);
            rich.SelectionColor = Color.Red;
            rich.SelectionIndent = 24;
            Point before = rich.GetPositionFromCharIndex(7);
            rich.Modified = modified;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(before, rich.GetPositionFromCharIndex(7));
            Assert.Equal(7, rich.GetCharIndexFromPosition(before));
            Assert.Equal(1, rich.GetLineFromCharIndex(7));
            Assert.Equal("first\nsecond", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal(0, changes);
        });

    [Fact]
    public void IndependentRichEditWindowUsesTheSameExtendedQueryProtocol()
        => OnSta(() =>
        {
            using var rich = new QueryRichTextBox
            {
                Size = new Size(350, 160), WordWrap = false, Text = "first\nsecond"
            };
            nint handle = rich.Handle;
            nint native = CreateWindowExW(0, "RICHEDIT50W", string.Empty,
                0x80000004, 0, 0, 350, 160, 0, 0, 0, 0);
            Assert.NotEqual((nint)0, native);
            try
            {
                SendText(native, 0x000C, 0, "first\nsecond");
                _ = SendMessageW(native, 0x0030, SendMessageW(handle, 0x0031, 0, 0), 0);
                Point independentPosition = ReadPosition(native, 7);
                Assert.Equal(7, ReadIndex(native, independentPosition));
                Assert.Equal((nint)1, SendMessageW(native, EmExLineFromChar, 0, 7));

                Point position = rich.GetPositionFromCharIndex(7);
                Assert.Equal(ReadPosition(handle, 7), position);
                Assert.Equal(7, rich.GetCharIndexFromPosition(position));
                Assert.Equal(1, rich.GetLineFromCharIndex(7));
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    private static void CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        _ = rich.Handle;
    }

    private static Point ReadPosition(nint handle, int index)
    {
        nint point = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(point, 0, 0);
            Marshal.WriteInt32(point, 4, 0);
            _ = SendMessageW(handle, EmPosFromChar, point, index);
            return new Point(Marshal.ReadInt32(point), Marshal.ReadInt32(point, 4));
        }
        finally { Marshal.FreeHGlobal(point); }
    }

    private static int ReadIndex(nint handle, Point value)
    {
        nint point = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(point, 0, value.X);
            Marshal.WriteInt32(point, 4, value.Y);
            return (int)SendMessageW(handle, EmCharFromPos, 0, point);
        }
        finally { Marshal.FreeHGlobal(point); }
    }

    private static void SendText(nint handle, int message, nint wParam, string text)
    {
        nint value = Marshal.StringToHGlobalUni(text);
        try { _ = SendMessageW(handle, message, wParam, value); }
        finally { Marshal.FreeHGlobal(value); }
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox position query test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct Query(int Message, nint WParam, nint LParam, Point Point);

    private sealed class QueryRichTextBox : RichTextBox
    {
        private bool _armed;
        internal List<string> Trace { get; } = [];
        internal List<Query> Queries { get; } = [];
        internal Func<string>? TextGetter { get; set; }
        internal int CharacterResult { get; set; }
        internal int LineResult { get; set; }
        internal Point PositionResult { get; set; }
        internal nint MessageResult { get; set; }
        internal bool FailCreation { get; set; }
        internal int CreatedCount { get; private set; }
        internal Action? OnQuery { get; set; }
        internal InvalidOperationException CreationFailure { get; } = new("rich query creation");

        internal void Arm() { Trace.Clear(); Queries.Clear(); _armed = true; }
        internal void Disarm() => _armed = false;
        internal void RecreateNow() => RecreateHandle();

        [AllowNull]
        public override string Text
        {
            get
            {
                if (_armed) { Trace.Add("text"); }
                return TextGetter is null ? base.Text : TextGetter();
            }
            set => base.Text = value;
        }

        public override int TextLength
        {
            get
            {
                if (_armed) { Trace.Add("text-length"); }
                return base.TextLength;
            }
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

        protected override void Dispose(bool disposing)
        {
            _armed = false;
            TextGetter = null;
            FailCreation = false;
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message message)
        {
            if (_armed && message.Msg is EmCharFromPos or EmPosFromChar or EmExLineFromChar)
            {
                if (message.Msg == EmCharFromPos)
                {
                    Trace.Add("char-message");
                    Queries.Add(new Query(message.Msg, message.WParam, message.LParam,
                        new Point(Marshal.ReadInt32(message.LParam), Marshal.ReadInt32(message.LParam, 4))));
                    message.Result = CharacterResult;
                }
                else if (message.Msg == EmPosFromChar)
                {
                    Trace.Add("position-message");
                    Queries.Add(new Query(message.Msg, message.WParam, message.LParam,
                        new Point(Marshal.ReadInt32(message.WParam), Marshal.ReadInt32(message.WParam, 4))));
                    Marshal.WriteInt32(message.WParam, 0, PositionResult.X);
                    Marshal.WriteInt32(message.WParam, 4, PositionResult.Y);
                    message.Result = MessageResult;
                }
                else
                {
                    Trace.Add("line-message");
                    Queries.Add(new Query(message.Msg, message.WParam, message.LParam, Point.Empty));
                    message.Result = LineResult;
                }
                Action? callback = OnQuery;
                OnQuery = null;
                callback?.Invoke();
                return;
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
    private static extern nint CreateWindowExW(uint extendedStyle, string className, string text,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);
}
