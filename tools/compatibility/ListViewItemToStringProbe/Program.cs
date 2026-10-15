using System.Collections;
using System.Drawing;
using System.Globalization;
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
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        string version = typeof(ListViewItem).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
#if REFERENCE
        if (version.Split('+')[0] != "10.0.11")
            throw new InvalidOperationException($"Expected WinForms 10.0.11; found {version}.");
#endif
        var observations = new List<object>();
        ObserveMetadata(observations);
        string?[] texts = [null, "", "plain", "中文列表项", "emoji 😀", "two\nlines", "two\r\nlines", "\t spaced \t", "{braces}: text", "nul\0tail", "אבג العربية", "e\u0301"];
        foreach (string? text in texts)
        {
            ObserveText("string-constructor", new ListViewItem(text), observations);
            ObserveText("array-constructor", new ListViewItem(new[] { text, "detail" }), observations);
            ObserveText("setter", new ListViewItem { Text = text }, observations);
            ObserveText("derived-inherited", new InheritedItem(text), observations);
        }
        ObserveText("default", new ListViewItem(), observations);
        ObserveText("null-array", new ListViewItem((string?[]?)null), observations);
        ObserveText("empty-array", new ListViewItem(Array.Empty<string>()), observations);
        ObserveChanges(observations);
        ObserveStyles(observations);
        foreach (string shape in new[] { "fresh", "property-cleared", "last-removed", "populated", "duplicates", "removed-first", "replaced-first", "cross-owner", "derived-first" })
        foreach (string consumer in new[] { "public", "interface", "generic" })
            ObserveCollection(shape, consumer, observations);
        ObserveDispatch(observations);
        Console.WriteLine(JsonSerializer.Serialize(new { version, uiCulture = CultureInfo.CurrentUICulture.Name, observations }));
    }

    private static void ObserveMetadata(List<object> observations)
    {
        MethodInfo? method = typeof(ListViewItem).GetMethod(nameof(ToString), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, Type.EmptyTypes);
        observations.Add(new
        {
            scenario = "metadata", declared = method is not null,
            method?.Name, returnType = method?.ReturnType.FullName,
            method?.IsPublic, method?.IsVirtual, method?.IsFinal, method?.IsStatic,
            method?.IsHideBySig, newSlot = method is not null && (method.Attributes & MethodAttributes.NewSlot) != 0,
            baseDeclaringType = method?.GetBaseDefinition().DeclaringType?.FullName,
            parameterCount = method?.GetParameters().Length,
            genericCount = method?.GetGenericArguments().Length,
            attributes = method?.CustomAttributes.Select(attribute => attribute.AttributeType.FullName).OrderBy(name => name).ToArray()
        });
    }

    private static void ObserveText(string scenario, ListViewItem item, List<object> observations)
    {
        var view = new SubItems(item);
        int countBefore = view.Count;
        string text = item.Text;
        observations.Add(new
        {
            scenario, text,
            direct = Capture(() => item.ToString()),
            throughObject = Capture(() => ((object)item).ToString()),
            interpolation = Capture(() => $"{item}"),
            concatenation = Capture(() => string.Concat(item)),
            countBefore, countAfter = view.Count,
            sameText = ReferenceEquals(text, item.Text)
        });
    }

    private static void ObserveChanges(List<object> observations)
    {
        var item = new ListViewItem("original");
        object tag = new PoisonTag();
        string?[] texts = ["original", "changed", null, "", "中文\r\nnew", "original"];
        for (int index = 0; index < texts.Length; index++)
        {
            item.Text = texts[index];
            item.Name = index % 2 == 0 ? "name-" + index : null!;
            item.ToolTipText = index % 2 == 0 ? "tip\0" + index : null!;
            item.Tag = index % 2 == 0 ? tag : null;
            string text = item.Text;
            string name = item.Name;
            string tip = item.ToolTipText;
            observations.Add(new
            {
                scenario = "changes-" + index, text, name, tip,
                first = Capture(() => item.ToString()), second = Capture(() => item.ToString()),
                sameText = ReferenceEquals(text, item.Text), sameName = ReferenceEquals(name, item.Name),
                sameTip = ReferenceEquals(tip, item.ToolTipText), sameTag = ReferenceEquals(index % 2 == 0 ? tag : null, item.Tag)
            });
        }
        item.SubItems[0].Text = "updated through first";
        observations.Add(new { scenario = "updated-through-first", item.Text, result = Capture(() => item.ToString()) });
        item.Name = "different name";
        item.ToolTipText = "different tooltip";
        item.Tag = tag;
        observations.Add(new { scenario = "metadata-only-update", item.Text, result = Capture(() => item.ToString()) });
    }

    private static void ObserveStyles(List<object> observations)
    {
        using var font = new Font("Arial", 11.5f, FontStyle.Bold, GraphicsUnit.Point);
        var item = new ListViewItem("styled") { Name = "name", Tag = new PoisonTag(), ToolTipText = "tip" };
        SubItem first = item.SubItems[0];
        first.ForeColor = Color.Crimson;
        first.BackColor = Color.AliceBlue;
        first.Font = font;
        for (int stage = 0; stage < 4; stage++)
        {
            if (stage == 1) { first.ForeColor = Color.Empty; first.BackColor = Color.Transparent; }
            if (stage == 2) first.ResetStyle();
            if (stage == 3) { item.ForeColor = Color.Green; first.ForeColor = Color.Green; first.BackColor = Color.Yellow; first.Font = font; }
            observations.Add(new
            {
                scenario = "style-" + stage, result = Capture(() => item.ToString()),
                item.Text, item.Name, item.ToolTipText,
                firstFore = first.ForeColor.ToArgb(), firstBack = first.BackColor.ToArgb(),
                borrowedFont = ReferenceEquals(font, first.Font), fontAlive = font.GetHeight(96f)
            });
        }
        using var disposedFont = new Font("Arial", 10f);
        first.Font = disposedFont;
        disposedFont.Dispose();
        observations.Add(new { scenario = "disposed-font-is-not-read", result = Capture(() => item.ToString()), sameFont = ReferenceEquals(disposedFont, first.Font) });
    }

    private static void ObserveCollection(string shape, string consumer, List<object> observations)
    {
        var item = new ListViewItem();
        var first = new SubItems(item);
        var second = new SubItems(item);
        var primary = new SubItem(null, "primary") { Name = "primary" };
        var detail = new SubItem(null, "detail") { Name = "target" };
        if (shape == "property-cleared") item.SubItems.Clear();
        else if (shape == "last-removed") { first.Add(primary); first.RemoveAt(0); }
        else if (shape != "fresh")
        {
            first.Add(primary);
            first.Add(detail);
            first.Add(new SubItem(null, "other") { Name = "other" });
            if (shape == "duplicates") first.Add(primary);
            if (shape == "removed-first") first.RemoveAt(0);
            if (shape == "replaced-first") first[0] = new SubItem(null, "replacement") { Name = "replacement" };
            if (shape == "cross-owner") new SubItems(new ListViewItem()).Add(primary);
            if (shape == "derived-first") { var hidden = new HiddenTextSubItem(); ((SubItem)hidden).Text = "base primary"; first[0] = hidden; }
        }
        int firstKeyBefore = first.IndexOfKey("target");
        int secondKeyBefore = second.IndexOfKey("other");
        if (first.Count > 0) first[0].Name = "target";
        if (first.Count > 1) first[1].Name = "other";
        SubItem[] before = Enumerable.Range(0, first.Count).Select(index => first[index]).ToArray();
        IEnumerator enumerator = consumer switch
        {
            "interface" => ((IEnumerable)first).GetEnumerator(),
            // The compatibility layer preserves this historical generic consumption path.
            // WinForms exposes only the public/non-generic shape; both use the same enumerator.
#if REFERENCE
            "generic" => first.GetEnumerator(),
#else
            "generic" => ((IEnumerable<SubItem>)first).GetEnumerator(),
#endif
            _ => first.GetEnumerator()
        };
        object firstMove = Capture(() => enumerator.MoveNext());
        object firstCurrent = Capture(() => Identity(before, enumerator.Current));
        string textBefore = item.Text;
        object result = Capture(() => item.ToString());
        object secondResult = Capture(() => ((object)item).ToString());
        object currentAfter = Capture(() => Identity(before, enumerator.Current));
        var remaining = new List<object>();
        for (int index = 0; index < before.Length + 1; index++)
        {
            bool? moved = null;
            object move = Capture(() => moved = enumerator.MoveNext());
            remaining.Add(new { move, current = Capture(() => Identity(before, enumerator.Current)) });
            if (moved != true) break;
        }
        object reset = Capture(() => { enumerator.Reset(); return null; });
        object afterReset = Capture(() => enumerator.MoveNext());
        int countAfter = first.Count;
        int firstOtherAfter = first.IndexOfKey("other");
        int secondOtherAfter = second.IndexOfKey("other");
        bool sameItems = before.Length == countAfter && before.Select((entry, index) => ReferenceEquals(entry, first[index])).All(value => value);
        // Explicit restoration after observation distinguishes an empty table from a new one.
        object? restore = null;
        if (countAfter == 0)
        {
            object property = Capture(() => ReferenceEquals(item.SubItems, item.SubItems));
            object oldMove = Capture(() => enumerator.MoveNext());
            object oldReset = Capture(() => { enumerator.Reset(); return enumerator.MoveNext(); });
            restore = new { property, oldMove, oldReset, count = first.Count, secondCount = second.Count };
        }
        observations.Add(new
        {
            scenario = "collection-" + shape + "-" + consumer, textBefore, textAfter = item.Text,
            countBefore = before.Length, countAfter, result, secondResult,
            sameItems, sameViews = first.Count == second.Count && Enumerable.Range(0, first.Count).All(index => ReferenceEquals(first[index], second[index])),
            firstKeyBefore, secondKeyBefore, firstOtherAfter, secondOtherAfter,
            firstMove, firstCurrent, currentAfter, remaining, reset, afterReset, restore,
            hiddenReads = before.OfType<HiddenTextSubItem>().Sum(entry => entry.HiddenReads)
        });
    }

    private static int Identity(SubItem[] items, object? value)
    {
        for (int index = 0; index < items.Length; index++)
            if (ReferenceEquals(items[index], value)) return index;
        return -1;
    }

    private static void ObserveDispatch(List<object> observations)
    {
        var overriding = new OverridingItem();
        observations.Add(new
        {
            scenario = "override-dispatch", direct = Capture(() => overriding.ToString()),
            asBase = Capture(() => ((ListViewItem)overriding).ToString()), asObject = Capture(() => ((object)overriding).ToString()),
            interpolation = Capture(() => $"{overriding}"), concatenation = Capture(() => string.Concat(overriding)),
            baseCall = Capture(() => overriding.CallBase()), overriding.Calls
        });
        var hidden = new HiddenMembersItem();
        ((ListViewItem)hidden).Text = "base text";
        observations.Add(new
        {
            scenario = "hidden-members", direct = Capture(() => hidden.ToString()),
            asBase = Capture(() => ((ListViewItem)hidden).ToString()), hidden.HiddenReads
        });
        var hiddenString = new HiddenStringItem();
        observations.Add(new
        {
            scenario = "hidden-string", direct = Capture(() => hiddenString.ToString()),
            asBase = Capture(() => ((ListViewItem)hiddenString).ToString()), asObject = Capture(() => ((object)hiddenString).ToString()),
            interpolation = Capture(() => $"{hiddenString}"), hiddenString.Calls
        });
        var expected = new InvalidOperationException("original derived failure");
        var throwing = new ThrowingItem(expected);
        observations.Add(new
        {
            scenario = "override-original-exception", asBase = Capture(() => ((ListViewItem)throwing).ToString(), expected),
            asObject = Capture(() => ((object)throwing).ToString(), expected), interpolation = Capture(() => $"{throwing}", expected),
            throwing.Calls
        });
    }

    private static object Capture(Func<object?> action, Exception? expected = null)
    {
        try { return new { exception = (string?)null, sameException = false, value = action() }; }
        catch (Exception exception) { return new { exception = exception.GetType().Name, sameException = ReferenceEquals(expected, exception), value = (object?)null }; }
    }

    private sealed class InheritedItem(string? text) : ListViewItem(text)
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("ToString must not call Equals.");
        public override int GetHashCode() => throw new InvalidOperationException("ToString must not call GetHashCode.");
    }

    private sealed class OverridingItem() : ListViewItem("derived text")
    {
        public int Calls { get; private set; }
        public override string ToString() { Calls++; return "override(" + base.ToString() + ")"; }
        public string CallBase() => base.ToString()!;
    }

    private sealed class ThrowingItem(Exception expected) : ListViewItem("ignored")
    {
        public int Calls { get; private set; }
        public override string ToString() { Calls++; throw expected; }
    }

    private sealed class HiddenMembersItem : ListViewItem
    {
        public int HiddenReads { get; private set; }
        public new string Text { get { HiddenReads++; throw new InvalidOperationException("Hidden Text must not be read."); } }
        public new string Name { get { HiddenReads++; throw new InvalidOperationException("Hidden Name must not be read."); } }
        public new object Tag { get { HiddenReads++; throw new InvalidOperationException("Hidden Tag must not be read."); } }
        public new string ToolTipText { get { HiddenReads++; throw new InvalidOperationException("Hidden tooltip must not be read."); } }
        public new SubItems SubItems { get { HiddenReads++; throw new InvalidOperationException("Hidden collection must not be read."); } }
    }

    private sealed class HiddenStringItem() : ListViewItem("base hidden text")
    {
        public int Calls { get; private set; }
        public new string ToString() { Calls++; return "hidden string"; }
    }

    private sealed class HiddenTextSubItem : SubItem
    {
        public int HiddenReads { get; private set; }
        public new string Text { get { HiddenReads++; throw new InvalidOperationException("Hidden primary Text must not be read."); } }
    }

    private sealed class PoisonTag
    {
        public override string ToString() => throw new InvalidOperationException("ListViewItem.ToString must not format Tag.");
    }
}
