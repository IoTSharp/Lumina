using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxPreferredCacheTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(int.MaxValue, int.MaxValue)]
    public void RepeatedUnconstrainedRequestsReuseTheMeasuredSize(int width, int height)
        => OnSta(() =>
        {
            using var textBox = new CountingTextBox { BorderStyle = BorderStyle.None, Text = "cached text" };
            textBox.ResetMeasurements();

            Size first = textBox.GetPreferredSize(new Size(width, height));
            Size second = textBox.GetPreferredSize(Size.Empty);

            Assert.True(first.Width > 0 && first.Height > 0);
            Assert.Equal(first, second);
            Assert.Equal(1, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ConstrainedRequestsMeasureEachTimeAndLeaveTheUnconstrainedCacheIntact()
        => OnSta(() =>
        {
            using var textBox = new CountingTextBox
            {
                BorderStyle = BorderStyle.None,
                Multiline = true,
                Text = "word word word word word word word word word word"
            };
            textBox.ResetMeasurements();
            Size unrestricted = textBox.GetPreferredSize(Size.Empty);
            Size narrow = textBox.GetPreferredSize(new Size(65, int.MaxValue));

            Assert.Equal(narrow, textBox.GetPreferredSize(new Size(65, int.MaxValue)));
            Assert.True(narrow.Height > unrestricted.Height);
            Assert.Equal(3, textBox.Measurements);
            Assert.Equal(unrestricted, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(3, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void FailedMeasurementsPropagateAndAreNotCached()
        => OnSta(() =>
        {
            using var textBox = new CountingTextBox { BorderStyle = BorderStyle.None, Text = "retry measurement" };
            var failure = new InvalidOperationException("measurement conversion");
            textBox.MeasurementFailure = failure;
            textBox.ResetMeasurements();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.GetPreferredSize(Size.Empty)));
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.GetPreferredSize(Size.Empty)));
            Assert.Equal(2, textBox.Measurements);

            textBox.MeasurementFailure = null;
            Size successful = textBox.GetPreferredSize(Size.Empty);
            Assert.Equal(successful, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(3, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void TextChangedReadsTheNewMeasurementBeforeNotificationWithoutParentLayout()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(600, 400) };
            using var textBox = new CountingTextBox { BorderStyle = BorderStyle.None, Text = "i" };
            parent.Controls.Add(textBox);
            Size before = textBox.GetPreferredSize(Size.Empty);
            textBox.ResetMeasurements();
            int layouts = 0;
            Size inEvent = Size.Empty;
            parent.Layout += (_, _) => layouts++;
            textBox.TextChanged += (_, _) =>
            {
                inEvent = textBox.GetPreferredSize(Size.Empty);
                Assert.True(inEvent.Width > before.Width);
                Assert.Equal(1, textBox.Measurements);
            };

            textBox.Text = "MMMMMMMMMMMMMMMMMMMM";

            Assert.Equal(inEvent, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.Equal(0, layouts);
            Assert.False(parent.IsHandleCreated);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ThrowingTextChangedHandlerStillLeavesTheOldCacheInvalidated()
        => OnSta(() =>
        {
            using var textBox = new CountingTextBox { BorderStyle = BorderStyle.None, Text = "i" };
            Size before = textBox.GetPreferredSize(Size.Empty);
            var failure = new InvalidOperationException("text changed");
            EventHandler handler = (_, _) => throw failure;
            textBox.TextChanged += handler;
            textBox.ResetMeasurements();

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.Text = "MMMMMMMMMMMMMMMM"));

            textBox.TextChanged -= handler;
            Size after = textBox.GetPreferredSize(Size.Empty);
            Assert.True(after.Width > before.Width);
            Assert.Equal(after, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AssigningTheSameTextKeepsTheCachedMeasurement()
        => OnSta(() =>
        {
            using var textBox = new CountingTextBox { BorderStyle = BorderStyle.None, Text = "unchanged" };
            Size before = textBox.GetPreferredSize(Size.Empty);
            textBox.ResetMeasurements();

            textBox.Text = "unchanged";

            Assert.Equal(before, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(0, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void WordWrapAssignmentsInvalidateTheCacheIncludingSameValue(bool autoSize, bool changeValue)
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(600, 400) };
            using var textBox = new CountingTextBox
            {
                BorderStyle = BorderStyle.None,
                Multiline = true,
                AutoSize = autoSize,
                Text = "word word word word word word"
            };
            parent.Controls.Add(textBox);
            _ = textBox.GetPreferredSize(Size.Empty);
            textBox.ResetMeasurements();
            int layouts = 0;
            parent.Layout += (_, _) => layouts++;

            textBox.WordWrap = !changeValue;

            Size refreshed = textBox.GetPreferredSize(Size.Empty);
            Assert.Equal(refreshed, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.Equal(autoSize ? 1 : 0, layouts);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void MultilineChangesDiscardThePreviouslyCachedSingleLineSize(bool autoSize)
        => OnSta(() =>
        {
            using var textBox = new CountingTextBox
            {
                BorderStyle = BorderStyle.None,
                AutoSize = autoSize,
                Text = "first\r\nsecond"
            };
            Size before = textBox.GetPreferredSize(Size.Empty);
            textBox.ResetMeasurements();

            textBox.Multiline = true;

            Size after = textBox.GetPreferredSize(Size.Empty);
            Assert.True(after.Height > before.Height);
            Assert.Equal(after, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void BorderStyleNotificationSeesTheNewBorderMeasurement(bool autoSize)
        => OnSta(() =>
        {
            using var textBox = new CountingTextBox
            {
                AutoSize = autoSize,
                BorderStyle = BorderStyle.None,
                Text = "border change"
            };
            Size before = textBox.GetPreferredSize(Size.Empty);
            Size inEvent = Size.Empty;
            textBox.ResetMeasurements();
            textBox.BorderStyleChanged += (_, _) =>
            {
                inEvent = textBox.GetPreferredSize(Size.Empty);
                Assert.True(inEvent.Width > before.Width);
                Assert.True(inEvent.Height > before.Height);
                Assert.Equal(1, textBox.Measurements);
            };

            textBox.BorderStyle = BorderStyle.Fixed3D;

            Assert.Equal(inEvent, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaddingNotificationSeesTheNewInsetsAndTheFinalResultIsCached()
        => OnSta(() =>
        {
            using var textBox = new CountingTextBox { BorderStyle = BorderStyle.None, Text = "padding change" };
            Size before = textBox.GetPreferredSize(Size.Empty);
            Size inEvent = Size.Empty;
            textBox.ResetMeasurements();
            textBox.PaddingChanged += (_, _) =>
            {
                inEvent = textBox.GetPreferredSize(Size.Empty);
                Assert.Equal(new Size(before.Width + 7, before.Height + 10), inEvent);
                Assert.Equal(1, textBox.Measurements);
            };

            textBox.Padding = new Padding(2, 3, 5, 7);

            Size after = textBox.GetPreferredSize(Size.Empty);
            Assert.Equal(inEvent, after);
            int finalMeasurements = textBox.Measurements;
            Assert.Equal(after, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(finalMeasurements, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void FontNotificationSeesTheNewFontAndTheFinalResultIsCached()
        => OnSta(() =>
        {
            using var small = new Font("Segoe UI", 8);
            using var large = new Font("Segoe UI", 22);
            using var textBox = new CountingTextBox { BorderStyle = BorderStyle.None, Font = small, Text = "MMMMMMMM" };
            Size before = textBox.GetPreferredSize(Size.Empty);
            Size inEvent = Size.Empty;
            textBox.ResetMeasurements();
            textBox.FontChanged += (_, _) =>
            {
                inEvent = textBox.GetPreferredSize(Size.Empty);
                Assert.True(inEvent.Width > before.Width);
                Assert.True(inEvent.Height > before.Height);
                Assert.Equal(1, textBox.Measurements);
            };

            textBox.Font = large;

            Size after = textBox.GetPreferredSize(Size.Empty);
            Assert.Equal(inEvent, after);
            int finalMeasurements = textBox.Measurements;
            Assert.Equal(after, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(finalMeasurements, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AnInheritedFontChangeInvalidatesTheChildMeasurement()
        => OnSta(() =>
        {
            using var small = new Font("Segoe UI", 8);
            using var large = new Font("Segoe UI", 22);
            using var parent = new Control { Font = small };
            using var textBox = new CountingTextBox { BorderStyle = BorderStyle.None, Text = "MMMMMMMM" };
            parent.Controls.Add(textBox);
            Size before = textBox.GetPreferredSize(Size.Empty);
            textBox.ResetMeasurements();

            parent.Font = large;

            Size after = textBox.GetPreferredSize(Size.Empty);
            Assert.True(after.Width > before.Width);
            Assert.True(after.Height > before.Height);
            Assert.Equal(after, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void TextBoxAutoSizeDoesNotLetTheParentLayoutEngineResizeItsWidth(int layoutKind)
        => OnSta(() =>
        {
            using Control parent = layoutKind switch
            {
                1 => new FlowLayoutPanel(),
                2 => new TableLayoutPanel { ColumnCount = 1, RowCount = 1 },
                _ => new Control()
            };
            parent.Size = new Size(600, 400);
            using var textBox = new CountingTextBox
            {
                BorderStyle = BorderStyle.None,
                Text = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM",
                Width = 123
            };
            parent.Controls.Add(textBox);
            Assert.True(textBox.AutoSize);
            Assert.True(textBox.GetPreferredSize(Size.Empty).Width > textBox.Width);

            parent.PerformLayout();
            textBox.Text += "MMMMMMMMMMMM";
            parent.PerformLayout();

            Assert.Equal(123, textBox.Width);
            Assert.False(parent.IsHandleCreated);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox preferred-size cache test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class CountingTextBox : TextBox
    {
        internal int Measurements { get; private set; }
        internal Exception? MeasurementFailure { get; set; }

        internal void ResetMeasurements() => Measurements = 0;

        protected override Size SizeFromClientSize(Size clientSize)
        {
            Measurements++;
            if (MeasurementFailure is not null) { throw MeasurementFailure; }
            return base.SizeFromClientSize(clientSize);
        }
    }
}
