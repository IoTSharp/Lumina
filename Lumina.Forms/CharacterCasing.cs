// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

namespace Lumina.Forms;

/// <summary>Specifies the case of characters entered into a text box.</summary>
public enum CharacterCasing
{
    /// <summary>Leaves character case unchanged.</summary>
    Normal = 0,

    /// <summary>Converts characters to uppercase.</summary>
    Upper = 1,

    /// <summary>Converts characters to lowercase.</summary>
    Lower = 2
}
