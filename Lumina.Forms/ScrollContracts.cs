namespace Lumina.Forms;

/// <summary>Identifies the axis of a scroll operation.</summary>
public enum ScrollOrientation
{
    Horizontal = 0,
    Vertical = 1,
}

/// <summary>Describes how a scroll position changed.</summary>
public enum ScrollEventType
{
    SmallDecrement = 0,
    SmallIncrement = 1,
    LargeDecrement = 2,
    LargeIncrement = 3,
    ThumbPosition = 4,
    ThumbTrack = 5,
    First = 6,
    Last = 7,
    EndScroll = 8,
}

/// <summary>Contains the old and new values for a scroll notification.</summary>
public class ScrollEventArgs : EventArgs
{
    public ScrollEventArgs(ScrollEventType type, int newValue, ScrollOrientation scrollOrientation)
        : this(type, newValue, newValue, scrollOrientation)
    {
    }

    public ScrollEventArgs(ScrollEventType type, int newValue, int oldValue, ScrollOrientation scrollOrientation)
    {
        Type = type;
        NewValue = newValue;
        OldValue = oldValue;
        ScrollOrientation = scrollOrientation;
    }

    public int NewValue { get; }

    public int OldValue { get; }

    public ScrollEventType Type { get; }

    public ScrollOrientation ScrollOrientation { get; }
}

/// <summary>Handles a scroll notification.</summary>
public delegate void ScrollEventHandler(object? sender, ScrollEventArgs e);

/// <summary>
/// Exposes the standard range and position contract for one axis of a
/// <see cref="ScrollableControl"/>. The control owns the actual range and
/// keeps the values synchronized with its client area and content.
/// </summary>
public abstract class ScrollProperties
{
    private readonly ScrollableControl _parentControl;
    private readonly ScrollOrientation _orientation;
    private int _largeChange;
    private int _smallChange = 1;
    private int _minimum;
    private int _maximum;
    private int _oldValue;

    protected ScrollProperties(ScrollableControl container, ScrollOrientation orientation)
    {
        _parentControl = container ?? throw new ArgumentNullException(nameof(container));
        _orientation = orientation;
    }

    /// <summary>Gets whether the position is at the end of the current range.</summary>
    public bool IsEnd => Value >= Maximum;

    /// <summary>Gets whether scrolling is currently enabled for this axis.</summary>
    public bool IsEnabled => _parentControl.IsScrollAxisEnabled(_orientation);

    /// <summary>Gets or sets the viewport-sized increment.</summary>
    public int LargeChange
    {
        get => _largeChange;
        set
        {
            int normalized = Math.Max(0, value);
            if (_largeChange == normalized) { return; }
            _largeChange = normalized;
            _parentControl.RefreshScrollState(requestLayout: false);
        }
    }

    /// <summary>Gets or sets the largest position in the range.</summary>
    public int Maximum
    {
        get => _maximum;
        set
        {
            int normalized = Math.Max(_minimum, value);
            if (_maximum == normalized) { return; }
            _maximum = normalized;
            _parentControl.RefreshScrollState(requestLayout: false);
        }
    }

    /// <summary>Gets or sets the smallest position in the range.</summary>
    public int Minimum
    {
        get => _minimum;
        set
        {
            int normalized = Math.Max(0, value);
            if (_minimum == normalized) { return; }
            _minimum = normalized;
            if (_maximum < normalized) { _maximum = normalized; }
            _parentControl.RefreshScrollState(requestLayout: false);
        }
    }

    /// <summary>Gets the value before the most recent value assignment.</summary>
    public int OldValue => _oldValue;

    /// <summary>Gets or sets the single-line increment.</summary>
    public int SmallChange
    {
        get => _smallChange;
        set => _smallChange = Math.Max(0, value);
    }

    /// <summary>Gets the orientation represented by this object.</summary>
    public ScrollOrientation ScrollOrientation => _orientation;

    /// <summary>Gets or sets the current scroll position.</summary>
    public int Value
    {
        get => _parentControl.GetScrollValue(_orientation);
        set => _parentControl.SetScrollValue(_orientation, value, ScrollEventType.ThumbPosition);
    }

    internal int StoredLargeChange => _largeChange;
    internal int StoredMaximum => _maximum;
    internal int StoredMinimum => _minimum;

    internal void SetRange(int minimum, int maximum, int largeChange)
    {
        _minimum = Math.Max(0, minimum);
        _maximum = Math.Max(_minimum, maximum);
        _largeChange = Math.Max(0, largeChange);
    }

    internal void RecordValue(int value) => _oldValue = value;
}

/// <summary>Scroll range for the horizontal axis.</summary>
public sealed class HScrollProperties : ScrollProperties
{
    public HScrollProperties(ScrollableControl container)
        : base(container, ScrollOrientation.Horizontal)
    {
    }
}

/// <summary>Scroll range for the vertical axis.</summary>
public sealed class VScrollProperties : ScrollProperties
{
    public VScrollProperties(ScrollableControl container)
        : base(container, ScrollOrientation.Vertical)
    {
    }
}
