// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// IME property and context portions adapted from dotnet/winforms v10.0.11.

using System.ComponentModel;

namespace Lumina.Forms;

public partial class Control
{
    private static readonly object s_imeModeChangedEvent = new();
    private static ImeMode s_propagatingImeMode = ImeMode.Inherit;
    private ImeMode? _imeMode;
    private int _disableImeModeChangedCount;
    private bool _lastCanEnableIme = true;
    private NativeImeContext? _imeContext;

    internal ImeMode CachedImeMode
    {
        get
        {
            ImeMode mode = _imeMode ?? DefaultImeMode;
            return mode == ImeMode.Inherit ? _parent?.CachedImeMode ?? ImeMode.NoControl : mode;
        }
        private set => _imeMode = value;
    }

    /// <summary>Gets whether this control can enable its input method editor.</summary>
    protected virtual bool CanEnableIme => DefaultImeMode != ImeMode.Disable;

    /// <summary>Gets the default input method editor mode for this control.</summary>
    protected virtual ImeMode DefaultImeMode => ImeMode.Inherit;

    /// <summary>Gets or sets the input method editor mode used when this control receives focus.</summary>
    [Category("Behavior")]
    [Localizable(true)]
    [AmbientValue(ImeMode.Inherit)]
    public ImeMode ImeMode
    {
        get
        {
            ImeMode mode = ImeModeBase;
            return mode == ImeMode.OnHalf ? ImeMode.On : mode;
        }
        set => ImeModeBase = value;
    }

    /// <summary>Gets or sets the input method editor mode before compatibility normalization.</summary>
    protected virtual ImeMode ImeModeBase
    {
        get => CachedImeMode;
        set
        {
            if (value < ImeMode.Inherit || value > ImeMode.OnHalf)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ImeMode));
            }

            ImeMode oldMode = CachedImeMode;
            CachedImeMode = value;
            if (oldMode == value) { return; }

            if (!DesignMode && NativeImeContext.CurrentLanguage != ImeLanguage.Unsupported)
            {
                Control? focused = Focused ? this : ContainsFocus ? FromChildHandle(Win32.GetFocus()) : null;
                if (focused is not null && focused.CanEnableIme)
                {
                    _disableImeModeChangedCount++;
                    try { focused.UpdateImeContextMode(); }
                    finally { _disableImeModeChangedCount--; }
                }
            }

            VerifyImeModeChanged(oldMode, CachedImeMode);
        }
    }

    /// <summary>Occurs when this control's input method editor mode changes.</summary>
    [Category("Behavior")]
    public event EventHandler? ImeModeChanged
    {
        add => Events.AddHandler(s_imeModeChangedEvent, value);
        remove => Events.RemoveHandler(s_imeModeChangedEvent, value);
    }

    /// <summary>Gets the input method editor mode propagated between controls without an explicit mode.</summary>
    protected static ImeMode PropagatingImeMode
    {
        get
        {
            if (s_propagatingImeMode == ImeMode.Inherit && OperatingSystem.IsWindows())
            {
                nint focused = Win32.GetFocus();
                ImeLanguage language = NativeImeContext.CurrentLanguage;
                ImeMode mode = NativeImeContext.GetImeMode(focused, language);
                if (mode == ImeMode.Disable)
                {
                    mode = NativeImeContext.GetImeMode(Win32.GetAncestor(focused, 2), language);
                }
                SetPropagatingImeMode(mode);
            }
            return s_propagatingImeMode;
        }
    }

    /// <summary>Raises the <see cref="ImeModeChanged"/> event.</summary>
    protected virtual void OnImeModeChanged(EventArgs e)
        => (Events[s_imeModeChangedEvent] as EventHandler)?.Invoke(this, e);

    /// <summary>Restores the default input method editor mode.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ResetImeMode() => ImeMode = DefaultImeMode;

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal virtual bool ShouldSerializeImeMode() => _imeMode is { } mode && mode != DefaultImeMode;

    private static void SetPropagatingImeMode(ImeMode mode)
    {
        if (mode is not ImeMode.NoControl and not ImeMode.Disable) { s_propagatingImeMode = mode; }
    }

    private void VerifyImeModeChanged(ImeMode oldMode, ImeMode newMode)
    {
        if (DefaultImeMode != ImeMode.Disable && _disableImeModeChangedCount == 0
            && newMode != ImeMode.NoControl && oldMode != newMode)
        {
            OnImeModeChanged(EventArgs.Empty);
        }
    }

    internal void UpdateImeContextMode()
    {
        ImeLanguage language = NativeImeContext.CurrentLanguage;
        if (DesignMode || language == ImeLanguage.Unsupported || !Focused) { return; }

        ImeMode cached = CachedImeMode;
        ImeMode requested = DefaultImeMode != ImeMode.Disable && CanEnableIme
            ? cached == ImeMode.NoControl ? PropagatingImeMode : cached
            : ImeMode.Disable;
        if (requested == ImeMode.Inherit || NativeImeContext.GetImeMode(NativeHandle, language) == requested) { return; }

        ImeMode savedPropagating = PropagatingImeMode;
        _disableImeModeChangedCount++;
        try { (_imeContext ??= new()).SetImeStatus(requested, NativeHandle, language); }
        finally
        {
            _disableImeModeChangedCount--;
            if (requested == ImeMode.Disable && language == ImeLanguage.Chinese)
            {
                SetPropagatingImeMode(savedPropagating);
            }
        }

        ImeMode actual = NativeImeContext.GetImeMode(NativeHandle, language);
        // A failed native status query does not establish a mapped IME mode.
        if (!CanEnableIme || actual == ImeMode.Inherit) { return; }
        if (cached == ImeMode.NoControl) { SetPropagatingImeMode(actual); }
        else
        {
            CachedImeMode = actual;
            VerifyImeModeChanged(requested, actual);
        }
    }

    internal void VerifyImeRestrictedModeChanged()
    {
        bool canEnable = CanEnableIme;
        if (_lastCanEnableIme == canEnable) { return; }
        if (Focused)
        {
            _disableImeModeChangedCount++;
            try { UpdateImeContextMode(); }
            finally { _disableImeModeChangedCount--; }
        }
        ImeMode cached = CachedImeMode;
        VerifyImeModeChanged(canEnable ? ImeMode.Disable : cached, canEnable ? cached : ImeMode.Disable);
        _lastCanEnableIme = canEnable;
    }

    private void WmImeNotify(ref Message message)
    {
        if (DefaultImeMode != ImeMode.Disable && !DesignMode && CanEnableIme
            && message.WParam is 0x0006 or 0x0008)
        {
            ImeMode actual = NativeImeContext.GetImeMode(NativeHandle, NativeImeContext.CurrentLanguage);
            if (actual != ImeMode.Inherit)
            {
                ImeMode oldMode = CachedImeMode;
                if (oldMode == ImeMode.NoControl) { SetPropagatingImeMode(actual); }
                else
                {
                    CachedImeMode = actual;
                    VerifyImeModeChanged(oldMode, actual);
                }
            }
        }
        DefWndProc(ref message);
    }

    private void ReleaseImeContext(nint handle)
    {
        _imeContext?.Release(handle);
        _imeContext = null;
    }
}
