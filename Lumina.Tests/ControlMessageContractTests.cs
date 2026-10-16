using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlMessageContractTests
{
    [Fact]
    public void MessageEqualityIncludesResultButHashUsesWindowAndMessage()
    {
        Message first = Message.Create((nint)0x1234, 0x000C, 0x3456, -7);
        Message second = first;
        Assert.Equal(first, second);
        Assert.Equal(first.GetHashCode(), second.GetHashCode());

        second.Result = 91;
        Assert.NotEqual(first, second);
        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void MessageGetLParamReadsASequentialPayloadAndValidatesType()
    {
        nint pointer = Marshal.AllocHGlobal(Marshal.SizeOf<MessagePayload>());
        try
        {
            Marshal.StructureToPtr(new MessagePayload { Number = 23, Handle = 456 }, pointer, false);
            Message message = Message.Create(0, 0, 0, pointer);
            var payload = Assert.IsType<MessagePayload>(message.GetLParam(typeof(MessagePayload)));
            Assert.Equal(23, payload.Number);
            Assert.Equal((nint)456, payload.Handle);
            Assert.Throws<ArgumentNullException>(() => message.GetLParam(null!));
            Assert.Null(Message.Create(0, 0, 0, 0).GetLParam(typeof(MessagePayload)));
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    [Fact]
    public void CreateParamsToStringMatchesWinFormsShape()
    {
        var parameters = new CreateParams
        {
            ClassName = "STATIC",
            Caption = "caption",
            Style = unchecked((int)0xA1234567),
            ExStyle = 0x200,
            X = -11,
            Y = 12,
            Width = -13,
            Height = 14,
        };

        Assert.Equal("CreateParams {'STATIC', 'caption', 0xa1234567, 0x200, {-11, 12, -13, 14}}", parameters.ToString());
    }

    [Fact]
    public void PaintArgsBorrowGraphicsWhenDisposed()
    {
        using Bitmap bitmap = new(10, 10);
        using Graphics graphics = Graphics.FromImage(bitmap);
        var args = new PaintEventArgs(graphics, new Rectangle(2, 3, 4, 5));
        Assert.Same(graphics, args.Graphics);
        Assert.Equal(new Rectangle(2, 3, 4, 5), args.ClipRectangle);
        args.Dispose();
        using Pen pen = new(Color.Black);
        args.Graphics.DrawRectangle(pen, 0, 0, 1, 1);
    }

    [Fact]
    public void FormPaintAndPrintClientReachTheStandardPaintHooks() => OnSta(() =>
    {
        using var form = new MessageForm { Bounds = new Rectangle(40, 40, 260, 180) };
        int paints = 0;
        form.Paint += (_, args) =>
        {
            paints++;
            args.Graphics.FillRectangle(Brushes.Lime, 0, 0, 12, 12);
        };
        form.Show();
        int previousPaints = paints;
        int previousBackgrounds = form.BackgroundPaints;
        form.Refresh();
        Assert.True(paints > previousPaints);
        Assert.True(form.BackgroundPaints > previousBackgrounds);

        form.Buffered = true;
        previousPaints = paints;
        form.Refresh();
        Assert.True(paints > previousPaints);

        using Bitmap bitmap = new(form.ClientSize.Width, form.ClientSize.Height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        nint hdc = graphics.GetHdc();
        previousPaints = paints;
        try { _ = SendMessageW(form.Handle, 0x0318 /* WM_PRINTCLIENT */, hdc, 4 /* PRF_CLIENT */); }
        finally { graphics.ReleaseHdc(hdc); }
        Assert.Equal(previousPaints + 1, paints);
        Assert.Equal(Color.Lime.ToArgb(), bitmap.GetPixel(3, 3).ToArgb());
    });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void FormNativeDestructionAlwaysReleasesMappingsBrushesAndRoots(bool throwOnDestroy) => OnSta(() =>
    {
        WeakReference reference = DestroyRecreateAndDispose(throwOnDestroy);
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        Assert.False(reference.IsAlive);
    });

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference DestroyRecreateAndDispose(bool throwOnDestroy)
    {
        using var form = new MessageForm { ConsumeDestroy = !throwOnDestroy, ThrowOnDestroy = throwOnDestroy };
        Exception? reported = null;
        ThreadExceptionEventHandler handler = (_, args) => reported = args.Exception;
        Application.ThreadException += handler;
        try
        {
            nint handle = form.Handle;
            nint hdc = GetDC(handle);
            nint brush;
            try { brush = SendMessageW(handle, 0x0136 /* WM_CTLCOLORDLG */, hdc, 0); }
            finally { _ = ReleaseDC(handle, hdc); }
            Assert.NotEqual(0, brush);
            Assert.Equal(2u /* OBJ_BRUSH */, GetObjectType(brush));

            Assert.True(DestroyWindow(handle));
            Assert.False(IsWindow(handle));
            Assert.False(form.IsHandleCreated);
            Assert.Null(Control.FromHandle(handle));
            Assert.Equal(0u, GetObjectType(brush));
            if (throwOnDestroy) { Assert.Same(form.DestroyFailure, reported); }
            else { Assert.Null(reported); }

            nint recreated = form.Handle;
            Assert.True(IsWindow(recreated));
            Assert.Same(form, Control.FromHandle(recreated));
            form.Dispose();
            Assert.False(IsWindow(recreated));
            Assert.Null(Control.FromHandle(recreated));
            return new WeakReference(form);
        }
        finally { Application.ThreadException -= handler; }
    }

    [Fact]
    public void FormNativeClassUsesItsOriginalProcedureAndDoesNotOccupyUserData() => OnSta(() =>
    {
        using var form = new MessageForm { NativeClassName = "STATIC", Text = "native caption" };
        nint handle = form.Handle;
        Assert.True(form.FirstMessageHadHandle);
        Assert.True(form.FirstMessageHadMapping);
        _ = SetWindowLongPtrW(handle, -21 /* GWLP_USERDATA */, 0x1234);
        Assert.Equal((nint)0x1234, GetWindowLongPtrW(handle, -21));
        Assert.Equal((nint)14, form.ReadNativeTextLength());
        Assert.Equal((nint)0x1234, GetWindowLongPtrW(handle, -21));
    });

    private sealed partial class MessageForm : Form
    {
        internal bool ConsumeDestroy { get; init; }
        internal bool ThrowOnDestroy { get; init; }
        internal string? NativeClassName { get; init; }
        internal Exception DestroyFailure { get; } = new InvalidOperationException("Form NCDESTROY callback");
        internal int BackgroundPaints { get; private set; }
        internal bool FirstMessageHadHandle { get; private set; }
        internal bool FirstMessageHadMapping { get; private set; }
        private bool _firstMessage = true;

        internal bool Buffered { set => DoubleBuffered = value; }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parameters = base.CreateParams;
                parameters.ClassName = NativeClassName;
                return parameters;
            }
        }

        internal nint ReadNativeTextLength()
        {
            Message message = Message.Create(Handle, 0x000E /* WM_GETTEXTLENGTH */, 0, 0);
            DefWndProc(ref message);
            return message.Result;
        }

        protected override void WndProc(ref Message m)
        {
            if (_firstMessage)
            {
                _firstMessage = false;
                FirstMessageHadHandle = IsHandleCreated && Handle == m.HWnd;
                FirstMessageHadMapping = ReferenceEquals(this, Control.FromHandle(m.HWnd));
            }
            if (m.Msg == 0x0082 /* WM_NCDESTROY */)
            {
                if (ThrowOnDestroy) { throw DestroyFailure; }
                if (ConsumeDestroy) { m.Result = 0; return; }
            }
            base.WndProc(ref m);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            BackgroundPaints++;
            base.OnPaintBackground(e);
        }
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        Thread thread = new(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "The Form message test did not finish.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("user32.dll")]
    private static extern nint SendMessageW(nint hwnd, int message, nint wParam, nint lParam);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hwnd);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hwnd);
    [DllImport("user32.dll")]
    private static extern nint GetDC(nint hwnd);
    [DllImport("user32.dll")]
    private static extern int ReleaseDC(nint hwnd, nint hdc);
    [DllImport("gdi32.dll")]
    private static extern uint GetObjectType(nint handle);
    [DllImport("user32.dll")]
    private static extern nint SetWindowLongPtrW(nint hwnd, int index, nint value);
    [DllImport("user32.dll")]
    private static extern nint GetWindowLongPtrW(nint hwnd, int index);

    [StructLayout(LayoutKind.Sequential)]
    private struct MessagePayload
    {
        public int Number;
        public nint Handle;
    }
}
