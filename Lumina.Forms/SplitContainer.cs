// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.ComponentModel;
using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Represents a two-panel split container.
/// </summary>
public class SplitContainer : ContainerControlBase, ISupportInitialize
{
    private bool _initializing;
    private bool _draggingSplitter;
    private int _dragOffset;
    private int _splitterDistance = 50;
    private int _splitterWidth = 4;
    private int _splitterIncrement = 1;
    private int _panel1MinSize = 25;
    private int _panel2MinSize = 25;
    private Orientation _orientation = Orientation.Vertical;
    private FixedPanel _fixedPanel;
    private bool _panel1Collapsed;
    private bool _panel2Collapsed;
    private int _lastPrimarySize = -1;
    private int _lastPanel1Size;
    private int _lastPanel2Size;
    private double _splitRatio;

    /// <summary>
    /// Initializes a split container with two child panels.
    /// </summary>
    public SplitContainer()
    {
        Panel1 = new Panel();
        Panel2 = new Panel();
        TabStop = true;
        Controls.AddRange(Panel1, Panel2);
    }

    /// <summary>
    /// Gets the first panel.
    /// </summary>
    public Panel Panel1 { get; }

    /// <summary>
    /// Gets the second panel.
    /// </summary>
    public Panel Panel2 { get; }

    /// <summary>
    /// Gets or sets the splitter distance measured from the leading edge of the control.
    /// </summary>
    public int SplitterDistance
    {
        get => _splitterDistance;
        set
        {
            _splitterDistance = Math.Max(0, NormalizeSplitterDistance(value));
            LayoutPanels();
        }
    }

    /// <summary>
    /// Gets or sets the splitter orientation.
    /// </summary>
    public Orientation Orientation
    {
        get => _orientation;
        set
        {
            if (_orientation == value)
            {
                return;
            }

            _orientation = value;
            LayoutPanels();
        }
    }

    /// <summary>
    /// Gets or sets the thickness of the splitter.
    /// </summary>
    public int SplitterWidth
    {
        get => _splitterWidth;
        set
        {
            int normalized = Math.Max(1, value);
            if (_splitterWidth == normalized)
            {
                return;
            }

            _splitterWidth = normalized;
            LayoutPanels();
        }
    }

    /// <summary>
    /// Gets or sets the minimum size of the first panel.
    /// </summary>
    public int Panel1MinSize
    {
        get => _panel1MinSize;
        set
        {
            _panel1MinSize = Math.Max(0, value);
            LayoutPanels();
        }
    }

