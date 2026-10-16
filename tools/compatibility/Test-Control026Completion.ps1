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
$probeRoot = Join-Path $repoRoot 'artifacts/compatibility-batch026/control-completion'
$runId = Get-Date -Format 'yyyyMMdd-HHmmss-fffffff'
$runRoot = Join-Path $probeRoot $runId
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($runRoot) | Out-Null
$commonScenarios = @(
    'queued-dispose', 'queued-destroy', 'queued-external', 'queued-recreate',
    'queued-cancel-dispose', 'queued-dispose-cancel',
    'started-success', 'started-error', 'started-oce', 'started-cancel',
    'sync-success', 'sync-error',
    'dispose-normal', 'dispose-native-before', 'dispose-native-after', 'dispose-native-event',
    'dispose-parent', 'dispose-child-before', 'dispose-child-after', 'dispose-site', 'dispose-disposed',
    'dispose-native-after-parent', 'dispose-native-after-child-before',
    'dispose-native-after-site', 'dispose-native-after-disposed'
)
$legacyScenarios = @(
    'legacy-queued-dispose', 'legacy-queued-recreate', 'legacy-queued-dispose-cancel',
    'legacy-started-success', 'legacy-started-error', 'legacy-started-oce', 'legacy-started-cancel'
)
if ($Scenario) {
    foreach ($name in $Scenario) {
        if ($name -notin $commonScenarios -and $name -notin $legacyScenarios) { throw "Unknown scenario: $name" }
    }
    $commonScenarios = @($commonScenarios | Where-Object { $_ -in $Scenario })
    $legacyScenarios = @($legacyScenarios | Where-Object { $_ -in $Scenario })
}
$variants = if ($ReferenceOnly) { @('reference') } elseif ($JitOnly) { @('lumina') } else { @('reference', 'lumina') }
$referenceRoot = if ($ReferenceRun) { Join-Path ([IO.Path]::GetFullPath($ReferenceRun)) 'reference' } else { Join-Path $runRoot 'reference' }
$runInfo = [ordered]@{
    runId = $runId
    runRoot = $runRoot
    referenceRoot = $referenceRoot
    fixedReference = 'Microsoft.WindowsDesktop.App 10.0.11; RollForward=Disable'
    createdAt = [DateTimeOffset]::Now.ToString('O')
    commonScenarios = $commonScenarios
    luminaOnlyScenarios = $legacyScenarios
    variant = $variants
    publishAot = [bool]$PublishAot
    sourceSha256 = (Get-FileHash -LiteralPath (Join-Path $PSScriptRoot 'Control026CompletionProbe/Program.cs') -Algorithm SHA256).Hash
    gitHead = (& git -C $repoRoot rev-parse HEAD)
}
[IO.File]::WriteAllText((Join-Path $runRoot 'run.json'), ($runInfo | ConvertTo-Json -Depth 8), $utf8)
Write-Output "Control 026 run: $runRoot"

function Invoke-ProbeProcess {
    param([string]$FilePath, [string[]]$Arguments, [string]$OutputRoot, [string]$Name)
    $scenarioRoot = Join-Path $OutputRoot $Name
    [IO.Directory]::CreateDirectory($scenarioRoot) | Out-Null
    $start = [Diagnostics.ProcessStartInfo]::new()
    $start.FileName = $FilePath
    foreach ($argument in $Arguments) { $start.ArgumentList.Add($argument) }
    $start.UseShellExecute = $false
    $start.CreateNoWindow = $true
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
    $process = [Diagnostics.Process]::Start($start)
    try {
        $stdoutTask = $process.StandardOutput.ReadToEndAsync()
        $stderrTask = $process.StandardError.ReadToEndAsync()
        $exited = $process.WaitForExit(30000)
        if (-not $exited) { $process.Kill($true); $process.WaitForExit() }
        $stdout = $stdoutTask.GetAwaiter().GetResult()
        $stderr = $stderrTask.GetAwaiter().GetResult()
        [IO.File]::WriteAllText((Join-Path $scenarioRoot 'stdout.log'), $stdout, $utf8)
        [IO.File]::WriteAllText((Join-Path $scenarioRoot 'stderr.log'), $stderr, $utf8)
        $info = [ordered]@{
            scenario = $Name
            executable = $FilePath
            arguments = $Arguments
            timedOut = -not $exited
            exitCode = if ($exited) { $process.ExitCode } else { $null }
            pendingObservation = $stdout.Contains('pending-after-750ms:')
            completeMarker = $stdout.Contains('complete=True')
            stdout = $stdout
            stderr = $stderr
        }
        [IO.File]::WriteAllText((Join-Path $scenarioRoot 'process.json'), ($info | ConvertTo-Json -Depth 8), $utf8)
        $info
    }
    finally { $process.Dispose() }
}

