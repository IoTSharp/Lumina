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
using SubItem = System.Windows.Forms.ListViewItem.ListViewSubItem;
using SubItems = System.Windows.Forms.ListViewItem.ListViewSubItemCollection;
#else
using Lumina.Forms;
using SubItem = Lumina.Forms.ListViewItem.ListViewSubItem;
using SubItems = Lumina.Forms.ListViewItem.ListViewSubItemCollection;
#endif

internal static class Program
{
    private static readonly string[] Shapes = ["fresh", "property-cleared", "external-cleared", "removed-only", "populated", "removed-first", "replaced-first", "duplicates", "cross-owner", "hidden-first"];
    private static readonly string[] Operations = ["read-index", "read-key", "index-minus-one", "index-zero", "index-max", "index-invalid", "key-null", "key-empty", "key-text", "key-repeat", "index-repeat"];

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
        var observations = new List<object>();
        var deferredMetadata = new List<object>();
        ObserveMetadata(observations, deferredMetadata);
        foreach (string construction in new[] { "default", "null-text", "text", "null-array", "empty-array", "array" })
            ObserveDefaults(construction, observations);
        foreach (string initial in new[] { "default", "index", "key", "null-key", "empty-key" })
        foreach (int value in new[] { int.MinValue, -2, -1, 0, 1, 255, 256, int.MaxValue })
            ObserveIndex(initial, value, observations);
        foreach (string initial in new[] { "default", "index", "key", "null-key", "empty-key" })
            ObserveKeySequence(initial, observations);
        using var font = new Font("Arial", 11f, FontStyle.Bold);
        foreach (string shape in Shapes)
        foreach (string operation in Operations)
        foreach (string consumer in new[] { "public", "interface", "generic" })
            ObserveCollection(shape, operation, consumer, font, observations);
        ObserveRelationship(font, observations);
        ObserveHiddenMembers(font, observations);
        ObserveBorrowedLifetime(observations);
        Console.Error.WriteLine(JsonSerializer.Serialize(new { deferredDesignerMetadata = deferredMetadata }));
        Console.WriteLine(JsonSerializer.Serialize(new { version, uiCulture = CultureInfo.CurrentUICulture.Name, observations, deferredMetadata }));
    }

    private static void ObserveMetadata(List<object> observations, List<object> deferredMetadata)
    {
        var nullability = new NullabilityInfoContext();
        foreach (string name in new[] { "ImageIndex", "ImageKey" })
        {
            PropertyInfo? property = typeof(ListViewItem).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            NullabilityInfo? nullable = property is null ? null : nullability.Create(property);
            observations.Add(new
            {
                scenario = "metadata", name, declared = property is not null, type = property?.PropertyType.FullName,
                getter = MethodShape(property?.GetMethod), setter = MethodShape(property?.SetMethod),
                allowNull = property?.GetCustomAttribute<AllowNullAttribute>() is not null,
                readState = nullable?.ReadState.ToString(), writeState = nullable?.WriteState.ToString(),
                attributes = property is null ? [] : property.GetCustomAttributes().Select(AttributeShape).Where(value => value is not null).OrderBy(value => value, StringComparer.Ordinal).ToArray()
            });
            // These contracts need the standard converter/editor dependency work.
            // Record exact names and values rather than inventing a replacement type or
            // treating their omission as metadata parity.
            deferredMetadata.Add(new
            {
                name,
                attributes = property is null ? [] : property.GetCustomAttributesData()
                    .Where(attribute => attribute.AttributeType == typeof(TypeConverterAttribute)
                        || attribute.AttributeType == typeof(EditorAttribute))
                    .Select(attribute => new
                    {
                        type = attribute.AttributeType.FullName,
                        arguments = attribute.ConstructorArguments.Select(argument => argument.Value?.ToString()).ToArray(),
                        namedArguments = attribute.NamedArguments.Select(argument => argument.MemberName + "=" + argument.TypedValue.Value).ToArray()
                    }).ToArray()
            });
        }
    }

    private static object? MethodShape(MethodInfo? method) => method is null ? null : new
    {
        method.IsPublic, method.IsVirtual, method.IsFinal, method.IsStatic,
        returns = method.ReturnType.FullName,
        parameters = method.GetParameters().Select(parameter => new { parameter.Name, type = parameter.ParameterType.FullName }).ToArray()
    };

    private static string? AttributeShape(Attribute attribute) => attribute switch
    {
        BrowsableAttribute value => "Browsable=" + value.Browsable,
        LocalizableAttribute value => "Localizable=" + value.IsLocalizable,
        BindableAttribute value => "Bindable=" + value.Bindable + ";Direction=" + value.Direction,
        DefaultValueAttribute value => "DefaultValue=" + (value.Value is null ? "<null>" : value.Value.GetType().FullName + ":" + value.Value),
        DesignerSerializationVisibilityAttribute value => "DesignerSerializationVisibility=" + value.Visibility,
        CategoryAttribute value => "Category=" + value.Category,
        DescriptionAttribute value => "Description=" + value.Description,
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
        int imageIndex = item.ImageIndex;
        int afterIndex = view.Count;
        string imageKey = item.ImageKey;
        observations.Add(new
        {
            scenario = "defaults", construction, before, afterIndex, afterKey = view.Count,
            imageIndex, imageKey, sameEmpty = ReferenceEquals(string.Empty, imageKey),
            item.Text, item.Name, item.ToolTipText, item.UseItemStyleForSubItems
        });
    }

    private static ListViewItem CreateInitial(string initial)
    {
        var item = new ListViewItem();
        switch (initial)
        {
            case "index": item.ImageIndex = 37; break;
            case "key": item.ImageKey = new string("original-key".ToCharArray()); break;
            case "null-key": item.ImageKey = null; break;
            case "empty-key": item.ImageKey = string.Empty; break;
        }
        return item;
    }

    private static void ObserveIndex(string initial, int value, List<object> observations)
    {
        ListViewItem item = CreateInitial(initial);
        string keyBefore = item.ImageKey;
        int indexBefore = item.ImageIndex;
        var view = new SubItems(item);
        object first = Capture(() => { item.ImageIndex = value; return null; });
        object state = ImageState(item);
        object repeat = Capture(() => { item.ImageIndex = value; return null; });
        observations.Add(new
        {
            scenario = "index", initial, input = value, indexBefore, keyBefore, first, state, repeat,
            repeatedState = ImageState(item), sameKey = ReferenceEquals(keyBefore, item.ImageKey), count = view.Count
        });
    }

    private static void ObserveKeySequence(string initial, List<object> observations)
    {
        ListViewItem item = CreateInitial(initial);
        string sameTextFirst = new string("same key".ToCharArray());
        string sameTextSecond = new string("same key".ToCharArray());
        string?[] values = [null, string.Empty, "plain", "中文 🧪", "line\r\nbreak\n", "\0", "before\0after", sameTextFirst, sameTextFirst, sameTextSecond, null, null, string.Empty];
        for (int index = 0; index < values.Length; index++)
        {
            string? value = values[index];
            item.ImageIndex = 37;
            object first = Capture(() => { item.ImageKey = value; return null; });
            string firstRead = item.ImageKey;
            object firstState = ImageState(item);
            object repeat = Capture(() => { item.ImageKey = value; return null; });
            observations.Add(new
            {
                scenario = "key-sequence", initial, step = index, input = value, first, firstState, repeat,
                state = ImageState(item), sameInput = ReferenceEquals(value, item.ImageKey),
                sameRepeatedRead = ReferenceEquals(firstRead, item.ImageKey),
                sameFirstEqual = ReferenceEquals(sameTextFirst, item.ImageKey), sameSecondEqual = ReferenceEquals(sameTextSecond, item.ImageKey),
                count = new SubItems(item).Count
            });
        }
        item.ImageKey = sameTextFirst;
        item.ImageKey = sameTextSecond;
        observations.Add(new
        {
            scenario = "equal-key-identity", initial, state = ImageState(item),
            sameFirst = ReferenceEquals(sameTextFirst, item.ImageKey), sameSecond = ReferenceEquals(sameTextSecond, item.ImageKey)
        });
        foreach (int value in new[] { -1, 0, 37, int.MaxValue })
        {
            item.ImageKey = sameTextFirst;
            item.ImageIndex = value;
            object afterIndex = ImageState(item);
            item.ImageKey = sameTextFirst;
            object afterKey = ImageState(item);
            item.ImageKey = string.Empty;
            observations.Add(new { scenario = "alternation", initial, input = value, afterIndex, afterKey, afterEmpty = ImageState(item) });
        }
    }

    private static ListViewItem CreateShape(string shape, Font font, out SubItem held)
    {
        var item = new ListViewItem { Tag = new PoisonTag(), ToolTipText = "stable tip", UseItemStyleForSubItems = false };
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
                view.Add(new SubItem(null, "detail", Color.Green, Color.Yellow, font) { Name = "target", Tag = new PoisonTag() });
                view.Add(new SubItem(null, "third") { Name = "other", Tag = new PoisonTag() });
                if (shape == "removed-first") view.RemoveAt(0);
                if (shape == "replaced-first") view[0] = new SubItem(null, "replacement", Color.Navy, Color.Beige, font) { Name = "replacement", Tag = new PoisonTag() };
                if (shape == "duplicates") view.Add(held);
                if (shape == "cross-owner") new SubItems(new ListViewItem()).Add(held);
                if (shape == "hidden-first")
                {
                    var hidden = new HiddenSubItem();
                    ((SubItem)hidden).Text = "base primary";
                    ((SubItem)hidden).Name = "base name";
                    ((SubItem)hidden).Font = font;
                    ((SubItem)hidden).Tag = new PoisonTag();
                    view[0] = hidden;
                }
            }
        }
        return item;
    }

    private static void ObserveCollection(string shape, string operation, string consumer, Font font, List<object> observations)
    {
        ListViewItem item = CreateShape(shape, font, out SubItem held);
        item.ImageKey = "before";
        var first = new SubItems(item);
        var second = new SubItems(item);
        int firstKeyBefore = first.IndexOfKey("target");
        int secondKeyBefore = second.IndexOfKey("other");
        for (int index = 0; index < first.Count; index++) first[index].Name = "target";
        SubItem[] before = Enumerable.Range(0, first.Count).Select(index => first[index]).ToArray();
        object? itemTag = item.Tag;
        object?[] tags = before.Select(entry => entry.Tag).ToArray();
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
        object result = Capture(() => Operate(item, operation));
        int countAfterOperation = first.Count;
        object currentAfter = Capture(() => Identity(before, enumerator.Current));
        object next = Capture(() => enumerator.MoveNext());
        object reset = Capture(() => { enumerator.Reset(); return null; });
        object afterReset = Capture(() => enumerator.MoveNext());
        int firstKeyAfter = first.IndexOfKey("target");
        int secondKeyAfter = second.IndexOfKey("target");
        int coldKeyAfter = new SubItems(item).IndexOfKey("target");
        bool sameItems = before.Length == first.Count && before.Select((entry, index) => ReferenceEquals(entry, first[index])).All(value => value);
        object stable = StableState(item, first, font);
        object? oldEmptyTable = null;
        if (before.Length == 0)
        {
            first.Add("external marker");
            oldEmptyTable = new
            {
                moveAfterAppend = Capture(() => enumerator.MoveNext()),
                resetAfterAppend = Capture(() => { enumerator.Reset(); return enumerator.MoveNext(); }), firstCount = first.Count, secondCount = second.Count
            };
        }
        observations.Add(new
        {
            scenario = "collection", shape, operation, consumer, countBefore = before.Length, countAfterOperation,
            result, image = ImageState(item), stable, sameItems, firstKeyBefore, secondKeyBefore, firstKeyAfter, secondKeyAfter, coldKeyAfter,
            firstMove, firstCurrent, currentAfter, next, reset, afterReset, oldEmptyTable,
            sameItemTag = ReferenceEquals(itemTag, item.Tag), sameSubitemTags = before.Select((entry, index) => ReferenceEquals(tags[index], entry.Tag)).All(value => value),
            held = SubState(held, font), hiddenCalls = before.OfType<HiddenSubItem>().Sum(value => value.Calls), fontAlive = font.GetHeight(96f)
        });
    }

    private static object? Operate(ListViewItem item, string operation)
    {
        switch (operation)
        {
            case "read-index": return item.ImageIndex;
            case "read-key": return item.ImageKey;
            case "index-minus-one": item.ImageIndex = -1; break;
            case "index-zero": item.ImageIndex = 0; break;
            case "index-max": item.ImageIndex = int.MaxValue; break;
            case "index-invalid": item.ImageIndex = -2; break;
            case "key-null": item.ImageKey = null; break;
            case "key-empty": item.ImageKey = string.Empty; break;
            case "key-text": item.ImageKey = "target\0中文"; break;
            case "key-repeat": item.ImageKey = "before"; item.ImageKey = "before"; break;
            case "index-repeat": item.ImageIndex = 23; item.ImageIndex = 23; break;
        }
        return null;
    }

    private static void ObserveRelationship(Font font, List<object> observations)
    {
        var item = new ListViewItem(new[] { "primary", "detail" }) { ImageKey = "retained key", Font = font, Name = "name", Tag = new PoisonTag(), ToolTipText = "tip" };
        var view = new SubItems(item);
        SubItem primary = view[0];
        var other = new ListViewItem { ImageIndex = 17 };
        new SubItems(other).Add(primary);
        Action[] actions = [
            () => item.Text = "changed", () => item.Name = "changed-name", () => item.Tag = new PoisonTag(),
            () => item.ToolTipText = "changed-tip", () => item.BackColor = Color.Red, () => item.ForeColor = Color.Blue,
            () => item.Font = null, () => item.UseItemStyleForSubItems = false, () => primary.ResetStyle(),
            () => view[0] = new SubItem(null, "replacement"), () => view.Clear(), () => _ = item.ToString(),
            () => _ = item.SubItems, () => item.ImageIndex = 19, () => primary.Text = "shared-change",
            () => other.ImageKey = "other-key", () => item.ImageKey = "last"
        ];
        for (int index = 0; index < actions.Length; index++)
        {
            object result = Capture(() => { actions[index](); return null; });
            observations.Add(new
            {
                scenario = "relationship", step = index, result, image = ImageState(item), otherImage = ImageState(other),
                stable = StableState(item, view, font), held = SubState(primary, font), fontAlive = font.GetHeight(96f)
            });
        }
    }

    private static void ObserveHiddenMembers(Font font, List<object> observations)
    {
        var hidden = new HiddenItem();
        ListViewItem item = hidden;
        item.Tag = new PoisonTag();
        item.ToolTipText = "base tip";
        var primary = new HiddenSubItem();
        ((SubItem)primary).Font = font;
        ((SubItem)primary).Tag = new PoisonTag();
        var view = new SubItems(item);
        view.Add(primary);
        foreach (string operation in Operations)
        {
            object result = Capture(() => Operate(item, operation));
            observations.Add(new { scenario = "hidden", operation, result, image = ImageState(item), stable = StableState(item, view, font), itemCalls = hidden.Calls, subitemCalls = primary.Calls });
        }
    }

    private static void ObserveBorrowedLifetime(List<object> observations)
    {
        foreach (string operation in Operations)
        {
            using var font = new Font("Arial", 11f);
            var item = new ListViewItem("primary") { Font = font };
            float before = font.GetHeight(96f);
            object result = Capture(() => Operate(item, operation));
            observations.Add(new { scenario = "borrowed-font", operation, result, before, after = Capture(() => font.GetHeight(96f)), sameFont = ReferenceEquals(font, item.Font) });
        }
        var disposed = new Font("Arial", 11f);
        var owner = new ListViewItem("primary") { Font = disposed };
        disposed.Dispose();
        foreach (string operation in Operations)
        {
            object result = Capture(() => Operate(owner, operation));
            observations.Add(new { scenario = "disposed-borrowed-font", operation, result, image = ImageState(owner), sameFont = ReferenceEquals(disposed, owner.Font) });
        }
    }

    private static object ImageState(ListViewItem item) => new { item.ImageIndex, item.ImageKey };
    private static object StableState(ListViewItem item, SubItems view, Font font) => new
    {
        count = view.Count, item.Text, item.Name, item.ToolTipText, item.UseItemStyleForSubItems,
        back = item.BackColor.ToArgb(), fore = item.ForeColor.ToArgb(), sameFont = ReferenceEquals(font, item.Font), formatted = item.ToString(),
        subitems = Enumerable.Range(0, view.Count).Select(index => SubState(view[index], font)).ToArray()
    };
    private static object SubState(SubItem item, Font font) => new { item.Text, item.Name, back = item.BackColor.ToArgb(), fore = item.ForeColor.ToArgb(), sameFont = ReferenceEquals(font, item.Font) };
    private static int Identity(SubItem[] items, object? value)
    {
        for (int index = 0; index < items.Length; index++) if (ReferenceEquals(items[index], value)) return index;
        return -1;
    }
    private static object Capture(Func<object?> action)
    {
        try { return new { exception = (string?)null, parameter = (string?)null, actualValue = (object?)null, value = action() }; }
        catch (Exception exception)
        {
            return new { exception = exception.GetType().FullName, parameter = (exception as ArgumentException)?.ParamName, actualValue = (exception as ArgumentOutOfRangeException)?.ActualValue, value = (object?)null };
        }
    }

    private sealed class HiddenItem : ListViewItem
    {
        public int Calls { get; private set; }
        private T Fail<T>() { Calls++; throw new InvalidOperationException("Hidden item member"); }
        public new int ImageIndex { get => Fail<int>(); set => Fail<object>(); }
        public new string ImageKey { get => Fail<string>(); set => Fail<object>(); }
        public new string Text { get => Fail<string>(); set => Fail<object>(); }
        public new string Name { get => Fail<string>(); set => Fail<object>(); }
        public new string ToolTipText { get => Fail<string>(); set => Fail<object>(); }
        public new object Tag { get => Fail<object>(); set => Fail<object>(); }
        public new Font Font { get => Fail<Font>(); set => Fail<object>(); }
        public new Color BackColor { get => Fail<Color>(); set => Fail<object>(); }
        public new Color ForeColor { get => Fail<Color>(); set => Fail<object>(); }
        public new bool UseItemStyleForSubItems { get => Fail<bool>(); set => Fail<object>(); }
        public new SubItems SubItems => Fail<SubItems>();
        public new string ToString() => Fail<string>();
    }
    private sealed class HiddenSubItem : SubItem
    {
        public int Calls { get; private set; }
        private T Fail<T>() { Calls++; throw new InvalidOperationException("Hidden subitem member"); }
        public new string Text { get => Fail<string>(); set => Fail<object>(); }
        public new string Name { get => Fail<string>(); set => Fail<object>(); }
        public new object Tag { get => Fail<object>(); set => Fail<object>(); }
        public new Font Font { get => Fail<Font>(); set => Fail<object>(); }
        public new Color BackColor { get => Fail<Color>(); set => Fail<object>(); }
        public new Color ForeColor { get => Fail<Color>(); set => Fail<object>(); }
        public new void ResetStyle() => Fail<object>();
        public new string ToString() => Fail<string>();
    }
    private sealed class PoisonTag
    {
        public override string ToString() => throw new InvalidOperationException("Image selection must not format Tag.");
        public override bool Equals(object? obj) => throw new InvalidOperationException("Image selection must not compare Tag.");
        public override int GetHashCode() => throw new InvalidOperationException("Image selection must not hash Tag.");
    }
}
