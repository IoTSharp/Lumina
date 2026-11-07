// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

namespace Lumina.Forms;

/// <summary>Specifies the kinds of content in a rich text box selection.</summary>
[Flags]
public enum RichTextBoxSelectionTypes
{
    /// <summary>The selection is empty.</summary>
    Empty = 0,
    /// <summary>The selection contains text.</summary>
    Text = 1,
    /// <summary>The selection contains an OLE object.</summary>
    Object = 2,
    /// <summary>The selection contains more than one character.</summary>
    MultiChar = 4,
    /// <summary>The selection contains more than one OLE object.</summary>
    MultiObject = 8,
}
