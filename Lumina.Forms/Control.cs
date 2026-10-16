// Layout, appearance, message and painting portions adapted from dotnet/winforms v10.0.11.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses those portions under the MIT license.

using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Lumina.Forms.Layout;

namespace Lumina.Forms;

/// <summary>
/// Provides the common WinForms-like surface for all LuminaForms controls backed by Win32 child windows.
/// </summary>
public class Control : Component, IWin32Window, ISynchronizeInvoke
{
    private static readonly Lock s_subclassLock = new();
    private static readonly Dictionary<nint, Control> s_controlsByHandle = [];
    private static readonly Win32.WindowProc s_subclassProc = SubclassWindowProcThunk;
    private static readonly Lazy<Font> s_defaultFont = new(static () => SystemFonts.MessageBoxFont!);
    private const string DefaultWindowClassName = "Lumina.Forms.Control";
    [ThreadStatic]
    private static Control? s_creatingControl;

    private string _text = string.Empty;
    private int _left;
    private int _top;
    private int _width;
    private int _height;
    private int _clientWidth;
    private int _clientHeight;
    private bool _enabled = true;
    private bool _visible = true;
    private bool _disposed;
    private bool _disposing;
    private bool _autoScroll;
    private bool _autoEllipsis;
    private AutoSizeMode _autoSizeMode;
    private Padding _padding;
    private Padding _margin = new(3);
    private DockStyle _dock;
    private AnchorStyles _anchor = AnchorStyles.Top | AnchorStyles.Left;
    private Color _backColor = Color.Empty;
    private Color _foreColor = Color.Empty;
    private Image? _backgroundImage;
    private ImageLayout _backgroundImageLayout = ImageLayout.Tile;
    private Font? _font;
    private int? _fontHeight;
    private nint _nativeFontHandle;
    private Font? _nativeFontSource;
    private bool _nativeFontDirty;
    private Size? _minimumSize;
    private Size? _maximumSize;
    private nint _backgroundBrush;
    private bool _ownsBackgroundBrush;
    private nint _originalWindowProc;
    private volatile WindowThreadDispatcher? _threadDispatcher;
    private long _lastContextMenuShownTick = long.MinValue;
    private Point _lastContextMenuScreenLocation = new(int.MinValue, int.MinValue);
    private Control? _parent;
    private ControlCollection? _controls;
    private int _tabIndex = -1;
    private bool _topLevel;
    private nint _parkingWindow;
    private bool _applyingBounds;
    private nint _handle;
    private bool _created;
    private bool _creatingHandle;
    private bool _recreatingHandle;
    private bool _destroyingHandle;
    private bool _handleDestroyedNotified;
    private bool _parentRecreatingHandle;
    private byte _layoutSuspendCount;
    private bool _layoutDeferred;
    private bool _clearLayoutArgs;
    private bool _layoutDirty;
    private LayoutEventArgs? _cachedLayoutEventArgs;
    private bool _autoSize;
    private Rectangle? _specifiedBounds;
    private Size _preferredSizeCache;
    private CreateParams? _createParams;
    private string? _lastLegacyClassName;
    private ControlStyles _controlStyle = ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
        | ControlStyles.StandardClick | ControlStyles.StandardDoubleClick
        | ControlStyles.UseTextForAccessibility | ControlStyles.Selectable;
    private bool _exceptionWhilePainting;

    internal bool CachePreferredSize { get; set; }
    internal bool ParticipatesInLayout => _visible;
    internal bool LayoutAutoSize => _autoSize;
    internal DockStyle LayoutDock => _dock;
    internal AnchorStyles LayoutAnchor => _anchor;
    internal AutoSizeMode LayoutAutoSizeMode => GetAutoSizeMode();
    internal Rectangle SpecifiedBounds => _specifiedBounds ?? Bounds;
    internal DefaultLayout.AnchorInfo? LayoutAnchorInfo { get; set; }
    internal bool ForceAnchorCalculations { get; set; }
    internal bool ChildrenNeedAnchorLayout { get; set; }
    internal void ClearPreferredSizeCache() => _preferredSizeCache = Size.Empty;

    /// <summary>
    /// Initializes a control and installs the thread's UI synchronization context when enabled.
    /// </summary>
    public Control()
    {
        // These extensions and setters are virtual. Preserve their construction
        // order and repeated reads while the initial bounds are still empty.
        if (DefaultMinimumSize != Size.Empty)
        {
            MinimumSize = DefaultMinimumSize;
        }

        if (DefaultMaximumSize != Size.Empty)
        {
            MaximumSize = DefaultMaximumSize;
        }

        // Construction assigns the default directly instead of routing through
        // bounds constraints or notifications. Reentrant defaults may already
        // have changed the location or client cache, so keep those values.
        Size defaultSize = DefaultSize;
        _width = defaultSize.Width;
        _height = defaultSize.Height;
        if (_width != 0 && _height != 0)
        {
            Size nonClient = GetNonClientSize();
            _clientWidth = unchecked(_width - nonClient.Width);
            _clientHeight = unchecked(_height - nonClient.Height);
        }

        ClearPreferredSizeCache();

        WindowsFormsSynchronizationContext.InstallIfNeeded();
    }

    /// <summary>
    /// Initializes a control with the specified text.
    /// </summary>
    public Control(string? text) : this()
    {
        Text = text;
    }

    /// <summary>
    /// Initializes a control with the specified text, location, and size.
    /// </summary>
    public Control(string? text, int left, int top, int width, int height) : this(text)
    {
        // Location and size are separate observable operations. Each consumes
        // the current bounds, virtual constraints, and standard notifications.
        Location = new Point(left, top);
        Size = new Size(width, height);
    }

    public Control(Control? parent, string? text) : this()
    {
        Parent = parent;
        Text = text;
    }

    public Control(Control? parent, string? text, int left, int top, int width, int height) : this(parent, text)
    {
        Location = new Point(left, top);
        Size = new Size(width, height);
    }

    /// <summary>
    /// Gets or sets the design-time or lookup name of the control.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the native window handle, creating it on first access.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DispId(-515)]
    public nint Handle
    {
        get
        {
            if (_handle == 0) { CreateHandle(); }
            return _handle;
        }
    }

    // Native existence checks must never materialize a window.
    internal nint NativeHandle => _handle;

    internal byte LayoutSuspendCount => _layoutSuspendCount;

    /// <summary>
    /// Gets or sets an arbitrary user-defined value associated with the control.
    /// </summary>
    public object? Tag { get; set; }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsHandleCreated => _handle != 0;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Created => _created;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool RecreatingHandle => _recreatingHandle;

    /// <summary>
    /// Gets whether the control's native window has the keyboard focus.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool Focused => NativeHandle != 0 && Win32.GetFocus() == NativeHandle;

    /// <summary>
    /// Gets whether this control or one of its native child windows has the keyboard focus.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ContainsFocus
    {
        get
        {
            nint handle = NativeHandle;
            if (handle == 0)
            {
                return false;
            }

            nint focusedHandle = Win32.GetFocus();
            return focusedHandle != 0
                && (focusedHandle == handle || Win32.IsChild(handle, focusedHandle));
        }
    }

    /// <summary>
    /// Gets whether the control has a visible, enabled native window that can receive focus.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanFocus => NativeHandle != 0 && Win32.IsWindowVisible(NativeHandle) && Win32.IsWindowEnabled(NativeHandle);

    /// <summary>
    /// Gets whether the calling thread differs from the thread that created the control's
    /// native window, or an available parent window when this control has no handle.
    /// </summary>
    public virtual bool InvokeRequired => FindMarshalingDispatcher()?.InvokeRequired ?? false;

    /// <summary>
    /// Gets or sets the context menu associated with the control.
    /// </summary>
    public ContextMenuStrip? ContextMenuStrip { get; set; }

