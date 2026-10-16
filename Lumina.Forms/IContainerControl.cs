namespace Lumina.Forms;

/// <summary>
/// Defines the active child control of a container.
/// </summary>
public interface IContainerControl
{
    Control? ActiveControl { get; set; }

    bool ActivateControl(Control active);
}
