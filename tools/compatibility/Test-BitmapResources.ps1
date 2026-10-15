param(
    [string]$Configuration = 'Release',
    [string]$RuntimeIdentifier = 'win-x64',
    [string]$ArtifactsDirectory = '',
    [switch]$SkipPublish
)

$ErrorActionPreference = 'Stop'
$taskRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeProject = Join-Path $PSScriptRoot 'ResourceDecodingProbe/ResourceDecodingProbe.csproj'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
if ([string]::IsNullOrWhiteSpace($ArtifactsDirectory)) {
    $ArtifactsDirectory = Join-Path $taskRepoRoot 'artifacts/compatibility-batch002/resources'
}
$ArtifactsDirectory = [IO.Path]::GetFullPath($ArtifactsDirectory)
[IO.Directory]::CreateDirectory($ArtifactsDirectory) | Out-Null

function Invoke-ProbeDotnet {
    param([string[]]$Arguments, [string]$LogName)
    $commandOutput = & dotnet @Arguments 2>&1
    $commandExitCode = $LASTEXITCODE
    [IO.File]::WriteAllLines((Join-Path $ArtifactsDirectory $LogName), [string[]]$commandOutput, $utf8)
    if ($commandExitCode -ne 0) {
        throw "Resource probe command failed. See $ArtifactsDirectory/$LogName"
    }
    return [string[]]$commandOutput
}

$observations = @{}
foreach ($framework in @('net10.0-windows', 'net10.0')) {
    $buildArguments = @('build', $probeProject, '-c', $Configuration, '-f', $framework, '--nologo', '-v', 'minimal')
    Invoke-ProbeDotnet -Arguments $buildArguments -LogName "$framework-build.log" | Out-Null
    $probeAssembly = Join-Path $PSScriptRoot "ResourceDecodingProbe/bin/$Configuration/$framework/ResourceDecodingProbe.dll"
    $observations[$framework] = @(Invoke-ProbeDotnet -Arguments @($probeAssembly) -LogName "$framework-results.log")
}
if (($observations['net10.0-windows'] -join "`n") -cne ($observations['net10.0'] -join "`n")) {
    throw "Managed Bitmap observations differ from real WinForms. See $ArtifactsDirectory"
}

if (-not $SkipPublish) {
    $publishDirectory = Join-Path $ArtifactsDirectory 'aot'
    $publishArguments = @('publish', $probeProject, '-c', $Configuration, '-f', 'net10.0', '-r', $RuntimeIdentifier,
        '-p:PublishAot=true', '-p:TrimmerSingleWarn=false', '--nologo', '-v', 'minimal', '-o', $publishDirectory)
    $publishOutput = @(Invoke-ProbeDotnet -Arguments $publishArguments -LogName 'aot-publish.log')
    $analysisWarnings = @($publishOutput | Where-Object { $_ -match 'warning IL[23]\d{3}' })
    $probeExecutable = Join-Path $publishDirectory 'ResourceDecodingProbe.exe'
    $nativeOutput = & $probeExecutable 2>&1
    $nativeExitCode = $LASTEXITCODE
    [IO.File]::WriteAllLines((Join-Path $ArtifactsDirectory 'aot-results.log'), [string[]]$nativeOutput, $utf8)
    if ($nativeExitCode -ne 0) {
        throw "Native AOT resource decoding failed. See $ArtifactsDirectory/aot-results.log"
    }
    if (($observations['net10.0-windows'] -join "`n") -cne ($nativeOutput -join "`n")) {
        throw "Native AOT Bitmap observations differ from real WinForms. See $ArtifactsDirectory"
    }
    if ($analysisWarnings.Count -ne 0) {
        throw "Native AOT decoding matched WinForms, but publication produced $($analysisWarnings.Count) detailed trimming/AOT warnings. See $ArtifactsDirectory/aot-publish.log and aot-results.log"
    }
}
Write-Output "Five actual Demo Bitmaps match real WinForms, including pixel hashes and resource semantics. Logs: $ArtifactsDirectory"
