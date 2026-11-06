# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### Added

- Batch 044: standard TextBoxBase.HideSelection notifications and native EDIT selection style, plus TextBox.TextAlign notifications, handle recreation and creation alignment styles. Horizontal scroll bars now also require logical Left alignment. HorizontalAlignment is corrected to Left=0, Right=1, Center=2; existing binaries and persisted integer values 1/2 need review. RichTextBox keeps its existing separate HideSelection storage through an explicit new declaration.
- Batch 043: standard AcceptsTabChanged notifications and WordWrap handle recreation/cache invalidation/conditional parent layout, plus validated TextBox.ScrollBars with native multiline scroll styles. WordWrap now changes actual EDIT wrapping, and ScrollBars.None no longer forces a vertical scroll bar. Other alignment, preferred-size and derived backend work remains open.
- Batch 042: the standard abstract TextBoxBase now owns the existing shared EDIT text, selection, multiline/read-only, keyboard and handle-state behavior; TextBox directly inherits it and keeps its password/Return rules and legacy SelectionChanged extension. RichTextBox/MaskedTextBox direct inheritance, complete editing APIs and native backends remain open.
- Batch 041: standard Application.OleRequired with one cached OLE initialization per thread, balanced owner-thread release on explicit ExitThread or the outer message-loop exit, and the required InputLanguage Current getter/setter precondition. Existing STA/OLE references and external MTA initialization remain owned by their callers. Other failed HRESULTs propagate instead of being reported as STA; full thread-context and modal-host lifetimes remain open.
- Batch 040: native thread keyboard-layout services through the standard InputLanguage/InputLanguageCollection and Application.CurrentInputLanguage APIs, plus standard Form input-language event contracts and descendant Control notification/cancellation. The fixed WinForms OLE initialization side effect and full modal/IME behavior remain open; MUI buffer growth is bounded to one retry with native-failure fallback.
- Batch 039: TextBox read-only/password IME restrictions, effective mode with preserved configured mode, native password character readback and system-password handle recreation, standard ReadOnlyChanged, and bounded Control input-language message/non-Form host propagation. Form input-language events, the public InputLanguage service and modal propagation remain open.
- Batch 038: standard effective ImeMode contracts and bounded IMM context ownership/readback, TextBox Multiline handle recreation with pending selection and native modified-state preservation, and UserControl AutoSize declarations and event forwarding. Full IME input, TextBoxBase and AutoSize remain open.
- Batch 037: bounded IME character dispatch and duplicate KeyPress suppression, TextBox input-key/dialog routing using existing AcceptsReturn/AcceptsTab settings, and UserControl native focus entry with nested validation/navigation coverage. Full IME composition, TextBoxBase inheritance and AutoSize remain open.
- Batch 036: native `WM_GETDLGCODE` input-key and input-character classification, `WM_SYSCHAR` dialog routing, bounded queued character suppression for `SuppressKeyPress`, validation-cancellation cleanup across parent controls, and Form default-button disposal cleanup. Button native-click validation was audited against WinForms 10.0.11 and covered by real HWND regressions without duplicating validation in `Button.cs`.
- Batch 035: queued keyboard messages now call Control preprocessing before translation/dispatch. Forms route Enter/Escape through standard accept/cancel button contracts, and Button consumes validation policy and writes DialogResult before Click. ButtonBase and modal-loop compatibility remain open.
- Batch 034: key-event dispatch now honors parent preview and Handled, nested containers yield Tab at their boundaries, forms support KeyPreview and top-level Tab wrapping, and UserControl gains bounded default-size and border-style behavior.
- Initial bounded FND-019 keyboard pipeline: `KeyDown`, `KeyPress`, and `KeyUp` event contracts plus Win32 key-message dispatch in `Control`.
- Bounded foundation/inheritance slices: `RightToLeft` inheritance and Win32 RTL styles, `ScrollableControl` auto-scroll state and protected contracts, `ContainerControl.ParentForm`/`AutoScaleFactor`, and the standard `UserControl : ContainerControl` chain.
- Panel now consumes the shared `ScrollableControl` chain; `UserControl.Load` has a one-time creation lifecycle; Windows scroll messages update the shared scroll state and native SCROLLINFO within the bounded batch scope.
- Bounded keyboard and scrolling slices: `Control` now exposes the standard PreviewKeyDown and preprocessing chain, `ContainerControl` routes Tab/Shift+Tab and descendant mnemonics, and `ScrollableControl.ScrollControlIntoView` accounts for existing offsets in nested content.

### Verification

