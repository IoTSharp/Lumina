// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// Portions adapted from dotnet/winforms v10.0.11 for Lumina.Forms.

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Lumina.Forms.Layout;

namespace Lumina.Forms;

/// <summary>
/// Represents a basic tool strip surface that can host logical items.
/// </summary>
public class ToolStrip : ContainerControlBase
{
    private const int StripItemSpacing = 0;

    private readonly ToolStripItemCollection _items;
    private readonly Dictionary<ToolStripItem, Control> _itemHosts = [];
    private bool _updatingItemHosts;

    internal event EventHandler? ItemsChanged;

    /// <summary>
    /// Initializes a tool strip with a typical strip height.
    /// </summary>
    public ToolStrip()
    {
        _items = new ToolStripItemCollection(OnItemsChanged);
        Size = new Size(100, 25);
        Height = 25;
        Dock = DockStyle.Top;
        Margin = Padding.Empty;
        Padding = new Padding(0, 0, 1, 0);
        TabStop = false;
        CanOverflow = true;
        ShowItemToolTips = true;
        GripStyle = ToolStripGripStyle.Visible;
        LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
    }

    /// <summary>
    /// Gets the items contained in the strip.
    /// </summary>
    public ToolStripItemCollection Items => _items;

    /// <summary>
    /// Gets or sets whether items may overflow onto another surface.
    /// </summary>
    public bool CanOverflow { get; set; }

    /// <summary>
    /// Gets or sets whether item tooltips are shown.
    /// </summary>
    public bool ShowItemToolTips { get; set; }

    /// <summary>
    /// Gets or sets whether the strip stretches across its docked edge.
    /// </summary>
    public bool Stretch { get; set; }

    /// <summary>
    /// Gets or sets the grip visibility.
    /// </summary>
    public ToolStripGripStyle GripStyle { get; set; }

    /// <summary>
    /// Gets or sets the layout style.
    /// </summary>
    public ToolStripLayoutStyle LayoutStyle { get; set; }

    /// <inheritdoc />
    [DefaultValue(DockStyle.Top)]
    public override DockStyle Dock
    {
        get => base.Dock;
        set
        {
            if (value == Dock)
            {
                return;
            }

            if (value < DockStyle.None || value > DockStyle.Fill)
            {
                throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(DockStyle));
            }

            using (new LayoutTransaction(this, this, nameof(Dock)))
            using (new LayoutTransaction(Parent, this, nameof(Dock)))
            {
                SetDockCore(value);
                UpdateDockLayoutStyle();
            }

            OnDockChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets the direction in which the strip arranges its items.
    /// </summary>
    [Browsable(false)]
    public Orientation Orientation => LayoutStyle switch
    {
        ToolStripLayoutStyle.HorizontalStackWithOverflow => Orientation.Horizontal,
        ToolStripLayoutStyle.VerticalStackWithOverflow => Orientation.Vertical,
        _ => Dock is DockStyle.Left or DockStyle.Right ? Orientation.Vertical : Orientation.Horizontal,
    };

    private void UpdateDockLayoutStyle()
    {
        if (LayoutStyle is ToolStripLayoutStyle.HorizontalStackWithOverflow or ToolStripLayoutStyle.VerticalStackWithOverflow)
        {
            return;
        }

        // Dock determines the orientation of the stack. Defer both the strip's
        // real item layout and the parent's dock arrangement to their enclosing
        // transactions, then deliver DockChanged after those layouts finish.
        using (new LayoutTransaction(this, this, nameof(Orientation)))
        {
            ClearPreferredSizeCache();
        }

        Parent?.RequestLayout(this, nameof(Orientation));
    }

    /// <inheritdoc />
    public override LayoutEngine LayoutEngine => StripLayoutEngine.Instance;

    /// <inheritdoc />
    public override Rectangle DisplayRectangle
    {
        get
        {
            int grip = GripStyle == ToolStripGripStyle.Visible ? 7 : 0;
            bool vertical = Orientation == Orientation.Vertical;
            int left = Padding.Left + (vertical ? 0 : grip);
            int top = Padding.Top + (vertical ? grip : 0);
            return new Rectangle(left, top,
                Math.Max(0, ClientSize.Width - Padding.Horizontal - (vertical ? 0 : grip)),
                Math.Max(0, ClientSize.Height - Padding.Vertical - (vertical ? grip : 0)));
        }
    }

    /// <inheritdoc />
    protected override string ClassName => "STATIC";

    /// <inheritdoc />
    protected override uint Style => Win32.WS_CHILD | Win32.WS_VISIBLE;

    /// <inheritdoc />
    protected override void OnHandleCreated()
    {
        base.OnHandleCreated();
        ClearNativeText();
        ApplyNativeThemeState();
        PerformLayout();
    }

    /// <inheritdoc />
    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        ClearNativeText();
    }

