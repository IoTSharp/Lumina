using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxProtectedNativeTests
{
    private const string Document = "before secret after";
    private const int WmSetText = 0x000C;
    private const int WmNotify = 0x004E;
    private const int WmClear = 0x0303;
    private const int EmReplaceSel = 0x00C2;
    private const int EmGetLimitText = 0x00D5;
    private const int EmGetLineCount = 0x00BA;
    private const int EmSetModify = 0x00B9;
    private const int EmExLimitText = 0x0435;
    private const int EmExGetSel = 0x0434;
    private const int EmExSetSel = 0x0437;
    private const int EmSetCharFormat = 0x0444;
    private const int EmSetEventMask = 0x0445;
    private const int EmGetEventMask = 0x043B;
    private const int EmStreamIn = 0x0449;
    private const int EnProtected = 0x0704;
    private const int EnmProtected = 0x00200000;

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 2)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 0)]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 0)]
    [InlineData(3, 1)]
    [InlineData(3, 2)]
    public void ProtectedSelectionRejectsPlainEmptyAndRtfReplacementAcrossRealParents(int host, int operation)
        => OnSta(() =>
        {
            using var form = new Form();
            using var panel = new Panel();
            using var rich = new ProtectedRichTextBox { Text = Document };
            nint handle = CreateHost(form, panel, rich, host);
            ProtectSecret(rich);
            rich.MaxLength = 3;
            rich.ClearUndo();
            rich.Modified = false;
            int textChanges = 0;
            int protectedEvents = 0;
            rich.TextChanged += (_, _) => textChanges++;
            rich.Protected += (sender, arguments) =>
            {
                Assert.Same(rich, sender);
                Assert.Same(EventArgs.Empty, arguments);
                Assert.Equal(Document, rich.Text);
                Assert.Equal(new NativeRange(7, 13), ReadSelection(handle));
                Assert.True(rich.SelectionProtected);
                protectedEvents++;
            };
            rich.Capture = true;

            if (operation == 0) { rich.SelectedText = "changed"; }
            else if (operation == 1) { rich.SelectedText = string.Empty; }
            else { rich.SelectedRtf = @"{\rtf1\ansi\b changed}"; }

            Assert.Equal(1, protectedEvents);
            Assert.Equal(1, rich.ProtectedCalls);
            Assert.Equal(Document, rich.Text);
            Assert.Equal(new NativeRange(7, 13), ReadSelection(handle));
            Assert.True(rich.SelectionProtected);
            Assert.False(rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal(3, rich.MaxLength);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.Equal(handle, rich.Handle);
            Assert.Equal(1, rich.CreatedCount);
            Assert.Equal(0, textChanges);
            Assert.DoesNotContain(EmSetModify, rich.SubmittedMessages);
            Assert.DoesNotContain(EmGetLineCount, rich.SubmittedMessages);
            Assert.Equal(operation == 1
                ? new[] { WmClear }
                : new[] { EmExLimitText, EmStreamIn, EmExLimitText }, rich.SubmittedMessages);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void ChangingTheProtectionBitIsAllowedAndEnablesTheNextReplacement(int host)
        => OnSta(() =>
        {
            using var form = new Form();
            using var panel = new Panel();
            using var rich = new ProtectedRichTextBox { Text = Document };
            _ = CreateHost(form, panel, rich, host);
            ProtectSecret(rich);
            int events = 0;
            rich.Protected += (_, _) => events++;

            rich.SelectionProtected = false;

            Assert.False(rich.SelectionProtected);
            Assert.Equal(0, events);
            Assert.Equal(0, rich.ProtectedCalls);
            rich.SelectedText = "open";
            Assert.Equal("before open after", rich.Text);
            Assert.Equal(0, events);
            Assert.Equal(0, rich.ProtectedCalls);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EntireDocumentReplacementRemainsAllowedWhenTheOldTextIsProtected(bool useRtf)
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox { Text = Document };
            _ = rich.Handle;
            rich.SelectAll();
            rich.SelectionProtected = true;
            int events = 0;
            rich.Protected += (_, _) => events++;

            if (useRtf) { rich.Rtf = @"{\rtf1\ansi new\b  document}"; }
            else { rich.Text = "new document"; }

            Assert.Equal("new document", rich.Text);
            Assert.Equal(0, events);
            Assert.Equal(0, rich.ProtectedCalls);
        });

    [Fact]
    public void ChangingOtherCharacterFormattingIsRejectedButRemovingProtectionIsAllowed()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox { Text = Document };
            _ = rich.Handle;
            ProtectSecret(rich);
            Color originalColor = rich.SelectionColor;
            Color requestedColor = originalColor.ToArgb() == Color.Blue.ToArgb() ? Color.DarkRed : Color.Blue;
            int events = 0;
            rich.Protected += (_, _) => events++;

            rich.SelectionColor = requestedColor;

            Assert.Equal(1, events);
            Assert.Equal(originalColor, rich.SelectionColor);
            Assert.Equal(Document, rich.Text);
            rich.SelectionProtected = false;
            rich.SelectionColor = requestedColor;
            Assert.Equal(requestedColor.ToArgb(), rich.SelectionColor.ToArgb());
            Assert.Equal(1, events);
        });

    [Fact]
    public void ChangingParagraphFormattingIsRejectedWithoutChangingTheDocument()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox { Text = Document };
            _ = rich.Handle;
            ProtectSecret(rich);
            HorizontalAlignment original = rich.SelectionAlignment;
            HorizontalAlignment requested = original == HorizontalAlignment.Center
                ? HorizontalAlignment.Right : HorizontalAlignment.Center;
            int events = 0;
            rich.Protected += (_, _) => events++;

            rich.SelectionAlignment = requested;

            Assert.Equal(1, events);
            Assert.Equal(original, rich.SelectionAlignment);
            Assert.Equal(Document, rich.Text);
            Assert.True(rich.SelectionProtected);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RecreatedRichEditRetainsProtectionAndStillRejectsSelectionReplacement(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var panel = new Panel();
            using var rich = new ProtectedRichTextBox { Text = Document };
            _ = CreateHost(form, panel, rich, 1);
            ProtectSecret(rich);
            rich.Modified = modified;

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(modified, rich.Modified);
            Assert.True(rich.SelectionProtected);
            Assert.NotEqual(0, (int)SendMessageW(rich.Handle, EmGetEventMask, 0, 0) & EnmProtected);
            int events = 0;
            rich.Protected += (_, _) => events++;
            rich.SelectedText = "changed";
            Assert.Equal(Document, rich.Text);
            Assert.Equal(new NativeRange(7, 13), ReadSelection(rich.Handle));
            Assert.Equal(1, events);
            Assert.Equal(modified, rich.Modified);
        });

    [Fact]
    public void RejectedSelectionDoesNotPreventEditingAnUnprotectedRange()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox { Text = Document };
            _ = rich.Handle;
            ProtectSecret(rich);
            int events = 0;
            rich.Protected += (_, _) => events++;
            rich.SelectedText = "blocked";
            Assert.Equal(Document, rich.Text);
            Assert.Equal(1, events);

            rich.Select(0, 6);
            rich.SelectedText = "open";

            Assert.Equal("open secret after", rich.Text);
            Assert.Equal(1, events);
            rich.Select(5, 6);
            Assert.True(rich.SelectionProtected);
        });

    [Fact]
    public void UnprotectingFromTheEventDoesNotRetroactivelyAllowTheRejectedOperation()
        => OnSta(() =>
        {
            using var rich = new ProtectedRichTextBox { Text = Document };
            _ = rich.Handle;
            ProtectSecret(rich);
            int events = 0;
            rich.Protected += (_, _) =>
            {
                events++;
                Assert.Equal(Document, rich.Text);
                rich.SelectionProtected = false;
            };

            rich.SelectedText = "blocked";

            Assert.Equal(1, events);
            Assert.Equal(Document, rich.Text);
            Assert.False(rich.SelectionProtected);
            rich.SelectedText = "open";
            Assert.Equal("before open after", rich.Text);
            Assert.Equal(1, events);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void IndependentRawRichEditUsesTheParentNotificationResultToAllowOrRejectChanges(bool reject)
        => OnSta(() =>
        {
            nint module = LoadLibraryExW("Msftedit.dll", 0, 0x00000800 /* LOAD_LIBRARY_SEARCH_SYSTEM32 */);
            Assert.NotEqual((nint)0, module);
            try
            {
                using var form = new RawProtectedForm { Reject = reject };
                nint handle = CreateWindowExW(0, "RICHEDIT50W", string.Empty,
                    0x40000004 /* WS_CHILD | ES_MULTILINE */, 0, 0, 240, 120, form.Handle, 0, 0, 0);
                Assert.NotEqual((nint)0, handle);
                try
                {
                    form.Source = handle;
                    _ = SendMessageW(handle, EmSetEventMask, 0, EnmProtected);
                    SendString(handle, WmSetText, "abcdef");
                    SetSelection(handle, 2, 4);
                    SetProtection(handle, true);
                    form.Notifications.Clear();
                    form.Results.Clear();

                    SendString(handle, EmReplaceSel, "XYZ");

                    Assert.Equal(reject ? "abcdef" : "abXYZef", ReadWindowText(handle));
                    ProtectedNotification notification = Assert.Single(form.Notifications);
                    Assert.Equal(handle, notification.Header.Window);
                    Assert.Equal(EnProtected, notification.Header.Code);
                    // RichEdit reports this programmatic REPLACESEL as WM_SETTEXT.
                    Assert.Equal((uint)WmSetText, notification.Message);
                    Assert.Equal(new NativeRange(2, 4), notification.Range);
                    Assert.Equal(reject ? (nint)1 : 0, Assert.Single(form.Results));
                }
                finally
                {
                    try { Assert.True(DestroyWindow(handle)); }
                    finally { form.Source = 0; }
                }
            }
            finally { Assert.True(FreeLibrary(module)); }
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void IndependentRawFormattingProtectionDependsOnTheEnabledNotificationMask(bool enableProtectionNotifications)
        => OnSta(() =>
        {
            nint module = LoadLibraryExW("Msftedit.dll", 0, 0x00000800 /* LOAD_LIBRARY_SEARCH_SYSTEM32 */);
            Assert.NotEqual((nint)0, module);
            try
            {
                using var form = new RawProtectedForm { Reject = true };
                nint handle = CreateWindowExW(0, "RICHEDIT50W", string.Empty,
                    0x40000004 /* WS_CHILD | ES_MULTILINE */, 0, 0, 240, 120, form.Handle, 0, 0, 0);
                Assert.NotEqual((nint)0, handle);
                try
                {
                    form.Source = handle;
                    SendString(handle, WmSetText, "abcdef");
                    SetSelection(handle, 2, 4);
                    SetNativeColor(handle, 0x000000FF /* red COLORREF */);
                    SetNativeBullet(handle, false);
                    SetProtection(handle, true);
                    Assert.True(ReadNativeProtection(handle));
                    Assert.Equal(0x000000FFu, ReadNativeColor(handle));
                    Assert.Equal((ushort)0, ReadNativeNumbering(handle));
                    _ = SendMessageW(handle, EmSetEventMask, 0,
                        enableProtectionNotifications ? EnmProtected : 0);
                    form.Notifications.Clear();
                    form.Results.Clear();

                    // These two requests contain no CFM_PROTECTED exemption.
                    SetNativeBullet(handle, true);
                    SetNativeColor(handle, 0x00FF0000 /* blue COLORREF */);

                    Assert.Equal(enableProtectionNotifications ? (ushort)0 : (ushort)1,
                        ReadNativeNumbering(handle));
                    Assert.Equal(enableProtectionNotifications ? 0x000000FFu : 0x00FF0000u,
                        ReadNativeColor(handle));
                    Assert.True(ReadNativeProtection(handle));
                    Assert.Equal("abcdef", ReadWindowText(handle));
                    Assert.Equal(new NativeRange(2, 4), ReadSelection(handle));
                    if (enableProtectionNotifications)
                    {
                        Assert.Equal(new uint[] { 0x0447 /* EM_SETPARAFORMAT */, EmSetCharFormat },
                            form.Notifications.Select(notification => notification.Message));
                        Assert.All(form.Notifications, notification =>
                        {
                            Assert.Equal(handle, notification.Header.Window);
                            Assert.Equal(EnProtected, notification.Header.Code);
                        });
                        Assert.Equal(new nint[] { 1, 1 }, form.Results);
                    }
                    else
                    {
                        Assert.Empty(form.Notifications);
                        Assert.Empty(form.Results);
                    }
                }
                finally
                {
                    try { Assert.True(DestroyWindow(handle)); }
                    finally { form.Source = 0; }
                }
            }
            finally { Assert.True(FreeLibrary(module)); }
        });

    private static nint CreateHost(Form form, Panel panel, ProtectedRichTextBox rich, int host)
    {
        if (host == 3)
        {
            nint original = rich.Handle;
            _ = form.Handle;
            form.Controls.Add(rich);
            Assert.Equal(original, rich.Handle);
            Assert.Equal(form.Handle, GetParent(rich.Handle));
            return original;
        }
        if (host != 0)
        {
            _ = form.Handle;
            if (host == 2)
            {
                form.Controls.Add(panel);
                _ = panel.Handle;
                panel.Controls.Add(rich);
            }
            else { form.Controls.Add(rich); }
        }
        nint handle = rich.Handle;
        if (host != 0) { Assert.Equal(host == 2 ? panel.Handle : form.Handle, GetParent(handle)); }
        Assert.NotEqual(0, (int)SendMessageW(handle, EmGetEventMask, 0, 0) & EnmProtected);
        return handle;
    }

    private static void ProtectSecret(RichTextBox rich)
    {
        rich.Select(7, 6);
        rich.SelectionProtected = true;
        Assert.True(rich.SelectionProtected);
        Assert.Equal(Document, rich.Text);
    }

    private static NativeRange ReadSelection(nint handle)
    {
        nint buffer = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(buffer, 0, -1);
            Marshal.WriteInt32(buffer, 4, -1);
            _ = SendMessageW(handle, EmExGetSel, 0, buffer);
            return new NativeRange(Marshal.ReadInt32(buffer), Marshal.ReadInt32(buffer, 4));
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SetSelection(nint handle, int start, int end)
    {
        nint buffer = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(buffer, 0, start);
            Marshal.WriteInt32(buffer, 4, end);
            _ = SendMessageW(handle, EmExSetSel, 0, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SetProtection(nint handle, bool protect)
    {
        var bytes = new byte[116];
        BitConverter.GetBytes(116).CopyTo(bytes, 0);
        BitConverter.GetBytes(0x10u).CopyTo(bytes, 4);
        BitConverter.GetBytes(protect ? 0x10u : 0u).CopyTo(bytes, 8);
        nint buffer = Marshal.AllocHGlobal(bytes.Length);
        try
        {
            Marshal.Copy(bytes, 0, buffer, bytes.Length);
            _ = SendMessageW(handle, EmSetCharFormat, 1, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SetNativeColor(nint handle, uint color)
    {
        var bytes = new byte[116];
        BitConverter.GetBytes(116).CopyTo(bytes, 0);
        BitConverter.GetBytes(0x40000000u /* CFM_COLOR */).CopyTo(bytes, 4);
        BitConverter.GetBytes(color).CopyTo(bytes, 20);
        nint buffer = Marshal.AllocHGlobal(bytes.Length);
        try
        {
            Marshal.Copy(bytes, 0, buffer, bytes.Length);
            _ = SendMessageW(handle, EmSetCharFormat, 1, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SetNativeBullet(nint handle, bool bullet)
    {
        var bytes = new byte[156];
        BitConverter.GetBytes(156).CopyTo(bytes, 0);
        BitConverter.GetBytes(0x20u /* PFM_NUMBERING */).CopyTo(bytes, 4);
        BitConverter.GetBytes(bullet ? (ushort)1 : (ushort)0).CopyTo(bytes, 8);
        nint buffer = Marshal.AllocHGlobal(bytes.Length);
        try
        {
            Marshal.Copy(bytes, 0, buffer, bytes.Length);
            _ = SendMessageW(handle, 0x0447 /* EM_SETPARAFORMAT */, 0, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static bool ReadNativeProtection(nint handle)
    {
        byte[] bytes = ReadNativeCharacterFormat(handle);
        return (BitConverter.ToUInt32(bytes, 4) & 0x10u) != 0
            && (BitConverter.ToUInt32(bytes, 8) & 0x10u) != 0;
    }

    private static uint ReadNativeColor(nint handle)
        => BitConverter.ToUInt32(ReadNativeCharacterFormat(handle), 20);

    private static byte[] ReadNativeCharacterFormat(nint handle)
    {
        var bytes = new byte[116];
        BitConverter.GetBytes(116).CopyTo(bytes, 0);
        nint buffer = Marshal.AllocHGlobal(bytes.Length);
        try
        {
            Marshal.Copy(bytes, 0, buffer, bytes.Length);
            _ = SendMessageW(handle, 0x043A /* EM_GETCHARFORMAT */, 1, buffer);
            Marshal.Copy(buffer, bytes, 0, bytes.Length);
            return bytes;
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static ushort ReadNativeNumbering(nint handle)
    {
        var bytes = new byte[156];
        BitConverter.GetBytes(156).CopyTo(bytes, 0);
        nint buffer = Marshal.AllocHGlobal(bytes.Length);
        try
        {
            Marshal.Copy(bytes, 0, buffer, bytes.Length);
            _ = SendMessageW(handle, 0x043D /* EM_GETPARAFORMAT */, 0, buffer);
            Marshal.Copy(buffer, bytes, 0, bytes.Length);
            return BitConverter.ToUInt16(bytes, 8);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SendString(nint handle, int message, string text)
    {
        nint buffer = Marshal.StringToHGlobalUni(text);
        try { _ = SendMessageW(handle, message, 0, buffer); }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static string ReadWindowText(nint handle)
    {
        var text = new StringBuilder(64);
        _ = GetWindowTextW(handle, text, text.Capacity);
        return text.ToString();
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox protected native test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ProtectedRichTextBox : RichTextBox
    {
        internal int CreatedCount { get; private set; }
        internal int ProtectedCalls { get; private set; }
        internal bool Capture { get; set; }
        internal List<int> SubmittedMessages { get; } = [];
        internal void RecreateNow() => RecreateHandle();

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void OnProtected(EventArgs e)
        {
            ProtectedCalls++;
            base.OnProtected(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (Capture && m.Msg is WmClear or EmExLimitText or EmStreamIn or EmSetModify or EmGetLineCount)
            {
                SubmittedMessages.Add(m.Msg);
            }
            base.WndProc(ref m);
        }
    }

    private sealed class RawProtectedForm : Form
    {
        internal nint Source { get; set; }
        internal bool Reject { get; set; }
        internal List<ProtectedNotification> Notifications { get; } = [];
        internal List<nint> Results { get; } = [];

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmNotify && m.LParam != 0)
            {
                NativeHeader header = Marshal.PtrToStructure<NativeHeader>(m.LParam);
                if (header.Window == Source && header.Code == EnProtected)
                {
                    Notifications.Add(Marshal.PtrToStructure<ProtectedNotification>(m.LParam));
                    m.Result = Reject ? 1 : 0;
                    Results.Add(m.Result);
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeHeader
    {
        internal nint Window;
        internal nuint Id;
        internal int Code;
    }

    [StructLayout(LayoutKind.Sequential)]
    private readonly record struct NativeRange(int Start, int End);

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct ProtectedNotification
    {
        internal NativeHeader Header;
        internal uint Message;
        internal nint WParam;
        internal nint LParam;
        internal NativeRange Range;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint handle, StringBuilder text, int maximum);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);

    [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint LoadLibraryExW(string fileName, nint file, uint flags);

    [DllImport("kernel32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FreeLibrary(nint module);
}
