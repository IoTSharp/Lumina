param(
    [ValidateSet('reference', 'lumina', 'both')][string]$Variant = 'both',
    [switch]$PublishAot,
    [string]$ReferenceResultsPath,
    [ValidateRange(5, 60)][int]$TimeoutSeconds = 30
)

$ErrorActionPreference = 'Stop'
$handleLifecycleRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $handleLifecycleRepoRoot 'artifacts/compatibility-batch019/control-handle-lifecycle'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
if ($PublishAot -and $Variant -eq 'reference') { throw '-PublishAot requires the lumina variant.' }
$runRoot = Join-Path $probeRoot ('runs/' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fffffff'))
[IO.Directory]::CreateDirectory($runRoot) | Out-Null
$sourcePath = Join-Path $PSScriptRoot 'ControlHandleLifecycleProbe/Program.cs'
$sourceHash = (Get-FileHash -LiteralPath $sourcePath -Algorithm SHA256).Hash
$scenarios = @('metadata', 'lazy', 'visible-tree', 'hidden-tree', 'destroy', 'recreate', 'external', 'mapping', 'reparent', 'posting', 'form-lazy', 'form-recreate', 'form-destroy', 'form-external', 'form-close-uncreated', 'exceptions-reentrancy')
$variants = if ($Variant -eq 'both') { @('reference', 'lumina') } else { @($Variant) }
$results = @{}

function Invoke-HandleLifecycleProbe {
    param([string]$FilePath, [string[]]$Arguments, [string]$LogRoot, [string]$Scenario)
    [IO.Directory]::CreateDirectory($LogRoot) | Out-Null
    $start = [Diagnostics.ProcessStartInfo]::new()
    $start.FileName = $FilePath
    foreach ($argument in $Arguments) { $start.ArgumentList.Add($argument) }
    $start.ArgumentList.Add($Scenario)
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
        $exited = $process.WaitForExit($TimeoutSeconds * 1000)
        if (-not $exited) { $process.Kill($true); $process.WaitForExit() }
        $outputText = $outputTask.GetAwaiter().GetResult()
        $errorText = $errorTask.GetAwaiter().GetResult()
        [IO.File]::WriteAllText((Join-Path $LogRoot 'stdout.log'), $outputText, $utf8)
        [IO.File]::WriteAllText((Join-Path $LogRoot 'stderr.log'), $errorText, $utf8)
        $lines = if ($outputText.Length -eq 0) { @() } else { @($outputText.TrimEnd() -split '\r?\n') }
        $complete = $lines.Count -gt 0 -and $lines[-1] -ceq 'complete=True'
        $processRecord = [ordered]@{ scenario = $Scenario; executable = $FilePath; arguments = @($Arguments) + $Scenario; timedOut = -not $exited; exitCode = $process.ExitCode; complete = $complete; stdout = $outputText; stderr = $errorText }
        [IO.File]::WriteAllText((Join-Path $LogRoot 'process.json'), ($processRecord | ConvertTo-Json -Depth 5), $utf8)
        if (-not $exited) { throw "Lifecycle $Scenario exceeded $TimeoutSeconds seconds; raw output retained in $LogRoot." }
        if ($process.ExitCode -ne 0) { throw "Lifecycle $Scenario exited $($process.ExitCode); raw output retained in $LogRoot." }
        if (-not $complete) { throw "Lifecycle $Scenario did not reach its completion marker; raw output retained in $LogRoot." }
        return $lines
    }
    finally { $process.Dispose() }
}

