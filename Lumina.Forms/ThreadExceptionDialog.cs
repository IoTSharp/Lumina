// Button results and warning behavior adapted from dotnet/winforms v10.0.11.
// Licensed to the .NET Foundation under the MIT license.

using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Lumina.Forms;

/// <summary>
/// Displays an unhandled UI-thread exception using the ordinary Lumina.Forms
/// controls.  The dialog keeps the exception text available without relying on
/// a second process or runtime-generated controls, which keeps the path usable
/// by the Native AOT target.
/// </summary>
public class ThreadExceptionDialog : Form
{
    private readonly Label _message = new();
    private readonly TextBox _details = new(multiline: true, readOnly: true);
    private readonly Button _detailsButton = new();
    private readonly Button _continueButton = new();
    private bool _detailsVisible;

    /// <summary>Initializes an exception dialog for <paramref name="t"/>.</summary>
    public ThreadExceptionDialog(Exception t)
    {
        ArgumentNullException.ThrowIfNull(t);
        Text = "Unhandled thread exception";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(540, 130);

        _message.Text = CreateMessage(t);
        _message.SetBounds(16, 16, 508, 58);

        _details.Text = CreateDetails(t);
        _details.ScrollBars = ScrollBars.Both;
        _details.WordWrap = false;
        _details.AcceptsReturn = false;
        _details.SetBounds(16, 130, 508, 190);
        _details.TabStop = false;
        _details.Visible = false;

        _detailsButton.Text = "Show details";
        _detailsButton.SetBounds(16, 88, 130, 26);
        _detailsButton.Click += DetailsButtonClick;

        _continueButton.Text = "Continue";
        _continueButton.DialogResult = DialogResult.Cancel;
        _continueButton.SetBounds(374, 88, 150, 26);

        Controls.Add(_message);
        Controls.Add(_details);
        if (t is not WarningException) { Controls.Add(_detailsButton); }
        Controls.Add(_continueButton);

        AcceptButton = _continueButton;
        CancelButton = _continueButton;

        if (t is not WarningException && Application.AllowQuit)
        {
            Button quitButton = new()
            {
                Text = "Quit",
                DialogResult = DialogResult.Abort,
            };
            quitButton.SetBounds(214, 88, 150, 26);
            Controls.Add(quitButton);
        }
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
        get => base.AutoSize;
        set => base.AutoSize = value;
    }

    /// <inheritdoc />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler? AutoSizeChanged
    {
        add => base.AutoSizeChanged += value;
        remove => base.AutoSizeChanged -= value;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && _detailsButton.Parent is null)
        {
            // Warning dialogs omit the details action, so that button is not
            // owned by Controls and must still participate in IDisposable.
            _detailsButton.Dispose();
        }
        base.Dispose(disposing);
    }

    private void DetailsButtonClick(object? sender, EventArgs e)
    {
        _detailsVisible = !_detailsVisible;
        _details.Visible = _detailsVisible;
        _detailsButton.Text = _detailsVisible ? "Hide details" : "Show details";
        ClientSize = _detailsVisible ? new Size(540, 330) : new Size(540, 130);
    }

    private static string CreateMessage(Exception exception)
    {
        string message = exception.Message;
        return string.IsNullOrWhiteSpace(message)
            ? exception.GetType().Name
            : $"{exception.GetType().Name}: {message}";
    }

    private static string CreateDetails(Exception exception)
    {
        var builder = new StringBuilder();
        builder.AppendLine(exception.ToString());
        builder.AppendLine();
        builder.Append("Base directory: ");
        builder.AppendLine(AppContext.BaseDirectory);
        builder.AppendLine();
        builder.AppendLine("Loaded assemblies:");
        int assemblyCount = 0;
        foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (assemblyCount++ >= 512) { break; }
            builder.AppendLine(assembly.GetName().Name ?? "<unnamed>");
        }
        return builder.ToString();
    }
}
