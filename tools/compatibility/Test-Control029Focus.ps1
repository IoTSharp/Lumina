param(
    [switch]$ReferenceOnly,
    [switch]$JitOnly,
    [switch]$PublishAot,
    [string]$ReferenceRun,
    [string[]]$Scenario
)

$ErrorActionPreference = 'Stop'
if ($ReferenceOnly -and $JitOnly) { throw 'Choose either -ReferenceOnly or -JitOnly.' }
if ($ReferenceOnly -and $PublishAot) { throw '-PublishAot requires a Lumina run.' }
$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $repoRoot 'artifacts/compatibility-batch029/control-focus'
$runId = Get-Date -Format 'yyyyMMdd-HHmmss-fffffff'
$runRoot = Join-Path $probeRoot $runId
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::InputEncoding = $utf8
[Console]::OutputEncoding = $utf8
$OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($runRoot) | Out-Null
$scenarios = @(
    'enabled-focused', 'enabled-ancestor', 'visible-focused', 'visible-ancestor',
    'tabstop-selectable', 'remove-focused', 'remove-nested', 'destroy-focused', 'nested-candidates',
    'validation-prevent-cancel', 'validation-allow-cancel', 'validation-disable-cancel',
    'validation-causes-false', 'validation-reentrant', 'validation-exception', 'validation-children'
)
if ($Scenario) {
    foreach ($name in $Scenario) {
        if ($name -notin $scenarios) { throw "Unknown scenario: $name" }
    }
    $scenarios = @($scenarios | Where-Object { $_ -in $Scenario })
}
$variants = if ($ReferenceOnly) { @('reference') } elseif ($JitOnly) { @('lumina') } else { @('reference', 'lumina') }
$referenceRoot = if ($ReferenceRun) { Join-Path ([IO.Path]::GetFullPath($ReferenceRun)) 'reference' } else { Join-Path $runRoot 'reference' }
$sourcePath = Join-Path $runRoot 'Program.cs'
[IO.File]::Copy((Join-Path $PSScriptRoot 'Control029FocusProbe/Program.cs'), $sourcePath)
$sourceHash = (Get-FileHash -LiteralPath $sourcePath -Algorithm SHA256).Hash
$runInfo = [ordered]@{
    runId = $runId
    runRoot = $runRoot
    referenceRoot = $referenceRoot
    fixedReference = 'Microsoft.WindowsDesktop.App 10.0.11; RollForward=Disable; runtime assembly version checked'
    createdAt = [DateTimeOffset]::Now.ToString('O')
    scenarios = $scenarios
    variants = $variants
    publishAot = [bool]$PublishAot
    sourceSha256 = $sourceHash
    sourceSnapshot = $sourcePath
    physicalInput = 'Not exercised. Focus/Select/SetFocus and injected messages exercise programmatic state only; the desktop may be locked.'
    driverSha256 = (Get-FileHash -LiteralPath $PSCommandPath -Algorithm SHA256).Hash
    comparisonPolicy = 'Raw stdout exact ordinal equality; stderr retained verbatim with assembly identity, HWND evidence and full original exception stacks.'
    outputContract = 'run.json plus per-scenario process.json, stdout.log and stderr.log; no output normalization.'
}
[IO.File]::WriteAllText((Join-Path $runRoot 'run.json'), ($runInfo | ConvertTo-Json -Depth 8), $utf8)
Write-Output "Control 029 run: $runRoot"

function Invoke-LoggedProcess {
    param([string]$FilePath, [string[]]$Arguments, [string]$OutputRoot, [string]$Name, [int]$TimeoutSeconds = 60, [switch]$Build)
    $processRoot = Join-Path $OutputRoot $Name
    [IO.Directory]::CreateDirectory($processRoot) | Out-Null
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
        $stdoutTask = $process.StandardOutput.ReadToEndAsync()
        $stderrTask = $process.StandardError.ReadToEndAsync()
        $exited = $process.WaitForExit($TimeoutSeconds * 1000)
        if (-not $exited) { $process.Kill($true); $process.WaitForExit() }
        $stdout = $stdoutTask.GetAwaiter().GetResult()
        $stderr = $stderrTask.GetAwaiter().GetResult()
        [IO.File]::WriteAllText((Join-Path $processRoot 'stdout.log'), $stdout, $utf8)
        [IO.File]::WriteAllText((Join-Path $processRoot 'stderr.log'), $stderr, $utf8)
        $info = [ordered]@{
            scenario = $Name
            executable = $FilePath
            arguments = $Arguments
            timedOut = -not $exited
            exitCode = if ($exited) { $process.ExitCode } else { $null }
            completeMarker = $stdout.EndsWith("complete=True`r`n", [StringComparison]::Ordinal) -or $stdout.EndsWith("complete=True`n", [StringComparison]::Ordinal)
            normalExit = $exited -and $process.ExitCode -eq 0
            nativeWindowSurvivors = if ($stdout -match 'native-window-survivors=(\d+)') { [int]$Matches[1] } else { $null }
            hwndEvidenceLines = @($stderr -split "`r?`n" | Where-Object { $_ -like 'hwnd:*' })
            stdout = $stdout
            stderr = $stderr
        }
        [IO.File]::WriteAllText((Join-Path $processRoot 'process.json'), ($info | ConvertTo-Json -Depth 10), $utf8)
        if ($Build -and (-not $exited -or $process.ExitCode -ne 0)) { throw "Control 029 $Name failed. Raw evidence: $processRoot" }
        $info
    }
    finally { $process.Dispose() }
}

