param(
    [Parameter(Mandatory = $true)]
    [string]$PackageDirectory,
    [string]$PackageVersion = '0.1.0'
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$utf8 = [System.Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
$OutputEncoding = $utf8
$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$packageRoot = (Resolve-Path -LiteralPath $PackageDirectory).Path
$probeRoot = Join-Path $repoRoot ('artifacts/compatibility-analyzers/' + [Guid]::NewGuid().ToString('N'))
[System.IO.Directory]::CreateDirectory($probeRoot) | Out-Null
Add-Type -AssemblyName System.IO.Compression.FileSystem

function Assert-AnalyzerPackage([string]$PackageId) {
    $packagePath = Join-Path $packageRoot ($PackageId + '.' + $PackageVersion + '.nupkg')
    $archive = [System.IO.Compression.ZipFile]::OpenRead($packagePath)
    try {
        $analyzerEntries = @($archive.Entries | Where-Object {
            $_.FullName -eq 'analyzers/dotnet/cs/Lumina.Forms.Analyzers.dll'
        })
        if ($analyzerEntries.Count -ne 1) {
            throw "$PackageId must contain exactly one analyzers/dotnet/cs/Lumina.Forms.Analyzers.dll entry."
        }

        $runtimeToolFiles = @($archive.Entries | Where-Object {
            $_.FullName -match '^(lib|runtimes)/.*(Lumina\.Forms\.Analyzers|Microsoft\.CodeAnalysis[^/]*|System\.Composition[^/]*)\.dll$'
        })
        if ($runtimeToolFiles.Count -ne 0) {
            throw "$PackageId contains analyzer tooling as a runtime or compile library."
        }

        $nuspecEntry = @($archive.Entries | Where-Object { $_.FullName -like '*.nuspec' })
        if ($nuspecEntry.Count -ne 1) { throw "$PackageId must contain exactly one nuspec." }
        $reader = [System.IO.StreamReader]::new($nuspecEntry[0].Open())
        try { [xml]$nuspec = $reader.ReadToEnd() }
        finally { $reader.Dispose() }

        $toolDependencies = @($nuspec.SelectNodes("//*[local-name()='dependency']") | Where-Object {
            $_.GetAttribute('id') -match '^(Microsoft\.CodeAnalysis(?:\.|$)|System\.Composition(?:\.|$)|Lumina\.Forms\.Analyzers$)'
        })
        if ($toolDependencies.Count -ne 0) {
            throw "$PackageId exposes analyzer tooling through NuGet dependencies."
        }
    }
    finally { $archive.Dispose() }
}

function Invoke-ProbeDotNet([string[]]$Arguments, [string]$LogPath) {
    $output = @(& dotnet @Arguments 2>&1 | ForEach-Object { $_.ToString() })
    $exitCode = $LASTEXITCODE
    [System.IO.File]::WriteAllLines($LogPath, [string[]]$output, $utf8)
    if ($exitCode -ne 0) {
        $output | Write-Output
        throw "dotnet failed with exit code $exitCode. See $LogPath"
    }
}

function Assert-ConsumerAssets([string]$ConsumerRoot) {
    $assetsPath = Join-Path $ConsumerRoot 'obj/project.assets.json'
    $assets = Get-Content -Raw -Encoding utf8 -LiteralPath $assetsPath | ConvertFrom-Json
    $toolPackages = @($assets.libraries.PSObject.Properties | Where-Object {
        $_.Name -match '^(Microsoft\.CodeAnalysis(?:\.|/|$)|System\.Composition(?:\.|/|$))'
    })
    if ($toolPackages.Count -ne 0) { throw "Roslyn/MEF leaked into the consumer restore graph: $ConsumerRoot" }

    foreach ($target in $assets.targets.PSObject.Properties) {
        foreach ($package in $target.Value.PSObject.Properties) {
            foreach ($assetKind in @('compile', 'runtime', 'runtimeTargets')) {
                $group = $package.Value.PSObject.Properties[$assetKind]
                if ($null -eq $group) { continue }
                $toolAssets = @($group.Value.PSObject.Properties | Where-Object {
                    $_.Name -match '(^|/)(Lumina\.Forms\.Analyzers|Microsoft\.CodeAnalysis[^/]*|System\.Composition[^/]*)\.dll$'
                })
                if ($toolAssets.Count -ne 0) { throw "Analyzer tooling leaked into $assetKind assets: $ConsumerRoot" }
            }
        }
    }

    $outputPath = Join-Path $ConsumerRoot 'bin/Release/net10.0'
    $outputToolFiles = @(Get-ChildItem -LiteralPath $outputPath -File -Recurse | Where-Object {
        $_.Name -match '^(Lumina\.Forms\.Analyzers|Microsoft\.CodeAnalysis.*|System\.Composition.*)\.dll$'
    })
    if ($outputToolFiles.Count -ne 0) { throw "Analyzer tooling leaked into consumer output: $ConsumerRoot" }

    $depsPath = Join-Path $outputPath 'AnalyzerPackageProbe.deps.json'
    $depsText = Get-Content -Raw -Encoding utf8 -LiteralPath $depsPath
    if ($depsText -match 'Lumina\.Forms\.Analyzers|Microsoft\.CodeAnalysis|System\.Composition') {
        throw "Analyzer tooling leaked into the consumer deps.json: $ConsumerRoot"
    }
}

Assert-AnalyzerPackage 'Lumina.Forms'
Assert-AnalyzerPackage 'Lumina.Forms.Analyzers'

$nugetConfig = @'
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="LocalProbePackages" value="__PACKAGE_SOURCE__" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
  <packageSourceMapping>
    <packageSource key="LocalProbePackages"><package pattern="Lumina.*" /></packageSource>
    <packageSource key="nuget.org"><package pattern="*" /></packageSource>
  </packageSourceMapping>
</configuration>
'@
$configPath = Join-Path $probeRoot 'NuGet.Config'
$nugetConfig = $nugetConfig.Replace('__PACKAGE_SOURCE__', [System.Security.SecurityElement]::Escape($packageRoot))
[System.IO.File]::WriteAllText($configPath, $nugetConfig, $utf8)

$projectTemplate = @'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <AssemblyName>AnalyzerPackageProbe</AssemblyName>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <EnableNETAnalyzers>false</EnableNETAnalyzers>
    <RunAnalyzersDuringBuild>true</RunAnalyzersDuringBuild>
    <ErrorLog>$(MSBuildProjectDirectory)/diagnostics.sarif,version=2.1</ErrorLog>
    <NuGetAudit>false</NuGetAudit>
  </PropertyGroup>
  <ItemGroup>
__PACKAGE_REFERENCES__
  </ItemGroup>
</Project>
'@
$probeSource = @'
internal static class Program
{
    private static void Main() { }
}

// Intentionally omit partial so a real package consumer must report LNF003.
internal class AnalyzerPackageProbeForm : Lumina.Forms.Form { }
'@
$versionAttribute = [System.Security.SecurityElement]::Escape($PackageVersion)
$consumers = @(
    @{
        Name = 'FormsBundledAnalyzer'
        References = '    <PackageReference Include="Lumina.Forms" Version="' + $versionAttribute + '" />'
    },
    @{
        Name = 'StandaloneAnalyzer'
        # Excluding the Forms analyzer proves the independent analyzer package works on its own.
        References = '    <PackageReference Include="Lumina.Forms" Version="' + $versionAttribute + '" ExcludeAssets="analyzers" />' + "`n" +
                     '    <PackageReference Include="Lumina.Forms.Analyzers" Version="' + $versionAttribute + '" />'
    }
)
$results = @()
foreach ($consumer in $consumers) {
    $consumerRoot = Join-Path $probeRoot $consumer.Name
    [System.IO.Directory]::CreateDirectory($consumerRoot) | Out-Null
    $projectPath = Join-Path $consumerRoot 'AnalyzerPackageProbe.csproj'
    $project = $projectTemplate.Replace('__PACKAGE_REFERENCES__', $consumer.References)
    [System.IO.File]::WriteAllText($projectPath, $project, $utf8)
    [System.IO.File]::WriteAllText((Join-Path $consumerRoot 'Program.cs'), $probeSource, $utf8)

    # A fresh cache ensures this run consumes the just-packed local packages, even at the same version.
    Invoke-ProbeDotNet @('restore', $projectPath, '--configfile', $configPath, '--packages', (Join-Path $probeRoot 'packages'), '--nologo') (Join-Path $consumerRoot 'restore.log')
    Invoke-ProbeDotNet @('build', $projectPath, '-c', 'Release', '--no-restore', '--nologo', '-v', 'minimal') (Join-Path $consumerRoot 'build.log')

    $sarif = Get-Content -Raw -Encoding utf8 -LiteralPath (Join-Path $consumerRoot 'diagnostics.sarif') | ConvertFrom-Json
    $diagnostics = @($sarif.runs | ForEach-Object { $_.results })
    $loadFailures = @($diagnostics | Where-Object { $_.ruleId -in @('CS8032', 'CS8784', 'CS8785', 'AD0001') })
    if ($loadFailures.Count -ne 0) { throw "Analyzer loading/execution failed: $($consumer.Name)" }
    $expectedDiagnostics = @($diagnostics | Where-Object { $_.ruleId -eq 'LNF003' })
    if ($expectedDiagnostics.Count -eq 0) { throw "LNF003 was not reported by the package consumer: $($consumer.Name)" }
    Assert-ConsumerAssets $consumerRoot
    $results += [pscustomobject]@{
        Consumer = $consumer.Name
        Diagnostic = 'LNF003'
        Count = $expectedDiagnostics.Count
        RuntimeToolingDependencies = 0
    }
}

$results | ConvertTo-Json | Out-File -LiteralPath (Join-Path $probeRoot 'results.json') -Encoding utf8
$results | Format-Table -AutoSize
Write-Host "Analyzer package checks passed. Evidence: $probeRoot"
