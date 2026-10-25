// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Represents a simple child-control container panel.
/// </summary>
public class Panel : ScrollableControl
{
    /// <summary>
    /// Initializes a panel whose preferred size is measured from its children.
    /// </summary>
    public Panel()
    {
        SetStyle(ControlStyles.ContainerControl, true);
        CachePreferredSize = true;
    }

    internal override Size GetPreferredSizeCore(Size proposedSize)
    {
        Size borderSize = SizeFromClientSize(Size.Empty);
        Size totalPadding = borderSize + new Size(Padding.Horizontal, Padding.Vertical);
        return LayoutEngine.GetPreferredSize(this, proposedSize - totalPadding) + totalPadding;
    }

    /// <inheritdoc />
    protected override string ClassName => "STATIC";

    /// <inheritdoc />
    protected override uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_CLIPCHILDREN | Win32.WS_CLIPSIBLINGS;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultBackgroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultForegroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        ApplyNativeThemeState();
    }

    /// <inheritdoc />
    protected override void ApplyTheme()
    {
        ApplyNativeThemeState();
    }

    private void ApplyNativeThemeState()
    {
        if (NativeHandle == 0)
        {
            return;
        }

        DarkModeNative.ApplyThemeToWindow(NativeHandle, CurrentVisualStyle.IsDarkMode);
    }
}
