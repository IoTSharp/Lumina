using System.ComponentModel;

namespace Lumina.Forms;

/// <summary>
/// Identifies the component and property responsible for a layout request.
/// </summary>
public sealed class LayoutEventArgs : EventArgs
{
    private readonly WeakReference<IComponent>? _affectedComponent;

    public LayoutEventArgs(IComponent? affectedComponent, string? affectedProperty)
    {
        _affectedComponent = affectedComponent is null ? null : new(affectedComponent);
        AffectedProperty = affectedProperty;
    }

    public LayoutEventArgs(Control? affectedControl, string? affectedProperty)
        : this((IComponent?)affectedControl, affectedProperty)
    {
    }

    public IComponent? AffectedComponent
        => _affectedComponent is not null && _affectedComponent.TryGetTarget(out IComponent? component) ? component : null;

    public Control? AffectedControl => AffectedComponent as Control;

    public string? AffectedProperty { get; }
}
