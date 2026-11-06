// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Values adapted from dotnet/winforms v10.0.11, Input/IMEMode.cs.

namespace Lumina.Forms;

/// <summary>Specifies the input method editor mode used when a control receives focus.</summary>
public enum ImeMode
{
    Inherit = -1,
    NoControl = 0,
    On = 1,
    Off = 2,
    Disable = 3,
    Hiragana = 4,
    Katakana = 5,
    KatakanaHalf = 6,
    AlphaFull = 7,
    Alpha = 8,
    HangulFull = 9,
    Hangul = 10,
    Close = 11,
    OnHalf = 12
}
