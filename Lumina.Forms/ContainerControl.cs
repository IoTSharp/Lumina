// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Coordinates active controls and automatic scaling within a control container.
/// </summary>
public class ContainerControl : ScrollableControl, IContainerControl
{
    private Control? _activeControl;
    private AutoScaleMode _autoScaleMode = AutoScaleMode.Inherit;
    private SizeF _autoScaleDimensions;

    /// <summary>
    /// Initializes a container that measures its preferred size from its layout.
    /// </summary>
    public ContainerControl()
    {
        CachePreferredSize = true;
    }

    internal override Size GetPreferredSizeCore(Size proposedSize)
    {
        Size borderSize = SizeFromClientSize(Size.Empty);
        Size totalPadding = borderSize + new Size(Padding.Horizontal, Padding.Vertical);
        return LayoutEngine.GetPreferredSize(this, proposedSize - totalPadding) + totalPadding;
    }

    /// <summary>
    /// Gets or sets the active descendant of this container.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? ActiveControl
    {
        get => _activeControl;
        set
        {
            if (value is not null && !IsDescendant(this, value))
            {
                throw new ArgumentException("The control is not a child of this container.", nameof(value));
            }

            SetActiveControl(value, focus: ContainsFocus);
        }
    }

    /// <summary>
    /// Gets or sets the scale mode applied to the container and its descendants.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AutoScaleMode AutoScaleMode
    {
        get => _autoScaleMode;
        set
        {
            if (!Enum.IsDefined(value))
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(AutoScaleMode));
            }

