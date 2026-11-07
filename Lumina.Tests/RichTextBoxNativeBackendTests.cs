using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxNativeBackendTests
{
    private const int WmChar = 0x0102;
    private const int EmGetSel = 0x00B0;
    private const int EmSetSel = 0x00B1;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmReplaceSel = 0x00C2;
    private const int EmCanUndo = 0x00C6;
    private const int EmGetLimitText = 0x00D5;
    private const int EmExLimitText = 0x0435;
    private const int EmGetEventMask = 0x043B;
    private const int EmSetEventMask = 0x0445;
    private const int EmGetTextLengthEx = 0x045F;
    private const int ChangeAndUpdateMask = 0x0003;

    [Fact]
    public void NativeSuperclassRetainsTheSystemRichEditProcedureAndModule()
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox();
            nint handle = CreateHostedHandle(form, richTextBox);

            Assert.Equal("RICHEDIT50W", richTextBox.RequestedParameters.ClassName);
            Assert.StartsWith("LuminaFormsWindow.RICHEDIT50W.", ReadClassName(handle));
            nint module = GetModuleHandleW("msftedit.dll");
            Assert.NotEqual((nint)0, module);
            var path = new char[1024];
            uint pathLength = GetModuleFileNameW(module, path, (uint)path.Length);
            Assert.InRange(pathLength, 1u, (uint)path.Length - 1);
            Assert.True(string.Equals(Path.Combine(Environment.SystemDirectory, "msftedit.dll"),
                new string(path, 0, (int)pathLength), StringComparison.OrdinalIgnoreCase));

            var sourceClass = new WindowClass { Size = (uint)Marshal.SizeOf<WindowClass>() };
            Assert.True(GetClassInfoExW(module, "RICHEDIT50W", ref sourceClass)
                || GetClassInfoExW(0, "RICHEDIT50W", ref sourceClass));
            nint originalProcedure = (nint)typeof(Control).GetField("_originalWindowProc",
                BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(richTextBox)!;
            Assert.NotEqual((nint)0, originalProcedure);
            Assert.Equal(sourceClass.WindowProcedure, originalProcedure);
            Assert.Same(richTextBox, Control.FromHandle(handle));
        });

    [Theory]
    [InlineData("native", 6)]
    [InlineData("中😀z", 4)]
    public void RichEditSpecificTextLengthMessageReachesTheOriginalNativeBackend(string text, int expectedLength)
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox { Text = text };
            nint handle = CreateHostedHandle(form, richTextBox);
            Assert.Equal(expectedLength, QueryRichLength(handle));

            // EDIT does not implement EM_GETTEXTLENGTHEX. An inherited EDIT
            // procedure with a renamed superclass must fail this comparison.
            CreateParams parameters = richTextBox.RequestedParameters;
            nint edit = CreateNativeWindow(form.Handle, parameters, "EDIT", text);
            try
            {
                Assert.Equal(text, ReadNativeText(edit));
                Assert.Equal(0, QueryRichLength(edit));
            }
            finally { Assert.True(DestroyWindow(edit)); }
        });

    [Fact]
    public void CreationInstallsChangeAndUpdateNotificationsAndTheFullRichEditLimit()
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox();
            nint handle = CreateHostedHandle(form, richTextBox);

            Assert.Equal(ChangeAndUpdateMask,
                (int)SendMessageW(handle, EmGetEventMask, 0, 0) & ChangeAndUpdateMask);
            Assert.Equal((nint)int.MaxValue, SendMessageW(handle, EmGetLimitText, 0, 0));
        });

    [Theory]
    [InlineData(1)]
    [InlineData(32768)]
    [InlineData(100000)]
    public void ConfiguredLengthUsesRichEditExtendedLimitBeforeAndAfterCreation(int limit)
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox { MaxLength = limit };
            Assert.False(richTextBox.IsHandleCreated);
            nint handle = CreateHostedHandle(form, richTextBox);
            Assert.Equal((nint)limit, SendMessageW(handle, EmGetLimitText, 0, 0));

            richTextBox.CaptureLimits = true;
            richTextBox.MaxLength = limit + 1;

            Assert.Equal(new[] { ((nint)0, (nint)(limit + 1)) }, richTextBox.ExtendedLimits);
            Assert.Equal((nint)(limit + 1), SendMessageW(handle, EmGetLimitText, 0, 0));
            Assert.Equal(handle, richTextBox.Handle);
        });

    [Theory]
    [InlineData(false, "abc")]
    [InlineData(true, "")]
    public void NativeTypingHonorsTheConfiguredLimitAndReadOnlyState(bool readOnly, string expected)
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox { MaxLength = 3, ReadOnly = readOnly };
            nint handle = CreateHostedHandle(form, richTextBox);

            _ = SendMessageW(handle, WmChar, 'a', 0);
            _ = SendMessageW(handle, WmChar, 'b', 0);
            _ = SendMessageW(handle, WmChar, 'c', 0);
            _ = SendMessageW(handle, WmChar, 'd', 0);

            Assert.Equal(expected, ReadNativeText(handle));
            Assert.Equal(expected, richTextBox.Text);
            Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
        });

    [Fact]
    public void ProgrammaticTextCanExceedTheInputLimitWithoutReplacingTheNativeHandle()
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox { MaxLength = 3 };
            nint handle = CreateHostedHandle(form, richTextBox);
            nint independent = CreateNativeWindow(form.Handle, richTextBox.RequestedParameters,
                "RICHEDIT50W", string.Empty);
            int changes = 0;
            richTextBox.TextChanged += (_, _) => changes++;
            try
            {
                _ = SendMessageW(independent, EmExLimitText, 0, 3);
                Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
                Assert.Equal((nint)3, SendMessageW(independent, EmGetLimitText, 0, 0));

                richTextBox.Text = "longer than three";
                Assert.True(SetWindowTextW(independent, "longer than three"));

                Assert.Equal("longer than three", ReadNativeText(handle));
                Assert.Equal(ReadNativeText(independent), ReadNativeText(handle));
                Assert.Equal("longer than three", richTextBox.Text);
                Assert.Equal(1, changes);
                Assert.False(richTextBox.Modified);
                Assert.Equal(3, richTextBox.MaxLength);
                // Batch059 streams programmatic text and restores MaxLength.
                // Retain WM_SETTEXT on the independent native window as the
                // counterexample: that message expands its input limit to 17.
                Assert.Equal((nint)17, SendMessageW(independent, EmGetLimitText, 0, 0));
                Assert.Equal((nint)3, SendMessageW(handle, EmGetLimitText, 0, 0));
                Assert.Equal(handle, richTextBox.Handle);
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    [Fact]
    public void HiddenRichEditNativeReplacementRaisesOneChangeAndKeepsNativeEditingState()
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox { Text = "abcdef" };
            nint handle = CreateHostedHandle(form, richTextBox);
            int changes = 0;
            richTextBox.TextChanged += (_, _) => changes++;

            _ = SendMessageW(handle, EmSetSel, 1, 4);
            _ = SendMessageW(handle, EmReplaceSel, 1, "X");

            Assert.Equal("aXef", ReadNativeText(handle));
            Assert.Equal("aXef", richTextBox.Text);
            Assert.Equal(1, changes);
            Assert.Equal(new NativeState(2, 2, true, true), ReadState(handle));
            Assert.Equal(2, richTextBox.SelectionStart);
            Assert.Equal(0, richTextBox.SelectionLength);
            Assert.True(richTextBox.Modified);
            Assert.True(richTextBox.CanUndo);
            Assert.Equal(handle, richTextBox.Handle);
        });

    [Fact]
    public void BasicReplacementMatchesAnIndependentRichEditWithTheSameConfiguration()
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox { Text = "abcdef", MaxLength = 8 };
            nint handle = CreateHostedHandle(form, richTextBox);
            nint independent = CreateNativeWindow(form.Handle, richTextBox.RequestedParameters,
                "RICHEDIT50W", "abcdef");
            try
            {
                _ = SendMessageW(independent, EmExLimitText, 0, 8);
                _ = SendMessageW(independent, EmSetEventMask, 0, ChangeAndUpdateMask);
                _ = SendMessageW(handle, EmSetModify, 0, 0);
                _ = SendMessageW(independent, EmSetModify, 0, 0);

                _ = SendMessageW(handle, EmSetSel, 1, 4);
                _ = SendMessageW(independent, EmSetSel, 1, 4);
                _ = SendMessageW(handle, EmReplaceSel, 1, "X");
                _ = SendMessageW(independent, EmReplaceSel, 1, "X");

                Assert.Equal("aXef", ReadNativeText(independent));
                Assert.Equal(ReadNativeText(independent), ReadNativeText(handle));
                Assert.Equal(QueryRichLength(independent), QueryRichLength(handle));
                Assert.Equal(ReadState(independent), ReadState(handle));
                Assert.Equal(SendMessageW(independent, EmGetLimitText, 0, 0),
                    SendMessageW(handle, EmGetLimitText, 0, 0));
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    [Fact]
    public void RecreationRestoresTextSelectionModificationAndTheRichEditConfiguration()
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox { Text = "recreate", MaxLength = 100000 };
            _ = CreateHostedHandle(form, richTextBox);
            richTextBox.Select(2, 3);
            richTextBox.Modified = true;
            int changes = 0;
            richTextBox.TextChanged += (_, _) => changes++;

            richTextBox.RecreateNow();

            nint recreated = richTextBox.Handle;
            Assert.Equal(2, richTextBox.CreatedCount);
            Assert.Equal(1, richTextBox.DestroyedCount);
            Assert.StartsWith("LuminaFormsWindow.RICHEDIT50W.", ReadClassName(recreated));
            Assert.Equal("recreate", ReadNativeText(recreated));
            Assert.Equal(new NativeState(2, 5, true, false), ReadState(recreated));
            Assert.Equal((nint)100000, SendMessageW(recreated, EmGetLimitText, 0, 0));
            Assert.Equal(ChangeAndUpdateMask,
                (int)SendMessageW(recreated, EmGetEventMask, 0, 0) & ChangeAndUpdateMask);
            Assert.Equal(0, changes);
        });

    [Fact]
    public void ExplicitDestructionRemovesTheNativeMappingAndCanCreateAnotherRichEdit()
        => OnSta(() =>
        {
            using var form = new Form();
            using var richTextBox = new NativeRichTextBox { Text = "survives" };
            nint original = CreateHostedHandle(form, richTextBox);

            richTextBox.DestroyNow();

            Assert.False(IsWindow(original));
            Assert.Null(Control.FromHandle(original));
            Assert.False(richTextBox.IsHandleCreated);
            Assert.Equal(1, richTextBox.DestroyedCount);

            nint recreated = richTextBox.Handle;
            Assert.True(IsWindow(recreated));
            Assert.Equal("survives", ReadNativeText(recreated));
            Assert.Equal(8, QueryRichLength(recreated));
            Assert.Equal(2, richTextBox.CreatedCount);
        });

    [Fact]
    public void DisposingOneRichEditKeepsAnotherControlAndItsNativeModuleUsable()
        => OnSta(() =>
        {
            using var form = new Form();
            using var first = new NativeRichTextBox { Text = "first" };
            using var second = new NativeRichTextBox { Text = "second" };
            nint firstHandle = CreateHostedHandle(form, first);
            nint secondHandle = CreateHostedHandle(form, second);
            nint module = GetModuleHandleW("msftedit.dll");

            first.Dispose();

            Assert.False(IsWindow(firstHandle));
            Assert.Null(Control.FromHandle(firstHandle));
            Assert.True(IsWindow(secondHandle));
            Assert.Equal(module, GetModuleHandleW("msftedit.dll"));
            Assert.Equal(6, QueryRichLength(secondHandle));
            second.Text = "still usable";
            Assert.Equal("still usable", ReadNativeText(secondHandle));
            using var third = new NativeRichTextBox { Text = "third" };
            Assert.Equal(5, QueryRichLength(CreateHostedHandle(form, third)));
        });

    private static nint CreateHostedHandle(Form form, RichTextBox richTextBox)
    {
        form.Controls.Add(richTextBox);
        nint parent = form.Handle;
        nint child = richTextBox.Handle;
        Assert.Equal(parent, GetParent(child));
        Assert.Same(form, Control.FromHandle(GetParent(child)));
        return child;
    }

    private static nint CreateNativeWindow(nint parent, CreateParams parameters, string className, string text)
    {
        nint handle = CreateWindowExW(unchecked((uint)parameters.ExStyle), className, text,
            unchecked((uint)parameters.Style) & ~0x10000000u, 0, 0, 180, 100,
            parent, 0, GetModuleHandleW(null), 0);
        Assert.NotEqual((nint)0, handle);
        return handle;
    }

    private static string ReadClassName(nint handle)
    {
        var buffer = new char[256];
        int length = GetClassNameW(handle, buffer, buffer.Length);
        Assert.InRange(length, 1, buffer.Length - 1);
        return new string(buffer, 0, length);
    }

    private static string ReadNativeText(nint handle)
    {
        var buffer = new char[256];
        int length = GetWindowTextW(handle, buffer, buffer.Length);
        Assert.InRange(length, 0, buffer.Length - 2);
        return new string(buffer, 0, length);
    }

    private static int QueryRichLength(nint handle)
    {
        var request = new GetTextLengthEx { Flags = 0x000A /* GTL_PRECISE | GTL_NUMCHARS */, CodePage = 1200 };
        return (int)SendMessageW(handle, EmGetTextLengthEx, ref request, 0);
    }

    private static NativeState ReadState(nint handle)
    {
        _ = SendMessageW(handle, EmGetSel, out int start, out int end);
        return new(start, end, SendMessageW(handle, EmGetModify, 0, 0) != 0,
            SendMessageW(handle, EmCanUndo, 0, 0) != 0);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox native backend test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeState(int Start, int End, bool Modified, bool CanUndo);

    [StructLayout(LayoutKind.Sequential)]
    private struct GetTextLengthEx
    {
        internal uint Flags;
        internal uint CodePage;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct WindowClass
    {
        internal uint Size;
        internal uint Style;
        internal nint WindowProcedure;
        internal int ClassExtra;
        internal int WindowExtra;
        internal nint Instance;
        internal nint Icon;
        internal nint Cursor;
        internal nint Background;
        internal nint MenuName;
        internal nint ClassName;
        internal nint SmallIcon;
    }

    private sealed class NativeRichTextBox : RichTextBox
    {
        internal CreateParams RequestedParameters => CreateParams;
        internal int CreatedCount { get; private set; }
        internal int DestroyedCount { get; private set; }
        internal bool CaptureLimits { get; set; }
        internal List<(nint WParam, nint LParam)> ExtendedLimits { get; } = new();

        internal void RecreateNow() => RecreateHandle();
        internal void DestroyNow() => DestroyHandle();

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            DestroyedCount++;
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message message)
        {
            if (CaptureLimits && message.Msg == EmExLimitText)
            {
                Assert.InRange(ExtendedLimits.Count, 0, 7);
                ExtendedLimits.Add((message.WParam, message.LParam));
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint GetModuleHandleW(string? moduleName);

    [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern uint GetModuleFileNameW(nint module, [Out] char[] path, uint count);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetClassInfoExW(nint instance, string className, ref WindowClass windowClass);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetClassNameW(nint handle, [Out] char[] className, int count);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint handle, [Out] char[] text, int count);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetWindowTextW(nint handle, string text);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, string lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, ref GetTextLengthEx wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, out int wParam, out int lParam);
}
