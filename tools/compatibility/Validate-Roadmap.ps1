param([switch]$ExportTasks)

$ErrorActionPreference = 'Stop'
$taskRepoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$roadmapRoot = Join-Path $taskRepoRoot 'docs/roadmap'
$utf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $utf8
$errors = [Collections.Generic.List[string]]::new()
$tasks = [Collections.Generic.List[object]]::new()
$taskIds = [Collections.Generic.HashSet[string]]::new()
function Get-DevelopmentState([string]$statusText) {
    if ($statusText.Contains('✅') -or $statusText.Contains('🧪 验收通过')) { return 'code-complete' }
    if ($statusText.Contains('🚧')) { return 'in-progress' }
    if ($statusText.Contains('🟡')) { return 'partial-unverified' }
    if ($statusText.Contains('🔎')) { return 'investigate' }
    if ($statusText.Contains('🚫')) { return 'platform-difference' }
    return 'pending'
}
function Get-VerificationState([string]$statusText) {
    if ($statusText.Contains('🧪 验收通过')) { return 'verified' }
    return 'unverified'
}
$types = Get-Content -Raw -Encoding UTF8 -LiteralPath (Join-Path $roadmapRoot 'api/types.json') | ConvertFrom-Json
$typeMap = @{}
foreach ($type in $types) { $typeMap[$type.Type] = $type }
function Test-DerivesFrom($item, [string]$baseName) {
    $node = $item
    while ($null -ne $node) {
        if ($node.Type -eq $baseName -or $node.BaseType -eq $baseName) { return $true }
        $node = $typeMap[$node.BaseType]
    }
    return $false
}
$allControls = @($types | Where-Object { Test-DerivesFrom $_ 'System.Windows.Forms.Control' })
$concreteControls = @($allControls | Where-Object { -not $_.Abstract -and -not $_.Type.Contains('+') })
$toolStripItems = @($types | Where-Object { Test-DerivesFrom $_ 'System.Windows.Forms.ToolStripItem' })
$dialogs = @($types | Where-Object { Test-DerivesFrom $_ 'System.Windows.Forms.CommonDialog' })
$menus = @($types | Where-Object { Test-DerivesFrom $_ 'System.Windows.Forms.Menu' })
$components = @($types | Where-Object { Test-DerivesFrom $_ 'System.ComponentModel.Component' })
$taskDialogs = @($types | Where-Object { $_.Type -like 'System.Windows.Forms.TaskDialog*' -and $_.Kind -eq 'Type' -and $_.BaseType -ne 'System.EventArgs' })
$gridElements = @($types | Where-Object { Test-DerivesFrom $_ 'System.Windows.Forms.DataGridViewElement' })
$pageMap = @(Import-Csv -LiteralPath (Join-Path $roadmapRoot 'api/page-map.csv') -Encoding UTF8)
$pageByType = @{}
$pageByDocument = @{}
$documentNames = [Collections.Generic.HashSet[string]]::new()
foreach ($page in $pageMap) {
    if ($pageByType.ContainsKey($page.Type)) { $errors.Add("Duplicate page type: $($page.Type)") }
    if (-not $documentNames.Add($page.DocumentType)) { $errors.Add("Duplicate page document: $($page.DocumentType)") }
    $pageByType[$page.Type] = $page
    $pageByDocument[$page.DocumentType] = $page
    foreach ($directory in @('controls', 'api')) {
        if (-not (Test-Path -LiteralPath (Join-Path $roadmapRoot "$directory/$($page.DocumentType).md"))) {
            $errors.Add("Missing $directory page: $($page.Type)")
        }
    }
    if ($page.Source -eq 'sdk-generated') { continue }
    $baselineType = $typeMap[$page.Type]
    if ($null -eq $baselineType) { $errors.Add("Unknown full baseline identity: $($page.Type)"); continue }
    foreach ($field in @('CandidatePresent', 'RelationshipsMatch', 'PublicProperties', 'PublicEvents', 'PublicMethods', 'ProtectedMembers')) {
        if ([string]$page.$field -ne [string]$baselineType.$field) { $errors.Add("Stale page map $field : $($page.Type)") }
    }
    $apiPath = Join-Path $roadmapRoot "api/$($page.DocumentType).md"
    if (Test-Path -LiteralPath $apiPath) {
        $apiBody = [IO.File]::ReadAllText($apiPath, $utf8)
        if (-not $apiBody.Contains(('类型：`' + $page.Type + '`'))) { $errors.Add("Wrong API page identity: $($page.DocumentType)") }
    }
}

