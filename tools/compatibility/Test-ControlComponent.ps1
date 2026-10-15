param()

$ErrorActionPreference = 'Stop'
$componentRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $componentRepoRoot 'artifacts/compatibility-batch002/component'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null

$source = @'
using System.ComponentModel;
using System.Text.Json;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        var results = new List<object>();
        var control = new Control();
        int count = 0;
        control.Disposed += (_, _) => count++;
        control.Dispose();
        results.Add(new { scenario = "first", count, control.IsDisposed, control.Disposing });
        control.Dispose();
        results.Add(new { scenario = "repeat", count, control.IsDisposed, control.Disposing });
        using var container = new Container();
        var sited = new Control();
        container.Add(sited, "sited");
        sited.Disposed += (_, _) => results.Add(new { scenario = "event", siteNull = sited.Site is null, count = container.Components.Count, sited.IsDisposed, sited.Disposing });
        sited.Dispose();
        results.Add(new { scenario = "sited-after", siteNull = sited.Site is null, count = container.Components.Count, sited.IsDisposed, sited.Disposing });
        var finalizer = new ExposedControl();
        container.Add(finalizer, "finalizer");
        int finalizerEvents = 0;
        finalizer.Disposed += (_, _) => finalizerEvents++;
        finalizer.ReleaseUnmanaged();
        results.Add(new { scenario = "false", siteNull = finalizer.Site is null, count = container.Components.Count, finalizerEvents, finalizer.IsDisposed, finalizer.Disposing });
        Console.WriteLine(JsonSerializer.Serialize(results));
    }

    private sealed class ExposedControl : Control
    {
        public void ReleaseUnmanaged() => Dispose(false);
    }
}
'@

$observations = @{}
foreach ($variant in @('reference', 'lumina')) {
    $variantRoot = Join-Path $probeRoot $variant
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $componentRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
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
    [IO.File]::WriteAllText((Join-Path $variantRoot 'ComponentProbe.csproj'), $project, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'Program.cs'), $source, $utf8)
    $buildOutput = & dotnet build (Join-Path $variantRoot 'ComponentProbe.csproj') -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Component $variant build failed. See $variantRoot/build.log" }
    $probe = Join-Path $variantRoot "bin/Release/$framework/ComponentProbe.dll"
    $probeOutput = & dotnet $probe
    if ($LASTEXITCODE -ne 0) { throw "Component $variant process failed." }
    $observations[$variant] = [string]$probeOutput
}

$report = [ordered]@{
    matches = $observations.reference -ceq $observations.lumina
    reference = $observations.reference | ConvertFrom-Json
    lumina = $observations.lumina | ConvertFrom-Json
}
$reportJson = $report | ConvertTo-Json -Depth 6
[IO.File]::WriteAllText((Join-Path $probeRoot 'results.json'), $reportJson, $utf8)
$reportJson
if (-not $report.matches) { throw 'Component lifetime observations differ from the installed WinForms baseline.' }
