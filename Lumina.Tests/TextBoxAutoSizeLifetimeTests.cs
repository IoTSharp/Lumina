using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxAutoSizeLifetimeTests
{
    [Fact]
    public void AutoSizeHasTheTextBoxDefaultOverrideAndHiddenDesignerMetadata()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            Assert.True(textBox.AutoSize);
            Assert.True(textBox.HasFixedHeight);
            Assert.False(textBox.IsHandleCreated);

            PropertyInfo property = typeof(TextBoxBase).GetProperty(nameof(TextBoxBase.AutoSize))!;
            Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
            Assert.Equal(typeof(Control), property.GetMethod!.GetBaseDefinition().DeclaringType);
            Assert.Equal(typeof(Control), property.SetMethod!.GetBaseDefinition().DeclaringType);
            Assert.True(property.GetMethod.IsVirtual && property.SetMethod.IsVirtual);
            Assert.Equal(true, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
            Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
            Assert.Equal(RefreshProperties.Repaint,
                property.GetCustomAttribute<RefreshPropertiesAttribute>()!.RefreshProperties);
            Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
            Assert.Equal(EditorBrowsableState.Never,
                property.GetCustomAttribute<EditorBrowsableAttribute>()!.State);

            EventInfo notification = typeof(TextBoxBase).GetEvent(nameof(TextBoxBase.AutoSizeChanged))!;
            Assert.Equal(typeof(TextBoxBase), notification.DeclaringType);
            Assert.False(notification.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
            Assert.Equal(EditorBrowsableState.Never,
                notification.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
        });

    [Fact]
    public void HiddenAndBaseAutoSizeEventsUseTheSameNotificationAndSupportRemoval()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            Control control = textBox;
            int editingCalls = 0;
            int controlCalls = 0;
            EventHandler editingHandler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                editingCalls++;
            };
            textBox.AutoSizeChanged += editingHandler;
            control.AutoSizeChanged += (_, _) => controlCalls++;

            textBox.AutoSize = false;
            textBox.AutoSizeChanged -= editingHandler;
            textBox.AutoSize = true;
            textBox.AutoSize = true;

            Assert.Equal(1, editingCalls);
            Assert.Equal(2, controlCalls);
            Assert.Equal(2, textBox.AutoSizeHookCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AutoSizeStateDoesNotEnableGenericParentAutoSizeLayout()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(600, 400) };
            using var textBox = new SizingTextBox { Text = "a long value which must not resize the width" };
            parent.Controls.Add(textBox);
            Size original = textBox.Size;
            int layouts = 0;
            parent.Layout += (_, _) => layouts++;

            textBox.AutoSize = false;
            textBox.AutoSize = true;

            Assert.Equal(original, textBox.Size);
            Assert.Equal(0, layouts);
            Assert.False(parent.IsHandleCreated);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(81)]
    [InlineData(240)]
    public void SingleLineKeepsTheRequestedHeightUntilAutoSizeIsDisabled(int requested)
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            int preferred = textBox.PreferredHeight;

            textBox.Size = new Size(173, requested);

            Assert.Equal(new Size(173, preferred), textBox.Size);
            Assert.True(textBox.HasFixedHeight);
            textBox.AutoSize = false;
            Assert.Equal(new Size(173, requested), textBox.Size);
            Assert.False(textBox.HasFixedHeight);
            textBox.AutoSize = true;
            Assert.Equal(new Size(173, preferred), textBox.Size);
            textBox.AutoSize = false;
            Assert.Equal(requested, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AssigningTheCurrentPreferredHeightDoesNotEraseAnEarlierRequest()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            textBox.Height = 92;
            textBox.Height = textBox.PreferredHeight;

            textBox.AutoSize = false;

            Assert.Equal(92, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void BoundsComponentsWithoutHeightPreserveTheEarlierHeightRequest()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            textBox.Height = 83;

            textBox.SetBounds(7, 9, 151, 999, BoundsSpecified.Location | BoundsSpecified.Width);

            Assert.Equal(new Rectangle(7, 9, 151, textBox.PreferredHeight), textBox.Bounds);
            textBox.AutoSize = false;
            Assert.Equal(new Rectangle(7, 9, 151, 83), textBox.Bounds);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ProtectedBoundsCoreAppliesTheTextBoxHeightContractToItsRawArguments()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();

            textBox.CallBoundsCore(11, 13, 177, 91, BoundsSpecified.None);

            Assert.Equal(new Rectangle(11, 13, 177, textBox.PreferredHeight), textBox.Bounds);
            textBox.AutoSize = false;
            Assert.Equal(new Rectangle(11, 13, 177, 91), textBox.Bounds);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void SingleLineAutoSizeNotificationObservesCommittedStateStyleAndRestoredHeight()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            textBox.Height = 87;
            var seen = new List<(bool AutoSize, bool FixedHeight, int Height)>();
            textBox.AutoSizeChanged += (_, _) => seen.Add((textBox.AutoSize, textBox.HasFixedHeight, textBox.Height));

            textBox.AutoSize = false;
            textBox.AutoSize = false;
            textBox.AutoSize = true;

            Assert.Equal(new[] { (false, false, 87), (true, true, textBox.PreferredHeight) }, seen);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AutoSizeCanReenterOnceWithoutLosingTheHeightRequest()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            textBox.Height = 89;
            var seen = new List<bool>();
            bool reentered = false;
            EventHandler handler = (_, _) =>
            {
                seen.Add(textBox.AutoSize);
                if (!reentered)
                {
                    reentered = true;
                    textBox.AutoSize = true;
                }
            };
            textBox.AutoSizeChanged += handler;

            textBox.AutoSize = false;

            Assert.Equal(new[] { false, true }, seen);
            Assert.True(textBox.AutoSize);
            Assert.Equal(textBox.PreferredHeight, textBox.Height);
            textBox.AutoSizeChanged -= handler;
            textBox.AutoSize = false;
            Assert.Equal(89, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void AutoSizeEventFailureKeepsItsCommittedStateAndOriginalException()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            textBox.Height = 93;
            var failure = new InvalidOperationException("AutoSize notification");
            EventHandler handler = (_, _) => throw failure;
            textBox.AutoSizeChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.AutoSize = false));

            Assert.False(textBox.AutoSize);
            Assert.False(textBox.HasFixedHeight);
            Assert.Equal(93, textBox.Height);
            textBox.AutoSizeChanged -= handler;
            textBox.AutoSize = true;
            textBox.AutoSize = false;
            Assert.Equal(93, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void FailedHeightAdjustmentRestoresTheGuardAndAllowsANewRequest()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            textBox.Height = 88;
            var failure = new InvalidOperationException("height adjustment");
            textBox.NextBoundsFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.AutoSize = false));

            Assert.False(textBox.AutoSize);
            Assert.False(textBox.HasFixedHeight);
            Assert.Equal(0, textBox.AutoSizeHookCalls);
            textBox.Height = 135;
            textBox.AutoSize = true;
            textBox.AutoSize = false;
            Assert.Equal(135, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MultilineTransitionRestoresTheRequestAndSingleLineRestoresPreferredHeight()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            textBox.Height = 98;
            var seen = new List<(bool Multiline, bool FixedHeight, int Height)>();
            textBox.MultilineChanged += (_, _) => seen.Add((textBox.Multiline, textBox.HasFixedHeight, textBox.Height));

            textBox.Multiline = true;
            textBox.Multiline = true;
            textBox.Multiline = false;
            textBox.AutoSize = false;

            Assert.Equal(new[] { (true, false, 98), (false, true, textBox.PreferredHeight) }, seen);
            Assert.Equal(98, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MultilineIgnoresAutoSizeHeightEffectsAndRetainsItsRequest()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox { Multiline = true, Height = 117 };
            int notifications = 0;
            textBox.AutoSizeChanged += (_, _) => notifications++;

            textBox.AutoSize = false;
            textBox.AutoSize = true;

            Assert.Equal(117, textBox.Height);
            Assert.False(textBox.HasFixedHeight);
            Assert.Equal(2, notifications);
            textBox.Multiline = false;
            Assert.True(textBox.HasFixedHeight);
            Assert.Equal(textBox.PreferredHeight, textBox.Height);
            textBox.AutoSize = false;
            Assert.Equal(117, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void DisablingMultilineWithAutoSizeOffDoesNotEnableFixedHeight()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox { AutoSize = false, Multiline = true, Height = 113 };

            textBox.Multiline = false;

            Assert.Equal(113, textBox.Height);
            Assert.False(textBox.HasFixedHeight);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MultilineNotificationPrecedesTheParentTransactionLayout()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(600, 400) };
            using var textBox = new SizingTextBox();
            parent.Controls.Add(textBox);
            textBox.Height = 84;
            var trace = new List<string>();
            parent.Layout += (_, _) => trace.Add("layout");
            textBox.MultilineChanged += (_, _) =>
            {
                Assert.Equal(84, textBox.Height);
                Assert.False(textBox.HasFixedHeight);
                trace.Add("multiline");
            };

            textBox.Multiline = true;

            Assert.True(trace.Count >= 2);
            Assert.Equal("multiline", trace[0]);
            Assert.Equal("layout", trace[^1]);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void FontNotificationRunsBeforeTheSingleLineHeightAdjustment()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            int oldHeight = textBox.Height;
            int heightAtEvent = -1;
            textBox.FontChanged += (_, _) => heightAtEvent = textBox.Height;
            using var font = new Font(textBox.Font.FontFamily, textBox.Font.Size + 9);

            textBox.Font = font;

            Assert.Equal(oldHeight, heightAtEvent);
            Assert.True(textBox.PreferredHeight > oldHeight);
            Assert.Equal(textBox.PreferredHeight, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void FontChangeKeepsAFreeOrMultilineRequestedHeight(bool multiline)
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox { AutoSize = false, Multiline = multiline, Height = 96 };
            using var font = new Font(textBox.Font.FontFamily, textBox.Font.Size + 9);

            textBox.Font = font;

            Assert.Equal(96, textBox.Height);
            textBox.AutoSize = true;
            Assert.Equal(multiline ? 96 : textBox.PreferredHeight, textBox.Height);
            textBox.AutoSize = false;
            Assert.Equal(96, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ThrowingFontNotificationPreventsTheSubsequentHeightAdjustment()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox();
            int oldHeight = textBox.Height;
            using var font = new Font(textBox.Font.FontFamily, textBox.Font.Size + 9);
            var failure = new InvalidOperationException("font notification");
            EventHandler handler = (_, _) => throw failure;
            textBox.FontChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.Font = font));

            Assert.Same(font, textBox.Font);
            Assert.Equal(oldHeight, textBox.Height);
            Assert.True(textBox.PreferredHeight > oldHeight);
            textBox.FontChanged -= handler;
            textBox.CallPaddingChanged();
            Assert.Equal(textBox.PreferredHeight, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaddingNotificationRunsBeforeTheSingleLineHeightAdjustment()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox { BorderStyle = BorderStyle.None };
            int oldHeight = textBox.Height;
            textBox.SetCachedFontHeight(61);
            int heightAtEvent = -1;
            textBox.PaddingChanged += (_, _) => heightAtEvent = textBox.Height;

            textBox.Padding = new Padding(4);

            Assert.Equal(oldHeight, heightAtEvent);
            Assert.Equal(61, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MultilinePaddingAdjustmentTemporarilyFitsTheFontThenRestoresTheRequest()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox { BorderStyle = BorderStyle.None, Multiline = true, Height = 1 };
            textBox.SetCachedFontHeight(61);
            textBox.Trace.Clear();
            textBox.PaddingChanged += (_, _) => textBox.Trace.Add($"padding:{textBox.Height}");

            textBox.Padding = new Padding(4);

            Assert.Equal(new[] { "padding:1", "bounds:63", "bounds:1" }, textBox.Trace);
            Assert.Equal(1, textBox.Height);
            textBox.Multiline = false;
            Assert.Equal(61, textBox.Height);
            textBox.AutoSize = false;
            Assert.Equal(1, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ThrowingPaddingNotificationPreventsAdjustmentAndPreservesTheRequest()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox { BorderStyle = BorderStyle.None };
            textBox.Height = 94;
            int oldHeight = textBox.Height;
            textBox.SetCachedFontHeight(61);
            var failure = new InvalidOperationException("padding notification");
            EventHandler handler = (_, _) => throw failure;
            textBox.PaddingChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.Padding = new Padding(4)));

            Assert.Equal(oldHeight, textBox.Height);
            Assert.Equal(new Padding(4), textBox.Padding);
            textBox.PaddingChanged -= handler;
            textBox.CallPaddingChanged();
            Assert.Equal(61, textBox.Height);
            textBox.AutoSize = false;
            Assert.Equal(94, textBox.Height);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void HandleCreationHonorsTheOppositeVerticalAnchorGuard(bool oppositeAnchors)
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox { BorderStyle = BorderStyle.None };
            int oldHeight = textBox.Height;
            textBox.Anchor = oppositeAnchors
                ? AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left
                : AnchorStyles.Top | AnchorStyles.Left;
            textBox.SetCachedFontHeight(61);
            int heightAtEvent = -1;
            textBox.HandleCreated += (_, _) => heightAtEvent = textBox.Height;

            nint handle = textBox.Handle;

            Assert.NotEqual(0, handle);
            Assert.Equal(oldHeight, heightAtEvent);
            Assert.Equal(oppositeAnchors ? oldHeight : 61, textBox.Height);
            Assert.True(GetWindowRect(handle, out NativeRect rect));
            Assert.Equal(textBox.Height, rect.Bottom - rect.Top);
            textBox.CallPaddingChanged();
            Assert.Equal(61, textBox.Height);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void NativeEditUsesTheRequestedSmallHeightWithoutTheLegacyClamp(bool multiline)
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox
            {
                AutoSize = false,
                BorderStyle = BorderStyle.None,
                Multiline = multiline,
                Size = new Size(141, 5)
            };

            nint handle = textBox.Handle;

            Assert.Equal(new Size(141, 5), textBox.Size);
            Assert.True(GetWindowRect(handle, out NativeRect rect));
            Assert.Equal(141, rect.Right - rect.Left);
            Assert.Equal(5, rect.Bottom - rect.Top);
            Assert.Equal(5, textBox.NativeHeightFor(5));
        });

    [Fact]
    public void NativeAutoSizeChangesPreserveTheHandleTextSelectionAndModifiedState()
        => OnSta(() =>
        {
            using var textBox = new SizingTextBox { Text = "alpha 中文 beta" };
            nint handle = textBox.Handle;
            textBox.Select(2, 5);
            textBox.Modified = true;
            int textEvents = 0;
            int modifiedEvents = 0;
            int createdEvents = 0;
            textBox.TextChanged += (_, _) => textEvents++;
            textBox.ModifiedChanged += (_, _) => modifiedEvents++;
            textBox.HandleCreated += (_, _) => createdEvents++;

            textBox.AutoSize = false;
            textBox.Height = 79;
            textBox.AutoSize = true;
            textBox.AutoSize = false;

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(79, textBox.Height);
            Assert.Equal("alpha 中文 beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.True(textBox.Modified);
            Assert.Equal(0, textEvents);
            Assert.Equal(0, modifiedEvents);
            Assert.Equal(0, createdEvents);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox AutoSize lifetime test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class SizingTextBox : TextBox
    {
        internal List<string> Trace { get; } = new();
        internal Exception? NextBoundsFailure { get; set; }
        internal int AutoSizeHookCalls { get; private set; }
        internal bool HasFixedHeight => GetStyle(ControlStyles.FixedHeight);
        internal int NativeHeightFor(int requested) => GetNativeHeight(requested);
        internal void SetCachedFontHeight(int height) => FontHeight = height;
        internal void CallPaddingChanged() => OnPaddingChanged(EventArgs.Empty);
        internal void CallBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
            => SetBoundsCore(x, y, width, height, specified);

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Trace.Add($"bounds:{height}");
            if (NextBoundsFailure is { } failure)
            {
                NextBoundsFailure = null;
                throw failure;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnAutoSizeChanged(EventArgs e)
        {
            AutoSizeHookCalls++;
            base.OnAutoSizeChanged(e);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint handle, out NativeRect rectangle);
}
