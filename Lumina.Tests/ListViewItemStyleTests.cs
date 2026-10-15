using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using Lumina.Forms;
using Xunit;
using SubItem = Lumina.Forms.ListViewItem.ListViewSubItem;
using SubItems = Lumina.Forms.ListViewItem.ListViewSubItemCollection;

namespace Lumina.Tests;

public class ListViewItemStyleTests
{
    [Theory]
    [InlineData("BackColor", typeof(Color))]
    [InlineData("ForeColor", typeof(Color))]
    [InlineData("Font", typeof(Font))]
    [InlineData("UseItemStyleForSubItems", typeof(bool))]
    public void Properties_HaveStandardNonVirtualPublicAccessors(string name, Type type)
    {
        PropertyInfo property = typeof(ListViewItem).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;

        Assert.NotNull(property);
        Assert.Equal(type, property.PropertyType);
        Assert.Empty(property.GetIndexParameters());
        Assert.Empty(property.GetMethod!.GetParameters());
        ParameterInfo parameter = Assert.Single(property.SetMethod!.GetParameters());
        Assert.Equal("value", parameter.Name);
        Assert.Equal(type, parameter.ParameterType);
        Assert.Null(property.GetCustomAttribute<AllowNullAttribute>());
        foreach (MethodInfo method in property.GetAccessors())
        {
            Assert.True(method.IsPublic);
            Assert.False(method.IsVirtual);
            Assert.False(method.IsFinal);
            Assert.False(method.IsStatic);
        }
    }

