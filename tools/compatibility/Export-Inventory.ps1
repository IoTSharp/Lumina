param(
    [string]$ReferenceVersion = '10.0.11',
    [switch]$SkipBuild
)

$ErrorActionPreference = 'Stop'
$utf8 = [System.Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
$OutputEncoding = $utf8
$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$auditRoot = Join-Path $repoRoot 'artifacts/compatibility-inventory'
[System.IO.Directory]::CreateDirectory($auditRoot) | Out-Null

if (-not $SkipBuild) {
    dotnet build (Join-Path $repoRoot 'Lumina.Forms/Lumina.Forms.csproj') -c Release -f net10.0 --nologo -v quiet
    if ($LASTEXITCODE -ne 0) { throw 'Lumina.Forms build failed.' }
}

# This temporary audit executable is never shipped or added to the solution.
# MetadataLoadContext comes from the selected SDK, so no audit NuGet package is needed.
$auditProject = @'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType><TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable>
    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
  </PropertyGroup>
  <ItemGroup>
    <Compile Include="../../tools/compatibility/Inventory.cs" />
    <Reference Include="System.Reflection.MetadataLoadContext">
      <HintPath>$(MSBuildToolsPath)/System.Reflection.MetadataLoadContext.dll</HintPath>
    </Reference>
  </ItemGroup>
</Project>
'@
$projectPath = Join-Path $auditRoot 'Inventory.csproj'
[System.IO.File]::WriteAllText($projectPath, $auditProject, $utf8)
dotnet run --project $projectPath -c Release -- $repoRoot $ReferenceVersion
if ($LASTEXITCODE -ne 0) { throw 'Compatibility inventory export failed.' }
