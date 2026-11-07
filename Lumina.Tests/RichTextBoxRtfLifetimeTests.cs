using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxRtfLifetimeTests
{
    private const int EmReplaceSel = 0x00C2;
    private const int EmGetLimitText = 0x00D5;
    private const int EmGetCharFormat = 0x043A;
    private const int EmGetParaFormat = 0x043D;
    private const int EmSetCharFormat = 0x0444;
    private const string StyledRtf = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil Arial;}}{\colortbl ;\red255\green0\blue0;}\uc1\pard\qc\li360\ri180\fi-120\f0\fs28\cf1\b styled\b0\cf0  tail \u20013?\u-10179?\u-8704?}";
    private const string StyledText = "styled tail 中😀";

    [Theory]
    [InlineData("explicit")]
    [InlineData("scrollbars")]
    [InlineData("urls")]
    [InlineData("selection")]
    [InlineData("border")]
    [InlineData("wrap")]
    [InlineData("multiline")]
    public void RecreationRetainsRealCharacterAndParagraphFormatting(string trigger)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox { MaxLength = 3 };
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            Assert.Equal(StyledText, rich.Text);
            rich.Select(1, 3);
            FormatState expected = ReadFormat(rich.Handle);
            Assert.Equal(1u, expected.Effects & 1u);
            Assert.Equal(0x000000FF, expected.Color);
            Assert.Equal(280, expected.Height);
            Assert.Equal((ushort)3, expected.Alignment);
            Assert.NotEqual(0, expected.StartIndent);
            Assert.Equal(180, expected.RightIndent);
            Assert.NotEqual(0, expected.Offset);
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            RecreateUsing(rich, trigger);

            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(StyledText, rich.Text);
            Assert.Equal(1, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(expected, ReadFormat(rich.Handle));
            Assert.Equal((nint)3, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RecreationPreservesModifiedSelectionAndLimitAndClearsUndo(bool modified)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox { MaxLength = 2 };
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            rich.Select(2, 4);
            rich.Modified = modified;
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.RecreateNow();

            Assert.Equal(StyledText, rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(4, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.False(rich.CanUndo);
            Assert.Equal(2, rich.MaxLength);
            Assert.Equal((nint)2, SendMessageW(rich.Handle, EmGetLimitText, 0, 0));
            Assert.Equal(0, changes);
        });

    [Fact]
    public void DestructionCachesTheLatestNativeFormattingRatherThanTheLastAssignedRtf()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            rich.Select(7, 4);
            SetNativeCharacterFormat(rich.Handle, bold: true, color: 0x0000FF00);
            FormatState expected = ReadFormat(rich.Handle);
            Assert.Equal(1u, expected.Effects & 1u);
            Assert.Equal(0x0000FF00, expected.Color);

            rich.RecreateNow();

            Assert.Equal(StyledText, rich.Text);
            Assert.Equal(7, rich.SelectionStart);
            Assert.Equal(4, rich.SelectionLength);
            Assert.Equal(expected, ReadFormat(rich.Handle));
        });

    [Fact]
    public void NativeTextEditsAfterRtfAssignmentAreIncludedInTheRecreatedDocument()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            nint handle = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            rich.Select(rich.TextLength, 0);
            _ = SendMessageW(handle, EmReplaceSel, 1, "!");
            Assert.Equal(StyledText + "!", rich.Text);

            rich.RecreateNow();

            Assert.Equal(StyledText + "!", rich.Text);
            rich.Select(0, 6);
            Assert.Equal(1u, ReadFormat(rich.Handle).Effects & 1u);
        });

    [Fact]
    public void SelectedRtfReplacementAndItsFormatSurviveRecreation()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox { Text = "left old right" };
            _ = CreateHostedHandle(form, rich);
            rich.Select(5, 3);
            rich.SelectedRtf = @"{\rtf1\ansi\b NEW}";
            Assert.Equal("left NEW right", rich.Text);
            rich.Select(5, 3);
            FormatState expected = ReadFormat(rich.Handle);
            Assert.Equal(1u, expected.Effects & 1u);

            rich.RecreateNow();

            Assert.Equal("left NEW right", rich.Text);
            Assert.Equal(5, rich.SelectionStart);
            Assert.Equal(3, rich.SelectionLength);
            Assert.Equal(expected, ReadFormat(rich.Handle));
        });

    [Fact]
    public void SelectedPlainTextEditsAreNotReplacedByAnOlderRtfCache()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            rich.Select(7, 4);
            rich.SelectedText = "new";
            string expected = rich.Text;
            Assert.Equal("styled new 中😀", expected);

            rich.RecreateNow();

            Assert.Equal(expected, rich.Text);
            rich.Select(0, 6);
            Assert.Equal(1u, ReadFormat(rich.Handle).Effects & 1u);
        });

    [Fact]
    public void RtfGetterAfterDestructionReturnsTheCachedDocumentWithoutCreatingAHandle()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            nint handle = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            string? expected = rich.Rtf;

            rich.DestroyNow();

            Assert.False(rich.IsHandleCreated);
            Assert.False(IsWindow(handle));
            Assert.Equal(expected, rich.Rtf);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(1, rich.CreatedCount);
        });

    [Fact]
    public void TextGetterRecreatesACachedRtfDocumentUsingTheNativeTranslator()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            rich.Select(0, 6);
            FormatState expected = ReadFormat(rich.Handle);
            rich.DestroyNow();
            Assert.False(rich.IsHandleCreated);
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            Assert.Equal(StyledText, rich.Text);

            Assert.True(rich.IsHandleCreated);
            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(0, rich.SelectionStart);
            Assert.Equal(6, rich.SelectionLength);
            Assert.Equal(expected, ReadFormat(rich.Handle));
            Assert.Equal(0, changes);
        });

    [Theory]
    [InlineData("replacement")]
    [InlineData("replacement 中😀\r\nline")]
    [InlineData("")]
    public void PlainTextAssignedWithoutAHandleTakesPrecedenceOverCachedRtf(string text)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            rich.DestroyNow();
            int changes = 0;
            rich.TextChanged += (_, _) => changes++;

            rich.Text = text;

            Assert.False(rich.IsHandleCreated);
            Assert.Equal(text, rich.Text);
            _ = rich.Handle;
            Assert.Equal(text.Replace("\r\n", "\n"), rich.Text);
            Assert.Equal(2, rich.CreatedCount);
            Assert.Equal(0, changes);
            Assert.False(rich.Modified);
        });

    [Fact]
    public void NewRtfAssignedWithoutAHandleSupersedesTheDestroyedDocument()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            rich.DestroyNow();

            rich.Rtf = @"{\rtf1\ansi\i newer}";

            Assert.True(rich.IsHandleCreated);
            Assert.Equal("newer", rich.Text);
            rich.Select(0, 5);
            Assert.Equal(2u, ReadFormat(rich.Handle).Effects & 2u);
            rich.RecreateNow();
            Assert.Equal("newer", rich.Text);
            Assert.Equal(2u, ReadFormat(rich.Handle).Effects & 2u);
        });

    [Fact]
    public void FailedHandleCreationLeavesCachedRtfAvailableForRetry()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            rich.DestroyNow();
            string? expected = rich.Rtf;
            rich.FailCreation = true;

            InvalidOperationException failure = Assert.Throws<InvalidOperationException>(() => rich.Text);

            Assert.Same(rich.CreationFailure, failure);
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(expected, rich.Rtf);
            rich.FailCreation = false;
            Assert.Equal(StyledText, rich.Text);
            Assert.Equal(2, rich.CreatedCount);
            rich.Select(0, 6);
            Assert.Equal(1u, ReadFormat(rich.Handle).Effects & 1u);
        });

    [Fact]
    public void RecreationReportsEmptyTextToDestroyHandlersButKeepsTheActualRtf()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new LifetimeRichTextBox();
            _ = CreateHostedHandle(form, rich);
            rich.Rtf = StyledRtf;
            bool observed = false;
            bool recreating = false;
            string? observedText = null;
            string? observedRtf = null;
            EventHandler handler = (_, _) =>
            {
                observed = true;
                recreating = rich.RecreatingHandle;
                observedText = rich.Text;
                observedRtf = rich.Rtf;
            };
            rich.HandleDestroyed += handler;

            try { rich.RecreateNow(); }
            finally { rich.HandleDestroyed -= handler; }

            Assert.True(observed);
            Assert.True(recreating);
            Assert.Equal(string.Empty, observedText);
            Assert.Contains(@"\b", observedRtf!);
            Assert.Equal(StyledText, rich.Text);
            rich.Select(0, 6);
            Assert.Equal(1u, ReadFormat(rich.Handle).Effects & 1u);
        });

    [Fact]
    public void SeparateInstancesKeepTheirOwnRtfCachesAcrossDestruction()
        => OnSta(() =>
        {
            using var first = new LifetimeRichTextBox();
            using var second = new LifetimeRichTextBox();
            first.Rtf = StyledRtf;
            second.Rtf = @"{\rtf1\ansi\i second}";
            first.DestroyNow();
            second.DestroyNow();

            Assert.Equal("second", second.Text);
            Assert.Equal(StyledText, first.Text);
            first.Select(0, 6);
            second.Select(0, 6);
            Assert.Equal(1u, ReadFormat(first.Handle).Effects & 1u);
            Assert.Equal(2u, ReadFormat(second.Handle).Effects & 2u);
            Assert.NotEqual(first.Handle, second.Handle);
        });

    [Fact]
    public void DisposalDestroysTheFormattedNativeWindowAndRemainsRepeatable()
        => OnSta(() =>
        {
            using var rich = new LifetimeRichTextBox();
            rich.Rtf = StyledRtf;
            nint handle = rich.Handle;

            rich.Dispose();
            rich.Dispose();

            Assert.True(rich.IsDisposed);
            Assert.False(rich.IsHandleCreated);
            Assert.False(IsWindow(handle));
        });

    private static void RecreateUsing(LifetimeRichTextBox rich, string trigger)
    {
        switch (trigger)
        {
            case "explicit": rich.RecreateNow(); break;
            case "scrollbars": rich.ScrollBars = RichTextBoxScrollBars.None; break;
            case "urls": rich.DetectUrls = false; break;
            case "selection": rich.HideSelection = false; break;
            case "border": rich.BorderStyle = BorderStyle.None; break;
            case "wrap": rich.WordWrap = false; break;
            case "multiline": rich.Multiline = false; break;
            default: throw new ArgumentOutOfRangeException(nameof(trigger));
        }
    }

    private static nint CreateHostedHandle(Form form, RichTextBox rich)
    {
        form.Controls.Add(rich);
        _ = form.Handle;
        return rich.Handle;
    }

    private static FormatState ReadFormat(nint handle)
    {
        nint storage = Marshal.AllocHGlobal(272);
        nint character = storage;
        nint paragraph = storage + 116;
        try
        {
            Marshal.Copy(new byte[116], 0, character, 116);
            Marshal.Copy(new byte[156], 0, paragraph, 156);
            Marshal.WriteInt32(character, 116);
            Marshal.WriteInt32(paragraph, 156);
            _ = SendMessageW(handle, EmGetCharFormat, 1, character);
            _ = SendMessageW(handle, EmGetParaFormat, 0, paragraph);
            return new FormatState(unchecked((uint)Marshal.ReadInt32(character, 8)),
                Marshal.ReadInt32(character, 20), Marshal.ReadInt32(character, 12),
                unchecked((ushort)Marshal.ReadInt16(paragraph, 24)),
                Marshal.ReadInt32(paragraph, 12), Marshal.ReadInt32(paragraph, 16),
                Marshal.ReadInt32(paragraph, 20));
        }
        finally
        {
            Marshal.FreeHGlobal(storage);
        }
    }

    private static void SetNativeCharacterFormat(nint handle, bool bold, int color)
    {
        nint format = Marshal.AllocHGlobal(116);
        try
        {
            Marshal.Copy(new byte[116], 0, format, 116);
            Marshal.WriteInt32(format, 116);
            Marshal.WriteInt32(format, 4, 0x40000001);
            Marshal.WriteInt32(format, 8, bold ? 1 : 0);
            Marshal.WriteInt32(format, 20, color);
            Assert.NotEqual((nint)0, SendMessageW(handle, EmSetCharFormat, 1, format));
        }
        finally { Marshal.FreeHGlobal(format); }
    }

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox RTF lifetime test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct FormatState(uint Effects, int Color, int Height,
        ushort Alignment, int StartIndent, int RightIndent, int Offset);

    private sealed class LifetimeRichTextBox : RichTextBox
    {
        internal int CreatedCount { get; private set; }
        internal bool FailCreation { get; set; }
        internal InvalidOperationException CreationFailure { get; } = new("RTF lifetime creation failure");

        internal void RecreateNow() => RecreateHandle();
        internal void DestroyNow() => DestroyHandle();

        protected override CreateParams CreateParams
        {
            get
            {
                if (FailCreation) { throw CreationFailure; }
                return base.CreateParams;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            CreatedCount++;
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, string text);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