$stages = @{}
foreach ($line in (Get-Content -Encoding UTF8 -LiteralPath (Join-Path $roadmapRoot 'README.md'))) {
    if ($line -match '^\| \[(?<Type>[^]]+)\]\(controls/[^)]+\) \| (?<Stage>[^|]+) \|') {
        $documentName = $Matches.Type
        if ($stages.ContainsKey($documentName)) { $errors.Add("Duplicate index row: $documentName") }
        $stages[$documentName] = $Matches.Stage.Trim()
        $page = $pageByDocument[$documentName]
        if ($null -eq $page) { $errors.Add("Unknown index identity: $documentName"); continue }
        $cells = @([regex]::Split($line.Trim().Trim('|'), '(?<!\\)\|') | ForEach-Object { $_.Trim() })
        if ($page.Source -eq 'reference-assembly') {
            $expectedCounts = "$($page.PublicProperties) / $($page.PublicEvents) / $($page.PublicMethods)"
            if ($cells[3] -ne $expectedCounts) { $errors.Add("Wrong index member counts: $documentName") }
            $expectedShape = if ($page.CandidatePresent -eq 'True') { '🟡 存在' } else { '⬜ 缺失' }
            if ($cells[2] -ne $expectedShape) { $errors.Add("Wrong index candidate identity: $documentName") }
        }
    }
}
foreach ($type in (@($allControls) + @($toolStripItems) + @($dialogs) + @($menus) + @($components) + @($taskDialogs) + @($gridElements) | Sort-Object Type -Unique)) {
    if (-not $pageByType.ContainsKey($type.Type)) { $errors.Add("Missing UI-family page: $($type.Type)") }
}
foreach ($page in $pageMap) {
    if (-not $stages.ContainsKey($page.DocumentType)) { $errors.Add("Missing roadmap stage: $($page.DocumentType)") }
}

$files = @(Get-ChildItem -LiteralPath $roadmapRoot -Recurse -File -Filter '*.md')
$files += Get-Item -LiteralPath (Join-Path $taskRepoRoot 'ROADMAP.md'), (Join-Path $taskRepoRoot 'README.md'), (Join-Path $taskRepoRoot 'README.zh-CN.md'), (Join-Path $taskRepoRoot 'HANDOFF.md')
foreach ($file in $files) {
    $body = [IO.File]::ReadAllText($file.FullName, $utf8)
    if ($body.Contains([char]0)) { $errors.Add("NUL byte: $($file.FullName)") }
    foreach ($link in [regex]::Matches($body, '\[[^\]\r\n]+\]\((?<target>[^)\r\n]+)\)')) {
        $target = $link.Groups['target'].Value.Trim().Trim('<', '>')
        if ($target -match '^(https?:|mailto:|#|/zh/)' -or $target -match '^data:') { continue }
        $target = ($target -split '#')[0]
        if ([string]::IsNullOrWhiteSpace($target)) { continue }
        $resolved = [IO.Path]::GetFullPath((Join-Path $file.DirectoryName ([Uri]::UnescapeDataString($target))))
        if (-not (Test-Path -LiteralPath $resolved)) { $errors.Add("Broken link: $($file.Name) -> $target") }
    }
    $tableColumns = 0
    foreach ($line in ($body -split '\r?\n')) {
        if (-not $line.StartsWith('|')) { $tableColumns = 0; continue }
        $columns = [regex]::Matches($line, '(?<!\\)\|').Count
        if ($tableColumns -eq 0) { $tableColumns = $columns }
        elseif ($columns -ne $tableColumns) { $errors.Add("Table columns: $($file.Name): $line") }
        if ($file.Directory.Name -ne 'controls') { continue }
        $idMatch = [regex]::Match($line, [regex]::Escape($file.BaseName) + '-[A-Za-z0-9]+')
        if (-not $idMatch.Success) { continue }
        $taskId = $idMatch.Value
        if (-not $taskIds.Add($taskId)) { $errors.Add("Duplicate task ID: $taskId") }
        $priority = [regex]::Match($line, '\bP[0-3]\b').Value
        if ([string]::IsNullOrEmpty($priority)) { $priority = 'P3' }
        $cells = [regex]::Split($line.Trim().Trim('|'), '(?<!\\)\|')
        $statusCells = ($cells[0..($cells.Count - 2)] -join ' ')
        $tasks.Add([pscustomobject]@{
            Stage = $stages[$file.BaseName]
            Priority = $priority
            Type = $file.BaseName
            TaskId = $taskId
            Status = Get-DevelopmentState $statusCells
            Verification = Get-VerificationState $statusCells
            Detail = $line.Trim().Trim('|').Trim()
            Document = 'controls/' + $file.Name
        })
    }
    if ($file.Directory.Name -eq 'controls') {
        if (-not $documentNames.Contains($file.BaseName)) { $errors.Add("Page not in full-type map: $($file.Name)") }
        foreach ($term in @('Windows', 'Linux', 'macOS', '修改历史')) {
            if (-not $body.Contains($term)) { $errors.Add("Missing $term section: $($file.Name)") }
        }
        if (-not $body.Contains('../api/' + $file.Name)) { $errors.Add("Missing full API link: $($file.Name)") }
    }
}

