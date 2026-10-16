// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.Drawing;

namespace Lumina.Forms.Layout;

/// <summary>Provides the standard extension points for a control's layout algorithm.</summary>
public abstract class LayoutEngine
{
    internal static Control GetControl(object value, string parameterName)
    {
        ArgumentNullException.ThrowIfNull(value, parameterName);
        return value as Control
            ?? throw new NotSupportedException($"LayoutEngine does not support objects of type {value.GetType()}.");
    }

    /// <summary>Initializes the layout information for the specified child.</summary>
    public virtual void InitLayout(object child, BoundsSpecified specified)
    {
        Control control = GetControl(child, nameof(child));
        InitLayoutCore(control, specified);
    }

    // The standard base engine has no geometry policy; derived engines opt in.
    private protected virtual void InitLayoutCore(Control child, BoundsSpecified specified) { }

    /// <summary>Arranges the children and indicates whether the parent needs another layout.</summary>
    public virtual bool Layout(object container, LayoutEventArgs layoutEventArgs)
        => LayoutCore(GetControl(container, nameof(container)), layoutEventArgs);

    private protected virtual bool LayoutCore(Control container, LayoutEventArgs layoutEventArgs) => false;

    internal virtual Size GetPreferredSize(Control container, Size proposedConstraints) => Size.Empty;
}
