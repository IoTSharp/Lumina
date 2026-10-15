param(
    [switch]$ReferenceOnly
)

$ErrorActionPreference = 'Stop'
$subItemRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$probeRoot = Join-Path $subItemRepoRoot 'artifacts/compatibility-batch004/listviewsubitem'
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
        Observe("default", new ListViewItem.ListViewSubItem(), results);
        Observe("null-owner-null-text", new ListViewItem.ListViewSubItem(null, null), results);
        Observe("null-owner-empty-text", new ListViewItem.ListViewSubItem(null, ""), results);
        string suppliedText = new string("provided text 中文".ToCharArray());
        var supplied = new ListViewItem.ListViewSubItem(null, suppliedText);
        results.Add(new { scenario = "constructor-text-identity", same = ReferenceEquals(suppliedText, supplied.Text) });
        Observe("null-owner-text", supplied, results);
        Observe("derived-default", new DerivedSubItem(), results);
        Observe("derived-owner-text", new DerivedSubItem(null, "derived"), results);

        var freshOwner = new ListViewItem();
        ObserveOwner("fresh-owner", freshOwner, results);
        var populatedOwner = new ListViewItem(new[] { "row", "detail" });
        ObserveOwner("populated-owner", populatedOwner, results);
        using var view = new ListView();
        var attachedOwner = new ListViewItem(new[] { "attached row", "attached detail" });
        view.Items.Add(attachedOwner);
        ObserveOwner("attached-owner", attachedOwner, results);

        var primaryOwner = new ListViewItem("row");
        var primary = primaryOwner.SubItems[0];
        primary.Name = "primary-name";
        object primaryTag = new();
        primary.Tag = primaryTag;
        primaryOwner.Text = suppliedText;
        results.Add(new
        {
            scenario = "primary-item-text", primaryOwner.Text, primaryText = primary.Text,
            primary.Name, sameTag = ReferenceEquals(primaryTag, primary.Tag),
            sameText = ReferenceEquals(suppliedText, primary.Text), samePrimary = ReferenceEquals(primary, primaryOwner.SubItems[0])
        });
        primary.Text = null;
        results.Add(new { scenario = "primary-null-text", primaryOwner.Text, primaryText = primary.Text, primary.Name, sameTag = ReferenceEquals(primaryTag, primary.Tag) });

        var replacement = new ListViewItem.ListViewSubItem(populatedOwner, "replacement");
        primaryOwner.SubItems[0] = replacement;
        primaryOwner.Text = "updated replacement";
        results.Add(new
        {
            scenario = "replace-primary", primaryOwner.Text, replacementText = replacement.Text,
            originalOwnerText = populatedOwner.Text, originalOwnerCount = populatedOwner.SubItems.Count,
            samePrimary = ReferenceEquals(replacement, primaryOwner.SubItems[0])
        });
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations = results }));
    }

    private static void Observe(string scenario, ListViewItem.ListViewSubItem item, List<object> results)
    {
        results.Add(new { scenario, phase = "initial", item.Text, item.Name, tagIsNull = item.Tag is null });
        string text = new string("updated text 中文".ToCharArray());
        string name = new string("MiXeD name 中文".ToCharArray());
        object tag = new();
        item.Text = text;
        item.Name = name;
        item.Tag = tag;
        results.Add(new
        {
            scenario, phase = "assigned", item.Text, item.Name,
            sameText = ReferenceEquals(text, item.Text), sameName = ReferenceEquals(name, item.Name), sameTag = ReferenceEquals(tag, item.Tag)
        });
        item.Text = null;
        item.Name = null;
        item.Tag = null;
        results.Add(new { scenario, phase = "null", item.Text, item.Name, tagIsNull = item.Tag is null });
        item.Text = "";
        item.Name = "";
        item.Tag = 42;
        object? boxed = item.Tag;
        results.Add(new { scenario, phase = "empty", item.Text, item.Name, tag = item.Tag, sameTag = ReferenceEquals(boxed, item.Tag) });
    }

    private static void ObserveOwner(string scenario, ListViewItem owner, List<object> results)
    {
        string beforeText = owner.Text;
        var beforeItems = owner.SubItems;
        var beforePrimary = beforeItems[0];
        int beforeCount = beforeItems.Count;
        var item = new ListViewItem.ListViewSubItem(owner, "constructed detail");
        results.Add(new
        {
            scenario, phase = "constructed", beforeText, beforeCount, owner.Text,
            count = owner.SubItems.Count, sameCollection = ReferenceEquals(beforeItems, owner.SubItems),
            samePrimary = ReferenceEquals(beforePrimary, owner.SubItems[0]), isMember = owner.SubItems.Contains(item),
            subItemText = item.Text, item.Name, tagIsNull = item.Tag is null
        });
        item.Text = "changed detached detail";
        results.Add(new { scenario, phase = "set-text", owner.Text, count = owner.SubItems.Count, subItemText = item.Text });
        owner.SubItems.Add(item);
        item.Text = "changed member detail";
        results.Add(new
        {
            scenario, phase = "added", owner.Text, count = owner.SubItems.Count,
            subItemText = owner.SubItems[beforeCount].Text, same = ReferenceEquals(item, owner.SubItems[beforeCount])
        });
    }

    private sealed class DerivedSubItem : ListViewItem.ListViewSubItem
    {
        internal DerivedSubItem()
        {
        }

        internal DerivedSubItem(ListViewItem? owner, string? text) : base(owner, text)
        {
        }
    }
}
'@

$observations = @{}
$variants = if ($ReferenceOnly) { @('reference') } else { @('reference', 'lumina') }
foreach ($variant in $variants) {
    $variantRoot = Join-Path $probeRoot $variant
    [IO.Directory]::CreateDirectory($variantRoot) | Out-Null
    $formsProject = [Security.SecurityElement]::Escape((Join-Path $subItemRepoRoot 'Lumina.Forms/Lumina.Forms.csproj'))
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
    [IO.File]::WriteAllText((Join-Path $variantRoot 'ListViewSubItemProbe.csproj'), $project, $utf8)
    [IO.File]::WriteAllText((Join-Path $variantRoot 'Program.cs'), $source, $utf8)
    $buildOutput = & dotnet build (Join-Path $variantRoot 'ListViewSubItemProbe.csproj') -c Release --nologo -v quiet 2>&1
    [IO.File]::WriteAllLines((Join-Path $variantRoot 'build.log'), [string[]]$buildOutput, $utf8)
    if ($LASTEXITCODE -ne 0) { throw "ListViewSubItem $variant build failed. See $variantRoot/build.log" }
    $probe = Join-Path $variantRoot "bin/Release/$framework/ListViewSubItemProbe.dll"
    $probeOutput = & dotnet $probe
    if ($LASTEXITCODE -ne 0) { throw "ListViewSubItem $variant process failed." }
    $observations[$variant] = ([string]$probeOutput) | ConvertFrom-Json
}

if ($ReferenceOnly) {
    $reportPath = Join-Path $probeRoot 'reference.json'
    $reportJson = $observations.reference | ConvertTo-Json -Depth 8
    [IO.File]::WriteAllText($reportPath, $reportJson + "`n", $utf8)
    Write-Output "Recorded WinForms $($observations.reference.version) subitem observations: $reportPath"
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
if (-not $report.matches) { throw "ListViewSubItem construction observations differ. See $reportPath" }
Write-Output "ListViewSubItem construction observations match WinForms 10.0.11 in $($report.reference.Count) observations. Results: $reportPath"
