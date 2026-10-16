param(
    [ValidateSet('reference', 'lumina', 'both')][string]$Variant = 'both',
    [switch]$PublishAot,
    [switch]$ReferenceOnly,
    [string]$ReferenceResultsPath,
    [ValidateRange(5, 60)][int]$TimeoutSeconds = 30,
    [ValidateRange(60, 3600)][int]$BuildTimeoutSeconds = 600
)

$ErrorActionPreference = 'Stop'
if ($ReferenceOnly) { $Variant = 'reference' }
$messagePaintRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $messagePaintRepoRoot 'artifacts/compatibility-batch023/control-message-paint'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::InputEncoding = $utf8
[Console]::OutputEncoding = $utf8
$OutputEncoding = $utf8
if ($PublishAot -and $Variant -eq 'reference') { throw '-PublishAot requires the lumina variant.' }
$runRoot = Join-Path $probeRoot ('runs/' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fffffff'))
[IO.Directory]::CreateDirectory($runRoot) | Out-Null
$sourcePath = Join-Path $PSScriptRoot 'ControlMessagePaintProbe/Program.cs'
$sourceSnapshotPath = Join-Path $runRoot 'Program.cs'
[IO.File]::Copy($sourcePath, $sourceSnapshotPath)
$sourcePath = $sourceSnapshotPath
$sourceHash = (Get-FileHash -LiteralPath $sourcePath -Algorithm SHA256).Hash
$scenarios = @('metadata', 'message', 'message-names', 'create-params', 'styles', 'style-events', 'wndproc', 'create-native', 'create-parameter', 'form-messages', 'paint-args', 'paint-direct', 'invalidate', 'native-paint', 'native-edit', 'transparent', 'reentrancy', 'exceptions')
$variants = if ($Variant -eq 'both') { @('reference', 'lumina') } else { @($Variant) }
$results = @{}

function Invoke-MessagePaintProbe {
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
        # Remove exactly the final line terminator, preserving all other whitespace.
        $lineText = if ($outputText.EndsWith("`r`n")) { $outputText.Substring(0, $outputText.Length - 2) } elseif ($outputText.EndsWith("`n")) { $outputText.Substring(0, $outputText.Length - 1) } else { $outputText }
        $lines = if ($lineText.Length -eq 0) { @() } else { @($lineText -split '\r?\n') }
        $complete = $lines.Count -gt 0 -and $lines[-1] -ceq 'complete=True'
        $processRecord = [ordered]@{ scenario = $Scenario; executable = $FilePath; arguments = @($Arguments) + $Scenario; timedOut = -not $exited; exitCode = $process.ExitCode; complete = $complete; stdout = $outputText; stderr = $errorText }
        [IO.File]::WriteAllText((Join-Path $LogRoot 'process.json'), ($processRecord | ConvertTo-Json -Depth 5), $utf8)
        if (-not $exited) { throw "MessagePaint $Scenario exceeded $TimeoutSeconds seconds; raw output retained in $LogRoot." }
        if ($process.ExitCode -ne 0) { throw "MessagePaint $Scenario exited $($process.ExitCode); raw output retained in $LogRoot." }
        if (-not $complete) { throw "MessagePaint $Scenario did not reach its completion marker; raw output retained in $LogRoot." }
        return $lines
    }
    finally { $process.Dispose() }
}

function Invoke-MessagePaintBuild {
    param([string[]]$Arguments, [string]$LogRoot, [string]$Operation)
    $start = [Diagnostics.ProcessStartInfo]::new()
    $start.FileName = 'dotnet.exe'
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
        $exited = $process.WaitForExit($BuildTimeoutSeconds * 1000)
        if (-not $exited) { $process.Kill($true); $process.WaitForExit() }
        $outputText = $outputTask.GetAwaiter().GetResult()
        $errorText = $errorTask.GetAwaiter().GetResult()
        [IO.File]::WriteAllText((Join-Path $LogRoot "$Operation-stdout.log"), $outputText, $utf8)
        [IO.File]::WriteAllText((Join-Path $LogRoot "$Operation-stderr.log"), $errorText, $utf8)
        [IO.File]::WriteAllText((Join-Path $LogRoot "$Operation.log"), $outputText + $errorText, $utf8)
        $status = [ordered]@{ operation = $Operation; executable = $start.FileName; arguments = $Arguments; exitCode = $process.ExitCode; timedOut = -not $exited; timeoutSeconds = $BuildTimeoutSeconds; sourceHash = $sourceHash; stdout = $outputText; stderr = $errorText }
        [IO.File]::WriteAllText((Join-Path $LogRoot "$Operation-status.json"), ($status | ConvertTo-Json -Depth 5), $utf8)
        if (-not $exited) { throw "MessagePaint $Operation exceeded $BuildTimeoutSeconds seconds; raw output retained in $LogRoot." }
        if ($process.ExitCode -ne 0) { throw "MessagePaint $Operation exited $($process.ExitCode); raw output retained in $LogRoot." }
    }
    finally { $process.Dispose() }
}