function Compare-HandleLifecycleOutput {
    param([string]$ExpectedPath, [string]$ActualPath, [string]$ComparisonPath)
    $expectedLines = [IO.File]::ReadAllLines($ExpectedPath, $utf8)
    $actualLines = [IO.File]::ReadAllLines($ActualPath, $utf8)
    $differences = [Collections.Generic.List[object]]::new()
    for ($line = 0; $line -lt [Math]::Max($expectedLines.Count, $actualLines.Count); $line++) {
        $expected = if ($line -lt $expectedLines.Count) { $expectedLines[$line] } else { $null }
        $actual = if ($line -lt $actualLines.Count) { $actualLines[$line] } else { $null }
        if ($expected -cne $actual) { $differences.Add([ordered]@{ line = $line + 1; expected = $expected; actual = $actual }) }
    }
    $comparison = [ordered]@{ strictMatches = $differences.Count -eq 0; expectedPath = $ExpectedPath; actualPath = $ActualPath; expectedLineCount = $expectedLines.Count; actualLineCount = $actualLines.Count; differingLineCount = $differences.Count; differences = $differences.ToArray() }
    [IO.File]::WriteAllText($ComparisonPath, ($comparison | ConvertTo-Json -Depth 6), $utf8)
    return $differences.Count -eq 0
}

foreach ($current in $variants) {
    $variantRoot = Join-Path $runRoot $current
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $handleLifecycleRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $probeSource = [Security.SecurityElement]::Escape($sourcePath)
    $framework = if ($current -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
    $settings = if ($current -eq 'reference') { '<UseWindowsForms>true</UseWindowsForms><DefineConstants>REFERENCE</DefineConstants><RuntimeFrameworkVersion>10.0.11</RuntimeFrameworkVersion><RollForward>Disable</RollForward>' } else { '' }
    $reference = if ($current -eq 'reference') { '' } else { '<ItemGroup><ProjectReference Include="' + $formsProject + '" /></ItemGroup>' }
    $project = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType><TargetFramework>$framework</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable>
    $settings
  </PropertyGroup>
  <ItemGroup><Compile Include="$probeSource" Link="Program.cs" /></ItemGroup>
  $reference
</Project>
"@
    $projectPath = Join-Path $variantRoot 'HandleLifecycleProbe.csproj'
    [IO.File]::WriteAllText($projectPath, $project, $utf8)
    $buildOutput = & dotnet build $projectPath -c Release --nologo -v quiet 2>&1
    $buildExitCode = $LASTEXITCODE
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'build-status.json'), (@{ exitCode = $buildExitCode; sourceHash = $sourceHash } | ConvertTo-Json), $utf8)
    if ($buildExitCode -ne 0) { throw "Lifecycle $current build failed. See $variantRoot/build.log" }
    $allLines = [Collections.Generic.List[string]]::new()
    foreach ($scenario in $scenarios) {
        $probeOutput = @(Invoke-HandleLifecycleProbe -FilePath 'dotnet.exe' -Arguments @((Join-Path $variantRoot "bin/Release/$framework/HandleLifecycleProbe.dll")) -LogRoot (Join-Path $variantRoot $scenario) -Scenario $scenario)
        $allLines.AddRange([string[]]$probeOutput)
        [IO.File]::WriteAllLines((Join-Path $variantRoot 'results.txt'), $allLines, $utf8)
    }
    $results[$current] = Join-Path $variantRoot 'results.txt'
    $manifest = [ordered]@{ variant = $current; sourceHash = $sourceHash; resultsPath = $results[$current]; scenarioCount = $scenarios.Count; lineCount = $allLines.Count; complete = $true }
    [IO.File]::WriteAllText((Join-Path $variantRoot 'manifest.json'), ($manifest | ConvertTo-Json), $utf8)
    [IO.File]::WriteAllText((Join-Path $probeRoot "latest-$current.json"), ($manifest | ConvertTo-Json), $utf8)
    Write-Output "$current complete: $($allLines.Count) lines; $($results[$current])"
}

$matches = $true
if ($Variant -ne 'reference') {
    if ($Variant -eq 'both') { $ReferenceResultsPath = $results['reference'] }
    elseif (-not $ReferenceResultsPath) {
        $baselineManifestPath = Join-Path $probeRoot 'latest-reference.json'
        if (-not (Test-Path -LiteralPath $baselineManifestPath)) { throw 'No saved reference baseline. Run -Variant reference first, or pass -ReferenceResultsPath.' }
        $baselineManifest = [IO.File]::ReadAllText($baselineManifestPath, $utf8) | ConvertFrom-Json
        if ($baselineManifest.sourceHash -cne $sourceHash) { throw 'Probe source changed after the saved reference run. Run -Variant reference again.' }
        $ReferenceResultsPath = $baselineManifest.resultsPath
    }
    $matches = Compare-HandleLifecycleOutput -ExpectedPath $ReferenceResultsPath -ActualPath $results['lumina'] -ComparisonPath (Join-Path $runRoot 'reference-lumina-comparison.json')
}

