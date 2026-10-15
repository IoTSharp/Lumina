param(
    [ValidateSet('reference', 'lumina', 'both')][string]$Variant = 'both',
    [switch]$PublishAot
)

$ErrorActionPreference = 'Stop'
$textResetRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $textResetRepoRoot 'artifacts/compatibility-batch017/control-text-reset'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null
$variants = if ($Variant -eq 'both') { @('reference', 'lumina') } else { @($Variant) }

function Invoke-TextResetProbe {
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
        if (-not $exited) { throw "Text reset process exceeded 60 seconds. See $LogRoot/stderr.log" }
        if ($process.ExitCode -ne 0) { throw "Text reset process exited $($process.ExitCode). See $LogRoot/stderr.log" }
        $outputText.TrimEnd() -split '\r?\n'
    }
    finally { $process.Dispose() }
}

foreach ($current in $variants) {
    $variantRoot = Join-Path $probeRoot $current
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $textResetRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
    $probeSource = [Security.SecurityElement]::Escape((Join-Path $PSScriptRoot 'ControlTextResetProbe/Program.cs'))
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
    $projectPath = Join-Path $variantRoot 'TextResetProbe.csproj'
    [IO.File]::WriteAllText($projectPath, $project, $utf8)
    $buildOutput = & dotnet build $projectPath -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Text reset $current build failed. See $variantRoot/build.log" }
    $probeOutput = Invoke-TextResetProbe -FilePath 'dotnet.exe' -Arguments @((Join-Path $variantRoot "bin/Release/$framework/TextResetProbe.dll")) -LogRoot $variantRoot
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'results.txt'), [string[]]$probeOutput, $utf8)
    $probeOutput
}

if ($Variant -eq 'both') {
    $baseline = [IO.File]::ReadAllText((Join-Path $probeRoot 'reference/results.txt'), $utf8)
    $actual = [IO.File]::ReadAllText((Join-Path $probeRoot 'lumina/results.txt'), $utf8)
    if ($baseline -cne $actual) {
        Compare-Object ($baseline -split '\r?\n') ($actual -split '\r?\n')
        throw 'Text reset observations differ from WinForms 10.0.11.'
    }
}

