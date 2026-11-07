// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

namespace Lumina.Forms;

/// <summary>Specifies the display and layout information for text strings.</summary>
[Flags]
public enum TextFormatFlags
{
    /// <summary>Aligns text to the bottom.</summary>
    Bottom = 0x0000_0008,
    /// <summary>Replaces the end of clipped text with an ellipsis.</summary>
    EndEllipsis = 0x0000_8000,
    /// <summary>Expands tab characters.</summary>
    ExpandTabs = 0x0000_0040,
    /// <summary>Includes the font's external leading in line height.</summary>
    ExternalLeading = 0x0000_0200,
    /// <summary>Uses the default formatting.</summary>
    Default = 0,
    /// <summary>Hides mnemonic underlines.</summary>
    HidePrefix = 0x0010_0000,
    /// <summary>Centers text horizontally.</summary>
    HorizontalCenter = 0x0000_0001,
    /// <summary>Uses the system font to calculate text metrics.</summary>
    Internal = 0x0000_1000,
    /// <summary>Aligns text to the left.</summary>
    Left = 0,
    /// <summary>Allows the native formatter to modify the string.</summary>
    [Obsolete("ModifyString mutates strings and should be avoided. It will be blocked in a future release.")]
    ModifyString = 0x0001_0000,
    /// <summary>Draws without clipping.</summary>
    NoClipping = 0x0000_0100,
    /// <summary>Treats ampersands as ordinary characters.</summary>
    NoPrefix = 0x0000_0800,
    /// <summary>Prevents line breaks between full-width characters.</summary>
    NoFullWidthCharacterBreak = 0x0008_0000,
    /// <summary>Replaces the middle of a path with an ellipsis.</summary>
    PathEllipsis = 0x0000_4000,
    /// <summary>Draws only mnemonic underlines.</summary>
    PrefixOnly = 0x0020_0000,
    /// <summary>Aligns text to the right.</summary>
    Right = 0x0000_0002,
    /// <summary>Uses right-to-left reading order.</summary>
    RightToLeft = 0x0002_0000,
    /// <summary>Formats text as a single line.</summary>
    SingleLine = 0x0000_0020,
    /// <summary>Uses text-box line formatting.</summary>
    TextBoxControl = 0x0000_2000,
    /// <summary>Aligns text to the top.</summary>
    Top = 0,
    /// <summary>Centers text vertically.</summary>
    VerticalCenter = 0x0000_0004,
    /// <summary>Wraps text at word boundaries.</summary>
    WordBreak = 0x0000_0010,
    /// <summary>Trims text at a word boundary and appends an ellipsis.</summary>
    WordEllipsis = 0x0004_0000,
    /// <summary>Preserves a Graphics object's clipping region.</summary>
    PreserveGraphicsClipping = 0x0100_0000,
    /// <summary>Preserves a Graphics object's translation.</summary>
    PreserveGraphicsTranslateTransform = 0x0200_0000,
    /// <summary>Adds padding for overhanging glyphs.</summary>
    GlyphOverhangPadding = 0,
    /// <summary>Omits glyph-overhang padding.</summary>
    NoPadding = 0x1000_0000,
    /// <summary>Adds extra padding to both sides of the text.</summary>
    LeftAndRightPadding = 0x2000_0000
}