$controlTaskCount = $tasks.Count
# These cross-control tasks are part of the same index, not a second scheduler.
$globalDocuments = @{
    'foundations.md' = 'Foundation'
    'project-consolidation.md' = 'Repository'
    'contracts.md' = 'Contract'
    'platforms.md' = 'Platform'
}
foreach ($document in $globalDocuments.Keys) {
    foreach ($line in (Get-Content -Encoding UTF8 -LiteralPath (Join-Path $roadmapRoot $document))) {
        if (-not $line.StartsWith('|')) { continue }
        $cells = @([regex]::Split($line.Trim().Trim('|'), '(?<!\\)\|') | ForEach-Object { $_.Trim() })
        $match = [regex]::Match($cells[0], '^(?:\d+ )?(?<Id>(?:FND|REP|CNS|XPL)-\d{3})$')
        if (-not $match.Success) { continue }
        $taskId = $match.Groups['Id'].Value
        if (-not $taskIds.Add($taskId)) { $errors.Add("Duplicate task ID: $taskId") }
        $priority = [regex]::Match($cells[1], '\bP[0-3]\b').Value
        if ([string]::IsNullOrEmpty($priority)) { $priority = if ($taskId.StartsWith('XPL-')) { 'P1' } elseif ($taskId -match '^FND-00') { 'P0' } else { 'P1' } }
        $stage = if ($taskId.StartsWith('XPL-')) { 'XPL' } elseif ($taskId -match '^FND-00|^REP-00[12]$') { 'M0' } elseif ($priority -in @('P0', 'P1')) { 'M1' } elseif ($priority -eq 'P2') { 'M3' } else { 'M4' }
        $tasks.Add([pscustomobject]@{
            Stage = $stage
            Priority = $priority
            Type = $globalDocuments[$document]
            TaskId = $taskId
            Status = Get-DevelopmentState $cells[1]
            Verification = Get-VerificationState $cells[1]
            Detail = $line.Trim().Trim('|').Trim()
            Document = $document
        })
    }
}

$ownership = @(Import-Csv -LiteralPath (Join-Path $roadmapRoot 'api/ownership.csv') -Encoding UTF8)
if ($ownership.Count -ne $types.Count) { $errors.Add('Ownership type count mismatch.') }
$ownedTypes = [Collections.Generic.HashSet[string]]::new()
foreach ($row in $ownership) {
    if (-not $ownedTypes.Add($row.Type)) { $errors.Add("Duplicate ownership type: $($row.Type)") }
    if (-not $typeMap.ContainsKey($row.Type)) { $errors.Add("Unknown ownership type: $($row.Type)") }
    if (-not (Test-Path -LiteralPath (Join-Path $roadmapRoot $row.OwnerDocument))) { $errors.Add("Missing owner: $($row.Type)") }
}
foreach ($type in $types) {
    if (-not $ownedTypes.Contains($type.Type)) { $errors.Add("Type without owner: $($type.Type)") }
}

