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

public class ListViewItemImageTests
{
    private static readonly string[] Operations = ["read-index", "read-key", "index-minus-one", "index-zero", "index-max", "index-invalid", "key-null", "key-empty", "key-text", "key-repeat", "index-repeat"];

    [Theory]
    [InlineData("ImageIndex", typeof(int), -1)]
    [InlineData("ImageKey", typeof(string), "")]
    public void Properties_HaveStandardShapeAndInScopeDesignerMetadata(string name, Type type, object defaultValue)
    {
        PropertyInfo property = typeof(ListViewItem).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;

        Assert.NotNull(property);
        Assert.Equal(type, property.PropertyType);
        Assert.Empty(property.GetIndexParameters());
        Assert.Empty(property.GetMethod!.GetParameters());
        ParameterInfo parameter = Assert.Single(property.SetMethod!.GetParameters());
        Assert.Equal("value", parameter.Name);
        Assert.Equal(type, parameter.ParameterType);
        foreach (MethodInfo method in property.GetAccessors())
        {
            Assert.True(method.IsPublic);
            Assert.False(method.IsVirtual);
            Assert.False(method.IsFinal);
            Assert.False(method.IsStatic);
        }
        NullabilityInfo nullable = new NullabilityInfoContext().Create(property);
        Assert.Equal(NullabilityState.NotNull, nullable.ReadState);
        Assert.Equal(NullabilityState.NotNull, nullable.WriteState);
        Assert.Null(property.GetCustomAttribute<AllowNullAttribute>());
        Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal(defaultValue, property.GetCustomAttribute<DefaultValueAttribute>()!.Value);
        Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        Assert.Equal(DesignerSerializationVisibility.Hidden, property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.Equal(RefreshProperties.Repaint, property.GetCustomAttribute<RefreshPropertiesAttribute>()!.RefreshProperties);
        Assert.Null(property.GetCustomAttribute<BrowsableAttribute>());
        Assert.Null(property.GetCustomAttribute<BindableAttribute>());
        if (name == "ImageIndex")
            Assert.Equal("The ImageList index value of the image displayed when the list view item is in the unselected state.", property.GetCustomAttribute<DescriptionAttribute>()!.Description);
        else
            Assert.Null(property.GetCustomAttribute<DescriptionAttribute>());
        // Standard converters and ImageIndexEditor are tracked as remaining designer
        // work. Do not assert that their absence is WinForms metadata compatibility.
    }

    [Theory]
    [InlineData("default", 0)]
    [InlineData("null-text", 1)]
    [InlineData("text", 1)]
    [InlineData("null-array", 0)]
    [InlineData("empty-array", 0)]
    [InlineData("array", 2)]
    public void Defaults_DoNotRestoreOrChangePrimarySubitems(string construction, int count)
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

        Assert.Equal(-1, item.ImageIndex);
        Assert.Same(string.Empty, item.ImageKey);
        Assert.Equal(count, view.Count);
        Assert.Equal(before, view);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(int.MaxValue)]
    public void Index_StoresUnattachedValueAndClearsKeyIncludingRepeatedMinusOne(int index)
    {
        var item = new ListViewItem { ImageKey = "original" };

        item.ImageIndex = index;
        Assert.Equal(index, item.ImageIndex);
        Assert.Same(string.Empty, item.ImageKey);
        item.ImageIndex = index;
        Assert.Equal(index, item.ImageIndex);
        Assert.Same(string.Empty, item.ImageKey);
        Assert.Empty(new SubItems(item));
    }