            _autoScaleMode = value;
        }
    }

    /// <summary>
    /// Gets or sets the design-time dimensions used as the scale baseline.
    /// </summary>
    [Localizable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SizeF AutoScaleDimensions
    {
        get => _autoScaleDimensions;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value.Width, nameof(value));
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height, nameof(value));
            _autoScaleDimensions = value;
        }
    }

    /// <summary>
    /// Gets the current scale dimensions for the effective scale mode.
    /// </summary>
    [Browsable(false)]
    public SizeF CurrentAutoScaleDimensions => ResolveAutoScaleMode() switch
    {
        AutoScaleMode.Dpi => Win32.GetSystemDpiScaleDimensions(),
        AutoScaleMode.Font => Win32.GetDefaultFontScaleDimensions(),
        _ => SizeF.Empty,
    };

    /// <inheritdoc />
    protected override uint ExStyle => base.ExStyle | Win32.WS_EX_CONTROLPARENT;

    /// <summary>
    /// Updates the container's active control through the standard interface path.
    /// </summary>
    bool IContainerControl.ActivateControl(Control active)
    {
        // Unlike the public property setter, this interface does not validate
        // membership or replace a descendant with its nearest container.
        Control? selected = ReferenceEquals(active, this) ? null : active;
        AssignActiveControl(selected);
        if (selected is not null && ContainsFocus)
        {
            _ = selected.Focus();
        }

        return ReferenceEquals(_activeControl, selected);
    }

    private void SetActiveControl(Control? value, bool focus)
    {
        Control? active = value;
        if (value is not null)
        {
            for (Control? parent = value.Parent; parent is not null && !ReferenceEquals(parent, this); parent = parent.Parent)
            {
                if (parent is ContainerControl container)
                {
                    container.AssignActiveControl(active);
                    active = container;
                }
            }
        }

        AssignActiveControl(active);
        if (value is not null)
        {
            NotifyControlFocused(this);
            if (focus)
            {
                _ = value.Focus();
            }
        }
    }

    private void AssignActiveControl(Control? value)
    {
        Control? previous = _activeControl;
        _activeControl = value;
        if (ReferenceEquals(previous, value))
        {
            return;
        }

        // A deactivated nested container must release its active descendant.
        // Keep its selection while the new active control remains in that path.
        for (Control? current = previous; current is not null && !ReferenceEquals(current, this); current = current.Parent)
        {
            if (current is ContainerControl container
                && !ReferenceEquals(container, value)
                && (value is null || !IsDescendant(container, value)))
            {
                container.AssignActiveControl(null);
            }
        }
    }

    internal static void NotifyControlFocused(Control control)
    {
        Control active = control;
        for (Control? parent = control.Parent; parent is not null; parent = parent.Parent)
        {
            if (parent is ContainerControl container)
            {
                container.AssignActiveControl(active);
                active = container;
            }
        }
    }

    internal static void NotifyControlRemoved(Control oldParent, Control removed)
    {
        for (Control? parent = oldParent; parent is not null; parent = parent.Parent)
        {
            if (parent is ContainerControl container
                && container._activeControl is { } active
                && (ReferenceEquals(active, removed) || IsDescendant(removed, active)))
            {
                Control? next = FindNextSelectableControl(container);
                container.SetActiveControl(next, focus: false);
                if (next is not null)
                {
                    // Restoring an active child also restores its native focus
                    // path. Reuse shared creation so visible siblings receive
                    // handles and hidden subtrees remain deferred.
                    container.CreateHandleRecursive();
                    _ = next.Focus();
                }
            }
        }
    }

    private static Control? FindNextSelectableControl(Control parent)
    {
        // Once a control has been removed, traversal starts at the first
        // remaining control in tab order. A nested container manages its own
        // tab sequence and is not traversed while looking for its sibling.
        foreach (Control child in parent.Controls.Cast<Control>().OrderBy(control => control.TabIndex))
        {
            if (child.IsDisposed || child.Disposing || !child.Visible || !child.Enabled)
            {
                continue;
            }

            if (child.TabStop)
            {
                return child;
            }

            Control? descendant = child is ContainerControl ? null : FindNextSelectableControl(child);
            if (descendant is not null)
            {
                return descendant;
            }
        }

        return null;
    }

    /// <summary>
    /// Releases the active-control reference before disposing the child tree.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _activeControl = null;
        }

        base.Dispose(disposing);
    }

    private static bool IsDescendant(Control parent, Control child)
    {
        for (Control? current = child.Parent; current is not null; current = current.Parent)
        {
            if (ReferenceEquals(current, parent))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Applies the configured scale to this container and its existing child tree.
    /// </summary>
    public void PerformAutoScale()
    {
        if (ResolveAutoScaleMode() == AutoScaleMode.None)
        {
            return;
        }

        SizeF currentDimensions = CurrentAutoScaleDimensions;
        if (currentDimensions.Width <= 0 || currentDimensions.Height <= 0)
        {
            return;
        }

        if (AutoScaleDimensions.Width <= 0 || AutoScaleDimensions.Height <= 0)
        {
            AutoScaleDimensions = currentDimensions;
            return;
        }

        float scaleX = currentDimensions.Width / AutoScaleDimensions.Width;
        float scaleY = currentDimensions.Height / AutoScaleDimensions.Height;
        if (Math.Abs(scaleX - 1f) >= 0.001f || Math.Abs(scaleY - 1f) >= 0.001f)
        {
            Size = new Size(ScaleSize(Width, scaleX), ScaleSize(Height, scaleY));
            foreach (Control control in ChildControls)
            {
                ScaleControlTree(control, scaleX, scaleY);
            }
        }

        AutoScaleDimensions = currentDimensions;
    }

    private AutoScaleMode ResolveAutoScaleMode()
    {
        if (AutoScaleMode != AutoScaleMode.Inherit)
        {
            return AutoScaleMode;
        }

        for (Control? parent = Parent; parent is not null; parent = parent.Parent)
        {
            if (parent is ContainerControl container)
            {
                return container.ResolveAutoScaleMode();
            }
        }

        return AutoScaleMode.None;
    }

    private static int ScaleCoordinate(int value, float factor)
        => (int)Math.Round(value * factor, MidpointRounding.AwayFromZero);

    private static int ScaleSize(int value, float factor)
        => Math.Max(1, ScaleCoordinate(value, factor));

    private static void ScaleControlTree(Control control, float scaleX, float scaleY)
    {
        Rectangle bounds = control.Bounds;
        control.SetBounds(ScaleCoordinate(bounds.X, scaleX), ScaleCoordinate(bounds.Y, scaleY),
            ScaleSize(bounds.Width, scaleX), ScaleSize(bounds.Height, scaleY));

        foreach (Control child in control.ChildControls)
        {
            ScaleControlTree(child, scaleX, scaleY);
        }
    }
}
