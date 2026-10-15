param(
    [switch]$ReferenceOnly
)

$ErrorActionPreference = 'Stop'
$enumerationRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $enumerationRepoRoot 'artifacts/compatibility-batch012/listviewsubitem-enumeration'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null
$source = [IO.File]::ReadAllText((Join-Path $PSScriptRoot 'ListViewSubItemEnumerationProbe/Program.cs'), $utf8)

$observations = @{}
$variants = if ($ReferenceOnly) { @('reference') } else { @('reference', 'lumina') }
foreach ($variant in $variants) {
    $variantRoot = Join-Path $probeRoot $variant
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $enumerationRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
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
    [IO.File]::WriteAllText((Join-Path $variantRoot 'ListViewSubItemEnumerationProbe.csproj'), $project, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'Program.cs'), $source, $utf8)
    $buildOutput = & dotnet build (Join-Path $variantRoot 'ListViewSubItemEnumerationProbe.csproj') -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "ListViewSubItem enumeration $variant build failed. See $variantRoot/build.log" }
    $probe = Join-Path $variantRoot "bin/Release/$framework/ListViewSubItemEnumerationProbe.dll"
    $probeOutput = & dotnet $probe
    if ($LASTEXITCODE -ne 0) { throw "ListViewSubItem enumeration $variant process failed." }
    $observations[$variant] = ([string]$probeOutput) | ConvertFrom-Json
}

if ($ReferenceOnly) {
    $reportPath = Join-Path $probeRoot 'reference.json'
    [IO.File]::WriteAllText($reportPath, ($observations.reference | ConvertTo-Json -Depth 24) + "`n", $utf8)
    Write-Output "Recorded WinForms $($observations.reference.version) subitem enumeration observations: $reportPath"
    return
}

$referenceJson = ConvertTo-Json -InputObject $observations.reference.observations -Depth 24 -Compress
$luminaJson = ConvertTo-Json -InputObject $observations.lumina.observations -Depth 24 -Compress
$report = [ordered]@{
    referenceVersion = $observations.reference.version
    matches = $referenceJson -ceq $luminaJson
    reference = $observations.reference.observations
    lumina = $observations.lumina.observations
}
$reportPath = Join-Path $probeRoot 'results.json'
[IO.File]::WriteAllText($reportPath, ($report | ConvertTo-Json -Depth 24) + "`n", $utf8)
if (-not $report.matches) { throw "ListViewSubItem enumeration observations differ. See $reportPath" }
Write-Output "ListViewSubItem enumeration observations match WinForms 10.0.11 in $($report.reference.Count) observations. Results: $reportPath"
