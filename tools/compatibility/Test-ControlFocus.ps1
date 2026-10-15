param(
    [ValidateSet('reference', 'lumina', 'both')][string]$Variant = 'both',
    [switch]$PublishAot
)

$ErrorActionPreference = 'Stop'
$focusRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $focusRepoRoot 'artifacts/compatibility-batch008/control-focus'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null
$variants = if ($Variant -eq 'both') { @('reference', 'lumina') } else { @($Variant) }

function Invoke-FocusProbe {
    param([string]$FilePath, [string[]]$Arguments, [string]$LogRoot)
    $start = [Diagnostics.ProcessStartInfo]::new()
    $start.FileName = $FilePath
    foreach ($argument in $Arguments) { $start.ArgumentList.Add($argument) }
    $start.UseShellExecute = $false
    $start.CreateNoWindow = $true
    $start.RedirectStandardOutput = $true
    $start.RedirectStandardError = $true
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
        if (-not $exited) { throw "Focus process exceeded 30 seconds. See $LogRoot/stderr.log" }
        if ($process.ExitCode -ne 0) { throw "Focus process exited $($process.ExitCode). See $LogRoot/stderr.log" }
        $outputText.TrimEnd() -split '\r?\n'
    }
    finally { $process.Dispose() }
}

foreach ($current in $variants) {
    $variantRoot = Join-Path $probeRoot $current
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $focusRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $probeSource = [Security.SecurityElement]::Escape((Join-Path $PSScriptRoot 'ControlFocusProbe/Program.cs'))
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
    $projectPath = Join-Path $variantRoot 'FocusProbe.csproj'
    [IO.File]::WriteAllText($projectPath, $project, $utf8)
    $buildOutput = & dotnet build $projectPath -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Focus $current build failed. See $variantRoot/build.log" }
    $probeOutput = Invoke-FocusProbe -FilePath 'dotnet.exe' -Arguments @((Join-Path $variantRoot "bin/Release/$framework/FocusProbe.dll")) -LogRoot $variantRoot
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'results.txt'), [string[]]$probeOutput, $utf8)
    $probeOutput
}

if ($Variant -eq 'both') {
    $baseline = [IO.File]::ReadAllText((Join-Path $probeRoot 'reference/results.txt'), $utf8)
    $actual = [IO.File]::ReadAllText((Join-Path $probeRoot 'lumina/results.txt'), $utf8)
    if ($baseline -cne $actual) {
        Compare-Object ($baseline -split '\r?\n') ($actual -split '\r?\n')
        throw 'Focus observations differ from WinForms 10.0.11.'
    }
}

if ($PublishAot) {
    if ($Variant -eq 'reference') { throw '-PublishAot requires the lumina variant.' }
    $variantRoot = Join-Path $probeRoot 'lumina'
    $publishRoot = Join-Path $variantRoot 'publish'
    $publishOutput = & dotnet publish (Join-Path $variantRoot 'FocusProbe.csproj') -c Release -r win-x64 -p:PublishAot=true -o $publishRoot --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'publish.log'), [string[]]$publishOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Focus AOT publish failed. See $variantRoot/publish.log" }
    $aotOutput = Invoke-FocusProbe -FilePath (Join-Path $publishRoot 'FocusProbe.exe') -Arguments @() -LogRoot $publishRoot
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'aot-results.txt'), [string[]]$aotOutput, $utf8)
    $jitOutput = [IO.File]::ReadAllLines((Join-Path $variantRoot 'results.txt'), $utf8)
    if (($aotOutput -join "`n") -cne ($jitOutput -join "`n")) { throw 'Focus AOT observations differ from Lumina JIT.' }
}
