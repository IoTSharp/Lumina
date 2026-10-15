using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Lumina.Forms;
using Xunit;
using SubItem = Lumina.Forms.ListViewItem.ListViewSubItem;
using SubItems = Lumina.Forms.ListViewItem.ListViewSubItemCollection;

namespace Lumina.Tests;

public class ListViewItemMetadataTests
{
    [Theory]
    [InlineData("Name", typeof(string))]
    [InlineData("Tag", typeof(object))]
    [InlineData("ToolTipText", typeof(string))]
    public void Properties_HaveStandardNonVirtualPublicAccessors(string name, Type type)
    {
        PropertyInfo property = typeof(ListViewItem).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;

        Assert.NotNull(property);
        Assert.Equal(type, property.PropertyType);
        Assert.Empty(property.GetIndexParameters());
        Assert.Null(property.GetCustomAttribute<AllowNullAttribute>());
        Assert.Empty(property.GetMethod!.GetParameters());
        Assert.Equal("value", Assert.Single(property.SetMethod!.GetParameters()).Name);
        foreach (MethodInfo accessor in property.GetAccessors())
        {
            Assert.True(accessor.IsPublic);
            Assert.False(accessor.IsVirtual);
            Assert.False(accessor.IsFinal);
            Assert.False(accessor.IsStatic);
        }
    }

