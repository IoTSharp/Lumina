param()

$ErrorActionPreference = 'Stop'
$taskRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $taskRepoRoot 'artifacts/compatibility-batch001/dpi'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null

# Each invocation gets its own process because Windows process DPI awareness is irreversible.
$source = @'
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetProcessDpiAwarenessContext(nint value);

    [STAThread]
    private static void Main(string[] args)
    {
        string scenario = args[0];
        int requested = int.Parse(args[1]);
        bool? preset = scenario == "preset" ? SetProcessDpiAwarenessContext(-2) : null;
        var before = Application.HighDpiMode;
        bool? first = null;
        bool? second = null;
        string? error = null;
        string? parameter = null;
        try
        {
            first = Application.SetHighDpiMode((HighDpiMode)requested);
            second = Application.SetHighDpiMode((HighDpiMode)requested);
        }
        catch (InvalidEnumArgumentException ex)
        {
            error = ex.GetType().Name;
            parameter = ex.ParamName;
        }
        Console.WriteLine(JsonSerializer.Serialize(new
        {
            scenario, requested, preset, before = (int)before, first, second,
            after = (int)Application.HighDpiMode, error, parameter
        }));
    }
}
'@

foreach ($variant in @('reference', 'lumina')) {
    $variantRoot = Join-Path $probeRoot $variant
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $taskRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $framework = if ($variant -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
    $settings = if ($variant -eq 'reference') { '<UseWindowsForms>true</UseWindowsForms><DefineConstants>REFERENCE</DefineConstants>' } else { '' }
    $reference = if ($variant -eq 'reference') { '' } else { '<ItemGroup><ProjectReference Include="' + $formsProject + '" /></ItemGroup>' }
    $project = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType><TargetFramework>$framework</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable>
    $settings
  </PropertyGroup>
  $reference
</Project>
"@
    [IO.File]::WriteAllText((Join-Path $variantRoot 'DpiProbe.csproj'), $project, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'Program.cs'), $source, $utf8)
    $buildOutput = & dotnet build (Join-Path $variantRoot 'DpiProbe.csproj') -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "DPI $variant build failed. See $variantRoot/build.log" }
}

$results = [Collections.Generic.List[object]]::new()
foreach ($scenario in @('fresh', 'preset')) {
    foreach ($mode in @(0, 1, 2, 3, 4, -1, 5)) {
        $observations = @{}
        foreach ($variant in @('reference', 'lumina')) {
            $framework = if ($variant -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
            $probe = Join-Path $probeRoot "$variant/bin/Release/$framework/DpiProbe.dll"
            $probeOutput = & dotnet $probe $scenario $mode
            if ($LASTEXITCODE -ne 0) { throw "DPI $variant/$scenario/$mode process failed." }
            $observations[$variant] = [string]$probeOutput
        }
        $matches = $observations.reference -ceq $observations.lumina
        $results.Add([ordered]@{ scenario = $scenario; mode = $mode; matches = $matches; reference = ($observations.reference | ConvertFrom-Json); lumina = ($observations.lumina | ConvertFrom-Json) })
    }
}
$resultPath = Join-Path $probeRoot 'results.json'
[IO.File]::WriteAllText($resultPath, (ConvertTo-Json -InputObject @($results) -Depth 5) + "`n", $utf8)
$failures = @($results | Where-Object { -not $_.matches })
if ($failures.Count -gt 0) { throw "$($failures.Count) DPI observations differ. See $resultPath" }
Write-Output "DPI observations match fixed local WinForms reference in $($results.Count) isolated scenarios. Results: $resultPath"
