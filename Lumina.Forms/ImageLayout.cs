// Adapted from dotnet/winforms v10.0.11.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Lumina.Forms;

/// <summary>Specifies how an image is positioned in a control's client area.</summary>
public enum ImageLayout
{
    /// <summary>Places the image at the top left.</summary>
    None,
    /// <summary>Repeats the image across the client area.</summary>
    Tile,
    /// <summary>Centers the image in the client area.</summary>
    Center,
    /// <summary>Stretches the image to fill the client area.</summary>
    Stretch,
    /// <summary>Fits the image while preserving its aspect ratio.</summary>
    Zoom,
}