function Build-Variant {
    param([string]$Current)
    $variantRoot = Join-Path $runRoot $Current
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $framework = if ($Current -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
    $escapedSource = [Security.SecurityElement]::Escape($sourcePath)
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $repoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $settings = if ($Current -eq 'reference') {
        '<UseWindowsForms>true</UseWindowsForms><DefineConstants>REFERENCE</DefineConstants><RuntimeFrameworkVersion>10.0.11</RuntimeFrameworkVersion><RollForward>Disable</RollForward>'
    } else { '<DefineConstants>LUMINA</DefineConstants>' }
    $reference = if ($Current -eq 'reference') { '' } else { '<ItemGroup><ProjectReference Include="' + $formsProject + '" /></ItemGroup>' }
    $project = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType><TargetFramework>$framework</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable>
    $settings
  </PropertyGroup>
  <ItemGroup><Compile Include="$escapedSource" Link="Program.cs" /></ItemGroup>
  $reference
</Project>
"@
    $projectPath = Join-Path $variantRoot 'Control029FocusProbe.csproj'
    [IO.File]::WriteAllText($projectPath, $project, $utf8)
    $null = Invoke-LoggedProcess -FilePath 'dotnet.exe' -Arguments @('build', $projectPath, '-c', 'Release', '--nologo', '-v', 'quiet') -OutputRoot $variantRoot -Name 'build' -Build -TimeoutSeconds 900
    @{ root = $variantRoot; project = $projectPath; dll = Join-Path $variantRoot "bin/Release/$framework/Control029FocusProbe.dll" }
}

function Compare-Runs {
    param([string]$BaselineRoot, [string]$ActualRoot, [string]$OutputName)
    $comparisons = @()
    foreach ($name in $scenarios) {
        $baselinePath = Join-Path $BaselineRoot "$name/process.json"
        $actualPath = Join-Path $ActualRoot "$name/process.json"
        $baseline = if (Test-Path -LiteralPath $baselinePath) { Get-Content -LiteralPath $baselinePath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
        $actual = if (Test-Path -LiteralPath $actualPath) { Get-Content -LiteralPath $actualPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
        $comparisons += [ordered]@{
            scenario = $name
            strictMatch = $null -ne $baseline -and $null -ne $actual -and -not $baseline.timedOut -and -not $actual.timedOut -and $baseline.exitCode -eq 0 -and $actual.exitCode -eq 0 -and $baseline.completeMarker -and $actual.completeMarker -and [string]::Equals($baseline.stdout, $actual.stdout, [StringComparison]::Ordinal)
            baseline = $baseline
            actual = $actual
        }
    }
    $summary = [ordered]@{
        baselineRoot = $BaselineRoot
        actualRoot = $ActualRoot
        strictMatches = @($comparisons | Where-Object { -not $_.strictMatch }).Count -eq 0
        scenarioCount = $scenarios.Count
        matchingScenarios = @($comparisons | Where-Object { $_.strictMatch }).Count
        scenarios = $comparisons
    }
    [IO.File]::WriteAllText((Join-Path $runRoot $OutputName), ($summary | ConvertTo-Json -Depth 14), $utf8)
    $summary
}

if ($ReferenceRun) {
    $referenceManifest = Get-Content -LiteralPath (Join-Path $ReferenceRun 'run.json') -Raw -Encoding UTF8 | ConvertFrom-Json
    if ($referenceManifest.sourceSha256 -cne $sourceHash) { throw 'Reference source hash differs; regenerate the fixed reference without normalizing observations.' }
    if (($referenceManifest.scenarios -join ',') -cne ($scenarios -join ',')) { throw 'Reference scenario selection differs.' }
}

$allRuns = @()
$luminaBuild = $null
foreach ($variant in $variants) {
    $build = Build-Variant $variant
    if ($variant -eq 'lumina') { $luminaBuild = $build }
    $runs = @()
    foreach ($name in $scenarios) {
        $runs += Invoke-LoggedProcess -FilePath 'dotnet.exe' -Arguments @($build.dll, $name) -OutputRoot $build.root -Name $name
    }
    $allRuns += $runs
    [IO.File]::WriteAllText((Join-Path $build.root 'summary.json'), ($runs | ConvertTo-Json -Depth 12), $utf8)
    [IO.File]::WriteAllText((Join-Path $build.root 'results.txt'), (($runs | ForEach-Object { $_.stdout }) -join ''), $utf8)
}

$strictMatches = $null
if (-not $ReferenceOnly -and ((-not $JitOnly) -or $ReferenceRun)) {
    $comparison = Compare-Runs -BaselineRoot $referenceRoot -ActualRoot $luminaBuild.root -OutputName 'reference-comparison.json'
    $strictMatches = $comparison.strictMatches
}
$aotMatches = $null
if ($PublishAot) {
    $publishRoot = Join-Path $luminaBuild.root 'publish'
    $null = Invoke-LoggedProcess -FilePath 'dotnet.exe' -Arguments @('publish', $luminaBuild.project, '-c', 'Release', '-r', 'win-x64', '-p:PublishAot=true', '-o', $publishRoot, '--nologo', '-v', 'quiet') -OutputRoot $luminaBuild.root -Name 'aot-build' -Build -TimeoutSeconds 1200
    $executable = Join-Path $publishRoot 'Control029FocusProbe.exe'
    $image = [IO.File]::ReadAllBytes($executable)
    $peOffset = [BitConverter]::ToInt32($image, 0x3c)
    $optionalHeaderOffset = $peOffset + 24
    if ([BitConverter]::ToUInt16($image, $optionalHeaderOffset) -ne 0x20b) { throw 'Expected win-x64 PE32+ AOT executable.' }
    $clrDirectoryOffset = $optionalHeaderOffset + 112 + 14 * 8
    $nativeImage = [ordered]@{ path = $executable; bytes = $image.Length; clrDirectoryRva = [BitConverter]::ToUInt32($image, $clrDirectoryOffset); clrDirectorySize = [BitConverter]::ToUInt32($image, $clrDirectoryOffset + 4) }
    [IO.File]::WriteAllText((Join-Path $luminaBuild.root 'aot-image.json'), ($nativeImage | ConvertTo-Json), $utf8)
    if ($nativeImage.clrDirectoryRva -ne 0 -or $nativeImage.clrDirectorySize -ne 0) { throw 'Published executable still has a CLR header.' }
    $runs = @()
    foreach ($name in $scenarios) { $runs += Invoke-LoggedProcess -FilePath $executable -Arguments @($name) -OutputRoot $publishRoot -Name $name }
    $allRuns += $runs
    [IO.File]::WriteAllText((Join-Path $publishRoot 'summary.json'), ($runs | ConvertTo-Json -Depth 12), $utf8)
    [IO.File]::WriteAllText((Join-Path $publishRoot 'results.txt'), (($runs | ForEach-Object { $_.stdout }) -join ''), $utf8)
    $comparison = Compare-Runs -BaselineRoot $luminaBuild.root -ActualRoot $publishRoot -OutputName 'aot-comparison.json'
    $aotMatches = $comparison.strictMatches
}

$failedProcesses = @($allRuns | Where-Object { $_.timedOut -or $_.exitCode -ne 0 -or -not $_.completeMarker }).Count
$result = [ordered]@{
    runRoot = $runRoot
    sourceSha256 = $sourceHash
    scenarioCount = $scenarios.Count
    failedProcesses = $failedProcesses
    strictMatches = $strictMatches
    aotMatches = $aotMatches
    survivingWindowProcesses = @($allRuns | Where-Object { $_.nativeWindowSurvivors -gt 0 }).Count
    physicalDeviceVerified = $false
}
[IO.File]::WriteAllText((Join-Path $runRoot 'result.json'), ($result | ConvertTo-Json -Depth 10), $utf8)
$result | ConvertTo-Json -Depth 10
if ($failedProcesses -gt 0 -or $strictMatches -eq $false -or $aotMatches -eq $false) { exit 1 }
