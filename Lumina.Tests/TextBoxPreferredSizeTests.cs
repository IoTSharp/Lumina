using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxPreferredSizeTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(90, 30)]
    [InlineData(int.MaxValue, int.MaxValue)]
    public void EmptyBorderlessTextKeepsTheFontHeightLowerBound(int width, int height)
        => OnSta(() =>
        {
            using var textBox = new MeasuringTextBox { BorderStyle = BorderStyle.None };
            textBox.SetFontHeight(123);

            Assert.Equal(new Size(0, 123), textBox.GetPreferredSize(new Size(width, height)));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void NullVirtualTextUsesTheEmptyMeasurementAndFontHeightLowerBound()
        => OnSta(() =>
        {
            using var textBox = new MeasuringTextBox { BorderStyle = BorderStyle.None };
            textBox.SetFontHeight(123);
            textBox.TextGetter = () => null!;

            Assert.Equal(new Size(0, 123), textBox.GetPreferredSize(Size.Empty));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void CoreConsumesTheProtectedClientConversionBeforeVirtualTextAndFont()
        => OnSta(() =>
        {
            using var textBox = new MeasuringTextBox { BorderStyle = BorderStyle.None, Text = "measurement" };
            textBox.SetFontHeight(123);
            textBox.Trace.Clear();

            Size preferred = textBox.GetPreferredSize(new Size(200, 200));

            Assert.True(preferred.Width > 0);
            Assert.Equal(123, preferred.Height);
            Assert.Equal(new[] { "size", "multiline", "text", "font" }, textBox.Trace);
            Assert.Equal(Size.Empty, textBox.LastClientSize);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ClientConversionFailurePrecedesTextAndFontReads()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("client conversion");
            using var textBox = new MeasuringTextBox { BorderStyle = BorderStyle.None };
            textBox.ClientSizeFailure = failure;
            textBox.Trace.Clear();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.GetPreferredSize(new Size(200, 200))));
            Assert.Equal(new[] { "size" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void VirtualTextFailurePrecedesTheVirtualFontRead()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("measurement text");
            using var textBox = new MeasuringTextBox { BorderStyle = BorderStyle.None };
            textBox.TextGetter = () => throw failure;
            textBox.Trace.Clear();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.GetPreferredSize(new Size(200, 200))));
            Assert.Equal(new[] { "size", "multiline", "text" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void VirtualFontFailurePropagatesAfterTheTextRead()
        => OnSta(() =>
        {
            var failure = new InvalidOperationException("measurement font");
            using var textBox = new MeasuringTextBox { BorderStyle = BorderStyle.None };
            textBox.FontGetter = () => throw failure;
            textBox.Trace.Clear();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.GetPreferredSize(new Size(200, 200))));
            Assert.Equal(new[] { "size", "multiline", "text", "font" }, textBox.Trace);
            Assert.False(textBox.IsHandleCreated);
            textBox.FontGetter = null;
        });

    [Fact]
    public void PreferredSizeFollowsTextInsteadOfTheAssignedBounds()
        => OnSta(() =>
        {
            using var textBox = new TextBox { AutoSize = false, BorderStyle = BorderStyle.None, Text = "i", Size = new Size(1000, 700) };
            Size shortText = textBox.GetPreferredSize(Size.Empty);
            textBox.Text = "MMMMMMMMMMMMMMMMMMMM";
            Size longText = textBox.GetPreferredSize(Size.Empty);

            Assert.True(longText.Width > shortText.Width);
            Assert.Equal(shortText.Height, longText.Height);
            Assert.Equal(new Size(1000, 700), textBox.Size);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AmpersandsRemainLiteralInsteadOfBecomingMnemonicPrefixes()
        => OnSta(() =>
        {
            using var textBox = new TextBox { BorderStyle = BorderStyle.None, Text = "A&B" };
            Size literal = textBox.GetPreferredSize(Size.Empty);
            textBox.Text = "AB";
            Size withoutAmpersand = textBox.GetPreferredSize(Size.Empty);

            Assert.True(literal.Width > withoutAmpersand.Width);
            Assert.Equal(literal.Height, withoutAmpersand.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData("First\r\nSecond")]
    [InlineData("中文\r\n文字")]
    public void MultilineHardBreaksIncreaseTheMeasuredHeight(string text)
        => OnSta(() =>
        {
            using var singleLine = new TextBox { BorderStyle = BorderStyle.None, Text = text };
            using var multiline = new TextBox { BorderStyle = BorderStyle.None, Text = text, Multiline = true };

            Size single = singleLine.GetPreferredSize(Size.Empty);
            Size multiple = multiline.GetPreferredSize(Size.Empty);

            Assert.True(multiple.Height > single.Height);
            Assert.True(multiple.Width > 0);
            Assert.False(singleLine.IsHandleCreated);
            Assert.False(multiline.IsHandleCreated);
        });

    [Fact]
    public void MultilineWordWrapUsesTheAvailableWidth()
        => OnSta(() =>
        {
            using var textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Multiline = true,
                Text = "word word word word word word word word word word"
            };
            Size unrestricted = textBox.GetPreferredSize(Size.Empty);
            Size narrow = textBox.GetPreferredSize(new Size(80, int.MaxValue));

            Assert.True(narrow.Height > unrestricted.Height);
            Assert.True(narrow.Width <= 80);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void DisablingMultilineWordWrapKeepsTheLineUnwrapped()
        => OnSta(() =>
        {
            using var textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Multiline = true,
                WordWrap = false,
                Text = "word word word word word word word word word word"
            };
            Size unrestricted = textBox.GetPreferredSize(Size.Empty);
            Size narrow = textBox.GetPreferredSize(new Size(50, int.MaxValue));

            Assert.Equal(unrestricted, narrow);
            Assert.True(narrow.Width > 50);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void SingleLineTextDoesNotWrapToTheProposedWidth()
        => OnSta(() =>
        {
            using var textBox = new TextBox { BorderStyle = BorderStyle.None, Text = "A long single line of text" };
            Size unrestricted = textBox.GetPreferredSize(Size.Empty);

            Assert.Equal(unrestricted, textBox.GetPreferredSize(new Size(1, int.MaxValue)));
            Assert.True(unrestricted.Width > 1);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaddingAddsItsFourSidesToTheUnconstrainedMeasurement()
        => OnSta(() =>
        {
            using var textBox = new TextBox { BorderStyle = BorderStyle.None, Text = "padding" };
            Size withoutPadding = textBox.GetPreferredSize(Size.Empty);
            textBox.Padding = new Padding(2, 3, 5, 7);

            Assert.Equal(new Size(withoutPadding.Width + 7, withoutPadding.Height + 10),
                textBox.GetPreferredSize(Size.Empty));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaddingReducesTheWidthAvailableToWrappedText()
        => OnSta(() =>
        {
            using var textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Multiline = true,
                Text = "word word word word word word word word word word"
            };
            Size unpadded = textBox.GetPreferredSize(new Size(100, int.MaxValue));
            textBox.Padding = new Padding(25, 0, 25, 0);
            Size padded = textBox.GetPreferredSize(new Size(100, int.MaxValue));

            Assert.True(padded.Height > unpadded.Height);
            Assert.True(padded.Width <= 100);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(BorderStyle.None, 6, 208)]
    [InlineData(BorderStyle.FixedSingle, 8, 213)]
    [InlineData(BorderStyle.Fixed3D, 6, 211)]
    public void BorderCompatibilityInsetsSupplementTheProtectedClientConversion(
        BorderStyle borderStyle, int width, int height)
        => OnSta(() =>
        {
            using var textBox = new MeasuringTextBox { BorderStyle = borderStyle, ClientExtra = new Size(6, 8) };
            textBox.SetFontHeight(200);

            Assert.Equal(new Size(width, height), textBox.GetPreferredSize(new Size(400, 400)));
            Assert.Equal(Size.Empty, textBox.LastClientSize);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PublicMinimumSizeStillConstrainsTheMeasuredResult()
        => OnSta(() =>
        {
            using var textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Text = "i",
                MinimumSize = new Size(200, 80)
            };

            Assert.Equal(new Size(200, 80), textBox.GetPreferredSize(new Size(1, 1)));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PublicMaximumSizeStillClampsTheMeasuredResult()
        => OnSta(() =>
        {
            using var textBox = new MeasuringTextBox
            {
                BorderStyle = BorderStyle.None,
                Text = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM",
                MaximumSize = new Size(50, 20)
            };
            textBox.SetFontHeight(200);

            Assert.Equal(new Size(50, 20), textBox.GetPreferredSize(Size.Empty));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void FontChangesAffectTheNextMeasurementWithoutCreatingAHandle()
        => OnSta(() =>
        {
            using var small = new Font("Segoe UI", 8);
            using var large = new Font("Segoe UI", 20);
            using var textBox = new TextBox { BorderStyle = BorderStyle.None, Text = "MMMMMMMM", Font = small };
            Size before = textBox.GetPreferredSize(Size.Empty);
            textBox.Font = large;
            Size after = textBox.GetPreferredSize(Size.Empty);

            Assert.True(after.Width > before.Width);
            Assert.True(after.Height > before.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MeasurementLeavesNativeEditingStateAndNotificationsUnchanged()
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = new TextBox { BorderStyle = BorderStyle.None, Text = "native 中文 text", Size = new Size(180, 50) };
            form.Controls.Add(textBox);
            nint handle = textBox.Handle;
            textBox.Select(2, 5);
            textBox.Modified = true;
            string text = textBox.Text;
            Size bounds = textBox.Size;
            bool undo = textBox.CanUndo;
            int textChanged = 0;
            int modifiedChanged = 0;
            int handleCreated = 0;
            textBox.TextChanged += (_, _) => textChanged++;
            textBox.ModifiedChanged += (_, _) => modifiedChanged++;
            textBox.HandleCreated += (_, _) => handleCreated++;

            Size preferred = textBox.GetPreferredSize(new Size(120, 100));

            Assert.True(preferred.Width > 0 && preferred.Height > 0);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(text, textBox.Text);
            Assert.Equal(bounds, textBox.Size);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.Equal(undo, textBox.CanUndo);
            Assert.Equal(0, textChanged);
            Assert.Equal(0, modifiedChanged);
            Assert.Equal(0, handleCreated);
        });

    [Fact]
    public void TextChangedHandlerCanObserveTheNewMeasuredTextWithoutParentRelayout()
        => OnSta(() =>
        {
            using var parent = new Control();
            using var textBox = new TextBox { BorderStyle = BorderStyle.None, Text = "i" };
            parent.Controls.Add(textBox);
            Size before = textBox.GetPreferredSize(Size.Empty);
            Size fromEvent = Size.Empty;
            int layouts = 0;
            parent.Layout += (_, _) => layouts++;
            textBox.TextChanged += (_, _) => fromEvent = textBox.GetPreferredSize(Size.Empty);

            textBox.Text = "MMMMMMMMMMMMMMMMMMMM";

            Assert.True(fromEvent.Width > before.Width);
            Assert.Equal(fromEvent, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(0, layouts);
            Assert.False(textBox.IsHandleCreated);
        });

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox preferred-size test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class MeasuringTextBox : TextBox
    {
        internal List<string> Trace { get; } = new();
        internal Size ClientExtra { get; set; }
        internal Size LastClientSize { get; private set; }
        internal Exception? ClientSizeFailure { get; set; }
        internal Func<string>? TextGetter { get; set; }
        internal Func<Font>? FontGetter { get; set; }

        internal void SetFontHeight(int height) => FontHeight = height;

        [AllowNull]
        public override string Text
        {
            get { Trace.Add("text"); return TextGetter is null ? base.Text : TextGetter(); }
            set => base.Text = value;
        }

        [AllowNull]
        public override Font Font
        {
            get { Trace.Add("font"); return FontGetter is null ? base.Font : FontGetter(); }
            set => base.Font = value;
        }

        public override bool Multiline
        {
            get { Trace.Add("multiline"); return base.Multiline; }
            set => base.Multiline = value;
        }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            Trace.Add("size");
            LastClientSize = clientSize;
            if (ClientSizeFailure is not null) { throw ClientSizeFailure; }
            return clientSize + ClientExtra;
        }
    }
}
