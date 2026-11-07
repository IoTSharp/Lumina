using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxBackgroundColorSyncTests
{
    private const int EmGetCharFormat = 0x043A;
    private const int EmSetBackgroundColor = 0x0443;
    private const int EmSetCharFormat = 0x0444;
    private const int EmSetOptions = 0x044D;
    private const int EmStreamIn = 0x0449;
    private const int HandleCreatedMarker = -101;
    private const uint ColorMask = 0x40000000;
    private const uint BackgroundMask = 0x04000000;

    [Fact]
    public void BackgroundChangeHookOverridesTheStandardProtectedVirtualMember()
    {
        MethodInfo method = typeof(RichTextBox).GetMethod("OnBackColorChanged",
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(method);
        Assert.True(method.IsFamily);
        Assert.True(method.IsVirtual);
        Assert.False(method.IsFinal);
        Assert.Equal(typeof(void), method.ReturnType);
        Assert.Equal(typeof(Control), method.GetBaseDefinition().DeclaringType);
        Assert.Equal(typeof(EventArgs), Assert.Single(method.GetParameters()).ParameterType);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void BackgroundChangeBeforeCreationForwardsOriginalArgumentsWithoutReadingColor(bool nullArguments)
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { FailBackColorRead = true, Capture = true };
            EventArgs? arguments = nullArguments ? null : new EventArgs();
            object? sender = null;
            EventArgs? observed = EventArgs.Empty;
            int changes = 0;
            rich.BackColorChanged += (s, e) => { sender = s; observed = e; changes++; };

            rich.RaiseBackgroundChange(arguments);

            Assert.Same(rich, sender);
            Assert.Same(arguments, observed);
            Assert.Equal(1, changes);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Requests);
        });

    [Theory]
    [InlineData(0x00112233)]
    [InlineData(0x00654321)]
    [InlineData(0x00FFFFFF)]
    public void BackgroundChangeSubmitsWin32ColorBeforeTheBaseEvent(int rgb)
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { BackColor = Color.FromArgb(unchecked((int)0xFF000000) | rgb) };
            _ = rich.Handle;
            rich.SuppressTheme = true;
            rich.Capture = true;
            EventArgs arguments = new();
            rich.BackColorChanged += (sender, e) =>
            {
                Assert.Same(rich, sender);
                Assert.Same(arguments, e);
                NativeRequest request = Assert.Single(rich.Requests);
                Assert.Equal(EmSetBackgroundColor, request.Message);
                Assert.Equal((nint)0, request.WParam);
                Assert.Equal((nint)ColorTranslator.ToWin32(rich.BackColor), request.LParam);
            };

            rich.RaiseBackgroundChange(arguments);

            Assert.Single(rich.Requests);
        });

    [Fact]
    public void DirectBackgroundNotificationsSubmitEvenWhenTheColorIsUnchanged()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { BackColor = Color.CornflowerBlue };
            _ = rich.Handle;
            rich.SuppressTheme = true;
            rich.Capture = true;
            int changes = 0;
            rich.BackColorChanged += (_, _) => changes++;

            rich.RaiseBackgroundChange(EventArgs.Empty);
            rich.RaiseBackgroundChange(EventArgs.Empty);

            Assert.Equal(2, changes);
            Assert.Equal(2, rich.Requests.Count);
            Assert.All(rich.Requests, request =>
            {
                Assert.Equal(EmSetBackgroundColor, request.Message);
                Assert.Equal((nint)0, request.WParam);
                Assert.Equal((nint)ColorTranslator.ToWin32(Color.CornflowerBlue), request.LParam);
            });
        });

    [Fact]
    public void AnUnchangedPropertyAssignmentDoesNotRaiseAnotherBackgroundNotification()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { BackColor = Color.SeaGreen };
            _ = rich.Handle;
            rich.Capture = true;
            int changes = 0;
            rich.BackColorChanged += (_, _) => changes++;

            rich.BackColor = Color.SeaGreen;

            Assert.Equal(0, changes);
            Assert.Empty(rich.Requests);
            Assert.Equal(ColorTranslator.ToWin32(Color.SeaGreen), ReadBackground(rich.Handle));
        });

    [Fact]
    public void BackgroundNotificationReadsTheVirtualEffectiveColor()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { BackColor = Color.Red };
            _ = rich.Handle;
            rich.SuppressTheme = true;
            rich.ColorOverride = Color.DarkSlateBlue;
            rich.Capture = true;

            rich.RaiseBackgroundChange(EventArgs.Empty);

            NativeRequest request = Assert.Single(rich.Requests);
            Assert.Equal((nint)ColorTranslator.ToWin32(Color.DarkSlateBlue), request.LParam);
            rich.Capture = false;
            Assert.Equal(ColorTranslator.ToWin32(Color.DarkSlateBlue), ReadBackground(rich.Handle));
        });

    [Fact]
    public void AnEffectiveColorGetterFailurePreventsNativeSubmissionAndTheBaseEventAndCanRetry()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { BackColor = Color.IndianRed };
            _ = rich.Handle;
            rich.SuppressTheme = true;
            rich.Capture = true;
            rich.FailBackColorRead = true;
            int changes = 0;
            rich.BackColorChanged += (_, _) => changes++;

            Assert.Same(rich.ColorReadFailure,
                Assert.Throws<InvalidOperationException>(() => rich.RaiseBackgroundChange(EventArgs.Empty)));
            Assert.Empty(rich.Requests);
            Assert.Equal(0, changes);

            rich.FailBackColorRead = false;
            rich.RaiseBackgroundChange(EventArgs.Empty);
            Assert.Equal(1, changes);
            Assert.Single(rich.Requests);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(723)]
    public void BackgroundNotificationIgnoresNativeReturnAndStillRaisesTheBaseEvent(int nativeResult)
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox();
            _ = rich.Handle;
            rich.SuppressTheme = true;
            rich.Capture = true;
            rich.BackgroundResult = nativeResult;
            int changes = 0;
            rich.BackColorChanged += (_, _) => changes++;

            rich.RaiseBackgroundChange(EventArgs.Empty);

            Assert.Equal(1, changes);
            NativeRequest request = Assert.Single(rich.Requests);
            Assert.Equal(EmSetBackgroundColor, request.Message);
        });

    [Fact]
    public void ABaseEventFailureRetainsTheNativeBackgroundAlreadySubmitted()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox();
            _ = rich.Handle;
            InvalidOperationException failure = new("background event failure");
            rich.BackColorChanged += (_, _) => throw failure;

            Assert.Same(failure,
                Assert.Throws<InvalidOperationException>(() => rich.BackColor = Color.Orchid));

            Assert.Equal(Color.Orchid, rich.BackColor);
            Assert.Equal(ColorTranslator.ToWin32(Color.Orchid), ReadBackground(rich.Handle));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void BackgroundAssignmentBeforeCreationKeepsItsColorWithoutCreatingAWindow(bool reset)
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { Capture = true };
            rich.BackColor = Color.Bisque;
            if (reset) { rich.ResetBackColor(); }

            Assert.Equal(reset ? SystemColors.Window : Color.Bisque, rich.BackColor);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(0, rich.CreatedCount);
            Assert.Empty(rich.Requests);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CreatedWindowUsesItsEffectiveBackgroundAndDefaultForegroundBeforeHandleCreated(bool explicitColors)
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox();
            if (explicitColors)
            {
                rich.BackColor = Color.LightGoldenrodYellow;
                rich.ForeColor = Color.DarkMagenta;
            }
            Color expectedBackColor = rich.BackColor;
            Color expectedForeColor = rich.ForeColor;
            int handleEvents = 0;
            int observedBackground = -1;
            int observedForeground = -1;
            rich.HandleCreated += (_, _) =>
            {
                handleEvents++;
                observedBackground = ReadBackground(rich.Handle);
                observedForeground = ReadFormat(rich.Handle, 0).TextColor;
            };

            _ = rich.Handle;

            Assert.Equal(1, handleEvents);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(ColorTranslator.ToWin32(expectedBackColor), observedBackground);
            Assert.Equal(ColorTranslator.ToWin32(expectedForeColor), observedForeground);
            Assert.Equal(ColorTranslator.ToWin32(expectedBackColor), ReadBackground(rich.Handle));
            Assert.Equal(ColorTranslator.ToWin32(expectedForeColor), ReadFormat(rich.Handle, 0).TextColor);
        });

    [Fact]
    public void CreationInitializesColorsAfterCachedSelectionBackgroundAndOptionsBeforeTextAndBaseEvent()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox
            {
                Text = "abcdef",
                ForeColor = Color.DarkGreen,
                BackColor = Color.LemonChiffon,
                SelectionBackColor = Color.Cyan,
                Capture = true
            };
            rich.HandleCreated += (_, _) => rich.Requests.Add(new NativeRequest(HandleCreatedMarker, 0, 0, null));

            _ = rich.Handle;

            int selection = rich.Requests.FindIndex(request => request.Message == EmSetCharFormat
                && request.WParam == 1 && request.Bytes is not null
                && BitConverter.ToUInt32(request.Bytes, 4) == BackgroundMask);
            int options = rich.Requests.FindIndex(request => request.Message == EmSetOptions);
            // Control applies the theme once before RichTextBox.OnHandleCreated.
            // Locate this hook's explicit initialization after AutoWordSelection.
            int background = rich.Requests.FindIndex(options + 1,
                request => request.Message == EmSetBackgroundColor);
            int foreground = rich.Requests.FindIndex(request => request.Message == EmSetCharFormat
                && request.WParam == 4 && request.Bytes is not null
                && BitConverter.ToUInt32(request.Bytes, 4) == ColorMask);
            int handleEvent = rich.Requests.FindIndex(request => request.Message == HandleCreatedMarker);
            int stream = rich.Requests.FindIndex(request => request.Message == EmStreamIn);
            Assert.True(selection >= 0);
            Assert.True(options > selection);
            Assert.True(background > options);
            Assert.True(foreground > background);
            Assert.True(handleEvent > foreground);
            Assert.True(stream > handleEvent);
            Assert.Equal("abcdef", rich.Text);
            Assert.Equal(ColorTranslator.ToWin32(Color.LemonChiffon), ReadBackground(rich.Handle));
            Assert.Equal(ColorTranslator.ToWin32(Color.DarkGreen), ReadFormat(rich.Handle, 0).TextColor);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void BackgroundChangeIsVisibleInsideTheEventAndKeepsTheDocumentState(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new BackgroundRichTextBox { Text = "abcdef", MaxLength = 20 };
            form.Controls.Add(rich);
            _ = form.Handle;
            _ = rich.Handle;
            rich.Select(2, 2);
            rich.SelectedText = "XYZ";
            rich.Select(1, 3);
            rich.SelectionColor = Color.Crimson;
            rich.SelectionBackColor = Color.Yellow;
            rich.Modified = modified;
            string text = rich.Text;
            NativeState before = ReadState(rich.Handle);
            NativeFormat formatBefore = ReadFormat(rich.Handle, 1);
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;
            rich.BackColorChanged += (_, _) =>
                Assert.Equal(ColorTranslator.ToWin32(Color.Teal), ReadBackground(rich.Handle));

            rich.BackColor = Color.Teal;

            Assert.Equal(ColorTranslator.ToWin32(Color.Teal), ReadBackground(rich.Handle));
            Assert.Equal(text, rich.Text);
            Assert.Equal(before, ReadState(rich.Handle));
            Assert.Equal(formatBefore, ReadFormat(rich.Handle, 1));
            Assert.Equal(20, rich.MaxLength);
            Assert.Equal((nint)20, SendMessageW(rich.Handle, 0x00D5, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void NestedBackgroundAssignmentLeavesTheLastEffectiveColorInTheNativeWindow()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox();
            _ = rich.Handle;
            int events = 0;
            rich.BackColorChanged += (_, _) =>
            {
                events++;
                if (events == 1)
                {
                    Assert.Equal(ColorTranslator.ToWin32(Color.Tomato), ReadBackground(rich.Handle));
                    rich.BackColor = Color.DodgerBlue;
                }
            };

            rich.BackColor = Color.Tomato;

            Assert.Equal(2, events);
            Assert.Equal(Color.DodgerBlue, rich.BackColor);
            Assert.Equal(ColorTranslator.ToWin32(Color.DodgerBlue), ReadBackground(rich.Handle));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RtfExplicitColorsAndViewBackgroundSurviveRecreation(bool modified)
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox
            {
                ForeColor = Color.DarkGreen,
                BackColor = Color.Navy
            };
            rich.Rtf = @"{\rtf1\ansi{\colortbl;\red255\green0\blue0;\red255\green255\blue0;}\cf1\highlight2\b abcdef}";
            rich.Select(1, 3);
            rich.Modified = modified;
            NativeFormat before = ReadFormat(rich.Handle, 1);
            Assert.Equal(ColorTranslator.ToWin32(Color.Red), before.TextColor);
            Assert.Equal(ColorTranslator.ToWin32(Color.Yellow), before.BackColor);
            int textChanges = 0;
            rich.TextChanged += (_, _) => textChanges++;

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal("abcdef", rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal(before, ReadFormat(rich.Handle, 1));
            Assert.Equal(ColorTranslator.ToWin32(Color.Navy), ReadBackground(rich.Handle));
            Assert.Equal(Color.DarkGreen, rich.ForeColor);
            Assert.Equal(0, textChanges);
        });

    [Fact]
    public void RtfAutomaticForegroundUsesTheInitializedDefaultDocumentColor()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { ForeColor = Color.DarkViolet, BackColor = Color.Honeydew };
            rich.Rtf = @"{\rtf1\ansi\cf0 abcdef}";
            rich.Select(0, 6);

            Assert.Equal(ColorTranslator.ToWin32(Color.DarkViolet), ReadFormat(rich.Handle, 1).TextColor);
            Assert.Equal(ColorTranslator.ToWin32(Color.Honeydew), ReadBackground(rich.Handle));
            Assert.Equal("abcdef", rich.Text);
        });

    [Fact]
    public void ResetBackgroundResubmitsTheInheritedEffectiveValue()
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox { BackColor = Color.Gold };
            _ = rich.Handle;

            rich.ResetBackColor();

            Assert.Equal(SystemColors.Window, rich.BackColor);
            Assert.Equal(ColorTranslator.ToWin32(SystemColors.Window), ReadBackground(rich.Handle));
        });

    [Theory]
    [InlineData(0x00112233)]
    [InlineData(0x00EEDDAA)]
    [InlineData(0x00000000)]
    public void BackgroundMutationMatchesAnIndependentSystemRichEdit(int rgb)
        => OnSta(() =>
        {
            using var rich = new BackgroundRichTextBox();
            _ = rich.Handle;
            nint native = CreateWindowExW(0, "RICHEDIT50W", string.Empty,
                unchecked((int)0x80000004), 0, 0, 300, 140, 0, 0, 0, 0);
            Assert.NotEqual((nint)0, native);
            try
            {
                Color color = Color.FromArgb(unchecked((int)0xFF000000) | rgb);
                rich.BackColor = color;
                _ = SendMessageW(native, EmSetBackgroundColor, 0, ColorTranslator.ToWin32(color));

                Assert.Equal(ColorTranslator.ToWin32(color), ReadBackground(native));
                Assert.Equal(ReadBackground(native), ReadBackground(rich.Handle));
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    private static int ReadBackground(nint handle)
    {
        int previous = (int)SendMessageW(handle, EmSetBackgroundColor, 0, 0x00010203);
        _ = SendMessageW(handle, EmSetBackgroundColor, 0, previous);
        return previous;
    }

    private static NativeFormat ReadFormat(nint handle, int range)
    {
        nint buffer = Marshal.AllocHGlobal(116);
        try
        {
            Marshal.Copy(new byte[116], 0, buffer, 116);
            Marshal.WriteInt32(buffer, 116);
            _ = SendMessageW(handle, EmGetCharFormat, range, buffer);
            return new NativeFormat(unchecked((uint)Marshal.ReadInt32(buffer, 4)),
                unchecked((uint)Marshal.ReadInt32(buffer, 8)), Marshal.ReadInt32(buffer, 12),
                Marshal.ReadInt32(buffer, 20), Marshal.ReadInt32(buffer, 96));
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static NativeState ReadState(nint handle)
    {
        _ = SendMessageW(handle, 0x00B0, out int start, out int end);
        return new NativeState(start, end, SendMessageW(handle, 0x00B8, 0, 0) != 0,
            SendMessageW(handle, 0x00C6, 0, 0) != 0);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox background synchronization test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeState(int Start, int End, bool Modified, bool CanUndo);
    private readonly record struct NativeFormat(uint Mask, uint Effects, int Height, int TextColor, int BackColor);
    private readonly record struct NativeRequest(int Message, nint WParam, nint LParam, byte[]? Bytes);

    private sealed class BackgroundRichTextBox : RichTextBox
    {
        internal bool Capture { get; set; }
        internal bool SuppressTheme { get; set; }
        internal bool FailBackColorRead { get; set; }
        internal Color? ColorOverride { get; set; }
        internal int? BackgroundResult { get; set; }
        internal InvalidOperationException ColorReadFailure { get; } = new("background color getter failure");
        internal List<NativeRequest> Requests { get; } = [];
        internal int CreatedCount { get; private set; }

        public override Color BackColor
        {
            get
            {
                if (FailBackColorRead) { throw ColorReadFailure; }
                return ColorOverride ?? base.BackColor;
            }
            set => base.BackColor = value;
        }

        internal void RaiseBackgroundChange(EventArgs? arguments) => base.OnBackColorChanged(arguments!);
        internal void RecreateNow() => RecreateHandle();

        protected override void ApplyTheme()
        {
            if (!SuppressTheme) { base.ApplyTheme(); }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void WndProc(ref Message message)
        {
            if (Capture && message.Msg is EmSetBackgroundColor or EmSetCharFormat or EmSetOptions or EmStreamIn)
            {
                byte[]? bytes = null;
                if (message.Msg == EmSetCharFormat)
                {
                    Assert.Equal(116, Marshal.ReadInt32(message.LParam));
                    bytes = new byte[116];
                    Marshal.Copy(message.LParam, bytes, 0, bytes.Length);
                }
                Requests.Add(new NativeRequest(message.Msg, message.WParam, message.LParam, bytes));
                if (message.Msg == EmSetBackgroundColor && BackgroundResult is int result)
                {
                    message.Result = result;
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

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(int extendedStyle, string className, string title,
        int style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);
}