    [Theory]
    [InlineData("BackColor", false)]
    [InlineData("ForeColor", false)]
    [InlineData("Font", true)]
    public void PrimaryStyle_HasStandardDesignerMetadata(string name, bool localizable)
    {
        PropertyInfo property = typeof(ListViewItem).GetProperty(name)!;

        Assert.Equal(CategoryAttribute.Appearance.Category, property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal(DesignerSerializationVisibility.Hidden, property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>());
        Assert.Null(property.GetCustomAttribute<BindableAttribute>());
        if (localizable) Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        else Assert.Null(property.GetCustomAttribute<LocalizableAttribute>());
    }

    [Fact]
    public void UseItemStyle_HasStandardDefaultAndCategoryMetadata()
    {
        PropertyInfo property = typeof(ListViewItem).GetProperty(nameof(ListViewItem.UseItemStyleForSubItems))!;

        Assert.Equal(CategoryAttribute.Appearance.Category, property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal(true, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.Null(property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>());
        Assert.Null(property.GetCustomAttribute<LocalizableAttribute>());
    }

    [Theory]
    [InlineData("default", 0)]
    [InlineData("null-text", 1)]
    [InlineData("text", 1)]
    [InlineData("null-array", 0)]
    [InlineData("empty-array", 0)]
    [InlineData("array", 2)]
    public void DefaultStyle_IsSharedWithoutRestoringThePrimarySubitem(string construction, int expectedCount)
    {
        ListViewItem item = construction switch
        {
            "null-text" => new ListViewItem((string?)null),
            "text" => new ListViewItem("primary"),
            "null-array" => new ListViewItem((string?[]?)null),
            "empty-array" => new ListViewItem(Array.Empty<string?>()),
            "array" => new ListViewItem(new[] { "primary", "detail" }),
            _ => new ListViewItem()
        };
        var view = new SubItems(item);
        SubItem[] before = view.ToArray();

        Assert.Equal(SystemColors.Window, item.BackColor);
        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Same(new SubItem().Font, item.Font);
        Assert.Same(new ListViewItem().Font, item.Font);
        Assert.True(item.UseItemStyleForSubItems);
        Assert.Equal(expectedCount, view.Count);
        Assert.Equal(before, view);
    }

    public static IEnumerable<object[]> EmptyOperations()
    {
        foreach (string shape in new[] { "fresh", "property-cleared", "external-cleared", "removed-only" })
        foreach (string operation in Operations)
        foreach (string consumer in new[] { "public", "interface", "generic" })
            yield return [shape, operation, consumer];
    }

    [Theory]
    [MemberData(nameof(EmptyOperations))]
    public void EmptyStyleOperations_PreserveOrReplaceTheTableAccordingToTheReference(string shape, string operation, string consumer)
    {
        using var font = CreateFont();
        ListViewItem item = CreateEmpty(shape, out SubItem held);
        var first = new SubItems(item);
        var second = new SubItems(item);
        IEnumerator enumerator = GetEnumerator(first, consumer);
        Assert.False(enumerator.MoveNext());

        Apply(item, held, operation, font);

        bool restores = RestoresPrimary(operation);
        Assert.Equal(restores ? 1 : 0, first.Count);
        Assert.Equal(first.Count, second.Count);
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.False(enumerator.MoveNext());
        Assert.Equal(-1, first.IndexOfKey("target"));
        Assert.Equal(-1, second.IndexOfKey("target"));
        Assert.Equal(string.Empty, item.Text);
        Assert.Equal(string.Empty, item.Name);
        Assert.Equal("stable tip", item.ToolTipText);

        if (restores)
        {
            SubItem restored = first[0];
            Assert.Same(restored, second[0]);
            Assert.NotSame(held, restored);
            Assert.Null(restored.Tag);
            IEnumerator restoredEnumerator = first.GetEnumerator();
            Assert.True(restoredEnumerator.MoveNext());
            Apply(item, held, operation, font);
            Assert.Same(restored, first[0]);
            Assert.False(restoredEnumerator.MoveNext());
        }

        first.Add("external marker");
        Assert.Equal(restores ? 2 : 1, second.Count);
        if (restores)
        {
            Assert.False(enumerator.MoveNext());
            enumerator.Reset();
            Assert.False(enumerator.MoveNext());
        }
        else
        {
            Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
            Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
        }
    }

    public static IEnumerable<object[]> ColorCases()
    {
        foreach (string shape in new[] { "fresh", "property-cleared", "populated", "replaced-first" })
        foreach (string property in new[] { "BackColor", "ForeColor" })
        foreach (Color value in new[] { Color.Empty, Color.Transparent, Color.Red, SystemColors.Window, SystemColors.WindowText, Color.FromArgb(0, 7, 8, 9), Color.FromArgb(123, 11, 22, 33), Color.FromName("UnrecognizedColor") })
            yield return [shape, property, value];
    }

    [Theory]
    [MemberData(nameof(ColorCases))]
    public void ColorSetters_PreserveExactColorsAndBorrowedFontWhileProxyingPrimary(string shape, string property, Color value)
    {
        using var font = CreateFont();
        ListViewItem item = shape is "fresh" or "property-cleared"
            ? CreateEmpty(shape, out _)
            : new ListViewItem("primary");
        var view = new SubItems(item);
        if (view.Count > 0)
        {
            view[0].BackColor = Color.AliceBlue;
            view[0].ForeColor = Color.Crimson;
            view[0].Font = font;
            if (shape == "replaced-first") view[0] = new SubItem(null, "replacement", Color.Crimson, Color.AliceBlue, font);
        }
        SubItem? primaryBefore = view.Count > 0 ? view[0] : null;

        SetColor(item, property, value);
        SubItem primary = view[0];
        Font fontAfter = item.Font;
        SetColor(item, property, value);

        Color expected = value.IsEmpty ? property == "BackColor" ? SystemColors.Window : SystemColors.WindowText : value;
        Assert.Equal(expected, GetColor(item, property));
        Assert.Equal(expected, property == "BackColor" ? primary.BackColor : primary.ForeColor);
        Assert.Same(primary, view[0]);
        if (primaryBefore is not null) Assert.Same(primaryBefore, primary);
        Assert.Same(fontAfter, item.Font);
        if (primaryBefore is not null)
        {
            Assert.Same(font, item.Font);
            Assert.Equal(property == "BackColor" ? Color.Crimson : Color.AliceBlue, property == "BackColor" ? item.ForeColor : item.BackColor);
        }
        Assert.True(font.GetHeight(96f) > 0);
    }

    public static IEnumerable<object[]> PopulatedOperations()
    {
        foreach (string shape in new[] { "populated", "removed-first", "replaced-first", "duplicates", "cross-owner", "hidden-first" })
        foreach (string operation in Operations)
            yield return [shape, operation];
    }

    [Theory]
    [MemberData(nameof(PopulatedOperations))]
    public void PopulatedStyleOperations_KeepSharedViewsEnumeratorsAndIndependentKeyCaches(string shape, string operation)
    {
        using var font = CreateFont();
        ListViewItem item = CreatePopulated(shape, font, out SubItem held);
        var first = new SubItems(item);
        var second = new SubItems(item);
        Assert.Equal(shape == "removed-first" ? 0 : 1, first.IndexOfKey("target"));
        Assert.Equal(shape == "removed-first" ? 1 : 2, second.IndexOfKey("other"));
        for (int index = 0; index < first.Count; index++) first[index].Name = "target";
        SubItem[] before = first.ToArray();
        IEnumerator[] enumerators = [GetEnumerator(first, "public"), GetEnumerator(first, "interface"), GetEnumerator(first, "generic")];
        foreach (IEnumerator enumerator in enumerators)
        {
            Assert.True(enumerator.MoveNext());
            Assert.Same(before[0], enumerator.Current);
        }

        Apply(item, held, operation, font);

        Assert.Equal(before, first);
        Assert.Equal(before, second);
        foreach (IEnumerator enumerator in enumerators)
        {
            Assert.Same(before[0], enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Same(before[1], enumerator.Current);
            enumerator.Reset();
            Assert.True(enumerator.MoveNext());
            Assert.Same(before[0], enumerator.Current);
        }
        Assert.Equal(shape == "removed-first" ? 0 : 1, first.IndexOfKey("target"));
        Assert.Equal(shape == "removed-first" ? 1 : 2, second.IndexOfKey("target"));
        Assert.Equal(0, new SubItems(item).IndexOfKey("target"));
        Assert.Equal(first[0].BackColor, item.BackColor);
        Assert.Equal(first[0].ForeColor, item.ForeColor);
        Assert.Same(first[0].Font, item.Font);
        Assert.Equal("target", item.Name);
        Assert.Equal("stable tip", item.ToolTipText);
        Assert.All(before.OfType<HiddenStyleSubItem>(), hidden => Assert.Equal(0, hidden.HiddenCalls));
    }

    [Fact]
    public void PrimaryStyle_TwoWayAssociationFollowsReplacementRemovalAndReset()
    {
        using var font = CreateFont();
        var tag = new PoisonTag();
        var subTag = new PoisonTag();
        var item = new ListViewItem(new[] { "primary", "detail" }) { Name = "primary name", Tag = tag, ToolTipText = "stable tip" };
        var view = item.SubItems;
        SubItem primary = view[0];
        primary.Tag = subTag;
        SubItem detail = view[1];
        Font defaultFont = item.Font;

        item.BackColor = Color.AliceBlue;
        item.ForeColor = Color.Crimson;
        item.Font = font;
        Assert.Equal(Color.AliceBlue, primary.BackColor);
        Assert.Equal(Color.Crimson, primary.ForeColor);
        Assert.Same(font, primary.Font);
        primary.BackColor = Color.Beige;
        primary.ForeColor = Color.Navy;
        primary.Font = null;
        Assert.Equal(Color.Beige, item.BackColor);
        Assert.Equal(Color.Navy, item.ForeColor);
        Assert.Same(defaultFont, item.Font);

        primary.ResetStyle();
        Assert.Equal(SystemColors.Window, item.BackColor);
        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Same(defaultFont, item.Font);
        Assert.Equal("primary name", item.Name);
        Assert.Same(subTag, primary.Tag);
        detail.BackColor = Color.Yellow;
        detail.ForeColor = Color.Green;
        detail.Font = font;
        view[0] = detail;
        Assert.Equal(Color.Yellow, item.BackColor);
        Assert.Equal(Color.Green, item.ForeColor);
        Assert.Same(font, item.Font);
        Assert.Equal("detail", item.Text);
        Assert.Equal(string.Empty, item.Name);
        view.RemoveAt(0);
        Assert.Same(detail, view[0]);
        Assert.Same(font, item.Font);
        view.Clear();
        Assert.Equal(SystemColors.Window, item.BackColor);
        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Same(defaultFont, item.Font);
        Assert.Empty(view);
        Assert.Same(tag, item.Tag);
        Assert.Equal("stable tip", item.ToolTipText);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void UseItemStyle_StoresValueWithoutRestoringPrimaryOrChangingUnattachedSubitemGetters(bool populated)
    {
        using var font = CreateFont();
        var item = populated ? new ListViewItem(new[] { "primary", "detail" }) : new ListViewItem();
        var view = new SubItems(item);
        int originalCount = view.Count;
        Font defaultFont = new SubItem().Font;

        item.UseItemStyleForSubItems = false;
        Assert.False(item.UseItemStyleForSubItems);
        Assert.Equal(originalCount, view.Count);
        item.BackColor = Color.AliceBlue;
        item.ForeColor = Color.Crimson;
        item.Font = font;
        var member = new SubItem(item, "member");
        var outside = new SubItem(item, "outside");
        view.Add(member);
        foreach (bool value in new[] { false, false, true, true, false, true })
        {
            item.UseItemStyleForSubItems = value;
            Assert.Equal(value, item.UseItemStyleForSubItems);
            Assert.Equal(Color.AliceBlue, item.BackColor);
            Assert.Equal(Color.Crimson, item.ForeColor);
            Assert.Same(font, item.Font);
            foreach (SubItem subitem in view.Skip(1).Append(outside))
            {
                Assert.Equal(SystemColors.Window, subitem.BackColor);
                Assert.Equal(SystemColors.WindowText, subitem.ForeColor);
                Assert.Same(defaultFont, subitem.Font);
            }
        }
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("property-cleared")]
    [InlineData("populated")]
    [InlineData("replaced-first")]
    public void FontSequence_PreservesSuppliedIdentityAndRestoresSharedDefault(string shape)
    {
        using var font = CreateFont();
        using var replacement = new Font("Consolas", 10f);
        ListViewItem item = shape is "fresh" or "property-cleared" ? CreateEmpty(shape, out _) : CreatePopulated(shape, font, out _);
        var view = new SubItems(item);
        Font defaultFont = new SubItem().Font;
        foreach (Font? value in new Font?[] { null, font, font, replacement, null, null, font })
        {
            Color backBefore = item.BackColor;
            Color foreBefore = item.ForeColor;
            item.Font = value!;

            Assert.Same(value ?? defaultFont, item.Font);
            Assert.Same(item.Font, view[0].Font);
            Assert.Equal(backBefore, item.BackColor);
            Assert.Equal(foreBefore, item.ForeColor);
            Assert.True(font.GetHeight(96f) > 0);
        }
    }

    [Theory]
    [InlineData("null")]
    [InlineData("replace")]
    [InlineData("reset-primary")]
    [InlineData("clear")]
    [InlineData("remove-primary")]
    [InlineData("replace-primary")]
    public void BorrowedFont_IsNotDisposedWhenStyleOrPrimaryChanges(string operation)
    {
        using var shared = CreateFont();
        using var replacement = new Font("Consolas", 10f);
        var item = new ListViewItem("primary") { Font = shared };
        var view = item.SubItems;
        SubItem held = view[0];
        var other = new SubItem(null, "other", Color.Empty, Color.Empty, shared);
        float height = shared.GetHeight(96f);
        switch (operation)
        {
            case "null": item.Font = null!; break;
            case "replace": item.Font = replacement; break;
            case "reset-primary": held.ResetStyle(); break;
            case "clear": view.Clear(); break;
            case "remove-primary": view.RemoveAt(0); break;
            case "replace-primary": view[0] = new SubItem(); break;
        }

        Assert.Equal(height, shared.GetHeight(96f));
        Assert.Same(shared, other.Font);
        Assert.Equal(height, other.Font.GetHeight(96f));
        if (operation is "clear" or "remove-primary" or "replace-primary") Assert.Same(shared, held.Font);
        else Assert.NotSame(shared, held.Font);
        if (operation == "replace") Assert.Same(replacement, item.Font);
        else Assert.Same(new SubItem().Font, item.Font);
    }

    [Fact]
    public void DisposedBorrowedFont_CanBeStoredAndReadByIdentityWithoutConsumingItsData()
    {
        var disposed = new Font("Arial", 11f);
        var item = new ListViewItem("primary") { Font = disposed };
        SubItem primary = item.SubItems[0];
        disposed.Dispose();

        Assert.Same(disposed, item.Font);
        Assert.Same(disposed, primary.Font);
        primary.ResetStyle();
        Assert.Same(new SubItem().Font, item.Font);
        item.Font = disposed;
        Assert.Same(disposed, item.Font);
        item.Font = null!;
        Assert.Same(new SubItem().Font, item.Font);
    }

    [Fact]
    public void SharedPrimary_UpdatesBothOwnersWithoutTransferringOrCopyingStyle()
    {
        using var font = CreateFont();
        var first = new ListViewItem("first") { Font = font, ForeColor = Color.Crimson, BackColor = Color.AliceBlue };
        SubItem shared = first.SubItems[0];
        var second = new ListViewItem();
        new SubItems(second).Add(shared);

        Assert.Same(font, second.Font);
        Assert.Equal(Color.Crimson, second.ForeColor);
        Assert.Equal(Color.AliceBlue, second.BackColor);
        second.ForeColor = Color.Green;
        Assert.Equal(Color.Green, first.ForeColor);
        first.BackColor = Color.Yellow;
        Assert.Equal(Color.Yellow, second.BackColor);
        shared.ResetStyle();
        Assert.Equal(SystemColors.Window, first.BackColor);
        Assert.Equal(SystemColors.WindowText, second.ForeColor);
        Assert.Same(first.Font, second.Font);
        Assert.True(font.GetHeight(96f) > 0);
    }

    [Fact]
    public void StyleOperations_IgnoreHiddenMembersAndPoisonTags()
    {
        using var font = CreateFont();
        var hidden = new HiddenStyleItem();
        ListViewItem item = hidden;
        var tag = new PoisonTag();
        var subTag = new PoisonTag();
        item.Tag = tag;
        var primary = new HiddenStyleSubItem();
        ((SubItem)primary).Tag = subTag;
        new SubItems(item).Add(primary);
        foreach (string operation in Operations)
        {
            Apply(item, primary, operation, font);
            Assert.Equal(((SubItem)primary).BackColor, item.BackColor);
            Assert.Equal(((SubItem)primary).ForeColor, item.ForeColor);
            Assert.Same(((SubItem)primary).Font, item.Font);
        }

        Assert.Equal(0, hidden.HiddenCalls);
        Assert.Equal(0, primary.HiddenCalls);
        Assert.Same(tag, item.Tag);
        Assert.Same(subTag, ((SubItem)primary).Tag);
    }

    private static readonly string[] Operations = ["read-back", "read-fore", "read-font", "read-use", "back-empty", "back-custom", "fore-empty", "fore-custom", "font-null", "font-custom", "use-false", "use-true", "reset-held"];
    private static bool RestoresPrimary(string operation) => operation is "back-empty" or "back-custom" or "fore-empty" or "fore-custom" or "font-null" or "font-custom";
    private static Font CreateFont() => new("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
    private static Color GetColor(ListViewItem item, string property) => property == "BackColor" ? item.BackColor : item.ForeColor;
    private static void SetColor(ListViewItem item, string property, Color value)
    {
        if (property == "BackColor") item.BackColor = value;
        else item.ForeColor = value;
    }

    private static ListViewItem CreateEmpty(string shape, out SubItem held)
    {
        var item = new ListViewItem { Tag = new PoisonTag(), ToolTipText = "stable tip" };
        var view = new SubItems(item);
        held = new SubItem(null, "held");
        if (shape == "property-cleared") item.SubItems.Clear();
        else if (shape is "external-cleared" or "removed-only")
        {
            view.Add(held);
            if (shape == "external-cleared") view.Clear();
            else view.RemoveAt(0);
        }
        return item;
    }

    private static ListViewItem CreatePopulated(string shape, Font font, out SubItem held)
    {
        var item = new ListViewItem { Tag = new PoisonTag(), ToolTipText = "stable tip" };
        var view = new SubItems(item);
        held = new SubItem(null, "primary", Color.Crimson, Color.AliceBlue, font) { Name = "primary", Tag = new PoisonTag() };
        view.Add(held);
        view.Add(new SubItem(null, "detail") { Name = "target" });
        view.Add(new SubItem(null, "third") { Name = "other" });
        if (shape == "removed-first") view.RemoveAt(0);
        if (shape == "replaced-first") view[0] = new SubItem(null, "replacement", Color.Navy, Color.Beige, null);
        if (shape == "duplicates") view.Add(held);
        if (shape == "cross-owner") new SubItems(new ListViewItem()).Add(held);
        if (shape == "hidden-first") view[0] = new HiddenStyleSubItem();
        return item;
    }

    private static IEnumerator GetEnumerator(SubItems view, string consumer) => consumer switch
    {
        "interface" => ((IEnumerable)view).GetEnumerator(),
        "generic" => ((IEnumerable<SubItem>)view).GetEnumerator(),
        _ => view.GetEnumerator()
    };

    private static void Apply(ListViewItem item, SubItem held, string operation, Font font)
    {
        switch (operation)
        {
            case "read-back": _ = item.BackColor; break;
            case "read-fore": _ = item.ForeColor; break;
            case "read-font": _ = item.Font; break;
            case "read-use": _ = item.UseItemStyleForSubItems; break;
            case "back-empty": item.BackColor = Color.Empty; break;
            case "back-custom": item.BackColor = Color.FromArgb(0, 7, 8, 9); break;
            case "fore-empty": item.ForeColor = Color.Empty; break;
            case "fore-custom": item.ForeColor = Color.FromArgb(123, 11, 22, 33); break;
            case "font-null": item.Font = null!; break;
            case "font-custom": item.Font = font; break;
            case "use-false": item.UseItemStyleForSubItems = false; break;
            case "use-true": item.UseItemStyleForSubItems = true; break;
            case "reset-held": held.ResetStyle(); break;
        }
    }

    private sealed class HiddenStyleItem : ListViewItem
    {
        public int HiddenCalls { get; private set; }
        public new Color BackColor { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
        public new Color ForeColor { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
        public new Font Font { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
        public new bool UseItemStyleForSubItems { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
        public new SubItems SubItems { get { HiddenCalls++; throw new InvalidOperationException(); } }
        public new object Tag { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
    }

    private sealed class HiddenStyleSubItem : SubItem
    {
        public int HiddenCalls { get; private set; }
        public new Color BackColor { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
        public new Color ForeColor { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
        public new Font Font { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
        public new object Tag { get { HiddenCalls++; throw new InvalidOperationException(); } set { HiddenCalls++; throw new InvalidOperationException(); } }
        public new void ResetStyle() { HiddenCalls++; throw new InvalidOperationException(); }
    }

    private sealed class PoisonTag
    {
        public override string ToString() => throw new InvalidOperationException("Style must not format Tag.");
        public override bool Equals(object? obj) => throw new InvalidOperationException("Style must not compare Tag.");
        public override int GetHashCode() => throw new InvalidOperationException("Style must not hash Tag.");
    }
}