if ($PublishAot) {
    if ($Variant -eq 'reference') { throw '-PublishAot requires the lumina variant.' }
    $variantRoot = Join-Path $probeRoot 'lumina'
    $publishRoot = Join-Path $variantRoot 'publish'
    $publishOutput = & dotnet publish (Join-Path $variantRoot 'TextResetProbe.csproj') -c Release -r win-x64 -p:PublishAot=true -o $publishRoot --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'publish.log'), [string[]]$publishOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "Text reset AOT publish failed. See $variantRoot/publish.log" }
    $aotOutput = Invoke-TextResetProbe -FilePath (Join-Path $publishRoot 'TextResetProbe.exe') -Arguments @() -LogRoot $publishRoot
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'aot-results.txt'), [string[]]$aotOutput, $utf8)
    $jitOutput = [IO.File]::ReadAllLines((Join-Path $variantRoot 'results.txt'), $utf8)
    $differences = [Collections.Generic.List[object]]::new()
    for ($line = 0; $line -lt [Math]::Max($jitOutput.Count, $aotOutput.Count); $line++) {
        $jitLine = if ($line -lt $jitOutput.Count) { $jitOutput[$line] } else { $null }
        $aotLine = if ($line -lt $aotOutput.Count) { $aotOutput[$line] } else { $null }
        if ($jitLine -cne $aotLine) {
            $differences.Add([ordered]@{ line = $line + 1; jit = $jitLine; aot = $aotLine })
        }
    }
    $strictMatches = $differences.Count -eq 0
    $knownMetadataDifference = $false
    $runtimePolicyEvidence = $null
    if ($jitOutput.Count -eq $aotOutput.Count -and $differences.Count -eq 1) {
        $difference = $differences[0]
        if ($difference.jit.StartsWith('Text-attributes=', [StringComparison]::Ordinal)) {
            $attributeValues = $difference.jit.Substring('Text-attributes='.Length).Split(';')
            $withoutDispId = 'Text-attributes=' + (($attributeValues | Where-Object { $_ -cne 'DispId=-517' }) -join ';')
            if (@($attributeValues | Where-Object { $_ -ceq 'DispId=-517' }).Count -eq 1 -and $difference.aot -ceq $withoutDispId) {
                # The native runtime's embedded linker rule deliberately removes
                # COM attributes. Check that exact policy before accepting its
                # sole known difference; never add a fabricated attribute value.
                $responsePath = Join-Path $variantRoot 'obj/Release/net10.0/win-x64/native/TextResetProbe.ilc.rsp'
                $compilerArguments = [IO.File]::ReadAllLines($responsePath, $utf8)
                $featureArgument = '--feature:System.Runtime.InteropServices.BuiltInComInterop.IsSupported=false'
                $corelibReference = @($compilerArguments | Where-Object { $_ -cmatch '^-r:.*[\\/]System\.Private\.CoreLib\.dll$' })
                if ($compilerArguments -ccontains $featureArgument -and $corelibReference.Count -eq 1) {
                    $corelibPath = $corelibReference[0].Substring(3)
                    $corelibText = $utf8.GetString([IO.File]::ReadAllBytes($corelibPath))
                    $policyStart = $corelibText.IndexOf('<assembly fullname="System.Private.CoreLib" feature="System.Runtime.InteropServices.BuiltInComInterop.IsSupported" featurevalue="false">', [StringComparison]::Ordinal)
                    if ($policyStart -ge 0) {
                        $policyEnd = $corelibText.IndexOf('</assembly>', $policyStart, [StringComparison]::Ordinal)
                        if ($policyEnd -ge $policyStart) {
                            $policyXml = $corelibText.Substring($policyStart, $policyEnd - $policyStart + '</assembly>'.Length)
                            $knownMetadataDifference = $policyXml -cmatch '<type fullname="System\.Runtime\.InteropServices\.DispIdAttribute">\s*<attribute internal="RemoveAttributeInstances"\s*/>'
                            $runtimePolicyEvidence = [ordered]@{
                                responseFile = $responsePath
                                featureArgument = $featureArgument
                                corelibPath = $corelibPath
                                embeddedRule = $policyXml
                                dispIdRemovalConfirmed = $knownMetadataDifference
                            }
                        }
                    }
                }
            }
        }
    }
    $inScopeMatches = $strictMatches -or $knownMetadataDifference
    $comparison = [ordered]@{
        strictMatches = $strictMatches
        inScopeMatches = $inScopeMatches
        jitLineCount = $jitOutput.Count
        aotLineCount = $aotOutput.Count
        differingLineCount = $differences.Count
        differences = $differences.ToArray()
        acceptedDispIdDifference = $knownMetadataDifference
        reason = if ($knownMetadataDifference) { 'Native AOT disables built-in COM interop; System.Private.CoreLib applies RemoveAttributeInstances to DispIdAttribute. The Text DispId=-517 attribute is present in JIT and absent in AOT. All other ordered observations are identical.' } else { $null }
        runtimePolicyEvidence = $runtimePolicyEvidence
    }
    $comparisonPath = Join-Path $variantRoot 'aot-comparison.json'
    [IO.File]::WriteAllText($comparisonPath, ($comparison | ConvertTo-Json -Depth 8), $utf8)
    if (-not $inScopeMatches) { throw "Text reset AOT observations differ from Lumina JIT beyond the documented DispId metadata policy. See $comparisonPath" }
    if ($knownMetadataDifference) { Write-Warning "Text reset AOT behavior and remaining metadata match; DispId metadata differs. strictMatches=false. See $comparisonPath" }
}
