using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Lumina.Forms;

/// <summary>
/// Provides the common WinForms-like surface for all LuminaForms controls backed by Win32 child windows.
/// </summary>
public class Control : Component, IWin32Window, ISynchronizeInvoke
{
    private static readonly Lock s_subclassLock = new();
    private static readonly Dictionary<nint, Control> s_controlsByHandle = [];
    private static readonly Win32.WindowProc s_subclassProc = SubclassWindowProcThunk;

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
    private Font? _font;
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
            _parent?.PerformLayout(this, nameof(AutoSize));
            OnAutoSizeChanged(EventArgs.Empty);
        }
    }

    public AutoSizeMode AutoSizeMode
    {
        get => _autoSizeMode;
        set => _autoSizeMode = value;
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
            if (_dock != DockStyle.None) { Dock = DockStyle.None; }
            _anchor = value;
            if (((previous & ~value) & (AnchorStyles.Right | AnchorStyles.Bottom)) != 0)
            {
                _parent?._parent?.PerformLayout(this, nameof(Anchor));
            }
            _parent?.PerformLayout(this, nameof(Anchor));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can move the focus to this control with the Tab key.
    /// </summary>
    public bool TabStop { get; set; } = true;

    public Font Font
    {
        get => _font ??= SystemFonts.DefaultFont;
        set => _font = value;
    }

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
            _parent?.PerformLayout(this, nameof(MaximumSize));
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
            if (_layoutDirty) { PerformLayout(this, nameof(Padding)); }
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
            _parent?.PerformLayout(this, nameof(Margin));
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
                _dock = value;
                using (new LayoutTransaction(_parent, this, nameof(Dock)))
                {
                    // Dock geometry is handled by the containing layout algorithm.
                }
                OnDockChanged(EventArgs.Empty);
            }
            finally { ResumeLayout(); }
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

    /// <summary>
    /// Occurs when the <see cref="ForeColor"/> property changes.
    /// </summary>
    public event EventHandler? ForeColorChanged;

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
    /// Gets or sets the background color used by the control. Set <see cref="Color.Empty"/> to fall back to the active theme.
    /// </summary>
    public Color BackColor
    {
        get => _backColor;
        set
        {
            ThrowIfDisposed();
            if (_backColor == value)
            {
                return;
            }

            _backColor = value;
            ReleaseBackgroundBrush();
            OnBackColorChanged(EventArgs.Empty);
            RefreshTheme();
        }
    }

    /// <summary>
    /// Gets or sets the foreground color used by the control. Set <see cref="Color.Empty"/> to fall back to the active theme.
    /// </summary>
    public Color ForeColor
    {
        get => _foreColor;
        set
        {
            ThrowIfDisposed();
            if (_foreColor == value)
            {
                return;
            }

            _foreColor = value;
            OnForeColorChanged(EventArgs.Empty);
            RefreshTheme();
        }
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
    public virtual Rectangle DisplayRectangle => new(
        Padding.Left,
        Padding.Top,
        Math.Max(0, Width - Padding.Horizontal),
        Math.Max(0, Height - Padding.Vertical));

    internal Form? Owner { get; private set; }

    internal int Id { get; private set; }

    /// <summary>
    /// Gets the Win32 class name used to create the underlying child window.
    /// </summary>
    protected virtual string ClassName => "STATIC";

    /// <summary>
    /// Gets the Win32 style flags used when creating the underlying child window.
    /// </summary>
    protected virtual uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE | Win32.WS_CLIPCHILDREN | Win32.WS_CLIPSIBLINGS;

    /// <summary>
    /// Gets optional Win32 extended style flags for the underlying child window.
    /// </summary>
    protected virtual uint ExStyle => 0;

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
            SetBoundsCore(x, y, width, height, specified);
            _parent?.PerformLayout(this, nameof(Bounds));
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
                _parent?.PerformLayout(this, nameof(Bounds));
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
    {
        if (NativeHandle != 0)
        {
            _ = Win32.InvalidateRect(NativeHandle, 0, true);
        }
    }

    /// <summary>
    /// Forces the control to repaint immediately.
    /// </summary>
    public void Refresh()
    {
        if (NativeHandle != 0)
        {
            Invalidate();
            _ = Win32.UpdateWindow(NativeHandle);
        }
    }

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
        }
    }

    /// <summary>
    /// Raises the <see cref="Layout"/> event.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnLayout(LayoutEventArgs levent) => Layout?.Invoke(this, levent);

    protected virtual void OnPaddingChanged(EventArgs e) => PaddingChanged?.Invoke(this, e);
    protected virtual void OnMarginChanged(EventArgs e) => MarginChanged?.Invoke(this, e);
    protected virtual void OnDockChanged(EventArgs e) => DockChanged?.Invoke(this, e);
    protected virtual void OnAutoSizeChanged(EventArgs e) => AutoSizeChanged?.Invoke(this, e);

    // Property transactions queue the original reason before user callbacks.
    // Nested changes therefore preserve the same first-request/Resume behavior
    // as explicit SuspendLayout and PerformLayout calls.
    private readonly struct LayoutTransaction : IDisposable
    {
        private readonly Control? _container;

        internal LayoutTransaction(Control? container)
        {
            _container = container;
            container?.SuspendLayout();
        }

        internal LayoutTransaction(Control? container, Control affectedControl, string property)
            : this(container)
        {
            container?.PerformLayout(affectedControl, property);
        }

        public void Dispose() => _container?.ResumeLayout();
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
        try
        {
            try
            {
                try
                {
                    // Native children are destroyed with their parent HWND.
                    // ParentChanged and ControlRemoved must observe that the
                    // disposed control no longer owns a live window.
                    ReleaseNativeResources();
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
                finally
                {
                    // A standard DestroyHandle override is called once. A
                    // skipped or failed base call deliberately leaves its
                    // native window available for the next explicit attempt.
                    ReleaseDetachedNativeResources();
                }
            }
            finally
            {
                // A failed standard notification leaves Component.Site and its
                // Disposed event untouched until a later disposal attempt.
                if (disposeComponent) { base.Dispose(true); }
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
            int nativeHeight = GetNativeHeight(_height);
            nint parentHandle = _parent?._handle ?? 0;
            if (parentHandle == 0 && !_topLevel) { parentHandle = GetParkingWindow(); }

            // Install the subclass before showing the window, so the initial
            // WM_SHOWWINDOW follows HandleCreated and creates the visible tree.
            uint style = _topLevel ? Style & ~Win32.WS_CHILD : Style | Win32.WS_CHILD;
            _handle = Win32.CreateWindowExW(
                ExStyle, ClassName, _text, style & ~Win32.WS_VISIBLE,
                _left, _top, _width, nativeHeight, parentHandle, (nint)Id,
                Owner?.InstanceHandle ?? Win32.GetModuleHandleW(null), 0);

            if (_handle == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), $"Failed to create control '{ClassName}'.");
            }

            if (_width < 0 || _height < 0)
            {
                if (!Win32.GetWindowRect(_handle, out Win32.RECT bounds))
                {
                    int error = Marshal.GetLastWin32Error();
                    _ = Win32.DestroyWindow(_handle);
                    _handle = 0;
                    throw new Win32Exception(error);
                }
                _width = bounds.Right - bounds.Left;
                _height = nativeHeight == _height ? bounds.Bottom - bounds.Top : Math.Max(0, _height);
            }
            UpdateClientSize();
            try { RegisterSubclass(); }
            catch (Exception registrationException)
            {
                // Until subclass registration succeeds, WM_NCDESTROY may not
                // reach us. Roll back the newly allocated HWND explicitly and
                // keep the registration exception (including its Win32 error).
                nint failedHandle = _handle;
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
            if (Owner is { UiFontHandle: not 0 } owner)
            {
                _ = Win32.SendMessageW(_handle, Win32.WM_SETFONT, owner.UiFontHandle, (nint)1);
            }
            _ = Win32.EnableWindow(_handle, Enabled);
            UpdateChildZOrder();
            ApplyTheme();
            NotifyNativeHandleCreated();
            if (_visible && _handle != 0) { _ = Win32.ShowWindow(_handle, Win32.SW_SHOW); }
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
        if (_recreatingHandle) { _threadDispatcher?.SuspendForRecreation(); }
        else
        {
            _threadDispatcher?.Close();
            _threadDispatcher = null;
        }
        _originalWindowProc = 0;
        _handle = 0;
        _created = false;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void DestroyHandle()
    {
        if (_handle == 0 || _destroyingHandle) { return; }
        if (!_recreatingHandle) { _threadDispatcher?.Close(); }
        _destroyingHandle = true;
        try
        {
            nint handle = _handle;
            if (!Win32.DestroyWindow(handle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            // A failed subclass-registration rollback can leave an owned HWND
            // with the original native procedure. It has no cleanup callback.
            if (_handle == handle) { ClearNativeHandle(); }
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
            _parent?.PerformLayout(this, nameof(Bounds));
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
        transparentBackground = UseParentBackgroundForTheming && BackColor.IsEmpty;
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
        PerformLayout(this, nameof(Bounds));
        Resize?.Invoke(this, e);
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
        BackColorChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="ForeColorChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnForeColorChanged(EventArgs e)
    {
        ForeColorChanged?.Invoke(this, e);
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
        if (!BackColor.IsEmpty)
        {
            if (_backgroundBrush == 0)
            {
                _backgroundBrush = Win32.CreateSolidBrush(Win32.ToColorRef(ToArgb(BackColor)));
                _ownsBackgroundBrush = _backgroundBrush != 0;
            }

            return _backgroundBrush != 0
                ? _backgroundBrush
                : GetFallbackBackgroundBrush();
        }

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
        if (!BackColor.IsEmpty)
        {
            return Win32.ToColorRef(ToArgb(BackColor));
        }

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
        if (!ForeColor.IsEmpty)
        {
            return Win32.ToColorRef(ToArgb(ForeColor));
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

    private void RegisterSubclass()
    {
        if (_handle == 0 || _originalWindowProc != 0)
        {
            return;
        }

        nint subclassPointer = Marshal.GetFunctionPointerForDelegate(s_subclassProc);
        nint previousWindowProc = Win32.SetWindowLongPtrW(NativeHandle, Win32.GWLP_WNDPROC, subclassPointer);
        if (previousWindowProc == 0)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        _originalWindowProc = previousWindowProc;
        RegisterNativeHandle(_handle);
    }

    private static nint SubclassWindowProcThunk(nint hwnd, uint message, nint wParam, nint lParam)
    {
        Control? control;
        lock (s_subclassLock)
        {
            _ = s_controlsByHandle.TryGetValue(hwnd, out control);
        }

        if (control is null)
        {
            return Win32.DefWindowProcW(hwnd, message, wParam, lParam);
        }

        try
        {
            return control.SubclassWindowProc(hwnd, message, wParam, lParam);
        }
        catch (Exception exception)
        {
            // Exceptions cannot unwind across the unmanaged window callback.
            // Use the same UI exception route as other native notifications.
            ReportNativeWindowException(exception);
            return 0;
        }
        finally
        {
            // Native lifetime bookkeeping must run even when a derived control
            // handles WM_NCDESTROY itself or its message handler throws.
            if (message == Win32.WM_NCDESTROY)
            {
                lock (s_subclassLock) { s_controlsByHandle.Remove(hwnd); }
                if (control.NativeHandle == hwnd) { control.ClearNativeHandle(); }
            }
        }
    }

    private nint SubclassWindowProc(nint hwnd, uint message, nint wParam, nint lParam)
    {
        if (message == Win32.WM_DESTROY) { NotifyNativeHandleDestroyed(hwnd); }
        if (_threadDispatcher?.TryDispatch(message) == true)
        {
            return 0;
        }

        if (HandleWindowMessage(message, wParam, lParam, out nint interceptedResult))
        {
            return interceptedResult;
        }

        nint result = _originalWindowProc != 0
            ? Win32.CallWindowProcW(_originalWindowProc, hwnd, message, wParam, lParam)
            : Win32.DefWindowProcW(hwnd, message, wParam, lParam);

        if (message == Win32.WM_SHOWWINDOW) { NotifyNativeVisibilityChanged(wParam != 0); }

        if (message == Win32.WM_CONTEXTMENU && ContextMenuStrip is not null && TryShowAttachedContextMenu(hwnd, lParam))
        {
            return 0;
        }

        if (message == Win32.WM_RBUTTONUP && ContextMenuStrip is not null)
        {
            _ = TryShowAttachedContextMenuFromClientPoint(hwnd, lParam);
        }

        return result;
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

            if (Parent is Form
                && Owner is not null
                && this is not MenuStrip
                && this is not ToolStrip
                && this is not StatusStrip)
            {
                nativeTop += Owner.GetClientContentTopInset();
            }

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
        Win32.RECT rectangle = default;
        if (!Win32.AdjustWindowRectEx(ref rectangle, Style, false, ExStyle))
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
            Owner.PerformLayout(value, nameof(Parent));
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
            Owner.PerformLayout(value, nameof(Parent));
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
            Owner.PerformLayout(child, "ChildIndex");
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
        // Preserve the existing container initialization path for tab pages,
        // table cells, and hosted items when their parent is first assigned.
        _parent?.OnChildAdded(this);
    }
}
