using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ApplicationOleStateTests
{
    private const int Succeeded = 0;
    private const int AlreadyInitialized = 1;
    private const int RpcEChangedMode = unchecked((int)0x80010106);
    private const uint CoinitMultithreaded = 0;
    private const uint CoinitApartmentThreaded = 2;
    private const int AptTypeSta = 0;
    private const int AptTypeMta = 1;
    private const int AptTypeMainSta = 3;

    // Test-only reflection binds the internal service without extending the
    // library's public WinForms surface or requiring a friend assembly.
    private static readonly Type s_stateType = typeof(Application).Assembly.GetType("Lumina.Forms.ApplicationOleState", throwOnError: true)!;
    private static readonly Func<ApartmentState> s_requireOle = s_stateType
        .GetMethod("OleRequired", BindingFlags.Static | BindingFlags.NonPublic)!
        .CreateDelegate<Func<ApartmentState>>();
    private static readonly Action s_releaseOle = s_stateType
        .GetMethod("ReleaseCurrentThread", BindingFlags.Static | BindingFlags.NonPublic)!
        .CreateDelegate<Action>();

    [Fact]
    public void FirstCallInitializesOleAndRepeatedCallsOwnOnlyOneOleReference()
        => OnApartmentThread(ApartmentState.STA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(Succeeded);
            Assert.Equal(ApartmentState.STA, s_requireOle());
            AssertOleProbe(AlreadyInitialized);
            Assert.Equal(ApartmentState.STA, s_requireOle());
            Assert.Equal(ApartmentState.STA, s_requireOle());

            s_releaseOle();
            AssertOleProbe(Succeeded);
            Assert.Equal(baseline, ReadNativeApartment());
        });

    [Fact]
    public void UnusedAndRepeatedReleaseLeaveOleUninitializedAndPreserveClrCom()
        => OnApartmentThread(ApartmentState.STA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(Succeeded);
            s_releaseOle();
            AssertOleProbe(Succeeded);
            Assert.Equal(ApartmentState.STA, s_requireOle());
            AssertOleProbe(AlreadyInitialized);
            s_releaseOle();
            s_releaseOle();

            AssertOleProbe(Succeeded);
            Assert.Equal(baseline, ReadNativeApartment());
        });

    [Fact]
    public void ReleasedOleStateCanAcquireAndReleaseAnotherOleReference()
        => OnApartmentThread(ApartmentState.STA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(Succeeded);
            Assert.Equal(ApartmentState.STA, s_requireOle());
            AssertOleProbe(AlreadyInitialized);
            s_releaseOle();
            AssertOleProbe(Succeeded);

            Assert.Equal(ApartmentState.STA, s_requireOle());
            AssertOleProbe(AlreadyInitialized);
            s_releaseOle();
            AssertOleProbe(Succeeded);
            Assert.Equal(baseline, ReadNativeApartment());
        });

    [Fact]
    public void LibraryOleCleanupPreservesExternalStaComStateAndItsKnownReference()
        => OnApartmentThread(ApartmentState.STA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(Succeeded);
            int initializationResult = CoInitializeEx(0, CoinitApartmentThreaded);
            try
            {
                Assert.InRange(initializationResult, Succeeded, AlreadyInitialized);
                NativeApartment withExternalReference = ReadNativeApartment();
                Assert.Equal(ApartmentState.STA, s_requireOle());
                Assert.Equal(ApartmentState.STA, s_requireOle());
                AssertOleProbe(AlreadyInitialized);
                s_releaseOle();
                AssertOleProbe(Succeeded);
                Assert.Equal(withExternalReference, ReadNativeApartment());
            }
            finally
            {
                try { s_releaseOle(); }
                finally
                {
                    if (initializationResult >= 0) { CoUninitialize(); }
                }
            }

            // The CLR owns the original COM initialization. Compare the native
            // baseline; do not drain unknown CLR references to count total COM use.
            Assert.Equal(baseline, ReadNativeApartment());
            AssertOleProbe(Succeeded);
        });

    [Fact]
    public void LibraryCleanupPreservesAnExternalOleReferenceUntilItsOwnerReleasesIt()
        => OnApartmentThread(ApartmentState.STA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(Succeeded);
            int initializationResult = OleInitialize(0);
            try
            {
                Assert.Equal(Succeeded, initializationResult);
                Assert.Equal(ApartmentState.STA, s_requireOle());
                Assert.Equal(ApartmentState.STA, s_requireOle());
                AssertOleProbe(AlreadyInitialized);
                s_releaseOle();
                AssertOleProbe(AlreadyInitialized);
                Assert.Equal(baseline, ReadNativeApartment());
            }
            finally
            {
                try { s_releaseOle(); }
                finally
                {
                    if (initializationResult >= 0) { OleUninitialize(); }
                }
            }

            AssertOleProbe(Succeeded);
            Assert.Equal(baseline, ReadNativeApartment());
        });

    [Fact]
    public void ChangedModeLeavesExternalMtaComStateIntactAfterLibraryCleanup()
        => OnApartmentThread(ApartmentState.MTA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(RpcEChangedMode);
            int initializationResult = CoInitializeEx(0, CoinitMultithreaded);
            try
            {
                Assert.InRange(initializationResult, Succeeded, AlreadyInitialized);
                NativeApartment withExternalReference = ReadNativeApartment();
                Assert.Equal(ApartmentState.MTA, s_requireOle());
                Assert.Equal(ApartmentState.MTA, s_requireOle());
                AssertOleProbe(RpcEChangedMode);
                s_releaseOle();
                Assert.Equal(withExternalReference, ReadNativeApartment());
                s_releaseOle();
                Assert.Equal(withExternalReference, ReadNativeApartment());
            }
            finally
            {
                try { s_releaseOle(); }
                finally
                {
                    if (initializationResult >= 0) { CoUninitialize(); }
                }
            }

            // This checks preservation of the observed MTA and our known native
            // reference, not a count of COM references owned by the CLR.
            Assert.Equal(baseline, ReadNativeApartment());
            AssertOleProbe(RpcEChangedMode);
        });

    [Fact]
    public void ReleasingCachedMtaAllowsAnotherMtaAttemptAndPreservesNativeState()
        => OnApartmentThread(ApartmentState.MTA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(RpcEChangedMode);
            Assert.Equal(ApartmentState.MTA, s_requireOle());
            Assert.Equal(ApartmentState.MTA, s_requireOle());
            s_releaseOle();

            Assert.Equal(baseline, ReadNativeApartment());
            Assert.Equal(ApartmentState.MTA, s_requireOle());
            AssertOleProbe(RpcEChangedMode);
            s_releaseOle();
            Assert.Equal(baseline, ReadNativeApartment());
        });

    [Fact]
    public void ReleasingUnusedLibraryStatePreservesExternalStaComWithoutOle()
        => OnApartmentThread(ApartmentState.STA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(Succeeded);
            int initializationResult = CoInitializeEx(0, CoinitApartmentThreaded);
            try
            {
                Assert.InRange(initializationResult, Succeeded, AlreadyInitialized);
                NativeApartment withExternalReference = ReadNativeApartment();
                s_releaseOle();
                Assert.Equal(withExternalReference, ReadNativeApartment());
                AssertOleProbe(Succeeded);
            }
            finally
            {
                if (initializationResult >= 0) { CoUninitialize(); }
            }

            Assert.Equal(baseline, ReadNativeApartment());
            AssertOleProbe(Succeeded);
        });

    [Fact]
    public void WorkerMtaStateAndCleanupDoNotReleaseTheOwnerStaOleReference()
        => OnApartmentThread(ApartmentState.STA, () =>
        {
            NativeApartment baseline = ReadNativeApartment();
            AssertOleProbe(Succeeded);
            Assert.Equal(ApartmentState.STA, s_requireOle());
            AssertOleProbe(AlreadyInitialized);

            OnApartmentThread(ApartmentState.MTA, () =>
            {
                NativeApartment workerBaseline = ReadNativeApartment();
                AssertOleProbe(RpcEChangedMode);
                s_releaseOle();
                Assert.Equal(ApartmentState.MTA, s_requireOle());
                s_releaseOle();
                Assert.Equal(workerBaseline, ReadNativeApartment());
                AssertOleProbe(RpcEChangedMode);
            });

            Assert.Equal(baseline, ReadNativeApartment());
            Assert.Equal(ApartmentState.STA, s_requireOle());
            AssertOleProbe(AlreadyInitialized);
            s_releaseOle();
            AssertOleProbe(Succeeded);
            Assert.Equal(baseline, ReadNativeApartment());
        });

    private readonly record struct NativeApartment(int Type, int Qualifier);

    private static NativeApartment ReadNativeApartment()
    {
        Assert.Equal(Succeeded, CoGetApartmentType(out int apartmentType, out int qualifier));
        return new NativeApartment(apartmentType, qualifier);
    }

    private static void AssertOleProbe(int expectedResult)
    {
        int result = OleInitialize(0);
        try { Assert.Equal(expectedResult, result); }
        finally
        {
            // Each successful probe owns exactly one native reference. A failed
            // MTA probe owns none; never uninitialize the CLR's apartment.
            if (result >= 0) { OleUninitialize(); }
        }
    }

    [DllImport("ole32.dll")]
    private static extern int CoGetApartmentType(out int apartmentType, out int qualifier);

    [DllImport("ole32.dll")]
    private static extern int CoInitializeEx(nint reserved, uint concurrencyModel);

    [DllImport("ole32.dll")]
    private static extern void CoUninitialize();

    [DllImport("ole32.dll")]
    private static extern int OleInitialize(nint reserved);

    [DllImport("ole32.dll")]
    private static extern void OleUninitialize();

    private static void OnApartmentThread(ApartmentState requestedApartment, Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            NativeApartment? baseline = null;
            try
            {
                baseline = ReadNativeApartment();
                if (requestedApartment == ApartmentState.STA)
                {
                    Assert.True(baseline.Value.Type is AptTypeSta or AptTypeMainSta,
                        $"Expected native STA, received {baseline.Value.Type}.");
                }
                else
                {
                    Assert.Equal(AptTypeMta, baseline.Value.Type);
                }

                action();
            }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try
                {
                    s_releaseOle();
                    if (baseline is { } expected) { Assert.Equal(expected, ReadNativeApartment()); }
                }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(requestedApartment);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The OLE state check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