$aotMatches = $true
if ($PublishAot) {
    $variantRoot = Join-Path $runRoot 'lumina'
    $publishRoot = Join-Path $variantRoot 'publish'
    $publishOutput = & dotnet publish (Join-Path $variantRoot 'HandleLifecycleProbe.csproj') -c Release -r win-x64 -p:PublishAot=true -o $publishRoot --nologo -v quiet 2>&1
    $publishExitCode = $LASTEXITCODE
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'publish.log'), [string[]]$publishOutput, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'publish-status.json'), (@{ exitCode = $publishExitCode } | ConvertTo-Json), $utf8)
    if ($publishExitCode -ne 0) { throw "Lifecycle AOT publish failed. See $variantRoot/publish.log" }
    $executablePath = Join-Path $publishRoot 'HandleLifecycleProbe.exe'
    $image = [IO.File]::ReadAllBytes($executablePath)
    $peOffset = [BitConverter]::ToInt32($image, 0x3c)
    $optionalHeaderOffset = $peOffset + 24
    if ([BitConverter]::ToUInt16($image, $optionalHeaderOffset) -ne 0x20b) { throw 'Expected a win-x64 PE32+ AOT executable.' }
    $clrDirectoryOffset = $optionalHeaderOffset + 112 + 14 * 8
    $clrDirectoryRva = [BitConverter]::ToUInt32($image, $clrDirectoryOffset)
    $clrDirectorySize = [BitConverter]::ToUInt32($image, $clrDirectoryOffset + 4)
    $nativeImage = [ordered]@{ path = $executablePath; bytes = $image.Length; clrDirectoryRva = $clrDirectoryRva; clrDirectorySize = $clrDirectorySize }
    [IO.File]::WriteAllText((Join-Path $variantRoot 'aot-image.json'), ($nativeImage | ConvertTo-Json), $utf8)
    if ($clrDirectoryRva -ne 0 -or $clrDirectorySize -ne 0) { throw 'Published executable still has a CLR header.' }
    $aotLines = [Collections.Generic.List[string]]::new()
    foreach ($scenario in $scenarios) {
        $aotOutput = @(Invoke-HandleLifecycleProbe -FilePath $executablePath -Arguments @() -LogRoot (Join-Path $publishRoot $scenario) -Scenario $scenario)
        $aotLines.AddRange([string[]]$aotOutput)
        [IO.File]::WriteAllLines((Join-Path $variantRoot 'aot-results.txt'), $aotLines, $utf8)
    }
    $aotMatches = Compare-HandleLifecycleOutput -ExpectedPath $results['lumina'] -ActualPath (Join-Path $variantRoot 'aot-results.txt') -ComparisonPath (Join-Path $runRoot 'aot-comparison.json')
    Write-Output "AOT complete: $($aotLines.Count) lines; $variantRoot/aot-results.txt"
}
$summary = [ordered]@{ variant = $Variant; sourceHash = $sourceHash; referenceMatches = $matches; aotRequested = [bool]$PublishAot; aotMatches = if ($PublishAot) { $aotMatches } else { $null }; runRoot = $runRoot }
[IO.File]::WriteAllText((Join-Path $runRoot 'summary.json'), ($summary | ConvertTo-Json), $utf8)
if (-not $aotMatches) { throw "Lifecycle AOT observations differ from Lumina JIT. See $runRoot/aot-comparison.json." }
if (-not $matches) { throw "Lifecycle observations differ from WinForms 10.0.11. See $runRoot/reference-lumina-comparison.json." }
