// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Provides a client-area container for scrollable controls.
/// </summary>
public class ScrollableControl : Control
{
    protected const int ScrollStateAutoScrolling = 1;
    protected const int ScrollStateHScrollVisible = 2;
    protected const int ScrollStateVScrollVisible = 4;
    protected const int ScrollStateUserHasScrolled = 8;
    protected const int ScrollStateFullDrag = 16;

    private readonly HScrollProperties _horizontalScroll;
    private readonly VScrollProperties _verticalScroll;
    private int _scrollState;
    private bool _autoScroll;
    private Size _autoScrollMargin;
    private Size _autoScrollMinSize;
    private int _horizontalOffset;
    private int _verticalOffset;
    private bool _refreshingScrollState;

    /// <summary>Initializes a scrollable container whose descendants participate in validation.</summary>
    public ScrollableControl()
    {
        _horizontalScroll = new HScrollProperties(this);
        _verticalScroll = new VScrollProperties(this);
        SetStyle(ControlStyles.ContainerControl, true);
    }

    /// <inheritdoc />
    public override Rectangle DisplayRectangle
    {
        get
        {
            Size client = ClientSize;
            int viewportWidth = Math.Max(0, client.Width - Padding.Horizontal);
            int viewportHeight = Math.Max(0, client.Height - Padding.Vertical);
            if (!AutoScroll)
            {
                return new Rectangle(Padding.Left, Padding.Top, viewportWidth, viewportHeight);
            }

            GetContentExtent(viewportWidth, viewportHeight, out int contentWidth, out int contentHeight);
            return new Rectangle(
                Padding.Left - _horizontalOffset,
                Padding.Top - _verticalOffset,
                contentWidth,
                contentHeight);
        }
    }

    /// <summary>
    /// Gets or sets whether this container maintains a scrollable content area.
    /// The value is virtual so callers that hold the control through a
    /// <see cref="Control"/> reference observe the same scrollable state.
    /// </summary>
    [Localizable(true)]
    [DefaultValue(false)]
    public override bool AutoScroll
    {
        get => _autoScroll;
        set
        {
            if (_autoScroll == value) { return; }
            _autoScroll = value;
            SetScrollState(ScrollStateAutoScrolling, value);
            if (!value)
            {
                SetScrollOffset(0, 0, ScrollEventType.EndScroll, raiseEvent: false, requestLayout: false);
            }

            RefreshScrollState(requestLayout: true);
        }
    }

