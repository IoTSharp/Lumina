using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxSizingMetricTests
{
    [Fact]
    public void DeviceDpiHasTheStandardReadOnlyNonvirtualContract()
    {
        PropertyInfo property = typeof(Control).GetProperty(nameof(Control.DeviceDpi))!;
        Assert.Equal(typeof(int), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.Null(property.SetMethod);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(EditorBrowsableState.Always,
            property.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
    }

    [Fact]
    public void PreferredHeightHasTheStandardReadOnlyNonvirtualContract()
    {
        PropertyInfo property = typeof(TextBoxBase).GetProperty(nameof(TextBoxBase.PreferredHeight))!;

        Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
        Assert.Equal(typeof(int), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual);
        Assert.Null(property.SetMethod);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(EditorBrowsableState.Advanced,
            property.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.Equal("Layout", property.GetCustomAttribute<CategoryAttribute>()!.Category);
    }

    [Fact]
    public void DefaultSizeAndInitialBoundsUseTheSingleLinePreferredHeight()
        => OnSta(() =>
        {
            using var textBox = new MetricTextBox();

            Assert.Equal(BorderStyle.Fixed3D, textBox.BorderStyle);
            Assert.Equal(new Size(100, textBox.PreferredHeight), textBox.ExposedDefaultSize);
            Assert.Equal(textBox.ExposedDefaultSize, textBox.Size);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(BorderStyle.None, 0)]
    [InlineData(BorderStyle.None, 13)]
    [InlineData(BorderStyle.None, 123)]
    [InlineData(BorderStyle.FixedSingle, 0)]
    [InlineData(BorderStyle.FixedSingle, 13)]
    [InlineData(BorderStyle.FixedSingle, 123)]
    [InlineData(BorderStyle.Fixed3D, 0)]
    [InlineData(BorderStyle.Fixed3D, 13)]
    [InlineData(BorderStyle.Fixed3D, 123)]
    public void PreferredHeightUsesFontHeightAndTheIndependentNativeBorderMetric(BorderStyle border, int fontHeight)
        => OnSta(() =>
        {
            using var textBox = new MetricTextBox { BorderStyle = border };
            textBox.SetFontHeight(fontHeight);
            int expected = fontHeight + (border == BorderStyle.None ? 0 : 4 * NativeBorderHeight(GetInternalDpi(textBox)) + 3);

            Assert.Equal(expected, textBox.PreferredHeight);
            Assert.Equal(new Size(100, expected), textBox.ExposedDefaultSize);
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
    public void BorderMeasurementUsesControlDpiOnlyOnAPerMonitorV2Thread(int awarenessContext, int controlDpi)
        => OnSta(() =>
        {
            using var context = new ThreadDpiScope(awarenessContext);
            using var textBox = new MetricTextBox { BorderStyle = BorderStyle.FixedSingle };
            textBox.SetFontHeight(37);
            SetInternalDpi(textBox, controlDpi);

            Assert.Equal(controlDpi, GetInternalDpi(textBox));
            Assert.Equal(37 + NativeBorderHeight(controlDpi) * 4 + 3, textBox.PreferredHeight);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-4)]
    public void PublicDeviceDpiUsesTheStandardThreadAwarenessProjection(int awarenessContext)
        => OnSta(() =>
        {
            using var context = new ThreadDpiScope(awarenessContext);
            using var textBox = new MetricTextBox();
            int initialDpi = textBox.DeviceDpi;
            int differentDpi = initialDpi == 144 ? 192 : 144;
            SetInternalDpi(textBox, differentDpi);

            Assert.Equal(differentDpi, GetInternalDpi(textBox));
            Assert.Equal(IsThreadPerMonitorV2() ? differentDpi : initialDpi, textBox.DeviceDpi);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, "A&B")]
    [InlineData(true, "first\r\nsecond")]
    [InlineData(true, "中文和emoji \ud83d\ude00")]
    public void PreferredHeightIgnoresTextWrappingPaddingAndAssignedBounds(bool multiline, string text)
        => OnSta(() =>
        {
            using var textBox = new MetricTextBox
            {
                AutoSize = false,
                Multiline = multiline,
                Text = text,
                WordWrap = true,
                Padding = new Padding(11, 23, 31, 47),
                Size = new Size(431, 217)
            };
            textBox.SetFontHeight(37);
            int expected = 37 + NativeBorderHeight(GetInternalDpi(textBox)) * 4 + 3;

            Assert.Equal(expected, textBox.PreferredHeight);
            Assert.Equal(new Size(100, expected), textBox.ExposedDefaultSize);
            Assert.Equal(new Size(431, 217), textBox.Size);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PreferredHeightDoesNotReadVirtualTextOrClientSizeMeasurement()
        => OnSta(() =>
        {
            using var textBox = new MetricTextBox { BorderStyle = BorderStyle.None };
            textBox.SetFontHeight(73);
            textBox.RejectTextAndClientMeasurement = true;
            try
            {
                Assert.Equal(73, textBox.PreferredHeight);
                Assert.Equal(new Size(100, 73), textBox.ExposedDefaultSize);
                Assert.False(textBox.IsHandleCreated);
            }
            finally { textBox.RejectTextAndClientMeasurement = false; }
        });

    [Theory]
    [InlineData(BorderStyle.None)]
    [InlineData(BorderStyle.FixedSingle)]
    [InlineData(BorderStyle.Fixed3D)]
    public void FontChangesRefreshThePreferredHeightWithoutCreatingAHandle(BorderStyle border)
        => OnSta(() =>
        {
            using var small = new Font("Segoe UI", 8);
            using var large = new Font("Segoe UI", 20);
            using var textBox = new MetricTextBox { BorderStyle = border, Font = small };
            int borderHeight = border == BorderStyle.None ? 0 : NativeBorderHeight(GetInternalDpi(textBox)) * 4 + 3;

            Assert.Equal(small.Height + borderHeight, textBox.PreferredHeight);
            textBox.Font = large;

            Assert.Equal(large.Height + borderHeight, textBox.PreferredHeight);
            Assert.True(large.Height > small.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PreferredHeightLeavesNativeEditingStateAndNotificationsUnchanged()
        => OnSta(() =>
        {
            using var form = new Form();
            _ = form.Handle;
            using var textBox = new MetricTextBox { Text = "native 中文 text" };
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

            Assert.True(textBox.PreferredHeight > 0);
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

    [Fact]
    public void CreatedPerMonitorV2ControlCapturesItsActualWindowDpi()
        => OnSta(() =>
        {
            using var context = new ThreadDpiScope(-4);
            using var form = new Form();
            _ = form.Handle;
            using var textBox = new MetricTextBox();
            form.Controls.Add(textBox);
            nint handle = textBox.Handle;
            uint nativeDpi = GetDpiForWindow(handle);

            Assert.NotEqual(0u, nativeDpi);
            Assert.Equal((int)nativeDpi, GetInternalDpi(textBox));
            Assert.Equal((int)nativeDpi, textBox.DeviceDpi);
        });

    private static int NativeBorderHeight(int dpi)
        => IsThreadPerMonitorV2() ? GetSystemMetricsForDpi(6, (uint)dpi) : GetSystemMetrics(6);

    private static bool IsThreadPerMonitorV2()
        => AreDpiAwarenessContextsEqual(GetThreadDpiAwarenessContext(), (nint)(-4));

    private static PropertyInfo InternalDpiProperty
        => typeof(Control).GetProperty("DeviceDpiInternal", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static int GetInternalDpi(Control control) => (int)InternalDpiProperty.GetValue(control)!;

    private static void SetInternalDpi(Control control, int dpi) => InternalDpiProperty.SetValue(control, dpi);

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox sizing metric test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class MetricTextBox : TextBox
    {
        internal Size ExposedDefaultSize => base.DefaultSize;
        internal bool RejectTextAndClientMeasurement { get; set; }
        internal void SetFontHeight(int height) => FontHeight = height;

        [AllowNull]
        public override string Text
        {
            get => RejectTextAndClientMeasurement ? throw new InvalidOperationException("Unexpected text read.") : base.Text;
            set => base.Text = value;
        }

        protected override Size SizeFromClientSize(Size clientSize)
            => RejectTextAndClientMeasurement ? throw new InvalidOperationException("Unexpected client conversion.") : base.SizeFromClientSize(clientSize);
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
    private static extern uint GetDpiForWindow(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetThreadDpiAwarenessContext();

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SetThreadDpiAwarenessContext(nint context);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AreDpiAwarenessContextsEqual(nint first, nint second);
}
