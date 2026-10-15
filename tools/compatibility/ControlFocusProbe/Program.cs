using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    private static readonly List<string> Results = [];

    [STAThread]
    private static void Main()
    {
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
        }
#endif
        MethodInfo focus = typeof(Control).GetMethod(nameof(Control.Focus), Type.EmptyTypes)!;
        Record("focus-signature", focus.ReturnType.Name + ":virtual=" + focus.IsVirtual);
        Record("focus-editor-browsable", focus.GetCustomAttribute<EditorBrowsableAttribute>()?.State);
        RecordProperty("focused", typeof(Control).GetProperty(nameof(Control.Focused))!);
        RecordProperty("contains-focus", typeof(Control).GetProperty(nameof(Control.ContainsFocus))!);
        RecordProperty("can-focus", typeof(Control).GetProperty(nameof(Control.CanFocus))!);

        using (var uncreated = new Control())
        {
            Snapshot("uncreated", uncreated);
            Record("uncreated-focus", uncreated.Focus());
            Snapshot("uncreated-after-focus", uncreated);
            uncreated.Enabled = false;
            Snapshot("uncreated-disabled", uncreated);
            uncreated.Visible = false;
            Snapshot("uncreated-hidden", uncreated);
            uncreated.Dispose();
            Snapshot("uncreated-disposed", uncreated);
            Record("uncreated-disposed-focus", uncreated.Focus());
            Record("uncreated-disposed-no-handle", !uncreated.IsHandleCreated);
        }

        using var form = new Form { Text = "Lumina focus probe", Width = 280, Height = 180 };
        using var first = new Control { Left = 10, Top = 10, Width = 90, Height = 30 };
        using var second = new Control { Left = 120, Top = 10, Width = 90, Height = 30 };
        using var handleless = new HandlelessControl { Visible = false };
        form.Controls.Add(first);
        form.Controls.Add(second);
        form.Controls.Add(handleless);
        Snapshot("attached-before-show", first);
        Record("attached-before-show-focus", first.Focus());
        Record("attached-before-show-no-handle", !first.IsHandleCreated);
        form.Show();
        _ = SetFocus(0);
        Snapshot("created-unfocused-first", first);
        Snapshot("created-unfocused-second", second);
        Snapshot("handleless-with-owner", handleless);
        Record("handleless-with-owner-focus", handleless.Focus());
        Record("handleless-with-owner-no-handle", !handleless.IsHandleCreated);

        Record("first-focus", first.Focus());
        Snapshot("first-focused", first);
        Snapshot("second-unfocused", second);
        Record("first-focus-again", first.Focus());
        Record("first-native-focus", GetFocus() == first.Handle);
        Record("second-focus", second.Focus());
        Snapshot("first-after-transfer", first);
        Snapshot("second-after-transfer", second);
        Record("second-native-focus", GetFocus() == second.Handle);

        _ = SetFocus(first.Handle);
        Record("external-native-focus-first", first.Focused);
        Record("external-native-focus-second", second.Focused);
        _ = SetFocus(0);
        Record("external-native-clear-focused", first.Focused);
        Record("external-native-clear-contains", first.ContainsFocus);

        first.Enabled = false;
        Snapshot("disabled-unfocused", first);
        Record("disabled-unfocused-focus", first.Focus());
        Record("disabled-keeps-native-focus-empty", GetFocus() == 0);
        first.Enabled = true;
        Record("reenabled-focus", first.Focus());
        first.Enabled = false;
        Snapshot("disabled-after-focus", first);
        Record("disabled-after-focus-result", first.Focus());
        first.Enabled = true;
        _ = SetFocus(0);

        first.Visible = false;
        Snapshot("hidden-unfocused", first);
        Record("hidden-unfocused-focus", first.Focus());
        Record("hidden-keeps-native-focus-empty", GetFocus() == 0);
        first.Visible = true;
        Record("reshown-focus", first.Focus());
        first.Visible = false;
        Snapshot("hidden-after-focus", first);
        Record("hidden-after-focus-result", first.Focus());
        first.Visible = true;
        _ = SetFocus(0);

        _ = EnableWindow(first.Handle, false);
        Record("native-disabled-can-focus", first.CanFocus);
        Record("native-disabled-focus", first.Focus());
        _ = EnableWindow(first.Handle, true);
        Record("native-reenabled-can-focus", first.CanFocus);
        _ = ShowWindow(first.Handle, 0);
        Record("native-hidden-can-focus", first.CanFocus);
        Record("native-hidden-focus", first.Focus());
        _ = ShowWindow(first.Handle, 5);
        Record("native-reshown-can-focus", first.CanFocus);

        nint nativeChild = CreateWindowExW(0, "STATIC", "native focus descendant", 0x50000000, 0, 0, 20, 20, first.Handle, 0, 0, 0);
        if (nativeChild == 0) { throw new InvalidOperationException("Could not create the native focus descendant."); }
        try
        {
            _ = SetFocus(nativeChild);
            Record("native-descendant-current", GetFocus() == nativeChild);
            Snapshot("native-descendant-parent", first);
            Record("native-descendant-sibling-contains", second.ContainsFocus);
            Record("native-descendant-parent-focus", first.Focus());
            Record("native-descendant-parent-now-current", GetFocus() == first.Handle);
        }
        finally { _ = DestroyWindow(nativeChild); }

        _ = SetFocus(0);
        _ = EnableWindow(form.Handle, false);
        Record("native-disabled-owner-can-focus", first.CanFocus);
        Record("native-disabled-owner-focus", first.Focus());
        _ = EnableWindow(form.Handle, true);
        _ = SetFocus(0);
        _ = ShowWindow(form.Handle, 0);
        Record("native-hidden-owner-can-focus", first.CanFocus);
        Record("native-hidden-owner-focus", first.Focus());
        _ = ShowWindow(form.Handle, 5);
        _ = SetFocus(0);

        Record("before-dispose-focus", first.Focus());
        nint disposedHandle = first.Handle;
        first.Dispose();
        Record("disposed-native-window-gone", !IsWindow(disposedHandle));
        Snapshot("created-disposed", first);
        Record("created-disposed-focus", first.Focus());
        Record("created-disposed-no-handle", !first.IsHandleCreated);
        form.Dispose();
        Snapshot("owner-disposed-child", second);
        Record("owner-disposed-child-focus", second.Focus());
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static void Snapshot(string name, Control control)
    {
        bool created = control.IsHandleCreated;
        Record(name + "-handle", created);
        Record(name + "-focused", control.Focused);
        Record(name + "-contains", control.ContainsFocus);
        Record(name + "-can-focus", control.CanFocus);
        Record(name + "-queries-preserve-handle", control.IsHandleCreated == created);
    }

    private static void RecordProperty(string name, PropertyInfo property)
    {
        Record(name + "-signature", property.PropertyType.Name + ":virtual=" + property.GetMethod!.IsVirtual + ":setter=" + (property.SetMethod is not null));
        Record(name + "-browsable", property.GetCustomAttribute<BrowsableAttribute>()?.Browsable);
        Record(name + "-editor-browsable", property.GetCustomAttribute<EditorBrowsableAttribute>()?.State);
        Record(name + "-serialization", property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()?.Visibility);
    }

    private static void Record(string name, object? value)
    {
        string result = name + "=" + (value?.ToString() ?? "null");
        Results.Add(result);
        Console.Error.WriteLine(result);
    }

    private sealed class HandlelessControl : Control
    {
#if !REFERENCE
        protected override bool ShouldCreateNativeHandle => false;
#endif
    }

    [DllImport("user32.dll")]
    private static extern nint GetFocus();
    [DllImport("user32.dll")]
    private static extern nint SetFocus(nint handle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnableWindow(nint handle, [MarshalAs(UnmanagedType.Bool)] bool enabled);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow(nint handle, int command);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text, uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
