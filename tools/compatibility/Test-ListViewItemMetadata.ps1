param(
    [switch]$ReferenceOnly
)

$ErrorActionPreference = 'Stop'
$metadataRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $metadataRepoRoot 'artifacts/compatibility-batch014/listviewitem-metadata'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::InputEncoding = $utf8
[Console]::OutputEncoding = $utf8
$OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null
$source = [IO.File]::ReadAllText((Join-Path $PSScriptRoot 'ListViewItemMetadataProbe/Program.cs'), $utf8)

function Invoke-ItemMetadataProbe {
    param([string]$FilePath, [string[]]$Arguments, [string]$LogRoot)
    $start = [Diagnostics.ProcessStartInfo]::new()
    $start.FileName = $FilePath
    foreach ($argument in $Arguments) { $start.ArgumentList.Add($argument) }
    $start.UseShellExecute = $false
    $start.CreateNoWindow = $true
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    $start.StandardOutputEncoding = $utf8
    $start.StandardErrorEncoding = $utf8
    $process = [Diagnostics.Process]::Start($start)
    try {
        $outputTask = $process.StandardOutput.ReadToEndAsync()
        $errorTask = $process.StandardError.ReadToEndAsync()
        $exited = $process.WaitForExit(30000)
        if (-not $exited) { $process.Kill($true); $process.WaitForExit() }
        $outputText = $outputTask.GetAwaiter().GetResult()
        $errorText = $errorTask.GetAwaiter().GetResult()
        [IO.File]::WriteAllText((Join-Path $LogRoot 'stdout.log'), $outputText, $utf8)
        [IO.File]::WriteAllText((Join-Path $LogRoot 'stderr.log'), $errorText, $utf8)
        if (-not $exited) { throw "ListViewItem metadata process exceeded 30 seconds. See $LogRoot/stderr.log" }
        if ($process.ExitCode -ne 0) { throw "ListViewItem metadata process exited $($process.ExitCode). See $LogRoot/stderr.log" }
        $outputText
    }
    finally { $process.Dispose() }
}

$observations = @{}
$variants = if ($ReferenceOnly) { @('reference') } else { @('reference', 'lumina') }
foreach ($variant in $variants) {
    $variantRoot = Join-Path $probeRoot $variant
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $metadataRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $framework = if ($variant -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
    $settings = if ($variant -eq 'reference') { '<UseWindowsForms>true</UseWindowsForms><DefineConstants>REFERENCE</DefineConstants><RuntimeFrameworkVersion>10.0.11</RuntimeFrameworkVersion><RollForward>Disable</RollForward>' } else { '' }
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
    [IO.File]::WriteAllText((Join-Path $variantRoot 'ListViewItemMetadataProbe.csproj'), $project, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'Program.cs'), $source, $utf8)
    $buildOutput = & dotnet build (Join-Path $variantRoot 'ListViewItemMetadataProbe.csproj') -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "ListViewItem metadata $variant build failed. See $variantRoot/build.log" }
    $probe = Join-Path $variantRoot "bin/Release/$framework/ListViewItemMetadataProbe.dll"
    $probeOutput = Invoke-ItemMetadataProbe -FilePath 'dotnet.exe' -Arguments @($probe) -LogRoot $variantRoot
    $observations[$variant] = ([string]$probeOutput) | ConvertFrom-Json
    if ($variant -eq 'reference' -and $observations.reference.version.Split('+')[0] -ne '10.0.11') {
        throw "Expected WinForms 10.0.11; found $($observations.reference.version)."
    }
    if ($observations[$variant].uiCulture -ne 'en-US') {
        throw "Expected en-US UI culture; found $($observations[$variant].uiCulture)."
    }
}

if ($ReferenceOnly) {
    $reportPath = Join-Path $probeRoot 'reference.json'
    [IO.File]::WriteAllText($reportPath, ($observations.reference | ConvertTo-Json -Depth 32) + "`n", $utf8)
    Write-Output "Recorded WinForms $($observations.reference.version) item metadata observations with en-US UI culture: $reportPath"
    return
}

$referenceJson = ConvertTo-Json -InputObject $observations.reference.observations -Depth 32 -Compress
$luminaJson = ConvertTo-Json -InputObject $observations.lumina.observations -Depth 32 -Compress
$report = [ordered]@{
    referenceVersion = $observations.reference.version
    referenceUICulture = $observations.reference.uiCulture
    luminaUICulture = $observations.lumina.uiCulture
    matches = $referenceJson -ceq $luminaJson
    reference = $observations.reference.observations
    lumina = $observations.lumina.observations
}
$reportPath = Join-Path $probeRoot 'results.json'
[IO.File]::WriteAllText($reportPath, ($report | ConvertTo-Json -Depth 32) + "`n", $utf8)
if (-not $report.matches) { throw "ListViewItem metadata observations differ. See $reportPath" }
Write-Output "ListViewItem metadata observations match WinForms 10.0.11 with en-US UI culture in $($report.reference.Count) observations. Results: $reportPath"
