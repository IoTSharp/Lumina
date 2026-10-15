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
        ObserveValidationAndEmptyViews(observations, font);
        ObserveObjectIdentity(observations, font);
        ObserveEmptyTextObjects(observations);
        ObserveTextAndStyle(observations, font);
        ObserveSharedViews(observations, font);
        ObserveKeyCaches(observations, font);
        ObserveFontLifetime(observations, font);
        Console.WriteLine(JsonSerializer.Serialize(new { version, observations }));
    }

    private static void ObserveSignatures(List<object> observations)
    {
        foreach (Type[] parameters in new[]
        {
            new[] { typeof(ListViewItem.ListViewSubItem[]) },
            new[] { typeof(string[]) },
            new[] { typeof(string[]), typeof(Color), typeof(Color), typeof(Font) }
        })
        {
            MethodInfo? method = typeof(ListViewItem.ListViewSubItemCollection).GetMethod("AddRange", parameters);
            observations.Add(new
            {
                scenario = "signature-" + string.Join("-", parameters.Select(type => type.Name)),
                exists = method is not null, returnsVoid = method?.ReturnType == typeof(void),
                isPublic = method?.IsPublic, isVirtual = method?.IsVirtual,
                parameterNames = method?.GetParameters().Select(parameter => parameter.Name).ToArray()
            });
        }
    }

    private static void ObserveValidationAndEmptyViews(List<object> observations, Font font)
    {
        foreach (string mode in new[] { "object", "text", "style" })
        {
            foreach (string shape in new[] { "fresh", "initialized", "cleared-external", "cleared-property" })
            {
                foreach (string payload in new[] { "null-array", "empty", "only-null", "null-first", "null-middle", "null-last", "all-nulls", "valid" })
                {
                    var owner = shape == "fresh" ? new ListViewItem() : new ListViewItem("original primary");
                    var first = new ListViewItem.ListViewSubItemCollection(owner);
                    var second = new ListViewItem.ListViewSubItemCollection(owner);
                    ListViewItem.ListViewSubItemCollection? property = shape == "cleared-property" ? owner.SubItems : null;
                    if (shape.StartsWith("cleared-", StringComparison.Ordinal))
                    {
                        first.Clear();
                    }

                    string?[]? texts = payload switch
                    {
                        "null-array" => null,
                        "empty" => [],
                        "only-null" => [null],
                        "null-first" => [null, "first", "second"],
                        "null-middle" => ["first", null, "second"],
                        "null-last" => ["first", "second", null],
                        "all-nulls" => [null, null, null],
                        _ => ["first", "second"]
                    };
                    int before = first.Count;
                    object result = Capture(() => AddRange(first, mode, texts, font));
                    string[] after = Texts(first);
                    int afterCount = first.Count;
                    string ownerText = owner.Text;
                    bool shared = first.Count == second.Count && Enumerable.Range(0, first.Count).All(index => ReferenceEquals(first[index], second[index]));
                    var recovery = first.Add("recovery");
                    observations.Add(new
                    {
                        scenario = "validation-" + mode + "-" + shape + "-" + payload,
                        before, result, afterCount, after, ownerText, shared,
                        afterRecovery = Texts(second), recoveryStored = ReferenceEquals(recovery, second[second.Count - 1]),
                        recoveryOwnerText = owner.Text, propertyWasAccessed = property is not null
                    });
                }
            }
        }
    }

    private static void ObserveObjectIdentity(List<object> observations, Font font)
    {
        var source = new ListViewItem(new[] { "source primary", "source detail" });
        var target = new ListViewItem("target primary");
        var sourceItems = source.SubItems;
        var items = target.SubItems;
        var primary = items[0];
        object tag = new();
        string text = new string("identity text".ToCharArray());
        var detached = new DerivedSubItem(null, text, Color.Crimson, Color.AliceBlue, font) { Name = "key", Tag = tag };
        var borrowed = sourceItems[1];
        var constructed = new ListViewItem.ListViewSubItem(source, "constructed for source");
        ListViewItem.ListViewSubItem[] input = [detached, borrowed, detached, primary, constructed];
        object result = Capture(() => items.AddRange(input));
        observations.Add(new
        {
            scenario = "object-identities", result, items = Texts(items), source = Texts(sourceItems),
            target.Text, sameObjects = Enumerable.Range(0, input.Length).Select(index => ReferenceEquals(input[index], items[index + 1])).ToArray(),
            sameText = ReferenceEquals(text, items[1].Text), sameTag = ReferenceEquals(tag, items[1].Tag),
            sameFont = ReferenceEquals(font, items[1].Font), derived = items[1] is DerivedSubItem,
            repeated = ReferenceEquals(items[1], items[3]), repeatedPrimary = ReferenceEquals(items[0], items[4]),
            sameSource = ReferenceEquals(borrowed, sourceItems[1]), constructedAbsentFromSource = !sourceItems.Contains(constructed)
        });
        input[0] = new ListViewItem.ListViewSubItem(null, "array replacement");
        borrowed.Text = "shared detail changed";
        detached.Text = "identity changed";
        observations.Add(new
        {
            scenario = "object-input-copy-and-shared-mutation", items = Texts(items), source = Texts(sourceItems),
            copiedInputSlots = ReferenceEquals(detached, items[1]), repeated = ReferenceEquals(items[1], items[3])
        });
        sourceItems.Clear();
        observations.Add(new { scenario = "object-source-clear", sourceCount = sourceItems.Count, targetItems = Texts(items), retainedBorrowed = ReferenceEquals(borrowed, items[2]) });
        items.AddRange([items[1], items[0], items[1]]);
        observations.Add(new { scenario = "object-existing-repeated-range", items = Texts(items), sameFirst = ReferenceEquals(items[1], items[6]), samePrimary = ReferenceEquals(items[0], items[7]), sameLast = ReferenceEquals(items[1], items[8]) });
    }

    private static void ObserveTextAndStyle(List<object> observations, Font font)
    {
        foreach (string mode in new[] { "text", "style" })
        {
            foreach ((string scenario, Color fore, Color back, Font? suppliedFont) in new[]
            {
                ("empty", Color.Empty, Color.Empty, (Font?)null),
                ("custom", Color.Crimson, Color.AliceBlue, font),
                ("transparent", Color.Transparent, Color.FromArgb(0, 7, 8, 9), (Font?)null),
                ("alpha", Color.FromArgb(123, 11, 22, 33), Color.FromArgb(67, 44, 55, 66), font)
            })
            {
                var owner = new ListViewItem("primary");
                var items = owner.SubItems;
                string identity = new string("identity text".ToCharArray());
                string?[] input = [null, "", " ", "detail 中文", identity, identity];
                if (mode == "style")
                {
                    items.AddRange(input!, fore, back, suppliedFont!);
                }
                else
                {
                    items.AddRange(input!);
                }

                string[] beforeMutation = Texts(items);
                object[] styles = Enumerable.Range(1, items.Count - 1).Select(index =>
                {
                    var item = items[index];
                    return (object)new
                    {
                        item.Text, item.Name, tagNull = item.Tag is null,
                        foreColor = DescribeColor(item.ForeColor), backColor = DescribeColor(item.BackColor),
                        sameSuppliedFont = suppliedFont is not null && ReferenceEquals(suppliedFont, item.Font),
                        sameDefaultFont = ReferenceEquals(new ListViewItem.ListViewSubItem().Font, item.Font),
                        sameIdentityText = ReferenceEquals(identity, item.Text)
                    };
                }).ToArray();
                int originalCount = items.Count;
                bool differentObjects = Enumerable.Range(1, items.Count - 1).All(index => Enumerable.Range(index + 1, items.Count - index - 1).All(other => !ReferenceEquals(items[index], items[other])));
                input[4] = "array replacement";
                items[items.Count - 1].Text = "last changed";
                observations.Add(new
                {
                    scenario = "text-style-" + mode + "-" + scenario, beforeMutation, originalCount, styles, differentObjects,
                    afterMutation = Texts(items), owner.Text
                });
            }
        }
    }

    private static void ObserveEmptyTextObjects(List<object> observations)
    {
        var owner = new ListViewItem();
        var items = new ListViewItem.ListViewSubItemCollection(owner);
        var nullText = new ListViewItem.ListViewSubItem(null, null);
        var emptyText = new ListViewItem.ListViewSubItem(null, "");
        items.AddRange([null!, nullText, emptyText, null!]);
        int beforeProperty = items.Count;
        observations.Add(new
        {
            scenario = "object-empty-text-is-retained", beforeProperty, items = Texts(items), owner.Text,
            sameNullText = ReferenceEquals(nullText, items[0]), sameEmptyText = ReferenceEquals(emptyText, items[1]),
            propertyCount = owner.SubItems.Count, propertyFirst = ReferenceEquals(nullText, owner.SubItems[0])
        });
    }

    private static void ObserveSharedViews(List<object> observations, Font font)
    {
        foreach (string mode in new[] { "object", "text", "style" })
        {
            foreach (string shape in new[] { "fresh", "initialized", "cleared-external", "cleared-property" })
            {
                var owner = shape == "fresh" ? new ListViewItem() : new ListViewItem("original primary");
                var first = new ListViewItem.ListViewSubItemCollection(owner);
                var second = new ListViewItem.ListViewSubItemCollection(owner);
                ListViewItem.ListViewSubItemCollection? property = shape == "cleared-property" ? owner.SubItems : null;
                if (shape.StartsWith("cleared-", StringComparison.Ordinal))
                {
                    first.Clear();
                }

                int before = first.Count;
                AddRange(first, mode, ["new primary", "new detail"], font);
                var addedFirst = first[before];
                var addedLast = first[first.Count - 1];
                observations.Add(new
                {
                    scenario = "views-" + mode + "-" + shape, before, owner.Text,
                    first = Texts(first), second = Texts(second), sameOtherFirst = ReferenceEquals(addedFirst, second[before]),
                    sameOtherLast = ReferenceEquals(addedLast, second[second.Count - 1])
                });
                property ??= owner.SubItems;
                first[0].Text = "changed primary";
                addedLast.Text = "changed last";
                observations.Add(new
                {
                    scenario = "views-mutate-" + mode + "-" + shape, owner.Text,
                    first = Texts(first), second = Texts(second), property = Texts(property),
                    sameFirst = ReferenceEquals(first[0], property[0]), stableProperty = ReferenceEquals(property, owner.SubItems)
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
            AddRange(first, mode, ["added first", "added second"], font);
            first[3].Name = first[4].Name = "target";
            observations.Add(new
            {
                scenario = "cache-add-range-" + mode, cachedFirst, cachedSecond, cachedProperty,
                first = first.IndexOfKey("target"), second = second.IndexOfKey("target"), property = property.IndexOfKey("target"),
                fresh = new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target")
            });

            property.Clear();
            AddRange(second, mode, ["new primary", "new earlier", "new later", "new last"], font);
            foreach (ListViewItem.ListViewSubItem item in first)
            {
                item.Name = "target";
            }
            observations.Add(new
            {
                scenario = "cache-clear-add-range-" + mode, owner.Text,
                first = first.IndexOfKey("target"), second = second.IndexOfKey("target"), property = property.IndexOfKey("target"),
                fresh = new ListViewItem.ListViewSubItemCollection(owner).IndexOfKey("target")
            });
            int firstNull = first.IndexOfKey(null);
            int secondEmpty = second.IndexOfKey("");
            object failure = Capture(() => AddRange(first, mode, null, font));
            AddRange(second, mode, [], font);
            observations.Add(new
            {
                scenario = "cache-null-empty-range-" + mode, firstNull, secondEmpty, failure,
                first = first.IndexOfKey("target"), second = second.IndexOfKey("target"), property = property.IndexOfKey("target"),
                items = Texts(first)
            });
        }
    }

    private static void ObserveFontLifetime(List<object> observations, Font font)
    {
        foreach (string operation in new[] { "clear", "remove", "reset", "replace-font", "null-font", "replace-item" })
        {
            var owner = new ListViewItem("primary");
            var items = owner.SubItems;
            items.AddRange(["first", "second"], Color.Red, Color.Blue, font);
            var first = items[1];
            var second = items[2];
            using var replacementFont = new Font("Consolas", 10f);
            switch (operation)
            {
                case "clear": items.Clear(); break;
                case "remove": items.Remove(first); break;
                case "reset": first.ResetStyle(); break;
                case "replace-font": first.Font = replacementFont; break;
                case "null-font": first.Font = null; break;
                case "replace-item": items[1] = new ListViewItem.ListViewSubItem(); break;
            }
            observations.Add(new
            {
                scenario = "font-lifetime-" + operation, sameOtherFont = ReferenceEquals(font, second.Font),
                height = font.GetHeight(96f), otherHeight = second.Font.GetHeight(96f)
            });
        }
    }

    private static void AddRange(ListViewItem.ListViewSubItemCollection items, string mode, string?[]? texts, Font font)
    {
        switch (mode)
        {
            case "object":
                items.AddRange(texts?.Select(text => text is null ? null! : new ListViewItem.ListViewSubItem(null, text)).ToArray()!);
                break;
            case "text": items.AddRange(texts!); break;
            case "style": items.AddRange(texts!, Color.Crimson, Color.AliceBlue, font); break;
            default: throw new ArgumentOutOfRangeException(nameof(mode));
        }
    }

    private static string[] Texts(ListViewItem.ListViewSubItemCollection items) => Enumerable.Range(0, items.Count).Select(index => items[index].Text).ToArray();

    private static object DescribeColor(Color color) => new { color.IsEmpty, color.IsNamedColor, color.IsKnownColor, color.Name, argb = color.ToArgb() };

    private static object Capture(Action action)
    {
        try
        {
            action();
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
