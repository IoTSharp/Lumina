using System.Drawing;
using System.Reflection;
using System.Text.Json;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        string version = typeof(ListViewItem).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "";
#if REFERENCE
        if (version.Split('+')[0] != "10.0.11")
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11; found {version}.");
        }
#endif
        using var font = new Font("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
        var observations = new List<object>();
        ObserveSignatures(observations);
        ObserveObjectAdd(observations, font);
        ObserveTextAdd(observations);
        ObserveStyledAdd(observations, font);
        ObserveSharedViews(observations, font);
        ObserveKeyCaches(observations, font);
        ObserveFontLifetime(observations, font);
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations }));
    }

    private static void ObserveSignatures(List<object> observations)
    {
        foreach (Type[] parameterTypes in new[]
        {
            new[] { typeof(ListViewItem.ListViewSubItem) },
            new[] { typeof(string) },
            new[] { typeof(string), typeof(Color), typeof(Color), typeof(Font) }
        })
        {
            MethodInfo? method = typeof(ListViewItem.ListViewSubItemCollection).GetMethod("Add", parameterTypes);
            observations.Add(new
            {
                scenario = "signature-" + string.Join("-", parameterTypes.Select(type => type.Name)),
                exists = method is not null, returnsSubItem = method?.ReturnType == typeof(ListViewItem.ListViewSubItem),
                isPublic = method?.IsPublic, isVirtual = method?.IsVirtual
            });
        }
    }

    private static void ObserveObjectAdd(List<object> observations, Font font)
    {
        foreach (bool initialized in new[] { false, true })
        {
            var owner = initialized ? new ListViewItem("primary") : new ListViewItem();
            var items = new ListViewItem.ListViewSubItemCollection(owner);
            observations.Add(new
            {
                scenario = "object-null-" + initialized, value = Capture(() => items.Add((ListViewItem.ListViewSubItem)null!)),
                items.Count, owner.Text, items = Texts(items)
            });
        }

        var target = new ListViewItem("target primary");
        var source = new ListViewItem(new[] { "source primary", "source detail" });
        var targetItems = target.SubItems;
        var sourceItems = source.SubItems;
        var detached = new DerivedSubItem(null, "detached", Color.Crimson, Color.AliceBlue, font)
        {
            Name = "identity", Tag = new object()
        };
        object tag = detached.Tag;
        var result = targetItems.Add(detached);
        observations.Add(new
        {
            scenario = "object-detached", sameReturned = ReferenceEquals(detached, result),
            sameStored = ReferenceEquals(detached, targetItems[1]), sameTag = ReferenceEquals(tag, result.Tag),
            sameFont = ReferenceEquals(font, result.Font), derived = result is DerivedSubItem,
            result.Text, result.Name, items = Texts(targetItems)
        });
        var repeated = targetItems.Add(detached);
        observations.Add(new
        {
            scenario = "object-repeated", sameReturned = ReferenceEquals(detached, repeated),
            samePositions = ReferenceEquals(targetItems[1], targetItems[2]), items = Texts(targetItems)
        });
        var existingPrimary = targetItems.Add(targetItems[0]);
        observations.Add(new
        {
            scenario = "object-existing-primary", sameReturned = ReferenceEquals(targetItems[0], existingPrimary),
            sameStored = ReferenceEquals(targetItems[0], targetItems[3]), target.Text, items = Texts(targetItems)
        });

        var borrowed = sourceItems[1];
        var borrowedResult = targetItems.Add(borrowed);
        observations.Add(new
        {
            scenario = "object-other-owner", sameReturned = ReferenceEquals(borrowed, borrowedResult),
            sameSource = ReferenceEquals(borrowed, sourceItems[1]), sameTarget = ReferenceEquals(borrowed, targetItems[4]),
            sourceItems = Texts(sourceItems), targetItems = Texts(targetItems), sourceText = source.Text, targetText = target.Text
        });
        borrowed.Text = "changed shared detail";
        observations.Add(new { scenario = "object-other-owner-text", sourceItems = Texts(sourceItems), targetItems = Texts(targetItems) });
        var constructedForOther = new ListViewItem.ListViewSubItem(source, "constructed for source");
        observations.Add(new
        {
            scenario = "object-other-constructor-owner", sameReturned = ReferenceEquals(constructedForOther, targetItems.Add(constructedForOther)),
            sourceItems = Texts(sourceItems), targetItems = Texts(targetItems)
        });
        sourceItems.Clear();
        observations.Add(new
        {
            scenario = "object-other-owner-clear", sourceCount = sourceItems.Count, sourceText = source.Text,
            sameTarget = ReferenceEquals(borrowed, targetItems[4]), targetItems = Texts(targetItems)
        });
    }

    private static void ObserveTextAdd(List<object> observations)
    {
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        foreach (string? text in new[] { null, "", " ", "detail 中文", new string("identity".ToCharArray()) })
        {
            var item = items.Add(text);
            observations.Add(new
            {
                scenario = "text-" + items.Count, sameStored = ReferenceEquals(item, items[items.Count - 1]),
                sameText = ReferenceEquals(text, item.Text), item.Text, item.Name, tagNull = item.Tag is null,
                foreColor = DescribeColor(item.ForeColor), backColor = DescribeColor(item.BackColor),
                sameDefaultFont = ReferenceEquals(new ListViewItem.ListViewSubItem().Font, item.Font),
                ownerText = owner.Text, items = Texts(items)
            });
        }
        var first = items.Add("same text");
        var second = items.Add("same text");
        observations.Add(new { scenario = "text-new-objects", differentObjects = !ReferenceEquals(first, second), firstText = first.Text, secondText = second.Text, items.Count });
    }

    private static void ObserveStyledAdd(List<object> observations, Font font)
    {
        var owner = new ListViewItem("primary");
        var items = owner.SubItems;
        foreach ((string scenario, string? text, Color fore, Color back, Font? suppliedFont) in new[]
        {
            ("null-empty", (string?)null, Color.Empty, Color.Empty, (Font?)null),
            ("custom", "styled detail", Color.Crimson, Color.AliceBlue, font),
            ("transparent", "transparent", Color.Transparent, Color.FromArgb(0, 7, 8, 9), (Font?)null),
            ("alpha", "alpha", Color.FromArgb(123, 11, 22, 33), Color.FromArgb(67, 44, 55, 66), font)
        })
        {
            var item = items.Add(text, fore, back, suppliedFont!);
            observations.Add(new
            {
                scenario = "style-" + scenario, sameStored = ReferenceEquals(item, items[items.Count - 1]),
                sameText = ReferenceEquals(text, item.Text), item.Text, item.Name, tagNull = item.Tag is null,
                foreColor = DescribeColor(item.ForeColor), backColor = DescribeColor(item.BackColor),
                sameSuppliedFont = suppliedFont is not null && ReferenceEquals(suppliedFont, item.Font),
                sameDefaultFont = ReferenceEquals(new ListViewItem.ListViewSubItem().Font, item.Font),
                ownerText = owner.Text, items = Texts(items)
            });
        }
        var first = items.Add("same", Color.Red, Color.Blue, font);
        var second = items.Add("same", Color.Red, Color.Blue, font);
        observations.Add(new
        {
            scenario = "style-new-objects", differentObjects = !ReferenceEquals(first, second),
            sameFont = ReferenceEquals(first.Font, second.Font), items.Count
        });
    }

    private static void ObserveSharedViews(List<object> observations, Font font)
    {
        foreach (string mode in new[] { "object", "text", "style" })
        {
            foreach (string shape in new[] { "fresh", "initialized", "cleared-external", "cleared-property" })
            {
                var owner = shape == "fresh" ? new ListViewItem() : new ListViewItem("primary");
                var first = new ListViewItem.ListViewSubItemCollection(owner);
                var second = new ListViewItem.ListViewSubItemCollection(owner);
                ListViewItem.ListViewSubItemCollection? property = shape == "cleared-property" ? owner.SubItems : null;
                if (shape.StartsWith("cleared-", StringComparison.Ordinal))
                {
                    first.Clear();
                }
                int before = first.Count;
                var added = Add(first, mode, "added primary", font);
                observations.Add(new
                {
                    scenario = "views-" + mode + "-" + shape, before, owner.Text,
                    first = Texts(first), second = Texts(second),
                    sameStored = ReferenceEquals(added, first[first.Count - 1]),
                    sameOtherView = ReferenceEquals(added, second[second.Count - 1])
                });
                property ??= owner.SubItems;
                observations.Add(new
                {
                    scenario = "property-" + mode + "-" + shape, owner.Text,
                    property = Texts(property), sameAdded = ReferenceEquals(added, property[property.Count - 1]),
                    sameFirst = ReferenceEquals(first[0], property[0]), stableProperty = ReferenceEquals(property, owner.SubItems)
                });
                first[0].Text = "changed primary";
                added.Text = "changed added";
                observations.Add(new
                {
                    scenario = "mutate-" + mode + "-" + shape, owner.Text,
                    first = Texts(first), second = Texts(second), property = Texts(property)
                });
            }
        }
    }

    private static void ObserveKeyCaches(List<object> observations, Font font)
    {
        foreach (string mode in new[] { "object", "text", "style" })
        {
            var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
            var property = owner.SubItems;
            var first = new ListViewItem.ListViewSubItemCollection(owner);
            var second = new ListViewItem.ListViewSubItemCollection(owner);
            first[2].Name = "target";
            int cachedFirst = first.IndexOfKey("target");
            first[1].Name = "target";
            int cachedSecond = second.IndexOfKey("target");
            int cachedProperty = property.IndexOfKey("target");
            var added = Add(first, mode, "added", font);
            added.Name = "target";
            observations.Add(new
            {
                scenario = "cache-add-" + mode, cachedFirst, cachedSecond, cachedProperty,
                first = first.IndexOfKey("target"), second = second.IndexOfKey("target"), property = property.IndexOfKey("target"),
                fresh = new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target"), items = Texts(first)
            });
            property.Clear();
            foreach (string text in new[] { "new primary", "new earlier", "new later", "new last" })
            {
                Add(second, mode, text, font).Name = "target";
            }
            observations.Add(new
            {
                scenario = "cache-clear-add-" + mode, owner.Text,
                first = first.IndexOfKey("target"), second = second.IndexOfKey("target"), property = property.IndexOfKey("target"),
                fresh = new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target"), items = Texts(first)
            });
            observations.Add(new
            {
                scenario = "cache-null-then-add-" + mode, firstNull = first.IndexOfKey(null), secondEmpty = second.IndexOfKey(""),
                sameReturned = ReferenceEquals(added = Add(first, mode, "last", font), first[first.Count - 1]),
                first = first.IndexOfKey("target"), second = second.IndexOfKey("target")
            });
        }
    }

    private static void ObserveFontLifetime(List<object> observations, Font font)
    {
        foreach (string operation in new[] { "clear", "remove", "reset", "replace-font", "null-font", "replace-item" })
        {
            var owner = new ListViewItem("primary");
            var first = owner.SubItems.Add("first", Color.Red, Color.Blue, font);
            var second = owner.SubItems.Add("second", Color.Green, Color.Yellow, font);
            using var replacementFont = new Font("Consolas", 10f);
            switch (operation)
            {
                case "clear": owner.SubItems.Clear(); break;
                case "remove": owner.SubItems.Remove(first); break;
                case "reset": first.ResetStyle(); break;
                case "replace-font": first.Font = replacementFont; break;
                case "null-font": first.Font = null; break;
                case "replace-item": owner.SubItems[1] = new ListViewItem.ListViewSubItem(); break;
            }
            observations.Add(new
            {
                scenario = "font-lifetime-" + operation, sameOtherFont = ReferenceEquals(font, second.Font),
                height = font.GetHeight(96f), otherHeight = second.Font.GetHeight(96f)
            });
        }
    }

    private static ListViewItem.ListViewSubItem Add(ListViewItem.ListViewSubItemCollection items, string mode, string text, Font font) => mode switch
    {
        "object" => items.Add(new ListViewItem.ListViewSubItem(null, text)),
        "text" => items.Add(text),
        "style" => items.Add(text, Color.Crimson, Color.AliceBlue, font),
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    private static string[] Texts(ListViewItem.ListViewSubItemCollection items) => Enumerable.Range(0, items.Count).Select(index => items[index].Text).ToArray();

    private static object DescribeColor(Color color) => new { color.IsEmpty, color.IsNamedColor, color.IsKnownColor, color.Name, argb = color.ToArgb() };

    private static object Capture(Func<object?> action)
    {
        try
        {
            _ = action();
            return new { exception = "", parameter = "" };
        }
        catch (Exception exception)
        {
            return new { exception = exception.GetType().Name, parameter = (exception as ArgumentException)?.ParamName ?? "" };
        }
    }

    private sealed class DerivedSubItem(ListViewItem? owner, string? text, Color fore, Color back, Font? font)
        : ListViewItem.ListViewSubItem(owner, text, fore, back, font)
    {
    }
}
