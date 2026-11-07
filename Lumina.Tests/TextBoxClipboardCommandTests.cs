using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxClipboardCommandTests
{
    private const int WmCut = 0x0300;
    private const int WmCopy = 0x0301;
    private const int WmPaste = 0x0302;

    [Theory]
    [InlineData(nameof(TextBoxBase.Copy))]
    [InlineData(nameof(TextBoxBase.Cut))]
    [InlineData(nameof(TextBoxBase.Paste))]
    public void ClipboardCommandsHaveStandardNonvirtualBaseSignatures(string name)
    {
        MethodInfo method = typeof(TextBox).GetMethod(name, Type.EmptyTypes)!;
        Assert.Equal(typeof(TextBoxBase), method.DeclaringType);
        Assert.True(method.IsPublic);
        Assert.False(method.IsVirtual);
        Assert.False(method.IsStatic);
        Assert.Equal(typeof(void), method.ReturnType);
        Assert.Empty(method.GetParameters());
    }

    [Theory]
    [InlineData(WmCopy)]
    [InlineData(WmCut)]
    [InlineData(WmPaste)]
    public void ClipboardCommandCreatesPendingHandleAndSendsOneZeroArgumentMessage(int command)
        => OnSta(() =>
        {
            using var textBox = new MessageTextBox { Text = "pending 甲\U0001F600" };
            textBox.Select(2, 4);
            Assert.False(textBox.IsHandleCreated);
            int created = 0;
            textBox.HandleCreated += (_, _) => created++;

            InvokeCommand(textBox, command);

            Assert.True(textBox.IsHandleCreated);
            Assert.Equal(1, created);
            Assert.Same(textBox, Control.FromHandle(textBox.Handle));
            Assert.Equal(new[] { (textBox.Handle, command, (nint)0, (nint)0) }, textBox.Commands);
            Assert.Equal("pending 甲\U0001F600", textBox.Text);
            Assert.Equal(2, textBox.SelectionStart);
            Assert.Equal(4, textBox.SelectionLength);
        });

    [Theory]
    [InlineData(WmCopy)]
    [InlineData(WmCut)]
    [InlineData(WmPaste)]
    public void ClipboardCommandUsesExistingHandleAndNativeProcedureWithoutManagedEditing(int command)
        => OnSta(() =>
        {
            using var textBox = new MessageTextBox
            {
                Text = "before 甲\U0001F600",
                ReadOnly = true,
                ShortcutsEnabled = false,
                MaxLength = 7,
                Modified = true,
            };
            textBox.Select(1, 3);
            nint handle = textBox.Handle;
            int created = 0;
            int changes = 0;
            int modifiedChanges = 0;
            textBox.HandleCreated += (_, _) => created++;
            textBox.TextChanged += (_, _) => changes++;
            textBox.ModifiedChanged += (_, _) => modifiedChanges++;
            textBox.RejectManagedTextAccess = true;

            InvokeCommand(textBox, command);
            InvokeCommand(textBox, command);

            textBox.RejectManagedTextAccess = false;
            Assert.Equal(new[]
            {
                (handle, command, (nint)0, (nint)0),
                (handle, command, (nint)0, (nint)0),
            }, textBox.Commands);
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(0, created);
            Assert.Equal(0, changes);
            Assert.Equal(0, modifiedChanges);
            Assert.Equal("before 甲\U0001F600", textBox.Text);
            Assert.Equal(1, textBox.SelectionStart);
            Assert.Equal(3, textBox.SelectionLength);
            Assert.True(textBox.ReadOnly);
            Assert.False(textBox.ShortcutsEnabled);
            Assert.True(textBox.Modified);
            Assert.Equal(7, textBox.MaxLength);
        });

    [Theory]
    [InlineData(WmCopy)]
    [InlineData(WmCut)]
    [InlineData(WmPaste)]
    public void ClipboardCommandPreservesOriginalHandleCreationFailureAndAllowsRetry(int command)
        => OnSta(() =>
        {
            using var textBox = new FailingCreationTextBox();
            var failure = new InvalidOperationException("clipboard command creation");
            textBox.CreationFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => InvokeCommand(textBox, command)));
            Assert.False(textBox.IsHandleCreated);
            Assert.Empty(textBox.Commands);

            textBox.CreationFailure = null;
            InvokeCommand(textBox, command);

            Assert.True(textBox.IsHandleCreated);
            Assert.Equal(2, textBox.CreationAttempts);
            Assert.Equal(new[] { (textBox.Handle, command, (nint)0, (nint)0) }, textBox.Commands);
        });

    [Theory]
    [InlineData(WmCopy)]
    [InlineData(WmCut)]
    [InlineData(WmPaste)]
    public void ClipboardCommandAllowsFiniteReentryAndIgnoresNativeReturnValue(int command)
        => OnSta(() =>
        {
            using var textBox = new MessageTextBox();
            nint handle = textBox.Handle;
            textBox.AfterCommand = () =>
            {
                if (textBox.Commands.Count == 1) { InvokeCommand(textBox, command); }
            };

            InvokeCommand(textBox, command);

            Assert.Equal(new[]
            {
                (handle, command, (nint)0, (nint)0),
                (handle, command, (nint)0, (nint)0),
            }, textBox.Commands);
            Assert.Equal(handle, textBox.Handle);
        });

    private static void InvokeCommand(TextBoxBase textBox, int command)
    {
        switch (command)
        {
            case WmCopy: textBox.Copy(); break;
            case WmCut: textBox.Cut(); break;
            case WmPaste: textBox.Paste(); break;
            default: throw new ArgumentOutOfRangeException(nameof(command));
        }
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox clipboard command test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private class MessageTextBox : TextBox
    {
        internal List<(nint HWnd, int Message, nint WParam, nint LParam)> Commands { get; } = new();
        internal Action? AfterCommand { get; set; }
        internal bool RejectManagedTextAccess { get; set; }

        [AllowNull]
        public override string Text
        {
            get { RejectTextAccess(); return base.Text; }
            set { RejectTextAccess(); base.Text = value; }
        }

        [AllowNull]
        public override string SelectedText
        {
            get { RejectTextAccess(); return base.SelectedText; }
            set { RejectTextAccess(); base.SelectedText = value; }
        }

        public override int TextLength
        {
            get { RejectTextAccess(); return base.TextLength; }
        }

        public override int MaxLength
        {
            get { RejectTextAccess(); return base.MaxLength; }
            set { RejectTextAccess(); base.MaxLength = value; }
        }

        private void RejectTextAccess()
        {
            if (RejectManagedTextAccess) { throw new InvalidOperationException("Unexpected managed clipboard editing."); }
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg is WmCopy or WmCut or WmPaste)
            {
                // Always stop here: these tests must never read or alter the system Clipboard.
                Commands.Add((message.HWnd, message.Msg, message.WParam, message.LParam));
                AfterCommand?.Invoke();
                message.Result = -1;
                return;
            }
            base.WndProc(ref message);
        }
    }

    private sealed class FailingCreationTextBox : MessageTextBox
    {
        internal Exception? CreationFailure { get; set; }
        internal int CreationAttempts { get; private set; }

        protected override void CreateHandle()
        {
            CreationAttempts++;
            if (CreationFailure is not null) { throw CreationFailure; }
            base.CreateHandle();
        }
    }
}