    /// <inheritdoc />
    protected override void ApplyTheme()
    {
        ApplyNativeThemeState();
    }

    private protected virtual void ApplyNativeThemeState()
    {
        if (NativeHandle == 0)
        {
            return;
        }

        DarkModeNative.ApplyThemeToWindow(NativeHandle, CurrentVisualStyle.IsDarkMode);
    }

    private void ClearNativeText()
    {
        if (NativeHandle != 0)
        {
            _ = Win32.SetWindowTextW(NativeHandle, string.Empty);
        }
    }

    /// <inheritdoc />
    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);
    }

    private void LayoutStrip()
    {
        // Default constraints can request layout from Control's constructor,
        // before this constructor has created the logical item collection.
        if (_items is null)
        {
            return;
        }

        EnsureItemHosts();
        LayoutItemHosts();
    }

    private sealed class StripLayoutEngine : LayoutEngine
    {
        internal static readonly StripLayoutEngine Instance = new();

        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            ArgumentNullException.ThrowIfNull(container);
            if (container is not ToolStrip strip)
            {
                return base.Layout(container, layoutEventArgs);
            }

            strip.LayoutStrip();
            return strip.AutoSize;
        }
    }

    private void OnItemsChanged()
    {
        ItemsChanged?.Invoke(this, EventArgs.Empty);

        if (Owner is not null || NativeHandle != 0)
        {
            PerformLayout();
        }
    }

    internal bool TryActivateMnemonic(char mnemonic)
    {
        EnsureItemHosts();

        char normalizedMnemonic = char.ToUpperInvariant(mnemonic);
        foreach (ToolStripItem item in Items)
        {
            if (!item.Visible || !item.Enabled)
            {
                continue;
            }

            if (item.GetMnemonic() != normalizedMnemonic)
            {
                continue;
            }

            ActivateItem(item);
            return true;
        }

        return false;
    }

    internal bool TryActivateFirstItem()
    {
        EnsureItemHosts();

        foreach (ToolStripItem item in Items)
        {
            if (!item.Visible || !item.Enabled)
            {
                continue;
            }

            ActivateItem(item);
            return true;
        }

        return false;
    }

    private protected bool TryGetItemHost(ToolStripItem item, out Control? host)
        => _itemHosts.TryGetValue(item, out host);

    private protected virtual bool ShouldCreateHostControl(ToolStripItem item) => true;

    private protected virtual bool IsCompatibleHostControl(Control host, ToolStripItem item)
    {
        if (OperatingSystem.IsWindows()
            && item.DisplayStyle == ToolStripItemDisplayStyle.Image
            && item.Image is not null)
        {
            return host is PictureBox;
        }

        return item switch
        {
            ToolStripLabel => host is Label,
            ToolStripStatusLabel => host is Label,
            ToolStripProgressBar => host is ProgressBar,
            ToolStripComboBox => host is ComboBox,
            ToolStripTextBox => host is TextBox,
            ToolStripSeparator => host is Panel,
            ToolStripDropDownItem => host is Button,
            ToolStripButton => host is Button,
            _ => host is Button,
        };
    }

    private protected void EnsureItemHosts()
    {
        if (_updatingItemHosts || Disposing || IsDisposed)
        {
            return;
        }

        // ControlCollection mutations now request layout synchronously. Keep
        // this reconciliation atomic when adding or replacing a hosted child.
        _updatingItemHosts = true;
        try
        {
            foreach (ToolStripItem item in Items)
            {
                if (!ShouldCreateHostControl(item))
                {
                    if (_itemHosts.Remove(item, out Control? removedHost))
                    {
                        RemoveItemHost(removedHost);
                    }

                    continue;
                }

                if (_itemHosts.TryGetValue(item, out Control? existingHost))
                {
                    if (existingHost is not null && IsCompatibleHostControl(existingHost, item))
                    {
                        continue;
                    }

                    _itemHosts.Remove(item);
                    if (existingHost is not null)
                    {
                        RemoveItemHost(existingHost);
                    }
                }

                Control host = CreateHostControl(item);
                _itemHosts[item] = host;
                Controls.Add(host);
            }
        }
        finally { _updatingItemHosts = false; }
    }

    private protected virtual void LayoutItemHosts()
    {
        int x = Padding.Left;
        int y = Padding.Top;
        int availableHeight = Math.Max(1, Height - Padding.Vertical);
        bool vertical = Orientation == Orientation.Vertical;

        foreach (ToolStripItem item in Items)
        {
            if (!_itemHosts.TryGetValue(item, out Control? host))
            {
                continue;
            }

            ApplyItemState(host, item);
            host.Visible = item.Visible;
            if (!item.Visible)
            {
                continue;
            }

            Size hostSize = ResolveHostSize(item, availableHeight);
            int itemX = vertical ? Padding.Left + Math.Max(0, (Width - Padding.Horizontal - hostSize.Width) / 2) : x;
            int itemY = vertical ? y : Padding.Top + Math.Max(0, (availableHeight - hostSize.Height) / 2);
            host.SetBounds(itemX, itemY, hostSize.Width, hostSize.Height);
            if (vertical)
            {
                y += hostSize.Height + StripItemSpacing;
            }
            else
            {
                x += hostSize.Width + StripItemSpacing;
            }
        }
    }

    private protected void LayoutItemHosts(int x, int y, int itemSpacing, int availableHeight, int explicitItemHeight)
    {
        foreach (ToolStripItem item in Items)
        {
            if (!_itemHosts.TryGetValue(item, out Control? host))
            {
                continue;
            }

            ApplyItemState(host, item);
            host.Visible = item.Visible;
            if (!item.Visible)
            {
                continue;
            }

            Size hostSize = ResolveHostSize(item, availableHeight);
            int height = explicitItemHeight > 0 ? explicitItemHeight : hostSize.Height;
            host.SetBounds(x, y, hostSize.Width, height);
            x += hostSize.Width + itemSpacing;
        }
    }

    /// <summary>
    /// Creates the native host control used to represent a logical strip item on the surface.
    /// </summary>
    /// <param name="item">The item that needs a host control.</param>
    /// <returns>The control used to host the item.</returns>
    protected virtual Control CreateHostControl(ToolStripItem item)
    {
        if (OperatingSystem.IsWindows()
            && item.DisplayStyle == ToolStripItemDisplayStyle.Image
            && item.Image is not null)
        {
            return item is ToolStripDropDownItem
                ? CreateImageDropDownHost(item)
                : CreateImageButtonHost(item);
        }

        return item switch
        {
            ToolStripLabel => new Label(),
            ToolStripStatusLabel => new Label(),
            ToolStripProgressBar => new ProgressBar(),
            ToolStripComboBox => new ComboBox(),
            ToolStripTextBox => new TextBox(),
            ToolStripSeparator => new Panel(),
            ToolStripDropDownItem => CreateDropDownHost(item),
            _ => CreateButtonHost(item),
        };
    }

    /// <summary>
    /// Creates the default clickable host for non-drop-down items.
    /// </summary>
    /// <param name="item">The item that needs a clickable host.</param>
    /// <returns>The control used to invoke the item.</returns>
    protected virtual Control CreateButtonHost(ToolStripItem item)
    {
        var button = new ItemButtonHost();
        button.Click += (_, _) => item.PerformClick();
        return button;
    }

    /// <summary>
    /// Creates the default clickable image host for non-drop-down items.
    /// </summary>
    /// <param name="item">The image item that needs a host.</param>
    /// <returns>The control used to invoke the item.</returns>
    protected virtual Control CreateImageButtonHost(ToolStripItem item)
    {
        if (!OperatingSystem.IsWindows())
        {
            return CreateButtonHost(item);
        }

        var pictureBox = new ItemPictureHost
        {
            SizeMode = PictureBoxSizeMode.CenterImage,
            TabStop = false,
        };
        pictureBox.Click += (_, _) => item.PerformClick();
        return pictureBox;
    }

    /// <summary>
    /// Creates the default clickable host for a top-level drop-down item.
    /// </summary>
    /// <param name="item">The drop-down item that needs a host.</param>
    /// <returns>The control used to open the drop-down.</returns>
    protected virtual Control CreateDropDownHost(ToolStripItem item)
    {
        var button = new ItemButtonHost();
        button.Click += (_, _) => ShowDropDownWithSiblingNavigation((ToolStripDropDownItem)item, button);
        return button;
    }

    /// <summary>
    /// Creates the default clickable image host for a top-level drop-down item.
    /// </summary>
    /// <param name="item">The image drop-down item that needs a host.</param>
    /// <returns>The control used to open the drop-down.</returns>
    protected virtual Control CreateImageDropDownHost(ToolStripItem item)
    {
        if (!OperatingSystem.IsWindows())
        {
            return CreateDropDownHost(item);
        }

        var pictureBox = new ItemPictureHost
        {
            SizeMode = PictureBoxSizeMode.CenterImage,
            TabStop = false,
        };
        pictureBox.Click += (_, _) => ShowDropDownWithSiblingNavigation((ToolStripDropDownItem)item, pictureBox);
        return pictureBox;
    }

    private protected virtual void ApplyItemState(Control host, ToolStripItem item)
    {
        host.Enabled = item.Enabled;

        switch (host)
        {
            case Label label:
                label.Text = item.Text;
                break;

            case ProgressBar progressBar when item is ToolStripProgressBar stripProgressBar:
                progressBar.Value = stripProgressBar.Value;
                break;

            case ComboBox comboBox when item is ToolStripComboBox stripComboBox:
                comboBox.Items.Clear();
                comboBox.Items.AddRange([.. stripComboBox.Items]);
                if (stripComboBox.SelectedIndex >= 0 && stripComboBox.SelectedIndex < stripComboBox.Items.Count)
                {
                    comboBox.SelectedIndex = stripComboBox.SelectedIndex;
                }
                break;

            case TextBox textBox:
                textBox.Text = item.Text;
                break;

            case PictureBox pictureBox when OperatingSystem.IsWindows():
                pictureBox.Image = item.Image;
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                break;

            case Button button:
                button.Text = ResolveItemText(item);
                break;
        }
    }

    private protected virtual Size ResolveHostSize(ToolStripItem item, int availableHeight)
    {
        if (item.Size.Width > 0 && item.Size.Height > 0)
        {
            return item.Size;
        }

        Size textSize = MeasureItemText(item);
        return item switch
        {
            ToolStripLabel => new Size(Math.Max(0, textSize.Width), Math.Min(availableHeight, 22)),
            ToolStripStatusLabel => new Size(Math.Max(0, textSize.Width), Math.Min(availableHeight, 17)),
            ToolStripProgressBar => new Size(112, Math.Min(availableHeight, 18)),
            ToolStripComboBox => new Size(136, Math.Min(availableHeight, 25)),
            ToolStripTextBox => new Size(120, Math.Min(availableHeight, 25)),
            ToolStripSeparator => new Size(10, availableHeight),
            _ when OperatingSystem.IsWindowsVersionAtLeast(6, 1)
                && item.DisplayStyle == ToolStripItemDisplayStyle.Image
                && item.Image is not null
                => new Size(
                    Math.Max(23, item.Image.Width + 8),
                    Math.Max(23, Math.Min(availableHeight, item.Image.Height + 8))),
            _ => new Size(
                Math.Max(23, textSize.Width + 4),
                Math.Max(22, Math.Min(availableHeight, 25))),
        };
    }

    private protected Size MeasureItemText(ToolStripItem item)
        => MeasureTextCore(ResolveItemText(item), useMnemonicPrefix: true);

    private protected Size MeasureTextCore(string text, bool useMnemonicPrefix)
    {
        if (string.IsNullOrEmpty(text))
        {
            return Size.Empty;
        }

        nint screenDc = Win32.GetDC(0);
        if (screenDc == 0)
        {
            return Size.Empty;
        }

        nint previousFont = 0;
        try
        {
            nint fontHandle = NativeFontHandle;
            if (fontHandle != 0)
            {
                previousFont = Win32.SelectObject(screenDc, fontHandle);
            }

            var textBounds = new Win32.RECT();
            uint flags = Win32.DT_SINGLELINE | Win32.DT_CALCRECT | (useMnemonicPrefix ? Win32.DT_HIDEPREFIX : Win32.DT_NOPREFIX);
            _ = Win32.DrawTextW(screenDc, text, text.Length, ref textBounds, flags);

            int width = Math.Max(0, textBounds.Width);
            int height = Math.Max(0, textBounds.Height);

            if ((width == 0 || height == 0) && Win32.GetTextExtentPoint32W(screenDc, text, text.Length, out Win32.SIZE textSize))
            {
                width = Math.Max(width, textSize.cx);
                height = Math.Max(height, textSize.cy);
            }

            return new Size(width, height);
        }
        finally
        {
            if (previousFont != 0)
            {
                _ = Win32.SelectObject(screenDc, previousFont);
            }

            _ = Win32.ReleaseDC(0, screenDc);
        }
    }

    /// <summary>
    /// Resolves the display text used by host controls when an item does not provide explicit content.
    /// </summary>
    /// <param name="item">The item whose text should be resolved.</param>
    /// <returns>The text that should appear on the host control.</returns>
    protected internal static string ResolveItemText(ToolStripItem item)
    {
        if (item.DisplayStyle == ToolStripItemDisplayStyle.Image && !string.IsNullOrWhiteSpace(item.Text))
        {
            return item.Text;
        }

        if (!string.IsNullOrWhiteSpace(item.Text))
        {
            return item.Text;
        }

        return string.IsNullOrWhiteSpace(item.Name)
            ? item.GetType().Name
            : item.Name;
    }

    private void ActivateItem(ToolStripItem item)
    {
        if (item is ToolStripDropDownItem dropDownItem && dropDownItem.DropDownItems.Count > 0)
        {
            if (_itemHosts.TryGetValue(item, out Control? host))
            {
                ShowDropDownWithSiblingNavigation(dropDownItem, host);
                return;
            }
        }

        item.PerformClick();
    }

    private protected virtual void OnDropDownStateChanged(ToolStripDropDownItem item, Control host, bool isOpen)
    {
    }

    /// <summary>
    /// Shows the drop-down for a top-level item and handles Left/Right sibling navigation
    /// by looping until the user either selects a command or presses Escape.
    /// </summary>
    protected void ShowDropDownWithSiblingNavigation(ToolStripDropDownItem initialItem, Control initialHost)
    {
        // Build ordered list of navigable drop-down items on this strip.
        List<ToolStripDropDownItem> navigable = [];
        List<Rectangle> navigableBounds = [];
        foreach (ToolStripItem it in Items)
        {
            if (it is ToolStripDropDownItem ddi && ddi.Visible && ddi.Enabled && ddi.DropDownItems.Count > 0)
            {
                navigable.Add(ddi);
                Control host = _itemHosts.TryGetValue(ddi, out Control? navigableHost) && navigableHost is not null
                    ? navigableHost
                    : initialHost;
                navigableBounds.Add(GetScreenBounds(host));
            }
        }

        int count = navigable.Count;
        int currentIndex = navigable.IndexOf(initialItem);

        if (currentIndex < 0 || count == 0 || !OperatingSystem.IsWindows())
        {
            ShowDropDown(initialItem, initialHost);
            return;
        }

        int armedIndex = -1;
        while (true)
        {
            ToolStripDropDownItem item = navigable[currentIndex];
            Control host = _itemHosts.TryGetValue(item, out Control? h) ? h : initialHost;
            Point screenLocation = GetDropDownScreenLocation(host);
            nint ownerHandle = Owner?.NativeHandle ?? NativeHandle;

            int nextIndex;
            bool keepCurrentArmed = armedIndex == currentIndex;
            if (!keepCurrentArmed)
            {
                OnDropDownStateChanged(item, host, isOpen: true);
            }

            nextIndex = ToolStripPopupMenu.ShowForMenuBar(
                item.DropDownItems,
                ownerHandle,
                screenLocation,
                Owner?.CurrentVisualStyle ?? Application.CurrentVisualStyle,
                currentIndex,
                [.. navigableBounds]);

            if (nextIndex < 0)
            {
                OnDropDownStateChanged(item, host, isOpen: false);
                break;
            }

            if (nextIndex != currentIndex)
            {
                ToolStripDropDownItem nextItem = navigable[nextIndex];
                Control nextHost = _itemHosts.TryGetValue(nextItem, out Control? nextHostControl) ? nextHostControl : initialHost;
                OnDropDownStateChanged(nextItem, nextHost, isOpen: true);
                OnDropDownStateChanged(item, host, isOpen: false);
            }

            armedIndex = nextIndex;
            currentIndex = nextIndex;
        }
    }

    private void ShowDropDown(ToolStripDropDownItem item, Control host)
    {
        if (item.DropDownItems.Count == 0)
        {
            item.PerformClick();
            return;
        }

        Point screenLocation = GetDropDownScreenLocation(host);
        OnDropDownStateChanged(item, host, isOpen: true);
        try
        {
            ToolStripPopupMenu.Show(item.DropDownItems, Owner?.NativeHandle ?? NativeHandle, screenLocation, Owner?.CurrentVisualStyle ?? Application.CurrentVisualStyle);
        }
        finally
        {
            OnDropDownStateChanged(item, host, isOpen: false);
        }
    }

    private static Point GetDropDownScreenLocation(Control host)
    {
        if (host.NativeHandle != 0 && Win32.GetWindowRect(host.NativeHandle, out var rect))
        {
            return new Point(rect.Left, Math.Max(rect.Top, rect.Bottom - 1));
        }

        return new Point(host.Left, host.Bottom);
    }

    private static Rectangle GetScreenBounds(Control host)
    {
        if (host.NativeHandle != 0 && Win32.GetWindowRect(host.NativeHandle, out var rect))
        {
            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        return new Rectangle(host.Left, host.Top, host.Width, host.Height);
    }

    private void RemoveItemHost(Control host)
    {
        _ = RemoveChild(host);
        host.Dispose();
    }

    private sealed class ItemButtonHost : Button
    {
        private protected override ThemeColorSlot DefaultBackgroundSlot => ThemeColorSlot.Surface;

        private protected override ThemeColorSlot DefaultForegroundSlot => ThemeColorSlot.Surface;

        protected override bool UseParentBackgroundForTheming => true;

        protected override int GetNativeHeight(int requestedHeight)
            => requestedHeight;
    }

    private sealed class ItemPictureHost : PictureBox
    {
        private protected override ThemeColorSlot DefaultBackgroundSlot => ThemeColorSlot.Surface;

        private protected override ThemeColorSlot DefaultForegroundSlot => ThemeColorSlot.Surface;

        protected override bool UseParentBackgroundForTheming => true;

        protected override int GetNativeHeight(int requestedHeight)
            => requestedHeight;
    }
}
