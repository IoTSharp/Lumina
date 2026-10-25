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
    private Control? _focusedControl;
    private Control? _unvalidatedControl;
    private bool _validating;
    private AutoScaleMode _autoScaleMode = AutoScaleMode.Inherit;
    private SizeF _autoScaleDimensions;
    private AutoValidate _autoValidate = AutoValidate.Inherit;

    /// <summary>
    /// Initializes a container that measures its preferred size from its layout.
    /// </summary>
    public ContainerControl()
    {
        SetStyle(ControlStyles.ContainerControl, true);
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
        set => SetActiveControl(value);
    }

    /// <summary>
    /// Gets the form that owns this container through its parent chain.
    /// </summary>
    /// <remarks>
    /// The value is resolved from the current parent chain each time it is
    /// read. This keeps the standard WinForms behavior when a container is
    /// detached and attached to another form without requiring a cached
    /// ownership field to be synchronized by every collection operation.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form? ParentForm => FindForm();

    /// <summary>
    /// Gets or sets the automatic validation policy used before focus leaves this container.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AmbientValue(AutoValidate.Inherit)]
    public virtual AutoValidate AutoValidate
    {
        get => _autoValidate == AutoValidate.Inherit ? GetAutoValidateForControl(this) : _autoValidate;
        set
        {
            if (!Enum.IsDefined(value))
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(AutoValidate));
            }

            if (_autoValidate == value) { return; }
            _autoValidate = value;
            OnAutoValidateChanged(EventArgs.Empty);
        }
    }

    /// <summary>Raised when <see cref="AutoValidate"/> changes.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event EventHandler? AutoValidateChanged;

    /// <summary>Raises <see cref="AutoValidateChanged"/>.</summary>
    protected virtual void OnAutoValidateChanged(EventArgs e)
        => AutoValidateChanged?.Invoke(this, e);

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

    /// <summary>
    /// Gets the ratio between the current and design-time automatic scaling
    /// dimensions.
    /// </summary>
    /// <remarks>
    /// A container without a usable design-time baseline has no meaningful
    /// ratio. WinForms exposes a neutral factor in that state, so callers can
    /// safely use the value while a form is being constructed or before its
    /// designer metadata has been applied.
    /// </remarks>
    [Browsable(false)]
    protected SizeF AutoScaleFactor
    {
        get
        {
            SizeF current = CurrentAutoScaleDimensions;
            SizeF design = AutoScaleDimensions;
            float width = current.Width > 0f && design.Width > 0f
                ? current.Width / design.Width
                : 1f;
            float height = current.Height > 0f && design.Height > 0f
                ? current.Height / design.Height
                : 1f;
            return new SizeF(width, height);
        }
    }

    /// <inheritdoc />
    protected override uint ExStyle => base.ExStyle | Win32.WS_EX_CONTROLPARENT;

    /// <summary>
    /// Updates the container's active control through the standard interface path.
    /// </summary>
    bool IContainerControl.ActivateControl(Control active) => ActivateControl(active);

    internal bool ActivateControl(Control? control)
    {
        // Unlike the public property setter, this interface does not validate
        // membership or replace a descendant with its nearest container.
        ContainerControl? parentContainer = Parent?.GetContainerControl() as ContainerControl;
        bool updateParent = parentContainer is not null && !ReferenceEquals(parentContainer.ActiveControl, this);
        if (ReferenceEquals(control, _activeControl) && !updateParent)
        {
            return true;
        }

        if (updateParent && !parentContainer!.ActivateControl(this))
        {
            return false;
        }

        return AssignActiveControl(ReferenceEquals(control, this) ? null : control);
    }

    internal void SetActiveControl(Control? value) => SetActiveControl(value, focus: true);

    private void SetActiveControl(Control? value, bool focus)
    {
        if (ReferenceEquals(_activeControl, value) && (value is null || value.Focused))
        {
            return;
        }

        if (value is not null && !IsDescendant(this, value))
        {
            throw new ArgumentException("Invisible or disabled control cannot be activated", nameof(value));
        }

        ContainerControl? container = value is null ? this : value.Parent?.GetContainerControl() as ContainerControl;
        bool selected = container is not null ? container.ActivateControl(value) : AssignActiveControl(value);
        if (!focus || container is null || !selected) { return; }

        ContainerControl ancestor = this;
        while (ancestor.Parent?.GetContainerControl() is ContainerControl parentContainer)
        {
            ancestor = parentContainer;
        }

        if (ancestor.ContainsFocus) { container.FocusActiveControlInternal(); }
    }

    private bool AssignActiveControl(Control? value)
    {
        if (!ReferenceEquals(_activeControl, value))
        {
            _activeControl = value;
            UpdateFocusedControl();
        }
        else
        {
            _focusedControl = _activeControl;
        }

        return ReferenceEquals(_activeControl, value);
    }

    private void UpdateFocusedControl()
    {
        // This tracks the logical Enter/Leave path independently of native focus.
        // Selection can change before any HWND exists, and event handlers can
        // replace the active control while this walk is in progress.
        EnsureUnvalidatedControl(_focusedControl);
        Control? pathControl = _focusedControl;
        while (!ReferenceEquals(_activeControl, pathControl))
        {
            if (pathControl is null || ContainsOrEquals(pathControl, _activeControl))
            {
                Control? nextControl = _activeControl;
                while (true)
                {
                    Control? parent = nextControl!.Parent;
                    if (ReferenceEquals(parent, this) || ReferenceEquals(parent, pathControl)) { break; }
                    nextControl = parent;
                }

                Control? priorFocusedControl = _focusedControl = pathControl;
                EnterValidation(nextControl!);
                if (!ReferenceEquals(_focusedControl, priorFocusedControl))
                {
                    pathControl = _focusedControl;
                    continue;
                }

                pathControl = nextControl;
                RaiseEnterOrLeave(pathControl!, enter: true);
            }
            else
            {
                ContainerControl innermost = this;
                while (innermost._focusedControl is ContainerControl inner) { innermost = inner; }

                Control? stopControl = null;
                if (innermost._focusedControl is not null)
                {
                    pathControl = innermost._focusedControl;
                    stopControl = innermost;
                    if (!ReferenceEquals(innermost, this))
                    {
                        innermost._focusedControl = null;
                        innermost._activeControl = null;
                    }
                }
                else
                {
                    pathControl = innermost;
                    if (innermost.Parent is { } parent)
                    {
                        ContainerControl? container = parent.GetContainerControl() as ContainerControl;
                        stopControl = container;
                        if (container is not null && !ReferenceEquals(container, this))
                        {
                            container._focusedControl = null;
                            container._activeControl = null;
                        }
                    }
                }

                do
                {
                    Control? leaving = pathControl;
                    pathControl = pathControl?.Parent;
                    if (ReferenceEquals(pathControl, this)) { pathControl = null; }
                    if (leaving is not null) { RaiseEnterOrLeave(leaving, enter: false); }
                }
                while (pathControl is not null && !ReferenceEquals(pathControl, stopControl)
                    && !ContainsOrEquals(pathControl, _activeControl));
            }
        }

        _focusedControl = _activeControl;
        if (_activeControl is not null) { EnterValidation(_activeControl); }
    }

    private static bool ContainsOrEquals(Control parent, Control? child)
        => child is not null && (ReferenceEquals(parent, child) || IsDescendant(parent, child));

    private static void RaiseEnterOrLeave(Control control, bool enter)
    {
        try
        {
            if (enter) { control.OnEnter(EventArgs.Empty); }
            else { control.OnLeave(EventArgs.Empty); }
        }
        catch (Exception exception)
        {
            Application.OnThreadException(exception);
        }
    }

    internal void FocusActiveControlInternal()
    {
        if (_activeControl is { Visible: true } active)
        {
            nint focusHandle = Win32.GetFocus();
            if (focusHandle == 0 || !ReferenceEquals(FromChildHandle(focusHandle), active))
            {
                _ = Win32.SetFocus(active.Handle);
            }
        }
        else
        {
            ContainerControl? container = this;
            while (container is not null && !container.Visible && container.Parent is { } parent)
            {
                container = parent.GetContainerControl() as ContainerControl;
            }

            if (container is { Visible: true }) { _ = Win32.SetFocus(container.Handle); }
        }
    }

    /// <inheritdoc />
    protected override void Select(bool directed, bool forward)
    {
        bool selected = true;
        if (Parent?.GetContainerControl() is { } container)
        {
            container.ActiveControl = this;
            selected = ReferenceEquals(container.ActiveControl, this);
        }

        if (directed && selected)
        {
            SelectNextControl(null, forward, tabStopOnly: true, nested: true, wrap: false);
        }
    }

    /// <inheritdoc />
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == Win32.WM_SETFOCUS)
        {
            if (ActiveControl is not null)
            {
                if (!ActiveControl.Visible) { InvokeGotFocus(this, EventArgs.Empty); }
                FocusActiveControlInternal();
                return;
            }

            if (Parent?.GetContainerControl() is { } container
                && !(container is ContainerControl knownContainer
                    ? knownContainer.ActivateControl(this)
                    : container.ActivateControl(this)))
            {
                return;
            }
        }

        base.WndProc(ref m);
    }

    internal static void NotifyControlRemoved(Control oldParent, Control removed)
    {
        if (oldParent.GetContainerControl() is ContainerControl container)
        {
            container.AfterControlRemoved(removed, oldParent);
        }
    }

    private void AfterControlRemoved(Control removed, Control oldParent)
    {
        if (ContainsOrEquals(removed, _activeControl))
        {
            bool selected = SelectNextControl(removed, true, true, true, true);
            if (selected && !ReferenceEquals(_activeControl, removed))
            {
                if (_activeControl?.Parent is not null)
                {
                    // The focus request also creates the visible container tree,
                    // including siblings, while hidden descendants stay deferred.
                    CreateHandleRecursive();
                    FocusActiveControlInternal();
                }
            }
            else
            {
                SetActiveControl(null);
            }
        }
        else if (_activeControl is null && Parent?.GetContainerControl() is ContainerControl parent
            && ReferenceEquals(parent.ActiveControl, this))
        {
            FindForm()?.SelectNextControl(this, true, true, true, true);
        }

        for (ContainerControl? container = Parent?.GetContainerControl() as ContainerControl;
            container is not null; container = container.Parent?.GetContainerControl() as ContainerControl)
        {
            if (ContainsOrEquals(removed, container._unvalidatedControl))
            {
                container._unvalidatedControl = oldParent;
            }
        }

        if (ContainsOrEquals(removed, _unvalidatedControl)) { _unvalidatedControl = null; }
    }

    private void EnsureUnvalidatedControl(Control? candidate)
    {
        if (_validating || _unvalidatedControl is not null || candidate is null
            || GetAutoValidateForControl(candidate) == AutoValidate.Disable)
        {
            return;
        }

        _unvalidatedControl = candidate;
        while (_unvalidatedControl is ContainerControl container)
        {
            if (container._unvalidatedControl is { } pending
                && GetAutoValidateForControl(pending) != AutoValidate.Disable)
            {
                _unvalidatedControl = pending;
            }
            else if (container._activeControl is { } active
                && GetAutoValidateForControl(active) != AutoValidate.Disable)
            {
                _unvalidatedControl = active;
            }
            else { break; }
        }
    }

    private void EnterValidation(Control enterControl)
    {
        if (_unvalidatedControl is null || !enterControl.CausesValidation) { return; }
        AutoValidate mode = GetAutoValidateForControl(_unvalidatedControl);
        if (mode == AutoValidate.Disable) { return; }

        Control? ancestor = enterControl;
        while (ancestor is not null && !ContainsOrEquals(ancestor, _unvalidatedControl))
        {
            ancestor = ancestor.Parent;
        }

        ValidateThroughAncestor(ancestor, mode == AutoValidate.EnablePreventFocusChange);
    }

    /// <summary>Validates the last unvalidated control and its ancestors in this container.</summary>
    public bool Validate() => Validate(checkAutoValidate: false);

    /// <summary>Validates the current control, optionally honoring disabled automatic validation.</summary>
    public bool Validate(bool checkAutoValidate) => ValidateInternal(checkAutoValidate, out _);

    private bool ValidateInternal(bool checkAutoValidate, out bool allowsFocusChange)
    {
        allowsFocusChange = false;
        if (AutoValidate != AutoValidate.EnablePreventFocusChange
            && !(_activeControl?.CausesValidation ?? false))
        {
            return true;
        }

        if (_unvalidatedControl is null)
        {
            if (_focusedControl is ContainerControl nested && nested.CausesValidation)
            {
                if (!nested.ValidateInternal(checkAutoValidate, out allowsFocusChange)) { return false; }
            }
            else { _unvalidatedControl = _focusedControl; }
        }

        bool preventFocusChange = true;
        Control? control = _unvalidatedControl ?? _focusedControl;
        if (control is not null)
        {
            AutoValidate mode = GetAutoValidateForControl(control);
            if (checkAutoValidate && mode == AutoValidate.Disable) { return true; }
            preventFocusChange = mode == AutoValidate.EnablePreventFocusChange;
            allowsFocusChange = mode == AutoValidate.EnableAllowFocusChange;
        }

        return ValidateThroughAncestor(null, preventFocusChange);
    }

    /// <summary>Validates selectable child controls, including descendants.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool ValidateChildren() => ValidateChildren(ValidationConstraints.Selectable);

    /// <summary>Validates descendants that satisfy the specified constraints.</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool ValidateChildren(ValidationConstraints validationConstraints)
    {
        if ((int)validationConstraints is < 0 or > 31)
        {
            throw new InvalidEnumArgumentException(nameof(validationConstraints),
                (int)validationConstraints, typeof(ValidationConstraints));
        }

        return !PerformContainerValidation(validationConstraints);
    }

    private bool ValidateThroughAncestor(Control? ancestor, bool preventFocusChange)
    {
        ancestor ??= this;
        if (_validating) { return false; }
        _unvalidatedControl ??= _focusedControl;
        if (_unvalidatedControl is null) { return true; }
        if (!ContainsOrEquals(ancestor, _unvalidatedControl)) { return false; }

        _validating = true;
        bool cancelled = false;
        Control? active = _activeControl;
        Control? validating = _unvalidatedControl;
        if (active is not null)
        {
            active.ValidationCancelled = false;
            if (active is ContainerControl activeContainer)
            {
                foreach (Control child in activeContainer.ChildControls) { child.ValidationCancelled = false; }
            }
        }

        try
        {
            while (validating is not null && !ReferenceEquals(validating, ancestor))
            {
                cancelled = validating.PerformControlValidation(bulkValidation: false);
                if (cancelled) { break; }
                validating = validating.Parent;
            }

            if (cancelled && preventFocusChange)
            {
                if (_unvalidatedControl is null && ContainsOrEquals(ancestor, validating))
                {
                    _unvalidatedControl = validating;
                }

                if (ReferenceEquals(active, _activeControl) && active is not null)
                {
                    active.ValidationCancelled = true;
                    if (active is ContainerControl activeContainer)
                    {
                        if (activeContainer._focusedControl is { } focused) { focused.ValidationCancelled = true; }
                        activeContainer.ResetActiveAndFocusedControlsRecursive();
                    }
                }

                SetActiveControl(_unvalidatedControl);
            }
        }
        finally
        {
            _unvalidatedControl = null;
            _validating = false;
        }

        return !cancelled;
    }

    private void ResetActiveAndFocusedControlsRecursive()
    {
        if (_activeControl is ContainerControl nested) { nested.ResetActiveAndFocusedControlsRecursive(); }
        _activeControl = null;
        _focusedControl = null;
    }

    /// <summary>
    /// Releases the active-control reference before disposing the child tree.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _activeControl = null;
            _unvalidatedControl = null;
        }

        base.Dispose(disposing);
        _focusedControl = null;
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
