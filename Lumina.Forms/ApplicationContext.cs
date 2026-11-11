// ApplicationContext lifecycle adapted from dotnet/winforms v10.0.11.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses those portions under the MIT license.

using System.ComponentModel;

namespace Lumina.Forms;

/// <summary>Owns the main form and exit policy for a UI thread's message loop.</summary>
public class ApplicationContext : IDisposable
{
    private Form? _mainForm;

    /// <summary>Creates a context without a main form.</summary>
    public ApplicationContext() : this(null) { }

    /// <summary>Creates a context whose message loop ends when its main form closes.</summary>
    public ApplicationContext(Form? mainForm)
    {
        if (GetType() == typeof(ApplicationContext)) { GC.SuppressFinalize(this); }
        MainForm = mainForm;
    }

    /// <summary>Releases unmanaged context resources.</summary>
    ~ApplicationContext() => Dispose(false);

    /// <summary>Gets or sets the form that controls this context's lifetime.</summary>
    public Form? MainForm
    {
        get => _mainForm;
        set
        {
            if (_mainForm is not null) { _mainForm.HandleDestroyed -= OnMainFormHandleDestroyed; }
            _mainForm = value;
            if (_mainForm is not null) { _mainForm.HandleDestroyed += OnMainFormHandleDestroyed; }
        }
    }

    /// <summary>Gets or sets application-specific data associated with the context.</summary>
    [Localizable(false)]
    [Category("Data")]
    [Bindable(true)]
    [DefaultValue(null)]
    [TypeConverter(typeof(StringConverter))]
    public object? Tag { get; set; }

    /// <summary>Occurs when this context requests that its UI thread exit.</summary>
    public event EventHandler? ThreadExit;

    /// <summary>Requests that the context's associated message loop exit.</summary>
    public void ExitThread() => ExitThreadCore();

    /// <summary>Raises ThreadExit without ending the process or another UI thread.</summary>
    protected virtual void ExitThreadCore() => ThreadExit?.Invoke(this, EventArgs.Empty);

    /// <summary>Exits the context when its main form is closed.</summary>
    protected virtual void OnMainFormClosed(object? sender, EventArgs e) => ExitThreadCore();

    private void OnMainFormHandleDestroyed(object? sender, EventArgs e)
    {
        if (sender is Form { RecreatingHandle: false })
        {
            ((Form)sender).HandleDestroyed -= OnMainFormHandleDestroyed;
            OnMainFormClosed(sender, e);
        }
    }

    /// <summary>Disposes the main form owned by this context.</summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>Releases resources owned by this context.</summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && _mainForm is { } form)
        {
            if (!form.IsDisposed) { form.Dispose(); }
            form.HandleDestroyed -= OnMainFormHandleDestroyed;
            _mainForm = null;
        }
    }
}