# The index distinguishes a matching type name from completed roadmap work.
# Source task pages remain authoritative; inherited/base work never closes a row.
$implementationByType = @{}
foreach ($page in $pageMap) {
    $developmentTasks = @($tasks | Where-Object { $_.Type -eq $page.DocumentType -and $_.TaskId -notmatch '-V$' })
    $completed = @($developmentTasks | Where-Object Status -eq 'code-complete' | Sort-Object TaskId)
    $unfinished = @($developmentTasks | Where-Object Status -ne 'code-complete')
    $assigned = @($developmentTasks | Where-Object Status -eq 'in-progress')
    if ($developmentTasks.Count -gt 0 -and $unfinished.Count -eq 0) {
        $summary = '✅ 全部开发任务代码完成；兼容验收另列'
    }
    elseif ($assigned.Count -gt 0) {
        $summary = '🚧 已分配：' + (($assigned | ForEach-Object TaskId) -join '、')
    }
    elseif ($completed.Count -gt 0 -or @($developmentTasks | Where-Object Status -eq 'partial-unverified').Count -gt 0) {
        $summary = '🟡 部分完成'
    }
    else {
        $summary = if ($page.CandidatePresent -eq 'True' -or $page.Source -eq 'sdk-generated') { '⬜ 开发任务待实现；已有类型待逐项核实' } else { '⬜ 待实现' }
    }
    if ($completed.Count -gt 0 -and $unfinished.Count -gt 0) {
        $summary += '；✅ ' + (($completed | ForEach-Object TaskId) -join '、') + '；其余任务未完成'
    }
    elseif ($completed.Count -eq 0) { $summary += '；暂无独立任务完成记录' }
    $implementationByType[$page.DocumentType] = $summary
}

$indexPath = Join-Path $roadmapRoot 'README.md'
if ($ExportTasks) {
    $rows = $tasks | Sort-Object Stage, Priority, Type, TaskId | ConvertTo-Csv -NoTypeInformation
    [IO.File]::WriteAllText((Join-Path $roadmapRoot 'tasks.csv'), ($rows -join "`n") + "`n", $utf8)
    $indexLines = @(foreach ($line in [IO.File]::ReadAllLines($indexPath, $utf8)) {
        if ($line -match '^\| \[(?<Type>[^]]+)\]\(controls/[^)]+\) \|') {
            $documentName = $Matches.Type
            $cells = @([regex]::Split($line.Trim().Trim('|'), '(?<!\\)\|') | ForEach-Object { $_.Trim() })
            '| ' + (($cells[0..4] + $implementationByType[$documentName]) -join ' | ') + ' |'
        }
        elseif ($line -eq '| 类型与独立任务 | 实施阶段 | 当前类型 | 公开属性 / 事件 / 方法 | Windows / Linux / macOS验收 |') {
            $line.TrimEnd('|').TrimEnd() + ' | 代码实施与已完成子任务 |'
        }
        elseif ($line -eq '| --- | --- | --- | --- | --- |') { '| --- | --- | --- | --- | --- | --- |' }
        else { $line }
    })
    [IO.File]::WriteAllText($indexPath, ($indexLines -join "`n") + "`n", $utf8)
}
foreach ($line in [IO.File]::ReadAllLines($indexPath, $utf8)) {
    if ($line -notmatch '^\| \[(?<Type>[^]]+)\]\(controls/[^)]+\) \|') { continue }
    $documentName = $Matches.Type
    $cells = @([regex]::Split($line.Trim().Trim('|'), '(?<!\\)\|') | ForEach-Object { $_.Trim() })
    if ($cells.Count -ne 6 -or $cells[5] -cne $implementationByType[$documentName]) {
        $errors.Add("Stale index implementation state: $documentName (run -ExportTasks after reviewing its task page)")
    }
}
$report = [ordered]@{
    baselineTypes = $types.Count
    allPublicControls = $allControls.Count
    concreteTopLevelControls = $concreteControls.Count
    publicToolStripItems = $toolStripItems.Count
    publicCommonDialogs = $dialogs.Count
    publicMenus = $menus.Count
    publicTaskDialogObjects = $taskDialogs.Count
    publicDataGridViewElements = $gridElements.Count
    publicComponents = $components.Count
    controlPages = @(Get-ChildItem -LiteralPath (Join-Path $roadmapRoot 'controls') -Filter '*.md').Count
    apiPages = @(Get-ChildItem -LiteralPath (Join-Path $roadmapRoot 'api') -Filter '*.md').Count
    detailedControlTasks = $controlTaskCount
    sharedTasks = $tasks.Count - $controlTaskCount
    detailedTasks = $tasks.Count
    markdownFilesChecked = $files.Count
    indexImplementationRows = $implementationByType.Count
    indexCodeCompleteTypes = @($implementationByType.Values | Where-Object { $_.StartsWith('✅') }).Count
    errors = @($errors)
}
$report | ConvertTo-Json -Depth 4
if ($errors.Count -gt 0) { exit 1 }
