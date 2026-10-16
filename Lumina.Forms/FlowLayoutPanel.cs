using System.Drawing;
using Lumina.Forms.Layout;

namespace Lumina.Forms;

/// <summary>
/// Represents a lightweight flow layout container.
/// </summary>
public class FlowLayoutPanel : Panel
{
    private FlowDirection _flowDirection = FlowDirection.LeftToRight;
    private bool _wrapContents = true;

    /// <inheritdoc />
    public override LayoutEngine LayoutEngine => FlowLayoutEngine.Instance;

    /// <summary>
    /// Gets or sets the direction used to place child controls.
    /// </summary>
    public FlowDirection FlowDirection
    {
        get => _flowDirection;
        set
        {
            if (_flowDirection == value)
            {
                return;
            }

            _flowDirection = value;
            RequestLayout(this, nameof(FlowDirection));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether controls wrap when they reach the container edge.
    /// </summary>
    public bool WrapContents
    {
        get => _wrapContents;
        set
        {
            if (_wrapContents == value)
            {
                return;
            }

            _wrapContents = value;
            RequestLayout(this, nameof(WrapContents));
        }
    }

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
    }

    private void LayoutFlow()
    {
        Rectangle displayBounds = DisplayRectangle;
        switch (FlowDirection)
        {
            case FlowDirection.RightToLeft:
                LayoutHorizontal(displayBounds, leftToRight: false);
                break;

            case FlowDirection.TopDown:
                LayoutVertical(displayBounds, topDown: true);
                break;

            case FlowDirection.BottomUp:
                LayoutVertical(displayBounds, topDown: false);
                break;

            default:
                LayoutHorizontal(displayBounds, leftToRight: true);
                break;
        }
    }

    private void LayoutHorizontal(Rectangle displayBounds, bool leftToRight)
    {
        int x = leftToRight ? displayBounds.Left : displayBounds.Right;
        int y = displayBounds.Top;
        int lineHeight = 0;
        bool firstInLine = true;

        foreach (Control child in ChildControls)
        {
            if (!child.ParticipatesInLayout)
            {
                continue;
            }

            Size childSize = GetChildLayoutSize(child, displayBounds.Size);
            int childWidth = Math.Max(1, childSize.Width);
            int childHeight = Math.Max(1, childSize.Height);
            int outerWidth = childWidth + child.Margin.Horizontal;
            int outerHeight = childHeight + child.Margin.Vertical;

            bool shouldWrap = WrapContents && !firstInLine && leftToRight
                ? x + outerWidth > displayBounds.Right
                : WrapContents && !firstInLine && x - outerWidth < displayBounds.Left;

            if (shouldWrap)
            {
                x = leftToRight ? displayBounds.Left : displayBounds.Right;
                y += lineHeight;
                lineHeight = 0;
                firstInLine = true;
            }

            int childLeft = leftToRight
                ? x + child.Margin.Left
                : x - outerWidth + child.Margin.Left;
            int childTop = y + child.Margin.Top;

            child.SetBoundsFromLayout(new Rectangle(childLeft, childTop, childWidth, childHeight), BoundsSpecified.None);

            x = leftToRight ? x + outerWidth : x - outerWidth;
            lineHeight = Math.Max(lineHeight, outerHeight);
            firstInLine = false;
        }
    }

    private void LayoutVertical(Rectangle displayBounds, bool topDown)
    {
        int x = displayBounds.Left;
        int y = topDown ? displayBounds.Top : displayBounds.Bottom;
        int lineWidth = 0;
        bool firstInLine = true;

        foreach (Control child in ChildControls)
        {
            if (!child.ParticipatesInLayout)
            {
                continue;
            }

            Size childSize = GetChildLayoutSize(child, displayBounds.Size);
            int childWidth = Math.Max(1, childSize.Width);
            int childHeight = Math.Max(1, childSize.Height);
            int outerWidth = childWidth + child.Margin.Horizontal;
            int outerHeight = childHeight + child.Margin.Vertical;

            bool shouldWrap = WrapContents && !firstInLine && topDown
                ? y + outerHeight > displayBounds.Bottom
                : WrapContents && !firstInLine && y - outerHeight < displayBounds.Top;

            if (shouldWrap)
            {
                y = topDown ? displayBounds.Top : displayBounds.Bottom;
                x += lineWidth;
                lineWidth = 0;
                firstInLine = true;
            }

            int childLeft = x + child.Margin.Left;
            int childTop = topDown
                ? y + child.Margin.Top
                : y - outerHeight + child.Margin.Top;

            child.SetBoundsFromLayout(new Rectangle(childLeft, childTop, childWidth, childHeight), BoundsSpecified.None);

            y = topDown ? y + outerHeight : y - outerHeight;
            lineWidth = Math.Max(lineWidth, outerWidth);
            firstInLine = false;
        }
    }

    private static Size GetChildLayoutSize(Control child, Size availableSize)
        => child.AutoSize
            ? child.GetPreferredSize(new Size(
                Math.Max(1, availableSize.Width - child.Margin.Horizontal),
                Math.Max(1, availableSize.Height - child.Margin.Vertical)))
            : child.SpecifiedBounds.Size;

    private Size MeasureFlow(Size proposedSize)
    {
        bool horizontal = FlowDirection is FlowDirection.LeftToRight or FlowDirection.RightToLeft;
        int availablePrimary = Math.Max(1, horizontal ? proposedSize.Width : proposedSize.Height);
        int linePrimary = 0;
        int lineSecondary = 0;
        int maximumPrimary = 0;
        int totalSecondary = 0;
        bool firstInLine = true;

        foreach (Control child in ChildControls)
        {
            if (!child.ParticipatesInLayout)
            {
                continue;
            }

            Size childSize = GetChildLayoutSize(child, proposedSize);
            int outerWidth = Math.Max(1, childSize.Width) + child.Margin.Horizontal;
            int outerHeight = Math.Max(1, childSize.Height) + child.Margin.Vertical;
            int childPrimary = horizontal ? outerWidth : outerHeight;
            int childSecondary = horizontal ? outerHeight : outerWidth;
            if (WrapContents && !firstInLine && (long)linePrimary + childPrimary > availablePrimary)
            {
                maximumPrimary = Math.Max(maximumPrimary, linePrimary);
                totalSecondary += lineSecondary;
                linePrimary = 0;
                lineSecondary = 0;
            }

            linePrimary += childPrimary;
            lineSecondary = Math.Max(lineSecondary, childSecondary);
            firstInLine = false;
        }

        maximumPrimary = Math.Max(maximumPrimary, linePrimary);
        totalSecondary += lineSecondary;
        return horizontal ? new Size(maximumPrimary, totalSecondary) : new Size(totalSecondary, maximumPrimary);
    }

    private sealed class FlowLayoutEngine : LayoutEngine
    {
        internal static readonly FlowLayoutEngine Instance = new();

        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            ArgumentNullException.ThrowIfNull(container);
            if (container is not FlowLayoutPanel panel)
            {
                return base.Layout(container, layoutEventArgs);
            }

            panel.LayoutFlow();
            return panel.AutoSize;
        }

        internal override Size GetPreferredSize(Control container, Size proposedSize)
            => ((FlowLayoutPanel)container).MeasureFlow(proposedSize);
    }
}