    [Fact]
    public void Name_HasStandardDesignerMetadata()
    {
        PropertyInfo property = typeof(ListViewItem).GetProperty(nameof(ListViewItem.Name))!;

        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden, property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>());
    }

    [Fact]
    public void Tag_HasStandardBindingAndConversionMetadata()
    {
        PropertyInfo property = typeof(ListViewItem).GetProperty(nameof(ListViewItem.Tag))!;
        BindableAttribute bindable = property.GetCustomAttribute<BindableAttribute>()!;

        Assert.True(bindable.Bindable);
        Assert.Equal(BindingDirection.OneWay, bindable.Direction);
        Assert.Equal(CategoryAttribute.Data.Category, property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.False(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        Assert.Equal(typeof(StringConverter).AssemblyQualifiedName, property.GetCustomAttribute<TypeConverterAttribute>()!.ConverterTypeName);
    }

    [Fact]
    public void ToolTipText_HasStandardDefaultAndCategoryMetadata()
    {
        PropertyInfo property = typeof(ListViewItem).GetProperty(nameof(ListViewItem.ToolTipText))!;

        Assert.Equal(CategoryAttribute.Appearance.Category, property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal(string.Empty, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.Null(property.GetCustomAttribute<LocalizableAttribute>());
    }

    [Theory]
    [InlineData("default", 0, "")]
    [InlineData("null-text", 1, "")]
    [InlineData("text", 1, "primary")]
    [InlineData("null-array", 0, "")]
    [InlineData("empty-array", 0, "")]
    [InlineData("array", 2, "primary")]
    public void Defaults_DoNotRestoreOrChangeTheSubitemTable(string construction, int expectedCount, string expectedText)
    {
        ListViewItem owner = construction switch
        {
            "null-text" => new ListViewItem((string?)null),
            "text" => new ListViewItem("primary"),
            "null-array" => new ListViewItem((string?[]?)null),
            "empty-array" => new ListViewItem(Array.Empty<string?>()),
            "array" => new ListViewItem(new[] { "primary", "detail" }),
            _ => new ListViewItem()
        };
        var view = new SubItems(owner);

        Assert.Equal(expectedCount, view.Count);
        Assert.Equal(string.Empty, owner.Name);
        Assert.Null(owner.Tag);
        Assert.Equal(string.Empty, owner.ToolTipText);
        Assert.Equal(expectedText, owner.Text);
        Assert.Equal(expectedCount, view.Count);
    }

    public static IEnumerable<object?[]> StringCases()
    {
        string?[] texts = [null, "", "plain", "中文项目", "emoji 😀", "two\nlines", "two\r\nlines", "\t spaced \t", "nul\0tail", "אבג العربية", "e\u0301", "{braces}: text"];
        foreach (string property in new[] { "Name", "ToolTipText" })
        foreach (bool populated in new[] { false, true })
        foreach (string? text in texts)
            yield return [property, populated, text];
    }

    [Theory]
    [MemberData(nameof(StringCases))]
    public void Strings_PreserveExactContentsAndAssignedIdentityWithNullNormalizedToEmpty(string property, bool populated, string? text)
    {
        var owner = populated ? new ListViewItem(new[] { "primary", "detail" }) : new ListViewItem();
        var view = new SubItems(owner);
        string? assigned = text is null ? null : new string(text.ToCharArray());

        SetString(owner, property, assigned);
        string first = GetString(owner, property);
        SetString(owner, property, assigned);

        Assert.Equal(text ?? string.Empty, first);
        Assert.Same(first, GetString(owner, property));
        if (assigned is not null) Assert.Same(assigned, first);
        Assert.Equal(populated ? 2 : property == "Name" ? 1 : 0, view.Count);
        Assert.Equal(populated ? "primary" : string.Empty, owner.Text);
        if (property == "Name") Assert.Same(first, view[0].Name);
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("property-cleared")]
    [InlineData("external-cleared")]
    [InlineData("removed-only")]
    public void NameGetter_OnAnEmptyTableDoesNotRestoreOrReplaceIt(string shape)
    {
        ListViewItem owner = EmptyOwner(shape);
        var view = new SubItems(owner);
        IEnumerator enumerator = view.GetEnumerator();

        Assert.Equal(string.Empty, owner.Name);
        Assert.Empty(view);
        view.Add("added externally");

        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
    }

    public static IEnumerable<object?[]> EmptyNameAssignments()
    {
        foreach (string shape in new[] { "fresh", "property-cleared", "external-cleared", "removed-only" })
        foreach (string? value in new string?[] { null, "", "new-name" })
            yield return [shape, value];
    }

    [Theory]
    [MemberData(nameof(EmptyNameAssignments))]
    public void NameSetter_EvenNullOrEmptyRestoresPrimaryByReplacingTheEmptyTable(string shape, string? value)
    {
        ListViewItem owner = EmptyOwner(shape);
        var first = new SubItems(owner);
        var second = new SubItems(owner);
        IEnumerator emptyEnumerator = first.GetEnumerator();

        owner.Name = value!;

        Assert.Single(first);
        Assert.Single(second);
        Assert.Same(first[0], second[0]);
        Assert.Equal(value ?? string.Empty, first[0].Name);
        Assert.Equal(string.Empty, first[0].Text);
        Assert.False(emptyEnumerator.MoveNext());
        emptyEnumerator.Reset();
        Assert.False(emptyEnumerator.MoveNext());
        first.Add("later");
        Assert.False(emptyEnumerator.MoveNext());
        Assert.Equal(2, owner.SubItems.Count);
    }

    [Fact]
    public void Name_TracksTheCurrentFirstSubitemAcrossTextChangesRemovalAndReplacement()
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var items = owner.SubItems;
        SubItem original = items[0];
        owner.Name = "item-name";
        Assert.Equal("item-name", original.Name);
        original.Name = "primary-name";
        Assert.Equal("primary-name", owner.Name);

        owner.Text = "changed-text";
        Assert.Equal("primary-name", owner.Name);
        Assert.Equal("changed-text", original.Text);
        items[1].Name = "detail-name";
        items.RemoveAt(0);
        Assert.Equal("detail-name", owner.Name);
        Assert.Equal("detail", owner.Text);

        var replacement = new SubItem(null, "replacement") { Name = "replacement-name" };
        items[0] = replacement;
        Assert.Equal("replacement-name", owner.Name);
        owner.Name = "renamed";
        Assert.Equal("renamed", replacement.Name);
        Assert.Equal("primary-name", original.Name);
        items.Clear();
        Assert.Equal(string.Empty, owner.Name);
        Assert.Empty(items);
        items.Add(original);
        Assert.Equal("primary-name", owner.Name);
        Assert.Equal("changed-text", owner.Text);
    }

    [Fact]
    public void Name_WritesSharedPrimaryObjectsWithoutChangingCollectionStructure()
    {
        var firstOwner = new ListViewItem();
        var firstView = new SubItems(firstOwner);
        var secondOwner = new ListViewItem();
        var secondView = new SubItems(secondOwner);
        var shared = new SubItem(null, "shared text") { Name = "before" };
        firstView.Add(shared);
        firstView.Add(shared);
        secondView.Add(shared);
        IEnumerator enumerator = firstView.GetEnumerator();
        Assert.True(enumerator.MoveNext());

        firstOwner.Name = "after";

        Assert.Equal("after", shared.Name);
        Assert.Equal("after", firstView[1].Name);
        Assert.Equal("after", secondOwner.Name);
        Assert.Equal(2, firstView.Count);
        Assert.Single(secondView);
        Assert.Same(shared, enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(shared, enumerator.Current);
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.True(enumerator.MoveNext());
    }

    [Theory]
    [InlineData("read-name")]
    [InlineData("write-name")]
    [InlineData("write-tag")]
    [InlineData("write-tooltip")]
    public void MetadataAccess_PreservesEachViewsIndependentKeyCache(string operation)
    {
        var owner = new ListViewItem(new[] { "primary", "earlier", "later" });
        var first = owner.SubItems;
        var second = new SubItems(owner);
        first[0].Name = "primary";
        first[1].Name = "earlier";
        first[2].Name = "target";
        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("earlier"));
        first[1].Name = "target";

        ApplyMetadata(owner, operation);

        Assert.Equal(2, first.IndexOfKey("target"));
        Assert.Equal(1, second.IndexOfKey("target"));
        Assert.Equal(3, first.Count);
    }

    public static IEnumerable<object?[]> NonStructuralOperations()
    {
        foreach (string operation in new[] { "read-name", "write-name", "write-tag", "write-tooltip" })
        foreach (string consumer in new[] { "public", "interface", "generic" })
            yield return [operation, consumer];
    }

    [Theory]
    [MemberData(nameof(NonStructuralOperations))]
    public void MetadataAccess_OnExistingSubitemsPreservesAllEnumerationConsumers(string operation, string consumer)
    {
        var owner = new ListViewItem(new[] { "primary", "detail" });
        var view = owner.SubItems;
        IEnumerator enumerator = consumer switch
        {
            "interface" => ((IEnumerable)view).GetEnumerator(),
            "generic" => ((IEnumerable<SubItem>)view).GetEnumerator(),
            _ => view.GetEnumerator()
        };
        Assert.True(enumerator.MoveNext());
        SubItem primary = view[0];
        SubItem detail = view[1];

        ApplyMetadata(owner, operation);

        Assert.Same(primary, enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(detail, enumerator.Current);
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.True(enumerator.MoveNext());
        Assert.Same(primary, enumerator.Current);
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("property-cleared")]
    [InlineData("external-cleared")]
    [InlineData("removed-only")]
    public void TagAndToolTipText_AreIndependentOfAnEmptySubitemTable(string shape)
    {
        ListViewItem owner = EmptyOwner(shape);
        var first = new SubItems(owner);
        var second = new SubItems(owner);
        IEnumerator enumerator = first.GetEnumerator();
        var poison = new PoisonTag();

        owner.Tag = poison;
        owner.ToolTipText = "tooltip 中文\r\n\0end";
        Assert.Same(poison, owner.Tag);
        Assert.Equal("tooltip 中文\r\n\0end", owner.ToolTipText);
        Assert.Empty(first);
        Assert.Empty(second);
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        first.Add("added externally");
        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        Assert.Equal(0, poison.Reads);
        Assert.Equal(0, poison.Disposals);
    }

    [Fact]
    public void Tag_PreservesReferencesAcrossRepeatedAssignmentReplacementAndNull()
    {
        var owner = new ListViewItem();
        var view = new SubItems(owner);
        var poison = new PoisonTag();
        string text = new("tag text 中文".ToCharArray());
        object?[] values = [null, new object(), text, poison, poison, null, text];

        foreach (object? value in values)
        {
            owner.Tag = value;
            Assert.Same(value, owner.Tag);
            owner.Tag = value;
            Assert.Same(value, owner.Tag);
            Assert.Empty(view);
        }

        Assert.Equal(0, poison.Reads);
        Assert.Equal(0, poison.Disposals);
    }

    [Fact]
    public void Tag_IsSeparateFromThePrimarySubitemTagAndSurvivesSubitemReplacement()
    {
        var owner = new ListViewItem("primary");
        var itemTag = new PoisonTag();
        var primaryTag = new PoisonTag();
        SubItem primary = owner.SubItems[0];
        owner.Tag = itemTag;
        primary.Tag = primaryTag;
        owner.ToolTipText = "persistent tip";

        owner.SubItems.Clear();
        owner.Name = "restored";

        Assert.Same(itemTag, owner.Tag);
        Assert.Same(primaryTag, primary.Tag);
        Assert.Null(owner.SubItems[0].Tag);
        Assert.Equal("persistent tip", owner.ToolTipText);
        Assert.Equal(0, itemTag.Reads);
        Assert.Equal(0, itemTag.Disposals);
        Assert.Equal(0, primaryTag.Reads);
        Assert.Equal(0, primaryTag.Disposals);
    }

    [Fact]
    public void Metadata_UsesBasePropertiesWithoutReadingOrWritingHiddenDerivedMembers()
    {
        var hidden = new HiddenItem();
        ListViewItem owner = hidden;
        var view = new SubItems(owner);
        var hiddenSubItem = new HiddenSubItem();
        SubItem primary = hiddenSubItem;
        primary.Name = "base-primary-name";
        primary.Text = "base-primary-text";
        view.Add(primary);
        var poison = new PoisonTag();
        Assert.Equal("base-primary-name", owner.Name);

        owner.Name = "base-name";
        owner.ToolTipText = "base-tip";
        owner.Tag = poison;

        Assert.Equal("base-name", owner.Name);
        Assert.Equal("base-tip", owner.ToolTipText);
        Assert.Equal("base-primary-text", owner.Text);
        Assert.Same(poison, owner.Tag);
        Assert.Equal(0, hidden.HiddenReads);
        Assert.Equal(0, hidden.HiddenWrites);
        Assert.Equal(0, hiddenSubItem.HiddenReads);
        Assert.Equal(0, hiddenSubItem.HiddenWrites);
        Assert.Equal(0, poison.Reads);
        Assert.Equal(0, poison.Disposals);
    }

    private static ListViewItem EmptyOwner(string shape)
    {
        var owner = new ListViewItem();
        if (shape == "property-cleared") owner.SubItems.Clear();
        if (shape is "external-cleared" or "removed-only")
        {
            var view = new SubItems(owner);
            view.Add("primary");
            if (shape == "external-cleared") view.Clear();
            else view.RemoveAt(0);
        }
        return owner;
    }

    private static void SetString(ListViewItem owner, string property, string? value)
    {
        if (property == "Name") owner.Name = value!;
        else owner.ToolTipText = value!;
    }

    private static string GetString(ListViewItem owner, string property) => property == "Name" ? owner.Name : owner.ToolTipText;

    private static void ApplyMetadata(ListViewItem owner, string operation)
    {
        switch (operation)
        {
            case "read-name": _ = owner.Name; break;
            case "write-name": owner.Name = "target"; break;
            case "write-tag": owner.Tag = new PoisonTag(); break;
            case "write-tooltip": owner.ToolTipText = "tip"; break;
        }
    }

    private sealed class PoisonTag : IDisposable
    {
        public int Reads { get; private set; }
        public int Disposals { get; private set; }
        public override string ToString() { Reads++; throw new InvalidOperationException("Metadata must not format Tag."); }
        public void Dispose() { Disposals++; throw new InvalidOperationException("Metadata must not dispose Tag."); }
    }

    private sealed class HiddenItem : ListViewItem
    {
        public int HiddenReads { get; private set; }
        public int HiddenWrites { get; private set; }
        public new string Name { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
        public new string ToolTipText { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
        public new object? Tag { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
        public new SubItems SubItems { get { HiddenReads++; throw new InvalidOperationException(); } }
    }

    private sealed class HiddenSubItem : SubItem
    {
        public int HiddenReads { get; private set; }
        public int HiddenWrites { get; private set; }
        public new string Name { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
        public new string Text { get { HiddenReads++; throw new InvalidOperationException(); } set { HiddenWrites++; throw new InvalidOperationException(); } }
    }
}
