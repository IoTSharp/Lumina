using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxLineQueryTests
{
    private const int EmLineIndex = 0x00BB;
    private const int EmLineFromChar = 0x00C9;
    private const int EmSetModify = 0x00B9;
    private const string NativeText = "alpha\r\n中😀\r\n\r\nomega\r\n";

    [Theory]
    [InlineData(nameof(TextBoxBase.GetLineFromCharIndex), true, "index")]
    [InlineData(nameof(TextBoxBase.GetFirstCharIndexFromLine), false, "lineNumber")]
    [InlineData(nameof(TextBoxBase.GetFirstCharIndexOfCurrentLine), false, null)]
    public void StandardMethodsAreDeclaredByTextBoxBase(string name, bool isVirtual, string? parameterName)
    {
        MethodInfo method = typeof(TextBox).GetMethod(name)!;
        Assert.Equal(typeof(TextBoxBase), method.DeclaringType);
        Assert.True(method.IsPublic);
        Assert.False(method.IsStatic);
        Assert.Equal(typeof(int), method.ReturnType);
        Assert.Equal(isVirtual, method.IsVirtual);
        ParameterInfo[] parameters = method.GetParameters();
        if (parameterName is null)
        {
            Assert.Empty(parameters);
        }
        else
        {
            ParameterInfo parameter = Assert.Single(parameters);
            Assert.Equal(parameterName, parameter.Name);
            Assert.Equal(typeof(int), parameter.ParameterType);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void ValidQueriesCreateTheEditOnce(int query)
        => OnSta(() =>
        {
            using var textBox = new QueryTextBox();
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, InvokeQuery(textBox, query));
            Assert.True(textBox.IsHandleCreated);
            nint handle = textBox.Handle;
            Assert.Equal(1, textBox.CreatedCount);
            Assert.Equal(0, InvokeQuery(textBox, query));
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(1, textBox.CreatedCount);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void OriginalCreationFailurePropagatesAndCanBeRetried(int query)
        => OnSta(() =>
        {
            var expected = new InvalidOperationException("line query creation");
            using var textBox = new QueryTextBox { CreationFailure = expected };
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => InvokeQuery(textBox, query)));
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.CreatedCount);
            textBox.CreationFailure = null;
            Assert.Equal(0, InvokeQuery(textBox, query));
            Assert.True(textBox.IsHandleCreated);
            Assert.Equal(1, textBox.CreatedCount);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(int.MinValue)]
    public void NegativeLineNumberIsRejectedBeforeCreatingOrReadingText(int lineNumber)
        => OnSta(() =>
        {
            using var textBox = new QueryTextBox { RejectTextRead = true };
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => textBox.GetFirstCharIndexFromLine(lineNumber));
            Assert.Equal("lineNumber", exception.ParamName);
            Assert.Equal(lineNumber, exception.ActualValue);
            Assert.False(textBox.IsHandleCreated);
            Assert.Equal(0, textBox.CreatedCount);
            Assert.Empty(textBox.Queries);
        });

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-2)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(7)]
    [InlineData(int.MaxValue)]
    public void CharacterIndexIsForwardedWithoutValidationOrManagedTextRead(int index)
        => OnSta(() =>
        {
            using var textBox = new QueryTextBox();
            nint handle = textBox.Handle;
            textBox.CaptureQueries = true;
            textBox.RejectTextRead = true;
            textBox.RejectSelectionRead = true;
            textBox.QueryResult = -7;

            Assert.Equal(-7, textBox.GetLineFromCharIndex(index));

            Query request = Assert.Single(textBox.Queries);
            Assert.Equal(new Query(EmLineFromChar, (nint)index, 0), request);
            Assert.Equal(handle, textBox.Handle);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(int.MaxValue)]
    public void NonnegativeLineNumberIsForwardedAndNativeResultIsPreserved(int lineNumber)
        => OnSta(() =>
        {
            using var textBox = new QueryTextBox();
            _ = textBox.Handle;
            textBox.CaptureQueries = true;
            textBox.RejectTextRead = true;
            textBox.RejectSelectionRead = true;
            textBox.QueryResult = -1;

            Assert.Equal(-1, textBox.GetFirstCharIndexFromLine(lineNumber));

            Assert.Equal(new Query(EmLineIndex, (nint)lineNumber, 0), Assert.Single(textBox.Queries));
        });

    [Fact]
    public void CurrentLineUsesTheNativeMinusOneSentinel()
        => OnSta(() =>
        {
            using var textBox = new QueryTextBox();
            _ = textBox.Handle;
            textBox.CaptureQueries = true;
            textBox.RejectTextRead = true;
            textBox.RejectSelectionRead = true;
            textBox.QueryResult = 123;

            Assert.Equal(123, textBox.GetFirstCharIndexOfCurrentLine());

            Assert.Equal(new Query(EmLineIndex, (nint)(-1), 0), Assert.Single(textBox.Queries));
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(4, 0)]
    [InlineData(5, 0)]
    [InlineData(6, 0)]
    [InlineData(7, 1)]
    [InlineData(8, 1)]
    [InlineData(9, 1)]
    [InlineData(10, 1)]
    [InlineData(11, 1)]
    [InlineData(12, 2)]
    [InlineData(13, 2)]
    [InlineData(14, 3)]
    [InlineData(20, 3)]
    [InlineData(21, 4)]
    [InlineData(int.MaxValue, 4)]
    public void RealEditUsesUtf16IndicesAndIncludesTheTrailingEmptyLine(int index, int expectedLine)
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = CreateNativeEdit(form);
            Assert.Equal(expectedLine, textBox.GetLineFromCharIndex(index));
            Assert.Equal(NativeText, textBox.Text);
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 7)]
    [InlineData(2, 12)]
    [InlineData(3, 14)]
    [InlineData(4, 21)]
    [InlineData(5, -1)]
    [InlineData(int.MaxValue, -1)]
    public void RealEditReturnsTheFirstUtf16IndexOrMinusOne(int lineNumber, int expectedIndex)
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = CreateNativeEdit(form);
            Assert.Equal(expectedIndex, textBox.GetFirstCharIndexFromLine(lineNumber));
        });

    [Theory]
    [InlineData(2, 0)]
    [InlineData(8, 7)]
    [InlineData(12, 12)]
    [InlineData(17, 14)]
    [InlineData(21, 21)]
    public void CurrentLineFollowsTheRealEditCaret(int caret, int expectedIndex)
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = CreateNativeEdit(form);
            textBox.Select(caret, 0);
            Assert.Equal(expectedIndex, textBox.GetFirstCharIndexOfCurrentLine());
            Assert.Equal(caret, textBox.SelectionStart);
            Assert.Equal(0, textBox.SelectionLength);
        });

    [Fact]
    public void WrappedPhysicalLinesAreQueriedWithoutInventingConceptualNewlines()
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = new TextBox
            {
                Multiline = true,
                WordWrap = true,
                Size = new Size(90, 160),
                Text = new string('a', 120)
            };
            form.Controls.Add(textBox);
            nint handle = textBox.Handle;
            Assert.Equal(form.Handle, GetParent(handle));

            int secondLine = textBox.GetFirstCharIndexFromLine(1);

            Assert.InRange(secondLine, 1, 119);
            Assert.Equal(1, textBox.GetLineFromCharIndex(secondLine));
            textBox.Select(secondLine, 0);
            Assert.Equal(secondLine, textBox.GetFirstCharIndexOfCurrentLine());
            Assert.DoesNotContain('\r', textBox.Text);
            Assert.DoesNotContain('\n', textBox.Text);
        });

    [Fact]
    public void QueriesKeepRealEditTextSelectionModifiedAndNotificationsUntouched()
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = CreateNativeEdit(form);
            nint handle = textBox.Handle;
            textBox.Select(8, 1);
            _ = SendMessageW(handle, EmSetModify, 1, 0);
            Assert.True(textBox.Modified);
            int textChanges = 0;
            int modifiedChanges = 0;
            int selectionChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.ModifiedChanged += (_, _) => modifiedChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;

            Assert.Equal(3, textBox.GetLineFromCharIndex(14));
            Assert.Equal(12, textBox.GetFirstCharIndexFromLine(2));
            Assert.Equal(7, textBox.GetFirstCharIndexOfCurrentLine());

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(NativeText, textBox.Text);
            Assert.Equal(8, textBox.SelectionStart);
            Assert.Equal(1, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.False(textBox.CanUndo);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, modifiedChanges);
            Assert.Equal(0, selectionChanges);
        });

    [Fact]
    public void FiniteNativeReentryPreservesEachQueryAndResult()
        => OnSta(() =>
        {
            using var textBox = new QueryTextBox();
            _ = textBox.Handle;
            textBox.CaptureQueries = true;
            textBox.OnQuery = message =>
            {
                if (message == EmLineFromChar)
                {
                    Assert.Equal(91, textBox.GetFirstCharIndexFromLine(2));
                    return 13;
                }
                return 91;
            };

            Assert.Equal(13, textBox.GetLineFromCharIndex(8));

            Assert.Collection(textBox.Queries,
                query => Assert.Equal(new Query(EmLineFromChar, 8, 0), query),
                query => Assert.Equal(new Query(EmLineIndex, 2, 0), query));
        });

    [Fact]
    public void CharacterLineQueryRemainsVirtualForConsumers()
        => OnSta(() =>
        {
            using var textBox = new OverrideTextBox();
            TextBoxBase editing = textBox;
            Assert.Equal(508, editing.GetLineFromCharIndex(8));
            Assert.False(textBox.IsHandleCreated);
        });

    private static int InvokeQuery(TextBoxBase editing, int query) => query switch
    {
        0 => editing.GetLineFromCharIndex(0),
        1 => editing.GetFirstCharIndexFromLine(0),
        2 => editing.GetFirstCharIndexOfCurrentLine(),
        _ => throw new ArgumentOutOfRangeException(nameof(query))
    };

    private static TextBox CreateNativeEdit(Form form)
    {
        var textBox = new TextBox
        {
            Multiline = true,
            WordWrap = false,
            Size = new Size(320, 180),
            Text = NativeText
        };
        form.Controls.Add(textBox);
        Assert.Equal(form.Handle, GetParent(textBox.Handle));
        Assert.Equal(NativeText, textBox.Text);
        return textBox;
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox line query test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct Query(int Message, nint WParam, nint LParam);

    private sealed class QueryTextBox : TextBox
    {
        internal int CreatedCount { get; private set; }
        internal Exception? CreationFailure { get; set; }
        internal bool CaptureQueries { get; set; }
        internal bool RejectTextRead { get; set; }
        internal bool RejectSelectionRead { get; set; }
        internal nint QueryResult { get; set; }
        internal Func<int, nint>? OnQuery { get; set; }
        internal List<Query> Queries { get; } = new();

        [AllowNull]
        public override string Text
        {
            get => RejectTextRead ? throw new InvalidOperationException("Unexpected Text read.") : base.Text;
            set => base.Text = value;
        }

        public override int SelectionLength
        {
            get => RejectSelectionRead ? throw new InvalidOperationException("Unexpected selection read.") : base.SelectionLength;
            set => base.SelectionLength = value;
        }

        protected override CreateParams CreateParams
            => CreationFailure is { } failure ? throw failure : base.CreateParams;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void WndProc(ref Message message)
        {
            if (CaptureQueries && message.Msg is EmLineFromChar or EmLineIndex)
            {
                Queries.Add(new Query(message.Msg, message.WParam, message.LParam));
                message.Result = OnQuery?.Invoke(message.Msg) ?? QueryResult;
                return;
            }
            base.WndProc(ref message);
        }
    }

    private sealed class OverrideTextBox : TextBox
    {
        public override int GetLineFromCharIndex(int index) => 500 + index;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);
}
