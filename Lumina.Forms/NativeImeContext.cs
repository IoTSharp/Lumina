// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Conversion tables and mode bits adapted from dotnet/winforms v10.0.11 Control.Ime.cs.

namespace Lumina.Forms;

internal enum ImeLanguage { Unsupported, Chinese, Japanese, Korean }

/// <summary>Maps WinForms IME modes to IMM while tracking contexts created by this control.</summary>
internal sealed class NativeImeContext
{
    private const uint Native = 0x0001;
    private const uint Katakana = 0x0002;
    private const uint FullShape = 0x0008;
    private nint _savedContext;
    private nint _ownedContext;

    internal static ImeLanguage CurrentLanguage => !OperatingSystem.IsWindows() ? ImeLanguage.Unsupported
        : ((nuint)Win32.GetKeyboardLayout(0) & 0xffff) switch
        {
            0x0404 or 0x0804 or 0x0c04 or 0x1004 or 0x1404 => ImeLanguage.Chinese,
            0x0411 => ImeLanguage.Japanese,
            0x0412 or 0x0812 => ImeLanguage.Korean,
            _ => ImeLanguage.Unsupported
        };

    internal static ImeMode GetImeMode(nint window, ImeLanguage language)
    {
        if (window == 0 || language == ImeLanguage.Unsupported) { return ImeMode.Inherit; }
        nint context = Win32.ImmGetContext(window);
        if (context == 0) { return ImeMode.Disable; }
        try
        {
            if (!Win32.ImmGetOpenStatus(context))
            {
                return language switch
                {
                    ImeLanguage.Japanese => ImeMode.Off,
                    ImeLanguage.Korean => ImeMode.Alpha,
                    _ => ImeMode.Close
                };
            }
            if (!Win32.ImmGetConversionStatus(context, out uint conversion, out _)) { return ImeMode.Inherit; }
            return ModeFromConversion(language, conversion);
        }
        finally { _ = Win32.ImmReleaseContext(window, context); }
    }

    private static ImeMode ModeFromConversion(ImeLanguage language, uint conversion)
    {
        bool native = (conversion & Native) != 0;
        bool full = (conversion & FullShape) != 0;
        return language switch
        {
            ImeLanguage.Chinese => native ? full ? ImeMode.On : ImeMode.OnHalf : ImeMode.Off,
            ImeLanguage.Korean => native ? full ? ImeMode.HangulFull : ImeMode.Hangul
                : full ? ImeMode.AlphaFull : ImeMode.Alpha,
            ImeLanguage.Japanese => native
                ? (conversion & Katakana) != 0 ? full ? ImeMode.Katakana : ImeMode.KatakanaHalf : ImeMode.Hiragana
                : full ? ImeMode.AlphaFull : ImeMode.Alpha,
            _ => ImeMode.Inherit
        };
    }

    internal void SetImeStatus(ImeMode mode, nint window, ImeLanguage language)
    {
        if (window == 0 || language == ImeLanguage.Unsupported || mode is ImeMode.Inherit or ImeMode.NoControl) { return; }
        if (mode == ImeMode.Disable)
        {
            SetOpenStatus(window, false);
            nint previous = Win32.ImmAssociateContext(window, 0);
            if (previous != 0) { _savedContext = previous; }
            return;
        }

        Enable(window);
        mode = mode switch
        {
            ImeMode.On => ImeMode.Hiragana,
            ImeMode.Off => language == ImeLanguage.Japanese ? ImeMode.Close : ImeMode.Alpha,
            ImeMode.Close when language == ImeLanguage.Korean => ImeMode.Alpha,
            _ => mode
        };
        if (mode == ImeMode.Close)
        {
            SetOpenStatus(window, false);
            return;
        }

        (uint set, uint clear) = mode switch
        {
            ImeMode.Hiragana => (FullShape | Native, Katakana),
            ImeMode.Katakana => (FullShape | Katakana | Native, 0u),
            ImeMode.KatakanaHalf => (Katakana | Native, FullShape),
            ImeMode.AlphaFull => (FullShape, Katakana | Native),
            ImeMode.Alpha => (0u, FullShape | Katakana | Native),
            ImeMode.HangulFull => (FullShape | Native, 0u),
            ImeMode.Hangul => (Native, FullShape),
            ImeMode.OnHalf => (Native, Katakana | FullShape),
            _ => (0u, 0u)
        };
        nint context = Win32.ImmGetContext(window);
        if (context == 0) { return; }
        try
        {
            if (Win32.ImmGetConversionStatus(context, out uint conversion, out uint sentence))
            {
                _ = Win32.ImmSetConversionStatus(context, (conversion | set) & ~clear, sentence);
            }
        }
        finally { _ = Win32.ImmReleaseContext(window, context); }
    }

    private void Enable(nint window)
    {
        nint context = Win32.ImmGetContext(window);
        if (context != 0) { _ = Win32.ImmReleaseContext(window, context); }
        else
        {
            nint restore = _savedContext;
            if (restore == 0)
            {
                restore = _ownedContext;
                if (restore == 0)
                {
                    restore = Win32.ImmCreateContext();
                    if (restore == 0) { return; }
                    _ownedContext = restore;
                }
            }
            _ = Win32.ImmAssociateContext(window, restore);
        }
        SetOpenStatus(window, true);
    }

    private static void SetOpenStatus(nint window, bool open)
    {
        nint context = Win32.ImmGetContext(window);
        if (context == 0) { return; }
        try { _ = Win32.ImmSetOpenStatus(context, open); }
        finally { _ = Win32.ImmReleaseContext(window, context); }
    }

    internal void Release(nint window)
    {
        if (_ownedContext != 0)
        {
            nint current = Win32.ImmGetContext(window);
            if (current != 0) { _ = Win32.ImmReleaseContext(window, current); }
            if (current == _ownedContext) { _ = Win32.ImmAssociateContext(window, 0); }
            if (!Win32.ImmDestroyContext(_ownedContext))
            {
                // IMM does not document a GetLastError value for this call.
                // Preserve the owned handle so the failed release can be retried.
                throw new InvalidOperationException("The input method context could not be destroyed.");
            }
            _ownedContext = 0;
        }
        // Detached contexts supplied by Windows are borrowed and must not be destroyed here.
        _savedContext = 0;
    }
}
