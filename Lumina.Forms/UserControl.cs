// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Drawing;
using Lumina.Forms.Layout;

namespace Lumina.Forms;

/// <summary>
/// Represents a reusable composite control hosted inside a form or another container.
/// </summary>
public class UserControl : ContainerControl
{
    private bool _loaded;
    private BorderStyle _borderStyle;

    /// <summary>Initializes a reusable composite control.</summary>
    public UserControl()
    {
        SetAutoSizeMode(AutoSizeMode.GrowOnly);
    }

    /// <summary>Gets or sets whether automatic sizing can also shrink this control.</summary>
    [Browsable(true)]
    [DefaultValue(AutoSizeMode.GrowOnly)]
    [Localizable(true)]
    public new AutoSizeMode AutoSizeMode
    {
        get => GetAutoSizeMode();
        set
        {
            if (value != AutoSizeMode.GrowOnly && value != AutoSizeMode.GrowAndShrink)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(AutoSizeMode));
            }

            if (GetAutoSizeMode() == value) { return; }
            SetAutoSizeMode(value);
            Control toLayout = DesignMode || Parent is null ? this : Parent;
            if (toLayout.LayoutEngine == DefaultLayout.Instance)
            {
                toLayout.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
            }
            toLayout.RequestLayout(this, nameof(AutoSize));
        }
    }

    /// <summary>Gets or sets the border drawn around this control.</summary>
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(BorderStyle.None)]
    public BorderStyle BorderStyle
    {
        get => _borderStyle;
        set
        {
            if (_borderStyle == value) { return; }
            if ((uint)value > (uint)BorderStyle.Fixed3D)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(BorderStyle));
            }

            _borderStyle = value;
            UpdateStyles();
        }
    }

    /// <inheritdoc />
    protected override Size DefaultSize => new(150, 150);

    /// <inheritdoc />
    protected override CreateParams CreateParams
    {
        get
        {
            const int WsBorder = 0x00800000;
            CreateParams parameters = base.CreateParams;
            parameters.Style &= ~WsBorder;
            parameters.ExStyle &= ~(int)Win32.WS_EX_CLIENTEDGE;
            if (_borderStyle == BorderStyle.FixedSingle) { parameters.Style |= WsBorder; }
            if (_borderStyle == BorderStyle.Fixed3D) { parameters.ExStyle |= (int)Win32.WS_EX_CLIENTEDGE; }
            return parameters;
        }
    }

    /// <summary>
    /// Occurs the first time the user control and its native child tree are
    /// created successfully.
    /// </summary>
    public event EventHandler? Load;

    /// <inheritdoc />
    protected override string ClassName => "STATIC";

    /// <inheritdoc />
    protected override uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_CLIPCHILDREN | Win32.WS_CLIPSIBLINGS;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultBackgroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultForegroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        ApplyNativeThemeState();
    }

    /// <inheritdoc />
    protected override void OnCreateControl()
    {
        base.OnCreateControl();
        if (!_loaded && !IsDisposed)
        {
            _loaded = true;
            OnLoad(EventArgs.Empty);
        }
    }

    /// <summary>Raises the <see cref="Load"/> event.</summary>
    protected virtual void OnLoad(EventArgs e) => Load?.Invoke(this, e);

    /// <inheritdoc />
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (!ContainsFocus)
        {
            _ = Focus();
        }

        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == Win32.WM_SETFOCUS)
        {
            if (ActiveControl is null)
            {
                SelectNextControl(null, true, true, true, false);
            }

            if (ValidationCancelled)
            {
                return;
            }
        }

        base.WndProc(ref m);
    }

    /// <inheritdoc />
    protected override void ApplyTheme()
    {
        ApplyNativeThemeState();
    }

    private void ApplyNativeThemeState()
    {
        if (NativeHandle == 0)
        {
            return;
        }

        DarkModeNative.ApplyThemeToWindow(NativeHandle, CurrentVisualStyle.IsDarkMode);
    }
}
