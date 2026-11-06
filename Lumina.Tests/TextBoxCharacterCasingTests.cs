using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxCharacterCasingTests
{
    private const int GwlStyle = -16;
    private const int CasingMask = 0x0018;
    private const int WmChar = 0x0102;
    private const int EmSetSel = 0x00B1;
    private const int EmReplaceSel = 0x00C2;
    private const int EmGetModify = 0x00B8;
    private const int EmSetModify = 0x00B9;
    private const int EmGetPasswordChar = 0x00D2;

    [Fact]
    public void StandardDefaultAndMetadataRemainLazy()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            Assert.Equal(0, (int)CharacterCasing.Normal);
            Assert.Equal(1, (int)CharacterCasing.Upper);
            Assert.Equal(2, (int)CharacterCasing.Lower);
            Assert.Equal(CharacterCasing.Normal, textBox.CharacterCasing);
            PropertyInfo property = typeof(TextBox).GetProperty(nameof(TextBox.CharacterCasing))!;
            Assert.Equal(typeof(TextBox), property.DeclaringType);
            Assert.Equal(CharacterCasing.Normal, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
            Assert.False(property.GetMethod!.IsVirtual);
            Assert.False(property.SetMethod!.IsVirtual);
            Assert.Null(typeof(TextBox).GetEvent("CharacterCasingChanged"));
            textBox.CharacterCasing = CharacterCasing.Normal;
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PendingChangesPreserveTextSelectionAndDoNotCreateAHandle()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "Mixed 中国" };
            textBox.Select(2, 3);
            int textChanges = 0;
            int selectionChanges = 0;
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;

            textBox.CharacterCasing = CharacterCasing.Upper;
            textBox.CharacterCasing = CharacterCasing.Upper;
            textBox.CharacterCasing = CharacterCasing.Lower;
            textBox.CharacterCasing = CharacterCasing.Normal;

            Assert.Equal("Mixed 中国", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, selectionChanges);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void InvalidCasingLeavesPendingStateUnchanged(int value)
        => OnSta(() =>
        {
            using var textBox = new TextBox { CharacterCasing = CharacterCasing.Upper, Text = "aBc" };
            textBox.Select(1, 2);

            InvalidEnumArgumentException failure = Assert.Throws<InvalidEnumArgumentException>(
                () => textBox.CharacterCasing = (CharacterCasing)value);

            Assert.Equal("value", failure.ParamName);
            Assert.Equal(CharacterCasing.Upper, textBox.CharacterCasing);
            Assert.Equal("aBc", textBox.Text);
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(2, textBox.SelectionLength);
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, CharacterCasing.Normal, 0)]
    [InlineData(false, CharacterCasing.Upper, 0x0008)]
    [InlineData(false, CharacterCasing.Lower, 0x0010)]
    [InlineData(true, CharacterCasing.Normal, 0)]
    [InlineData(true, CharacterCasing.Upper, 0x0008)]
    [InlineData(true, CharacterCasing.Lower, 0x0010)]
    public void InitialStylesMatchIndependentNativeEdit(bool multiline, CharacterCasing casing, int expectedBits)
        => OnSta(() =>
        {
            using var textBox = new StyleTextBox
            {
                Multiline = multiline,
                CharacterCasing = casing,
                WordWrap = false,
                ScrollBars = ScrollBars.Both
            };
            Assert.Equal(expectedBits, textBox.RequestedStyle & CasingMask);
            Assert.Equal(multiline ? 0x00300000 : 0, textBox.RequestedStyle & 0x00300000);
            Assert.False(textBox.IsHandleCreated);

            nint nativeEdit = CreateNativeEdit(multiline, expectedBits);
            try
            {
                Assert.Equal(expectedBits, GetWindowLongW(nativeEdit, GwlStyle) & CasingMask);
                Assert.Equal(expectedBits, GetWindowLongW(textBox.Handle, GwlStyle) & CasingMask);
            }
            finally { Assert.True(DestroyWindow(nativeEdit)); }
        });

    [Theory]
    [InlineData(false, CharacterCasing.Normal, 0, "AbC中国")]
    [InlineData(false, CharacterCasing.Upper, 0x0008, "ABC中国")]
    [InlineData(false, CharacterCasing.Lower, 0x0010, "abc中国")]
    [InlineData(true, CharacterCasing.Normal, 0, "AbC中国")]
    [InlineData(true, CharacterCasing.Upper, 0x0008, "ABC中国")]
    [InlineData(true, CharacterCasing.Lower, 0x0010, "abc中国")]
    public void DirectCharacterMessagesUseNativeCaseConversion(
        bool multiline, CharacterCasing casing, int expectedBits, string expectedText)
        => OnSta(() =>
        {
            using var textBox = new TextBox { Multiline = multiline, CharacterCasing = casing };
            nint nativeEdit = CreateNativeEdit(multiline, expectedBits);
            try
            {
                SendCharacters(nativeEdit);
                SendCharacters(textBox.Handle);

                Assert.Equal(expectedText, ReadNativeText(nativeEdit));
                Assert.Equal(expectedText, textBox.Text);
                Assert.Equal(ReadNativeText(nativeEdit), ReadNativeText(textBox.Handle));
            }
            finally { Assert.True(DestroyWindow(nativeEdit)); }
        });

    [Theory]
    [InlineData(CharacterCasing.Normal, 0)]
    [InlineData(CharacterCasing.Upper, 0x0008)]
    [InlineData(CharacterCasing.Lower, 0x0010)]
    public void ProgrammaticTextAndReplacementFollowIndependentNativeEdit(CharacterCasing casing, int expectedBits)
        => OnSta(() =>
        {
            using var textBox = new TextBox { CharacterCasing = casing };
            nint handle = textBox.Handle;
            nint nativeEdit = CreateNativeEdit(false, expectedBits);
            try
            {
                Assert.True(SetWindowTextW(nativeEdit, "aBc中国"));
                textBox.Text = "aBc中国";
                Assert.Equal(ReadNativeText(nativeEdit), textBox.Text);
                Assert.Equal(5, textBox.TextLength);

                _ = SendMessageW(nativeEdit, EmSetSel, 1, 2);
                textBox.Select(1, 1);
                _ = SendMessageTextW(nativeEdit, EmReplaceSel, 1, "xY");
                _ = SendMessageTextW(handle, EmReplaceSel, 1, "xY");
                Assert.Equal(ReadNativeText(nativeEdit), textBox.Text);
                Assert.Equal(6, textBox.TextLength);
                Assert.Equal(expectedBits, GetWindowLongW(handle, GwlStyle) & CasingMask);
            }
            finally { Assert.True(DestroyWindow(nativeEdit)); }
        });

    [Fact]
    public void CasingRecreatesOnceAndPreservesEditingStateAndOtherStyles()
        => OnSta(() =>
        {
            using var textBox = new TextBox
            {
                Multiline = true,
                WordWrap = false,
                ScrollBars = ScrollBars.Both,
                TextAlign = HorizontalAlignment.Right,
                ReadOnly = true,
                HideSelection = false,
                Text = "12345 中国"
            };
            nint original = textBox.Handle;
            textBox.Select(2, 3);
            _ = SendMessageW(original, EmSetModify, 1, 0);
            var lifecycle = new List<string>();
            int textChanges = 0;
            int selectionChanges = 0;
            textBox.HandleDestroyed += (_, _) => lifecycle.Add("destroyed");
            textBox.HandleCreated += (_, _) =>
            {
                Assert.Equal(CharacterCasing.Upper, textBox.CharacterCasing);
                lifecycle.Add("created");
            };
            textBox.TextChanged += (_, _) => textChanges++;
            textBox.SelectionChanged += (_, _) => selectionChanges++;

            textBox.CharacterCasing = CharacterCasing.Upper;

            Assert.Equal(new[] { "destroyed", "created" }, lifecycle);
            Assert.False(IsWindow(original));
            Assert.Equal("12345 中国", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.NotEqual(0, SendMessageW(textBox.Handle, EmGetModify, 0, 0));
            Assert.Equal(0x0020090A, GetWindowLongW(textBox.Handle, GwlStyle) & 0x0030091B);
            Assert.Equal(0, textChanges);
            Assert.Equal(0, selectionChanges);

            nint current = textBox.Handle;
            textBox.CharacterCasing = CharacterCasing.Upper;
            Assert.Equal("value", Assert.Throws<InvalidEnumArgumentException>(
                () => textBox.CharacterCasing = (CharacterCasing)3).ParamName);
            Assert.Equal(current, textBox.Handle);
            Assert.Equal(2, lifecycle.Count);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CasingRecreationPreservesCustomAndSystemPasswordMode(bool systemPassword)
        => OnSta(() =>
        {
            using var textBox = new TextBox
            {
                PasswordChar = '#',
                UseSystemPasswordChar = systemPassword,
                Text = "12345"
            };
            nint original = textBox.Handle;
            nint password = SendMessageW(original, EmGetPasswordChar, 0, 0);
            Assert.NotEqual(0, password);
            textBox.Select(1, 3);

            textBox.CharacterCasing = CharacterCasing.Lower;

            Assert.False(IsWindow(original));
            Assert.Equal(password, SendMessageW(textBox.Handle, EmGetPasswordChar, 0, 0));
            Assert.Equal(systemPassword, textBox.UseSystemPasswordChar);
            Assert.Equal("12345", textBox.Text);
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
        });

    [Fact]
    public void RecreationFailurePreservesCommittedCasingAndOriginalException()
        => OnSta(() =>
        {
            using var textBox = new FailingTextBox();
            _ = textBox.Handle;
            var expected = new InvalidOperationException("casing create parameters");
            textBox.ParameterFailure = expected;

            Assert.Same(expected, Assert.Throws<InvalidOperationException>(
                () => textBox.CharacterCasing = CharacterCasing.Upper));
            Assert.Equal(CharacterCasing.Upper, textBox.CharacterCasing);

            textBox.ParameterFailure = null;
            Assert.Equal(0x0008, GetWindowLongW(textBox.Handle, GwlStyle) & CasingMask);
        });

    private static void SendCharacters(nint handle)
    {
        _ = SendMessageW(handle, WmChar, 'A', 1);
        _ = SendMessageW(handle, WmChar, 'b', 1);
        _ = SendMessageW(handle, WmChar, 'C', 1);
        _ = SendMessageW(handle, WmChar, '中', 1);
        _ = SendMessageW(handle, WmChar, '国', 1);
    }

    private static nint CreateNativeEdit(bool multiline, int casingBits)
    {
        uint style = 0x800100C0u | (multiline ? 0x00000004u : 0) | (uint)casingBits;
        nint handle = CreateWindowExW(0x00000200, "EDIT", string.Empty, style,
            0, 0, 200, 100, 0, 0, 0, 0);
        Assert.NotEqual(0, handle);
        return handle;
    }

    private static string ReadNativeText(nint handle)
    {
        int length = GetWindowTextLengthW(handle);
        var text = new StringBuilder(length + 1);
        Assert.Equal(length, GetWindowTextW(handle, text, text.Capacity));
        return text.ToString();
    }

    private static void OnSta(Action action)
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox casing test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class StyleTextBox : TextBox
    {
        internal int RequestedStyle => CreateParams.Style;
    }

    private sealed class FailingTextBox : TextBox
    {
        internal Exception? ParameterFailure { get; set; }
        protected override CreateParams CreateParams => ParameterFailure is { } failure ? throw failure : base.CreateParams;
    }

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string? windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint param);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", EntryPoint = "SendMessageW", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageTextW(nint hWnd, int message, nint wParam, string lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetWindowTextW(nint hWnd, string text);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint hWnd, StringBuilder text, int capacity);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowTextLengthW(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);
}