- Batch 044: Release fixed dual-target builds pass with 0 errors and 2 existing NU1510 warnings; final 42 new, 2,356 related and 4,737 full-suite tests pass with no failures/skips. The first new run passed 38/42: four RTL native fixtures lacked the existing Control creation-time style resubmission. Original receipts are retained; the corrected fixture verifies that pipeline without claiming full RTL/mirroring or visual parity. Designer, Native AOT, physical devices and platform acceptance were not run.
- Batch 043: the first Release fixed dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; all 41 new, 359 related and 4,695 full-suite tests pass without failures or skips. Independent native EDIT comparisons cover 16 style combinations and native line counts verify wrapping. Designer, Native AOT, physical devices and platform acceptance were not run.
- Batch 042: Release fixed dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 19 new tests, 318 related tests and all 4,654 tests pass with no failures or skips. Eleven message tests verify native IME default dispatch and existing character state without changing production Control code. Full composition/candidate UI, modal hosts, designer, Native AOT and platform acceptance remain open.
- Batch 041: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 18 new tests, 148 related tests and all 4,635 tests pass. The first 9 internal-service fixture failures assumed CLR threads had no COM initialization; original receipts are retained and only that fixture was corrected. Native OLE reference probes and real nested message loops verify the bounded lifetime; full host-context, physical IME, designer, Native AOT and platform acceptance remain open.
- Batch 040: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 27 new tests, 96 related IME/focus tests and all 4,617 tests pass with no failures or skips. Native HKL readback, available-layout setters/default reset, registry MUI names, thread isolation and real HWND notifications/cancellation are covered. Physical language switching/composition, OLE lifetime, designer, Native AOT and platform acceptance remain open.
- Batch 039: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 22 new tests, a 50-test focus/IME regression set and all 4,590 tests pass. Original compilation/fixture failures are retained; native multiline EDIT readback is compared with an independent native control, and the historical no-handle focus fixture now isolates its uninitialized propagation cache. Physical IME, designer, Native AOT and platform acceptance were not run.
- Batch 038: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 32 new tests and all 4,568 tests pass. Initial uint tuple inference compilation errors were corrected, and simultaneous handle-notification/IME-release failures retain both errors. Physical IME input, designer, Native AOT and platform acceptance were not run.
- Batch 037: Release dual-target solution build passes with 0 errors and 2 existing NU1510 warnings; 22 new tests and all 4,536 tests pass. Physical input, designer, Native AOT and platform acceptance were not run.
- Batch 036: Release solution build passes with 0 errors and 2 existing NU1510 warnings; 9 new tests and all 4,514 tests pass. Existing keyboard queue/event regressions add 14 passing tests. Fixed 10.0.11 API inventory, designer, Native AOT, physical input and platform acceptance remain open.
- Batch 035: Release solution build passes with 0 errors and 2 existing NU1510 warnings; 37 new tests and all 4,505 tests pass. Fixed 10.0.11 API inventory remains blocked; designer, Native AOT, physical input and platform acceptance were not run. Initial missing Keys.Return compilation and malformed KeyUp fixture failures are recorded in the batch notes.
- Batch 034: Release solution build passes with 0 errors and 2 existing NU1510 warnings; 29 new tests and the full 4,468-test suite pass. Fixed 10.0.11 API inventory remains blocked by the missing reference pack; AutoSizeMode numeric identity, full input/dialog behavior, AOT, designer and platform acceptance remain open.
- Release solution build and the full `Lumina.Tests` suite pass for the 033 scope (4,439 tests); complete command/dialog routing, IME, physical keyboard/wheel/touch input, Cursor, Native AOT, designer, and cross-platform gates remain open.

### Changed
- Correct AutoSizeMode numeric identity to GrowAndShrink=0 and GrowOnly=1 while preserving the existing named GrowOnly default. Previously compiled enum constants and persisted numeric 0/1 values require review; source using enum names retains the intended named behavior after recompilation.

### Planned
- DLL injection into `dwm.exe`
- SystemBackdrop / AccentBlur / CustomBlur effect implementations
- Win32 native GUI with Direct2D
- XML config system
- Multi-language support

---

## [0.1.0] — 2026-03-25

### Added
- Initial project structure: `Lumina.App` (AOT WinExe) + `Lumina.Ext` (AOT Shared DLL)
- Solution file (`Lumina.slnx`)
- Native AOT configuration for both projects (net10.0-windows, x64)
- `InlineHook.cs` — x64 inline hook engine using VirtualProtect trampoline, no minhook dependency
- `NativeMethods.cs` — Win32 and DWM P/Invoke definitions (`dwmapi.dll`, `kernel32.dll`, `user32.dll`)
- `OsVersion.cs` — Windows build number detection helper
- `ExtMain.cs` — Native DLL entry point with `[UnmanagedCallersOnly]` DllMain export
- `.gitignore` based on Visual Studio template with AOT `native/` entry
- `README.md`, `ROADMAP.md`, `CHANGELOG.md`