    /// <summary>
    /// Gets or sets the tab order index used by designer-style layout code.
    /// </summary>
    public int TabIndex
    {
        get => _tabIndex == -1 ? 0 : _tabIndex;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            _tabIndex = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control sizes itself to its contents.
    /// </summary>
    public virtual bool AutoSize
    {
        get => _autoSize;
        set
        {
            if (value == AutoSize) { return; }
            _autoSize = value;
            if (!value) { SetBoundsFromLayout(SpecifiedBounds, BoundsSpecified.None); }
            if (value && _parent?.LayoutEngine == DefaultLayout.Instance)
            {
                _parent.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
            }
            RequestLayout(_parent, this, nameof(AutoSize));
            OnAutoSizeChanged(EventArgs.Empty);
        }
    }

    public AutoSizeMode AutoSizeMode
    {
        get => _autoSizeMode;
        set => _autoSizeMode = value;
    }

    protected void SetAutoSizeMode(AutoSizeMode mode)
        => _autoSizeMode = mode == AutoSizeMode.GrowAndShrink ? mode : AutoSizeMode.GrowOnly;

    protected AutoSizeMode GetAutoSizeMode()
        => _autoSizeMode == AutoSizeMode.GrowAndShrink ? _autoSizeMode : AutoSizeMode.GrowOnly;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual LayoutEngine LayoutEngine => DefaultLayout.Instance;

    [Browsable(false)]
    public Size PreferredSize => GetPreferredSize(Size.Empty);

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual Size GetPreferredSize(Size proposedSize)
    {
        if (_disposing || _disposed) { return _preferredSizeCache; }
        proposedSize = ApplySizeConstraints(new Size(
            proposedSize.Width == 0 ? int.MaxValue : proposedSize.Width,
            proposedSize.Height == 0 ? int.MaxValue : proposedSize.Height));
        bool unconstrained = proposedSize == new Size(int.MaxValue, int.MaxValue);
        if (CachePreferredSize && unconstrained && !_preferredSizeCache.IsEmpty) { return _preferredSizeCache; }
        Size preferred = ApplySizeConstraints(GetPreferredSizeCore(proposedSize));
        if (CachePreferredSize && unconstrained) { _preferredSizeCache = preferred; }
        return preferred;
    }

    internal virtual Size GetPreferredSizeCore(Size proposedSize) => SpecifiedBounds.Size;

    private Size ApplySizeConstraints(Size proposedSize)
    {
        if (MaximumSize != Size.Empty || MinimumSize != Size.Empty)
        {
            Size maximum = MaximumSize;
            proposedSize.Width = Math.Min(proposedSize.Width, maximum.Width == 0 ? int.MaxValue : maximum.Width);
            proposedSize.Height = Math.Min(proposedSize.Height, maximum.Height == 0 ? int.MaxValue : maximum.Height);
            Size minimum = MinimumSize;
            proposedSize.Width = Math.Max(proposedSize.Width, minimum.Width);
            proposedSize.Height = Math.Max(proposedSize.Height, minimum.Height);
        }
        return proposedSize;
    }

    public bool AutoScroll
    {
        get => _autoScroll;
        set => _autoScroll = value;
    }

    public bool AutoEllipsis
    {
        get => _autoEllipsis;
        set => _autoEllipsis = value;
    }

    public virtual AnchorStyles Anchor
    {
        get => _anchor;
        set
        {
            AnchorStyles previous = _anchor;
            if (previous == value) { return; }
            if (_dock != DockStyle.None && ParticipatesInLayout) { SetDockCore(DockStyle.None); }
            // WinForms stores the translated anchor in a four-bit section.
            int stored = value == AnchorStyles.None ? 5 : value == (AnchorStyles.Top | AnchorStyles.Left) ? 0 : (int)value;
            stored &= 15;
            _anchor = stored == 0 ? AnchorStyles.Top | AnchorStyles.Left : stored == 5 ? AnchorStyles.None : (AnchorStyles)stored;
            _dock = DockStyle.None;
            if (_anchor != (AnchorStyles.Top | AnchorStyles.Left) || _autoSize) { DefaultLayout.UpdateAnchorInfo(this); }
            else { LayoutAnchorInfo = null; }
            if (((previous & ~value) & (AnchorStyles.Right | AnchorStyles.Bottom)) != 0)
            {
                RequestLayout(_parent?._parent, this, nameof(Anchor));
            }
            RequestLayout(_parent, this, nameof(Anchor));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can move the focus to this control with the Tab key.
    /// </summary>
    public bool TabStop { get; set; } = true;

    /// <summary>
    /// Gets the default background color of a top-level control.
    /// </summary>
    public static Color DefaultBackColor => SystemColors.Control;

    /// <summary>
    /// Gets the default font of a top-level control.
    /// </summary>
    public static Font DefaultFont => s_defaultFont.Value;

    /// <summary>
    /// Gets the default foreground color of a top-level control.
    /// </summary>
    public static Color DefaultForeColor => SystemColors.ControlText;

    /// <summary>
    /// Gets or sets the font. A null value restores inheritance from the parent.
    /// </summary>
    [Category("Appearance")]
    [Localizable(true)]
    [DispId(-512)]
    [AmbientValue(null)]
    [AllowNull]
    public virtual Font Font
    {
        get => GetCurrentFont();
        set
        {
            // Equality of explicit values preserves the original borrowed Font
            // reference. An equal inherited font still establishes a local value.
            if (Equals(_font, value)) { return; }
            Font previous = Font;
            _font = value;
            InvalidateNativeFont();
            if (previous.Equals(value))
            {
                if (NativeHandle != 0) { ApplyFontToHandle(); }
                return;
            }

            if (_fontHeight.HasValue) { _fontHeight = value is null ? -1 : value.Height; }
            using LayoutTransaction transaction = new(_parent, this, nameof(Font));
            OnFontChanged(EventArgs.Empty);
        }
    }

    // The ambient font traverses stored parent fonts; it deliberately does not
    // consume a parent's override of the public Font getter.
    private Font GetCurrentFont() => _font ?? _parent?.GetCurrentFont() ?? DefaultFont;

    /// <summary>
    /// Gets or sets the cached height of the control's font.
    /// </summary>
    protected int FontHeight
    {
        get
        {
            if (_fontHeight is { } height && height != -1) { return height; }
            if (_font is not null) { return (_fontHeight = _font.Height).Value; }
            int inheritedHeight = _parent?.FontHeight ?? -1;
            return inheritedHeight != -1 ? inheritedHeight : (_fontHeight = Font.Height).Value;
        }
        set => _fontHeight = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void ResetFont() => Font = null;

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal virtual bool ShouldSerializeFont() => _font is not null;

    /// <summary>
    /// Gets or sets the minimum size of the control.
    /// </summary>
    [Localizable(true)]
    [Category("Layout")]
    [Description("Specifies the minimum size of the control.")]
    public virtual Size MinimumSize
    {
        get
        {
            // The default extension is evaluated even when an explicit value is
            // present. Derived controls can observe its calls and exceptions.
            Size defaultSize = DefaultMinimumSize;
            return _minimumSize ?? defaultSize;
        }
        set
        {
            if (MinimumSize == value)
            {
                return;
            }

            _minimumSize = value;
            using LayoutTransaction transaction = new(_parent, this, nameof(MinimumSize));
            Size currentSize = Size;
            SetBoundsCore(_left, _top, Math.Max(currentSize.Width, value.Width),
                Math.Max(currentSize.Height, value.Height), BoundsSpecified.Size);
        }
    }

    /// <summary>
    /// Gets or sets the maximum size of the control. A zero dimension leaves that axis unbounded.
    /// </summary>
    [Localizable(true)]
    [Category("Layout")]
    [Description("Specifies the maximum size of the control.")]
    [AmbientValue(typeof(Size), "0, 0")]
    public virtual Size MaximumSize
    {
        get
        {
            Size defaultSize = DefaultMaximumSize;
            return _maximumSize ?? defaultSize;
        }
        set
        {
            // Clearing the maximum removes the stored override without consuming
            // its getter or resizing. MinimumSize explicitly stores Size.Empty.
            if (value == Size.Empty)
            {
                _maximumSize = null;
                return;
            }

            if (MaximumSize == value)
            {
                return;
            }

            _maximumSize = value;
            Size currentSize = Size;
            SetBoundsCore(_left, _top, Math.Min(currentSize.Width, value.Width),
                Math.Min(currentSize.Height, value.Height), BoundsSpecified.Size);
            RequestLayout(_parent, this, nameof(MaximumSize));
        }
    }

    /// <summary>
    /// Gets the default minimum size of the control.
    /// </summary>
    protected virtual Size DefaultMinimumSize => Size.Empty;

    /// <summary>
    /// Gets the default maximum size of the control.
    /// </summary>
    protected virtual Size DefaultMaximumSize => Size.Empty;

    /// <summary>
    /// Gets the default size of the control.
    /// </summary>
    protected virtual Size DefaultSize => Size.Empty;

    /// <summary>
    /// Gets or sets the interior spacing between the control border and its content area.
    /// </summary>
    public Padding Padding
    {
        get => _padding;
        set
        {
            if (_padding == value)
            {
                return;
            }

            _padding = ClampPadding(value);
            _layoutDirty = true;
            using (new LayoutTransaction(_parent, this, nameof(Padding)))
            {
                OnPaddingChanged(EventArgs.Empty);
            }
            if (_layoutDirty) { RequestLayout(this, nameof(Padding)); }
        }
    }

    /// <summary>
    /// Gets or sets the exterior spacing between this control and surrounding layout.
    /// </summary>
    public Padding Margin
    {
        get => _margin;
        set
        {
            value = ClampPadding(value);
            if (_margin == value)
            {
                return;
            }

            _margin = value;
            RequestLayout(_parent, this, nameof(Margin));
            OnMarginChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets which edge of the parent container the control is docked to.
    /// </summary>
    public virtual DockStyle Dock
    {
        get => _dock;
        set
        {
            if (Dock == value)
            {
                return;
            }

            if (value < DockStyle.None || value > DockStyle.Fill)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(DockStyle));
            }
            SuspendLayout();
            try
            {
                SetDockCore(value);
                OnDockChanged(EventArgs.Empty);
            }
            finally { ResumeLayout(); }
        }
    }

    internal void SetDockCore(DockStyle value)
    {
        bool wasDocked = _dock != DockStyle.None && ParticipatesInLayout;
        _dock = value;
        _anchor = AnchorStyles.Top | AnchorStyles.Left;
        using (new LayoutTransaction(_parent, this, nameof(Dock)))
        {
            if (value != DockStyle.None) { SetBoundsFromLayout(SpecifiedBounds, BoundsSpecified.All); }
            else if (wasDocked)
            {
                SetBoundsFromLayout(SpecifiedBounds, BoundsSpecified.None);
                if (_autoSize) { DefaultLayout.UpdateAnchorInfo(this); }
            }
        }
    }

    private static Padding ClampPadding(Padding value)
        => new(Math.Max(0, value.Left), Math.Max(0, value.Top), Math.Max(0, value.Right), Math.Max(0, value.Bottom));

    /// <summary>
    /// Gets a value indicating whether the control has been disposed.
    /// </summary>
    public bool IsDisposed => _disposed;

    /// <summary>
    /// Gets a value indicating whether the control is releasing its managed resources.
    /// </summary>
    public bool Disposing => _disposing;

    /// <summary>
    /// Gets or sets the parent control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? Parent
    {
        get => _parent;
        set
        {
            if (_parent == value) { return; }
            if (value is not null) { value.Controls.Add(this); }
            else { _parent?.Controls.Remove(this); }
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ControlCollection Controls => _controls ??= CreateControlsInstance();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasChildren => _controls is { Count: > 0 };

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? TopLevelControl
    {
        get
        {
            for (Control? control = this; control is not null; control = control._parent)
            {
                if (control.GetTopLevel()) { return control; }
            }
            return null;
        }
    }

    public event EventHandler? ParentChanged;
    public event EventHandler? EnabledChanged;
    public event EventHandler? VisibleChanged;
    public event ControlEventHandler? ControlAdded;
    public event ControlEventHandler? ControlRemoved;
    public event LayoutEventHandler? Layout;
    public event EventHandler? PaddingChanged;
    public event EventHandler? MarginChanged;
    public event EventHandler? DockChanged;
    public event EventHandler? AutoSizeChanged;

    protected virtual ControlCollection CreateControlsInstance() => new(this);

    public bool Contains(Control? ctl)
    {
        for (Control? control = ctl?._parent; control is not null; control = control._parent)
        {
            if (control == this) { return true; }
        }
        return false;
    }

    public Form? FindForm()
    {
        for (Control? control = this; control is not null; control = control._parent)
        {
            if (control is Form form) { return form; }
        }
        return null;
    }

    protected bool GetTopLevel() => _topLevel;

    protected void SetTopLevel(bool value)
    {
        if (_topLevel == value) { return; }
        if (value && _parent is not null)
        {
            throw new ArgumentException("A top-level control cannot have a parent.");
        }
        _topLevel = value;
        if (NativeHandle != 0)
        {
            uint style = unchecked((uint)(nuint)Win32.GetWindowLongPtrW(NativeHandle, Win32.GWL_STYLE));
            style = value ? style & ~Win32.WS_CHILD : style | Win32.WS_CHILD;
            _ = Win32.SetWindowLongPtrW(NativeHandle, Win32.GWL_STYLE, unchecked((nint)style));
            ReparentNativeWindow();
        }
    }

    /// <summary>
    /// Occurs when the <see cref="Text"/> property changes.
    /// </summary>
    [Category("Property Changed")]
    [Description("Event raised when the value of the Text property is changed on Control.")]
    public event EventHandler? TextChanged;
    public event EventHandler? Enter;
    public event EventHandler? Leave;
    public event EventHandler? HandleCreated;
    public event EventHandler? HandleDestroyed;

    /// <summary>
    /// Occurs when the control location changes.
    /// </summary>
    public event EventHandler? LocationChanged;

    /// <summary>
    /// Occurs when the control moves.
    /// </summary>
    public event EventHandler? Move;

    /// <summary>
    /// Occurs when the control size changes.
    /// </summary>
    public event EventHandler? SizeChanged;

    /// <summary>
    /// Occurs when the size of the control's client area changes.
    /// </summary>
    public event EventHandler? ClientSizeChanged;

    /// <summary>
    /// Occurs when the control is resized.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? Resize;

    /// <summary>
    /// Occurs when the <see cref="BackColor"/> property changes.
    /// </summary>
    public event EventHandler? BackColorChanged;

    /// <summary>Occurs when the background image changes.</summary>
    [Category("PropertyChanged")]
    public event EventHandler? BackgroundImageChanged;

    /// <summary>Occurs when the background image layout changes.</summary>
    [Category("PropertyChanged")]
    public event EventHandler? BackgroundImageLayoutChanged;

    /// <summary>
    /// Occurs when the <see cref="ForeColor"/> property changes.
    /// </summary>
    public event EventHandler? ForeColorChanged;

    /// <summary>
    /// Occurs when the font changes.
    /// </summary>
    public event EventHandler? FontChanged;

    [Category("Appearance")]
    public event PaintEventHandler? Paint;

    [Category("Behavior")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event InvalidateEventHandler? Invalidated;

    [Category("Behavior")]
    public event EventHandler? StyleChanged;

    /// <summary>
    /// Gets or sets the text displayed by the control.
    /// </summary>
    [Category("Appearance")]
    [Localizable(true)]
    [Bindable(true)]
    [DispId(-517)]
    [Description("The text associated with the control.")]
    [AllowNull]
    public virtual string Text
    {
        get
        {
            if (NativeHandle != 0)
            {
                _text = Win32.GetText(NativeHandle);
            }

            return _text;
        }
        set
        {
            // Without a native window, text remains managed state even after
            // disposal. Equality must still consume the virtual getter.
            string newText = value ?? string.Empty;
            string currentText = Text;
            if (string.Equals(currentText, newText, StringComparison.Ordinal))
            {
                return;
            }

            _text = newText;
            if (NativeHandle != 0)
            {
                _ = Win32.SetWindowTextW(NativeHandle, _text);
            }

            OnTextChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Resets the text through the control's virtual <see cref="Text"/> setter.
    /// </summary>
    public virtual void ResetText() => Text = string.Empty;

    /// <summary>
    /// Gets or sets the background color. An empty value restores parent inheritance.
    /// </summary>
    [Category("Appearance")]
    [DispId(-501)]
    public virtual Color BackColor
    {
        get
        {
            if (!_backColor.IsEmpty) { return _backColor; }
            if (_parent is not null)
            {
                Color inherited = _parent.BackColor;
                if (inherited.IsEmpty || inherited.A == 255 || GetStyle(ControlStyles.SupportsTransparentBackColor)) { return inherited; }
            }
            return DefaultBackColor;
        }
        set
        {
            if (!value.IsEmpty && !GetStyle(ControlStyles.SupportsTransparentBackColor) && value.A < 255)
            {
                throw new ArgumentException("Control does not support transparent background colors.");
            }
            Color previous = BackColor;
            Color previousLocal = _backColor;
            _backColor = value;
            if (!previous.Equals(BackColor)) { OnBackColorChanged(EventArgs.Empty); }
            else if (previousLocal != value) { RefreshInheritedColorTheme(background: true); }
        }
    }

    /// <summary>Gets or sets the background image. The caller retains ownership of the image.</summary>
    [Category("Appearance")]
    [DefaultValue(null)]
    [Localizable(true)]
    public virtual Image? BackgroundImage
    {
        get => _backgroundImage;
        set
        {
            if (BackgroundImage == value) { return; }
            _backgroundImage = value;
            OnBackgroundImageChanged(EventArgs.Empty);
        }
    }

    /// <summary>Gets or sets how the background image is positioned in the client area.</summary>
    [Category("Appearance")]
    [DefaultValue(ImageLayout.Tile)]
    [Localizable(true)]
    public virtual ImageLayout BackgroundImageLayout
    {
        get => _backgroundImageLayout;
        set
        {
            if (BackgroundImageLayout == value) { return; }
            if ((uint)value > (uint)ImageLayout.Zoom)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ImageLayout));
            }
            if (value is ImageLayout.Center or ImageLayout.Stretch or ImageLayout.Zoom)
            {
                SetStyle(ControlStyles.ResizeRedraw, true);
                if (IsImageTransparent(BackgroundImage)) { DoubleBuffered = true; }
            }
            _backgroundImageLayout = value;
            OnBackgroundImageLayoutChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the foreground color. An empty value restores parent inheritance.
    /// </summary>
    [Category("Appearance")]
    [DispId(-513)]
    public virtual Color ForeColor
    {
        get => !_foreColor.IsEmpty ? _foreColor : _parent?.ForeColor ?? DefaultForeColor;
        set
        {
            Color previous = ForeColor;
            Color previousLocal = _foreColor;
            _foreColor = value;
            if (!previous.Equals(ForeColor)) { OnForeColorChanged(EventArgs.Empty); }
            else if (previousLocal != value) { RefreshInheritedColorTheme(background: false); }
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void ResetBackColor() => BackColor = Color.Empty;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void ResetForeColor() => ForeColor = Color.Empty;

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal virtual bool ShouldSerializeBackColor() => !_backColor.IsEmpty;

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal virtual bool ShouldSerializeForeColor() => !_foreColor.IsEmpty;

    internal Color LocalBackColor => _backColor;
    internal Color LocalForeColor => _foreColor;

    // A palette supplies the native default only while no ancestor has a local
    // value or a non-default public override. Return the current virtual value,
    // so derived getters and background transparency filtering are respected.
    internal virtual bool TryGetExplicitBackColor(out Color color)
    {
        color = BackColor;
        return !_backColor.IsEmpty || color != DefaultBackColor
            || (_parent?.TryGetExplicitBackColor(out _) ?? false);
    }

    internal virtual bool TryGetExplicitForeColor(out Color color)
    {
        color = ForeColor;
        return !_foreColor.IsEmpty || color != DefaultForeColor
            || (_parent?.TryGetExplicitForeColor(out _) ?? false);
    }

    // Every control owns its HFONT, including when the managed Font is inherited
    // or borrowed. A parent's HWND recreation therefore cannot invalidate fonts
    // still selected by parked child windows.
    internal nint NativeFontHandle
    {
        get
        {
            Font font = GetCurrentFont();
            if (_nativeFontHandle != 0 && !_nativeFontDirty && font.Equals(_nativeFontSource))
            {
                return _nativeFontHandle;
            }

            nint replacement = font.ToHfont();
            nint previous = _nativeFontHandle;
            _nativeFontHandle = replacement;
            _nativeFontSource = font;
            _nativeFontDirty = false;
            try
            {
                if (NativeHandle != 0 && !GetStyle(ControlStyles.UserPaint))
                {
                    _ = Win32.SendMessageW(NativeHandle, Win32.WM_SETFONT, replacement, (nint)1);
                }
            }
            finally
            {
                // Keep the old HFONT alive until the HWND has consumed its
                // replacement. Reentrant updates own their newer handle.
                if (previous != 0) { _ = Win32.DeleteObject(previous); }
            }
            return _nativeFontHandle;
        }
    }

    internal void ApplyFontToHandle()
    {
        if (NativeHandle == 0 || GetStyle(ControlStyles.UserPaint)) { return; }
        nint previous = _nativeFontHandle;
        nint font = NativeFontHandle;
        if (NativeHandle != 0 && previous == font)
        {
            _ = Win32.SendMessageW(NativeHandle, Win32.WM_SETFONT, font, (nint)1);
        }
    }

    private void InvalidateNativeFont()
    {
        if (NativeHandle == 0) { ReleaseNativeFont(); }
        else { _nativeFontDirty = true; }
    }

    private void ReleaseNativeFont()
    {
        nint font = _nativeFontHandle;
        _nativeFontHandle = 0;
        _nativeFontSource = null;
        _nativeFontDirty = false;
        if (font != 0) { _ = Win32.DeleteObject(font); }
    }

    /// <summary>
    /// Gets the left position of the control in client coordinates.
    /// </summary>
    public int Left
    {
        get => _left;
        set => SetBounds(value, _top, _width, _height, BoundsSpecified.X);
    }

    /// <summary>
    /// Gets the top position of the control in client coordinates.
    /// </summary>
    public int Top
    {
        get => _top;
        set => SetBounds(_left, value, _width, _height, BoundsSpecified.Y);
    }

    /// <summary>
    /// Gets the width of the control.
    /// </summary>
    public int Width
    {
        get => _width;
        set => SetBounds(_left, _top, value, _height, BoundsSpecified.Width);
    }

    /// <summary>
    /// Gets the height of the control.
    /// </summary>
    public int Height
    {
        get => _height;
        set => SetBounds(_left, _top, _width, value, BoundsSpecified.Height);
    }

    /// <summary>
    /// Gets or sets the size of the control's client area.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size ClientSize
    {
        get => new(_clientWidth, _clientHeight);
        set => SetClientSizeCore(value.Width, value.Height);
    }

    /// <summary>
    /// Gets the rectangle representing the control's client area.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle ClientRectangle => new(0, 0, _clientWidth, _clientHeight);

    /// <summary>
    /// Gets or sets the right edge of the control.
    /// </summary>
    public int Right
    {
        get => _left + _width;
        set => SetBounds(value - _width, _top, _width, _height);
    }

    /// <summary>
    /// Gets or sets the bottom edge of the control.
    /// </summary>
    public int Bottom
    {
        get => _top + _height;
        set => SetBounds(_left, value - _height, _width, _height);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control can receive user input.
    /// </summary>
    public bool Enabled
    {
        get => _enabled && (_parent is null || _parent.Enabled);
        set
        {
            bool previous = Enabled;
            _enabled = value;
            if (previous != Enabled) { OnEnabledChanged(EventArgs.Empty); }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is visible.
    /// </summary>
    public bool Visible
    {
        get => _visible && (_parent is null || _parent.Visible);
        set => SetVisibleCore(value);
    }

    protected virtual void SetVisibleCore(bool value)
    {
        bool previous = Visible;
        if (value != previous && _topLevel)
        {
            if (_handle != 0 || value)
            {
                _ = Win32.ShowWindow(Handle, value ? Win32.SW_SHOW : Win32.SW_HIDE);
            }
            if (value != Visible)
            {
                _visible = value;
                OnVisibleChanged(EventArgs.Empty);
            }
            return;
        }

        _visible = value;
        if (value != previous && (_handle != 0 || (value && _parent?._created == true)))
        {
            try
            {
                if (value) { CreateControl(); }
                if (_handle != 0) { _ = Win32.ShowWindow(_handle, value ? Win32.SW_SHOW : Win32.SW_HIDE); }
            }
            catch
            {
                _visible = !value;
                throw;
            }
        }
        else if (_handle != 0)
        {
            _ = Win32.ShowWindow(_handle, value ? Win32.SW_SHOW : Win32.SW_HIDE);
        }
        if (value != previous)
        {
            using (new LayoutTransaction(_parent, this, nameof(Visible)))
            {
                OnVisibleChanged(EventArgs.Empty);
            }
        }
    }

    internal void SetVisibleState(bool value) => _visible = value;

    internal void NotifyNativeVisibilityChanged(bool visible)
    {
        if (_recreatingHandle) { return; }
        bool previous = Visible;
        if (visible)
        {
            bool previousLocal = _visible;
            _visible = true;
            bool succeeded = false;
            try
            {
                CreateControl();
                succeeded = true;
            }
            finally { if (!succeeded) { _visible = previousLocal; } }
        }
        else if (_parent?.Visible ?? _topLevel)
        {
            _visible = false;
        }
        if (!_parentRecreatingHandle && previous != visible) { OnVisibleChanged(EventArgs.Empty); }
    }

    /// <summary>
    /// Gets or sets the upper-left location of the control.
    /// </summary>
    public Point Location
    {
        get => new(_left, _top);
        set => SetBounds(value.X, value.Y, _width, _height, BoundsSpecified.Location);
    }

    /// <summary>
    /// Gets or sets the size of the control.
    /// </summary>
    public Size Size
    {
        get => new(_width, _height);
        set => SetBounds(_left, _top, value.Width, value.Height, BoundsSpecified.Size);
    }

    /// <summary>
    /// Gets or sets the bounds of the control.
    /// </summary>
    public Rectangle Bounds
    {
        get => new(_left, _top, _width, _height);
        set => SetBounds(value.X, value.Y, value.Width, value.Height);
    }

    /// <summary>
    /// Gets the rectangle that represents the logical content area available to child content.
    /// </summary>
    public virtual Rectangle DisplayRectangle => ClientRectangle;

    internal Form? Owner { get; private set; }

    internal int Id { get; private set; }

    /// <summary>
    /// Gets the Win32 class name used to create the underlying child window.
    /// </summary>
    protected virtual string ClassName => DefaultWindowClassName;

    /// <summary>
    /// Gets the Win32 style flags used when creating the underlying child window.
    /// </summary>
    protected virtual uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_CLIPCHILDREN | Win32.WS_CLIPSIBLINGS;

    /// <summary>
    /// Gets optional Win32 extended style flags for the underlying child window.
    /// </summary>
    protected virtual uint ExStyle => 0;

    /// <summary>Gets the native parameters used when creating or updating the control's window.</summary>
    protected virtual CreateParams CreateParams
    {
        get
        {
            CreateParams parameters = _createParams ??= new CreateParams();
            string legacyClassName = ClassName;
            // The standard base class has no public native class name. Retain
            // the historical ClassName extension for existing native controls.
            if (legacyClassName != DefaultWindowClassName || _lastLegacyClassName != legacyClassName)
            {
                parameters.ClassName = legacyClassName == DefaultWindowClassName ? null : legacyClassName;
            }
            _lastLegacyClassName = legacyClassName;
            // WinForms exposes a null caption for an unassigned Text value in
            // CreateParams. Native creation still supplies an empty string
            // when the caption is null.
            parameters.Caption = _text.Length == 0 ? null : _text;
            parameters.X = _left;
            parameters.Y = _top;
            parameters.Width = _width;
            parameters.Height = _height;
            uint style = Style & ~(Win32.WS_VISIBLE | Win32.WS_DISABLED | Win32.WS_TABSTOP);
            style = _topLevel ? style & ~Win32.WS_CHILD : style | Win32.WS_CHILD;
            if (_visible) { style |= Win32.WS_VISIBLE; }
            if (!Enabled) { style |= Win32.WS_DISABLED; }
            if (TabStop) { style |= Win32.WS_TABSTOP; }
            parameters.Style = unchecked((int)style);
            parameters.ExStyle = unchecked((int)ExStyle);
            if (GetStyle(ControlStyles.ContainerControl)) { parameters.ExStyle |= (int)Win32.WS_EX_CONTROLPARENT; }
            parameters.ClassStyle = (int)Win32.CS_DBLCLKS;
            parameters.Parent = _topLevel ? 0 : _parent?.NativeHandle ?? 0;
            return parameters;
        }
    }

    [Category("Behavior")]
    protected virtual bool DoubleBuffered
    {
        get => GetStyle(ControlStyles.OptimizedDoubleBuffer);
        set
        {
            if (value == DoubleBuffered) { return; }
            SetStyle(value ? ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint
                : ControlStyles.OptimizedDoubleBuffer, value);
        }
    }

    protected bool ResizeRedraw
    {
        get => GetStyle(ControlStyles.ResizeRedraw);
        set => SetStyle(ControlStyles.ResizeRedraw, value);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected bool GetStyle(ControlStyles flag) => (_controlStyle & flag) == flag;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected void SetStyle(ControlStyles flag, bool value)
        => _controlStyle = value ? _controlStyle | flag : _controlStyle & ~flag;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected void UpdateStyles()
    {
        if (NativeHandle != 0)
        {
            CreateParams parameters = CreateParams;
            if (_visible) { parameters.Style |= (int)Win32.WS_VISIBLE; }
            if (unchecked((int)(nuint)Win32.GetWindowLongPtrW(NativeHandle, Win32.GWL_STYLE)) != parameters.Style)
            {
                _ = Win32.SetWindowLongPtrW(NativeHandle, Win32.GWL_STYLE, unchecked((nint)parameters.Style));
            }
            if (unchecked((int)(nuint)Win32.GetWindowLongPtrW(NativeHandle, Win32.GWL_EXSTYLE)) != parameters.ExStyle)
            {
                _ = Win32.SetWindowLongPtrW(NativeHandle, Win32.GWL_EXSTYLE, unchecked((nint)parameters.ExStyle));
            }
            _ = Win32.SetWindowPos(NativeHandle, 0, 0, 0, 0, 0,
                Win32.SWP_FRAMECHANGED | Win32.SWP_NOACTIVATE | Win32.SWP_NOMOVE | Win32.SWP_NOSIZE | Win32.SWP_NOZORDER);
            UpdateBoundsFromNativeWindow();
            Invalidate(true);
        }
        OnStyleChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Gets a value indicating whether this control should materialize a native child window.
    /// </summary>
    protected virtual bool ShouldCreateNativeHandle => true;

    /// <summary>
    /// Gets the default semantic background slot used when <see cref="BackColor"/> is not explicitly set.
    /// </summary>
    private protected virtual ThemeColorSlot DefaultBackgroundSlot => ThemeColorSlot.Control;

    /// <summary>
    /// Gets the default semantic foreground slot used when <see cref="ForeColor"/> is not explicitly set.
    /// </summary>
    private protected virtual ThemeColorSlot DefaultForegroundSlot => ThemeColorSlot.Control;

    /// <summary>
    /// Gets a value indicating whether the control should inherit its background from the parent surface when themed.
    /// </summary>
    protected virtual bool UseParentBackgroundForTheming => false;

    /// <summary>
    /// Gets the current resolved visual style available to the control.
    /// </summary>
    protected ResolvedVisualStyle CurrentVisualStyle => this is Form form
        ? form.CurrentVisualStyle
        : Owner?.CurrentVisualStyle ?? Application.CurrentVisualStyle;

    internal void Attach(Form owner, int id, Control? parent)
    {
        Owner = owner;
        Id = id;
        if (NativeHandle != 0)
        {
            // GWLP_ID changes with the receiving form's command registration,
            // while reparenting deliberately preserves the existing HWND.
            _ = Win32.SetWindowLongPtrW(NativeHandle, -12, id);
        }
    }

    internal ReadOnlySpan<Control> ChildControls => _controls is null ? [] : _controls.ItemsSpan;

    internal void AddChild(Control control) => Controls.Add(control);

    internal bool RemoveChild(Control control)
    {
        bool contained = Controls.Contains(control);
        Controls.Remove(control);
        return contained;
    }

    internal virtual void OnChildAdded(Control control) { }
    internal virtual void OnChildRemoved(Control control) { }

    internal void AttachChildrenToOwner(Form owner)
    {
        foreach (Control child in ChildControls) { owner.AttachControl(child, this); }
    }

    internal void CreateChildHandles()
    {
        foreach (Control child in ChildControls)
        {
            if (child._visible) { child.CreateHandleRecursive(); }
        }
    }

    internal void DetachFromOwner()
    {
        Owner = null;
        Id = 0;
    }

    /// <summary>
    /// Updates the control bounds using client coordinates.
    /// </summary>
    /// <param name="x">The left position.</param>
    /// <param name="y">The top position.</param>
    /// <param name="width">The desired width.</param>
    /// <param name="height">The desired height.</param>
    public void SetBounds(int x, int y, int width, int height)
        => SetBounds(x, y, width, height, BoundsSpecified.All);

    /// <summary>
    /// Updates the specified parts of the control bounds using client coordinates.
    /// </summary>
    /// <param name="x">The left position.</param>
    /// <param name="y">The top position.</param>
    /// <param name="width">The desired width.</param>
    /// <param name="height">The desired height.</param>
    /// <param name="specified">The bounds supplied explicitly by the caller.</param>
    public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
    {
        ThrowIfDisposed();

        if ((specified & BoundsSpecified.X) == 0) { x = _left; }
        if ((specified & BoundsSpecified.Y) == 0) { y = _top; }
        if ((specified & BoundsSpecified.Width) == 0) { width = _width; }
        if ((specified & BoundsSpecified.Height) == 0) { height = _height; }

        if (_left != x || _top != y || _width != width || _height != height)
        {
            ForceAnchorCalculations = true;
            try { SetBoundsCore(x, y, width, height, specified); }
            finally { ForceAnchorCalculations = false; }
            RequestLayout(_parent, this, nameof(Bounds));
        }
    }

    /// <summary>
    /// Applies control bounds after unspecified components have been preserved by <see cref="SetBounds(int, int, int, int, BoundsSpecified)"/>.
    /// </summary>
    /// <param name="x">The left position.</param>
    /// <param name="y">The top position.</param>
    /// <param name="width">The desired width.</param>
    /// <param name="height">The desired height.</param>
    /// <param name="specified">The bounds supplied explicitly by the original caller.</param>
    protected virtual void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        ThrowIfDisposed();

        // Compare the raw request before consuming virtual constraint getters.
        // A direct core call with the current rectangle is an observable no-op.
        if (_left == x && _top == y && _width == width && _height == height)
        {
            return;
        }

        // The parent must see the completed size notifications before its
        // deferred layout runs. Capture it so reparenting callbacks still
        // balance the suspension on the original parent.
        using LayoutTransaction suspension = new(_parent);

        try
        {
            UpdateSpecifiedBounds(new Rectangle(x, y, width, height), specified);

            if (MaximumSize != Size.Empty || MinimumSize != Size.Empty)
            {
                // Preserve WinForms' short circuit and repeated virtual reads. A
                // getter can reenter sizing, change constraints, or throw here.
                Size maximumSize = MaximumSize;
                int maximumWidth = maximumSize.Width == 0 ? int.MaxValue : maximumSize.Width;
                int maximumHeight = maximumSize.Height == 0 ? int.MaxValue : maximumSize.Height;
                width = Math.Min(width, maximumWidth);
                height = Math.Min(height, maximumHeight);
                Size minimumSize = MinimumSize;
                width = Math.Max(width, minimumSize.Width);
                height = Math.Max(height, minimumSize.Height);
            }

            // WinForms preserves requested negative dimensions before a control has its own
            // HWND. Once created, native window dimensions normalize them to zero.
            bool hasHandle = NativeHandle != 0;
            if (hasHandle)
            {
                width = Math.Max(0, width);
                height = Math.Max(0, height);
            }

            bool locationChanged = _left != x || _top != y;
            bool sizeChanged = _width != width || _height != height;

            if (hasHandle && !locationChanged && !sizeChanged)
            {
                return;
            }

            // The public entry point filters unspecified components. The virtual core
            // accepts the resulting rectangle, including adjustments made by overrides.
            _left = x;
            _top = y;
            _width = width;
            _height = height;

            if (hasHandle)
            {
                ApplyBounds();
            }

            int previousClientWidth = _clientWidth;
            int previousClientHeight = _clientHeight;
            UpdateClientSize();
            sizeChanged |= previousClientWidth != _clientWidth || previousClientHeight != _clientHeight;

            if (!locationChanged && !sizeChanged)
            {
                return;
            }

            OnBoundsChanged();

            // Keep the standard notification chain independent of the historical layout
            // hook: existing derived controls need not call base.OnBoundsChanged().
            // Snapshot both changes before callbacks so reentrant setters do not erase
            // the outer operation's pending notification.
            try
            {
                if (locationChanged) { OnLocationChanged(EventArgs.Empty); }
                if (sizeChanged)
                {
                    OnSizeChanged(EventArgs.Empty);
                    OnClientSizeChanged(EventArgs.Empty);
                    ClearPreferredSizeCache();
                    RequestLayout(_parent, this, nameof(Bounds));
                }
            }
            catch (Exception exception) when (hasHandle)
            {
                // WinForms dispatches HWND-backed bounds notifications through its
                // window procedure. Preserve its thread-exception route while keeping
                // handleless setters and direct protected calls synchronous.
                Application.OnThreadException(exception);
            }
        }
        finally
        {
            if (_parent is { } parent)
            {
                parent.ClearPreferredSizeCache();
                parent.LayoutEngine.InitLayout(this, specified);
            }
        }
    }

    internal void SetBoundsFromLayout(Rectangle bounds, BoundsSpecified specified)
        => SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);

    private void UpdateSpecifiedBounds(Rectangle requested, BoundsSpecified specified)
    {
        Rectangle previous = SpecifiedBounds;
        bool preserveX = (specified & BoundsSpecified.X) == 0 && requested.X != previous.X;
        bool preserveY = (specified & BoundsSpecified.Y) == 0 && requested.Y != previous.Y;
        bool preserveWidth = (specified & BoundsSpecified.Width) == 0 && requested.Width != previous.Width;
        bool preserveHeight = (specified & BoundsSpecified.Height) == 0 && requested.Height != previous.Height;
        if (preserveX || preserveY || preserveWidth || preserveHeight)
        {
            _specifiedBounds = new Rectangle(preserveX ? previous.X : requested.X, preserveY ? previous.Y : requested.Y,
                preserveWidth ? previous.Width : requested.Width, preserveHeight ? previous.Height : requested.Height);
        }
        else { _specifiedBounds = null; }
    }

    /// <summary>
    /// Computes the control's outer size for the specified client size.
    /// </summary>
    /// <param name="clientSize">The desired client size.</param>
    /// <returns>The client size plus the control's non-client border dimensions.</returns>
    protected virtual Size SizeFromClientSize(Size clientSize)
    {
        Size nonClient = GetNonClientSize();
        return new Size(unchecked(clientSize.Width + nonClient.Width), unchecked(clientSize.Height + nonClient.Height));
    }

    /// <summary>
    /// Sets the size of the client area and adjusts the control's outer size.
    /// </summary>
    /// <param name="x">The desired client width.</param>
    /// <param name="y">The desired client height.</param>
    protected virtual void SetClientSizeCore(int x, int y)
    {
        ThrowIfDisposed();

        // WinForms does not dispatch through the protected SizeFromClientSize
        // override here. Keep its independent client-size conversion route.
        Size nonClient = GetNonClientSize();
        Size = new Size(unchecked(x + nonClient.Width), unchecked(y + nonClient.Height));

        // WinForms commits the original request after the bounds operation. Keep
        // this separate from the native client rectangle: negative requests and
        // reentrant handlers can intentionally leave the two values different.
        _clientWidth = x;
        _clientHeight = y;
        OnClientSizeChanged(EventArgs.Empty);
    }

    /// <summary>
    /// Invalidates the control so the underlying native window repaints.
    /// </summary>
    public void Invalidate()
        => Invalidate(false);

    public void Invalidate(bool invalidateChildren)
    {
        if (NativeHandle == 0) { return; }
        if (invalidateChildren)
        {
            _ = Win32.RedrawWindow(NativeHandle, 0, 0, Win32.RDW_INVALIDATE | Win32.RDW_ERASE | Win32.RDW_ALLCHILDREN);
        }
        else { _ = Win32.InvalidateRect(NativeHandle, 0, !GetStyle(ControlStyles.Opaque)); }
        NotifyInvalidate(ClientRectangle);
    }

    public void Invalidate(Rectangle rc) => Invalidate(rc, false);

    public unsafe void Invalidate(Rectangle rc, bool invalidateChildren)
    {
        if (rc.IsEmpty) { Invalidate(invalidateChildren); return; }
        if (NativeHandle == 0) { return; }
        Win32.RECT rectangle = new() { Left = rc.Left, Top = rc.Top, Right = rc.Right, Bottom = rc.Bottom };
        if (invalidateChildren)
        {
            _ = Win32.RedrawWindow(NativeHandle, (nint)(&rectangle), 0,
                Win32.RDW_INVALIDATE | Win32.RDW_ERASE | Win32.RDW_ALLCHILDREN);
        }
        else { _ = Win32.InvalidateRect(NativeHandle, (nint)(&rectangle), !GetStyle(ControlStyles.Opaque)); }
        NotifyInvalidate(rc);
    }

    public void Invalidate(Region? region) => Invalidate(region, false);

    public void Invalidate(Region? region, bool invalidateChildren)
    {
        if (region is null) { Invalidate(invalidateChildren); return; }
        if (NativeHandle == 0) { return; }
        using Graphics graphics = Graphics.FromHwnd(NativeHandle);
        nint regionHandle = region.GetHrgn(graphics);
        try
        {
            if (invalidateChildren)
            {
                _ = Win32.RedrawWindow(NativeHandle, 0, regionHandle,
                    Win32.RDW_INVALIDATE | Win32.RDW_ERASE | Win32.RDW_ALLCHILDREN);
            }
            else { _ = Win32.InvalidateRgn(NativeHandle, regionHandle, !GetStyle(ControlStyles.Opaque)); }
            // The Region overload deliberately calls OnInvalidated directly.
            OnInvalidated(new InvalidateEventArgs(Rectangle.Ceiling(region.GetBounds(graphics))));
        }
        finally { _ = Win32.DeleteObject(regionHandle); }
    }

    public void Update()
    {
        if (NativeHandle != 0) { _ = Win32.UpdateWindow(NativeHandle); }
    }

    /// <summary>
    /// Forces the control to repaint immediately.
    /// </summary>
    public virtual void Refresh()
    {
        Invalidate(true);
        Update();
    }

    /// <summary>Creates a drawing surface for the control. The caller owns the returned graphics.</summary>
    public Graphics CreateGraphics() => Graphics.FromHwnd(Handle);

    /// <summary>
    /// Sets input focus to the control.
    /// </summary>
    /// <returns>Whether the control has the keyboard focus after the request.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool Focus()
    {
        if (CanFocus)
        {
            _ = Win32.SetFocus(NativeHandle);
        }

        return Focused;
    }

    /// <summary>
    /// Suspends layout until the matching resume operation.
    /// </summary>
    public void SuspendLayout()
    {
        unchecked { _layoutSuspendCount++; }
    }

    /// <summary>
    /// Resumes layout logic for compatibility with designer-generated code.
    /// </summary>
    public void ResumeLayout()
    {
        ResumeLayout(true);
    }

    /// <summary>
    /// Resumes layout logic for compatibility with designer-generated code.
    /// </summary>
    /// <param name="performLayout">Whether layout should be performed immediately.</param>
    public void ResumeLayout(bool performLayout)
    {
        bool performedLayout = false;
        if (_layoutSuspendCount > 0)
        {
            _layoutSuspendCount--;
            if (_layoutSuspendCount == 0 && _layoutDeferred && performLayout)
            {
                PerformLayout();
                performedLayout = true;
            }
        }
        if (!performedLayout) { _clearLayoutArgs = true; }
        if (!performLayout)
        {
            ClearPreferredSizeCache();
            for (int index = 0; _controls is not null && index < _controls.Count; index++)
            {
                Control child = _controls[index];
                LayoutEngine.InitLayout(child, BoundsSpecified.All);
                child.ClearPreferredSizeCache();
            }
        }
    }

    /// <summary>
    /// Performs layout for compatibility with designer-generated code.
    /// </summary>
    public void PerformLayout()
    {
        if (_cachedLayoutEventArgs is not null)
        {
            PerformLayout(_cachedLayoutEventArgs);
            // A throwing callback retains the same arguments for the next call.
            // A suspended call still consumes this cache, as in WinForms.
            _cachedLayoutEventArgs = null;
            _clearLayoutArgs = false;
        }
        else
        {
            PerformLayout(null, null);
        }
    }

    /// <summary>
    /// Requests layout for a change to the specified control and property.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void PerformLayout(Control? affectedControl, string? affectedProperty)
        => PerformLayout(new LayoutEventArgs(affectedControl, affectedProperty));

    internal void RequestLayout(Control? affectedControl, string? affectedProperty)
        => RequestLayout(this, affectedControl, affectedProperty);

    private static void RequestLayout(Control? container, Control? affectedControl, string? affectedProperty)
    {
        affectedControl?.ClearPreferredSizeCache();
        container?.ClearPreferredSizeCache();
        container?.PerformLayout(new LayoutEventArgs(affectedControl, affectedProperty));
    }

    private void PerformLayout(LayoutEventArgs args)
    {
        for (Control? control = this; control is not null; control = control._parent)
        {
            if (control.Disposing) { return; }
        }

        if (_layoutSuspendCount > 0)
        {
            _layoutDeferred = true;
            if (_cachedLayoutEventArgs is null || _clearLayoutArgs)
            {
                _cachedLayoutEventArgs = args;
                _clearLayoutArgs = false;
            }
            // The standard suspended path consumes the virtual engine getter.
            _ = LayoutEngine;
            return;
        }

        // Reentrant requests are cached while the current layout is executing.
        // They do not recursively execute or automatically start another pass.
        _layoutSuspendCount = 1;
        try { OnLayout(args); }
        finally
        {
            _layoutDeferred = false;
            _layoutDirty = false;
            _layoutSuspendCount = 0;
            if (_parent is { _layoutDirty: true } parent)
            {
                parent.RequestLayout(this, nameof(PreferredSize));
            }
        }
    }

    /// <summary>
    /// Raises the <see cref="Layout"/> event.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnLayout(LayoutEventArgs levent)
    {
        Layout?.Invoke(this, levent);
        if (LayoutEngine.Layout(this, levent) && _parent is not null) { _parent._layoutDirty = true; }
    }

    protected virtual void OnPaddingChanged(EventArgs e) => PaddingChanged?.Invoke(this, e);
    protected virtual void OnMarginChanged(EventArgs e) => MarginChanged?.Invoke(this, e);
    protected virtual void OnDockChanged(EventArgs e) => DockChanged?.Invoke(this, e);
    protected virtual void OnAutoSizeChanged(EventArgs e) => AutoSizeChanged?.Invoke(this, e);

    // Property transactions queue the original reason before user callbacks.
    // Nested changes therefore preserve the same first-request/Resume behavior
    // as explicit SuspendLayout and PerformLayout calls.
    internal readonly struct LayoutTransaction : IDisposable
    {
        private readonly Control? _container;
        private readonly bool _resumeLayout;

        internal LayoutTransaction(Control? container)
        {
            _container = container;
            _resumeLayout = true;
            container?.SuspendLayout();
        }

        internal LayoutTransaction(Control? container, Control affectedControl, string property, bool resumeLayout = true)
        {
            affectedControl.ClearPreferredSizeCache();
            _container = container;
            _resumeLayout = resumeLayout;
            container?.SuspendLayout();
            container?.ClearPreferredSizeCache();
            if (resumeLayout) { container?.RequestLayout(affectedControl, property); }
        }

        public void Dispose() => _container?.ResumeLayout(_resumeLayout);
    }

    public virtual object? Invoke(Delegate method)
        => Invoke(method, args: null);

    /// <summary>
    /// Executes a delegate on the thread that owns this control's native window.
    /// </summary>
    public object? Invoke(Delegate method, params object?[]? args)
    {
        WindowThreadDispatcher dispatcher = FindMarshalingDispatcher() ?? throw WindowThreadDispatcher.NoMarshalingHandle();
        return dispatcher.Invoke(this, method, args);
    }

    /// <summary>
    /// Executes an action on the thread that owns this control's native window.
    /// </summary>
    public void Invoke(Action method) => Invoke((Delegate)method);

    /// <summary>
    /// Executes a function on the thread that owns this control's native window and returns its result.
    /// </summary>
    public T Invoke<T>(Func<T> method)
    {
        ArgumentNullException.ThrowIfNull(method);
        T result = default!;
        Invoke((Delegate)(Action)(() => result = method()));
        return result;
    }

    public virtual IAsyncResult BeginInvoke(Delegate method)
        => BeginInvoke(method, args: null);

    /// <summary>
    /// Queues a delegate for the thread that owns this control's native window.
    /// </summary>
    public IAsyncResult BeginInvoke(Delegate method, params object?[]? args)
    {
        WindowThreadDispatcher dispatcher = FindMarshalingDispatcher() ?? throw WindowThreadDispatcher.NoMarshalingHandle();
        return dispatcher.BeginInvoke(this, method, args);
    }

    /// <summary>
    /// Queues an action for the thread that owns this control's native window.
    /// </summary>
    public IAsyncResult BeginInvoke(Action method) => BeginInvoke((Delegate)method);

    /// <summary>
    /// Waits for a callback queued with BeginInvoke and returns its result.
    /// </summary>
    public object? EndInvoke(IAsyncResult asyncResult) => WindowThreadDispatcher.EndInvoke(asyncResult);

    /// <summary>
    /// Queues an action on the thread that owns this control's native window.
    /// </summary>
    public virtual Task InvokeAsync(Action action) => InvokeAsync(action, CancellationToken.None);

    /// <summary>
    /// Queues an action on the native window's creating thread and observes its completion.
    /// </summary>
    public async Task InvokeAsync(Action callback, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(callback);
        WindowThreadDispatcher dispatcher = NativeHandle != 0 && _threadDispatcher is { } ownDispatcher
            ? ownDispatcher : throw WindowThreadDispatcher.NoMarshalingHandle();
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        await dispatcher.InvokeAsync(() => { callback(); return true; }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Queues a function on the native window's creating thread and returns its result.
    /// </summary>
    public async Task<T> InvokeAsync<T>(Func<T> callback, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(callback);
        WindowThreadDispatcher dispatcher = NativeHandle != 0 && _threadDispatcher is { } ownDispatcher
            ? ownDispatcher : throw WindowThreadDispatcher.NoMarshalingHandle();
        if (cancellationToken.IsCancellationRequested)
        {
            return default!;
        }

        return await dispatcher.InvokeAsync(callback, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Queues an asynchronous callback on the native window's creating thread and observes its completion.
    /// </summary>
    public async Task InvokeAsync(Func<CancellationToken, ValueTask> callback, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(callback);
        WindowThreadDispatcher dispatcher = NativeHandle != 0 && _threadDispatcher is { } ownDispatcher
            ? ownDispatcher : throw WindowThreadDispatcher.NoMarshalingHandle();
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        await dispatcher.InvokeAsync(callback, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Queues an asynchronous function on the native window's creating thread and returns its result.
    /// </summary>
    public async Task<T> InvokeAsync<T>(Func<CancellationToken, ValueTask<T>> callback, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(callback);
        WindowThreadDispatcher dispatcher = NativeHandle != 0 && _threadDispatcher is { } ownDispatcher
            ? ownDispatcher : throw WindowThreadDispatcher.NoMarshalingHandle();
        if (cancellationToken.IsCancellationRequested)
        {
            return default!;
        }

        return await dispatcher.InvokeAsync(callback, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Queues the existing Task-based callback on the native window's creating thread.
    /// </summary>
    // Keep existing Task delegates callable while async token lambdas select the
    // standard ValueTask callback overload instead of becoming ambiguous.
    [OverloadResolutionPriority(-1)]
    public virtual async Task InvokeAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(action);
        WindowThreadDispatcher dispatcher = NativeHandle != 0 && _threadDispatcher is { } ownDispatcher
            ? ownDispatcher : throw WindowThreadDispatcher.NoMarshalingHandle();
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        await dispatcher.InvokeAsync(action, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Releases managed and unmanaged resources used by the control.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> when called from <see cref="Dispose()"/>; otherwise, <see langword="false"/>.</param>
    protected override void Dispose(bool disposing)
    {
        if (!disposing)
        {
            try
            {
                ReleaseNativeResources();
            }
            finally
            {
                base.Dispose(false);
            }

            return;
        }

        if (_disposing)
        {
            return;
        }

        if (_creatingHandle)
        {
            throw new InvalidOperationException("Dispose cannot be called while the control handle is being created.");
        }

        _disposing = true;
        bool disposeComponent = false;
        List<Exception>? completionFailures = null;
        try
        {
            try
            {
                try
                {
                    // Native children are destroyed with their parent HWND.
                    // ParentChanged and ControlRemoved must observe that the
                    // disposed control no longer owns a live window.
                    try { ReleaseNativeResources(); }
                    catch (Exception exception) when (NativeHandle == 0)
                    {
                        // The window is already gone, so a dispatcher completion
                        // or post-base DestroyHandle failure must not strand the
                        // managed ownership tree. A still-live window keeps the
                        // normal failure boundary and can be retried explicitly.
                        completionFailures = [exception];
                    }
                    _parent?.Controls.Remove(this);
                    if (!_disposed)
                    {
                        try
                        {
                            OnDisposing();
                        }
                        catch
                        {
                            // Preserve the historical hook's Component cleanup
                            // contract without applying it to standard parent or
                            // child disposal notifications that fail earlier.
                            disposeComponent = true;
                            throw;
                        }
                    }
                    if (_controls is { } children)
                    {
                        for (int index = 0; index < children.Count; index++)
                        {
                            Control child = children[index];
                            child._parent = null;
                            child.Owner?.DetachControl(child);
                            child.Dispose();
                        }
                        // A caller may retain the old collection. WinForms leaves
                        // that detached view intact while subsequent Controls reads
                        // obtain a fresh, empty collection.
                        _controls = null;
                    }
                    disposeComponent = true;
                }
                catch (Exception exception) when (completionFailures is not null)
                {
                    // Preserve the normal parent/child notification short circuit.
                    // Only the original native completion failure changes how
                    // errors are reported; later managed stages remain skipped.
                    completionFailures.Add(exception);
                }
                finally
                {
                    // A standard DestroyHandle override is called once. A
                    // skipped or failed base call deliberately leaves its
                    // native window available for the next explicit attempt.
                    try { ReleaseDetachedNativeResources(); }
                    catch (Exception exception) when (completionFailures is not null)
                    {
                        completionFailures.Add(exception);
                    }
                }
            }
            finally
            {
                // A failed standard notification leaves Component.Site and its
                // Disposed event untouched until a later disposal attempt.
                if (disposeComponent)
                {
                    try { base.Dispose(true); }
                    catch (Exception exception) when (completionFailures is not null)
                    {
                        // Component keeps its own Site/Disposed ordering. Do not
                        // manufacture an event when Container.Remove itself fails.
                        completionFailures.Add(exception);
                    }
                }
            }

            if (completionFailures is { Count: 1 })
            {
                ExceptionDispatchInfo.Capture(completionFailures[0]).Throw();
            }
            if (completionFailures is { Count: > 1 })
            {
                throw new AggregateException(completionFailures);
            }
        }
        finally
        {
            _disposed = true;
            _disposing = false;
        }
    }

    /// <summary>
    /// Creates this control and its visible descendants.
    /// </summary>
    public void CreateControl() => CreateControl(ignoreVisible: false);

    internal void CreateControl(bool ignoreVisible)
    {
        if (!ignoreVisible && (_created || !Visible)) { return; }
        _created = true;
        bool succeeded = false;
        try
        {
            if (_handle == 0) { CreateHandle(); }
            // Creation and native reparenting can call application code and
            // change the collection. Process the tree that was present here.
            Control[] children = ChildControls.ToArray();
            foreach (Control child in children)
            {
                if (child._handle != 0)
                {
                    child.ReparentNativeWindow();
                    child._parentRecreatingHandle = false;
                }
                child.CreateControl(ignoreVisible);
            }
            succeeded = true;
        }
        finally { if (!succeeded) { _created = false; } }
        OnCreateControl();
    }

    internal void CreateHandleRecursive() => CreateControl();

    internal void ReleaseHandleRecursive()
    {
        foreach (Control child in ChildControls)
        {
            child.ReleaseHandleRecursive();
        }

        ClearNativeHandle();
        ReleaseBackgroundBrush();
    }

    internal bool BeginNativeHandleCreation()
    {
        ThrowIfDisposed();
        if (_creatingHandle) { return false; }
        if (_handle != 0) { throw new InvalidOperationException("The control already has a window handle."); }
        _creatingHandle = true;
        return true;
    }

    internal void EndNativeHandleCreation() => _creatingHandle = false;
    internal bool IsCreatingHandle => _creatingHandle;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void CreateHandle()
    {
        if (!BeginNativeHandleCreation()) { return; }
        try
        {
            if (!ShouldCreateNativeHandle) { return; }
            CreateParams parameters = CreateParams;
            int logicalHeight = parameters.Height;
            int nativeHeight = GetNativeHeight(logicalHeight);
            nint parentHandle = parameters.Parent;
            uint style = unchecked((uint)parameters.Style);
            if (parentHandle == 0 && (style & Win32.WS_CHILD) != 0) { parentHandle = GetParkingWindow(); }
            nint instance = Owner?.InstanceHandle ?? Win32.GetModuleHandleW(null);
            NativeWindowClassRegistration windowClass = NativeWindowClass.GetOrRegister(
                parameters.ClassName, parameters.ClassStyle, Marshal.GetFunctionPointerForDelegate(s_subclassProc), instance);
            _originalWindowProc = windowClass.OriginalWindowProc;
            if (parameters.Caption is { Length: > short.MaxValue } caption) { parameters.Caption = caption[..short.MaxValue]; }
            using NativeCreateParameter nativeParameter = NativeCreateParameter.Create(parameters.Param);
            Control? previousCreatingControl = s_creatingControl;
            s_creatingControl = this;
            nint createdHandle;
            try
            {
                // Association happens at WM_NCCREATE, before any native class
                // messages reach the standard virtual WndProc. lpParam remains
                // the caller's marshalled CreateParams.Param throughout.
                createdHandle = Win32.CreateWindowExW(
                    unchecked((uint)parameters.ExStyle), windowClass.Name, parameters.Caption ?? string.Empty,
                    style & ~Win32.WS_VISIBLE, parameters.X, parameters.Y, parameters.Width, nativeHeight,
                    parentHandle, (nint)Id, instance, nativeParameter.Pointer);
            }
            finally { s_creatingControl = previousCreatingControl; }
            if (createdHandle == 0)
            {
                int error = Marshal.GetLastWin32Error();
                if (_handle != 0 && !Win32.IsWindow(_handle)) { ClearNativeHandle(); }
                throw new Win32Exception(error, $"Failed to create control '{parameters.ClassName}'.");
            }
            try
            {
                if (_handle != createdHandle) { RegisterNativeHandle(createdHandle); }
                if (!Win32.GetWindowRect(createdHandle, out Win32.RECT bounds))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                Win32.POINT location = new() { x = bounds.Left, y = bounds.Top };
                if ((style & Win32.WS_CHILD) != 0 && parentHandle != 0)
                {
                    _ = Win32.ScreenToClient(parentHandle, ref location);
                }
                _left = location.x;
                _top = location.y;
                _width = bounds.Right - bounds.Left;
                _height = nativeHeight == logicalHeight ? bounds.Bottom - bounds.Top : Math.Max(0, logicalHeight);
                UpdateClientSize();
            }
            catch (Exception registrationException)
            {
                nint failedHandle = createdHandle;
                bool destroyed = Win32.DestroyWindow(failedHandle);
                int destroyError = Marshal.GetLastWin32Error();
                if (destroyed || !Win32.IsWindow(failedHandle))
                {
                    if (_handle == failedHandle) { ClearNativeHandle(); }
                    ReleaseDetachedNativeResources();
                }
                else
                {
                    // Preserve ownership so a later Dispose can retry when the
                    // operating system rejects teardown; expose both failures
                    // without replacing the original registration exception.
                    registrationException.Data["NativeHandleCleanupError"] = new Win32Exception(destroyError);
                }
                throw;
            }
            ApplyFontToHandle();
            // Handle recreation preserves the current native text even when a
            // derived CreateParams override supplies a temporary caption.
            if (_text.Length != 0) { _ = Win32.SetWindowTextW(_handle, _text); }
            _ = Win32.EnableWindow(_handle, Enabled);
            UpdateChildZOrder();
            ApplyTheme();
            NotifyNativeHandleCreated();
            if ((style & Win32.WS_VISIBLE) != 0 && _handle != 0) { _ = Win32.ShowWindow(_handle, Win32.SW_SHOW); }
        }
        finally { EndNativeHandleCreation(); }
    }

    internal virtual WindowThreadDispatcher? NativeThreadDispatcher => _threadDispatcher;

    internal void RegisterNativeHandle(nint handle, WindowThreadDispatcher? dispatcher = null)
    {
        _handle = handle;
        _handleDestroyedNotified = false;
        if (_threadDispatcher is { } preserved && _recreatingHandle)
        {
            if (dispatcher is not null && dispatcher != preserved) { dispatcher.Close(); }
            preserved.AttachWindow(handle);
        }
        else { _threadDispatcher = dispatcher ?? new WindowThreadDispatcher(handle, this); }
        lock (s_subclassLock) { s_controlsByHandle[handle] = this; }
        UpdateClientSize();
    }

    internal void NotifyNativeHandleDestroyed(nint handle)
    {
        if (_handle != handle || _handleDestroyedNotified) { return; }
        _handleDestroyedNotified = true;
        try { OnHandleDestroyed(EventArgs.Empty); }
        finally
        {
            if (_disposing) { _visible = false; }
            else if (!_recreatingHandle) { _created = false; }
        }
    }

    internal void NotifyNativeHandleCreated()
    {
        try { OnHandleCreated(EventArgs.Empty); }
        catch (Exception exception) { ReportNativeWindowException(exception); }
    }

    private static void ReportNativeWindowException(Exception exception)
    {
        try { Application.OnThreadException(exception); }
        catch (Exception reportingException)
        {
            Environment.FailFast("The application UI thread exception handler failed.", reportingException);
        }
    }

    internal void ClearNativeHandle()
    {
        lock (s_subclassLock)
        {
            if (NativeHandle != 0) { s_controlsByHandle.Remove(NativeHandle); }
        }
        try
        {
            if (_recreatingHandle) { _threadDispatcher?.SuspendForRecreation(); }
            else { _threadDispatcher?.Close(); }
        }
        finally
        {
            // Completion can fail if a caller disposed an invocation's wait
            // handle. Native ownership must still be released in that case.
            if (!_recreatingHandle) { _threadDispatcher = null; }
            _originalWindowProc = 0;
            _handle = 0;
            _created = false;
            ReleaseNativeFont();
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void DestroyHandle()
    {
        if (_handle == 0 || _destroyingHandle) { return; }
        _destroyingHandle = true;
        try
        {
            Exception? closeFailure = null;
            try
            {
                if (!_recreatingHandle) { _threadDispatcher?.Close(); }
            }
            catch (Exception exception) { closeFailure = exception; }

            try
            {
                nint handle = _handle;
                if (handle != 0)
                {
                    if (!Win32.DestroyWindow(handle))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    // A failed subclass-registration rollback can leave an owned HWND
                    // with the original native procedure. It has no cleanup callback.
                    if (_handle == handle) { ClearNativeHandle(); }
                }
            }
            catch (Exception destructionFailure) when (closeFailure is not null)
            {
                throw new AggregateException(closeFailure, destructionFailure);
            }

            // A disposed borrowed wait handle must not prevent native destruction.
            // Keep the completion error visible after the window has been released.
            if (closeFailure is not null) { ExceptionDispatchInfo.Capture(closeFailure).Throw(); }
        }
        finally { _destroyingHandle = false; }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected void RecreateHandle()
    {
        if (_handle == 0 || _recreatingHandle) { return; }
        bool wasCreated = _created;
        nint focused = ContainsFocus ? Win32.GetFocus() : 0;
        nint nativeParent = Win32.GetParent(_handle);
        Control[] children = ChildControls.ToArray();
        _recreatingHandle = true;
        try
        {
            foreach (Control child in children)
            {
                if (child._handle == 0) { continue; }
                child._parentRecreatingHandle = true;
                Marshal.SetLastPInvokeError(0);
                nint previous = Win32.SetParent(child._handle, child.GetParkingWindow());
                int error = Marshal.GetLastPInvokeError();
                if (previous == 0 && error != 0) { throw new Win32Exception(error); }
            }
            DestroyHandle();
            CreateHandle();
        }
        catch
        {
            if (_handle == 0)
            {
                _created = false;
                _threadDispatcher?.Close();
                _threadDispatcher = null;
            }
            throw;
        }
        finally
        {
            _recreatingHandle = false;
            if (_handle != 0)
            {
                foreach (Control child in children)
                {
                    if (child._handle != 0 && child._parent == this)
                    {
                        child.ReparentNativeWindow();
                        child.RecreateHandle();
                        child._parentRecreatingHandle = false;
                    }
                }
            }
        }
        if (wasCreated) { CreateControl(); }
        if (nativeParent != 0 && Win32.IsWindow(nativeParent)
            && (_parent is null || FromHandle(nativeParent) is null))
        {
            Marshal.SetLastPInvokeError(0);
            nint previous = Win32.SetParent(_handle, nativeParent);
            int error = Marshal.GetLastPInvokeError();
            if (previous == 0 && error != 0) { throw new Win32Exception(error); }
        }
        if (focused != 0)
        {
            _ = Win32.SetFocus(Win32.IsWindow(focused) ? focused : _handle);
        }
    }

    internal void UpdateBoundsFromNativeWindow()
    {
        if (_handle == 0 || _applyingBounds) { return; }
        if (!Win32.GetWindowRect(NativeHandle, out Win32.RECT bounds))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        Win32.POINT location = new() { x = bounds.Left, y = bounds.Top };
        if (_parent?.NativeHandle is nint parentHandle && parentHandle != 0)
        {
            _ = Win32.ScreenToClient(parentHandle, ref location);
        }
        bool moved = _left != location.x || _top != location.y;
        bool resized = _width != bounds.Right - bounds.Left || _height != bounds.Bottom - bounds.Top;
        _left = location.x;
        _top = location.y;
        _width = bounds.Right - bounds.Left;
        _height = bounds.Bottom - bounds.Top;
        int previousClientWidth = _clientWidth;
        int previousClientHeight = _clientHeight;
        UpdateClientSize();
        resized |= previousClientWidth != _clientWidth || previousClientHeight != _clientHeight;
        if (moved || resized) { OnBoundsChanged(); }
        if (moved) { OnLocationChanged(EventArgs.Empty); }
        if (resized)
        {
            OnSizeChanged(EventArgs.Empty);
            OnClientSizeChanged(EventArgs.Empty);
            ClearPreferredSizeCache();
            RequestLayout(_parent, this, nameof(Bounds));
        }
    }

    private nint GetParkingWindow()
    {
        if (_parkingWindow == 0)
        {
            _parkingWindow = Win32.CreateWindowExW(0, "STATIC", string.Empty, 0,
                0, 0, 0, 0, 0, 0, Win32.GetModuleHandleW(null), 0);
            if (_parkingWindow == 0) { throw new Win32Exception(Marshal.GetLastWin32Error()); }
        }
        return _parkingWindow;
    }

    private void ReparentNativeWindow()
    {
        if (_handle == 0) { return; }
        nint parentHandle = _parent?.NativeHandle ?? 0;
        if (parentHandle == 0 && !_topLevel) { parentHandle = GetParkingWindow(); }
        // Reassigning the same native parent produces hide/show notifications.
        // A recreated subtree is already attached by its own CreateHandle.
        if (Win32.GetAncestor(_handle, 1) == parentHandle) { return; }
        Marshal.SetLastPInvokeError(0);
        nint previous = Win32.SetParent(NativeHandle, parentHandle);
        int error = Marshal.GetLastPInvokeError();
        if (previous == 0 && error != 0) { throw new Win32Exception(error); }
        if (parentHandle != _parkingWindow)
        {
            ReleaseParkingWindow();
        }
        ApplyBounds();
        UpdateChildZOrder();
    }

    private void UpdateChildZOrder()
    {
        if (_handle == 0 || _parent?.NativeHandle is not nint parentHandle || parentHandle == 0) { return; }
        nint previous = 0;
        foreach (Control sibling in _parent.ChildControls)
        {
            if (sibling == this) { break; }
            if (sibling.NativeHandle != 0) { previous = sibling.NativeHandle; }
        }
        if (!Win32.SetWindowPos(NativeHandle, previous, 0, 0, 0, 0,
            Win32.SWP_NOMOVE | Win32.SWP_NOSIZE | Win32.SWP_NOACTIVATE))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }

    internal static bool TryGetControlByHandle(nint handle, out Control? control)
    {
        lock (s_subclassLock)
        {
            return s_controlsByHandle.TryGetValue(handle, out control);
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Control? FromHandle(nint handle)
        => TryGetControlByHandle(handle, out Control? control) ? control : null;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Control? FromChildHandle(nint handle)
    {
        while (handle != 0)
        {
            if (FromHandle(handle) is { } control) { return control; }
            handle = Win32.GetAncestor(handle, 1);
        }
        return null;
    }

    internal bool HandleCommand(int notificationCode)
        => OnCommand(notificationCode);

    internal bool HandleNotify(int notificationCode, nint lParam)
        => OnNotify(notificationCode, lParam);

    internal void RefreshTheme()
    {
        if (NativeHandle != 0)
        {
            ApplyTheme();
            Refresh();
        }
    }

    internal bool TryGetThemeColors(out nint brush, out uint backgroundColorRef, out uint foregroundColorRef, out bool transparentBackground)
    {
        Form? owner = Owner;
        if (owner is null)
        {
            brush = 0;
            backgroundColorRef = 0;
            foregroundColorRef = 0;
            transparentBackground = false;
            return false;
        }

        ResolvedVisualStyle visualStyle = owner.CurrentVisualStyle;
        brush = GetResolvedBackgroundBrush(visualStyle);
        backgroundColorRef = GetResolvedBackColorRef(visualStyle);
        foregroundColorRef = GetResolvedForeColorRef(visualStyle);
        transparentBackground = UseParentBackgroundForTheming && _backColor.IsEmpty;
        return brush != 0;
    }

    /// <summary>
    /// Lets a derived control expand or normalize its native height before the child window is created or resized.
    /// </summary>
    /// <param name="requestedHeight">The requested logical height.</param>
    /// <returns>The native Win32 height to apply.</returns>
    protected virtual int GetNativeHeight(int requestedHeight) => requestedHeight;

    /// <summary>
    /// Called after the native handle is created and themed.
    /// </summary>
    protected virtual void OnHandleCreated()
    {
        ApplyBounds();
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnHandleCreated(EventArgs e)
    {
        OnHandleCreated();
        HandleCreated?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnHandleDestroyed(EventArgs e)
    {
        HandleDestroyed?.Invoke(this, e);
        if (_handle != 0 && !_disposing) { _text = Text; }
        if (!_recreatingHandle) { ReleaseBackgroundBrush(); }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCreateControl()
    {
        // The standard extension point has no default notifications. Descendants
        // (notably Form) perform their initial managed creation here.
    }

    protected virtual void OnEnter(EventArgs e)
    {
        Enter?.Invoke(this, e);
    }

    protected virtual void OnLeave(EventArgs e)
    {
        Leave?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="Move"/> and <see cref="LocationChanged"/> events.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnLocationChanged(EventArgs e)
    {
        OnMove(e);
        LocationChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="Move"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnMove(EventArgs e)
    {
        Move?.Invoke(this, e);
        if (GetStyle(ControlStyles.SupportsTransparentBackColor) && BackColor.A < 255) { Invalidate(); }
    }

    /// <summary>
    /// Raises the <see cref="Resize"/> and <see cref="SizeChanged"/> events.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnSizeChanged(EventArgs e)
    {
        OnResize(EventArgs.Empty);
        SizeChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="ClientSizeChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnClientSizeChanged(EventArgs e)
    {
        ClientSizeChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="Resize"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnResize(EventArgs e)
    {
        if (GetStyle(ControlStyles.ResizeRedraw) || _exceptionWhilePainting) { Invalidate(); }
        RequestLayout(this, nameof(Bounds));
        Resize?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnStyleChanged(EventArgs e) => StyleChanged?.Invoke(this, e);

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void NotifyInvalidate(Rectangle invalidatedArea)
        => OnInvalidated(new InvalidateEventArgs(invalidatedArea));

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnInvalidated(InvalidateEventArgs e)
    {
        if (_controls is { } children)
        {
            // Invalidated handlers can remove or dispose siblings. Re-read the
            // live count, as WinForms does, instead of retaining the old span.
            for (int index = 0; index < children.Count; index++)
            {
                Control child = children[index];
                if (child.NativeHandle == 0 || !child.GetStyle(ControlStyles.SupportsTransparentBackColor)
                    || child.BackColor.A == 255) { continue; }
                Rectangle rectangle = e.InvalidRect;
                rectangle.Offset(-child.Left, -child.Top);
                rectangle = Rectangle.Intersect(child.ClientRectangle, rectangle);
                if (!rectangle.IsEmpty) { child.Invalidate(rectangle); }
            }
        }
        Invalidated?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnPaint(PaintEventArgs e) => Paint?.Invoke(this, e);

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnPaintBackground(PaintEventArgs pevent)
    {
        // A direct call before creation must not create a window. WinForms
        // paints the actual native client rectangle, which is then empty.
        _ = Win32.GetClientRect(NativeHandle, out Win32.RECT client);
        ArgumentNullException.ThrowIfNull(pevent, "e");
        Rectangle rectangle = Rectangle.FromLTRB(client.Left, client.Top, client.Right, client.Bottom);
        Color color = BackColor;
        if (GetStyle(ControlStyles.SupportsTransparentBackColor) && color.A < 255)
        {
            PaintTransparentBackground(pevent, rectangle);
        }
        if (BackgroundImage is not null && !IsHighContrastBackground())
        {
            bool imageIsTransparent = IsImageTransparent(BackgroundImage);
            if (!(GetStyle(ControlStyles.SupportsTransparentBackColor) && color.A < 255)
                && BackgroundImageLayout == ImageLayout.Tile && imageIsTransparent)
            {
                PaintTransparentBackground(pevent, rectangle);
            }
            if (imageIsTransparent) { PaintBackColor(pevent, rectangle, color); }
            DrawBackgroundImage(pevent.Graphics, BackgroundImage!, color, BackgroundImageLayout, ClientRectangle, rectangle);
        }
        else { PaintBackColor(pevent, rectangle, color); }
    }

    protected void InvokePaint(Control c, PaintEventArgs e) => c.OnPaint(e);

    protected void InvokePaintBackground(Control c, PaintEventArgs e) => c.OnPaintBackground(e);

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnPrint(PaintEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        if (GetStyle(ControlStyles.UserPaint))
        {
            PaintWithErrorHandling(e, background: true);
            // Only internally created native printing arguments request the
            // SaveState behavior. Public PaintEventArgs borrow the caller's
            // graphics, including changes made by OnPaintBackground.
            if (e is PrintPaintEventArgs printArgs) { printArgs.ResetGraphics(); }
            PaintWithErrorHandling(e, background: false);
        }
        else if (e is PrintPaintEventArgs printArgs)
        {
            Message message = printArgs.Message;
            DefWndProc(ref message);
        }
        else
        {
            nint hdc = e.Graphics.GetHdc();
            try
            {
                Message message = Message.Create(Handle, Win32.WM_PRINTCLIENT, hdc, (nint)0x1E);
                DefWndProc(ref message);
            }
            finally { e.Graphics.ReleaseHdc(hdc); }
        }
    }

    private unsafe void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle)
    {
        Control? parent = _parent;
        if (parent is null)
        {
            PaintBackColor(e, rectangle, SystemColors.Control);
            return;
        }

        Rectangle parentClip = rectangle;
        parentClip.Offset(Left, Top);
        using PaintHdcScope hdc = new(e.Graphics);
        if (!Win32.OffsetViewportOrgEx(hdc.Handle, -Left, -Top, null))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        using Graphics parentGraphics = Graphics.FromHdc(hdc.Handle);
        using PaintEventArgs parentArgs = new(parentGraphics, parentClip);
        InvokePaintBackground(parent, parentArgs);
        InvokePaint(parent, parentArgs);
    }

    private static bool IsImageTransparent(Image? image)
        => image is not null && (image.Flags & (int)ImageFlags.HasAlpha) != 0;

    private static bool IsHighContrastBackground()
    {
        Win32.HIGHCONTRASTW highContrast = new() { cbSize = (uint)Marshal.SizeOf<Win32.HIGHCONTRASTW>() };
        return Win32.SystemParametersInfoW(Win32.SPI_GETHIGHCONTRAST, highContrast.cbSize, ref highContrast, 0)
            && (highContrast.dwFlags & Win32.HCF_HIGHCONTRASTON) != 0;
    }

    private static void PaintBackColor(PaintEventArgs e, Rectangle rectangle, Color color)
    {
        if (color.A == 255)
        {
            using PaintHdcScope hdc = new(e.Graphics);
            nint brush = Win32.CreateSolidBrush(Win32.ToColorRef(unchecked((uint)color.ToArgb())));
            if (brush == 0) { throw new Win32Exception(Marshal.GetLastWin32Error()); }
            try
            {
                Win32.RECT bounds = new() { Left = rectangle.Left, Top = rectangle.Top, Right = rectangle.Right, Bottom = rectangle.Bottom };
                _ = Win32.FillRect(hdc.Handle, ref bounds, brush);
            }
            finally { _ = Win32.DeleteObject(brush); }
        }
        else if (color.A != 0)
        {
            using SolidBrush brush = new(color);
            e.Graphics.FillRectangle(brush, rectangle);
        }
    }

    // Adapted from ControlPaint.DrawBackgroundImage and CalculateBackgroundImageRectangle
    // in the fixed WinForms source. RTL and scrolling remain with their parent tasks.
    private static void DrawBackgroundImage(Graphics graphics, Image image, Color backColor, ImageLayout layout,
        Rectangle bounds, Rectangle clip)
    {
        if (layout == ImageLayout.Tile)
        {
            using TextureBrush brush = new(image, WrapMode.Tile);
            graphics.FillRectangle(brush, clip);
            // GDI+ texture rendering resets an HDC viewport origin. Restore it
            // before the parent's foreground paints into a transparent child.
            nint hdc = graphics.GetHdc();
            graphics.ReleaseHdc(hdc);
            return;
        }

        Rectangle imageRectangle = CalculateBackgroundImageRectangle(bounds, image.Size, layout);
        using (SolidBrush brush = new(backColor)) { graphics.FillRectangle(brush, clip); }
        if (!clip.Contains(imageRectangle))
        {
            if (layout is ImageLayout.Stretch or ImageLayout.Zoom)
            {
                imageRectangle.Intersect(clip);
                graphics.DrawImage(image, imageRectangle);
            }
            else if (layout == ImageLayout.None)
            {
                imageRectangle.Offset(clip.Location);
                imageRectangle.Intersect(clip);
                graphics.DrawImage(image, imageRectangle, 0, 0, imageRectangle.Width, imageRectangle.Height, GraphicsUnit.Pixel);
            }
            else
            {
                Rectangle visible = Rectangle.Intersect(imageRectangle, clip);
                graphics.DrawImage(image, visible, visible.X - imageRectangle.X, visible.Y - imageRectangle.Y,
                    visible.Width, visible.Height, GraphicsUnit.Pixel);
            }
        }
        else
        {
            using ImageAttributes attributes = new();
            attributes.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, imageRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
        }
    }

    private static Rectangle CalculateBackgroundImageRectangle(Rectangle bounds, Size imageSize, ImageLayout layout)
    {
        Rectangle result = bounds;
        switch (layout)
        {
            case ImageLayout.None:
                result.Size = imageSize;
                break;
            case ImageLayout.Center:
                result.Size = imageSize;
                if (bounds.Width > result.Width) { result.X = (bounds.Width - result.Width) / 2; }
                if (bounds.Height > result.Height) { result.Y = (bounds.Height - result.Height) / 2; }
                break;
            case ImageLayout.Zoom:
                float xRatio = bounds.Width / (float)imageSize.Width;
                float yRatio = bounds.Height / (float)imageSize.Height;
                if (xRatio < yRatio)
                {
                    result.Width = bounds.Width;
                    result.Height = (int)(imageSize.Height * xRatio + .5f);
                    if (bounds.Y >= 0) { result.Y = (bounds.Height - result.Height) / 2; }
                }
                else
                {
                    result.Height = bounds.Height;
                    result.Width = (int)(imageSize.Width * yRatio + .5f);
                    if (bounds.X >= 0) { result.X = (bounds.Width - result.Width) / 2; }
                }
                break;
        }
        return result;
    }

    // Graphics.GetHdc does not apply its managed clip or translation. Match
    // DeviceContextHdcScope while keeping the caller's Graphics borrowed and the
    // parent callback's Graphics independent, including when a callback throws.
    private readonly ref struct PaintHdcScope
    {
        private readonly Graphics _graphics;
        private readonly int _savedState;
        internal nint Handle { get; }

        internal unsafe PaintHdcScope(Graphics graphics)
        {
            graphics.GetContextInfo(out PointF offset, out Region? clip);
            nint region = 0;
            nint hdc = 0;
            int savedState = 0;
            try
            {
                if (clip is not null) { region = clip.GetHrgn(graphics); }
                hdc = graphics.GetHdc();
                savedState = Win32.SaveDC(hdc);
                if (savedState == 0) { throw new Win32Exception(Marshal.GetLastWin32Error()); }
                if (region != 0 && Win32.ExtSelectClipRgn(hdc, region, 1 /* RGN_AND */) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                if (!offset.IsEmpty && !Win32.OffsetViewportOrgEx(hdc, (int)offset.X, (int)offset.Y, null))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                _graphics = graphics;
                Handle = hdc;
                _savedState = savedState;
            }
            catch
            {
                if (savedState != 0) { _ = Win32.RestoreDC(hdc, savedState); }
                if (hdc != 0) { graphics.ReleaseHdc(hdc); }
                throw;
            }
            finally
            {
                if (region != 0) { _ = Win32.DeleteObject(region); }
                clip?.Dispose();
            }
        }

        public void Dispose()
        {
            _ = Win32.RestoreDC(Handle, _savedState);
            _graphics.ReleaseHdc(Handle);
        }
    }

    private void PaintWithErrorHandling(PaintEventArgs e, bool background)
    {
        if (_exceptionWhilePainting)
        {
            if (background) { PaintException(e); }
            return;
        }
        try
        {
            if (!background) { OnPaint(e); }
            else if (!GetStyle(ControlStyles.Opaque)) { OnPaintBackground(e); }
        }
        catch
        {
            _exceptionWhilePainting = true;
            Invalidate();
            throw;
        }
    }

    private void PaintException(PaintEventArgs e)
    {
        Rectangle clientRectangle = ClientRectangle;
        Rectangle rectangle = clientRectangle;
        using Pen pen = new(Color.Red, 2);
        rectangle.X++;
        rectangle.Y++;
        rectangle.Width--;
        rectangle.Height--;
        e.Graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
        rectangle.Inflate(-1, -1);
        e.Graphics.FillRectangle(Brushes.White, rectangle);
        e.Graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Top, clientRectangle.Right, clientRectangle.Bottom);
        e.Graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Bottom, clientRectangle.Right, clientRectangle.Top);
    }

    private sealed class PrintPaintEventArgs : PaintEventArgs
    {
        private GraphicsState? _savedGraphicsState;

        internal PrintPaintEventArgs(Message message, Graphics graphics, Rectangle clipRectangle)
            : base(graphics, clipRectangle)
        {
            Message = message;
            _savedGraphicsState = graphics.Save();
        }

        internal Message Message { get; }

        internal void ResetGraphics()
        {
            if (_savedGraphicsState is not { } state) { return; }
            Graphics.Restore(state);
            _savedGraphicsState = null;
        }
    }

    private void PaintWindow(ref Message message)
    {
        bool buffered = DoubleBuffered || GetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint);
        bool beginPaint = message.WParam == 0;
        Win32.PAINTSTRUCT paint = default;
        nint hdc = beginPaint ? Win32.BeginPaint(message.HWnd, out paint) : message.WParam;
        try
        {
            Rectangle clip = beginPaint
                ? Rectangle.FromLTRB(paint.rcPaint.Left, paint.rcPaint.Top, paint.rcPaint.Right, paint.rcPaint.Bottom)
                : ClientRectangle;
            if (buffered && (clip.Width <= 0 || clip.Height <= 0)) { return; }
            if (hdc == 0) { throw new Win32Exception(Marshal.GetLastWin32Error()); }
            bool background = (beginPaint && GetStyle(ControlStyles.AllPaintingInWmPaint)) || buffered;
            using BufferedGraphics? buffer = buffered ? BufferedGraphicsManager.Current.Allocate(hdc, ClientRectangle) : null;
            using Graphics? directGraphics = buffer is null ? Graphics.FromHdc(hdc) : null;
            Graphics graphics = buffer?.Graphics ?? directGraphics!;
            if (buffer is not null) { graphics.SetClip(clip); }
            using PaintEventArgs args = new(graphics, clip);
            if (background)
            {
                GraphicsState state = graphics.Save();
                PaintWithErrorHandling(args, background: true);
                graphics.Restore(state);
            }
            PaintWithErrorHandling(args, background: false);
            buffer?.Render();
        }
        finally
        {
            if (beginPaint) { _ = Win32.EndPaint(message.HWnd, ref paint); }
        }
        message.Result = 0;
    }

    /// <summary>
    /// Called when the parent form routes a <c>WM_COMMAND</c> notification to the control.
    /// </summary>
    /// <param name="notificationCode">The Win32 notification code.</param>
    /// <returns><see langword="true"/> if the notification was handled; otherwise, <see langword="false"/>.</returns>
    protected virtual bool OnCommand(int notificationCode) => false;

    /// <summary>
    /// Called when the parent form routes a <c>WM_NOTIFY</c> notification to the control.
    /// </summary>
    /// <param name="notificationCode">The Win32 notification code.</param>
    /// <param name="lParam">The raw notification payload.</param>
    /// <returns><see langword="true"/> if the notification was handled; otherwise, <see langword="false"/>.</returns>
    protected virtual bool OnNotify(int notificationCode, nint lParam) => false;

    /// <summary>
    /// Gives derived controls access to raw subclassed window messages before the native control processes them.
    /// </summary>
    /// <param name="message">The window message identifier.</param>
    /// <param name="wParam">The message <c>wParam</c> value.</param>
    /// <param name="lParam">The message <c>lParam</c> value.</param>
    /// <param name="result">Receives the handled result.</param>
    /// <returns><see langword="true"/> if the message was handled; otherwise, <see langword="false"/>.</returns>
    protected virtual bool HandleWindowMessage(uint message, nint wParam, nint lParam, out nint result)
    {
        result = 0;
        switch (message)
        {
            case Win32.WM_SETFOCUS:
                ContainerControl.NotifyControlFocused(this);
                break;
            case Win32.WM_COMMAND:
                if (lParam != 0 && TryGetControlByHandle(lParam, out Control? commandControl) && commandControl is not null)
                {
                    if (Owner is not null)
                    {
                        Owner.RouteCommandFromContainer(commandControl, Win32.HighWord(wParam), lParam);
                    }
                    else { _ = commandControl.HandleCommand(Win32.HighWord(wParam)); }
                    return true;
                }
                break;
            case Win32.WM_NOTIFY:
                if (lParam != 0)
                {
                    Win32.NMHDR header = Marshal.PtrToStructure<Win32.NMHDR>(lParam);
                    if (TryGetControlByHandle(header.hwndFrom, out Control? notifyControl) && notifyControl is not null)
                    {
                        if (Owner is not null)
                        {
                            _ = Owner.RouteNotifyFromContainer(notifyControl, unchecked((int)header.code), lParam);
                        }
                        else { _ = notifyControl.HandleNotify(unchecked((int)header.code), lParam); }
                        return true;
                    }
                }
                break;
            case Win32.WM_CTLCOLORBTN:
            case Win32.WM_CTLCOLORDLG:
            case Win32.WM_CTLCOLOREDIT:
            case Win32.WM_CTLCOLORLISTBOX:
            case Win32.WM_CTLCOLORSTATIC:
                if (wParam != 0)
                {
                    _ = TryGetControlByHandle(lParam, out Control? child);
                    Control colorControl = child ?? this;
                    if (colorControl.TryGetThemeColors(out nint brush, out uint background, out uint foreground, out bool transparent))
                    {
                        if (transparent || (child is null && message == Win32.WM_CTLCOLORSTATIC))
                        {
                            _ = Win32.SetBkMode(wParam, Win32.TRANSPARENT);
                        }
                        else { _ = Win32.SetBkColor(wParam, background); }
                        _ = Win32.SetTextColor(wParam, foreground);
                        result = brush;
                        return true;
                    }
                }
                break;
        }
        return false;
    }

    protected virtual void OnParentChanged(EventArgs e) => ParentChanged?.Invoke(this, e);

    protected virtual void OnEnabledChanged(EventArgs e)
    {
        if (NativeHandle != 0) { _ = Win32.EnableWindow(NativeHandle, Enabled); }
        EnabledChanged?.Invoke(this, e);
        for (int index = 0; _controls is not null && index < _controls.Count; index++)
        {
            _controls[index].OnParentEnabledChanged(e);
        }
    }

    protected virtual void OnParentEnabledChanged(EventArgs e)
    {
        if (_enabled) { OnEnabledChanged(e); }
    }

    protected virtual void OnVisibleChanged(EventArgs e)
    {
        if (Visible && _handle == 0 && _parent is not null)
        {
            bool canCreate = true;
            for (Control? ancestor = this; ancestor is not null; ancestor = ancestor._parent)
            {
                if (ancestor.Disposing) { canCreate = false; break; }
            }
            if (canCreate) { CreateHandleRecursive(); }
        }
        VisibleChanged?.Invoke(this, e);
        for (int index = 0; _controls is not null && index < _controls.Count; index++)
        {
            Control child = _controls[index];
            if (child.Visible) { child.OnParentVisibleChanged(e); }
        }
    }

    protected virtual void OnParentVisibleChanged(EventArgs e)
    {
        if (_visible) { OnVisibleChanged(e); }
    }

    private void AssignParent(Control? value)
    {
        Font previousFont = Font;
        Color previousForeColor = ForeColor;
        Color previousBackColor = BackColor;
        bool previousEnabled = Enabled;
        bool previousVisible = Visible;
        _parent = value;
        OnParentChanged(EventArgs.Empty);
        for (Control? ancestor = this; ancestor is not null; ancestor = ancestor._parent)
        {
            if (ancestor.Disposing) { return; }
        }
        if (previousEnabled != Enabled) { OnEnabledChanged(EventArgs.Empty); }
        bool visible = Visible;
        if (previousVisible != visible && !(!previousVisible && visible && _parent is null && !_topLevel))
        {
            OnVisibleChanged(EventArgs.Empty);
        }
        if (!previousFont.Equals(Font)) { OnFontChanged(EventArgs.Empty); }
        if (!previousForeColor.Equals(ForeColor)) { OnForeColorChanged(EventArgs.Empty); }
        else { RefreshInheritedColorTheme(background: false); }
        if (!previousBackColor.Equals(BackColor)) { OnBackColorChanged(EventArgs.Empty); }
        else { RefreshInheritedColorTheme(background: true); }
        ForceAnchorCalculations = true;
        try { _parent?.LayoutEngine.InitLayout(this, BoundsSpecified.All); }
        finally { ForceAnchorCalculations = false; }
    }

    protected virtual void OnControlAdded(ControlEventArgs e) => ControlAdded?.Invoke(this, e);

    protected virtual void OnControlRemoved(ControlEventArgs e) => ControlRemoved?.Invoke(this, e);

    /// <summary>
    /// Called after the control bounds change.
    /// </summary>
    protected virtual void OnBoundsChanged()
    {
    }

    /// <summary>
    /// Called to release control-owned resources after its native windows have been destroyed.
    /// </summary>
    protected virtual void OnDisposing()
    {
    }

    /// <summary>
    /// Applies native theming customizations after the control handle is created.
    /// </summary>
    protected virtual void ApplyTheme()
    {
    }

    /// <summary>
    /// Raises the <see cref="BackColorChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnBackColorChanged(EventArgs e)
    {
        if (GetAnyDisposingInHierarchy()) { return; }
        ReleaseBackgroundBrush();
        RefreshTheme();
        Invalidate();
        BackColorChanged?.Invoke(this, e);
        if (_controls is { } children)
        {
            for (int index = 0; index < children.Count; index++)
            {
                children[index].OnParentBackColorChanged(e);
            }
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnParentBackColorChanged(EventArgs e)
    {
        if (_backColor.IsEmpty) { OnBackColorChanged(e); }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnBackgroundImageChanged(EventArgs e)
    {
        if (GetAnyDisposingInHierarchy()) { return; }
        Invalidate();
        BackgroundImageChanged?.Invoke(this, e);
        if (_controls is { } children)
        {
            for (int index = 0; index < children.Count; index++)
            {
                children[index].OnParentBackgroundImageChanged(e);
            }
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
    {
        if (GetAnyDisposingInHierarchy()) { return; }
        Invalidate();
        BackgroundImageLayoutChanged?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnParentBackgroundImageChanged(EventArgs e) => OnBackgroundImageChanged(e);

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnFontChanged(EventArgs e)
    {
        if (GetAnyDisposingInHierarchy()) { return; }
        Invalidate();
        if (_fontHeight.HasValue) { _fontHeight = -1; }
        InvalidateNativeFont();
        if (NativeHandle != 0) { ApplyFontToHandle(); }
        FontChanged?.Invoke(this, e);
        if (_controls is { } children)
        {
            using LayoutTransaction transaction = new(this, this, nameof(Font), resumeLayout: false);
            for (int index = 0; index < children.Count; index++)
            {
                children[index].OnParentFontChanged(e);
            }
        }
        RequestLayout(this, this, nameof(Font));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnParentFontChanged(EventArgs e)
    {
        if (_font is null) { OnFontChanged(e); }
    }

    /// <summary>
    /// Raises the <see cref="ForeColorChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnForeColorChanged(EventArgs e)
    {
        if (GetAnyDisposingInHierarchy()) { return; }
        RefreshTheme();
        Invalidate();
        ForeColorChanged?.Invoke(this, e);
        if (_controls is { } children)
        {
            for (int index = 0; index < children.Count; index++)
            {
                children[index].OnParentForeColorChanged(e);
            }
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnParentForeColorChanged(EventArgs e)
    {
        if (_foreColor.IsEmpty) { OnForeColorChanged(e); }
    }

    private bool GetAnyDisposingInHierarchy()
    {
        for (Control? control = this; control is not null; control = control._parent)
        {
            if (control.Disposing) { return true; }
        }
        return false;
    }

    // Establishing/removing a local value can alter palette consumption while
    // leaving the public effective value equal. Refresh native caches without
    // manufacturing property-change notifications. Forms also keep palette
    // brushes before HWND creation, so their existing palette refresh is needed.
    private void RefreshInheritedColorTheme(bool background)
    {
        if (GetAnyDisposingInHierarchy()) { return; }
        if (background) { ReleaseBackgroundBrush(); }
        if (this is Form form) { form.RefreshVisualStyles(); }
        RefreshTheme();
        for (int index = 0; _controls is not null && index < _controls.Count; index++)
        {
            Control child = _controls[index];
            if (background ? child._backColor.IsEmpty : child._foreColor.IsEmpty)
            {
                child.RefreshInheritedColorTheme(background);
            }
        }
    }

    /// <summary>
    /// Raises the <see cref="TextChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnTextChanged(EventArgs e)
    {
        TextChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Updates the cached <see cref="Text"/> value from the native child window and raises <see cref="TextChanged"/>
    /// if the value changed.
    /// </summary>
    /// <returns><see langword="true"/> if the text changed; otherwise, <see langword="false"/>.</returns>
    protected bool UpdateTextFromHandle()
    {
        if (_handle == 0)
        {
            return false;
        }

        string currentText = Win32.GetText(NativeHandle);
        if (string.Equals(_text, currentText, StringComparison.Ordinal))
        {
            return false;
        }

        _text = currentText;
        OnTextChanged(EventArgs.Empty);
        return true;
    }

    /// <summary>
    /// Applies the standard Explorer-themed native control styling and upgrades to the dark variant when supported.
    /// </summary>
    protected void ApplyExplorerTheme()
    {
        if (_handle == 0 || !OperatingSystem.IsWindows())
        {
            return;
        }

        ResolvedVisualStyle visualStyle = CurrentVisualStyle;
        string preferredThemeClass = GetPreferredThemeClass(visualStyle);
        if (string.IsNullOrWhiteSpace(preferredThemeClass))
        {
            return;
        }

        if (Win32.SetWindowTheme(NativeHandle, preferredThemeClass, null) == 0)
        {
            return;
        }

        string fallbackThemeClass = GetFallbackThemeClass(visualStyle);
        if (!string.IsNullOrWhiteSpace(fallbackThemeClass)
            && !string.Equals(preferredThemeClass, fallbackThemeClass, StringComparison.Ordinal))
        {
            _ = Win32.SetWindowTheme(NativeHandle, fallbackThemeClass, null);
        }
    }

    /// <summary>
    /// Gets the preferred native theme class for the control.
    /// </summary>
    /// <param name="visualStyle">The resolved visual style currently active for the owning form.</param>
    /// <returns>The preferred native theme class name.</returns>
    protected virtual string GetPreferredThemeClass(ResolvedVisualStyle visualStyle)
        => visualStyle.IsDarkMode && OperatingSystem.IsWindowsVersionAtLeast(10, 0, 17763)
            ? "DarkMode_Explorer"
            : "Explorer";

    /// <summary>
    /// Gets the fallback native theme class for the control if the preferred one is unavailable.
    /// </summary>
    /// <param name="visualStyle">The resolved visual style currently active for the owning form.</param>
    /// <returns>The fallback native theme class name.</returns>
    protected virtual string GetFallbackThemeClass(ResolvedVisualStyle visualStyle)
        => "Explorer";

    private nint GetResolvedBackgroundBrush(ResolvedVisualStyle visualStyle)
    {
        if (TryGetExplicitBackColor(out Color color))
        {
            if (_backgroundBrush == 0)
            {
                _backgroundBrush = Win32.CreateSolidBrush(Win32.ToColorRef(ToArgb(color)));
                _ownsBackgroundBrush = _backgroundBrush != 0;
            }

            return _backgroundBrush != 0
                ? _backgroundBrush
                : GetFallbackBackgroundBrush();
        }

        if (this is Form form) { return form.GetThemeBrush(ThemeColorSlot.Window); }

        if (UseParentBackgroundForTheming)
        {
            if (Parent is not null)
            {
                return Parent.GetResolvedBackgroundBrush(visualStyle);
            }

            return Owner?.GetThemeBrush(ThemeColorSlot.Window) ?? 0;
        }

        return Owner?.GetThemeBrush(DefaultBackgroundSlot) ?? GetFallbackBackgroundBrush();
    }

    private uint GetResolvedBackColorRef(ResolvedVisualStyle visualStyle)
    {
        if (TryGetExplicitBackColor(out Color color))
        {
            return Win32.ToColorRef(ToArgb(color));
        }

        if (this is Form form) { return form.GetThemeBackgroundColorRef(ThemeColorSlot.Window); }

        if (UseParentBackgroundForTheming)
        {
            if (Parent is not null)
            {
                return Parent.GetResolvedBackColorRef(visualStyle);
            }

            return Owner?.GetThemeBackgroundColorRef(ThemeColorSlot.Window) ?? 0;
        }

        return Owner?.GetThemeBackgroundColorRef(DefaultBackgroundSlot)
            ?? Win32.ToColorRef(visualStyle.Palette.GetBackground(DefaultBackgroundSlot));
    }

    private uint GetResolvedForeColorRef(ResolvedVisualStyle visualStyle)
    {
        if (TryGetExplicitForeColor(out Color color))
        {
            return Win32.ToColorRef(ToArgb(color));
        }

        return Owner?.GetThemeForegroundColorRef(DefaultForegroundSlot)
            ?? Win32.ToColorRef(visualStyle.Palette.GetForeground(DefaultForegroundSlot));
    }

    private nint GetFallbackBackgroundBrush()
    {
        return DefaultBackgroundSlot switch
        {
            ThemeColorSlot.Window => Win32.GetSysColorBrush(Win32.COLOR_BTNFACE),
            ThemeColorSlot.Surface => Win32.GetSysColorBrush(Win32.COLOR_BTNFACE),
            _ => Win32.GetSysColorBrush(Win32.COLOR_WINDOW),
        };
    }

    private void ReleaseBackgroundBrush()
    {
        if (_backgroundBrush != 0 && _ownsBackgroundBrush)
        {
            _ = Win32.DeleteObject(_backgroundBrush);
        }

        _backgroundBrush = 0;
        _ownsBackgroundBrush = false;
    }

    private void ReleaseNativeResources()
    {
        try
        {
            if (NativeHandle != 0) { DestroyHandle(); }
        }
        finally
        {
            ReleaseDetachedNativeResources();
        }
    }

    private void ReleaseDetachedNativeResources()
    {
        // Native WM_NCDESTROY already clears windows destroyed by their
        // parent. Parked descendants remain live until their own Dispose;
        // clearing their handles here would orphan those native windows.
        if (_handle == 0)
        {
            ReleaseBackgroundBrush();
            ReleaseNativeFont();
            ReleaseParkingWindow();
        }
    }

    private void ReleaseParkingWindow()
    {
        nint parkingWindow = _parkingWindow;
        _parkingWindow = 0;
        if (parkingWindow != 0)
        {
            _ = Win32.DestroyWindow(parkingWindow);
        }
    }

    private static uint ToArgb(Color color)
        => unchecked((uint)color.ToArgb());

    private static nint SubclassWindowProcThunk(nint hwnd, uint message, nint wParam, nint lParam)
    {
        Control? control;
        lock (s_subclassLock)
        {
            _ = s_controlsByHandle.TryGetValue(hwnd, out control);
        }

        nint result = 0;
        Exception? callbackFailure = null;
        try
        {
            if (control is null && s_creatingControl is { NativeHandle: 0 } creating)
            {
                control = creating;
                control.RegisterNativeHandle(hwnd);
            }

            result = control is null
                ? Win32.DefWindowProcW(hwnd, message, wParam, lParam)
                : control.DispatchWindowMessage(hwnd, message, wParam, lParam);
        }
        catch (Exception exception)
        {
            // Keep the managed callback failure separate from a later native
            // lifetime failure. Each native callback is reported independently
            // so cleanup cannot replace the original exception.
            callbackFailure = exception;
        }

        List<Exception>? cleanupFailures = null;
        if (message == Win32.WM_NCDESTROY && control is not null)
        {
            try
            {
                // Native lifetime bookkeeping must run even when a derived
                // message handler consumes WM_NCDESTROY or throws.
                lock (s_subclassLock) { s_controlsByHandle.Remove(hwnd); }
            }
            catch (Exception cleanupFailure)
            {
                (cleanupFailures ??= []).Add(cleanupFailure);
            }

            try
            {
                if (control.NativeHandle == hwnd) { control.ClearNativeHandle(); }
            }
            catch (Exception cleanupFailure)
            {
                // A failed dispatcher/resource release must not mask the
                // callback failure or unwind through the unmanaged callback.
                (cleanupFailures ??= []).Add(cleanupFailure);
            }
        }

        if (callbackFailure is not null) { ReportNativeWindowException(callbackFailure); }
        if (cleanupFailures is not null)
        {
            foreach (Exception cleanupFailure in cleanupFailures)
            {
                ReportNativeWindowException(cleanupFailure);
            }
        }

        return callbackFailure is null ? result : 0;
    }

    internal nint DispatchWindowMessage(nint hwnd, uint message, nint wParam, nint lParam)
    {
        Message dispatch = Message.Create(hwnd, unchecked((int)message), wParam, lParam);
        WndProc(ref dispatch);
        return dispatch.Result;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void DefWndProc(ref Message m)
    {
        m.Result = _originalWindowProc != 0
            ? Win32.CallWindowProcW(_originalWindowProc, m.HWnd, unchecked((uint)m.Msg), m.WParam, m.LParam)
            : Win32.DefWindowProcW(m.HWnd, unchecked((uint)m.Msg), m.WParam, m.LParam);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void WndProc(ref Message m)
    {
        if (m.Msg == Win32.WM_DESTROY) { NotifyNativeHandleDestroyed(m.HWnd); }
        if (_threadDispatcher?.TryDispatch(unchecked((uint)m.Msg)) == true)
        {
            m.Result = 0;
            return;
        }

        // Retain existing control-specific native hooks inside the standard
        // virtual entry, so WndProc overrides can observe every native message.
        if (HandleWindowMessage(unchecked((uint)m.Msg), m.WParam, m.LParam, out nint interceptedResult))
        {
            m.Result = interceptedResult;
            return;
        }

        switch (m.Msg)
        {
            case Win32.WM_PAINT when GetStyle(ControlStyles.UserPaint):
                PaintWindow(ref m);
                return;
            case Win32.WM_ERASEBKGND when GetStyle(ControlStyles.UserPaint):
                if (!GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    if (m.WParam == 0) { m.Result = 0; return; }
                    _ = Win32.GetClientRect(m.HWnd, out Win32.RECT client);
                    using Graphics graphics = Graphics.FromHdc(m.WParam);
                    using PaintEventArgs args = new(graphics,
                        Rectangle.FromLTRB(client.Left, client.Top, client.Right, client.Bottom));
                    PaintWithErrorHandling(args, background: true);
                }
                m.Result = 1;
                return;
            case Win32.WM_PRINTCLIENT when GetStyle(ControlStyles.UserPaint):
                if (m.WParam != 0)
                {
                    using Graphics graphics = Graphics.FromHdc(m.WParam);
                    using PaintEventArgs args = new PrintPaintEventArgs(m, graphics, ClientRectangle);
                    OnPrint(args);
                }
                m.Result = 0;
                return;
        }

        DefWndProc(ref m);
        if (m.Msg == Win32.WM_SHOWWINDOW) { NotifyNativeVisibilityChanged(m.WParam != 0); }

        if (m.Msg == Win32.WM_CONTEXTMENU && ContextMenuStrip is not null && TryShowAttachedContextMenu(m.HWnd, m.LParam))
        {
            m.Result = 0;
            return;
        }

        if (m.Msg == Win32.WM_RBUTTONUP && ContextMenuStrip is not null)
        {
            _ = TryShowAttachedContextMenuFromClientPoint(m.HWnd, m.LParam);
        }
    }

    private WindowThreadDispatcher? FindMarshalingDispatcher()
    {
        for (Control? control = this; control is not null; control = control.Parent)
        {
            if (control.NativeHandle != 0 && control._threadDispatcher is { } dispatcher)
            {
                return dispatcher;
            }
        }

        return Owner?.ThreadDispatcher;
    }

    internal bool TryShowAttachedContextMenu(nint hwnd, nint lParam)
    {
        if (!TryResolveContextMenuScreenLocation(hwnd, lParam, out Point screenLocation))
        {
            return false;
        }

        return TryShowAttachedContextMenuAtScreenPoint(hwnd, screenLocation);
    }

    internal bool TryShowAttachedContextMenuFromClientPoint(nint hwnd, nint lParam)
    {
        if (!TryResolveClientContextMenuScreenLocation(hwnd, lParam, out Point screenLocation))
        {
            return false;
        }

        return TryShowAttachedContextMenuAtScreenPoint(hwnd, screenLocation);
    }

    internal bool TryShowAttachedContextMenuAtScreenPoint(nint hwnd, Point screenLocation)
    {
        ContextMenuStrip? contextMenuStrip = ContextMenuStrip;
        if (contextMenuStrip is null)
        {
            return false;
        }

        if (ShouldSuppressDuplicateContextMenu(screenLocation))
        {
            return true;
        }

        _lastContextMenuShownTick = Environment.TickCount64;
        _lastContextMenuScreenLocation = screenLocation;

        nint ownerHandle = Owner?.NativeHandle ?? Parent?.NativeHandle ?? hwnd;
        contextMenuStrip.ShowAtScreenPoint(ownerHandle, screenLocation, Owner?.CurrentVisualStyle ?? Application.CurrentVisualStyle);
        return true;
    }

    private bool TryResolveContextMenuScreenLocation(nint hwnd, nint lParam, out Point screenLocation)
    {
        if (lParam == (nint)(-1))
        {
            if (Win32.GetWindowRect(hwnd, out var rect))
            {
                screenLocation = new Point(rect.Left + Math.Min(16, Math.Max(0, rect.Width - 1)), rect.Top + Math.Min(16, Math.Max(0, rect.Height - 1)));
                return true;
            }

            if (Win32.GetCursorPos(out var cursor))
            {
                screenLocation = new Point(cursor.x, cursor.y);
                return true;
            }

            screenLocation = default;
            return false;
        }

        screenLocation = ExtractPoint(lParam);
        return true;
    }

    private static bool TryResolveClientContextMenuScreenLocation(nint hwnd, nint lParam, out Point screenLocation)
    {
        var point = new Win32.POINT
        {
            x = unchecked((short)(nuint)lParam),
            y = unchecked((short)(((nuint)lParam >> 16) & 0xFFFF)),
        };

        if (!Win32.ClientToScreen(hwnd, ref point))
        {
            screenLocation = default;
            return false;
        }

        screenLocation = new Point(point.x, point.y);
        return true;
    }

    private bool ShouldSuppressDuplicateContextMenu(Point screenLocation)
    {
        long elapsed = Environment.TickCount64 - _lastContextMenuShownTick;
        return elapsed is >= 0 and <= 250
            && Math.Abs(_lastContextMenuScreenLocation.X - screenLocation.X) <= 4
            && Math.Abs(_lastContextMenuScreenLocation.Y - screenLocation.Y) <= 4;
    }

    internal static Point ExtractPoint(nint lParam)
    {
        return new Point(
            unchecked((short)(nuint)lParam),
            unchecked((short)(((nuint)lParam >> 16) & 0xFFFF)));
    }

    private void ApplyBounds()
    {
        if (NativeHandle != 0)
        {
            int nativeLeft = _left;
            int nativeTop = _top;

            _applyingBounds = true;
            try
            {
                _ = Win32.MoveWindow(NativeHandle, nativeLeft, nativeTop, _width, GetNativeHeight(_height), true);
            }
            finally { _applyingBounds = false; }
        }
    }

    private void UpdateClientSize()
    {
        if (NativeHandle != 0)
        {
            if (!Win32.GetClientRect(NativeHandle, out Win32.RECT client))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            _clientWidth = client.Right - client.Left;
            _clientHeight = client.Bottom - client.Top;
        }
        else
        {
            Size nonClient = GetNonClientSize();
            _clientWidth = unchecked(_width - nonClient.Width);
            _clientHeight = unchecked(_height - nonClient.Height);
        }
    }

    private Size GetNonClientSize()
    {
        CreateParams parameters = CreateParams;
        Win32.RECT rectangle = default;
        if (!Win32.AdjustWindowRectEx(ref rectangle, unchecked((uint)parameters.Style), false, unchecked((uint)parameters.ExStyle)))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        return new Size(rectangle.Right - rectangle.Left, rectangle.Bottom - rectangle.Top);
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    /// <summary>
    /// Represents the ordered child controls belonging to a control.
    /// </summary>
    [ListBindable(false)]
    public class ControlCollection : Layout.ArrangedElementCollection, IList, ICloneable, IEnumerable<Control>
    {
        private int _lastAccessedIndex = -1;

        public ControlCollection(Control owner)
        {
            ArgumentNullException.ThrowIfNull(owner);
            Owner = owner;
        }

        public Control Owner { get; }

        internal ReadOnlySpan<Control> ItemsSpan => CollectionsMarshal.AsSpan(InnerList);

        public virtual Control this[int index]
        {
            get
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
                return InnerList[index];
            }
        }

        public virtual Control? this[string? key]
        {
            get
            {
                if (string.IsNullOrEmpty(key)) { return null; }
                int index = IndexOfKey(key);
                return IsValidIndex(index) ? this[index] : null;
            }
        }

        public virtual void Add(Control? value)
        {
            if (value is null) { return; }
            if (value.GetTopLevel())
            {
                throw new ArgumentException("Top-level control cannot be added to a control.");
            }

            uint ownerThread = Owner.NativeHandle == 0 ? Win32.GetCurrentThreadId() : Win32.GetWindowThreadProcessId(Owner.NativeHandle, out _);
            uint childThread = value.NativeHandle == 0 ? Win32.GetCurrentThreadId() : Win32.GetWindowThreadProcessId(value.NativeHandle, out _);
            if (ownerThread != childThread)
            {
                throw new ArgumentException("Controls created on one thread cannot be parented to a control on a different thread.");
            }
            for (Control? ancestor = Owner; ancestor is not null; ancestor = ancestor._parent)
            {
                if (ancestor == value)
                {
                    throw new ArgumentException("A circular control reference has been made. A control cannot be owned by or parented to itself.");
                }
            }
            if (value._parent == Owner)
            {
                Owner.Controls.SetChildIndex(value, -1);
                return;
            }

            value._parent?.Controls.Remove(value);
            InnerList.Add(value);
            if (value._tabIndex == -1)
            {
                int next = 0;
                for (int i = 0; i < Count - 1; i++)
                {
                    int tabIndex = this[i].TabIndex;
                    if (next <= tabIndex) { next = unchecked(tabIndex + 1); }
                }
                value._tabIndex = next;
            }

            Owner.SuspendLayout();
            try
            {
                Control? previous = value._parent;
                try
                {
                    (Owner as Form ?? Owner.Owner)?.AttachControl(value, Owner);
                    value.AssignParent(Owner);
                }
                finally
                {
                    if (previous != value._parent)
                    {
                        value.ReparentNativeWindow();
                        if (Owner.NativeHandle != 0 && value.Visible && !value.IsDisposed)
                        {
                            value.CreateHandleRecursive();
                        }
                    }
                }
            }
            finally { Owner.ResumeLayout(false); }

            value.InitLayout();
            Owner.RequestLayout(value, nameof(Parent));
            Owner.OnControlAdded(new ControlEventArgs(value));
        }

        // Historical table-layout convenience remains available to existing
        // designer files until TableLayoutControlCollection owns that overload.
        public void Add(Control control, int column, int row)
        {
            if (Owner is TableLayoutPanel table) { table.AddCell(control, column, row); }
            else { Add(control); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual void AddRange(params Control[] controls)
        {
            ArgumentNullException.ThrowIfNull(controls);
            if (controls.Length == 0) { return; }
            Owner.SuspendLayout();
            try
            {
                for (int i = 0; i < controls.Length; i++) { Add(controls[i]); }
            }
            finally { Owner.ResumeLayout(true); }
        }

        public void AddRange(IEnumerable<Control> controls)
        {
            ArgumentNullException.ThrowIfNull(controls);
            AddRange(controls.ToArray());
        }

        public virtual void Remove(Control? value)
        {
            if (value is null || value._parent != Owner) { return; }

            // Park an existing native window before the managed link changes.
            // Removal must preserve the handle and the child's owned descendants.
            if (value.NativeHandle != 0)
            {
                Marshal.SetLastPInvokeError(0);
                nint previous = Win32.SetParent(value.NativeHandle, value.GetParkingWindow());
                int error = Marshal.GetLastPInvokeError();
                if (previous == 0 && error != 0) { throw new Win32Exception(error); }
            }
            InnerList.Remove(value);
            value.Owner?.DetachControl(value);
            value.AssignParent(null);
            Owner.OnChildRemoved(value);
            Owner.RequestLayout(value, nameof(Parent));
            Owner.OnControlRemoved(new ControlEventArgs(value));
            ContainerControl.NotifyControlRemoved(Owner, value);
        }

        public void RemoveAt(int index) => Remove(this[index]);

        public virtual void RemoveByKey(string? key)
        {
            int index = IndexOfKey(key);
            if (IsValidIndex(index)) { RemoveAt(index); }
        }

        public virtual void Clear()
        {
            Owner.SuspendLayout();
            try
            {
                while (Count != 0) { RemoveAt(Count - 1); }
            }
            finally { Owner.ResumeLayout(true); }
        }

        public bool Contains(Control? control) => ((IList)InnerList).Contains(control);

        public int IndexOf(Control? control) => ((IList)InnerList).IndexOf(control);

        public virtual bool ContainsKey(string? key) => IsValidIndex(IndexOfKey(key));

        public virtual int IndexOfKey(string? key)
        {
            if (string.IsNullOrEmpty(key)) { return -1; }
            if (IsValidIndex(_lastAccessedIndex) && NameMatches(this[_lastAccessedIndex].Name, key))
            {
                return _lastAccessedIndex;
            }
            for (int index = 0; index < Count; index++)
            {
                if (NameMatches(this[index].Name, key))
                {
                    _lastAccessedIndex = index;
                    return index;
                }
            }
            _lastAccessedIndex = -1;
            return -1;
        }

        public Control[] Find(string key, bool searchAllChildren)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "The key may not be null or empty.");
            }
            List<Control> matches = [];
            FindChildren(this, key, searchAllChildren, matches);
            return [.. matches];
        }

        private static void FindChildren(ControlCollection controls, string key, bool searchAllChildren, List<Control> matches)
        {
            try
            {
                for (int index = 0; index < controls.Count; index++)
                {
                    // The indexer is virtual. Preserve each standard access,
                    // including derived collections that return null or throw.
                    if (controls[index] is null) { continue; }
                    if (NameMatches(controls[index].Name, key)) { matches.Add(controls[index]); }
                }
                if (!searchAllChildren) { return; }
                for (int index = 0; index < controls.Count; index++)
                {
                    if (controls[index] is null) { continue; }
                    if (controls[index].Controls.Count > 0)
                    {
                        FindChildren(controls[index].Controls, key, true, matches);
                    }
                }
            }
            catch (Exception exception) when (exception is not (NullReferenceException
                or StackOverflowException or OutOfMemoryException or System.Threading.ThreadAbortException
                or IndexOutOfRangeException or AccessViolationException))
            {
                // WinForms retains matches collected before an ordinary derived
                // collection failure. Critical runtime exceptions propagate.
            }
        }

        public int GetChildIndex(Control child) => GetChildIndex(child, true);

        public virtual int GetChildIndex(Control child, bool throwException)
        {
            int index = IndexOf(child);
            if (index < 0 && throwException) { throw new ArgumentException("Control is not a child of this parent."); }
            return index;
        }

        public virtual void SetChildIndex(Control child, int newIndex)
        {
            ArgumentNullException.ThrowIfNull(child);
            int current = GetChildIndex(child);
            if (current == newIndex) { return; }
            if (newIndex >= Count || newIndex == -1) { newIndex = Count - 1; }
            if (newIndex < 0) { _ = InnerList[newIndex]; }
            InnerList.RemoveAt(current);
            InnerList.Insert(newIndex, child);
            child.UpdateChildZOrder();
            Owner.RequestLayout(child, "ChildIndex");
        }

        object ICloneable.Clone()
        {
            ControlCollection clone = Owner.CreateControlsInstance();
            clone.InnerList.AddRange(InnerList);
            return clone;
        }

        int IList.Add(object? control)
        {
            if (control is not Control value) { throw new ArgumentException("Object must be a Control.", nameof(control)); }
            Add(value);
            return IndexOf(value);
        }

        void IList.Remove(object? control)
        {
            if (control is Control value) { Remove(value); }
        }

        void IList.Clear() => Clear();
        void IList.RemoveAt(int index) => RemoveAt(index);

        public override IEnumerator GetEnumerator() => new ControlCollectionEnumerator(this);

        IEnumerator<Control> IEnumerable<Control>.GetEnumerator()
        {
            IEnumerator enumerator = GetEnumerator();
            while (enumerator.MoveNext()) { yield return (Control)enumerator.Current!; }
        }

        private bool IsValidIndex(int index) => index >= 0 && index < Count;

        private static bool NameMatches(string? name, string key)
            => name?.Length == key.Length && string.Equals(name, key, StringComparison.InvariantCultureIgnoreCase);

        private sealed class ControlCollectionEnumerator(ControlCollection controls) : IEnumerator
        {
            private readonly int _originalCount = controls.Count;
            private int _current = -1;

            public object? Current => _current < 0 ? null : controls[_current];

            public bool MoveNext()
            {
                if (_current < controls.Count - 1 && _current < _originalCount - 1)
                {
                    _current++;
                    return true;
                }
                return false;
            }

            public void Reset() => _current = -1;
        }
    }

    /// <summary>
    /// Initializes layout after the control is added to its parent.
    /// </summary>
    protected virtual void InitLayout()
    {
        LayoutEngine.InitLayout(this, BoundsSpecified.All);
        // Preserve the existing container initialization path for tab pages,
        // table cells, and hosted items when their parent is first assigned.
        _parent?.OnChildAdded(this);
    }
}
