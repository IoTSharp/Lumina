using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxPositionQueryTests
{
    private const int EmPosFromChar = 0x00D6;
    private const int EmCharFromPos = 0x00D7;

    [Theory]
    [InlineData("GetCharFromPosition", typeof(char), typeof(Point), "pt")]
    [InlineData("GetCharIndexFromPosition", typeof(int), typeof(Point), "pt")]
    [InlineData("GetPositionFromCharIndex", typeof(Point), typeof(int), "index")]
    public void PositionQueriesDeclareStandardVirtualBaseSignatures(
        string name, Type resultType, Type parameterType, string parameterName)
    {
        MethodInfo method = typeof(TextBox).GetMethod(name, new[] { parameterType })!;
        Assert.Equal(typeof(TextBoxBase), method.DeclaringType);
        Assert.Equal(resultType, method.ReturnType);
        Assert.True(method.IsPublic && method.IsVirtual && !method.IsFinal);
        Assert.Equal(parameterName, Assert.Single(method.GetParameters()).Name);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, 65)]
    [InlineData(1, 0xD83D)]
    [InlineData(2, 0xDE00)]
    [InlineData(3, 90)]
    [InlineData(4, 0)]
    [InlineData(int.MaxValue, 0)]
    public void CharacterLookupUsesTextSnapshotAndVirtualUtf16Index(int index, int expected)
        => OnSta(() =>
        {
            using var textBox = new CharacterTextBox { Text = "A\uD83D\uDE00Z", Index = index };
            var point = new Point(-17, 39);
            // Text initialization legitimately consults the virtual getter.
            // Observe only the position query that follows that setup.
            textBox.Trace.Clear();

            Assert.Equal((char)expected, textBox.GetCharFromPosition(point));

            Assert.Equal(new[] { "text", "index" }, textBox.Trace);
            Assert.Equal(point, textBox.LastPoint);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void CharacterLookupKeepsSnapshotWhenVirtualIndexChangesText()
        => OnSta(() =>
        {
            using var textBox = new CharacterTextBox { Text = "abcd", Index = 2 };
            textBox.OnIndex = () => textBox.Text = "x";
            textBox.Trace.Clear();

            Assert.Equal('c', textBox.GetCharFromPosition(new Point(3, 4)));
            Assert.Equal("x", textBox.StoredText);
            // The callback's public Text setter also reads the virtual getter;
            // retain that observation after the snapshot and index query.
            Assert.Equal(new[] { "text", "index", "text" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void CharacterLookupPropagatesTextFailureBeforeVirtualIndex()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("snapshot");
            using var textBox = new CharacterTextBox { TextGetter = () => throw failure };

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.GetCharFromPosition(Point.Empty)));
            Assert.Equal(new[] { "text" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void CharacterLookupPropagatesVirtualIndexFailureAfterText()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("index");
            using var textBox = new CharacterTextBox { Text = "abc", IndexFailure = failure };
            textBox.Trace.Clear();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.GetCharFromPosition(Point.Empty)));
            Assert.Equal(new[] { "text", "index" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    public void CharacterLookupDoesNotNormalizeNullVirtualText(int index, bool throws)
        => OnSta(() =>
        {
            using var textBox = new CharacterTextBox { TextGetter = () => null!, Index = index };
            if (throws)
            {
                Assert.Throws<NullReferenceException>(() => textBox.GetCharFromPosition(Point.Empty));
            }
            else
            {
                Assert.Equal('\0', textBox.GetCharFromPosition(Point.Empty));
            }
            Assert.Equal(new[] { "text", "index" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(0, 0, 0L)]
    [InlineData(-1, -2, 4294901759L)]
    [InlineData(65537, 131075, 196609L)]
    [InlineData(int.MinValue, int.MaxValue, 4294901760L)]
    public void CharacterIndexSendsPackedClientPointBeforeReadingText(int x, int y, long packed)
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { Text = "abcd", CharacterResult = (nint)0x76540002 };
            _ = textBox.Handle;
            textBox.Arm();

            Assert.Equal(2, textBox.GetCharIndexFromPosition(new Point(x, y)));

            Assert.Equal(new[] { "char-message", "text" }, textBox.Trace);
            Assert.Equal((EmCharFromPos, (nint)0, (nint)packed), Assert.Single(textBox.Messages));
        });

    [Theory]
    [InlineData(0L, "abcd", 0)]
    [InlineData(0x7FFF0002L, "abcd", 2)]
    [InlineData(-1L, "abcd", 3)]
    [InlineData(0xFFFF8000L, "abcd", 3)]
    [InlineData(0x12340002L, "", 0)]
    public void CharacterIndexUsesUnsignedLowWordAndClampsToVirtualText(
        long nativeResult, string text, int expected)
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { Text = "native", CharacterResult = (nint)nativeResult };
            _ = textBox.Handle;
            textBox.TextGetter = () => text;
            textBox.Arm();

            Assert.Equal(expected, textBox.GetCharIndexFromPosition(Point.Empty));
            Assert.Equal(new[] { "char-message", "text" }, textBox.Trace);
            Assert.Single(textBox.Messages);
        });

    [Fact]
    public void CharacterIndexRetainsSixteenBitNativeIndexForLongText()
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { CharacterResult = (nint)0x1234FFFF };
            _ = textBox.Handle;
            string longText = new('x', 70000);
            textBox.TextGetter = () => longText;
            textBox.Arm();

            Assert.Equal(65535, textBox.GetCharIndexFromPosition(Point.Empty));
            Assert.Equal(new[] { "char-message", "text" }, textBox.Trace);
        });

    [Fact]
    public void CharacterIndexPropagatesTextFailureAfterSendingQuery()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("after query");
            using var textBox = new PositionTextBox { CharacterResult = 1 };
            _ = textBox.Handle;
            textBox.TextGetter = () => throw failure;
            textBox.Arm();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.GetCharIndexFromPosition(Point.Empty)));
            Assert.Equal(new[] { "char-message", "text" }, textBox.Trace);
            Assert.Single(textBox.Messages);
        });

    [Fact]
    public void CharacterIndexDoesNotNormalizeNullVirtualTextAfterQuery()
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { CharacterResult = 0 };
            _ = textBox.Handle;
            textBox.TextGetter = () => null!;
            textBox.Arm();

            Assert.Throws<NullReferenceException>(() => textBox.GetCharIndexFromPosition(Point.Empty));
            Assert.Equal(new[] { "char-message", "text" }, textBox.Trace);
        });

    [Fact]
    public void CharacterIndexAllowsOneFiniteQueryReentry()
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { Text = "abcd", CharacterResult = 3 };
            _ = textBox.Handle;
            textBox.Arm();
            textBox.OnQuery = () => Assert.Equal(3, textBox.GetCharIndexFromPosition(new Point(1, 2)));

            Assert.Equal(3, textBox.GetCharIndexFromPosition(Point.Empty));
            Assert.Equal(new[] { "char-message", "char-message", "text", "text" }, textBox.Trace);
            Assert.Equal(2, textBox.Messages.Count);
        });

    [Fact]
    public void CharacterIndexCreatesHandleAndPreservesCreationFailureForRetry()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("creation");
            using var textBox = new PositionTextBox { Text = "abc", CharacterResult = 1, CreationFailure = failure };

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.GetCharIndexFromPosition(Point.Empty)));
            Assert.False(textBox.IsHandleCreated);
            Assert.Empty(textBox.Messages);
            textBox.CreationFailure = null;

            Assert.Equal(1, textBox.GetCharIndexFromPosition(Point.Empty));
            Assert.True(textBox.IsHandleCreated);
            Assert.Single(textBox.Messages);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void NegativePositionIndexReturnsEmptyBeforeTextOrHandle(int index)
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { TextGetter = () => throw new InvalidOperationException("text") };
            textBox.Arm();

            Assert.Equal(Point.Empty, textBox.GetPositionFromCharIndex(index));
            Assert.Empty(textBox.Trace);
            Assert.Empty(textBox.Messages);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData("", 0)]
    [InlineData("abc", 3)]
    [InlineData("abc", int.MaxValue)]
    public void OutOfRangePositionIndexReadsTextOnceAndDoesNotCreateHandle(string text, int index)
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { Text = text };
            textBox.Arm();

            Assert.Equal(Point.Empty, textBox.GetPositionFromCharIndex(index));
            Assert.Equal(new[] { "text" }, textBox.Trace);
            Assert.Empty(textBox.Messages);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PositionIndexPropagatesTextFailureBeforeCreatingHandle()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("position text");
            using var textBox = new PositionTextBox { TextGetter = () => throw failure };
            textBox.Arm();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.GetPositionFromCharIndex(0)));
            Assert.Equal(new[] { "text" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PositionIndexDoesNotNormalizeNullVirtualText()
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { TextGetter = () => null! };
            textBox.Arm();

            Assert.Throws<NullReferenceException>(() => textBox.GetPositionFromCharIndex(0));
            Assert.Equal(new[] { "text" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(0L, 0, 0)]
    [InlineData(-1L, -1, -1)]
    [InlineData(0x7FFF8000L, -32768, 32767)]
    [InlineData(0xFFFF7FFFL, 32767, -1)]
    [InlineData(0x1234ABCDL, -21555, 4660)]
    public void PositionIndexSendsExactIndexAndUnpacksSignedCoordinates(long result, int x, int y)
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { Text = "abcd", PositionResult = (nint)result };
            _ = textBox.Handle;
            textBox.Arm();

            Assert.Equal(new Point(x, y), textBox.GetPositionFromCharIndex(3));
            Assert.Equal(new[] { "text", "position-message" }, textBox.Trace);
            Assert.Equal((EmPosFromChar, (nint)3, (nint)0), Assert.Single(textBox.Messages));
        });

    [Fact]
    public void ValidPositionIndexCreatesHandleAfterTextBoundsCheck()
        => OnSta(() =>
        {
            using var textBox = new PositionTextBox { Text = "abc", PositionResult = (nint)0x00020001 };
            Assert.False(textBox.IsHandleCreated);

            Assert.Equal(new Point(1, 2), textBox.GetPositionFromCharIndex(0));

            Assert.True(textBox.IsHandleCreated);
            Assert.Equal((EmPosFromChar, (nint)0, (nint)0), Assert.Single(textBox.Messages));
        });

    [Theory]
    [InlineData(0, 97)]
    [InlineData(1, 98)]
    [InlineData(4, 0x4E2D)]
    [InlineData(7, 90)]
    [InlineData(10, 120)]
    [InlineData(11, 121)]
    public void RealMultilineEditRoundTripsPhysicalCharacterPositionsWithoutEditing(int index, int expected)
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(360, 190) };
            nint parentHandle = parent.Handle;
            using var textBox = new TextBox
            {
                Parent = parent, Multiline = true, WordWrap = false,
                Size = new Size(320, 140), Text = "ab\r\n中\uD83D\uDE00Z\r\nxy"
            };
            nint handle = textBox.Handle;
            Assert.Equal(parentHandle, GetParent(handle));
            textBox.Select(4, 3);
            textBox.Modified = true;
            int textChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;

            Point point = textBox.GetPositionFromCharIndex(index);

            Assert.Equal(index, textBox.GetCharIndexFromPosition(point));
            Assert.Equal((char)expected, textBox.GetCharFromPosition(point));
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("ab\r\n中\uD83D\uDE00Z\r\nxy", textBox.Text);
            Assert.Equal(4, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.False(textBox.CanUndo);
            Assert.Equal(0, textChanges);
            Assert.Equal(new Point(-1, -1), DecodePosition(SendMessageW(handle, EmPosFromChar, (nint)int.MaxValue, 0)));
            if (index >= 4)
            {
                Assert.True(point.Y > textBox.GetPositionFromCharIndex(0).Y);
            }
        });

    [Fact]
    public void RealEmptyEditReturnsZeroIndexAndNullCharacter()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Size = new Size(200, 40) };

            Assert.Equal(0, textBox.GetCharIndexFromPosition(new Point(500, 500)));
            Assert.True(textBox.IsHandleCreated);
            Assert.Equal('\0', textBox.GetCharFromPosition(Point.Empty));
            Assert.Equal(Point.Empty, textBox.GetPositionFromCharIndex(0));
            Assert.Equal(string.Empty, textBox.Text);
        });

    private static Point DecodePosition(nint result)
        => new(unchecked((short)(int)result), unchecked((short)((int)result >> 16)));

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox position query test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class CharacterTextBox : TextBox
    {
        internal List<string> Trace { get; } = new();
        internal int Index { get; set; }
        internal Point LastPoint { get; private set; }
        internal Func<string>? TextGetter { get; set; }
        internal Exception? IndexFailure { get; set; }
        internal Action? OnIndex { get; set; }
        internal string StoredText => base.Text;

        [AllowNull]
        public override string Text
        {
            get { Trace.Add("text"); return TextGetter is null ? base.Text : TextGetter(); }
            set => base.Text = value;
        }

        public override int GetCharIndexFromPosition(Point pt)
        {
            Trace.Add("index");
            LastPoint = pt;
            if (IndexFailure is not null) { throw IndexFailure; }
            Action? callback = OnIndex;
            OnIndex = null;
            callback?.Invoke();
            return Index;
        }
    }

    private sealed class PositionTextBox : TextBox
    {
        private bool _armed;
        internal List<string> Trace { get; } = new();
        internal List<(int Message, nint WParam, nint LParam)> Messages { get; } = new();
        internal nint CharacterResult { get; set; }
        internal nint PositionResult { get; set; }
        internal Func<string>? TextGetter { get; set; }
        internal Exception? CreationFailure { get; set; }
        internal Action? OnQuery { get; set; }

        internal void Arm() { Trace.Clear(); Messages.Clear(); _armed = true; }

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
            => _armed ? throw new InvalidOperationException("TextLength must not replace Text.Length") : base.TextLength;

        protected override CreateParams CreateParams
        {
            get
            {
                if (CreationFailure is not null) { throw CreationFailure; }
                return base.CreateParams;
            }
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg is EmCharFromPos or EmPosFromChar)
            {
                Trace.Add(message.Msg == EmCharFromPos ? "char-message" : "position-message");
                Messages.Add((message.Msg, message.WParam, message.LParam));
                Action? callback = OnQuery;
                OnQuery = null;
                callback?.Invoke();
                message.Result = message.Msg == EmCharFromPos ? CharacterResult : PositionResult;
                return;
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);
}
