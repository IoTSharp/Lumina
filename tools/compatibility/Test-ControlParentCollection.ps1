param(
    [ValidateSet('reference', 'lumina', 'both')][string]$Variant = 'both',
    [switch]$PublishAot,
    [string]$ArtifactsDirectory
)

$ErrorActionPreference = 'Stop'
$parentCollectionRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = if ($ArtifactsDirectory) { [IO.Path]::GetFullPath($ArtifactsDirectory) } else { Join-Path $parentCollectionRepoRoot 'artifacts/compatibility-batch018/control-parent-collection' }
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null
$variants = if ($Variant -eq 'both') { @('reference', 'lumina') } else { @($Variant) }
if ($PublishAot -and $Variant -eq 'reference') { throw '-PublishAot requires the lumina variant.' }

function Invoke-ParentCollectionProbe {
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
        $exited = $process.WaitForExit(60000)
        if (-not $exited) { $process.Kill($true); $process.WaitForExit() }
        $outputText = $outputTask.GetAwaiter().GetResult()
        $errorText = $errorTask.GetAwaiter().GetResult()
        [IO.File]::WriteAllText((Join-Path $LogRoot 'stdout.log'), $outputText, $utf8)
        [IO.File]::WriteAllText((Join-Path $LogRoot 'stderr.log'), $errorText, $utf8)
        if (-not $exited) { throw "Parent collection process exceeded 60 seconds. See $LogRoot/stderr.log" }
        if ($process.ExitCode -ne 0) { throw "Parent collection process exited $($process.ExitCode). See $LogRoot/stderr.log" }
        $lines = $outputText.TrimEnd() -split '\r?\n'
        if ($lines[-1] -cne 'complete=True') { throw 'Parent collection probe did not reach its completion marker.' }
        $lines
    }
    finally { $process.Dispose() }
}

function Compare-ParentCollectionOutput {
    param([string]$ExpectedPath, [string]$ActualPath, [string]$ComparisonPath)
    $expectedLines = [IO.File]::ReadAllLines($ExpectedPath, $utf8)
    $actualLines = [IO.File]::ReadAllLines($ActualPath, $utf8)
    $differences = [Collections.Generic.List[object]]::new()
    for ($line = 0; $line -lt [Math]::Max($expectedLines.Count, $actualLines.Count); $line++) {
        $expected = if ($line -lt $expectedLines.Count) { $expectedLines[$line] } else { $null }
        $actual = if ($line -lt $actualLines.Count) { $actualLines[$line] } else { $null }
        if ($expected -cne $actual) { $differences.Add([ordered]@{ line = $line + 1; expected = $expected; actual = $actual }) }
    }
    $comparison = [ordered]@{
        strictMatches = $differences.Count -eq 0
        expectedPath = $ExpectedPath
        actualPath = $ActualPath
        expectedLineCount = $expectedLines.Count
        actualLineCount = $actualLines.Count
        differingLineCount = $differences.Count
        differences = $differences.ToArray()
    }
    [IO.File]::WriteAllText($ComparisonPath, ($comparison | ConvertTo-Json -Depth 6), $utf8)
    return $differences.Count -eq 0
}

foreach ($current in $variants) {
    $variantRoot = Join-Path $probeRoot $current
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $parentCollectionRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $probeSource = [Security.SecurityElement]::Escape((Join-Path $PSScriptRoot 'ControlParentCollectionProbe/Program.cs'))
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
    $projectPath = Join-Path $variantRoot 'ParentCollectionProbe.csproj'
    [IO.File]::WriteAllText($projectPath, $project, $utf8)
    $buildOutput = & dotnet build $projectPath -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Parent collection $current build failed. See $variantRoot/build.log" }
    $probeOutput = Invoke-ParentCollectionProbe -FilePath 'dotnet.exe' -Arguments @((Join-Path $variantRoot "bin/Release/$framework/ParentCollectionProbe.dll")) -LogRoot $variantRoot
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'results.txt'), [string[]]$probeOutput, $utf8)
    $probeOutput
}

$matches = $true
if ($Variant -eq 'both') {
    $matches = Compare-ParentCollectionOutput -ExpectedPath (Join-Path $probeRoot 'reference/results.txt') -ActualPath (Join-Path $probeRoot 'lumina/results.txt') -ComparisonPath (Join-Path $probeRoot 'reference-lumina-comparison.json')
}

if ($PublishAot) {
    $variantRoot = Join-Path $probeRoot 'lumina'
    $publishRoot = Join-Path $variantRoot 'publish'
    $publishOutput = & dotnet publish (Join-Path $variantRoot 'ParentCollectionProbe.csproj') -c Release -r win-x64 -p:PublishAot=true -o $publishRoot --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'publish.log'), [string[]]$publishOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Parent collection AOT publish failed. See $variantRoot/publish.log" }
    $executablePath = Join-Path $publishRoot 'ParentCollectionProbe.exe'
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
    $aotOutput = Invoke-ParentCollectionProbe -FilePath $executablePath -Arguments @() -LogRoot $publishRoot
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'aot-results.txt'), [string[]]$aotOutput, $utf8)
    $aotMatches = Compare-ParentCollectionOutput -ExpectedPath (Join-Path $variantRoot 'results.txt') -ActualPath (Join-Path $variantRoot 'aot-results.txt') -ComparisonPath (Join-Path $variantRoot 'aot-comparison.json')
    if (-not $aotMatches) { throw 'Parent collection AOT observations differ from Lumina JIT. See aot-comparison.json.' }
}
if (-not $matches) { throw 'Parent collection observations differ from WinForms 10.0.11. See reference-lumina-comparison.json.' }
