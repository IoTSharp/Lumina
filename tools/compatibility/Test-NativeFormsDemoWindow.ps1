#requires -Version 7.0
param(
    [Parameter(Mandatory)][string]$ExecutablePath,
    [Parameter(Mandatory)][string]$ArtifactsDirectory,
    [ValidateRange(1, 60)][int]$StartupTimeoutSeconds = 30,
    [ValidateRange(1, 60)][int]$ExitTimeoutSeconds = 15,
    [ValidateRange(0, 300)][int]$HoldSeconds = 0
)

# This verifies one Demo checkbox interaction and normal window/process shutdown.
# It does not establish full Control, Form, designer, or FND-004 acceptance.
# HoldSeconds leaves the discovered window available for an external screenshot,
# then continues the same interaction and automatic close. No screenshot is taken.
$ErrorActionPreference = 'Stop'
if (-not $IsWindows) { throw 'This probe requires Windows and an interactive desktop.' }
$demoExecutablePath = (Resolve-Path -LiteralPath $ExecutablePath).Path
if (-not [IO.File]::Exists($demoExecutablePath)) { throw 'ExecutablePath must name a file.' }
$demoArtifactsPath = [IO.Path]::GetFullPath($ArtifactsDirectory)
[IO.Directory]::CreateDirectory($demoArtifactsPath) | Out-Null
$demoUtf8 = [Text.UTF8Encoding]::new($false)
[Console]::OutputEncoding = $demoUtf8
$demoEvidencePath = Join-Path $demoArtifactsPath 'demo-window.json'
$demoProcess = $null
$demoOutputTask = $null
$demoErrorTask = $null
$demoFailure = $null
$demoMainWindow = $null
$demoObservedWindows = [Collections.Generic.Dictionary[long, object]]::new()
$demoEvidence = [ordered]@{
    scope = 'Windows NativeFormsDemo main-window Enable option checkbox and normal shutdown only'
    result = 'pending'
    stage = 'preparing'
    executablePath = $demoExecutablePath
    executableSha256 = (Get-FileHash -LiteralPath $demoExecutablePath -Algorithm SHA256).Hash
    startedUtc = [DateTimeOffset]::UtcNow.ToString('O')
    completedUtc = $null
    processId = $null
    holdSeconds = $HoldSeconds
    mainWindow = $null
    childWindows = @()
    checkbox = $null
    checkBefore = $null
    clickMessage = 'BM_CLICK (0x00F5)'
    clickReturn = $null
    checkAfter = $null
    toggled = $false
    closePosted = $false
    exited = $false
    exitCode = $null
    originalWindowsAfterExit = @()
    allOriginalWindowsDestroyed = $false
    forcedCleanup = $false
    error = $null
    diagnostics = $null
    cleanupError = $null
}

function Save-DemoWindowEvidence {
    [IO.File]::WriteAllText($demoEvidencePath, ($demoEvidence | ConvertTo-Json -Depth 10), $demoUtf8)
}

