param(
    [ValidateSet('reference', 'lumina', 'both')][string]$Variant = 'both',
    [switch]$PublishAot
)

$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $repoRoot 'artifacts/compatibility-batch025/control-boundary'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null
$scenarios = @(
    'drain-set-failure',
    'drain-callback-error',
    'unobserved-threadexception',
    'same-thread-invoke',
    'message-entry',
    'context-entry',
    'external-ncdestroy',
    'form-ncdestroy'
)
$variants = if ($Variant -eq 'both') { @('reference', 'lumina') } else { @($Variant) }

function Invoke-ProbeProcess {
    param(
        [string]$FilePath,
        [string[]]$Arguments,
        [string]$OutputRoot,
        [string]$Scenario
    )

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
        if (-not $exited) {
            $process.Kill($true)
            $process.WaitForExit()
        }
        $stdout = $stdoutTask.GetAwaiter().GetResult()
        $stderr = $stderrTask.GetAwaiter().GetResult()
        $scenarioRoot = Join-Path $OutputRoot $Scenario
        [IO.Directory]::CreateDirectory($scenarioRoot) | Out-Null
        [IO.File]::WriteAllText((Join-Path $scenarioRoot 'stdout.log'), $stdout, $utf8)
        [IO.File]::WriteAllText((Join-Path $scenarioRoot 'stderr.log'), $stderr, $utf8)
        $processInfo = [ordered]@{
            scenario = $Scenario
            executable = $FilePath
            arguments = $Arguments
            timedOut = -not $exited
            exitCode = if ($exited) { $process.ExitCode } else { $null }
            stdout = $stdout
            stderr = $stderr
        }
        [IO.File]::WriteAllText((Join-Path $scenarioRoot 'process.json'), ($processInfo | ConvertTo-Json -Depth 8), $utf8)
        [ordered]@{ scenario = $Scenario; timedOut = -not $exited; exitCode = if ($exited) { $process.ExitCode } else { $null }; stdout = $stdout; stderr = $stderr }
    }
    finally { $process.Dispose() }
}

function Invoke-Variant {
    param([string]$Current)

    $variantRoot = Join-Path $probeRoot $Current
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $framework = if ($Current -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
    $sourcePath = [Security.SecurityElement]::Escape((Join-Path $PSScriptRoot 'Control025BoundaryProbe/Program.cs'))
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $repoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $settings = if ($Current -eq 'reference') {
        '<UseWindowsForms>true</UseWindowsForms><DefineConstants>REFERENCE</DefineConstants><RuntimeFrameworkVersion>10.0.11</RuntimeFrameworkVersion><RollForward>Disable</RollForward>'
    } else { '<DefineConstants>LUMINA</DefineConstants>' }
    $reference = if ($Current -eq 'reference') { '' } else { '<ItemGroup><ProjectReference Include="' + $formsProject + '" /></ItemGroup>' }
    $project = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>$framework</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    $settings
  </PropertyGroup>
  <ItemGroup><Compile Include="$sourcePath" Link="Program.cs" /></ItemGroup>
  $reference
</Project>
"@
    $projectPath = Join-Path $variantRoot 'Control025BoundaryProbe.csproj'
    [IO.File]::WriteAllText($projectPath, $project, $utf8)
    $buildOutput = & dotnet build $projectPath -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Control 025 $Current build failed. See $variantRoot/build.log" }

    $probe = Join-Path $variantRoot "bin/Release/$framework/Control025BoundaryProbe.dll"
    $runs = @()
    foreach ($scenario in $scenarios) {
        $runs += Invoke-ProbeProcess -FilePath 'dotnet.exe' -Arguments @($probe, $scenario) -OutputRoot $variantRoot -Scenario $scenario
    }
    $summary = [ordered]@{
        variant = $Current
        framework = $framework
        runtimeFrameworkVersion = if ($Current -eq 'reference') { '10.0.11' } else { $null }
        scenarios = $runs
    }
    [IO.File]::WriteAllText((Join-Path $variantRoot 'summary.json'), ($summary | ConvertTo-Json -Depth 12), $utf8)
    $runs
}

$allRuns = [ordered]@{}
foreach ($current in $variants) {
    $allRuns[$current] = Invoke-Variant $current
}

if ($Variant -eq 'both') {
    $comparisons = @()
    foreach ($scenario in $scenarios) {
        $referenceText = [IO.File]::ReadAllText((Join-Path $probeRoot "reference/$scenario/stdout.log"), $utf8)
        $luminaText = [IO.File]::ReadAllText((Join-Path $probeRoot "lumina/$scenario/stdout.log"), $utf8)
        $same = $referenceText -ceq $luminaText
        $comparison = [ordered]@{ scenario = $scenario; strictMatch = $same; reference = $referenceText; lumina = $luminaText }
        $comparisons += $comparison
        [IO.File]::WriteAllText((Join-Path $probeRoot "$scenario-comparison.json"), ($comparison | ConvertTo-Json -Depth 8), $utf8)
    }
    [IO.File]::WriteAllText((Join-Path $probeRoot 'comparison.json'), ($comparisons | ConvertTo-Json -Depth 8), $utf8)
    if (($comparisons | Where-Object { -not $_.strictMatch }).Count -gt 0) {
        Write-Output 'Control 025 reference/Lumina differences are preserved in comparison.json.'
    }
}

if ($PublishAot) {
    if ($Variant -eq 'reference') { throw '-PublishAot requires the lumina or both variant.' }
    $variantRoot = Join-Path $probeRoot 'lumina'
    $projectPath = Join-Path $variantRoot 'Control025BoundaryProbe.csproj'
    $publishRoot = Join-Path $variantRoot 'publish'
    $publishOutput = & dotnet publish $projectPath -c Release -r win-x64 -p:PublishAot=true -o $publishRoot --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'publish.log'), [string[]]$publishOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Control 025 Lumina AOT publish failed. See $variantRoot/publish.log" }
    foreach ($scenario in $scenarios) {
        Invoke-ProbeProcess -FilePath (Join-Path $publishRoot 'Control025BoundaryProbe.exe') -Arguments @($scenario) -OutputRoot (Join-Path $variantRoot 'publish') -Scenario $scenario | Out-Null
    }
}