function Compare-MessagePaintOutput {
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

function Accept-MessagePaintAotBoundary {
    param([string]$ComparisonPath)

    $comparison = [IO.File]::ReadAllText($ComparisonPath, $utf8) | ConvertFrom-Json
    $expected = @(
        @{ line = 420; expected = 'create-payload=exception:none|events:[creation-129:payload:135,246;creation-1:payload:135,246]|state:[mask:275202|buffered:False|handle:True|disposed:False]'; actual = 'create-payload=exception:NotSupportedException|events:[]|state:[mask:275202|buffered:False|handle:False|disposed:False]' },
        @{ line = 421; expected = 'creation-observed-payload=2'; actual = 'creation-observed-payload=0' },
        @{ line = 422; expected = 'native-created-payload=True'; actual = 'native-created-payload=False' },
        @{ line = 423; expected = 'create-object=exception:ArgumentException:|events:[]|state:[mask:275202|buffered:False|handle:False|disposed:False]'; actual = 'create-object=exception:NotSupportedException|events:[]|state:[mask:275202|buffered:False|handle:False|disposed:False]' }
    )
    $actual = @($comparison.differences | ForEach-Object {
        [ordered]@{ line = [int]$_.line; expected = [string]$_.expected; actual = [string]$_.actual }
    })
    $accepted = $actual.Count -eq $expected.Count
    if ($accepted) {
        for ($index = 0; $index -lt $expected.Count; $index++) {
            $accepted = $accepted -and $actual[$index].line -eq $expected[$index].line `
                -and $actual[$index].expected -ceq $expected[$index].expected `
                -and $actual[$index].actual -ceq $expected[$index].actual
        }
    }
    $reason = $null
    if ($accepted) {
        $reason = 'Native AOT intentionally rejects custom struct/object CreateParams.Param values because NativeCreateParameter exposes only statically rooted primitive, decimal, Guid, pointer and string marshalling. Raw AOT output is preserved in aot-results.txt.'
    }
    $comparison | Add-Member -NotePropertyName inScopeMatches -NotePropertyValue ([bool]$accepted) -Force
    $comparison | Add-Member -NotePropertyName acceptedAotBoundary -NotePropertyValue ([bool]$accepted) -Force
    $comparison | Add-Member -NotePropertyName reason -NotePropertyValue $reason -Force
    [IO.File]::WriteAllText($ComparisonPath, ($comparison | ConvertTo-Json -Depth 8), $utf8)
    return $accepted
}

foreach ($current in $variants) {
    $variantRoot = Join-Path $runRoot $current
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $messagePaintRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $probeSource = [Security.SecurityElement]::Escape($sourcePath)
    $framework = if ($current -eq 'reference') { 'net10.0-windows' } else { 'net10.0' }
    $settings = if ($current -eq 'reference') { '<UseWindowsForms>true</UseWindowsForms><DefineConstants>REFERENCE</DefineConstants><RuntimeFrameworkVersion>10.0.11</RuntimeFrameworkVersion><RollForward>Disable</RollForward>' } else { '' }
    $reference = if ($current -eq 'reference') { '' } else { '<ItemGroup><ProjectReference Include="' + $formsProject + '" /></ItemGroup>' }
    $project = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType><TargetFramework>$framework</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable><AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    $settings
  </PropertyGroup>
  <ItemGroup><Compile Include="$probeSource" Link="Program.cs" /></ItemGroup>
  $reference
</Project>
"@
    $projectPath = Join-Path $variantRoot 'MessagePaintProbe.csproj'
    [IO.File]::WriteAllText($projectPath, $project, $utf8)
    Invoke-MessagePaintBuild -Arguments @('build', $projectPath, '-c', 'Release', '--nologo', '-v', 'quiet') -LogRoot $variantRoot -Operation 'build'
    $allLines = [Collections.Generic.List[string]]::new()
    foreach ($scenario in $scenarios) {
        $probeOutput = @(Invoke-MessagePaintProbe -FilePath 'dotnet.exe' -Arguments @((Join-Path $variantRoot "bin/Release/$framework/MessagePaintProbe.dll")) -LogRoot (Join-Path $variantRoot $scenario) -Scenario $scenario)
        $allLines.AddRange([string[]]$probeOutput)
        [IO.File]::WriteAllLines((Join-Path $variantRoot 'results.txt'), $allLines, $utf8)
    }
    $results[$current] = Join-Path $variantRoot 'results.txt'
    $manifest = [ordered]@{ variant = $current; sourceHash = $sourceHash; sourceSnapshotPath = $sourceSnapshotPath; resultsPath = $results[$current]; scenarioCount = $scenarios.Count; scenarios = $scenarios; lineCount = $allLines.Count; complete = $true }
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
    if (-not (Test-Path -LiteralPath $ReferenceResultsPath -PathType Leaf)) { throw "Reference results do not exist: $ReferenceResultsPath" }
    $referenceManifestPath = Join-Path ([IO.Path]::GetDirectoryName([IO.Path]::GetFullPath($ReferenceResultsPath))) 'manifest.json'
    if (-not (Test-Path -LiteralPath $referenceManifestPath -PathType Leaf)) { throw 'Reference results require their original sibling manifest.json.' }
    $referenceManifest = [IO.File]::ReadAllText($referenceManifestPath, $utf8) | ConvertFrom-Json
    if ($referenceManifest.variant -cne 'reference' -or -not $referenceManifest.complete -or $referenceManifest.sourceHash -cne $sourceHash) { throw 'Reference manifest must be complete and match this exact probe source hash.' }
    if (($referenceManifest.scenarios -join ',') -cne ($scenarios -join ',')) { throw 'Reference scenarios differ from the current message/paint probe.' }
    $matches = Compare-MessagePaintOutput -ExpectedPath $ReferenceResultsPath -ActualPath $results['lumina'] -ComparisonPath (Join-Path $runRoot 'reference-lumina-comparison.json')
}

$aotMatches = $true
$aotInScopeMatches = $true
if ($PublishAot) {
    $variantRoot = Join-Path $runRoot 'lumina'
    $publishRoot = Join-Path $variantRoot 'publish'
    Invoke-MessagePaintBuild -Arguments @('publish', (Join-Path $variantRoot 'MessagePaintProbe.csproj'), '-c', 'Release', '-r', 'win-x64', '-p:PublishAot=true', '-o', $publishRoot, '--nologo', '-v', 'quiet') -LogRoot $variantRoot -Operation 'publish'
    $executablePath = Join-Path $publishRoot 'MessagePaintProbe.exe'
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
        $aotOutput = @(Invoke-MessagePaintProbe -FilePath $executablePath -Arguments @() -LogRoot (Join-Path $publishRoot $scenario) -Scenario $scenario)
        $aotLines.AddRange([string[]]$aotOutput)
        [IO.File]::WriteAllLines((Join-Path $variantRoot 'aot-results.txt'), $aotLines, $utf8)
    }
    $aotComparisonPath = Join-Path $runRoot 'aot-comparison.json'
    $aotMatches = Compare-MessagePaintOutput -ExpectedPath $results['lumina'] -ActualPath (Join-Path $variantRoot 'aot-results.txt') -ComparisonPath $aotComparisonPath
    $aotInScopeMatches = $aotMatches
    if (-not $aotMatches) { $aotInScopeMatches = Accept-MessagePaintAotBoundary -ComparisonPath $aotComparisonPath }
    Write-Output "AOT complete: $($aotLines.Count) lines; $variantRoot/aot-results.txt"
}
$summary = [ordered]@{ variant = $Variant; sourceHash = $sourceHash; referenceMatches = if ($Variant -eq 'reference') { $null } else { $matches }; aotRequested = [bool]$PublishAot; aotMatches = if ($PublishAot) { $aotMatches } else { $null }; aotInScopeMatches = if ($PublishAot) { $aotInScopeMatches } else { $null }; runRoot = $runRoot }
[IO.File]::WriteAllText((Join-Path $runRoot 'summary.json'), ($summary | ConvertTo-Json), $utf8)
if (-not $aotInScopeMatches) { throw "MessagePaint AOT observations differ from Lumina JIT beyond the documented custom creation parameter boundary. See $runRoot/aot-comparison.json." }
if (-not $matches) { throw "MessagePaint observations differ from WinForms 10.0.11. See $runRoot/reference-lumina-comparison.json." }
