namespace Lumina.Forms;

/// <summary>
/// Specifies whether a control lays out its content from right to left.
/// </summary>
public enum RightToLeft
{
    /// <summary>Use the value supplied by the parent control.</summary>
    Inherit = 0,

    /// <summary>Use the normal left-to-right layout.</summary>
    No = 1,

    /// <summary>Use the right-to-left layout.</summary>
    Yes = 2,
}
