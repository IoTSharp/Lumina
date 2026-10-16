// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.Drawing;

namespace Lumina.Forms.Layout;

/// <summary>The default Control docking, anchoring and automatic sizing policy.</summary>
internal sealed class DefaultLayout : LayoutEngine
{
    internal static DefaultLayout Instance { get; } = new();
    private static readonly bool s_anchorLayoutV2 = AppContext.TryGetSwitch("System.Windows.Forms.AnchorLayoutV2", out bool enabled) && enabled;

    internal sealed class AnchorInfo
    {
        internal int Left;
        internal int Top;
        internal int Right;
        internal int Bottom;
    }

    private static bool NeedsAnchor(Control child)
        => child.LayoutDock == DockStyle.None
            && (child.LayoutAutoSize || child.LayoutAnchor != (AnchorStyles.Top | AnchorStyles.Left));

    private static bool NeedsDock(Control child)
        => child.LayoutDock != DockStyle.None && child.ParticipatesInLayout;

    private protected override void InitLayoutCore(Control child, BoundsSpecified specified)
    {
        if (specified != BoundsSpecified.None && (NeedsAnchor(child) || child.ChildrenNeedAnchorLayout))
        {
            UpdateAnchorInfo(child);
        }
    }

    // WinForms keeps its original policy unless the standard V2 switch is enabled.
    internal static void UpdateAnchorInfo(Control child)
    {
        if (!s_anchorLayoutV2)
        {
            if (!NeedsAnchor(child) || child.Parent is not { } container) { return; }
            Rectangle bounds = child.Bounds;
            Rectangle display = container.DisplayRectangle;
            AnchorStyles anchor = child.LayoutAnchor;
            AnchorInfo info = new()
            {
                Left = bounds.Left - display.X,
                Top = bounds.Top - display.Y,
                Right = bounds.Right - display.X,
                Bottom = bounds.Bottom - display.Y
            };
            if ((anchor & AnchorStyles.Right) != 0)
            {
                info.Right -= display.Width;
                if ((anchor & AnchorStyles.Left) == 0) { info.Left -= display.Width; }
            }
            else if ((anchor & AnchorStyles.Left) == 0)
            {
                info.Left -= display.Width / 2;
                info.Right -= display.Width / 2;
            }
            if ((anchor & AnchorStyles.Bottom) != 0)
            {
                info.Bottom -= display.Height;
                if ((anchor & AnchorStyles.Top) == 0) { info.Top -= display.Height; }
            }
            else if ((anchor & AnchorStyles.Top) == 0)
            {
                info.Top -= display.Height / 2;
                info.Bottom -= display.Height / 2;
            }
            child.LayoutAnchorInfo = info;
            return;
        }

        if (NeedsAnchor(child) && child.Parent is { } parent)
        {
            AnchorInfo? anchor = child.LayoutAnchorInfo;
            bool designMode = false;
            for (Control? ancestor = child; ancestor is not null; ancestor = ancestor.Parent)
            {
                if (ancestor.Site?.DesignMode == true) { designMode = true; break; }
            }
            if (anchor is null && parent.LayoutSuspendCount > (designMode ? 1 : 0))
            {
                parent.ChildrenNeedAnchorLayout = true;
                return;
            }

            if (anchor is null || child.ForceAnchorCalculations)
            {
                Rectangle display = parent.DisplayRectangle;
                Rectangle bounds = child.Bounds;
                child.LayoutAnchorInfo = new AnchorInfo
                {
                    Left = bounds.X,
                    Top = bounds.Y,
                    Right = display.Width - bounds.Right,
                    Bottom = display.Height - bounds.Bottom
                };
                parent.ChildrenNeedAnchorLayout = false;
            }
        }

        if (child.ChildrenNeedAnchorLayout && child.Parent?.ChildrenNeedAnchorLayout != true)
        {
            for (int index = 0; index < child.Controls.Count; index++)
            {
                UpdateAnchorInfo(child.Controls[index]);
            }
        }
    }

    private protected override bool LayoutCore(Control container, LayoutEventArgs layoutEventArgs)
    {
        Arrange(container, measureOnly: false);
        return container.LayoutAutoSize;
    }

