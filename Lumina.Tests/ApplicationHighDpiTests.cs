using System.ComponentModel;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public class ApplicationHighDpiTests
{
    [Fact]
    public void Run_MatchesVoidEntryPointAndNamedParameter()
    {
        Action<Form> run = Application.Run;

        Assert.Equal(typeof(void), run.Method.ReturnType);
        Assert.Equal("mainForm", Assert.Single(run.Method.GetParameters()).Name);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void SetHighDpiMode_RejectsUndefinedModeBeforeChangingProcessState(int value)
    {
        Func<HighDpiMode, bool> setMode = Application.SetHighDpiMode;

        InvalidEnumArgumentException exception = Assert.Throws<InvalidEnumArgumentException>(
            () => setMode((HighDpiMode)value));

        Assert.Equal("highDpiMode", exception.ParamName);
    }

    [Fact]
    public void HighDpiMode_UsesWinFormsNumericValues()
    {
        Assert.Equal(0, (int)HighDpiMode.DpiUnaware);
        Assert.Equal(1, (int)HighDpiMode.SystemAware);
        Assert.Equal(2, (int)HighDpiMode.PerMonitor);
        Assert.Equal(3, (int)HighDpiMode.PerMonitorV2);
        Assert.Equal(4, (int)HighDpiMode.DpiUnawareGdiScaled);
    }
}
