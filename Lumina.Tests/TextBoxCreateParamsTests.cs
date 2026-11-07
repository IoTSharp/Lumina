using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxCreateParamsTests
{
    private const int GwlStyle = -16;
    private const int GwlExStyle = -20;
    private const int WsBorder = 0x00800000;
    private const int WsExClientEdge = 0x00000200;
    private const int EsMultiline = 0x0004;
    private const int EsAutoVScroll = 0x0040;
    private const int EsAutoHScroll = 0x0080;
    private const int EsNoHideSel = 0x0100;
    private const int EsReadOnly = 0x0800;
    private const int EsWantReturn = 0x1000;
    private const int EditMask = EsMultiline | EsAutoVScroll | EsAutoHScroll
        | EsNoHideSel | EsReadOnly | EsWantReturn;

    [Theory]
    [InlineData(false, false, false, false)]
    [InlineData(false, false, false, true)]
    [InlineData(false, false, true, false)]
    [InlineData(false, false, true, true)]
    [InlineData(false, true, false, false)]
    [InlineData(false, true, false, true)]
    [InlineData(false, true, true, false)]
    [InlineData(false, true, true, true)]
    [InlineData(true, false, false, false)]
    [InlineData(true, false, false, true)]
    [InlineData(true, false, true, false)]
    [InlineData(true, false, true, true)]
    [InlineData(true, true, false, false)]
    [InlineData(true, true, false, true)]
    [InlineData(true, true, true, false)]
    [InlineData(true, true, true, true)]
    public void EditRequestsKeepAutomaticScrollingAndDoNotForceWantReturn(
        bool multiline, bool wordWrap, bool readOnly, bool hideSelection)
        => OnSta(() =>
        {
            using var textBox = new ParameterTextBox
            {
                Multiline = multiline,
                WordWrap = wordWrap,
                ReadOnly = readOnly,
                HideSelection = hideSelection
            };

            CreateParams parameters = textBox.ReadParameters();

            int expected = EsAutoVScroll;
            if (!multiline || !wordWrap) { expected |= EsAutoHScroll; }
            if (multiline) { expected |= EsMultiline; }
            if (readOnly) { expected |= EsReadOnly; }
            if (!hideSelection) { expected |= EsNoHideSel; }
            Assert.Equal(expected, parameters.Style & EditMask);
            Assert.Equal("EDIT", parameters.ClassName);
            Assert.True(textBox.HasImplicitTheming);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void RepeatedReadsReuseParametersAndRemovePreviousEditAndBorderBits()
        => OnSta(() =>
        {
            using var textBox = new ParameterTextBox
            {
                Multiline = true,
                WordWrap = false,
                ReadOnly = true,
                HideSelection = false,
                BorderStyle = BorderStyle.FixedSingle
            };
            CreateParams parameters = textBox.ReadParameters();
            Assert.Equal(EsMultiline | EsAutoVScroll | EsAutoHScroll | EsNoHideSel | EsReadOnly,
                parameters.Style & EditMask);
            Assert.Equal(WsBorder, parameters.Style & WsBorder);
            Assert.Same(parameters, textBox.ReadParameters());

            textBox.Multiline = false;
            textBox.WordWrap = true;
            Assert.False(textBox.IsHandleCreated);
            textBox.ReadOnly = false;
            // Leaving ReadOnly evaluates PasswordProtect through PasswordChar;
            // the standard getter creates the EDIT while checking IME mode.
            Assert.True(textBox.IsHandleCreated);
            textBox.HideSelection = true;
            textBox.BorderStyle = BorderStyle.None;

            Assert.Same(parameters, textBox.ReadParameters());
            Assert.Equal(EsAutoVScroll | EsAutoHScroll, parameters.Style & EditMask);
            Assert.Equal(0, parameters.Style & WsBorder);
            Assert.Equal(0, parameters.ExStyle & WsExClientEdge);
            textBox.Multiline = true;
            Assert.Same(parameters, textBox.ReadParameters());
            Assert.Equal(EsMultiline | EsAutoVScroll, parameters.Style & EditMask);
            Assert.True(textBox.IsHandleCreated);
        });

    [Fact]
    public void ImplicitThemingIsSetBeforeEveryLegacyParameterProjection()
        => OnSta(() =>
        {
            using var textBox = new ParameterTextBox();
            textBox.ResetObservation();

            _ = textBox.ReadParameters();

            Assert.Equal(new[] { "class:True", "style:True", "extended:True" }, textBox.Observations);
            Assert.True(textBox.HasImplicitTheming);
            textBox.ResetObservation();
            _ = textBox.ReadParameters();
            Assert.Equal(new[] { "class:True", "style:True", "extended:True" }, textBox.Observations);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData("class")]
    [InlineData("style")]
    [InlineData("extended")]
    public void LegacyProjectionFailureKeepsThemingAndPropagatesTheOriginalException(string stage)
        => OnSta(() =>
        {
            using var textBox = new ParameterTextBox();
            var failure = new InvalidOperationException("CreateParams " + stage);
            textBox.ResetObservation();
            textBox.FailureStage = stage;
            textBox.ProjectionFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.ReadParameters()));

            Assert.True(textBox.HasImplicitTheming);
            Assert.Equal(stage + ":True", textBox.Observations[^1]);
            Assert.False(textBox.IsHandleCreated);
            textBox.FailureStage = null;
            textBox.ResetObservation();
            Assert.NotNull(textBox.ReadParameters());
            Assert.Equal(new[] { "class:True", "style:True", "extended:True" }, textBox.Observations);
        });

    [Fact]
    public void LegacyStyleExtensionsRemainAvailableAfterStandardEditProjection()
        => OnSta(() =>
        {
            using var textBox = new ParameterTextBox
            {
                ExtraStyle = 0x0008,
                ExtraExStyle = 0x08000000,
                BorderStyle = BorderStyle.None
            };

            CreateParams parameters = textBox.ReadParameters();

            Assert.Equal(0x0008, parameters.Style & 0x0008);
            Assert.Equal(0x08000000, parameters.ExStyle & 0x08000000);
            Assert.Equal(0, parameters.Style & (WsBorder | EsWantReturn));
            Assert.Equal(0, parameters.ExStyle & WsExClientEdge);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, BorderStyle.None)]
    [InlineData(false, BorderStyle.FixedSingle)]
    [InlineData(false, BorderStyle.Fixed3D)]
    [InlineData(true, BorderStyle.None)]
    [InlineData(true, BorderStyle.FixedSingle)]
    [InlineData(true, BorderStyle.Fixed3D)]
    public void NativeEditUsesTheStandardRequestWithoutWantReturnAndMatchesAnIndependentEdit(
        bool multiline, BorderStyle border)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ParameterTextBox
            {
                AutoSize = false,
                Size = new Size(160, 80),
                Multiline = multiline,
                WordWrap = false,
                ReadOnly = true,
                HideSelection = false,
                BorderStyle = border,
                Text = "alpha beta"
            };
            form.Controls.Add(textBox);
            nint parent = form.Handle;
            nint handle = textBox.Handle;
            int borderRequest = border == BorderStyle.FixedSingle ? WsBorder : 0;
            int extendedRequest = border == BorderStyle.Fixed3D ? WsExClientEdge : 0;
            int editRequest = EsAutoVScroll | EsAutoHScroll | EsReadOnly | EsNoHideSel
                | (multiline ? EsMultiline : 0);
            Assert.Equal(editRequest, textBox.ReadParameters().Style & EditMask);
            Assert.Equal(borderRequest, textBox.ReadParameters().Style & WsBorder);
            nint independent = CreateWindowExW((uint)extendedRequest, "EDIT", "alpha beta",
                0x50010000u | (uint)editRequest | (uint)borderRequest,
                0, 0, 160, 80, parent, 0, 0, 0);
            Assert.NotEqual(0, independent);
            try
            {
                Assert.Equal(parent, GetParent(handle));
                Assert.Same(form, Control.FromHandle(GetParent(handle)));
                Assert.Equal(editRequest, GetWindowLongW(handle, GwlStyle) & EditMask);
                Assert.Equal(GetWindowLongW(independent, GwlStyle) & (EditMask | WsBorder),
                    GetWindowLongW(handle, GwlStyle) & (EditMask | WsBorder));
                Assert.Equal(GetWindowLongW(independent, GwlExStyle) & WsExClientEdge,
                    GetWindowLongW(handle, GwlExStyle) & WsExClientEdge);
                // EDIT normalizes a requested single border at creation.
                // Keep its native result separate from the requested bit.
                Assert.Equal(0, GetWindowLongW(handle, GwlStyle) & WsBorder);
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void AcceptedReturnRemainsAManagedInputRuleWithoutAddingANativeWantReturnBit(bool acceptsReturn)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new ParameterTextBox
            {
                Multiline = true,
                WordWrap = false,
                AcceptsReturn = acceptsReturn,
                Text = "alpha"
            };
            form.Controls.Add(textBox);
            nint parent = form.Handle;
            nint handle = textBox.Handle;
            nint independent = CreateWindowExW(0, "EDIT", "alpha",
                0x50010000u | EsAutoVScroll | EsAutoHScroll | EsMultiline,
                0, 0, 160, 80, parent, 0, 0, 0);
            Assert.NotEqual(0, independent);
            try
            {
                Assert.Equal(0, textBox.ReadParameters().Style & EsWantReturn);
                Assert.Equal(0, GetWindowLongW(handle, GwlStyle) & EsWantReturn);
                Assert.Equal(acceptsReturn, textBox.InputKey(Keys.Enter));
                Assert.Equal(acceptsReturn, textBox.InputKey(Keys.Control | Keys.Enter));
                Assert.False(textBox.InputKey(Keys.Alt | Keys.Enter));
                Assert.Equal(SendMessageW(independent, 0x0087, (nint)Keys.Enter, 0),
                    SendMessageW(handle, 0x0087, (nint)Keys.Enter, 0));
                textBox.Select(5, 0);
                _ = SendMessageW(handle, 0x0102, 13, 1);
                Assert.Equal("alpha\r\n", textBox.Text);
                Assert.Equal(7, textBox.SelectionStart);
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    [Fact]
    public void ParameterReadsDoNotRecreateOrChangeNativeEditingStateAndNotifications()
        => OnSta(() =>
        {
            using var textBox = new ParameterTextBox { Text = "alpha 中文 beta" };
            nint handle = textBox.Handle;
            textBox.Select(2, 5);
            textBox.Modified = true;
            int textChanges = 0;
            int modifiedChanges = 0;
            int selectionChanges = 0;
            int created = 0;
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.ModifiedChanged += (_, _) => modifiedChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;
            textBox.HandleCreated += (_, _) => created++;
            textBox.ResetObservation();

            CreateParams parameters = textBox.ReadParameters();
            Assert.Same(parameters, textBox.ReadParameters());

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal("alpha 中文 beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, modifiedChanges);
            Assert.Equal(0, selectionChanges);
            Assert.Equal(0, created);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox CreateParams test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ParameterTextBox : TextBox
    {
        private bool _observe;
        internal List<string> Observations { get; } = new();
        internal string? FailureStage { get; set; }
        internal Exception? ProjectionFailure { get; set; }
        internal uint ExtraStyle { get; set; }
        internal uint ExtraExStyle { get; set; }
        internal bool HasImplicitTheming => GetStyle(ControlStyles.ApplyThemingImplicitly);
        internal CreateParams ReadParameters() => base.CreateParams;
        internal bool InputKey(Keys key) => IsInputKey(key);

        internal void ResetObservation()
        {
            _observe = true;
            Observations.Clear();
            SetStyle(ControlStyles.ApplyThemingImplicitly, false);
        }

        protected override string ClassName
        {
            get { Observe("class"); return base.ClassName; }
        }

        protected override uint Style
        {
            get { Observe("style"); return base.Style | ExtraStyle; }
        }

        protected override uint ExStyle
        {
            get { Observe("extended"); return base.ExStyle | ExtraExStyle; }
        }

        private void Observe(string stage)
        {
            if (!_observe) { return; }
            Observations.Add(stage + ":" + HasImplicitTheming);
            if (FailureStage == stage) { throw ProjectionFailure!; }
        }
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string? windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint param);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint handle, int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);
}
