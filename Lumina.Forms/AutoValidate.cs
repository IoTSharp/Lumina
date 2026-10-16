// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Lumina.Forms;

/// <summary>Specifies how a container handles validation failures during focus changes.</summary>
public enum AutoValidate
{
    /// <summary>Disables automatic validation on focus changes.</summary>
    Disable = 0,
    /// <summary>Validates automatically and keeps focus on an invalid control.</summary>
    EnablePreventFocusChange = 1,
    /// <summary>Validates automatically while allowing focus to leave an invalid control.</summary>
    EnableAllowFocusChange = 2,
    /// <summary>Uses the containing container's validation policy.</summary>
    Inherit = -1,
}
