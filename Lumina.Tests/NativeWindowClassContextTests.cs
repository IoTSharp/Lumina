using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class NativeWindowClassContextTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EditSuperclassUsesTheSourceClassOfEachStaActivationContext(bool firstHosted)
    {
        ClassIdentity first = ObserveOnSta(firstHosted);
        ClassIdentity second = ObserveOnSta(!firstHosted);
        ClassIdentity repeated = ObserveOnSta(firstHosted);

        Assert.NotEqual(first.WindowProc, second.WindowProc);
        Assert.Equal(first, repeated);
    }

    private static ClassIdentity ObserveOnSta(bool hosted)
    {
        ClassIdentity identity = default;
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            nuint defaultContextCookie = 0;
            try
            {
                // Override any activation context inherited from the test
                // runner before selecting this observation's explicit mode.
                Assert.True(ActivateActCtx(0, out defaultContextCookie));
                using Form? form = hosted ? new Form() : null;
                if (form is not null) { _ = form.Handle; }
                var source = new NativeClass { Size = (uint)Marshal.SizeOf<NativeClass>() };
                Assert.True(GetClassInfoExW(0, "EDIT", ref source));
                identity = new(source.Instance, source.WindowProc, string.Empty);
                Assert.NotEqual(0, identity.WindowProc);

                using var textBox = new TextBox { Text = "native class identity" };
                if (form is not null) { form.Controls.Add(textBox); }
                nint handle = textBox.Handle;
                Assert.True(IsWindow(handle));
                var registeredName = new StringBuilder(256);
                Assert.NotEqual(0, GetClassNameW(handle, registeredName, registeredName.Capacity));
                identity = identity with { RegistrationName = registeredName.ToString() };
                if (form is not null)
                {
                    Assert.Equal(form.Handle, GetParent(handle));
                    Assert.Same(form, Control.FromHandle(GetParent(handle)));
                }

                nint original = (nint)typeof(Control).GetField("_originalWindowProc",
                    BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(textBox)!;
                Assert.True(identity.WindowProc == original,
                    $"hosted={hosted}; source instance=0x{identity.Instance:x}, " +
                    $"source wndproc=0x{identity.WindowProc:x}, superclass original=0x{original:x}");
            }
            catch (Exception exception) { failure = exception; }
            finally
            {
                Application.ExitThread();
                // ExitThread releases OLE outside a loop; the visual-style
                // owner normally cleans up in the outer loop's finally.
                // This fixture has no loop, so release its context explicitly
                // before removing the default context below it on the stack.
                typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                    BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                if (defaultContextCookie != 0) { Assert.True(DeactivateActCtx(0, defaultContextCookie)); }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "Native class context observation timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
        return identity;
    }

    private readonly record struct ClassIdentity(nint Instance, nint WindowProc, string RegistrationName);

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeClass
    {
        public uint Size;
        public uint Style;
        public nint WindowProc;
        public int ClassExtra;
        public int WindowExtra;
        public nint Instance;
        public nint Icon;
        public nint Cursor;
        public nint Background;
        public nint MenuName;
        public nint ClassName;
        public nint SmallIcon;
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetClassInfoExW(nint instance, string className, ref NativeClass nativeClass);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetClassNameW(nint handle, StringBuilder className, int maxCount);

    [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ActivateActCtx(nint context, out nuint cookie);

    [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeactivateActCtx(uint flags, nuint cookie);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
