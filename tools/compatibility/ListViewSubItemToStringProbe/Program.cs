using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Text.Json;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif
using SubItem =
#if REFERENCE
    System.Windows.Forms.ListViewItem.ListViewSubItem;
#else
    Lumina.Forms.ListViewItem.ListViewSubItem;
#endif
using SubItems =
#if REFERENCE
    System.Windows.Forms.ListViewItem.ListViewSubItemCollection;
#else
    Lumina.Forms.ListViewItem.ListViewSubItemCollection;
#endif

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        string version = typeof(ListViewItem).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
#if REFERENCE
        if (version.Split('+')[0] != "10.0.11")
            throw new InvalidOperationException($"Expected WinForms 10.0.11; found {version}.");
#endif
        var observations = new List<object>();
        ObserveMetadata(observations);
        string?[] texts = [null, "", "plain", "中文子项", "emoji 😀", "two\nlines", "two\r\nlines", "\t spaced \t", "{braces}: text", "nul\0tail", "אבג العربية", "e\u0301"];
        foreach (string? text in texts)
        {
            ObserveText("null-owner", new SubItem(null, text), observations);
            ObserveText("assigned", new SubItem { Text = text }, observations);
            ObserveText("derived-inherited", new InheritedSubItem(text), observations);
        }
        ObserveText("default", new SubItem(), observations);
        ObserveChanges(observations);
        ObserveStyles(observations);
        foreach (string shape in new[] { "fresh", "property-cleared", "populated", "duplicates", "removed", "cross-owner" })
            ObserveOwner(shape, observations);
        ObserveDispatch(observations);
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations }));
    }

    private static void ObserveMetadata(List<object> observations)
    {
        MethodInfo? method = typeof(SubItem).GetMethod(nameof(ToString), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, Type.EmptyTypes);
        observations.Add(new
        {
            scenario = "metadata", declared = method is not null,
            method?.Name, returnType = method?.ReturnType.FullName,
            method?.IsPublic, method?.IsVirtual, method?.IsFinal, method?.IsStatic,
            method?.IsHideBySig, newSlot = method is not null && (method.Attributes & MethodAttributes.NewSlot) != 0,
            baseDeclaringType = method?.GetBaseDefinition().DeclaringType?.FullName,
            parameterCount = method?.GetParameters().Length,
            genericCount = method?.GetGenericArguments().Length
        });
    }

    private static void ObserveText(string scenario, SubItem item, List<object> observations)
    {
        observations.Add(new
        {
            scenario, item.Text,
            direct = Capture(() => item.ToString()),
            throughObject = Capture(() => ((object)item).ToString()),
            interpolation = Capture(() => $"{item}"),
            concatenation = Capture(() => string.Concat(item))
        });
    }

    private static void ObserveChanges(List<object> observations)
    {
        var item = new SubItem(null, "original");
        object tag = new PoisonTag();
        string?[] texts = ["original", "changed", null, "", "中文\r\nnew", "original"];
        for (int index = 0; index < texts.Length; index++)
        {
            item.Text = texts[index];
            item.Name = index % 2 == 0 ? "name-" + index : null;
            item.Tag = index % 2 == 0 ? tag : null;
            observations.Add(new
            {
                scenario = "changes-" + index, item.Text, item.Name,
                sameTag = ReferenceEquals(tag, item.Tag),
                first = Capture(() => item.ToString()), second = Capture(() => item.ToString()),
                textAfter = item.Text, nameAfter = item.Name,
                sameTagAfter = ReferenceEquals(tag, item.Tag)
            });
        }
    }

    private static void ObserveStyles(List<object> observations)
    {
        using var font = new Font("Arial", 11.5f, FontStyle.Bold, GraphicsUnit.Point);
        var item = new SubItem(null, "styled", Color.Crimson, Color.AliceBlue, font) { Name = "name", Tag = new PoisonTag() };
        object tag = item.Tag;
        for (int stage = 0; stage < 4; stage++)
        {
            if (stage == 1) { item.ForeColor = Color.Empty; item.BackColor = Color.Transparent; }
            if (stage == 2) item.ResetStyle();
            if (stage == 3) { item.ForeColor = Color.Green; item.BackColor = Color.Yellow; item.Font = font; }
            observations.Add(new
            {
                scenario = "style-" + stage, result = Capture(() => item.ToString()),
                item.Text, item.Name, sameTag = ReferenceEquals(tag, item.Tag),
                fore = item.ForeColor.ToArgb(), back = item.BackColor.ToArgb(),
                borrowedFont = ReferenceEquals(font, item.Font), fontAlive = font.GetHeight(96f)
            });
        }
        var disposedFont = new Font("Arial", 10f);
        item.Font = disposedFont;
        disposedFont.Dispose();
        observations.Add(new { scenario = "disposed-font-is-not-read", result = Capture(() => item.ToString()), sameFont = ReferenceEquals(disposedFont, item.Font) });
    }

    private static void ObserveOwner(string shape, List<object> observations)
    {
        var owner = new ListViewItem();
        var first = new SubItems(owner);
        var second = new SubItems(owner);
        var item = new InheritedSubItem("owner detail") { Name = "target", Tag = new PoisonTag() };
        // This constructor associates an owner without inserting or restoring a primary item.
        SubItem detached = new SubItem(owner, "detached");
        if (shape == "property-cleared") owner.SubItems.Clear();
        else if (shape != "fresh")
        {
            first.Add(new SubItem(null, "primary") { Name = "primary" });
            first.Add(item);
            first.Add(new SubItem(null, "other") { Name = "other" });
            if (shape == "duplicates") first.Add(item);
            if (shape == "removed") first.Remove(item);
            if (shape == "cross-owner") new SubItems(new ListViewItem()).Add(item);
        }
        // Cache different matching positions, then introduce a duplicate key before ToString.
        int firstKeyBefore = first.IndexOfKey("target");
        int secondKeyBefore = second.IndexOfKey("other");
        if (first.Count > 0) first[0].Name = "target";
        if (first.Count > 1) first[1].Name = "other";
        SubItem[] beforeItems = Enumerable.Range(0, first.Count).Select(index => first[index]).ToArray();
        int countBefore = first.Count;
        string ownerTextBefore = owner.Text;
        IEnumerator enumerator = first.GetEnumerator();
        var calls = new List<object>
        {
            Capture(() => detached.ToString()), Capture(() => item.ToString()),
            Capture(() => ((object)item).ToString())
        };
        foreach (SubItem member in beforeItems) calls.Add(Capture(() => member.ToString()));
        var enumeration = new List<object>();
        for (int index = 0; index < countBefore + 2; index++)
        {
            bool? moved = null;
            object move = Capture(() => moved = enumerator.MoveNext());
            enumeration.Add(new { move, current = Capture(() => Index(beforeItems, enumerator.Current)) });
            if (moved != true) break;
        }
        object reset = Capture(() => { enumerator.Reset(); return null; });
        observations.Add(new
        {
            scenario = "owner-" + shape, countBefore, countAfter = first.Count, secondCount = second.Count,
            ownerTextBefore, ownerTextAfter = owner.Text,
            sameItems = beforeItems.Length == first.Count && beforeItems.Select((entry, index) => ReferenceEquals(entry, first[index])).All(value => value),
            sameViews = first.Count == second.Count && Enumerable.Range(0, first.Count).All(index => ReferenceEquals(first[index], second[index])),
            firstKeyBefore, secondKeyBefore,
            firstKeyAfter = first.IndexOfKey("target"), secondKeyAfter = second.IndexOfKey("other"),
            calls, enumeration, reset
        });
    }

    private static int Index(SubItem[] items, object? value)
    {
        for (int index = 0; index < items.Length; index++)
            if (ReferenceEquals(items[index], value)) return index;
        return -1;
    }

    private static void ObserveDispatch(List<object> observations)
    {
        var item = new OverridingSubItem("derived text");
        var owner = new ListViewItem();
        var view = new SubItems(owner);
        view.Add(item);
        view.Add(item);
        observations.Add(new
        {
            scenario = "override-dispatch",
            direct = Capture(() => item.ToString()),
            asBase = Capture(() => ((SubItem)item).ToString()),
            asObject = Capture(() => ((object)item).ToString()),
            member = Capture(() => view[0].ToString()),
            duplicate = Capture(() => view[1].ToString()),
            baseCall = Capture(() => item.CallBase()), item.Calls, count = view.Count
        });
        var shadowed = new ShadowedSubItem();
        ((SubItem)shadowed).Text = "base text";
        observations.Add(new
        {
            scenario = "hidden-members-are-not-read",
            result = Capture(() => shadowed.ToString()),
            text = ((SubItem)shadowed).Text, shadowed.HiddenReads
        });
        var expected = new InvalidOperationException("original derived failure");
        var throwing = new ThrowingSubItem(expected);
        observations.Add(new
        {
            scenario = "override-original-exception",
            asBase = Capture(() => ((SubItem)throwing).ToString(), expected),
            asObject = Capture(() => ((object)throwing).ToString(), expected),
            interpolation = Capture(() => $"{throwing}", expected),
            throwing.Calls
        });
    }

    private static object Capture(Func<object?> action, Exception? expected = null)
    {
        try { return new { exception = (string?)null, sameException = false, value = action() }; }
        catch (Exception exception) { return new { exception = exception.GetType().Name, sameException = ReferenceEquals(expected, exception), value = (object?)null }; }
    }

    private class InheritedSubItem(string? text) : SubItem(null, text)
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("ToString must not call Equals.");
        public override int GetHashCode() => throw new InvalidOperationException("ToString must not call GetHashCode.");
    }

    private sealed class OverridingSubItem(string text) : SubItem(null, text)
    {
        public int Calls { get; private set; }
        public override string ToString() { Calls++; return "override(" + base.ToString() + ")"; }
        public string CallBase() => base.ToString()!;
    }

    private sealed class ThrowingSubItem(Exception expected) : SubItem(null, "ignored")
    {
        public int Calls { get; private set; }
        public override string ToString() { Calls++; throw expected; }
    }

    private sealed class ShadowedSubItem : SubItem
    {
        public int HiddenReads { get; private set; }
        public new string Text { get { HiddenReads++; throw new InvalidOperationException("Hidden Text must not be read."); } }
        public new string Name { get { HiddenReads++; throw new InvalidOperationException("Hidden Name must not be read."); } }
        public new object Tag { get { HiddenReads++; throw new InvalidOperationException("Hidden Tag must not be read."); } }
    }

    private sealed class PoisonTag
    {
        public override string ToString() => throw new InvalidOperationException("Subitem ToString must not format Tag.");
    }
}
