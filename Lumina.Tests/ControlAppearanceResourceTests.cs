using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class ControlAppearanceResourceTests
{
    [Fact]
    public void TextBox_UsesTheInheritedBorrowedFontForBothTypedViews()
        => OnStaThread(() =>
        {
            using var parentFont = new Font("Segoe UI", 12f);
            using var localFont = new Font("Segoe UI", 18f, FontStyle.Bold);
            using var form = new Form { Font = parentFont };
            var textBox = new TextBox();
            form.Controls.Add(textBox);

            Assert.Same(parentFont, textBox.Font);
            Assert.Same(parentFont, ((Control)textBox).Font);
            textBox.Font = localFont;
            Assert.Same(localFont, textBox.Font);
            Assert.Same(localFont, ((Control)textBox).Font);
            textBox.ResetFont();
            Assert.Same(parentFont, textBox.Font);
            textBox.Font = localFont;
            form.Dispose();

            AssertUsableFont(parentFont);
            AssertUsableFont(localFont);
        });

    [Fact]
    public void TextBox_ColorDefaultsDoNotInheritParentValuesOrBecomeLocalOnReadOnly()
        => OnStaThread(() =>
        {
            using var form = new Form { BackColor = Color.Red, ForeColor = Color.Blue };
            var textBox = new TextBox();
            form.Controls.Add(textBox);
            int changes = 0;
            textBox.BackColorChanged += (_, _) => changes++;

            Assert.Equal(SystemColors.Window, textBox.BackColor);
            Assert.Equal(SystemColors.WindowText, textBox.ForeColor);
            textBox.ReadOnly = true;
            Assert.Equal(SystemColors.Control, textBox.BackColor);
            Assert.Equal(0, changes);
            textBox.BackColor = Color.Gold;
            textBox.ForeColor = Color.Maroon;
            textBox.ReadOnly = false;
            Assert.Equal(Color.Gold, textBox.BackColor);
            Assert.Equal(Color.Maroon, textBox.ForeColor);
            textBox.ResetBackColor();
            textBox.ResetForeColor();
            Assert.Equal(SystemColors.Window, textBox.BackColor);
            Assert.Equal(SystemColors.WindowText, textBox.ForeColor);
            Assert.False(HasLocalColor(textBox, nameof(Control.BackColor)));
            Assert.False(HasLocalColor(textBox, nameof(Control.ForeColor)));
        });

    [Fact]
    public void ThemeDefaultsKeepWindowSurfaceAndControlSlotsAfterEffectiveColorGetters()
        => OnStaThread(() =>
        {
            using var form = new Form();
            ThemePalette palette = CreatePalette();
            form.SetPalette(palette);
            var group = new GroupBox();
            var textBox = new TextBox();
            var tabControl = new TabControl();
            form.Controls.AddRange(group, textBox, tabControl);

            Assert.Equal(SystemColors.Control, form.BackColor);
            Assert.Equal(SystemColors.ControlText, form.ForeColor);
            Assert.Equal(ToColorRef(palette.WindowBackground), GetFormThemeColor(form, "GetThemeBackgroundColorRef"));
            Assert.Equal(ToColorRef(palette.WindowForeground), GetFormThemeColor(form, "GetThemeForegroundColorRef"));
            AssertThemeColors(group, palette.WindowBackground, palette.SurfaceForeground);
            AssertThemeColors(textBox, palette.ControlBackground, palette.ControlForeground);
            AssertThemeColors(tabControl, palette.SurfaceBackground, palette.SurfaceForeground);
            textBox.ReadOnly = true;
            AssertThemeColors(textBox, palette.SurfaceBackground, palette.ControlForeground);
            Assert.False(HasLocalColor(form, nameof(Control.BackColor)));
            Assert.False(HasLocalColor(tabControl, nameof(Control.BackColor)));
        });

    [Fact]
    public void ExplicitDefaultFormColorsRefreshPaletteBrushesWithoutPropertyEvents()
        => OnStaThread(() =>
        {
            using var form = new Form();
            ThemePalette palette = CreatePalette();
            form.SetPalette(palette);
            var group = new GroupBox();
            form.Controls.Add(group);
            int changes = 0;
            form.BackColorChanged += (_, _) => changes++;
            form.ForeColorChanged += (_, _) => changes++;

            form.BackColor = SystemColors.Control;
            form.ForeColor = SystemColors.ControlText;

            Assert.Equal(0, changes);
            Assert.Equal(ToColorRef(unchecked((uint)SystemColors.Control.ToArgb())), GetFormBrushColor(form));
            AssertThemeColors(group, unchecked((uint)SystemColors.Control.ToArgb()), unchecked((uint)SystemColors.ControlText.ToArgb()));
            form.ResetBackColor();
            form.ResetForeColor();
            Assert.Equal(0, changes);
            Assert.Equal(ToColorRef(palette.WindowBackground), GetFormBrushColor(form));
            AssertThemeColors(group, palette.WindowBackground, palette.SurfaceForeground);
        });

    [Fact]
    public void ThemeColorsRespectExplicitAncestorValuesAndTextBoxDefaultBoundary()
        => OnStaThread(() =>
        {
            using var form = new Form();
            ThemePalette palette = CreatePalette();
            form.SetPalette(palette);
            var panel = new Panel { BackColor = Color.Navy, ForeColor = Color.Yellow };
            var group = new GroupBox();
            var textBox = new TextBox();
            form.Controls.Add(panel);
            panel.Controls.AddRange(group, textBox);

            AssertThemeColors(group, unchecked((uint)Color.Navy.ToArgb()), unchecked((uint)Color.Yellow.ToArgb()));
            AssertThemeColors(textBox, palette.ControlBackground, palette.ControlForeground);
            textBox.BackColor = Color.Teal;
            textBox.ForeColor = Color.White;
            AssertThemeColors(textBox, unchecked((uint)Color.Teal.ToArgb()), unchecked((uint)Color.White.ToArgb()));
            textBox.ResetBackColor();
            textBox.ResetForeColor();
            AssertThemeColors(textBox, palette.ControlBackground, palette.ControlForeground);
            panel.BackColor = Color.Olive;
            panel.ForeColor = Color.Pink;
            AssertThemeColors(group, unchecked((uint)Color.Olive.ToArgb()), unchecked((uint)Color.Pink.ToArgb()));
        });

    [Fact]
    public void TextBox_ThemeConsumesDerivedColorGettersWithAndWithoutLocalValues()
        => OnStaThread(() =>
        {
            using var form = new Form();
            form.SetPalette(CreatePalette());
            var textBox = new GetterTextBox();
            form.Controls.Add(textBox);

            AssertThemeColors(textBox, unchecked((uint)Color.Coral.ToArgb()), unchecked((uint)Color.Indigo.ToArgb()));
            textBox.BackColor = Color.Green;
            textBox.ForeColor = Color.Blue;
            AssertThemeColors(textBox, unchecked((uint)Color.Coral.ToArgb()), unchecked((uint)Color.Indigo.ToArgb()));
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void TabControl_ThemeChangesPreserveItsOwnAndPageLocalColors()
        => OnStaThread(() =>
        {
            using var form = new Form();
            var tabs = new TabControl { BackColor = Color.Navy, ForeColor = Color.Yellow };
            var page = new TabPage { BackColor = Color.Olive, ForeColor = Color.Pink, UseVisualStyleBackColor = true };
            tabs.Controls.Add(page);
            form.Controls.Add(tabs);
            form.Show();

            form.UseDarkTheme();
            form.SetPalette(CreatePalette());

            Assert.Equal(Color.Navy, tabs.BackColor);
            Assert.Equal(Color.Yellow, tabs.ForeColor);
            Assert.Equal(Color.Olive, page.BackColor);
            Assert.Equal(Color.Pink, page.ForeColor);
            tabs.ResetBackColor();
            tabs.ResetForeColor();
            page.ResetBackColor();
            page.ResetForeColor();
            form.UseLightTheme();
            Assert.False(HasLocalColor(tabs, nameof(Control.BackColor)));
            Assert.False(HasLocalColor(tabs, nameof(Control.ForeColor)));
            Assert.False(HasLocalColor(page, nameof(Control.BackColor)));
            Assert.False(HasLocalColor(page, nameof(Control.ForeColor)));
        });

    [Fact]
    public void GroupBox_DisplayRectangleTracksTheEffectiveFont()
        => OnStaThread(() =>
        {
            using var smallFont = new Font("Segoe UI", 9f);
            using var largeFont = new Font("Segoe UI", 24f);
            using var form = new Form { Font = smallFont };
            var group = new GroupBox { Size = new Size(250, 160), Padding = new Padding(4) };
            form.Controls.Add(group);

            Assert.Equal(smallFont.Height + 4, group.DisplayRectangle.Top);
            form.Font = largeFont;
            Assert.Equal(largeFont.Height + 4, group.DisplayRectangle.Top);
            group.Font = smallFont;
            Assert.Equal(smallFont.Height + 4, group.DisplayRectangle.Top);
            Assert.False(group.IsHandleCreated);
        });

    [Fact]
    public void MenuStrip_MeasuresWithItsEffectiveFontAndReleasesFontWithoutAWindow()
        => OnStaThread(() =>
        {
            using var smallFont = new Font("Segoe UI", 9f);
            using var largeFont = new Font("Segoe UI", 24f);
            using var menu = new MenuStrip { Font = smallFont };
            menu.Items.Add(new ToolStripMenuItem { Text = "WWWWWW" });
            menu.PerformLayout();
            Control host = Assert.Single(menu.Controls.Cast<Control>());
            int smallWidth = host.Width;
            int layouts = 0;
            menu.Layout += (_, _) => layouts++;
            menu.Font = largeFont;
            Assert.Same(host, Assert.Single(menu.Controls.Cast<Control>()));
            Assert.True(host.Width > smallWidth,
                $"The menu item did not remeasure its larger inherited font: {smallWidth} -> {host.Width}; " +
                $"measured {MeasureStripText(menu, "WWWWWW")}; layouts {layouts}; native {ReadFont(GetNativeFontHandle(menu))}.");
            Assert.Same(largeFont, host.Font);
            Assert.False(menu.IsHandleCreated);
            Assert.False(host.IsHandleCreated);
            nint fontHandle = GetNativeFontHandle(menu);
            Assert.Equal(6u, GetObjectType(fontHandle));
            menu.Dispose();
            AssertReleasedNativeFont(menu, fontHandle);
            AssertUsableFont(largeFont);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NativeFontsFollowInheritanceAndLocalResetWithoutBorrowingParentHandles()
        => OnStaThread(() =>
        {
            using var parentFont = new Font("Segoe UI", 13f, FontStyle.Italic);
            using var localFont = new Font("Segoe UI", 19f, FontStyle.Bold);
            using var form = new Form { Font = parentFont };
            var group = new GroupBox();
            var textBox = new TextBox();
            form.Controls.Add(group);
            group.Controls.Add(textBox);
            form.Show();

            // A custom top-level window does not keep WM_SETFONT state in
            // DefWindowProc. The form owns a font for its managed consumers.
            Assert.Equal(0, GetWindowFont(form));
            AssertOwnedFontMatches(form, parentFont);
            AssertNativeFontMatches(group, parentFont);
            AssertNativeFontMatches(textBox, parentFont);
            Assert.NotEqual(GetNativeFontHandle(form), GetWindowFont(group));
            Assert.NotEqual(GetWindowFont(group), GetWindowFont(textBox));
            textBox.Font = localFont;
            AssertNativeFontMatches(textBox, localFont);
            textBox.ResetFont();
            AssertNativeFontMatches(textBox, parentFont);
            nint textFont = GetWindowFont(textBox);
            Assert.Equal(textFont, GetStoredNativeFontHandle(textBox));
            nint textWindow = textBox.Handle;
            form.Close();
            Assert.True(form.IsDisposed);
            Assert.False(IsWindow(textWindow));
            Assert.True(group.IsDisposed);
            AssertReleasedNativeFont(textBox, textFont);
            AssertUsableFont(parentFont);
            AssertUsableFont(localFont);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FailedFormRecreationKeepsParkedChildFontAliveUntilChildDisposal()
        => OnStaThread(() =>
        {
            using var firstFont = new Font("Segoe UI", 12f);
            using var nextFont = new Font("Segoe UI", 17f, FontStyle.Bold);
            using var form = new FailingRecreationForm { Font = firstFont };
            var textBox = new TextBox();
            form.Controls.Add(textBox);
            form.Show();
            nint childWindow = textBox.Handle;
            nint childFont = GetWindowFont(textBox);
            form.FailNextCreation = true;

            Assert.Throws<InvalidOperationException>(() => form.RebuildHandle());

            Assert.False(form.IsHandleCreated);
            Assert.True(IsWindow(childWindow));
            Assert.Equal(childFont, GetWindowFont(textBox));
            Assert.Equal(6u, GetObjectType(childFont));
            AssertNativeFontMatches(textBox, firstFont);
            form.Font = nextFont;
            AssertNativeFontMatches(textBox, nextFont);
            nint replacementFont = GetWindowFont(textBox);
            form.Dispose();
            Assert.False(IsWindow(childWindow));
            AssertReleasedNativeFont(textBox, replacementFont);
            AssertUsableFont(firstFont);
            AssertUsableFont(nextFont);
        });

    private static ThemePalette CreatePalette() => new()
    {
        WindowBackground = 0xFF123456,
        WindowForeground = 0xFFABCDEF,
        SurfaceBackground = 0xFF234567,
        SurfaceForeground = 0xFFBCDEFA,
        ControlBackground = 0xFF345678,
        ControlForeground = 0xFFCDEFAB,
    };

    private static bool HasLocalColor(Control control, string propertyName)
        => (bool)typeof(Control).GetMethod("ShouldSerialize" + propertyName, BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(control, null)!;

    private static nint GetNativeFontHandle(Control control)
        => (nint)typeof(Control).GetProperty("NativeFontHandle", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(control)!;

    private static nint GetStoredNativeFontHandle(Control control)
        => (nint)typeof(Control).GetField("_nativeFontHandle", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(control)!;

    private static Size MeasureStripText(ToolStrip strip, string text)
        => (Size)typeof(ToolStrip).GetMethod("MeasureTextCore", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(strip, [text, true])!;

    private static uint GetFormThemeColor(Form form, string methodName)
    {
        Type slotType = typeof(Form).Assembly.GetType("Lumina.Forms.ThemeColorSlot", true)!;
        return (uint)typeof(Form).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)!
            .Invoke(form, [Enum.ToObject(slotType, 0)])!;
    }

    private static uint GetFormBrushColor(Form form)
    {
        Type slotType = typeof(Form).Assembly.GetType("Lumina.Forms.ThemeColorSlot", true)!;
        nint handle = (nint)typeof(Form).GetMethod("GetThemeBrush", BindingFlags.Instance | BindingFlags.NonPublic)!
            .Invoke(form, [Enum.ToObject(slotType, 0)])!;
        Assert.Equal(Marshal.SizeOf<LogBrush>(), GetObjectW(handle, Marshal.SizeOf<LogBrush>(), out LogBrush brush));
        return brush.Color;
    }

    private static void AssertThemeColors(Control control, uint background, uint foreground)
    {
        object?[] arguments = [(nint)0, 0u, 0u, false];
        Assert.True((bool)typeof(Control).GetMethod("TryGetThemeColors", BindingFlags.Instance | BindingFlags.NonPublic)!
            .Invoke(control, arguments)!);
        Assert.Equal(ToColorRef(background), (uint)arguments[1]!);
        Assert.Equal(ToColorRef(foreground), (uint)arguments[2]!);
        Assert.Equal(Marshal.SizeOf<LogBrush>(), GetObjectW((nint)arguments[0]!, Marshal.SizeOf<LogBrush>(), out LogBrush brush));
        Assert.Equal(ToColorRef(background), brush.Color);
    }

    private static uint ToColorRef(uint argb)
        => ((argb >> 16) & 255) | (argb & 0xFF00) | ((argb & 255) << 16);

    private static nint GetWindowFont(Control control) => SendMessageW(control.Handle, 0x0031, 0, 0);

    private static void AssertNativeFontMatches(Control control, Font font)
    {
        nint expectedHandle = font.ToHfont();
        try
        {
            Assert.Equal(ReadFont(expectedHandle), ReadFont(GetWindowFont(control)));
        }
        finally { Assert.True(DeleteObject(expectedHandle)); }
    }

    private static void AssertOwnedFontMatches(Control control, Font font)
    {
        nint expectedHandle = font.ToHfont();
        try { Assert.Equal(ReadFont(expectedHandle), ReadFont(GetNativeFontHandle(control))); }
        finally { Assert.True(DeleteObject(expectedHandle)); }
    }

    private static string ReadFont(nint handle)
    {
        Assert.NotEqual(0, handle);
        Assert.Equal(Marshal.SizeOf<LogFont>(), GetObjectW(handle, Marshal.SizeOf<LogFont>(), out LogFont font));
        return $"{font.Height}/{font.Width}/{font.Escapement}/{font.Orientation}/{font.Weight}/{font.Italic}/{font.Underline}/{font.StrikeOut}/{font.CharSet}/{font.OutPrecision}/{font.ClipPrecision}/{font.Quality}/{font.PitchAndFamily}/{font.FaceName}";
    }

    private static string DescribeNativeFont(nint handle)
    {
        int bytes = GetObjectW(handle, Marshal.SizeOf<LogFont>(), out LogFont font);
        return $"type={GetObjectType(handle)}, bytes={bytes}, height={font.Height}, weight={font.Weight}, face={font.FaceName}";
    }

    private static void AssertReleasedNativeFont(Control control, nint handle)
    {
        Assert.True(control.IsDisposed);
        Assert.False(control.IsHandleCreated);
        Assert.Equal(0, GetStoredNativeFontHandle(control));
        // GetObjectType can retain a stale type after deletion. Require the
        // native object itself to be unreadable, alongside managed teardown.
        Assert.True(GetObjectW(handle, Marshal.SizeOf<LogFont>(), out LogFont _) == 0,
            $"HFONT {handle} remains readable after disposal: {DescribeNativeFont(handle)}.");
    }

    private static void AssertUsableFont(Font font)
    {
        nint handle = font.ToHfont();
        try { Assert.Equal(6u, GetObjectType(handle)); }
        finally { Assert.True(DeleteObject(handle)); }
    }

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The appearance resource check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class GetterTextBox : TextBox
    {
        public override Color BackColor { get => Color.Coral; set => base.BackColor = value; }
        public override Color ForeColor { get => Color.Indigo; set => base.ForeColor = value; }
    }

    private sealed partial class FailingRecreationForm : Form
    {
        internal bool FailNextCreation { get; set; }
        internal void RebuildHandle() => RecreateHandle();
        protected override uint WindowExStyle
        {
            get
            {
                if (FailNextCreation)
                {
                    FailNextCreation = false;
                    throw new InvalidOperationException("Recreation style getter");
                }
                return base.WindowExStyle;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct LogBrush
    {
        public uint Style;
        public uint Color;
        public nuint Hatch;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    private struct LogFont
    {
        public int Height, Width, Escapement, Orientation, Weight;
        public byte Italic, Underline, StrikeOut, CharSet, OutPrecision, ClipPrecision, Quality, PitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string FaceName;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint window, uint message, nint wParam, nint lParam);
    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint window);
    [DllImport("gdi32.dll", ExactSpelling = true)]
    private static extern uint GetObjectType(nint handle);
    [DllImport("gdi32.dll", ExactSpelling = true)]
    private static extern int GetObjectW(nint handle, int size, out LogFont value);
    [DllImport("gdi32.dll", ExactSpelling = true)]
    private static extern int GetObjectW(nint handle, int size, out LogBrush value);
    [DllImport("gdi32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteObject(nint handle);
}
