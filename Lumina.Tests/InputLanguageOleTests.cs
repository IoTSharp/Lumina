using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;
using Xunit.Abstractions;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class InputLanguageOleTests(ITestOutputHelper output)
{
    private const int S_OK = 0;
    private const int S_FALSE = 1;
    private const int RPC_E_CHANGED_MODE = unchecked((int)0x80010106);

    [Fact]
    public void CurrentGetterRetainsOleInitializationUntilOwnerExit()
        => OnApartment(ApartmentState.STA, () =>
        {
            int baseline = ProbeOleInitialization();
            Assert.InRange(baseline, S_OK, S_FALSE);
            nint nativeLayout = GetKeyboardLayout(0);

            Assert.Equal(nativeLayout, InputLanguage.CurrentInputLanguage.Handle);

            AssertRetainedThenReleased(baseline);
        });

    [Fact]
    public void CurrentSetterInitializesOleBeforeActivatingTheLayout()
        => OnApartment(ApartmentState.STA, () =>
        {
            int baseline = ProbeOleInitialization();
            Assert.InRange(baseline, S_OK, S_FALSE);
            nint nativeLayout = GetKeyboardLayout(0);

            InputLanguage.CurrentInputLanguage = CreateLanguage(nativeLayout);

            Assert.Equal(nativeLayout, GetKeyboardLayout(0));
            AssertRetainedThenReleased(baseline);
        });

    [Fact]
    public void NullSetterInitializesOleAndResolvesTheNativeDefault()
        => OnApartment(ApartmentState.STA, () =>
        {
            int baseline = ProbeOleInitialization();
            Assert.InRange(baseline, S_OK, S_FALSE);
            nint original = GetKeyboardLayout(0);
            try
            {
                InputLanguage.CurrentInputLanguage = null;

                Assert.Equal(InputLanguage.DefaultInputLanguage.Handle, GetKeyboardLayout(0));
                AssertRetainedThenReleased(baseline);
            }
            finally
            {
                Assert.NotEqual((nint)0, ActivateKeyboardLayout(original, 0));
            }
        });

    [Fact]
    public void RejectedLayoutStillPerformsTheOlePrecondition()
        => OnApartment(ApartmentState.STA, () =>
        {
            int baseline = ProbeOleInitialization();
            Assert.InRange(baseline, S_OK, S_FALSE);
            nint original = GetKeyboardLayout(0);

            ArgumentException error = Assert.Throws<ArgumentException>(() =>
                InputLanguage.CurrentInputLanguage = CreateLanguage(unchecked((nint)0x77773333)));

            Assert.Equal("value", error.ParamName);
            Assert.Equal(original, GetKeyboardLayout(0));
            AssertRetainedThenReleased(baseline);
        });

    [Fact]
    public void ApplicationForwardingAndRepeatedReadsShareOneOleInitialization()
        => OnApartment(ApartmentState.STA, () =>
        {
            int baseline = ProbeOleInitialization();
            Assert.InRange(baseline, S_OK, S_FALSE);
            nint original = GetKeyboardLayout(0);

            InputLanguage first = Application.CurrentInputLanguage;
            InputLanguage second = InputLanguage.CurrentInputLanguage;
            Application.CurrentInputLanguage = first;
            Assert.Equal(original, first.Handle);
            Assert.Equal(first, second);
            Assert.Equal(original, GetKeyboardLayout(0));
            Assert.Equal(ApartmentState.STA, Application.OleRequired());

            // One owner exit must return to the original native OLE state even
            // after all four public paths requested initialization.
            AssertRetainedThenReleased(baseline);
            Application.ExitThread();
            Assert.Equal(baseline, ProbeOleInitialization());
        });

    [Fact]
    public void DefaultInstalledAndLayoutMetadataDoNotAddAnOleInitialization()
        => OnApartment(ApartmentState.STA, () =>
        {
            int baseline = ProbeOleInitialization();
            Assert.InRange(baseline, S_OK, S_FALSE);
            nint original = GetKeyboardLayout(0);
            InputLanguage language = CreateLanguage(original);

            Assert.NotEqual((nint)0, InputLanguage.DefaultInputLanguage.Handle);
            Assert.NotEmpty(InputLanguage.InstalledInputLanguages);
            Assert.NotNull(language.Culture);
            Assert.False(string.IsNullOrEmpty(language.LayoutName));

            Assert.Equal(original, GetKeyboardLayout(0));
            Assert.Equal(baseline, ProbeOleInitialization());
        });

    [Fact]
    public void MtaLanguageAccessPreservesTheCallersComInitialization()
        => OnApartment(ApartmentState.MTA, () =>
        {
            // Own an explicit native MTA reference so the assertion does not
            // depend on when the runtime initializes a managed MTA thread.
            int initialized = CoInitializeEx(0, 0);
            Assert.InRange(initialized, S_OK, S_FALSE);
            try
            {
                Assert.Equal(RPC_E_CHANGED_MODE, ProbeOleInitialization());
                nint original = GetKeyboardLayout(0);

                Assert.Equal(original, InputLanguage.CurrentInputLanguage.Handle);
                InputLanguage.CurrentInputLanguage = CreateLanguage(original);
                Assert.Equal(ApartmentState.MTA, Application.OleRequired());
                ArgumentException error = Assert.Throws<ArgumentException>(() =>
                    Application.CurrentInputLanguage = CreateLanguage(unchecked((nint)0x77773333)));
                Assert.Equal("value", error.ParamName);
                Assert.Equal(original, GetKeyboardLayout(0));

                Application.ExitThread();

                Assert.Equal(S_OK, CoGetApartmentType(out int apartment, out _));
                Assert.Equal(1, apartment);
                Assert.Equal(RPC_E_CHANGED_MODE, ProbeOleInitialization());
            }
            finally
            {
                CoUninitialize();
            }
        });

    private void AssertRetainedThenReleased(int baseline)
    {
        int retained = ProbeOleInitialization();
        Assert.Equal(S_FALSE, retained);

        Application.ExitThread();

        int released = ProbeOleInitialization();
        output.WriteLine($"Balanced native OLE probes: baseline={baseline}, retained={retained}, released={released}.");
        // A preexisting external OLE reference can make the initial probe
        // S_FALSE; the receipt exposes that boundary instead of unwinding it.
        Assert.Equal(baseline, released);
    }

    private static int ProbeOleInitialization()
    {
        int result = OleInitialize(0);
        if (result >= 0)
        {
            // Balance only the reference acquired by this probe.
            OleUninitialize();
        }

        return result;
    }

    private static InputLanguage CreateLanguage(nint handle)
        => (InputLanguage)typeof(InputLanguage).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
            null, [typeof(nint)], null)!.Invoke([handle]);

    private static void OnApartment(ApartmentState apartment, Action action)
    {
        if (!OperatingSystem.IsWindows()) { return; }
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try { Application.ExitThread(); }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(apartment);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The input-language OLE worker did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("ole32.dll")]
    private static extern int OleInitialize(nint reserved);

    [DllImport("ole32.dll")]
    private static extern void OleUninitialize();

    [DllImport("ole32.dll")]
    private static extern int CoInitializeEx(nint reserved, uint flags);

    [DllImport("ole32.dll")]
    private static extern void CoUninitialize();

    [DllImport("ole32.dll")]
    private static extern int CoGetApartmentType(out int apartment, out int qualifier);

    [DllImport("user32.dll")]
    private static extern nint GetKeyboardLayout(uint threadId);

    [DllImport("user32.dll")]
    private static extern nint ActivateKeyboardLayout(nint layout, uint flags);
}