    /// <summary>
    /// Gets or sets the minimum size of the second panel.
    /// </summary>
    public int Panel2MinSize
    {
        get => _panel2MinSize;
        set
        {
            _panel2MinSize = Math.Max(0, value);
            LayoutPanels();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the splitter is fixed in place.
    /// </summary>
    public bool IsSplitterFixed { get; set; }

    /// <summary>
    /// Gets or sets which panel remains fixed while the control is resized.
    /// </summary>
    public FixedPanel FixedPanel
    {
        get => _fixedPanel;
        set
        {
            if (_fixedPanel == value)
            {
                return;
            }

            _fixedPanel = value;
            LayoutPanels();
        }
    }

    /// <summary>
    /// Gets or sets whether the first panel is collapsed.
    /// </summary>
    public bool Panel1Collapsed
    {
        get => _panel1Collapsed;
        set
        {
            if (_panel1Collapsed == value)
            {
                return;
            }

            _panel1Collapsed = value;
            if (value)
            {
                _panel2Collapsed = false;
            }

            LayoutPanels();
        }
    }

    /// <summary>
    /// Gets or sets whether the second panel is collapsed.
    /// </summary>
    public bool Panel2Collapsed
    {
        get => _panel2Collapsed;
        set
        {
            if (_panel2Collapsed == value)
            {
                return;
            }

            _panel2Collapsed = value;
            if (value)
            {
                _panel1Collapsed = false;
            }

            LayoutPanels();
        }
    }

    /// <summary>
    /// Gets or sets the step interval used while the splitter moves.
    /// </summary>
    public int SplitterIncrement
    {
        get => _splitterIncrement;
        set => _splitterIncrement = Math.Max(1, value);
    }

    /// <summary>
    /// Gets the current bounds of the splitter bar.
    /// </summary>
    public Rectangle SplitterRectangle => GetSplitterBounds();

    /// <summary>
    /// Occurs while the splitter is being moved.
    /// </summary>
    public event SplitterEventHandler? SplitterMoving;

    /// <summary>
    /// Occurs after the splitter has moved.
    /// </summary>
    public event SplitterEventHandler? SplitterMoved;

    /// <inheritdoc />
    protected override string ClassName => "STATIC";

    /// <inheritdoc />
    protected override uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_CLIPCHILDREN | Win32.WS_CLIPSIBLINGS | Win32.SS_NOTIFY;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultBackgroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    private protected override ThemeColorSlot DefaultForegroundSlot => ThemeColorSlot.Surface;

    /// <inheritdoc />
    public void BeginInit()
    {
        _initializing = true;
    }

    /// <inheritdoc />
    public void EndInit()
    {
        _initializing = false;
        LayoutPanels();
    }

    /// <inheritdoc />
    protected override bool HandleWindowMessage(uint message, nint wParam, nint lParam, out nint result)
    {
        Point point = ExtractPoint(lParam);

        switch (message)
        {
            case Win32.WM_LBUTTONDOWN:
                if (!IsSplitterFixed && GetSplitterBounds().Contains(point))
                {
                    Rectangle splitterBounds = GetSplitterBounds();
                    _draggingSplitter = true;
                    _dragOffset = IsVerticalLayout
                        ? point.X - splitterBounds.Left
                        : point.Y - splitterBounds.Top;
                    _ = Win32.SetCapture(NativeHandle);
                    result = 0;
                    return true;
                }

                break;

            case Win32.WM_MOUSEMOVE:
                if (_draggingSplitter)
                {
                    int proposedDistance = ResolveDraggedSplitterDistance(point);
                    SplitterDistance = proposedDistance;
                    SplitterMoving?.Invoke(this, CreateSplitterEventArgs(point, proposedDistance));
                    result = 0;
                    return true;
                }

                break;

            case Win32.WM_LBUTTONUP:
                if (_draggingSplitter)
                {
                    int proposedDistance = ResolveDraggedSplitterDistance(point);
                    SplitterDistance = proposedDistance;
                    StopDraggingSplitter();
                    SplitterMoved?.Invoke(this, CreateSplitterEventArgs(point, _splitterDistance));
                    result = 0;
                    return true;
                }

                break;

            case Win32.WM_SETCURSOR:
                if (!IsSplitterFixed
                    && Win32.LowWord(lParam) == Win32.HTCLIENT
                    && NativeHandle != 0
                    && Win32.GetCursorPos(out var cursor)
                    && IsSplitterHot(new Point(cursor.x, cursor.y)))
                {
                    nint cursorHandle = Win32.LoadCursorW(0, (nint)(IsVerticalLayout ? Win32.IDC_SIZEWE : Win32.IDC_SIZENS));
                    if (cursorHandle != 0)
                    {
                        _ = Win32.SetCursor(cursorHandle);
                        result = (nint)1;
                        return true;
                    }
                }

                break;
        }

        return base.HandleWindowMessage(message, wParam, lParam, out result);
    }

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        ApplyNativeThemeState();
        LayoutPanels();
    }

    /// <inheritdoc />
    protected override void OnDisposing()
    {
        StopDraggingSplitter();
        base.OnDisposing();
    }

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs e)
    {
        // Control can raise layout while applying derived default constraints,
        // before this constructor has created the two panels.
        if (Panel1 is null || Panel2 is null)
        {
            base.OnLayout(e);
            return;
        }

        // Resize layout commits both panel bounds before either panel resumes.
        // Direct splitter-property updates retain their immediate panel updates.
        Panel1.SuspendLayout();
        Panel2.SuspendLayout();
        try
        {
            LayoutPanels(updateRatio: false);
        }
        finally
        {
            try { Panel1.ResumeLayout(); }
            finally { Panel2.ResumeLayout(); }
        }

        base.OnLayout(e);
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
        if (Panel1.NativeHandle != 0)
        {
            DarkModeNative.ApplyThemeToWindow(Panel1.NativeHandle, CurrentVisualStyle.IsDarkMode);
        }

        if (Panel2.NativeHandle != 0)
        {
            DarkModeNative.ApplyThemeToWindow(Panel2.NativeHandle, CurrentVisualStyle.IsDarkMode);
        }
    }