    internal override Size GetPreferredSize(Control container, Size proposedConstraints)
        => Arrange(container, measureOnly: true);

    private static Size Arrange(Control container, bool measureOnly)
    {
        if (!container.HasChildren) { return Size.Empty; }

        bool hasDock = false;
        bool hasAnchor = false;
        bool hasAutoSize = false;
        foreach (Control child in container.ChildControls)
        {
            hasDock |= NeedsDock(child);
            hasAnchor |= NeedsAnchor(child);
            hasAutoSize |= child.LayoutAutoSize && child.LayoutDock == DockStyle.None;
        }

        Size dockSize = hasDock ? LayoutDockedControls(container, measureOnly) : Size.Empty;
        Dictionary<Control, Rectangle> bounds = [];
        bool deferAnchors = false;

        if (!measureOnly && hasAnchor)
        {
            Rectangle display = container.DisplayRectangle;
            deferAnchors = container.LayoutAutoSize && (display.Width == 0 || display.Height == 0);
            for (int index = container.Controls.Count - 1; !deferAnchors && index >= 0; index--)
            {
                Control child = container.Controls[index];
                if (NeedsAnchor(child))
                {
                    CacheBounds(bounds, child, GetAnchoredBounds(child, display));
                }
            }
        }

        for (int index = container.Controls.Count - 1; hasAutoSize && index >= 0; index--)
        {
            Control child = container.Controls[index];
            if (!child.LayoutAutoSize || child.LayoutDock != DockStyle.None) { continue; }

            Rectangle oldBounds = bounds.GetValueOrDefault(child, child.Bounds);
            AnchorStyles anchor = child.LayoutAnchor;
            Size proposed = new(int.MaxValue, int.MaxValue);
            if ((anchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right))
            {
                proposed.Width = oldBounds.Width;
            }
            if ((anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom))
            {
                proposed.Height = oldBounds.Height;
            }

            Size preferred = child.GetPreferredSize(proposed);
            Size newSize = child.LayoutAutoSizeMode == AutoSizeMode.GrowAndShrink
                ? preferred
                : new Size(Math.Max(oldBounds.Width, preferred.Width), Math.Max(oldBounds.Height, preferred.Height));
            Point location = oldBounds.Location;
            if ((anchor & (AnchorStyles.Left | AnchorStyles.Right)) == AnchorStyles.Right)
            {
                location.X -= newSize.Width - oldBounds.Width;
            }
            if ((anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == AnchorStyles.Bottom)
            {
                location.Y -= newSize.Height - oldBounds.Height;
            }
            CacheBounds(bounds, child, new Rectangle(location, newSize));
        }

        if (!measureOnly)
        {
            if (container.LayoutAutoSize)
            {
                Rectangle display = container.DisplayRectangle;
                deferAnchors = display.Width == 0 || display.Height == 0;
            }
            if (!deferAnchors)
            {
                foreach ((Control child, Rectangle destination) in bounds)
                {
                    child.SetBoundsFromLayout(destination, BoundsSpecified.None);
                }
            }
            return Size.Empty;
        }

        Size anchorSize = GetAnchorPreferredSize(container, bounds);
        anchorSize.Width -= container.Padding.Left;
        anchorSize.Height -= container.Padding.Top;
        return new Size(Math.Max(dockSize.Width, anchorSize.Width), Math.Max(dockSize.Height, anchorSize.Height));
    }

    private static void CacheBounds(Dictionary<Control, Rectangle> bounds, Control child, Rectangle destination)
    {
        if (destination != bounds.GetValueOrDefault(child, child.Bounds)) { bounds[child] = destination; }
    }

    private static Rectangle GetAnchoredBounds(Control child, Rectangle display)
    {
        Rectangle bounds = child.Bounds;
        AnchorInfo? info = child.LayoutAnchorInfo;
        if (!s_anchorLayoutV2) { return GetLegacyAnchoredBounds(child, display, false, bounds); }
        if (display.IsEmpty || info is null) { return bounds; }

        AnchorStyles anchor = child.LayoutAnchor;
        int width = bounds.Width;
        int height = bounds.Height;
        if ((anchor & AnchorStyles.Left) != 0)
        {
            if ((anchor & AnchorStyles.Right) != 0) { width = display.Width - info.Right - info.Left; }
        }
        else if ((anchor & AnchorStyles.Right) != 0)
        {
            info.Left = display.Width - width - info.Right;
        }
        else
        {
            int delta = (display.Width - info.Left - info.Right - width) / 2;
            info.Left += delta;
            info.Right += delta;
        }

        if ((anchor & AnchorStyles.Top) != 0)
        {
            if ((anchor & AnchorStyles.Bottom) != 0) { height = display.Height - info.Bottom - info.Top; }
        }
        else if ((anchor & AnchorStyles.Bottom) != 0)
        {
            info.Top = display.Height - height - info.Bottom;
        }
        else
        {
            int delta = (display.Height - info.Top - info.Bottom - height) / 2;
            info.Top += delta;
            info.Bottom += delta;
        }
        return new Rectangle(info.Left, info.Top, width, height);
    }

    private static Rectangle GetLegacyAnchoredBounds(Control child, Rectangle display, bool measureOnly, Rectangle cached)
    {
        AnchorInfo? info = child.LayoutAnchorInfo;
        if (info is null) { return cached; }
        int left = info.Left + display.X;
        int top = info.Top + display.Y;
        int right = info.Right + display.X;
        int bottom = info.Bottom + display.Y;
        AnchorStyles anchor = child.LayoutAnchor;
        if ((anchor & AnchorStyles.Right) != 0)
        {
            right += display.Width;
            if ((anchor & AnchorStyles.Left) == 0) { left += display.Width; }
        }
        else if ((anchor & AnchorStyles.Left) == 0)
        {
            left += display.Width / 2;
            right += display.Width / 2;
        }
        if ((anchor & AnchorStyles.Bottom) != 0)
        {
            bottom += display.Height;
            if ((anchor & AnchorStyles.Top) == 0) { top += display.Height; }
        }
        else if ((anchor & AnchorStyles.Top) == 0)
        {
            top += display.Height / 2;
            bottom += display.Height / 2;
        }
        if (!measureOnly)
        {
            right = Math.Max(left, right);
            bottom = Math.Max(top, bottom);
        }
        else
        {
            Rectangle current = child.Bounds;
            if (right < left || cached.Width != current.Width || cached.X != current.X)
            {
                if (cached != current) { left = Math.Max(Math.Abs(left), Math.Abs(cached.Left)); }
                right = left + Math.Max(current.Width, cached.Width) + Math.Abs(right);
            }
            else
            {
                left = left > 0 ? left : current.Left;
                right = right > 0 ? right : current.Right + Math.Abs(right);
            }
            if (bottom < top || cached.Height != current.Height || cached.Y != current.Y)
            {
                if (cached != current) { top = Math.Max(Math.Abs(top), Math.Abs(cached.Top)); }
                bottom = top + Math.Max(current.Height, cached.Height) + Math.Abs(bottom);
            }
            else
            {
                top = top > 0 ? top : current.Top;
                bottom = bottom > 0 ? bottom : current.Bottom + Math.Abs(bottom);
            }
        }
        return new Rectangle(left, top, right - left, bottom - top);
    }

    private static Size LayoutDockedControls(Control container, bool measureOnly)
    {
        Rectangle remaining = measureOnly ? Rectangle.Empty : container.DisplayRectangle;
        Size preferred = Size.Empty;
        for (int index = container.Controls.Count - 1; index >= 0; index--)
        {
            Control child = container.Controls[index];
            if (!NeedsDock(child)) { continue; }
            DockStyle dock = child.LayoutDock;
            Size size;
            Rectangle destination;
            switch (dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    size = child.LayoutAutoSize ? child.GetPreferredSize(new Size(remaining.Width, 1)) : child.Size;
                    size.Width = measureOnly ? Math.Max(size.Width, remaining.Width) : remaining.Width;
                    destination = new Rectangle(remaining.X, dock == DockStyle.Top ? remaining.Y : remaining.Bottom - size.Height, size.Width, size.Height);
                    CalculateDockedBounds(child, destination, measureOnly, ref preferred, ref remaining);
                    if (dock == DockStyle.Top) { remaining.Y += child.Height; }
                    remaining.Height -= child.Height;
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    size = child.LayoutAutoSize ? child.GetPreferredSize(new Size(1, remaining.Height)) : child.Size;
                    size.Height = measureOnly ? Math.Max(size.Height, remaining.Height) : remaining.Height;
                    destination = new Rectangle(dock == DockStyle.Left ? remaining.X : remaining.Right - size.Width, remaining.Y, size.Width, size.Height);
                    CalculateDockedBounds(child, destination, measureOnly, ref preferred, ref remaining);
                    if (dock == DockStyle.Left) { remaining.X += child.Width; }
                    remaining.Width -= child.Width;
                    break;
                case DockStyle.Fill:
                    CalculateDockedBounds(child, remaining, measureOnly, ref preferred, ref remaining);
                    break;
            }
        }
        return preferred;
    }

    private static void CalculateDockedBounds(Control child, Rectangle destination, bool measureOnly,
        ref Size preferred, ref Rectangle remaining)
    {
        if (!measureOnly)
        {
            child.SetBoundsFromLayout(destination, BoundsSpecified.None);
            return;
        }

        Size needed = new(Math.Max(0, destination.Width - remaining.Width), Math.Max(0, destination.Height - remaining.Height));
        DockStyle dock = child.LayoutDock;
        if (dock is DockStyle.Top or DockStyle.Bottom) { needed.Width = 0; }
        if (dock is DockStyle.Left or DockStyle.Right) { needed.Height = 0; }
        if (dock != DockStyle.Fill)
        {
            preferred += needed;
            remaining.Size += needed;
        }
        else if (child.LayoutAutoSize)
        {
            Size childPreferred = child.GetPreferredSize(needed);
            preferred += childPreferred;
            remaining.Size += childPreferred;
        }
    }

    private static Size GetAnchorPreferredSize(Control container, Dictionary<Control, Rectangle> cached)
    {
        Size preferred = Size.Empty;
        for (int index = container.Controls.Count - 1; index >= 0; index--)
        {
            Control child = container.Controls[index];
            if (NeedsDock(child) || !child.ParticipatesInLayout) { continue; }
            Rectangle bounds = cached.GetValueOrDefault(child, child.Bounds);
            AnchorStyles anchor = child.LayoutAnchor;
            if ((anchor & (AnchorStyles.Left | AnchorStyles.Right)) == AnchorStyles.Left)
            {
                preferred.Width = Math.Max(preferred.Width, bounds.Right + child.Margin.Right);
            }
            if ((anchor & AnchorStyles.Bottom) == 0)
            {
                preferred.Height = Math.Max(preferred.Height, bounds.Bottom + child.Margin.Bottom);
            }
            if ((anchor & AnchorStyles.Right) != 0)
            {
                if (s_anchorLayoutV2) { preferred.Width = Math.Max(preferred.Width, bounds.Right + (child.LayoutAnchorInfo?.Right ?? 0)); }
                else
                {
                    Rectangle destination = GetLegacyAnchoredBounds(child, Rectangle.Empty, true, bounds);
                    preferred.Width = Math.Max(preferred.Width, destination.Width < 0 ? bounds.Right + child.Margin.Right + destination.Width : destination.Right);
                }
            }
            if ((anchor & AnchorStyles.Bottom) != 0)
            {
                if (s_anchorLayoutV2) { preferred.Height = Math.Max(preferred.Height, bounds.Bottom + (child.LayoutAnchorInfo?.Bottom ?? 0)); }
                else
                {
                    Rectangle destination = GetLegacyAnchoredBounds(child, Rectangle.Empty, true, bounds);
                    preferred.Height = Math.Max(preferred.Height, destination.Height < 0 ? bounds.Bottom + child.Margin.Bottom + destination.Height : destination.Bottom);
                }
            }
        }
        return preferred;
    }
}
