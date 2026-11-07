// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Values follow dotnet/winforms v10.0.11.

namespace Lumina.Forms;

/// <summary>Specifies the scroll bars displayed by a RichTextBox.</summary>
public enum RichTextBoxScrollBars
{
    None = 0,
    Horizontal = 1,
    Vertical = 2,
    Both = Horizontal | Vertical,
    ForcedHorizontal = 16 | Horizontal,
    ForcedVertical = 16 | Vertical,
    ForcedBoth = ForcedHorizontal | ForcedVertical,
}
