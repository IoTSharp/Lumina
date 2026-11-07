using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxScrollBarPreferredSizeTests
{
    [Theory]
    [InlineData(false, false, ScrollBars.None)]
    [InlineData(false, false, ScrollBars.Horizontal)]
    [InlineData(false, false, ScrollBars.Vertical)]
    [InlineData(false, false, ScrollBars.Both)]
    [InlineData(false, true, ScrollBars.None)]
    [InlineData(false, true, ScrollBars.Horizontal)]
    [InlineData(false, true, ScrollBars.Vertical)]
    [InlineData(false, true, ScrollBars.Both)]
    [InlineData(true, false, ScrollBars.None)]
    [InlineData(true, false, ScrollBars.Horizontal)]
    [InlineData(true, false, ScrollBars.Vertical)]
    [InlineData(true, false, ScrollBars.Both)]
    [InlineData(true, true, ScrollBars.None)]
    [InlineData(true, true, ScrollBars.Horizontal)]
    [InlineData(true, true, ScrollBars.Vertical)]
    [InlineData(true, true, ScrollBars.Both)]
    public void ScrollBarInsetsFollowMultilineWrappingAndTheRequestedBars(
        bool multiline, bool wordWrap, ScrollBars bars)
        => OnSta(() =>
        {
            using var textBox = CreateMeasuringTextBox(multiline, wordWrap, bars);
            textBox.SetFontHeight(200);
            Size inset = NativeScrollBarInsets(textBox);

            Assert.Equal(new Size(inset.Width, 200 + inset.Height),
                textBox.GetPreferredSize(new Size(1000, 1000)));
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(HorizontalAlignment.Left)]
    [InlineData(HorizontalAlignment.Center)]
    [InlineData(HorizontalAlignment.Right)]
    public void HorizontalBarMeasurementDoesNotUseTheCreationAlignmentGuard(HorizontalAlignment alignment)
        => OnSta(() =>
        {
            using var textBox = CreateMeasuringTextBox(true, false, ScrollBars.Horizontal);
            textBox.TextAlign = alignment;
            textBox.SetFontHeight(200);
            int horizontalHeight = NativeMetric(3, GetInternalDpi(textBox));

            Assert.Equal(new Size(0, 200 + horizontalHeight),
                textBox.GetPreferredSize(new Size(1000, 1000)));
            Assert.Equal(alignment == HorizontalAlignment.Left,
                (textBox.RequestedStyle & 0x00100000) != 0);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1, 96)]
    [InlineData(-1, 144)]
    [InlineData(-1, 192)]
    [InlineData(-2, 96)]
    [InlineData(-2, 144)]
    [InlineData(-2, 192)]
    [InlineData(-4, 96)]
    [InlineData(-4, 144)]
    [InlineData(-4, 192)]
    public void ScrollBarMetricsUseControlDpiOnlyOnAPerMonitorV2Thread(int awarenessContext, int controlDpi)
        => OnSta(() =>
        {
            using var context = new ThreadDpiScope(awarenessContext);
            using var textBox = CreateMeasuringTextBox(true, false, ScrollBars.Both);
            textBox.SetFontHeight(200);
            InternalDpiProperty.SetValue(textBox, controlDpi);

            Assert.Equal(new Size(NativeMetric(2, controlDpi), 200 + NativeMetric(3, controlDpi)),
                textBox.GetPreferredSize(new Size(1000, 1000)));
            Assert.Equal(controlDpi, GetInternalDpi(textBox));
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(ScrollBars.Vertical, true)]
    [InlineData(ScrollBars.Both, true)]
    [InlineData(ScrollBars.Both, false)]
    public void ScrollBarInsetsReduceTheConstraintBeforeTextIsMeasured(ScrollBars bars, bool wordWrap)
        => OnSta(() =>
        {
            using var textBox = CreateMeasuringTextBox(true, wordWrap, bars);
            using var withoutBars = CreateMeasuringTextBox(true, wordWrap, ScrollBars.None);
            const string text = "word word word word word word word word word word word word";
            textBox.Text = text;
            withoutBars.Text = text;
            Size inset = NativeScrollBarInsets(textBox);
            Size proposal = new(110, 500);
            Size textOnly = withoutBars.GetPreferredSize(proposal - inset);

            Assert.Equal(textOnly + inset, textBox.GetPreferredSize(proposal));
            if (wordWrap)
            {
                Assert.True(textOnly.Height > withoutBars.GetPreferredSize(proposal).Height);
            }
            Assert.False(textBox.IsHandleCreated);
            Assert.False(withoutBars.IsHandleCreated);
        });

    [Fact]
    public void AConstrainedRequestObservesNewBarsWithoutCreatingAHandle()
        => OnSta(() =>
        {
            using var textBox = CreateMeasuringTextBox(true, false, ScrollBars.None);
            textBox.SetFontHeight(200);
            Assert.Equal(new Size(0, 200), textBox.GetPreferredSize(Size.Empty));

            textBox.ScrollBars = ScrollBars.Both;

            Size inset = NativeScrollBarInsets(textBox);
            Assert.Equal(new Size(inset.Width, 200 + inset.Height),
                textBox.GetPreferredSize(new Size(1000, 1000)));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ChangingBarsWithoutAHandleKeepsTheExistingUnconstrainedCache()
        => OnSta(() =>
        {
            using var textBox = CreateMeasuringTextBox(true, false, ScrollBars.None);
            textBox.SetFontHeight(200);
            Size cached = textBox.GetPreferredSize(Size.Empty);
            textBox.ResetMeasurements();

            textBox.ScrollBars = ScrollBars.Both;

            Assert.Equal(cached, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(0, textBox.Measurements);
            Size inset = NativeScrollBarInsets(textBox);
            Assert.Equal(new Size(inset.Width, 200 + inset.Height),
                textBox.GetPreferredSize(new Size(1000, 1000)));
            Assert.Equal(1, textBox.Measurements);
            Assert.Equal(cached, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AssigningTheSameBarsKeepsTheUnconstrainedMeasurementCached()
        => OnSta(() =>
        {
            using var textBox = CreateMeasuringTextBox(true, false, ScrollBars.Both);
            textBox.SetFontHeight(200);
            textBox.ResetMeasurements();
            Size before = textBox.GetPreferredSize(Size.Empty);
            Assert.Equal(1, textBox.Measurements);

            textBox.ScrollBars = ScrollBars.Both;

            Assert.Equal(before, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void RecreatingTheNativeHandleDiscardsTheOldScrollBarMeasurement()
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = CreateMeasuringTextBox(true, false, ScrollBars.None);
            textBox.Text = "native text";
            form.Controls.Add(textBox);
            _ = textBox.Handle;
            Size withoutBars = textBox.GetPreferredSize(Size.Empty);
            textBox.ResetMeasurements();
            int created = 0;
            textBox.HandleCreated += (_, _) => created++;

            textBox.ScrollBars = ScrollBars.Both;

            Size withBars = textBox.GetPreferredSize(Size.Empty);
            Assert.Equal(withoutBars + NativeScrollBarInsets(textBox), withBars);
            Assert.Equal(withBars, textBox.GetPreferredSize(Size.Empty));
            Assert.Equal(1, textBox.Measurements);
            Assert.Equal(1, created);
            Assert.True(textBox.IsHandleCreated);
        });

    [Fact]
    public void PublicMaximumSizeClampsTheFinalSizeIncludingScrollBarInsets()
        => OnSta(() =>
        {
            using var textBox = CreateMeasuringTextBox(true, false, ScrollBars.Both);
            textBox.SetFontHeight(200);
            textBox.MaximumSize = new Size(8, 9);

            Assert.Equal(new Size(8, 9), textBox.GetPreferredSize(Size.Empty));
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MeasuringBarsPreservesNativeTextSelectionUndoModifiedAndBounds()
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = new TextBox
            {
                AutoSize = false,
                Multiline = true,
                WordWrap = false,
                ScrollBars = ScrollBars.Both,
                Text = "native 中文 text\r\nsecond line",
                Size = new Size(210, 90)
            };
            form.Controls.Add(textBox);
            nint handle = textBox.Handle;
            textBox.Select(2, 5);
            textBox.Modified = true;
            string text = textBox.Text;
            Size bounds = textBox.Size;
            bool canUndo = textBox.CanUndo;
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
            Assert.Equal(canUndo, textBox.CanUndo);
            Assert.Equal(0, textChanged);
            Assert.Equal(0, modifiedChanged);
            Assert.Equal(0, handleCreated);
        });

    private static MeasuringTextBox CreateMeasuringTextBox(bool multiline, bool wordWrap, ScrollBars bars)
        => new()
        {
            AutoSize = false,
            BorderStyle = BorderStyle.None,
            Multiline = multiline,
            WordWrap = wordWrap,
            ScrollBars = bars
        };

    private static Size NativeScrollBarInsets(TextBox textBox)
        => new(textBox.Multiline && (textBox.ScrollBars & ScrollBars.Vertical) != 0
                ? NativeMetric(2, GetInternalDpi(textBox)) : 0,
            textBox.Multiline && !textBox.WordWrap && (textBox.ScrollBars & ScrollBars.Horizontal) != 0
                ? NativeMetric(3, GetInternalDpi(textBox)) : 0);

    private static int NativeMetric(int index, int dpi)
        => AreDpiAwarenessContextsEqual(GetThreadDpiAwarenessContext(), (nint)(-4))
            ? GetSystemMetricsForDpi(index, unchecked((uint)dpi)) : GetSystemMetrics(index);

    private static PropertyInfo InternalDpiProperty
        => typeof(Control).GetProperty("DeviceDpiInternal", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static int GetInternalDpi(Control control) => (int)InternalDpiProperty.GetValue(control)!;

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox scroll-bar preferred-size test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class MeasuringTextBox : TextBox
    {
        internal int Measurements { get; private set; }
        internal int RequestedStyle => CreateParams.Style;
        internal void SetFontHeight(int height) => FontHeight = height;
        internal void ResetMeasurements() => Measurements = 0;

        protected override Size SizeFromClientSize(Size clientSize)
        {
            Measurements++;
            return clientSize;
        }
    }

    private sealed class ThreadDpiScope : IDisposable
    {
        private readonly nint _previousContext;

        internal ThreadDpiScope(int context)
        {
            _previousContext = SetThreadDpiAwarenessContext((nint)context);
            Assert.NotEqual((nint)0, _previousContext);
            try { Assert.True(AreDpiAwarenessContextsEqual(GetThreadDpiAwarenessContext(), (nint)context)); }
            catch
            {
                _ = SetThreadDpiAwarenessContext(_previousContext);
                throw;
            }
        }

        public void Dispose()
        {
            nint result = SetThreadDpiAwarenessContext(_previousContext);
            Assert.NotEqual((nint)0, result);
            Assert.True(AreDpiAwarenessContextsEqual(GetThreadDpiAwarenessContext(), _previousContext));
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetSystemMetrics(int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetSystemMetricsForDpi(int index, uint dpi);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SetThreadDpiAwarenessContext(nint context);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetThreadDpiAwarenessContext();

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AreDpiAwarenessContextsEqual(nint first, nint second);
}
