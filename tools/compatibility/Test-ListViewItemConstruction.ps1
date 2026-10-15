param(
    [switch]$ReferenceOnly
)

$ErrorActionPreference = 'Stop'
$itemRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $itemRepoRoot 'artifacts/compatibility-batch003/listviewitem'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
[IO.Directory]::CreateDirectory($probeRoot) | Out-Null

$source = @'
using System.Reflection;
using System.Text.Json;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        string version = typeof(ListViewItem).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
#if REFERENCE
        if (version.Split('+')[0] != "10.0.11")
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11; found {version}.");
        }
#endif
        var results = new List<object>();
        Observe("default", new ListViewItem(), results);
        Observe("string-null", new ListViewItem((string?)null), results);
        Observe("string-empty", new ListViewItem(""), results);
        Observe("string-text", new ListViewItem("row"), results);
        Observe("array-null", new ListViewItem((string[]?)null), results);
        Observe("array-empty", new ListViewItem(Array.Empty<string>()), results);
        Observe("array-one", new ListViewItem(new[] { "row" }), results);
        Observe("array-many", new ListViewItem(new[] { "row", "detail", "last" }), results);
        Observe("array-null-element", new ListViewItem(new string[] { null! }), results);
        Observe("array-mixed", new ListViewItem(new string[] { null!, "", null!, "detail" }), results);

        string[] source = ["row", "detail"];
        var copied = new ListViewItem(source);
        source[0] = "changed source";
        source[1] = "changed detail";
        results.Add(new { scenario = "array-copy", copied.Text, texts = Texts(copied.SubItems) });

        Console.WriteLine(JsonSerializer.Serialize(new { version, observations = results }));
    }

    private static void Observe(string scenario, ListViewItem item, List<object> results)
    {
        string textBeforeAccess = item.Text;
        var subItems = item.SubItems;
        var primary = subItems[0];
        results.Add(new { scenario, phase = "initial", textBeforeAccess, item.Text, subItems.Count, texts = Texts(subItems) });

        item.Text = "updated row";
        bool samePrimaryAfterSet = ReferenceEquals(primary, subItems[0]);
        primary.Text = null;
        results.Add(new { scenario, phase = "text-sync", samePrimaryAfterSet, item.Text, subItems.Count, texts = Texts(subItems) });

        subItems.Clear();
        int countBeforeText = subItems.Count;
        string textAfterClear = item.Text;
        int countAfterText = subItems.Count;
        var reopened = item.SubItems;
        results.Add(new
        {
            scenario, phase = "clear-and-access", countBeforeText, textAfterClear, countAfterText,
            sameCollection = ReferenceEquals(subItems, reopened), samePrimary = ReferenceEquals(primary, reopened[0]),
            reopened.Count, texts = Texts(reopened)
        });

        reopened.Clear();
        var added = reopened.Add("manual first");
        results.Add(new { scenario, phase = "add-before-access", item.Text, samePrimary = ReferenceEquals(added, item.SubItems[0]), reopened.Count, texts = Texts(reopened) });
    }

    private static string[] Texts(ListViewItem.ListViewSubItemCollection items)
    {
        var result = new string[items.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = items[i].Text;
        }
        return result;
    }
}
'@

$observations = @{}
$variants = if ($ReferenceOnly) { @('reference') } else { @('reference', 'lumina') }
foreach ($variant in $variants) {
    $variantRoot = Join-Path $probeRoot $variant
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $itemRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
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
    [IO.File]::WriteAllText((Join-Path $variantRoot 'ListViewItemProbe.csproj'), $project, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'Program.cs'), $source, $utf8)
    $buildOutput = & dotnet build (Join-Path $variantRoot 'ListViewItemProbe.csproj') -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "ListViewItem $variant build failed. See $variantRoot/build.log" }
    $probe = Join-Path $variantRoot "bin/Release/$framework/ListViewItemProbe.dll"
    $probeOutput = & dotnet $probe
    if ($LASTEXITCODE -ne 0) { throw "ListViewItem $variant process failed." }
    $observations[$variant] = ([string]$probeOutput) | ConvertFrom-Json
}

if ($ReferenceOnly) {
    $reportPath = Join-Path $probeRoot 'reference.json'
    $reportJson = $observations.reference | ConvertTo-Json -Depth 8
    [IO.File]::WriteAllText($reportPath, $reportJson + "`n", $utf8)
    Write-Output "Recorded WinForms $($observations.reference.version) constructor observations: $reportPath"
    return
}

$referenceJson = ConvertTo-Json -InputObject $observations.reference.observations -Depth 8 -Compress
$luminaJson = ConvertTo-Json -InputObject $observations.lumina.observations -Depth 8 -Compress
$report = [ordered]@{
    referenceVersion = $observations.reference.version
    matches = $referenceJson -ceq $luminaJson
    reference = $observations.reference.observations
    lumina = $observations.lumina.observations
}
$reportPath = Join-Path $probeRoot 'results.json'
[IO.File]::WriteAllText($reportPath, ($report | ConvertTo-Json -Depth 8) + "`n", $utf8)
if (-not $report.matches) { throw "ListViewItem construction observations differ. See $reportPath" }
Write-Output "ListViewItem construction observations match WinForms 10.0.11 in $($report.reference.Count) observations. Results: $reportPath"