    private bool IsVerticalLayout => Orientation == Orientation.Vertical;

    private void LayoutPanels(bool updateRatio = true)
    {
        if (_initializing)
        {
            return;
        }

        // A direct splitter-property update changes both panels immediately.
        // Reuse an existing transaction so an extra resume cannot clear its
        // cached reason before a later resize request arrives.
        bool needsSuspension = LayoutSuspendCount == 0;
        if (needsSuspension)
        {
            SuspendLayout();
        }

        try
        {
            LayoutPanelsCore(updateRatio);
        }
        finally
        {
            if (needsSuspension)
            {
                ResumeLayout(false);
            }
        }
    }

    private void LayoutPanelsCore(bool updateRatio)
    {
        Rectangle contentBounds = new(
            Padding.Left,
            Padding.Top,
            Math.Max(1, Width - Padding.Horizontal),
            Math.Max(1, Height - Padding.Vertical));

        int splitterWidth = Math.Max(1, SplitterWidth);
        int primarySize = IsVerticalLayout ? contentBounds.Width : contentBounds.Height;
        if (_panel1Collapsed || _panel2Collapsed)
        {
            LayoutCollapsedPanels(contentBounds);
            _lastPrimarySize = primarySize;
            return;
        }

        Panel1.Visible = true;
        Panel2.Visible = true;

        int desiredDistance = updateRatio ? _splitterDistance : ResolveSplitterDistanceForResize(primarySize, splitterWidth);
        int firstSize = ClampSplitterDistance(desiredDistance, primarySize, splitterWidth);
        _splitterDistance = firstSize;
        int secondSize = Math.Max(0, primarySize - firstSize - splitterWidth);

        Size firstPanelSize = IsVerticalLayout
            ? new Size(Math.Max(1, firstSize), Math.Max(1, contentBounds.Height))
            : new Size(Math.Max(1, contentBounds.Width), Math.Max(1, firstSize));
        Size secondPanelSize = IsVerticalLayout
            ? new Size(Math.Max(1, secondSize), Math.Max(1, contentBounds.Height))
            : new Size(Math.Max(1, contentBounds.Width), Math.Max(1, secondSize));
        Point secondPanelLocation = IsVerticalLayout
            ? new Point(contentBounds.Left + firstSize + splitterWidth, contentBounds.Top)
            : new Point(contentBounds.Left, contentBounds.Top + firstSize + splitterWidth);

        if (updateRatio)
        {
            // A distance edit updates Panel1 immediately. Panel2's size and
            // location then form one layout transaction of their own.
            SetPanelBounds(Panel1, new Rectangle(contentBounds.Location, firstPanelSize));
            Panel2.SuspendLayout();
            try
            {
                SetPanelBounds(Panel2, new Rectangle(Panel2.Location, secondPanelSize));
                SetPanelBounds(Panel2, new Rectangle(secondPanelLocation, secondPanelSize));
            }
            finally { Panel2.ResumeLayout(); }
        }
        else
        {
            // Match the reference's genuine bounds operations: a fixed trailing
            // panel is resized first; the unfixed leading panel changes its
            // primary and secondary dimensions separately.
            if (FixedPanel == FixedPanel.Panel2)
            {
                SetPanelBounds(Panel2, new Rectangle(Panel2.Location, secondPanelSize));
            }

            if (FixedPanel == FixedPanel.Panel1)
            {
                SetPanelBounds(Panel1, new Rectangle(Panel1.Location, firstPanelSize));
            }
            else
            {
                Size primarySizeOnly = IsVerticalLayout
                    ? new Size(firstPanelSize.Width, Panel1.Height)
                    : new Size(Panel1.Width, firstPanelSize.Height);
                SetPanelBounds(Panel1, new Rectangle(Panel1.Location, primarySizeOnly));
                SetPanelBounds(Panel1, new Rectangle(Panel1.Location, firstPanelSize));
            }

            if (FixedPanel != FixedPanel.Panel2)
            {
                SetPanelBounds(Panel2, new Rectangle(Panel2.Location, secondPanelSize));
            }

            SetPanelBounds(Panel1, new Rectangle(contentBounds.Location, firstPanelSize));
            SetPanelBounds(Panel2, new Rectangle(secondPanelLocation, secondPanelSize));
        }

        _lastPrimarySize = primarySize;
        _lastPanel1Size = firstSize;
        _lastPanel2Size = secondSize;
        if (updateRatio && firstSize > 0)
        {
            // The reference preserves the ratio of the entire primary axis to
            // Panel1. Resizing must not replace it with the rounded result.
            _splitRatio = primarySize / (double)firstSize;
        }

        Invalidate();
    }