try {
    if (-not ('LuminaCompatibility.DemoWindowNative' -as [type])) {
        Add-Type -TypeDefinition @'
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace LuminaCompatibility
{
    public sealed class DemoWindowInfo
    {
        public long HandleValue { get; set; }
        public string Handle { get; set; }
        public uint ProcessId { get; set; }
        public uint ThreadId { get; set; }
        public long ParentValue { get; set; }
        public string Parent { get; set; }
        public string Caption { get; set; }
        public string ClassName { get; set; }
        public bool Exists { get; set; }
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public int ControlId { get; set; }
        public int Style { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }

    public static class DemoWindowNative
    {
        private delegate bool EnumWindowCallback(IntPtr window, IntPtr parameter);

        public static DemoWindowInfo[] TopLevelWindows(uint processId)
        {
            var result = new List<DemoWindowInfo>();
            EnumWindowCallback callback = (window, parameter) =>
            {
                GetWindowThreadProcessId(window, out uint owner);
                if (owner == processId) result.Add(Inspect(window.ToInt64()));
                return true;
            };
            if (!EnumWindows(callback, IntPtr.Zero)) throw new Win32Exception(Marshal.GetLastWin32Error());
            GC.KeepAlive(callback);
            return result.ToArray();
        }

        public static DemoWindowInfo[] ChildWindows(long handle, uint processId)
        {
            IntPtr parent = RequireOwned(handle, processId);
            var result = new List<DemoWindowInfo>();
            EnumWindowCallback callback = (window, parameter) =>
            {
                GetWindowThreadProcessId(window, out uint owner);
                if (owner == processId) result.Add(Inspect(window.ToInt64()));
                return true;
            };
            EnumChildWindows(parent, callback, IntPtr.Zero);
            GC.KeepAlive(callback);
            return result.ToArray();
        }

        public static DemoWindowInfo Inspect(long handle)
        {
            IntPtr window = new IntPtr(handle);
            uint threadId = GetWindowThreadProcessId(window, out uint processId);
            IntPtr parent = GetParent(window);
            var caption = new StringBuilder(4096);
            var className = new StringBuilder(256);
            GetWindowTextW(window, caption, caption.Capacity);
            GetClassNameW(window, className, className.Capacity);
            GetWindowRect(window, out NativeRect bounds);
            return new DemoWindowInfo
            {
                HandleValue = handle, Handle = "0x" + handle.ToString("X"),
                ProcessId = processId, ThreadId = threadId,
                ParentValue = parent.ToInt64(), Parent = "0x" + parent.ToInt64().ToString("X"),
                Caption = caption.ToString(), ClassName = className.ToString(),
                Exists = IsWindow(window), Visible = IsWindowVisible(window), Enabled = IsWindowEnabled(window),
                ControlId = GetDlgCtrlID(window), Style = GetWindowLongW(window, -16),
                Left = bounds.Left, Top = bounds.Top, Right = bounds.Right, Bottom = bounds.Bottom
            };
        }

        public static bool IsCheckbox(DemoWindowInfo window)
        {
            bool button = window.ClassName.Equals("BUTTON", StringComparison.OrdinalIgnoreCase)
                || window.ClassName.StartsWith("WindowsForms10.BUTTON.", StringComparison.OrdinalIgnoreCase);
            int kind = window.Style & 0xF;
            return button && (kind == 2 || kind == 3 || kind == 5 || kind == 6)
                && window.Caption.Equals("Enable option", StringComparison.Ordinal)
                && window.Visible && window.Enabled;
        }

        public static long CheckboxMessage(long parentHandle, long checkboxHandle, uint processId, uint message)
        {
            IntPtr parent = RequireOwned(parentHandle, processId);
            IntPtr checkbox = RequireOwned(checkboxHandle, processId);
            if (!IsChild(parent, checkbox) || !IsCheckbox(Inspect(checkboxHandle)))
                throw new InvalidOperationException("The selected checkbox is no longer an enabled visible child of this process's main window.");
            if (message != 0x00F0 && message != 0x00F5)
                throw new ArgumentOutOfRangeException(nameof(message));
            if (SendMessageTimeoutW(checkbox, message, IntPtr.Zero, IntPtr.Zero, 0x23, 3000, out IntPtr result) == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Checkbox message failed or timed out.");
            return result.ToInt64();
        }

        public static void CloseOwnedWindow(long handle, uint processId)
        {
            IntPtr window = RequireOwned(handle, processId);
            if (!PostMessageW(window, 0x0010, IntPtr.Zero, IntPtr.Zero))
                throw new Win32Exception(Marshal.GetLastWin32Error(), "WM_CLOSE could not be posted.");
        }

        private static IntPtr RequireOwned(long handle, uint processId)
        {
            IntPtr window = new IntPtr(handle);
            GetWindowThreadProcessId(window, out uint owner);
            if (!IsWindow(window) || owner != processId)
                throw new InvalidOperationException("Window ownership changed; no message was sent.");
            return window;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NativeRect { public int Left, Top, Right, Bottom; }
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowCallback callback, IntPtr parameter);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr parent, EnumWindowCallback callback, IntPtr parameter);
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr window, out uint processId);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextW(IntPtr window, StringBuilder text, int maximum);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetClassNameW(IntPtr window, StringBuilder text, int maximum);
        [DllImport("user32.dll")]
        private static extern int GetDlgCtrlID(IntPtr window);
        [DllImport("user32.dll")]
        private static extern int GetWindowLongW(IntPtr window, int index);
        [DllImport("user32.dll")]
        private static extern IntPtr GetParent(IntPtr window);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindow(IntPtr window);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr window);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowEnabled(IntPtr window);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsChild(IntPtr parent, IntPtr child);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr window, out NativeRect bounds);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessageTimeoutW(IntPtr window, uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, out IntPtr result);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessageW(IntPtr window, uint message, IntPtr wParam, IntPtr lParam);
    }
}
'@
    }
    $demoEvidence.stage = 'starting'
    Save-DemoWindowEvidence
    $demoStart = [Diagnostics.ProcessStartInfo]::new()
    $demoStart.FileName = $demoExecutablePath
    $demoStart.WorkingDirectory = [IO.Path]::GetDirectoryName($demoExecutablePath)
    $demoStart.UseShellExecute = $false
    $demoStart.CreateNoWindow = $true
    $demoStart.RedirectStandardOutput = $true
    $demoStart.RedirectStandardError = $true
    $demoStart.StandardOutputEncoding = $demoUtf8
    $demoStart.StandardErrorEncoding = $demoUtf8
    $demoProcess = [Diagnostics.Process]::Start($demoStart)
    $demoEvidence.processId = $demoProcess.Id
    $demoOutputTask = $demoProcess.StandardOutput.ReadToEndAsync()
    $demoErrorTask = $demoProcess.StandardError.ReadToEndAsync()
    $demoEvidence.stage = 'discovering-main-window'
    Save-DemoWindowEvidence
    $demoStartupWatch = [Diagnostics.Stopwatch]::StartNew()
    $demoCheckbox = $null
    while ($demoStartupWatch.Elapsed.TotalSeconds -lt $StartupTimeoutSeconds) {
        if ($demoProcess.HasExited) { throw "Demo exited before its main window was ready: $($demoProcess.ExitCode)." }
        $demoTopWindows = @([LuminaCompatibility.DemoWindowNative]::TopLevelWindows([uint32]$demoProcess.Id))
        foreach ($demoCandidate in $demoTopWindows) {
            if (-not $demoCandidate.Visible) { continue }
            $demoChildren = @([LuminaCompatibility.DemoWindowNative]::ChildWindows($demoCandidate.HandleValue, [uint32]$demoProcess.Id))
            $demoCheckboxes = @($demoChildren | Where-Object { [LuminaCompatibility.DemoWindowNative]::IsCheckbox($_) })
            if ($demoCheckboxes.Count -gt 1) { throw 'Multiple enabled visible Enable option checkboxes were found; target selection is ambiguous.' }
            if ($demoCheckboxes.Count -eq 1) {
                $demoMainWindow = $demoCandidate
                $demoCheckbox = $demoCheckboxes[0]
                $demoEvidence.mainWindow = $demoMainWindow
                $demoEvidence.childWindows = $demoChildren
                $demoEvidence.checkbox = $demoCheckbox
                $demoObservedWindows[$demoMainWindow.HandleValue] = $demoMainWindow
                foreach ($demoChild in $demoChildren) { $demoObservedWindows[$demoChild.HandleValue] = $demoChild }
                break
            }
        }
        if ($null -ne $demoMainWindow) { break }
        Start-Sleep -Milliseconds 100
    }
    if ($null -eq $demoMainWindow) { throw 'Timed out finding a visible main window with the enabled Enable option BUTTON checkbox.' }
    $demoEvidence.stage = 'ready'
    Save-DemoWindowEvidence
    Write-Output "Demo window ready: process=$($demoProcess.Id), handle=$($demoMainWindow.Handle), evidence=$demoEvidencePath"
    if ($HoldSeconds -gt 0) {
        $demoHoldWatch = [Diagnostics.Stopwatch]::StartNew()
        while ($demoHoldWatch.Elapsed.TotalSeconds -lt $HoldSeconds) {
            if ($demoProcess.HasExited) { throw 'Demo exited during the requested screenshot hold period.' }
            Start-Sleep -Milliseconds 100
        }
    }
    $demoEvidence.stage = 'checkbox-interaction'
    $demoEvidence.checkBefore = [LuminaCompatibility.DemoWindowNative]::CheckboxMessage($demoMainWindow.HandleValue, $demoCheckbox.HandleValue, [uint32]$demoProcess.Id, 0x00F0)
    Save-DemoWindowEvidence
    if ($demoEvidence.checkBefore -notin @(0, 1)) { throw "Expected a two-state checkbox; BM_GETCHECK returned $($demoEvidence.checkBefore)." }
    $demoEvidence.clickReturn = [LuminaCompatibility.DemoWindowNative]::CheckboxMessage($demoMainWindow.HandleValue, $demoCheckbox.HandleValue, [uint32]$demoProcess.Id, 0x00F5)
    $demoEvidence.checkAfter = [LuminaCompatibility.DemoWindowNative]::CheckboxMessage($demoMainWindow.HandleValue, $demoCheckbox.HandleValue, [uint32]$demoProcess.Id, 0x00F0)
    $demoEvidence.toggled = $demoEvidence.checkAfter -eq (1 - $demoEvidence.checkBefore)
    Save-DemoWindowEvidence
    if (-not $demoEvidence.toggled) { throw "BM_CLICK did not toggle the checkbox: $($demoEvidence.checkBefore) -> $($demoEvidence.checkAfter)." }
    foreach ($demoChild in [LuminaCompatibility.DemoWindowNative]::ChildWindows($demoMainWindow.HandleValue, [uint32]$demoProcess.Id)) {
        $demoObservedWindows[$demoChild.HandleValue] = $demoChild
    }
    $demoEvidence.stage = 'closing'
    [LuminaCompatibility.DemoWindowNative]::CloseOwnedWindow($demoMainWindow.HandleValue, [uint32]$demoProcess.Id)
    $demoEvidence.closePosted = $true
    Save-DemoWindowEvidence
    $demoEvidence.exited = $demoProcess.WaitForExit($ExitTimeoutSeconds * 1000)
    if (-not $demoEvidence.exited) { throw 'Demo did not exit after WM_CLOSE within the configured timeout.' }
    $demoEvidence.exitCode = $demoProcess.ExitCode
    $demoEvidence.originalWindowsAfterExit = @($demoObservedWindows.Keys | Sort-Object | ForEach-Object { [LuminaCompatibility.DemoWindowNative]::Inspect($_) })
    $demoEvidence.allOriginalWindowsDestroyed = @($demoEvidence.originalWindowsAfterExit | Where-Object Exists).Count -eq 0
    if ($demoEvidence.exitCode -ne 0) { throw "Demo exited abnormally: $($demoEvidence.exitCode)." }
    if (-not $demoEvidence.allOriginalWindowsDestroyed) { throw 'At least one recorded HWND still exists after process exit; inspect its current owner in the evidence.' }
    $demoEvidence.result = 'passed'
    $demoEvidence.stage = 'completed'
}
catch {
    $demoFailure = $_
    $demoEvidence.result = 'failed'
    $demoEvidence.error = $_.Exception.ToString()
    if ($null -ne $demoProcess -and -not $demoProcess.HasExited -and ('LuminaCompatibility.DemoWindowNative' -as [type])) {
        try {
            $demoDiagnosticWindows = @([LuminaCompatibility.DemoWindowNative]::TopLevelWindows([uint32]$demoProcess.Id))
            $demoEvidence.diagnostics = @($demoDiagnosticWindows | ForEach-Object {
                [ordered]@{ window = $_; children = @([LuminaCompatibility.DemoWindowNative]::ChildWindows($_.HandleValue, [uint32]$demoProcess.Id)) }
            })
        }
        catch { $demoEvidence.diagnostics = $_.Exception.ToString() }
    }
}
finally {
    if ($null -ne $demoProcess) {
        try {
            if (-not $demoProcess.HasExited) {
                if ($null -ne $demoMainWindow) {
                    try { [LuminaCompatibility.DemoWindowNative]::CloseOwnedWindow($demoMainWindow.HandleValue, [uint32]$demoProcess.Id) }
                    catch { $demoEvidence.cleanupError = $_.Exception.ToString() }
                }
                if (-not $demoProcess.WaitForExit(2000)) {
                    $demoEvidence.forcedCleanup = $true
                    $demoProcess.Kill()
                    if (-not $demoProcess.WaitForExit(5000)) { throw 'The owned Demo process did not exit after cleanup termination.' }
                }
            }
            $demoEvidence.exited = $demoProcess.HasExited
            if ($demoProcess.HasExited) { $demoEvidence.exitCode = $demoProcess.ExitCode }
            if ($null -ne $demoOutputTask) { [IO.File]::WriteAllText((Join-Path $demoArtifactsPath 'stdout.log'), $demoOutputTask.GetAwaiter().GetResult(), $demoUtf8) }
            if ($null -ne $demoErrorTask) { [IO.File]::WriteAllText((Join-Path $demoArtifactsPath 'stderr.log'), $demoErrorTask.GetAwaiter().GetResult(), $demoUtf8) }
        }
        catch {
            $demoEvidence.cleanupError = $_.Exception.ToString()
            $demoEvidence.result = 'failed'
            if ($null -eq $demoFailure) { $demoFailure = $_ }
        }
        finally { $demoProcess.Dispose() }
    }
    $demoEvidence.completedUtc = [DateTimeOffset]::UtcNow.ToString('O')
    Save-DemoWindowEvidence
}
if ($null -ne $demoFailure) { throw "NativeFormsDemo window probe failed: $($demoFailure.Exception.Message) Evidence: $demoEvidencePath" }
Write-Output "NativeFormsDemo checkbox and normal shutdown passed. Evidence: $demoEvidencePath"
