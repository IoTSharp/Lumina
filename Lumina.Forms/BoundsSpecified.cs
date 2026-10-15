namespace Lumina.Forms;

/// <summary>
/// Specifies which position and size components are supplied when setting control bounds.
/// </summary>
[Flags]
public enum BoundsSpecified
{
    /// <summary>No bounds components are supplied.</summary>
    None = 0,
    /// <summary>The left position is supplied.</summary>
    X = 1,
    /// <summary>The top position is supplied.</summary>
    Y = 2,
    /// <summary>Both position components are supplied.</summary>
    Location = X | Y,
    /// <summary>The width is supplied.</summary>
    Width = 4,
    /// <summary>The height is supplied.</summary>
    Height = 8,
    /// <summary>Both size components are supplied.</summary>
    Size = Width | Height,
    /// <summary>All position and size components are supplied.</summary>
    All = Location | Size
}
