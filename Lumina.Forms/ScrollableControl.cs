// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Provides a client-area container for scrollable controls.
/// </summary>
public class ScrollableControl : Control
{
    /// <summary>Initializes a scrollable container whose descendants participate in validation.</summary>
    public ScrollableControl()
    {
        SetStyle(ControlStyles.ContainerControl, true);
    }

    /// <inheritdoc />
    public override Rectangle DisplayRectangle => new(
        Padding.Left,
        Padding.Top,
        Math.Max(0, ClientSize.Width - Padding.Horizontal),
        Math.Max(0, ClientSize.Height - Padding.Vertical));

    /// <inheritdoc />
    protected override void OnVisibleChanged(EventArgs e)
    {
        if (Visible)
        {
            RequestLayout(this, nameof(Visible));
        }

        base.OnVisibleChanged(e);
    }
}
