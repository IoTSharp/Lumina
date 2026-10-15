param(
    [ValidateSet('reference', 'lumina', 'both')]
    [string]$Variant = 'both'
)

$ErrorActionPreference = 'Stop'
$itemImageRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $itemImageRepoRoot 'artifacts/compatibility-batch017/listviewitem-image'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::InputEncoding = $utf8
[Console]::OutputEncoding = $utf8
$OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null
$source = [IO.File]::ReadAllText((Join-Path $PSScriptRoot 'ListViewItemImageProbe/Program.cs'), $utf8)

function Invoke-ItemImageProbe {
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
        if (-not $exited) { throw "ListViewItem image process exceeded 30 seconds. See $LogRoot/stderr.log" }
        if ($process.ExitCode -ne 0) { throw "ListViewItem image process exited $($process.ExitCode). See $LogRoot/stderr.log" }
        $outputText
    }
    finally { $process.Dispose() }
}

$observations = @{}
$variants = if ($Variant -eq 'both') { @('reference', 'lumina') } else { @($Variant) }
foreach ($currentVariant in $variants) {
    $variantRoot = Join-Path $probeRoot $currentVariant
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $itemImageRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $framework = if ($currentVariant -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
    $settings = if ($currentVariant -eq 'reference') { '<UseWindowsForms>true</UseWindowsForms><DefineConstants>REFERENCE</DefineConstants><RuntimeFrameworkVersion>10.0.11</RuntimeFrameworkVersion><RollForward>Disable</RollForward>' } else { '' }
    $reference = if ($currentVariant -eq 'reference') { '' } else { '<ItemGroup><ProjectReference Include="' + $formsProject + '" /></ItemGroup>' }
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
    [IO.File]::WriteAllText((Join-Path $variantRoot 'ListViewItemImageProbe.csproj'), $project, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'Program.cs'), $source, $utf8)
    $buildOutput = & dotnet build (Join-Path $variantRoot 'ListViewItemImageProbe.csproj') -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "ListViewItem image $currentVariant build failed. See $variantRoot/build.log" }
    $probe = Join-Path $variantRoot "bin/Release/$framework/ListViewItemImageProbe.dll"
    $probeOutput = Invoke-ItemImageProbe -FilePath 'dotnet.exe' -Arguments @($probe) -LogRoot $variantRoot
    $observations[$currentVariant] = ([string]$probeOutput) | ConvertFrom-Json
    if ($currentVariant -eq 'reference' -and $observations.reference.version.Split('+')[0] -ne '10.0.11') {
        throw "Expected WinForms 10.0.11; found $($observations.reference.version)."
    }
    if ($observations[$currentVariant].uiCulture -ne 'en-US') {
        throw "Expected en-US UI culture; found $($observations[$currentVariant].uiCulture)."
    }
    [IO.File]::WriteAllText((Join-Path $probeRoot "$currentVariant.json"), ($observations[$currentVariant] | ConvertTo-Json -Depth 48) + "`n", $utf8)
}

if ($Variant -eq 'reference') {
    Write-Output "Recorded WinForms $($observations.reference.version) image observations with en-US UI culture: $probeRoot/reference.json"
    return
}
if ($Variant -eq 'lumina') {
    $referencePath = Join-Path $probeRoot 'reference.json'
    if (-not (Test-Path -LiteralPath $referencePath)) { throw 'Run -Variant reference first to record the fixed baseline.' }
    $observations.reference = [IO.File]::ReadAllText($referencePath, $utf8) | ConvertFrom-Json
    if ($observations.reference.version.Split('+')[0] -ne '10.0.11' -or $observations.reference.uiCulture -ne 'en-US') {
        throw 'The recorded baseline must be WinForms 10.0.11 with en-US UI culture.'
    }
}

$referenceJson = ConvertTo-Json -InputObject $observations.reference.observations -Depth 48 -Compress
$luminaJson = ConvertTo-Json -InputObject $observations.lumina.observations -Depth 48 -Compress
$report = [ordered]@{
    referenceVersion = $observations.reference.version
    referenceUICulture = $observations.reference.uiCulture
    luminaUICulture = $observations.lumina.uiCulture
    matches = $referenceJson -ceq $luminaJson
    reference = $observations.reference.observations
    lumina = $observations.lumina.observations
    deferredDesignerMetadata = [ordered]@{
        status = 'Standard NoneExcludedImageIndexConverter, ImageKeyConverter and ImageIndexEditor metadata remain outside this unattached-state scope; omission is not metadata parity. The fixed reference declares no RelatedImageListAttribute on these properties.'
        reference = $observations.reference.deferredMetadata
        lumina = $observations.lumina.deferredMetadata
    }
}
$reportPath = Join-Path $probeRoot 'results.json'
[IO.File]::WriteAllText($reportPath, ($report | ConvertTo-Json -Depth 48) + "`n", $utf8)
if (-not $report.matches) { throw "ListViewItem image observations differ. See $reportPath" }
Write-Output "ListViewItem image observations match WinForms 10.0.11 with en-US UI culture in $($report.reference.Count) observations; deferred designer metadata is separately recorded. Results: $reportPath"
