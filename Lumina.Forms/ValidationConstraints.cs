// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Lumina.Forms;

/// <summary>Specifies which controls are included by <see cref="ContainerControl.ValidateChildren()"/>.</summary>
[Flags]
public enum ValidationConstraints
{
    /// <summary>Validates all child controls that cause validation.</summary>
    None = 0,
    /// <summary>Includes controls with the Selectable style.</summary>
    Selectable = 1,
    /// <summary>Includes enabled controls.</summary>
    Enabled = 2,
    /// <summary>Includes visible controls.</summary>
    Visible = 4,
    /// <summary>Includes controls with TabStop enabled.</summary>
    TabStop = 8,
    /// <summary>Excludes descendants below the immediate children.</summary>
    ImmediateChildren = 16,
}