    private static void SetPanelBounds(Panel panel, Rectangle bounds)
        => panel.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);

    private Rectangle GetSplitterBounds()
    {
        if (_panel1Collapsed || _panel2Collapsed)
        {
            return Rectangle.Empty;
        }

        Rectangle contentBounds = new(
            Padding.Left,
            Padding.Top,
            Math.Max(1, Width - Padding.Horizontal),
            Math.Max(1, Height - Padding.Vertical));

        return IsVerticalLayout
            ? new Rectangle(contentBounds.Left + _splitterDistance, contentBounds.Top, Math.Max(1, SplitterWidth), Math.Max(1, contentBounds.Height))
            : new Rectangle(contentBounds.Left, contentBounds.Top + _splitterDistance, Math.Max(1, contentBounds.Width), Math.Max(1, SplitterWidth));
    }

    private bool IsSplitterHot(Point screenPoint)
    {
        Rectangle splitterBounds = GetSplitterBounds();
        if (NativeHandle != 0 && Win32.GetWindowRect(NativeHandle, out var rect))
        {
            splitterBounds.Offset(rect.Left, rect.Top);
        }

        return splitterBounds.Contains(screenPoint);
    }

    private int ResolveDraggedSplitterDistance(Point point)
    {
        int coordinate = IsVerticalLayout ? point.X : point.Y;
        int availablePrimarySize = IsVerticalLayout
            ? Math.Max(0, Width - Padding.Horizontal)
            : Math.Max(0, Height - Padding.Vertical);
        return ClampSplitterDistance(NormalizeSplitterDistance(coordinate - _dragOffset), availablePrimarySize, Math.Max(1, SplitterWidth));
    }

    private int ClampSplitterDistance(int proposedDistance, int availablePrimarySize, int splitterWidth)
    {
        int usablePrimarySize = Math.Max(0, availablePrimarySize);
        int maxDistance = Math.Max(0, usablePrimarySize - splitterWidth - Panel2MinSize);
        int minDistance = Math.Min(Panel1MinSize, Math.Max(0, usablePrimarySize - splitterWidth));

        if (maxDistance < minDistance)
        {
            minDistance = Math.Max(0, usablePrimarySize - splitterWidth);
            maxDistance = minDistance;
        }

        return Math.Clamp(proposedDistance, minDistance, maxDistance);
    }

    private int NormalizeSplitterDistance(int distance)
    {
        int increment = Math.Max(1, SplitterIncrement);
        return Math.Max(0, (int)Math.Round(distance / (double)increment) * increment);
    }

    private int ResolveSplitterDistanceForResize(int primarySize, int splitterWidth)
    {
        if (_lastPrimarySize <= 0 || primarySize == _lastPrimarySize || _draggingSplitter)
        {
            return _splitterDistance;
        }

        return FixedPanel switch
        {
            FixedPanel.Panel1 => _lastPanel1Size,
            FixedPanel.Panel2 => Math.Max(0, primarySize - splitterWidth - _lastPanel2Size),
            _ => _splitRatio > 0 ? (int)Math.Floor(primarySize / _splitRatio) : _splitterDistance,
        };
    }

    private void LayoutCollapsedPanels(Rectangle contentBounds)
    {
        if (_panel1Collapsed)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
            Panel2.SetBounds(contentBounds.Left, contentBounds.Top, contentBounds.Width, contentBounds.Height);
            return;
        }

        Panel1.Visible = true;
        Panel2.Visible = false;
        Panel1.SetBounds(contentBounds.Left, contentBounds.Top, contentBounds.Width, contentBounds.Height);
    }

    private SplitterEventArgs CreateSplitterEventArgs(Point point, int distance)
    {
        return IsVerticalLayout
            ? new SplitterEventArgs(point.X, point.Y, distance, 0)
            : new SplitterEventArgs(point.X, point.Y, 0, distance);
    }

    private void StopDraggingSplitter()
    {
        _draggingSplitter = false;
        if (NativeHandle != 0 && Win32.GetCapture() == NativeHandle)
        {
            _ = Win32.ReleaseCapture();
        }
    }
}