    /// <summary>Gets or sets the margin added around the child content.</summary>
    [Localizable(true)]
    public Size AutoScrollMargin
    {
        get => _autoScrollMargin;
        set
        {
            if (value.Width < 0 || value.Height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "AutoScrollMargin cannot be negative.");
            }

            if (_autoScrollMargin == value) { return; }
            _autoScrollMargin = value;
            RefreshScrollState(requestLayout: true);
        }
    }

    /// <summary>Gets or sets the minimum logical content size.</summary>
    [Localizable(true)]
    public Size AutoScrollMinSize
    {
        get => _autoScrollMinSize;
        set
        {
            if (value.Width < 0 || value.Height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "AutoScrollMinSize cannot be negative.");
            }

            if (_autoScrollMinSize == value) { return; }
            _autoScrollMinSize = value;
            RefreshScrollState(requestLayout: true);
        }
    }

    /// <summary>
    /// Gets or sets the logical scroll position. Setters use positive content
    /// coordinates; the getter follows WinForms and returns the negative offset.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Point AutoScrollPosition
    {
        get => new(-_horizontalOffset, -_verticalOffset);
        set => SetScrollOffset(value.X, value.Y, ScrollEventType.ThumbPosition, raiseEvent: true, requestLayout: true);
    }

    /// <summary>Gets the horizontal scroll range.</summary>
    [Browsable(false)]
    public HScrollProperties HorizontalScroll => _horizontalScroll;

    /// <summary>Gets the vertical scroll range.</summary>
    [Browsable(false)]
    public VScrollProperties VerticalScroll => _verticalScroll;

    /// <summary>Gets or sets whether the horizontal scroll bar is visible.</summary>
    protected bool HScroll
    {
        get => GetScrollState(ScrollStateHScrollVisible);
        set => SetScrollState(ScrollStateHScrollVisible, value);
    }

    /// <summary>Gets or sets whether the vertical scroll bar is visible.</summary>
    protected bool VScroll
    {
        get => GetScrollState(ScrollStateVScrollVisible);
        set => SetScrollState(ScrollStateVScrollVisible, value);
    }

    /// <summary>Raised when either scroll axis changes.</summary>
    public event ScrollEventHandler? Scroll;

    /// <summary>Raises a scroll notification for a derived control.</summary>
    protected virtual void OnScroll(ScrollEventArgs se) => Scroll?.Invoke(this, se);

    /// <summary>
    /// Recomputes the logical scroll ranges and the visibility state of the
    /// two standard scroll bars.
    /// </summary>
    protected virtual void AdjustFormScrollbars(bool displayScrollbars)
    {
        RefreshScrollState(requestLayout: false);
        HScroll = displayScrollbars && AutoScroll && _horizontalScroll.Maximum > _horizontalScroll.Minimum;
        VScroll = displayScrollbars && AutoScroll && _verticalScroll.Maximum > _verticalScroll.Minimum;
    }

    /// <summary>Returns whether a standard scroll state bit is set.</summary>
    protected bool GetScrollState(int bit) => (_scrollState & bit) != 0;

    /// <summary>Sets or clears a standard scroll state bit.</summary>
    protected void SetScrollState(int bit, bool value)
    {
        if (value) { _scrollState |= bit; }
        else { _scrollState &= ~bit; }
    }

    /// <summary>
    /// Positions the logical display rectangle. The WinForms contract uses
    /// negative display coordinates for a positive scroll offset.
    /// </summary>
    protected void SetDisplayRectLocation(int x, int y)
        => SetScrollOffset(-x, -y, ScrollEventType.ThumbPosition, raiseEvent: true, requestLayout: true);

    /// <summary>
    /// Scrolls the active descendant into the visible client area. A control
    /// outside this container is ignored, matching the WinForms no-op path.
    /// </summary>
    public void ScrollControlIntoView(Control? activeControl)
    {
        if (activeControl is null || (!ReferenceEquals(activeControl.Parent, this) && !Contains(activeControl)))
        {
            return;
        }

        Point target = ScrollToControl(activeControl);
        AutoScrollPosition = target;
    }

    /// <summary>
    /// Computes the position needed to expose a descendant. Derived containers
    /// can override this method to account for their own content projection.
    /// </summary>
    protected virtual Point ScrollToControl(Control activeControl)
    {
        ArgumentNullException.ThrowIfNull(activeControl);
        if (!ReferenceEquals(activeControl.Parent, this) && !Contains(activeControl)) { return AutoScrollPosition; }

        Rectangle viewport = new(Padding.Left, Padding.Top,
            Math.Max(0, ClientSize.Width - Padding.Horizontal),
            Math.Max(0, ClientSize.Height - Padding.Vertical));
        Rectangle bounds = activeControl.Bounds;
        bounds.Offset(activeControl.AutoScrollOffset);
        for (Control? ancestor = activeControl.Parent; ancestor is not null && !ReferenceEquals(ancestor, this); ancestor = ancestor.Parent)
        {
            bounds.Offset(ancestor.Left, ancestor.Top);
        }

        int x = _horizontalOffset;
        int y = _verticalOffset;
        if (bounds.Left < viewport.Left) { x -= viewport.Left - bounds.Left; }
        else if (bounds.Right > viewport.Right) { x += bounds.Right - viewport.Right; }
        if (bounds.Top < viewport.Top) { y -= viewport.Top - bounds.Top; }
        else if (bounds.Bottom > viewport.Bottom) { y += bounds.Bottom - viewport.Bottom; }
        return new Point(Math.Max(0, x), Math.Max(0, y));
    }

    /// <summary>Sets both scroll margins using the standard two-axis entry point.</summary>
    public void SetAutoScrollMargin(int x, int y)
    {
        if (x < 0) { throw new ArgumentOutOfRangeException(nameof(x)); }
        if (y < 0) { throw new ArgumentOutOfRangeException(nameof(y)); }
        AutoScrollMargin = new Size(x, y);
    }

    internal bool IsScrollAxisEnabled(ScrollOrientation orientation)
        => AutoScroll && (orientation == ScrollOrientation.Horizontal
            ? _horizontalScroll.Maximum > _horizontalScroll.Minimum
            : _verticalScroll.Maximum > _verticalScroll.Minimum);

    internal int GetScrollValue(ScrollOrientation orientation)
        => orientation == ScrollOrientation.Horizontal ? _horizontalOffset : _verticalOffset;

    internal void SetScrollValue(ScrollOrientation orientation, int value, ScrollEventType eventType)
    {
        if (orientation == ScrollOrientation.Horizontal)
        {
            SetScrollOffset(value, _verticalOffset, eventType, raiseEvent: true, requestLayout: true);
        }
        else
        {
            SetScrollOffset(_horizontalOffset, value, eventType, raiseEvent: true, requestLayout: true);
        }
    }

    internal void RefreshScrollState(bool requestLayout)
    {
        if (_refreshingScrollState) { return; }
        _refreshingScrollState = true;
        try
        {
            Size client = ClientSize;
            int viewportWidth = Math.Max(0, client.Width - Padding.Horizontal);
            int viewportHeight = Math.Max(0, client.Height - Padding.Vertical);
            GetContentExtent(viewportWidth, viewportHeight, out int contentWidth, out int contentHeight);
            int maximumX = Math.Max(0, contentWidth - viewportWidth);
            int maximumY = Math.Max(0, contentHeight - viewportHeight);
            _horizontalScroll.SetRange(0, maximumX, viewportWidth);
            _verticalScroll.SetRange(0, maximumY, viewportHeight);
            HScroll = AutoScroll && maximumX > 0;
            VScroll = AutoScroll && maximumY > 0;

            int x = AutoScroll ? Math.Min(Math.Max(0, _horizontalOffset), maximumX) : 0;
            int y = AutoScroll ? Math.Min(Math.Max(0, _verticalOffset), maximumY) : 0;
            bool changed = x != _horizontalOffset || y != _verticalOffset;
            if (changed)
            {
                SetScrollOffset(x, y, ScrollEventType.EndScroll, raiseEvent: true, requestLayout: requestLayout);
                if (requestLayout) { RequestLayout(this, nameof(AutoScrollPosition)); }
            }
            else if (requestLayout)
            {
                RequestLayout(this, nameof(AutoScrollPosition));
            }
        }
        finally { _refreshingScrollState = false; }
    }

    private void SetScrollOffset(int x, int y, ScrollEventType eventType, bool raiseEvent, bool requestLayout)
    {
        if (!AutoScroll)
        {
            x = 0;
            y = 0;
        }
        else
        {
            Size client = ClientSize;
            int viewportWidth = Math.Max(0, client.Width - Padding.Horizontal);
            int viewportHeight = Math.Max(0, client.Height - Padding.Vertical);
            GetContentExtent(viewportWidth, viewportHeight, out int contentWidth, out int contentHeight);
            x = Math.Min(Math.Max(0, x), Math.Max(0, contentWidth - viewportWidth));
            y = Math.Min(Math.Max(0, y), Math.Max(0, contentHeight - viewportHeight));
        }

        int oldX = _horizontalOffset;
        int oldY = _verticalOffset;
        if (oldX == x && oldY == y) { return; }
        _horizontalOffset = x;
        _verticalOffset = y;
        if (raiseEvent) { SetScrollState(ScrollStateUserHasScrolled, true); }
        if (oldX != x) { _horizontalScroll.RecordValue(oldX); }
        if (oldY != y) { _verticalScroll.RecordValue(oldY); }
        if (requestLayout && !_refreshingScrollState) { RequestLayout(this, nameof(AutoScrollPosition)); }
        if (!raiseEvent) { return; }
        if (oldX != x)
        {
            OnScroll(new ScrollEventArgs(eventType, x, oldX, ScrollOrientation.Horizontal));
        }
        if (oldY != y)
        {
            OnScroll(new ScrollEventArgs(eventType, y, oldY, ScrollOrientation.Vertical));
        }
    }

    private void GetContentExtent(int viewportWidth, int viewportHeight, out int contentWidth, out int contentHeight)
    {
        if (!AutoScroll)
        {
            contentWidth = viewportWidth;
            contentHeight = viewportHeight;
            return;
        }

        int left = 0;
        int top = 0;
        int right = 0;
        int bottom = 0;
        foreach (Control child in ChildControls)
        {
            Rectangle bounds = child.Bounds;
            left = Math.Min(left, bounds.Left - Padding.Left - _autoScrollMargin.Width);
            top = Math.Min(top, bounds.Top - Padding.Top - _autoScrollMargin.Height);
            right = Math.Max(right, bounds.Right - Padding.Left + _autoScrollMargin.Width);
            bottom = Math.Max(bottom, bounds.Bottom - Padding.Top + _autoScrollMargin.Height);
        }

        int measuredWidth = Math.Max(0, right - left);
        int measuredHeight = Math.Max(0, bottom - top);
        contentWidth = Math.Max(viewportWidth, Math.Max(_autoScrollMinSize.Width, measuredWidth));
        contentHeight = Math.Max(viewportHeight, Math.Max(_autoScrollMinSize.Height, measuredHeight));
    }

    /// <inheritdoc />
    protected override void OnControlAdded(ControlEventArgs e)
    {
        base.OnControlAdded(e);
        RefreshScrollState(requestLayout: true);
    }

    /// <inheritdoc />
    protected override void OnControlRemoved(ControlEventArgs e)
    {
        base.OnControlRemoved(e);
        RefreshScrollState(requestLayout: true);
    }

    /// <inheritdoc />
    protected override void OnClientSizeChanged(EventArgs e)
    {
        base.OnClientSizeChanged(e);
        RefreshScrollState(requestLayout: true);
    }

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
        RefreshScrollState(requestLayout: false);
    }

    /// <inheritdoc />
    protected override void OnVisibleChanged(EventArgs e)
    {
        if (Visible)
        {
            RequestLayout(this, nameof(Visible));
        }

        base.OnVisibleChanged(e);
    }
}