    [Theory]
    [InlineData(int.MinValue, false)]
    [InlineData(-2, false)]
    [InlineData(int.MinValue, true)]
    [InlineData(-2, true)]
    public void InvalidIndex_PreservesPreviousSelectionAndReportsActualValue(int invalid, bool keyMode)
    {
        var item = new ListViewItem();
        string key = new("original key".ToCharArray());
        if (keyMode) item.ImageKey = key;
        else item.ImageIndex = 37;
        string beforeKey = item.ImageKey;

        for (int attempt = 0; attempt < 2; attempt++)
        {
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => item.ImageIndex = invalid);
            Assert.Equal("value", exception.ParamName);
            Assert.Equal(invalid, exception.ActualValue);
            Assert.Equal(keyMode ? -1 : 37, item.ImageIndex);
            Assert.Same(beforeKey, item.ImageKey);
        }
        Assert.Empty(new SubItems(item));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("plain")]
    [InlineData("中文 🧪")]
    [InlineData("line\r\nbreak\n")]
    [InlineData("\0")]
    [InlineData("before\0after")]
    public void Key_PreservesTextAndIdentityAndAlwaysClearsIndex(string? text)
    {
        var item = new ListViewItem { ImageIndex = int.MaxValue };
        string? value = text is null ? null : new string(text.ToCharArray());

        item.ImageKey = value!;
        Assert.Equal(-1, item.ImageIndex);
        Assert.Same(value ?? string.Empty, item.ImageKey);
        item.ImageKey = value!;
        Assert.Equal(-1, item.ImageIndex);
        Assert.Same(value ?? string.Empty, item.ImageKey);
        Assert.Empty(new SubItems(item));
    }

    [Fact]
    public void EqualKeys_ReplaceStringIdentityWithoutAnEqualityShortcut()
    {
        string first = new("same key".ToCharArray());
        string second = new("same key".ToCharArray());
        var item = new ListViewItem { ImageKey = first };

        item.ImageKey = second;

        Assert.NotSame(first, item.ImageKey);
        Assert.Same(second, item.ImageKey);
        Assert.Equal(-1, item.ImageIndex);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(37)]
    [InlineData(int.MaxValue)]
    public void IndexAndKey_AlternateIncludingNullAndEmptyKeys(int index)
    {
        var item = new ListViewItem();
        foreach (string? key in new[] { "selected", string.Empty, null })
        {
            item.ImageIndex = index;
            Assert.Equal(index, item.ImageIndex);
            Assert.Same(string.Empty, item.ImageKey);
            item.ImageKey = key!;
            Assert.Equal(-1, item.ImageIndex);
            Assert.Same(key ?? string.Empty, item.ImageKey);
        }
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
    public void ImageOperations_PreserveEmptyTableAndItsEnumerators(string shape, string operation, string consumer)
    {
        var item = new ListViewItem { Tag = new PoisonTag(), ToolTipText = "tip" };
        var first = new SubItems(item);
        if (shape == "property-cleared") item.SubItems.Clear();
        if (shape is "external-cleared" or "removed-only")
        {
            first.Add("removed");
            if (shape == "external-cleared") first.Clear();
            else first.RemoveAt(0);
        }
        var second = new SubItems(item);
        IEnumerator enumerator = GetEnumerator(first, consumer);
        Assert.False(enumerator.MoveNext());

        Apply(item, operation);

        Assert.Empty(first);
        Assert.Empty(second);
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.False(enumerator.MoveNext());
        Assert.Equal(-1, first.IndexOfKey("target"));
        Assert.Equal(-1, second.IndexOfKey("target"));
        Assert.Equal("tip", item.ToolTipText);
        Assert.Equal(string.Empty, item.Name);
        Assert.Equal("ListViewItem: {}", item.ToString());

        // Unlike primary-text/style restoration, selection leaves the old empty
        // table in place, so appending through another view invalidates its iterator.
        first.Add("appended");
        Assert.Single(second);
        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
    }

    public static IEnumerable<object[]> PopulatedOperations()
    {
        foreach (string shape in new[] { "populated", "removed-first", "replaced-first", "duplicates", "cross-owner", "hidden-first" })
        foreach (string operation in Operations)
        foreach (string consumer in new[] { "public", "interface", "generic" })
            yield return [shape, operation, consumer];
    }

    [Theory]
    [MemberData(nameof(PopulatedOperations))]
    public void ImageOperations_PreserveSubitemsStylesEnumeratorsAndIndependentKeyCaches(string shape, string operation, string consumer)
    {
        using var font = new Font("Arial", 11f);
        ListViewItem item = CreatePopulated(shape, font);
        var first = new SubItems(item);
        var second = new SubItems(item);
        int firstKey = first.IndexOfKey("target");
        int secondKey = second.IndexOfKey("other");
        foreach (SubItem subitem in first) subitem.Name = "target";
        SubItem[] before = first.ToArray();
        var styles = before.Select(entry => (entry.Text, entry.ForeColor, entry.BackColor, entry.Font, entry.Tag)).ToArray();
        object? tag = item.Tag;
        string text = item.Text;
        IEnumerator enumerator = GetEnumerator(first, consumer);
        Assert.True(enumerator.MoveNext());
        Assert.Same(before[0], enumerator.Current);

        Apply(item, operation);

        Assert.Same(before[0], enumerator.Current);
        Assert.True(enumerator.MoveNext());
        Assert.Same(before[1], enumerator.Current);
        enumerator.Reset();
        Assert.True(enumerator.MoveNext());
        Assert.Same(before[0], enumerator.Current);
        Assert.Equal(before.Length, first.Count);
        Assert.Equal(firstKey, first.IndexOfKey("target"));
        Assert.Equal(secondKey, second.IndexOfKey("target"));
        Assert.Equal(0, new SubItems(item).IndexOfKey("target"));
        Assert.Same(tag, item.Tag);
        Assert.Equal(text, item.Text);
        Assert.Equal("target", item.Name);
        Assert.Equal("tip", item.ToolTipText);
        Assert.False(item.UseItemStyleForSubItems);
        Assert.Equal($"ListViewItem: {{{text}}}", item.ToString());
        for (int index = 0; index < before.Length; index++)
        {
            Assert.Same(before[index], first[index]);
            Assert.Same(before[index], second[index]);
            Assert.Equal(styles[index].Text, first[index].Text);
            Assert.Equal(styles[index].ForeColor, first[index].ForeColor);
            Assert.Equal(styles[index].BackColor, first[index].BackColor);
            Assert.Same(styles[index].Font, first[index].Font);
            Assert.Same(styles[index].Tag, first[index].Tag);
        }
        Assert.All(before.OfType<HiddenSubItem>(), hidden => Assert.Equal(0, hidden.Calls));
        Assert.True(font.GetHeight(96f) > 0);
    }

    [Fact]
    public void SharedPrimaryAndStyleMetadataChanges_KeepEachOwnersImageSelectionIndependent()
    {
        using var font = new Font("Arial", 11f);
        var first = new ListViewItem("first") { ImageKey = "first image", Font = font };
        SubItem shared = first.SubItems[0];
        var second = new ListViewItem { ImageIndex = 17 };
        new SubItems(second).Add(shared);
        Action[] operations = [
            () => first.Text = "new text", () => first.Name = "name", () => first.Tag = new PoisonTag(),
            () => first.ToolTipText = "tip", () => first.BackColor = Color.Red, () => first.ForeColor = Color.Blue,
            () => first.Font = null!, () => first.UseItemStyleForSubItems = false, () => shared.ResetStyle(),
            () => first.SubItems[0] = new SubItem(null, "replacement"), () => new SubItems(first).Clear(),
            () => _ = first.ToString(), () => _ = first.SubItems
        ];
        foreach (Action operation in operations)
        {
            operation();
            Assert.Equal(-1, first.ImageIndex);
            Assert.Equal("first image", first.ImageKey);
            Assert.Equal(17, second.ImageIndex);
            Assert.Equal(string.Empty, second.ImageKey);
        }
        second.ImageKey = "second image";
        Assert.Equal("first image", first.ImageKey);
        first.ImageIndex = 19;
        Assert.Equal("second image", second.ImageKey);
        Assert.Equal(-1, second.ImageIndex);
        Assert.True(font.GetHeight(96f) > 0);
    }

    [Fact]
    public void ImageOperations_IgnoreHiddenMembersAndPoisonTags()
    {
        using var font = new Font("Arial", 11f);
        var hidden = new HiddenItem();
        ListViewItem item = hidden;
        var tag = new PoisonTag();
        item.Tag = tag;
        var hiddenSubitem = new HiddenSubItem();
        SubItem primary = hiddenSubitem;
        var subTag = new PoisonTag();
        primary.Tag = subTag;
        primary.Font = font;
        new SubItems(item).Add(primary);

        foreach (string operation in Operations) Apply(item, operation);

        Assert.Equal(0, hidden.Calls);
        Assert.Equal(0, hiddenSubitem.Calls);
        Assert.Same(tag, item.Tag);
        Assert.Same(subTag, primary.Tag);
        Assert.Same(font, item.Font);
        Assert.True(font.GetHeight(96f) > 0);
    }

    [Fact]
    public void ImageOperations_DoNotUseOrReleaseBorrowedFontsIncludingDisposedInstances()
    {
        using var font = new Font("Arial", 11f);
        var item = new ListViewItem("primary") { Font = font };
        float height = font.GetHeight(96f);
        foreach (string operation in Operations)
        {
            Apply(item, operation);
            Assert.Same(font, item.Font);
            Assert.Equal(height, font.GetHeight(96f));
        }
        var disposed = new Font("Arial", 11f);
        item.Font = disposed;
        disposed.Dispose();
        foreach (string operation in Operations)
        {
            Apply(item, operation);
            Assert.Same(disposed, item.Font);
        }
    }

    private static ListViewItem CreatePopulated(string shape, Font font)
    {
        var item = new ListViewItem { Tag = new PoisonTag(), ToolTipText = "tip", UseItemStyleForSubItems = false };
        var view = new SubItems(item);
        var held = new SubItem(null, "primary", Color.Crimson, Color.AliceBlue, font) { Name = "primary", Tag = new PoisonTag() };
        view.Add(held);
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
        return item;
    }

    private static IEnumerator GetEnumerator(SubItems view, string consumer) => consumer switch
    {
        "interface" => ((IEnumerable)view).GetEnumerator(),
        "generic" => ((IEnumerable<SubItem>)view).GetEnumerator(),
        _ => view.GetEnumerator()
    };

    private static void Apply(ListViewItem item, string operation)
    {
        switch (operation)
        {
            case "read-index": _ = item.ImageIndex; break;
            case "read-key": _ = item.ImageKey; break;
            case "index-minus-one": item.ImageIndex = -1; break;
            case "index-zero": item.ImageIndex = 0; break;
            case "index-max": item.ImageIndex = int.MaxValue; break;
            case "index-invalid": Assert.Throws<ArgumentOutOfRangeException>(() => item.ImageIndex = -2); break;
            case "key-null": item.ImageKey = null!; break;
            case "key-empty": item.ImageKey = string.Empty; break;
            case "key-text": item.ImageKey = "target\0中文"; break;
            case "key-repeat": item.ImageKey = "before"; item.ImageKey = "before"; break;
            case "index-repeat": item.ImageIndex = 23; item.ImageIndex = 23; break;
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
