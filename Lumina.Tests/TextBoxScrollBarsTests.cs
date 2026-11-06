using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxScrollBarsTests
{
    private const int GwlStyle = -16;
    private const int ScrollStyleMask = 0x003000C4;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmGetPasswordChar = 0x00D2;

    [Fact]
    public void ScrollBarsDefaultsAndDesignerMetadataMatchTheStandardProperty()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox();
            Assert.Equal(ScrollBars.None, textBox.ScrollBars);
            PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBox.ScrollBars))!;
            Assert.Equal(ScrollBars.None, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
            textBox.ScrollBars = ScrollBars.None;
            textBox.ScrollBars = ScrollBars.Both;
            Assert.Equal(ScrollBars.Both, textBox.ScrollBars);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void InvalidScrollBarsValuesPreservePendingStateAndDoNotCreateAHandle(int value)
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { ScrollBars = ScrollBars.Vertical };
            InvalidEnumArgumentException error = Assert.Throws<InvalidEnumArgumentException>(
                () => textBox.ScrollBars = (ScrollBars)value);
            Assert.Equal("value", error.ParamName);
            Assert.Equal(ScrollBars.Vertical, textBox.ScrollBars);
            Assert.False(textBox.IsHandleCreated);
        });

    // Expected bits come from the fixed v10.0.11 TextBoxBase/CreateParams and
    // TextBox/CreateParams contracts. The independent EDIT also captures any
    // normalization USER32 applies when creating an actual edit window.
    [Theory]
    [InlineData(false, false, ScrollBars.None, 0x000000C0)]
    [InlineData(false, false, ScrollBars.Horizontal, 0x000000C0)]
    [InlineData(false, false, ScrollBars.Vertical, 0x000000C0)]
    [InlineData(false, false, ScrollBars.Both, 0x000000C0)]
    [InlineData(false, true, ScrollBars.None, 0x000000C0)]
    [InlineData(false, true, ScrollBars.Horizontal, 0x000000C0)]
    [InlineData(false, true, ScrollBars.Vertical, 0x000000C0)]
    [InlineData(false, true, ScrollBars.Both, 0x000000C0)]
    [InlineData(true, false, ScrollBars.None, 0x000000C4)]
    [InlineData(true, false, ScrollBars.Horizontal, 0x001000C4)]
    [InlineData(true, false, ScrollBars.Vertical, 0x002000C4)]
    [InlineData(true, false, ScrollBars.Both, 0x003000C4)]
    [InlineData(true, true, ScrollBars.None, 0x00000044)]
    [InlineData(true, true, ScrollBars.Horizontal, 0x00000044)]
    [InlineData(true, true, ScrollBars.Vertical, 0x00200044)]
    [InlineData(true, true, ScrollBars.Both, 0x00200044)]
    public void ScrollBarsAndWordWrapApplyTheFixedStylesToARealEdit(
        bool multiline, bool wordWrap, ScrollBars scrollBars, int expectedStyle)
        => OnStaThread(() =>
        {
            string text = new string('w', 250) + "\r\nsecond\r\nthird\r\nfourth";
            using var textBox = new StyleTextBox
            {
                Multiline = multiline,
                WordWrap = wordWrap,
                ScrollBars = scrollBars,
                Text = text,
                Width = 160,
                Height = 56
            };
            Assert.Equal(expectedStyle, textBox.RequestedStyle & ScrollStyleMask);
            Assert.False(textBox.IsHandleCreated);

            nint nativeEdit = CreateWindowExW(0x00000200, "EDIT", text,
                0x80010000u | (uint)expectedStyle, 0, 0, 160, 56, 0, 0, 0, 0);
            Assert.NotEqual(0, nativeEdit);
            try
            {
                Assert.True(IsWindow(textBox.Handle));
                Assert.Equal(GetWindowLongW(nativeEdit, GwlStyle) & ScrollStyleMask,
                    GetWindowLongW(textBox.Handle, GwlStyle) & ScrollStyleMask);
                Assert.Equal(scrollBars, textBox.ScrollBars);
            }
            finally
            {
                Assert.True(DestroyWindow(nativeEdit));
            }
        });

    [Fact]
    public void ChangingScrollBarsRecreatesOnceAndPreservesEditingState()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { Multiline = true, WordWrap = false, ReadOnly = true, Text = "alpha beta" };
            nint original = textBox.Handle;
            textBox.Select(2, 5);
            _ = SendMessageW(original, EmSetModify, 1, 0);
            var notifications = new List<string>();
            int textChanges = 0;
            int selectionChanges = 0;
            textBox.HandleDestroyed += (_, _) => notifications.Add("destroyed");
            textBox.HandleCreated += (_, _) =>
            {
                Assert.Equal(ScrollBars.Both, textBox.ScrollBars);
                notifications.Add("created");
            };
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;

            textBox.ScrollBars = ScrollBars.Both;

            Assert.Equal(new[] { "destroyed", "created" }, notifications);
            Assert.False(IsWindow(original));
            Assert.Equal("alpha beta", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(5, textBox.SelectionLength);
            Assert.NotEqual(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
            Assert.NotEqual(0, GetWindowLongW(textBox.Handle, GwlStyle) & 0x0800);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, selectionChanges);

            nint recreated = textBox.Handle;
            textBox.ScrollBars = ScrollBars.Both;
            Assert.Equal(recreated, textBox.Handle);
            Assert.Equal(2, notifications.Count);
            Assert.Equal("value", Assert.Throws<InvalidEnumArgumentException>(
                () => textBox.ScrollBars = (ScrollBars)4).ParamName);
            Assert.Equal(recreated, textBox.Handle);
            Assert.Equal(ScrollBars.Both, textBox.ScrollBars);
            Assert.Equal(2, notifications.Count);
        });

    [Fact]
    public void ScrollBarsRecreationAlsoAppliesToSingleLineAndKeepsSystemPasswordMode()
        => OnStaThread(() =>
        {
            using var textBox = new TextBox { UseSystemPasswordChar = true, Text = "secret" };
            nint original = textBox.Handle;
            nint password = SendMessageW(original, EmGetPasswordChar, 0, 0);
            Assert.NotEqual(0, password);
            textBox.Select(1, 3);
            int destroyed = 0;
            int created = 0;
            textBox.HandleDestroyed += (_, _) => destroyed++;
            textBox.HandleCreated += (_, _) => created++;

            textBox.ScrollBars = ScrollBars.Horizontal;

            Assert.Equal(1, destroyed);
            Assert.Equal(1, created);
            Assert.False(IsWindow(original));
            Assert.Equal(0, GetWindowLongW(textBox.Handle, GwlStyle) & 0x00300000);
            Assert.Equal(password, SendMessageW(textBox.Handle, EmGetPasswordChar, 0, 0));
            Assert.Equal("secret", textBox.Text);
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
        });

    [Fact]
    public void WordWrapSwitchReappliesRequestedHorizontalScrollBarsWithoutChangingTheProperty()
        => OnStaThread(() =>
        {
            using var textBox = new StyleTextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                Text = new string('w', 250) + "\r\nsecond\r\nthird\r\nfourth",
                Width = 160,
                Height = 56
            };
            _ = textBox.Handle;
            Assert.Equal(0x00200044, textBox.RequestedStyle & ScrollStyleMask);

            textBox.WordWrap = false;

            Assert.Equal(ScrollBars.Both, textBox.ScrollBars);
            Assert.Equal(0x003000C4, textBox.RequestedStyle & ScrollStyleMask);
            Assert.Equal(0x003000C4, GetWindowLongW(textBox.Handle, GwlStyle) & ScrollStyleMask);

            textBox.WordWrap = true;

            Assert.Equal(ScrollBars.Both, textBox.ScrollBars);
            Assert.Equal(0x00200044, textBox.RequestedStyle & ScrollStyleMask);
            Assert.Equal(0x00200044, GetWindowLongW(textBox.Handle, GwlStyle) & ScrollStyleMask);
        });

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox scrollbar lifecycle timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class StyleTextBox : TextBox
    {
        internal int RequestedStyle => CreateParams.Style;
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string? windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint param);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);
}
