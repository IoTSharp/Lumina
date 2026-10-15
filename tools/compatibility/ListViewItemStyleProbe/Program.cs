using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
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
        Console.OutputEncoding = new UTF8Encoding(false);
        string version = typeof(ListViewItem).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
#if REFERENCE
        if (version.Split('+')[0] != "10.0.11")
            throw new InvalidOperationException($"Expected WinForms 10.0.11; found {version}.");
#endif
        using var font = new Font("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
        using var replacementFont = new Font("Consolas", 10f, FontStyle.Underline, GraphicsUnit.Point);
        var observations = new List<object>();
        ObserveMetadata(observations);
        foreach (string construction in new[] { "default", "null-text", "text", "null-array", "empty-array", "array" })
            ObserveDefaults(construction, observations);
        foreach (string shape in new[] { "fresh", "property-cleared", "populated", "replaced-first" })
        foreach (string property in new[] { "BackColor", "ForeColor" })
        foreach (Color value in new[] { Color.Empty, Color.Transparent, Color.Red, SystemColors.Window, SystemColors.WindowText, Color.FromArgb(0, 7, 8, 9), Color.FromArgb(123, 11, 22, 33), Color.FromName("UnrecognizedColor") })
            ObserveColors(shape, property, value, font, observations);
        foreach (string shape in new[] { "fresh", "property-cleared", "populated", "replaced-first" })
            ObserveFonts(shape, font, replacementFont, observations);
        foreach (string shape in Shapes)
        foreach (string operation in Operations)
        foreach (string consumer in new[] { "public", "interface", "generic" })
            ObserveCollection(shape, operation, consumer, font, observations);
        ObserveRelationship(font, observations);
        ObserveUseItemStyle(font, observations);
        ObserveBorrowedLifetime(observations);
        ObserveHiddenMembers(font, observations);
        Console.WriteLine(JsonSerializer.Serialize(new { version, uiCulture = CultureInfo.CurrentUICulture.Name, observations }));
    }

    private static readonly string[] Shapes = ["fresh", "property-cleared", "external-cleared", "removed-only", "populated", "removed-first", "replaced-first", "duplicates", "cross-owner", "hidden-first"];
    private static readonly string[] Operations = ["read-back", "read-fore", "read-font", "read-use", "back-empty", "back-custom", "fore-empty", "fore-custom", "font-null", "font-custom", "use-false", "use-true", "reset-held"];

    private static void ObserveMetadata(List<object> observations)
    {
        foreach (string name in new[] { "BackColor", "ForeColor", "Font", "UseItemStyleForSubItems" })
        {
            PropertyInfo? property = typeof(ListViewItem).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            observations.Add(new
            {
                scenario = "metadata", name, declared = property is not null, type = property?.PropertyType.FullName,
                getter = MethodShape(property?.GetMethod), setter = MethodShape(property?.SetMethod),
                allowNull = property?.GetCustomAttribute<AllowNullAttribute>() is not null,
                attributes = property is null ? [] : property.GetCustomAttributes().Select(AttributeShape).Where(value => value is not null).OrderBy(value => value, StringComparer.Ordinal).ToArray()
            });
        }
    }

    private static object? MethodShape(MethodInfo? method) => method is null ? null : new
    {
        method.IsPublic, method.IsVirtual, method.IsFinal, method.IsStatic,
        parameters = method.GetParameters().Select(parameter => new { parameter.Name, type = parameter.ParameterType.FullName }).ToArray()
    };

    private static string? AttributeShape(Attribute attribute) => attribute switch
    {
        BrowsableAttribute value => "Browsable=" + value.Browsable,
        LocalizableAttribute value => "Localizable=" + value.IsLocalizable,
        BindableAttribute value => "Bindable=" + value.Bindable + ";Direction=" + value.Direction,
        DefaultValueAttribute value => "DefaultValue=" + (value.Value is null ? "<null>" : value.Value.GetType().FullName + ":" + value.Value),
        DesignerSerializationVisibilityAttribute value => "DesignerSerializationVisibility=" + value.Visibility,
        TypeConverterAttribute value => "TypeConverter=" + value.ConverterTypeName,
        CategoryAttribute value => "Category=" + value.Category,
        RefreshPropertiesAttribute value => "RefreshProperties=" + value.RefreshProperties,
        _ => null
    };

    private static ListViewItem Create(string construction) => construction switch
    {
        "null-text" => new ListViewItem((string?)null),
        "text" => new ListViewItem("primary"),
        "null-array" => new ListViewItem((string?[]?)null),
        "empty-array" => new ListViewItem(Array.Empty<string?>()),
        "array" => new ListViewItem(new[] { "primary", "detail" }),
        _ => new ListViewItem()
    };

    private static void ObserveDefaults(string construction, List<object> observations)
    {
        ListViewItem item = Create(construction);
        var view = new SubItems(item);
        int before = view.Count;
        Color back = item.BackColor;
        int afterBack = view.Count;
        Color fore = item.ForeColor;
        int afterFore = view.Count;
        Font font = item.Font;
        int afterFont = view.Count;
        bool use = item.UseItemStyleForSubItems;
        int afterUse = view.Count;
        var standalone = new SubItem();
        observations.Add(new
        {
            scenario = "defaults-" + construction, before, afterBack, afterFore, afterFont, afterUse,
            back = DescribeColor(back), fore = DescribeColor(fore), font = DescribeFont(font), use,
            sameReadFont = ReferenceEquals(font, item.Font), sameSubitemDefault = ReferenceEquals(font, standalone.Font),
            samePrimaryFont = view.Count > 0 && ReferenceEquals(font, view[0].Font),
            sameOtherItemFont = ReferenceEquals(font, new ListViewItem().Font), item.Text, item.Name, item.ToolTipText
        });
    }

    private static ListViewItem CreateShape(string shape, Font font, out SubItem held)
    {
        var item = new ListViewItem { Tag = new PoisonTag(), ToolTipText = "stable tip" };
        var view = new SubItems(item);
        held = new SubItem(null, "primary", Color.Crimson, Color.AliceBlue, font) { Name = "primary", Tag = new PoisonTag() };
        if (shape == "property-cleared") item.SubItems.Clear();
        else if (shape != "fresh")
        {
            view.Add(held);
            if (shape == "removed-only") view.RemoveAt(0);
            else if (shape == "external-cleared") view.Clear();
            else
            {
                view.Add(new SubItem(null, "detail", Color.Green, Color.Yellow, null) { Name = "target", Tag = new PoisonTag() });
                view.Add(new SubItem(null, "third") { Name = "other", Tag = new PoisonTag() });
                if (shape == "removed-first") view.RemoveAt(0);
                if (shape == "replaced-first") view[0] = new SubItem(null, "replacement", Color.Navy, Color.Beige, null) { Name = "replacement", Tag = new PoisonTag() };
                if (shape == "duplicates") view.Add(held);
                if (shape == "cross-owner") new SubItems(new ListViewItem()).Add(held);
                if (shape == "hidden-first")
                {
                    var hidden = new HiddenStyleSubItem();
                    ((SubItem)hidden).Text = "base primary";
                    ((SubItem)hidden).ForeColor = Color.Orange;
                    ((SubItem)hidden).BackColor = Color.LightBlue;
                    ((SubItem)hidden).Font = font;
                    view[0] = hidden;
                }
            }
        }
        return item;
    }

    private static void ObserveColors(string shape, string property, Color value, Font font, List<object> observations)
    {
        ListViewItem item = CreateShape(shape, font, out SubItem held);
        var view = new SubItems(item);
        SubItem? beforePrimary = view.Count > 0 ? view[0] : null;
        int before = view.Count;
        object set = Capture(() => { SetColor(item, property, value); return null; });
        object afterFirst = ReadStyle(item, view, font);
        object repeat = Capture(() => { SetColor(item, property, value); return null; });
        observations.Add(new
        {
            scenario = "colors", shape, property, input = DescribeColor(value), before, set, afterFirst, repeat,
            afterRepeat = ReadStyle(item, view, font), primaryRetained = ReferenceEquals(beforePrimary, view.Count > 0 ? view[0] : null),
            heldStyle = ReadSubStyle(held, font), suppliedFontAlive = font.GetHeight(96f)
        });
    }

    private static void SetColor(ListViewItem item, string property, Color value)
    {
        if (property == "BackColor") item.BackColor = value;
        else item.ForeColor = value;
    }

    private static void ObserveFonts(string shape, Font font, Font replacement, List<object> observations)
    {
        ListViewItem item = CreateShape(shape, font, out _);
        var view = new SubItems(item);
        Font defaultFont = new SubItem().Font;
        Font?[] fonts = [null, font, font, replacement, null, null, font];
        for (int index = 0; index < fonts.Length; index++)
        {
            int before = view.Count;
            Font? assigned = fonts[index];
            object set = Capture(() => { item.Font = assigned; return null; });
            observations.Add(new
            {
                scenario = "font-sequence", shape, index, before, set, after = ReadStyle(item, view, font),
                sameAssigned = ReferenceEquals(assigned, item.Font), sameDefault = ReferenceEquals(defaultFont, item.Font),
                sameReplacement = ReferenceEquals(replacement, item.Font), originalFontAlive = font.GetHeight(96f)
            });
        }
    }

    private static void ObserveCollection(string shape, string operation, string consumer, Font font, List<object> observations)
    {
        ListViewItem item = CreateShape(shape, font, out SubItem held);
        var first = new SubItems(item);
        var second = new SubItems(item);
        int firstKeyBefore = first.IndexOfKey("target");
        int secondKeyBefore = second.IndexOfKey("other");
        for (int index = 0; index < first.Count; index++) first[index].Name = "target";
        SubItem[] before = Enumerable.Range(0, first.Count).Select(index => first[index]).ToArray();
        IEnumerator enumerator = consumer switch
        {
            "interface" => ((IEnumerable)first).GetEnumerator(),
#if REFERENCE
            "generic" => first.GetEnumerator(),
#else
            "generic" => ((IEnumerable<SubItem>)first).GetEnumerator(),
#endif
            _ => first.GetEnumerator()
        };
        object firstMove = Capture(() => enumerator.MoveNext());
        object firstCurrent = Capture(() => Identity(before, enumerator.Current));
        object result = Capture(() => Operate(item, held, operation, font));
        int countAfterOperation = first.Count;
        object currentAfter = Capture(() => Identity(before, enumerator.Current));
        object next = Capture(() => enumerator.MoveNext());
        object reset = Capture(() => { enumerator.Reset(); return null; });
        object afterReset = Capture(() => enumerator.MoveNext());
        int firstKeyAfter = first.IndexOfKey("target");
        int secondKeyAfter = second.IndexOfKey("target");
        var cold = new SubItems(item);
        int coldKeyAfter = cold.IndexOfKey("target");
        bool sameItems = before.Length == first.Count && before.Select((entry, index) => ReferenceEquals(entry, first[index])).All(value => value);
        // Reading the item style here is itself under observation. Counts are recorded first.
        object style = ReadStyle(item, first, font);
        object? oldEmptyTable = null;
        if (before.Length == 0)
        {
            first.Add("external marker");
            oldEmptyTable = new
            {
                moveAfterAppend = Capture(() => enumerator.MoveNext()),
                resetAfterAppend = Capture(() => { enumerator.Reset(); return enumerator.MoveNext(); }),
                firstCount = first.Count, secondCount = second.Count
            };
        }
        observations.Add(new
        {
            scenario = "collection", shape, operation, consumer, countBefore = before.Length, countAfterOperation,
            result, style, sameItems, firstKeyBefore, secondKeyBefore, firstKeyAfter, secondKeyAfter, coldKeyAfter,
            firstMove, firstCurrent, currentAfter, next, reset, afterReset, oldEmptyTable,
            hiddenReads = before.OfType<HiddenStyleSubItem>().Sum(value => value.HiddenCalls),
            sameViews = first.Count == second.Count && Enumerable.Range(0, first.Count).All(index => ReferenceEquals(first[index], second[index]))
        });
    }

    private static object? Operate(ListViewItem item, SubItem held, string operation, Font font)
    {
        switch (operation)
        {
            case "read-back": return DescribeColor(item.BackColor);
            case "read-fore": return DescribeColor(item.ForeColor);
            case "read-font": return DescribeFont(item.Font);
            case "read-use": return item.UseItemStyleForSubItems;
            case "back-empty": item.BackColor = Color.Empty; break;
            case "back-custom": item.BackColor = Color.FromArgb(0, 7, 8, 9); break;
            case "fore-empty": item.ForeColor = Color.Empty; break;
            case "fore-custom": item.ForeColor = Color.FromArgb(123, 11, 22, 33); break;
            case "font-null": item.Font = null; break;
            case "font-custom": item.Font = font; break;
            case "use-false": item.UseItemStyleForSubItems = false; break;
            case "use-true": item.UseItemStyleForSubItems = true; break;
            case "reset-held": held.ResetStyle(); break;
        }
        return null;
    }

    private static void ObserveRelationship(Font font, List<object> observations)
    {
        var item = new ListViewItem(new[] { "primary", "detail" }) { Name = "stable name", Tag = new PoisonTag(), ToolTipText = "stable tip" };
        var first = new SubItems(item);
        SubItem primary = first[0];
        SubItem detail = first[1];
        object itemTag = item.Tag;
        object subTag = new PoisonTag();
        primary.Tag = subTag;
        Action[] actions =
        [
            () => { },
            () => item.BackColor = Color.AliceBlue,
            () => item.ForeColor = Color.Crimson,
            () => item.Font = font,
            () => { primary.BackColor = Color.Beige; primary.ForeColor = Color.Navy; primary.Font = null; },
            () => { detail.BackColor = Color.Yellow; detail.ForeColor = Color.Green; detail.Font = font; },
            () => item.UseItemStyleForSubItems = false,
            () => item.UseItemStyleForSubItems = true,
            () => primary.ResetStyle(),
            () => first[0] = detail,
            () => first.RemoveAt(0),
            () => first.Clear(),
            () => item.UseItemStyleForSubItems = false,
            () => { item.ForeColor = Color.Empty; item.BackColor = Color.Empty; item.Font = null; },
            () => first[0] = primary
        ];
        for (int index = 0; index < actions.Length; index++)
        {
            object action = Capture(() => { actions[index](); return null; });
            observations.Add(new
            {
                scenario = "relationship", index, action, after = ReadStyle(item, first, font),
                originalPrimary = ReadSubStyle(primary, font), originalDetail = ReadSubStyle(detail, font),
                item.Name, item.ToolTipText, sameItemTag = ReferenceEquals(itemTag, item.Tag), sameSubTag = ReferenceEquals(subTag, primary.Tag),
                sameDefaultFont = ReferenceEquals(new SubItem().Font, item.Font), fontAlive = font.GetHeight(96f)
            });
        }
    }

    private static void ObserveUseItemStyle(Font font, List<object> observations)
    {
        foreach (bool populated in new[] { false, true })
        {
            var item = populated ? new ListViewItem(new[] { "primary", "detail", "third" }) : new ListViewItem();
            var view = new SubItems(item);
            int before = view.Count;
            item.UseItemStyleForSubItems = false;
            observations.Add(new { scenario = "use-empty-storage", populated, before, after = view.Count, item.UseItemStyleForSubItems });
            item.BackColor = Color.AliceBlue;
            item.ForeColor = Color.Crimson;
            item.Font = font;
            var member = new SubItem(item, "member");
            var outside = new SubItem(item, "outside");
            view.Add(member);
            foreach (bool value in new[] { false, false, true, true, false, true })
            {
                item.UseItemStyleForSubItems = value;
                observations.Add(new
                {
                    scenario = "use-consumption", populated, input = value, after = ReadStyle(item, view, font),
                    member = ReadSubStyle(member, font), outside = ReadSubStyle(outside, font),
                    first = ReadSubStyle(view[0], font), sameFont = ReferenceEquals(font, member.Font)
                });
            }
        }
    }

    private static void ObserveBorrowedLifetime(List<object> observations)
    {
        foreach (string operation in new[] { "null", "replace", "reset-primary", "clear", "remove-primary", "replace-primary" })
        {
            using var shared = new Font("Arial", 11f);
            using var replacement = new Font("Consolas", 9f);
            var item = new ListViewItem("primary") { Font = shared };
            var view = new SubItems(item);
            SubItem primary = view[0];
            var other = new SubItem(null, "other", Color.Empty, Color.Empty, shared);
            float heightBefore = shared.GetHeight(96f);
            switch (operation)
            {
                case "null": item.Font = null; break;
                case "replace": item.Font = replacement; break;
                case "reset-primary": primary.ResetStyle(); break;
                case "clear": view.Clear(); break;
                case "remove-primary": view.RemoveAt(0); break;
                case "replace-primary": view[0] = new SubItem(null, "replacement"); break;
            }
            observations.Add(new
            {
                scenario = "borrowed-lifetime", operation, heightBefore,
                heightAfter = Capture(() => shared.GetHeight(96f)), otherHeight = Capture(() => other.Font.GetHeight(96f)),
                sameOtherFont = ReferenceEquals(shared, other.Font), sameHeldFont = ReferenceEquals(shared, primary.Font),
                sameItemFont = ReferenceEquals(shared, item.Font), replacementItemFont = ReferenceEquals(replacement, item.Font), count = view.Count
            });
        }
        var disposed = new Font("Arial", 11f);
        var owner = new ListViewItem("primary");
        owner.Font = disposed;
        SubItem first = owner.SubItems[0];
        disposed.Dispose();
        object read = Capture(() => ReferenceEquals(disposed, owner.Font));
        object reset = Capture(() => { first.ResetStyle(); return null; });
        object assignDisposed = Capture(() => { owner.Font = disposed; return null; });
        object sameDisposed = Capture(() => ReferenceEquals(disposed, owner.Font));
        object clear = Capture(() => { owner.Font = null; return null; });
        observations.Add(new { scenario = "disposed-borrowed-font", read, reset, assignDisposed, sameDisposed, clear, defaultRestored = ReferenceEquals(new SubItem().Font, owner.Font) });
    }

    private static void ObserveHiddenMembers(Font font, List<object> observations)
    {
        var hidden = new HiddenStyleItem();
        ListViewItem item = hidden;
        item.Tag = new PoisonTag();
        var view = new SubItems(item);
        var primary = new HiddenStyleSubItem();
        ((SubItem)primary).Tag = new PoisonTag();
        view.Add(primary);
        foreach (string operation in Operations)
        {
            object result = Capture(() => Operate(item, primary, operation, font));
            observations.Add(new { scenario = "hidden-members", operation, result, after = ReadStyle(item, view, font), itemCalls = hidden.HiddenCalls, subitemCalls = primary.HiddenCalls });
        }
    }

    private static object ReadStyle(ListViewItem item, SubItems view, Font font) => new
    {
        count = view.Count, item.Text, item.Name, item.ToolTipText,
        back = DescribeColor(item.BackColor), fore = DescribeColor(item.ForeColor), font = DescribeFont(item.Font),
        sameSuppliedFont = ReferenceEquals(font, item.Font), item.UseItemStyleForSubItems,
        primary = view.Count == 0 ? null : ReadSubStyle(view[0], font),
        subitems = Enumerable.Range(0, view.Count).Select(index => ReadSubStyle(view[index], font)).ToArray()
    };

    private static object ReadSubStyle(SubItem item, Font font) => new
    {
        item.Text, item.Name, back = DescribeColor(item.BackColor), fore = DescribeColor(item.ForeColor),
        font = DescribeFont(item.Font), sameSuppliedFont = ReferenceEquals(font, item.Font)
    };

    private static object DescribeColor(Color color) => new { color.IsEmpty, color.IsNamedColor, color.IsKnownColor, color.Name, argb = color.ToArgb() };
    private static object DescribeFont(Font font) => new { font.Name, font.Size, style = (int)font.Style, unit = (int)font.Unit, font.GdiCharSet, font.GdiVerticalFont };
    private static int Identity(SubItem[] items, object? value)
    {
        for (int index = 0; index < items.Length; index++) if (ReferenceEquals(items[index], value)) return index;
        return -1;
    }

    private static object Capture(Func<object?> action)
    {
        try { return new { exception = (string?)null, value = action() }; }
        catch (Exception exception) { return new { exception = exception.GetType().Name, value = (object?)null }; }
    }

    private sealed class HiddenStyleItem : ListViewItem
    {
        public int HiddenCalls { get; private set; }
        public new Color BackColor { get { HiddenCalls++; throw new InvalidOperationException("Hidden BackColor"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden BackColor"); } }
        public new Color ForeColor { get { HiddenCalls++; throw new InvalidOperationException("Hidden ForeColor"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden ForeColor"); } }
        public new Font Font { get { HiddenCalls++; throw new InvalidOperationException("Hidden Font"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden Font"); } }
        public new bool UseItemStyleForSubItems { get { HiddenCalls++; throw new InvalidOperationException("Hidden UseItemStyleForSubItems"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden UseItemStyleForSubItems"); } }
        public new SubItems SubItems { get { HiddenCalls++; throw new InvalidOperationException("Hidden SubItems"); } }
        public new object Tag { get { HiddenCalls++; throw new InvalidOperationException("Hidden Tag"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden Tag"); } }
    }

    private sealed class HiddenStyleSubItem : SubItem
    {
        public int HiddenCalls { get; private set; }
        public new Color BackColor { get { HiddenCalls++; throw new InvalidOperationException("Hidden subitem BackColor"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden subitem BackColor"); } }
        public new Color ForeColor { get { HiddenCalls++; throw new InvalidOperationException("Hidden subitem ForeColor"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden subitem ForeColor"); } }
        public new Font Font { get { HiddenCalls++; throw new InvalidOperationException("Hidden subitem Font"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden subitem Font"); } }
        public new object Tag { get { HiddenCalls++; throw new InvalidOperationException("Hidden subitem Tag"); } set { HiddenCalls++; throw new InvalidOperationException("Hidden subitem Tag"); } }
        public new void ResetStyle() { HiddenCalls++; throw new InvalidOperationException("Hidden ResetStyle"); }
    }

    private sealed class PoisonTag
    {
        public override string ToString() => throw new InvalidOperationException("Style operations must not format Tag.");
        public override bool Equals(object? obj) => throw new InvalidOperationException("Style operations must not compare Tag.");
        public override int GetHashCode() => throw new InvalidOperationException("Style operations must not hash Tag.");
    }
}
