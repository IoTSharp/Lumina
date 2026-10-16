namespace Lumina.Forms;

/// <summary>
/// Preserves the existing container base while sharing the standard control tree.
/// </summary>
public abstract class ContainerControlBase : Control
{
    /// <inheritdoc />
    public override void PerformLayout()
    {
        foreach (Control child in ChildControls)
        {
            child.PerformLayout();
        }

        base.PerformLayout();
    }
}