function Build-Variant {
    param([string]$Current)
    $variantRoot = Join-Path $runRoot $Current
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $framework = if ($Current -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
    $sourcePath = [Security.SecurityElement]::Escape((Join-Path $PSScriptRoot 'Control026CompletionProbe/Program.cs'))
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
  <ItemGroup><Compile Include="$sourcePath" Link="Program.cs" /></ItemGroup>
  $reference
</Project>
"@
    $projectPath = Join-Path $variantRoot 'Control026CompletionProbe.csproj'
    [IO.File]::WriteAllText($projectPath, $project, $utf8)
    $buildOutput = & dotnet build $projectPath -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Control 026 $Current build failed. See $variantRoot/build.log" }
    @{ root = $variantRoot; project = $projectPath; dll = Join-Path $variantRoot "bin/Release/$framework/Control026CompletionProbe.dll" }
}

function Compare-Runs {
    param([string]$BaselineRoot, [string]$ActualRoot, [string[]]$Names, [string]$OutputName)
    $comparisons = @()
    foreach ($name in $Names) {
        $baselinePath = Join-Path $BaselineRoot "$name/process.json"
        $actualPath = Join-Path $ActualRoot "$name/process.json"
        $baseline = if (Test-Path -LiteralPath $baselinePath) { Get-Content -LiteralPath $baselinePath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
        $actual = if (Test-Path -LiteralPath $actualPath) { Get-Content -LiteralPath $actualPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
        $comparisons += [ordered]@{
            scenario = $name
            strictMatch = $null -ne $baseline -and $null -ne $actual -and -not $baseline.timedOut -and -not $actual.timedOut -and $baseline.exitCode -eq 0 -and $actual.exitCode -eq 0 -and $baseline.completeMarker -and $actual.completeMarker -and $baseline.stdout -ceq $actual.stdout
            baseline = $baseline
            actual = $actual
        }
    }
    $summary = [ordered]@{
        baselineRoot = $BaselineRoot
        actualRoot = $ActualRoot
        strictMatches = @($comparisons | Where-Object { -not $_.strictMatch }).Count -eq 0
        scenarioCount = $Names.Count
        scenarios = $comparisons
    }
    [IO.File]::WriteAllText((Join-Path $runRoot $OutputName), ($summary | ConvertTo-Json -Depth 12), $utf8)
    $summary
}

$allRuns = @()
$luminaBuild = $null
foreach ($variant in $variants) {
    $build = Build-Variant $variant
    if ($variant -eq 'lumina') { $luminaBuild = $build }
    $names = if ($variant -eq 'reference') { $commonScenarios } else { @($commonScenarios) + @($legacyScenarios) }
    $runs = @()
    foreach ($name in $names) {
        $runs += Invoke-ProbeProcess -FilePath 'dotnet.exe' -Arguments @($build.dll, $name) -OutputRoot $build.root -Name $name
    }
    $allRuns += $runs
    [IO.File]::WriteAllText((Join-Path $build.root 'summary.json'), ($runs | ConvertTo-Json -Depth 10), $utf8)
}

$strictMatches = $null
if (-not $ReferenceOnly -and ((-not $JitOnly) -or $ReferenceRun)) {
    $comparison = Compare-Runs -BaselineRoot $referenceRoot -ActualRoot $luminaBuild.root -Names $commonScenarios -OutputName 'reference-comparison.json'
    $strictMatches = $comparison.strictMatches
}
$aotMatches = $null
if ($PublishAot) {
    $publishRoot = Join-Path $luminaBuild.root 'publish'
    $publishOutput = & dotnet publish $luminaBuild.project -c Release -r win-x64 -p:PublishAot=true -o $publishRoot --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $luminaBuild.root 'publish.log'), [string[]]$publishOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Control 026 AOT publish failed. See $($luminaBuild.root)/publish.log" }
    $runs = @()
    foreach ($name in (@($commonScenarios) + @($legacyScenarios))) {
        $runs += Invoke-ProbeProcess -FilePath (Join-Path $publishRoot 'Control026CompletionProbe.exe') -Arguments @($name) -OutputRoot $publishRoot -Name $name
    }
    $allRuns += $runs
    [IO.File]::WriteAllText((Join-Path $publishRoot 'summary.json'), ($runs | ConvertTo-Json -Depth 10), $utf8)
    $comparison = Compare-Runs -BaselineRoot $luminaBuild.root -ActualRoot $publishRoot -Names (@($commonScenarios) + @($legacyScenarios)) -OutputName 'aot-comparison.json'
    $aotMatches = $comparison.strictMatches
}
$failedProcesses = @($allRuns | Where-Object { $_.timedOut -or $_.exitCode -ne 0 -or -not $_.completeMarker }).Count
$result = [ordered]@{
    runRoot = $runRoot
    failedProcesses = $failedProcesses
    pendingTaskObservations = @($allRuns | Where-Object { $_.pendingObservation }).Count
    strictMatches = $strictMatches
    aotMatches = $aotMatches
    legacyComparison = 'Lumina-only; excluded from WinForms strict comparison.'
}
[IO.File]::WriteAllText((Join-Path $runRoot 'result.json'), ($result | ConvertTo-Json -Depth 8), $utf8)
$result | ConvertTo-Json -Depth 8
if ($failedProcesses -gt 0 -or $strictMatches -eq $false -or $aotMatches -eq $false) { exit 1 }
