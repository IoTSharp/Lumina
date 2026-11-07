using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxForeColorSyncTests
{
    private const int EmGetCharFormat = 0x043A;
    private const int EmSetCharFormat = 0x0444;
    private const int FormatSize = 116;
    private const uint ColorMask = 0x40000000;

    [Fact]
    public void ForeColorDeclaresTheStandardOverrideOfTheControlProperty()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.ForeColor),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(Color), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.True(property.SetMethod!.IsPublic);
        Assert.True(property.GetMethod.IsVirtual);
        Assert.True(property.SetMethod.IsVirtual);
        Assert.Equal(typeof(Control), property.GetMethod.GetBaseDefinition().DeclaringType);
        Assert.Equal(typeof(Control), property.SetMethod.GetBaseDefinition().DeclaringType);
    }

    [Fact]
    public void GetterUsesTheManagedBaseColorWithoutCreatingOrQueryingAWindow()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Capture = true };
            Assert.Equal(SystemColors.WindowText, rich.ForeColor);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Messages);
            Assert.Equal(0, rich.CreatedCount);
        });

    [Theory]
    [InlineData(false, -65536)]
    [InlineData(false, 0x00112233)]
    [InlineData(true, 0)]
    public void SetterBeforeCreationUsesOnlyTheBaseColorAndKeepsTheWindowAbsent(bool empty, int argb)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Navy, Capture = true };
            int changes = 0;
            rich.ForeColorChanged += (_, _) => changes++;
            Color value = empty ? Color.Empty : Color.FromArgb(argb);

            rich.ForeColor = value;
            rich.ForeColor = value;

            Assert.Equal(empty ? SystemColors.WindowText : value, rich.ForeColor);
            Assert.Equal(1, changes);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Messages);
        });

    [Fact]
    public void SetterBeforeCreationDoesNotReadCreateParams()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { FailCreation = true, Capture = true };
            rich.ForeColor = Color.Crimson;
            Assert.Equal(Color.Crimson, rich.ForeColor);
            Assert.False(rich.IsHandleCreated);
            Assert.Empty(rich.Messages);
        });

    [Theory]
    [InlineData(false, -65536)]
    [InlineData(false, 0x00112233)]
    [InlineData(false, -16711165)]
    [InlineData(true, 0)]
    public void SetterQueriesDefaultAndSubmitsAllTextPreservingEveryNonColorField(bool empty, int argb)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            byte[] initial = MakePoisonFormat(0xFFFFFFFF, 0x7FFFFFFF, 0x00665544);
            rich.Reply = initial;
            rich.SetResult = 1;
            rich.Capture = true;
            Color value = empty ? Color.Empty : Color.FromArgb(argb);

            rich.ForeColor = value;

            Assert.Equal(2, rich.Messages.Count);
            AssertDefaultQuery(rich.Messages[0]);
            FormatMessage submission = rich.Messages[1];
            Assert.Equal(EmSetCharFormat, submission.Id);
            Assert.Equal((nint)4, submission.Range);
            byte[] expected = (byte[])initial.Clone();
            BitConverter.GetBytes(ColorMask).CopyTo(expected, 4);
            BitConverter.GetBytes(0).CopyTo(expected, 8);
            BitConverter.GetBytes(ColorTranslator.ToWin32(value)).CopyTo(expected, 20);
            Assert.Equal(expected, submission.Bytes);
            Assert.Equal(empty ? SystemColors.WindowText : value, rich.ForeColor);
            Assert.Equal(1, rich.CreatedCount);
        });

    [Theory]
    [InlineData(0u)]
    [InlineData(0x20000000u)]
    public void MatchingRgbWithoutTheColorMaskStillSubmitsTheAllTextFormat(uint mask)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.Reply = MakeFormat(mask, 0, ColorTranslator.ToWin32(Color.Crimson));
            rich.SetResult = 1;
            rich.Capture = true;

            rich.ForeColor = Color.Crimson;

            Assert.Equal(new[] { EmGetCharFormat, EmSetCharFormat }, rich.Messages.Select(message => message.Id));
            Assert.Equal((nint)4, rich.Messages[1].Range);
            Assert.Equal(Color.Crimson, rich.ForeColor);
        });

    [Theory]
    [InlineData(0u)]
    [InlineData(ColorMask)]
    [InlineData(0xFFFFFFFFu)]
    public void MatchingValidNativeColorSkipsSetRegardlessOfEffectsAndStillUpdatesTheBaseColor(uint effects)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Navy };
            _ = rich.Handle;
            rich.Reply = MakeFormat(ColorMask, effects, ColorTranslator.ToWin32(Color.Crimson));
            rich.SetResult = 0;
            rich.Capture = true;
            int changes = 0;
            rich.ForeColorChanged += (_, _) => changes++;

            rich.ForeColor = Color.Crimson;

            AssertDefaultQuery(Assert.Single(rich.Messages));
            Assert.Equal(Color.Crimson, rich.ForeColor);
            Assert.Equal(1, changes);
        });

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void EveryNonzeroNativeResultAllowsTheManagedAssignment(int result)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Navy };
            _ = rich.Handle;
            rich.Reply = MakeFormat(0, 0, 0);
            rich.SetResult = result;
            rich.Capture = true;
            int changes = 0;
            rich.ForeColorChanged += (_, _) => changes++;

            rich.ForeColor = Color.Crimson;

            Assert.Equal(Color.Crimson, rich.ForeColor);
            Assert.Equal(1, changes);
            Assert.Equal(2, rich.Messages.Count);
        });

    [Fact]
    public void ZeroNativeResultKeepsTheManagedColorAndNotificationsAndAllowsRetry()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Navy };
            _ = rich.Handle;
            rich.Reply = MakeFormat(0, 0, 0);
            rich.SetResult = 0;
            rich.Capture = true;
            int changes = 0;
            rich.ForeColorChanged += (_, _) => changes++;

            rich.ForeColor = Color.Crimson;

            Assert.Equal(Color.Navy, rich.ForeColor);
            Assert.Equal(0, changes);
            Assert.Equal(2, rich.Messages.Count);
            rich.Messages.Clear();
            rich.SetResult = 1;

            rich.ForeColor = Color.Crimson;

            Assert.Equal(Color.Crimson, rich.ForeColor);
            Assert.Equal(1, changes);
            Assert.Equal(2, rich.Messages.Count);
            AssertDefaultQuery(rich.Messages[0]);
        });

    [Fact]
    public void RepeatedAssignmentsQueryWithANewZeroBufferEvenWhenTheManagedValueMatches()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Crimson };
            _ = rich.Handle;
            rich.Reply = MakePoisonFormat(0, 0xFFFFFFFF, 0x00665544);
            rich.SetResult = 1;
            rich.Capture = true;
            int changes = 0;
            rich.ForeColorChanged += (_, _) => changes++;

            rich.ForeColor = Color.Crimson;
            rich.ForeColor = Color.Crimson;

            Assert.Equal(4, rich.Messages.Count);
            AssertDefaultQuery(rich.Messages[0]);
            AssertDefaultQuery(rich.Messages[2]);
            Assert.Equal(rich.Messages[1].Bytes, rich.Messages[3].Bytes);
            Assert.Equal(0, changes);
        });

    [Fact]
    public void MatchingManagedAndNativeColorsStillQueryWithoutRaisingAnEvent()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Crimson };
            _ = rich.Handle;
            rich.Reply = MakeFormat(ColorMask, 0, ColorTranslator.ToWin32(Color.Crimson));
            rich.Capture = true;
            int changes = 0;
            rich.ForeColorChanged += (_, _) => changes++;

            rich.ForeColor = Color.Crimson;

            AssertDefaultQuery(Assert.Single(rich.Messages));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void GetterDoesNotQueryOrReplaceItsManagedValueAfterANativeChange()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Crimson };
            _ = rich.Handle;
            SetFormat(rich.Handle, 0, MakeFormat(ColorMask, 0, ColorTranslator.ToWin32(Color.Blue)));
            rich.Capture = true;

            Assert.Equal(Color.Crimson, rich.ForeColor);

            Assert.Empty(rich.Messages);
        });

    [Fact]
    public void SetterDoesNotReadVirtualTextOrSelectionProperties()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            _ = rich.Handle;
            rich.Reply = MakeFormat(0, 0, 0);
            rich.SetResult = 1;
            rich.Capture = true;
            rich.FailUnrelatedGetters = true;
            try
            {
                rich.ForeColor = Color.Crimson;
                Assert.Equal(Color.Crimson, rich.ForeColor);
                Assert.Equal(2, rich.Messages.Count);
            }
            finally { rich.FailUnrelatedGetters = false; }
        });

    [Fact]
    public void NativeSubmissionPrecedesTheBaseEventAndRetainsTheOriginalEventException()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Navy };
            _ = rich.Handle;
            rich.Reply = MakeFormat(0, 0, 0);
            rich.SetResult = 1;
            rich.Capture = true;
            var expected = new InvalidOperationException("ForeColor event failure");
            rich.ForeColorChanged += (_, _) =>
            {
                Assert.Equal(Color.Crimson, rich.ForeColor);
                Assert.Equal(2, rich.Messages.Count);
                Assert.Equal(EmSetCharFormat, rich.Messages[1].Id);
                throw expected;
            };

            InvalidOperationException actual = Assert.Throws<InvalidOperationException>(() => rich.ForeColor = Color.Crimson);

            Assert.Same(expected, actual);
            Assert.Equal(Color.Crimson, rich.ForeColor);
        });

    [Fact]
    public void EventReentryPerformsItsOwnNativeQueryAndKeepsTheLastAssignment()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { ForeColor = Color.Navy };
            _ = rich.Handle;
            rich.Reply = MakeFormat(0, 0, 0);
            rich.SetResult = 1;
            rich.Capture = true;
            int changes = 0;
            rich.ForeColorChanged += (_, _) =>
            {
                changes++;
                if (changes == 1) { rich.ForeColor = Color.Lime; }
            };

            rich.ForeColor = Color.Crimson;

            Assert.Equal(Color.Lime, rich.ForeColor);
            Assert.Equal(2, changes);
            Assert.Equal(4, rich.Messages.Count);
            AssertDefaultQuery(rich.Messages[0]);
            AssertDefaultQuery(rich.Messages[2]);
            Assert.Equal(ColorTranslator.ToWin32(Color.Crimson), BitConverter.ToInt32(rich.Messages[1].Bytes, 20));
            Assert.Equal(ColorTranslator.ToWin32(Color.Lime), BitConverter.ToInt32(rich.Messages[3].Bytes, 20));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RealSetterFormatsTheWholeDocumentAndPreservesTextSelectionAndOtherFormats(bool readOnly)
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            rich.Rtf = @"{\rtf1\ansi{\colortbl ;\red255\green0\blue0;\red255\green255\blue0;}\fs30\b\i\cf1\highlight2 abcdefgh}";
            rich.ReadOnly = readOnly;
            rich.Select(2, 3);
            byte[] before = ReadFormat(rich.Handle, 1);

            rich.ForeColor = Color.FromArgb(17, 34, 51);

            Assert.Equal("abcdefgh", rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(readOnly, rich.ReadOnly);
            AssertRgb(Color.FromArgb(17, 34, 51), rich.ForeColor);
            Assert.Equal(0x00332211, BitConverter.ToInt32(ReadFormat(rich.Handle, 0), 20));
            foreach (int index in new[] { 0, 3, 7 })
            {
                rich.Select(index, 1);
                byte[] after = ReadFormat(rich.Handle, 1);
                Assert.Equal(0x00332211, BitConverter.ToInt32(after, 20));
                Assert.Equal(BitConverter.ToInt32(before, 12), BitConverter.ToInt32(after, 12));
                Assert.Equal(BitConverter.ToUInt32(before, 8) & 3u, BitConverter.ToUInt32(after, 8) & 3u);
                Assert.Equal(BitConverter.ToInt32(before, 96), BitConverter.ToInt32(after, 96));
            }
        });

    [Fact]
    public void RealMatchingDefaultColorLeavesDifferentSelectionFormattingIntact()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            _ = rich.Handle;
            rich.ForeColor = Color.Crimson;
            rich.Select(2, 3);
            rich.SelectionColor = Color.Blue;
            Assert.Equal(ColorTranslator.ToWin32(Color.Crimson), BitConverter.ToInt32(ReadFormat(rich.Handle, 0), 20));
            rich.Capture = true;

            rich.ForeColor = Color.Crimson;

            AssertDefaultQuery(Assert.Single(rich.Messages));
            rich.Capture = false;
            AssertRgb(Color.Blue, rich.SelectionColor);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal("abcdefgh", rich.Text);
        });

    [Fact]
    public void SameManagedColorRepairsAChangedNativeDefaultAndFormatsAllText()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh", ForeColor = Color.Crimson };
            _ = rich.Handle;
            SetFormat(rich.Handle, 0, MakeFormat(ColorMask, 0, ColorTranslator.ToWin32(Color.Blue)));
            rich.Select(2, 3);
            rich.Capture = true;

            rich.ForeColor = Color.Crimson;

            Assert.Equal(new[] { EmGetCharFormat, EmSetCharFormat }, rich.Messages.Select(message => message.Id));
            rich.Capture = false;
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            rich.SelectAll();
            AssertRgb(Color.Crimson, rich.SelectionColor);
            Assert.Equal("abcdefgh", rich.Text);
        });

    [Fact]
    public void EmptyColorSubmitsBlackWhileRestoringTheBaseDefaultProperty()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh", ForeColor = Color.Blue };
            _ = rich.Handle;

            rich.ForeColor = Color.Empty;

            Assert.Equal(SystemColors.WindowText, rich.ForeColor);
            Assert.Equal(0, BitConverter.ToInt32(ReadFormat(rich.Handle, 0), 20));
            rich.SelectAll();
            Assert.Equal(0, BitConverter.ToInt32(ReadFormat(rich.Handle, 1), 20));
        });

    [Fact]
    public void RealAllRangeColorRequestMatchesAnIndependentRichEditAndKeepsSelection()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox { Text = "abcdefgh" };
            _ = rich.Handle;
            nint native = CreateWindowExW(0, "RICHEDIT50W", "abcdefgh", unchecked((int)0x80000004),
                0, 0, 100, 96, 0, 0, 0, 0);
            Assert.NotEqual((nint)0, native);
            try
            {
                byte[] initial = MakeFormat(ColorMask | 0x80000003u, 3, ColorTranslator.ToWin32(Color.Blue));
                BitConverter.GetBytes(280).CopyTo(initial, 12);
                SetFormat(native, 4, initial);
                SetFormat(rich.Handle, 4, initial);
                SelectNative(native, 2, 5);
                rich.Select(2, 3);
                _ = SendMessageW(native, 0x00B9, 0, 0);
                rich.Modified = false;
                Color value = Color.FromArgb(17, 34, 51);

                byte[] expectedRequest = ReadFormat(native, 0);
                BitConverter.GetBytes(ColorMask).CopyTo(expectedRequest, 4);
                BitConverter.GetBytes(0).CopyTo(expectedRequest, 8);
                BitConverter.GetBytes(ColorTranslator.ToWin32(value)).CopyTo(expectedRequest, 20);
                SetFormat(native, 4, expectedRequest);
                rich.ForeColor = value;

                Assert.Equal("abcdefgh", rich.Text);
                Assert.Equal(2, rich.SelectionStart);
                Assert.Equal(3, rich.SelectionLength);
                Assert.Equal(SendMessageW(native, 0x00B8, 0, 0) != 0, rich.Modified);
                Assert.Equal(SendMessageW(native, 0x00C6, 0, 0) != 0, rich.CanUndo);
                Assert.Equal(BitConverter.ToInt32(ReadFormat(native, 0), 20),
                    BitConverter.ToInt32(ReadFormat(rich.Handle, 0), 20));
                byte[] expected = ReadFormat(native, 1);
                byte[] actual = ReadFormat(rich.Handle, 1);
                Assert.Equal(0x00332211, BitConverter.ToInt32(actual, 20));
                Assert.Equal(BitConverter.ToInt32(expected, 20), BitConverter.ToInt32(actual, 20));
                Assert.Equal(BitConverter.ToInt32(expected, 12), BitConverter.ToInt32(actual, 12));
                Assert.Equal(BitConverter.ToUInt32(expected, 8) & 3u, BitConverter.ToUInt32(actual, 8) & 3u);
            }
            finally { Assert.True(DestroyWindow(native)); }
        });

    [Fact]
    public void AllTextForegroundSurvivesRtfHandleRecreationWithSelectionAndFormatting()
        => OnSta(() =>
        {
            using var rich = new FormatRichTextBox();
            rich.Rtf = @"{\rtf1\ansi\fs30\b abcdefgh}";
            rich.Select(2, 3);
            rich.ForeColor = Color.Crimson;
            rich.Modified = true;
            byte[] before = ReadFormat(rich.Handle, 1);

            rich.RecreateNow();

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal("abcdefgh", rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.True(rich.Modified);
            byte[] after = ReadFormat(rich.Handle, 1);
            Assert.Equal(BitConverter.ToInt32(before, 20), BitConverter.ToInt32(after, 20));
            Assert.Equal(BitConverter.ToInt32(before, 12), BitConverter.ToInt32(after, 12));
            Assert.Equal(BitConverter.ToUInt32(before, 8) & 1u, BitConverter.ToUInt32(after, 8) & 1u);
        });

    private static void AssertDefaultQuery(FormatMessage request)
    {
        Assert.Equal(EmGetCharFormat, request.Id);
        Assert.Equal((nint)0, request.Range);
        Assert.Equal(MakeFormat(0, 0, 0), request.Bytes);
    }

    private static byte[] MakeFormat(uint mask, uint effects, int foreground)
    {
        byte[] bytes = new byte[FormatSize];
        BitConverter.GetBytes(FormatSize).CopyTo(bytes, 0);
        BitConverter.GetBytes(mask).CopyTo(bytes, 4);
        BitConverter.GetBytes(effects).CopyTo(bytes, 8);
        BitConverter.GetBytes(foreground).CopyTo(bytes, 20);
        return bytes;
    }

    private static byte[] MakePoisonFormat(uint mask, uint effects, int foreground)
    {
        byte[] bytes = new byte[FormatSize];
        Array.Fill(bytes, (byte)0x5A);
        BitConverter.GetBytes(FormatSize).CopyTo(bytes, 0);
        BitConverter.GetBytes(mask).CopyTo(bytes, 4);
        BitConverter.GetBytes(effects).CopyTo(bytes, 8);
        BitConverter.GetBytes(foreground).CopyTo(bytes, 20);
        return bytes;
    }

    private static byte[] ReadFormat(nint handle, nint range)
    {
        nint buffer = Marshal.AllocHGlobal(FormatSize);
        try
        {
            Marshal.Copy(MakeFormat(0, 0, 0), 0, buffer, FormatSize);
            _ = SendMessageW(handle, EmGetCharFormat, range, buffer);
            byte[] bytes = new byte[FormatSize];
            Marshal.Copy(buffer, bytes, 0, bytes.Length);
            return bytes;
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SetFormat(nint handle, nint range, byte[] bytes)
    {
        nint buffer = Marshal.AllocHGlobal(FormatSize);
        try
        {
            Marshal.Copy(bytes, 0, buffer, bytes.Length);
            Assert.NotEqual((nint)0, SendMessageW(handle, EmSetCharFormat, range, buffer));
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void SelectNative(nint handle, int start, int end)
    {
        nint buffer = Marshal.AllocHGlobal(8);
        try
        {
            Marshal.WriteInt32(buffer, start);
            Marshal.WriteInt32(buffer, 4, end);
            _ = SendMessageW(handle, 0x0437, 0, buffer);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    private static void AssertRgb(Color expected, Color actual)
        => Assert.Equal(Color.FromArgb(expected.R, expected.G, expected.B).ToArgb(), actual.ToArgb());

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try
                {
                    Application.ExitThread();
                    typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                        BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox ForeColor synchronization test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct FormatMessage(int Id, nint Range, byte[] Bytes);

    private sealed class FormatRichTextBox : RichTextBox
    {
        internal bool Capture { get; set; }
        internal byte[]? Reply { get; set; }
        internal nint? SetResult { get; set; }
        internal List<FormatMessage> Messages { get; } = [];
        internal bool FailCreation { get; set; }
        internal bool FailUnrelatedGetters { get; set; }
        internal int CreatedCount { get; private set; }

        public override string Text
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("Text must not be read.") : base.Text;
            set => base.Text = value;
        }

        public override int SelectionLength
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("SelectionLength must not be read.") : base.SelectionLength;
            set => base.SelectionLength = value;
        }

        public override string SelectedText
        {
            get => FailUnrelatedGetters ? throw new InvalidOperationException("SelectedText must not be read.") : base.SelectedText;
            set => base.SelectedText = value;
        }

        internal void RecreateNow() => RecreateHandle();

        protected override CreateParams CreateParams
        {
            get => FailCreation ? throw new InvalidOperationException("ForeColor must not create a window.") : base.CreateParams;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }

        protected override void WndProc(ref Message message)
        {
            if (Capture && message.Msg is EmGetCharFormat or EmSetCharFormat)
            {
                byte[] bytes = new byte[FormatSize];
                Marshal.Copy(message.LParam, bytes, 0, bytes.Length);
                Messages.Add(new FormatMessage(message.Msg, message.WParam, bytes));
                if (message.Msg == EmGetCharFormat && Reply is not null)
                {
                    Marshal.Copy(Reply, 0, message.LParam, Reply.Length);
                    message.Result = 0;
                    return;
                }
                if (message.Msg == EmSetCharFormat && SetResult is nint result)
                {
                    message.Result = result;
                    return;
                }
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(int extendedStyle, string className, string title,
        int style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);
}
