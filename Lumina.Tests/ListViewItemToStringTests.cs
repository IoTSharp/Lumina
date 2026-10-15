using System.Collections;
using System.Drawing;
using System.Reflection;
using Lumina.Forms;
using Xunit;
using SubItem = Lumina.Forms.ListViewItem.ListViewSubItem;
using SubItems = Lumina.Forms.ListViewItem.ListViewSubItemCollection;

namespace Lumina.Tests;

public class ListViewItemToStringTests
{
    [Fact]
    public void ToString_OverridesTheObjectSlotWithTheStandardSignature()
    {
        MethodInfo method = Assert.Single(typeof(ListViewItem).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly), entry => entry.Name == nameof(ToString));

        Assert.Equal(typeof(string), method.ReturnType);
        Assert.Empty(method.GetParameters());
        Assert.Empty(method.GetGenericArguments());
        Assert.Empty(method.CustomAttributes);
        Assert.True(method.IsPublic);
        Assert.True(method.IsVirtual);
        Assert.True(method.IsHideBySig);
        Assert.False(method.IsFinal);
        Assert.False(method.IsStatic);
        Assert.Equal((MethodAttributes)0, method.Attributes & MethodAttributes.NewSlot);
        Assert.Equal(typeof(object), method.GetBaseDefinition().DeclaringType);
    }

    public static IEnumerable<object?[]> TextCases()
    {
        string?[] texts = [null, "", "plain", "中文列表项", "emoji 😀", "two\nlines", "two\r\nlines", "\t spaced \t", "{braces}: text", "nul\0tail", "אבג العربية", "e\u0301"];
        foreach (string creation in new[] { "string", "array", "setter", "inherited" })
        foreach (string? text in texts)
            yield return [creation, text];
    }

    [Theory]
    [MemberData(nameof(TextCases))]
    public void ToString_UsesNormalizedPrimaryTextWithoutEscapingOrTheDerivedTypeName(string creation, string? text)
    {
        ListViewItem item = creation switch
        {
            "string" => new ListViewItem(text),
            "array" => new ListViewItem(new[] { text, "detail" }),
            "setter" => new ListViewItem { Text = text },
            _ => new InheritedItem(text)
        };
        var view = new SubItems(item);
        int countBefore = view.Count;
        string normalizedText = item.Text;
        string expected = "ListViewItem: {" + text + "}";

        Assert.Equal(expected, item.ToString());
        Assert.Equal(expected, ((object)item).ToString());
        Assert.Equal(expected, $"{item}");
        Assert.Equal(expected, string.Concat(item));
        Assert.Equal(text ?? string.Empty, item.Text);
        Assert.Same(normalizedText, item.Text);
        Assert.Equal(countBefore, view.Count);
    }

    [Theory]
    [InlineData("default")]
    [InlineData("null-array")]
    [InlineData("empty-array")]
    public void ToString_FormatsUninitializedPrimaryTextWithoutCreatingASubitem(string creation)
    {
        ListViewItem item = creation switch
        {
            "null-array" => new ListViewItem((string?[]?)null),
            "empty-array" => new ListViewItem(Array.Empty<string?>()),
            _ => new ListViewItem()
        };
        var view = new SubItems(item);

        Assert.Equal("ListViewItem: {}", item.ToString());
        Assert.Equal("ListViewItem: {}", ((object)item).ToString());
        Assert.Empty(view);
        Assert.Equal(string.Empty, item.Text);
    }

    [Fact]
    public void ToString_ReadsTheCurrentPrimaryTextAfterEveryUpdate()
    {
        var item = new ListViewItem("original");
        SubItem original = item.SubItems[0];
        Assert.Equal("ListViewItem: {original}", item.ToString());

        item.Text = "changed 中文\r\n\0tail";
        Assert.Equal("ListViewItem: {changed 中文\r\n\0tail}", item.ToString());
        original.Text = "changed through first";
        Assert.Equal("ListViewItem: {changed through first}", item.ToString());
        item.Text = null;
        Assert.Equal("ListViewItem: {}", item.ToString());
        item.Text = string.Empty;
        Assert.Equal("ListViewItem: {}", item.ToString());
        item.Text = "original";
        Assert.Equal("ListViewItem: {original}", item.ToString());
        Assert.Same(original, item.SubItems[0]);
    }

    [Fact]
    public void ToString_DoesNotConsumeOrChangeNameTagAndToolTipText()
    {
        string text = new("primary text".ToCharArray());
        string name = new("primary name 中文".ToCharArray());
        string tooltip = new("tooltip\r\n\0text".ToCharArray());
        var tag = new PoisonTag();
        var primaryTag = new PoisonTag();
        var item = new ListViewItem(text) { Name = name, Tag = tag, ToolTipText = tooltip };
        SubItem primary = item.SubItems[0];
        primary.Tag = primaryTag;

        Assert.Equal("ListViewItem: {primary text}", item.ToString());
        Assert.Equal("ListViewItem: {primary text}", item.ToString());
        Assert.Same(text, item.Text);
        Assert.Same(name, item.Name);
        Assert.Same(name, primary.Name);
        Assert.Same(tooltip, item.ToolTipText);
        Assert.Same(tag, item.Tag);
        Assert.Same(primaryTag, primary.Tag);

        item.Name = null!;
        item.ToolTipText = null!;
        item.Tag = null;
        Assert.Equal("ListViewItem: {primary text}", item.ToString());
        Assert.Equal(string.Empty, item.Name);
        Assert.Equal(string.Empty, item.ToolTipText);
        Assert.Null(item.Tag);
        Assert.Equal(0, tag.Reads);
        Assert.Equal(0, tag.Disposals);
        Assert.Equal(0, primaryTag.Reads);
        Assert.Equal(0, primaryTag.Disposals);
    }

    [Fact]
    public void ToString_PreservesPrimaryStylesAndTheBorrowedFontLifetime()
    {
        using var font = new Font("Arial", 11.5f, FontStyle.Bold, GraphicsUnit.Point);
        float height = font.GetHeight(96f);
        var item = new ListViewItem("styled");
        SubItem primary = item.SubItems[0];
        primary.ForeColor = Color.Crimson;
        primary.BackColor = Color.AliceBlue;
        primary.Font = font;

        Assert.Equal("ListViewItem: {styled}", item.ToString());
        Assert.Equal(Color.Crimson, primary.ForeColor);
        Assert.Equal(Color.AliceBlue, primary.BackColor);
        Assert.Same(font, primary.Font);
        Assert.Equal(height, font.GetHeight(96f));

        primary.ResetStyle();
        Assert.Equal("ListViewItem: {styled}", item.ToString());
        Assert.Equal(SystemColors.WindowText, primary.ForeColor);
        Assert.Equal(SystemColors.Window, primary.BackColor);
        Assert.Equal(height, font.GetHeight(96f));

        item.ForeColor = Color.Green;
        primary.ForeColor = Color.Green;
        primary.BackColor = Color.Yellow;
        primary.Font = font;
        Assert.Equal("ListViewItem: {styled}", item.ToString());
        Assert.Equal(Color.Green, item.ForeColor);
        Assert.Equal(Color.Green, primary.ForeColor);
        Assert.Equal(Color.Yellow, primary.BackColor);
        Assert.Same(font, primary.Font);
        Assert.Equal(height, font.GetHeight(96f));
    }

    [Fact]
    public void ToString_DoesNotConsumeADisposedBorrowedFont()
    {
        using var font = new Font("Arial", 10f);
        var item = new ListViewItem("text");
        SubItem primary = item.SubItems[0];
        primary.Font = font;
        font.Dispose();

        Assert.Equal("ListViewItem: {text}", item.ToString());
        Assert.Same(font, primary.Font);
    }

    public static IEnumerable<object[]> EmptyCollectionCases()
    {
        foreach (string shape in new[] { "fresh", "property-cleared", "external-cleared", "last-removed" })
        foreach (string consumer in new[] { "public", "interface", "generic" })
            yield return [shape, consumer];
    }

    [Theory]
    [MemberData(nameof(EmptyCollectionCases))]
    public void ToString_OnAnEmptyTablePreservesAllEnumeratorsAndDefersPrimaryRestoration(string shape, string consumer)
    {
        ListViewItem item = EmptyItem(shape);
        var first = new SubItems(item);
        var second = new SubItems(item);
        IEnumerator enumerator = GetEnumerator(first, consumer);
        Assert.False(enumerator.MoveNext());

        Assert.Equal("ListViewItem: {}", item.ToString());
        Assert.Equal("ListViewItem: {}", ((object)item).ToString());

        Assert.Empty(first);
        Assert.Empty(second);
        Assert.Equal(string.Empty, item.Name);
        Assert.Equal(string.Empty, item.Text);
        Assert.False(enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => _ = enumerator.Current);
        enumerator.Reset();
        Assert.False(enumerator.MoveNext());

        _ = item.SubItems;
        Assert.Single(first);
        Assert.Same(first[0], second[0]);
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.False(enumerator.MoveNext());
        first.Add("later");
        Assert.False(enumerator.MoveNext());
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("property-cleared")]
    [InlineData("external-cleared")]
    [InlineData("last-removed")]
    public void ToString_DoesNotReplaceTheEmptyTableObservedByAnExistingEnumerator(string shape)
    {
        ListViewItem item = EmptyItem(shape);
        var view = new SubItems(item);
        IEnumerator enumerator = view.GetEnumerator();

        Assert.Equal("ListViewItem: {}", item.ToString());
        view.Add("added through the existing view");

        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
        Assert.Equal("added through the existing view", item.Text);
    }

    public static IEnumerable<object[]> PopulatedCollectionCases()
    {
        foreach (string shape in new[] { "populated", "duplicates", "removed-first", "replaced-first", "cross-owner", "derived-first" })
        foreach (string consumer in new[] { "public", "interface", "generic" })
            yield return [shape, consumer];
    }

    [Theory]
    [MemberData(nameof(PopulatedCollectionCases))]
    public void ToString_PreservesCurrentSharedSubitemsAndEveryEnumerationPath(string shape, string consumer)
    {
        var item = new ListViewItem();
        var first = new SubItems(item);
        var second = new SubItems(item);
        var primary = new SubItem(null, "primary");
        first.Add(primary);
        first.Add(new SubItem(null, "detail"));
        first.Add(new SubItem(null, "other"));
        if (shape == "duplicates") first.Add(primary);
        if (shape == "removed-first") first.RemoveAt(0);
        if (shape == "replaced-first") first[0] = new SubItem(null, "replacement");
        if (shape == "cross-owner") new SubItems(new ListViewItem()).Add(primary);
        HiddenTextSubItem? hidden = null;
        if (shape == "derived-first")
        {
            hidden = new HiddenTextSubItem();
            ((SubItem)hidden).Text = "base primary";
            first[0] = hidden;
        }
        SubItem[] before = Enumerable.Range(0, first.Count).Select(index => first[index]).ToArray();
        string text = item.Text;
        IEnumerator enumerator = GetEnumerator(first, consumer);
        Assert.True(enumerator.MoveNext());
        Assert.Same(before[0], enumerator.Current);

        Assert.Equal("ListViewItem: {" + text + "}", item.ToString());
        Assert.Equal("ListViewItem: {" + text + "}", ((object)item).ToString());

        Assert.Equal(before.Length, first.Count);
        Assert.Equal(before.Length, second.Count);
        Assert.Same(text, item.Text);
        for (int index = 0; index < before.Length; index++)
        {
            Assert.Same(before[index], first[index]);
            Assert.Same(before[index], second[index]);
            Assert.Same(before[index], enumerator.Current);
            if (index < before.Length - 1) Assert.True(enumerator.MoveNext());
        }
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.True(enumerator.MoveNext());
        Assert.Same(before[0], enumerator.Current);
        if (hidden is not null) Assert.Equal(0, hidden.HiddenReads);
    }

    [Fact]
    public void ToString_PreservesEachViewsIndependentKeyCache()
    {
        var item = new ListViewItem(new[] { "primary", "detail", "other" });
        var first = item.SubItems;
        var second = new SubItems(item);
        first[0].Name = "primary";
        first[1].Name = "target";
        first[2].Name = "other";
        Assert.Equal(1, first.IndexOfKey("target"));
        Assert.Equal(2, second.IndexOfKey("other"));
        first[0].Name = "target";
        first[1].Name = "other";

        Assert.Equal("ListViewItem: {primary}", item.ToString());
        Assert.Equal("ListViewItem: {primary}", item.ToString());

        Assert.Equal(1, first.IndexOfKey("other"));
        Assert.Equal(2, second.IndexOfKey("other"));
        Assert.Equal(0, first.IndexOfKey("target"));
        Assert.Equal(2, second.IndexOfKey("other"));
    }

    [Fact]
    public void ToString_ReadsASharedPrimaryObjectWithoutChangingAnyOwnersCollection()
    {
        var first = new ListViewItem();
        var firstView = new SubItems(first);
        var second = new ListViewItem();
        var secondView = new SubItems(second);
        var shared = new SubItem(null, "shared primary");
        firstView.Add(shared);
        firstView.Add(shared);
        secondView.Add(shared);

        Assert.Equal("ListViewItem: {shared primary}", first.ToString());
        Assert.Equal("ListViewItem: {shared primary}", second.ToString());
        shared.Text = "changed shared primary";
        Assert.Equal("ListViewItem: {changed shared primary}", first.ToString());
        Assert.Equal("ListViewItem: {changed shared primary}", second.ToString());
        Assert.Equal(2, firstView.Count);
        Assert.Single(secondView);
        Assert.Same(shared, firstView[0]);
        Assert.Same(shared, firstView[1]);
        Assert.Same(shared, secondView[0]);
    }

    [Fact]
    public void ToString_UsesBaseTextWithoutReadingHiddenDerivedProperties()
    {
        var item = new HiddenMembersItem();
        ((ListViewItem)item).Text = "base text";

        Assert.Equal("ListViewItem: {base text}", item.ToString());
        Assert.Equal("ListViewItem: {base text}", ((ListViewItem)item).ToString());
        Assert.Equal(0, item.HiddenReads);
    }

    [Fact]
    public void ToString_DispatchesOverridesThroughBaseObjectAndFormattingReferences()
    {
        var item = new OverridingItem();

        Assert.Equal("override(ListViewItem: {derived text})", item.ToString());
        Assert.Equal("override(ListViewItem: {derived text})", ((ListViewItem)item).ToString());
        Assert.Equal("override(ListViewItem: {derived text})", ((object)item).ToString());
        Assert.Equal("override(ListViewItem: {derived text})", $"{item}");
        Assert.Equal("override(ListViewItem: {derived text})", string.Concat(item));
        Assert.Equal(5, item.Calls);
        Assert.Equal("ListViewItem: {derived text}", item.CallBase());
        Assert.Equal(5, item.Calls);
    }

    [Fact]
    public void ToString_UsesTheVirtualBaseSlotWhenADerivedClassHidesTheMethod()
    {
        var item = new HiddenStringItem();

        Assert.Equal("hidden string", item.ToString());
        Assert.Equal("ListViewItem: {base hidden text}", ((ListViewItem)item).ToString());
        Assert.Equal("ListViewItem: {base hidden text}", ((object)item).ToString());
        Assert.Equal("ListViewItem: {base hidden text}", $"{item}");
        Assert.Equal(1, item.Calls);
    }

    [Theory]
    [InlineData("base")]
    [InlineData("object")]
    [InlineData("interpolation")]
    public void ToString_PropagatesTheOriginalDerivedException(string consumer)
    {
        var expected = new InvalidOperationException("original derived failure");
        var item = new ThrowingItem(expected);

        InvalidOperationException actual = Assert.Throws<InvalidOperationException>(() =>
        {
            _ = consumer switch
            {
                "base" => ((ListViewItem)item).ToString(),
                "object" => ((object)item).ToString(),
                _ => $"{item}"
            };
        });

        Assert.Same(expected, actual);
        Assert.Equal(1, item.Calls);
    }

    private static ListViewItem EmptyItem(string shape)
    {
        var item = new ListViewItem();
        if (shape == "property-cleared") item.SubItems.Clear();
        if (shape is "external-cleared" or "last-removed")
        {
            var view = new SubItems(item);
            view.Add("primary");
            if (shape == "external-cleared") view.Clear();
            else view.RemoveAt(0);
        }
        return item;
    }

    private static IEnumerator GetEnumerator(SubItems items, string consumer) => consumer switch
    {
        "interface" => ((IEnumerable)items).GetEnumerator(),
        "generic" => ((IEnumerable<SubItem>)items).GetEnumerator(),
        _ => items.GetEnumerator()
    };

    private sealed class InheritedItem(string? text) : ListViewItem(text)
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("ToString must not call Equals.");
        public override int GetHashCode() => throw new InvalidOperationException("ToString must not call GetHashCode.");
    }

    private sealed class OverridingItem() : ListViewItem("derived text")
    {
        public int Calls { get; private set; }
        public override string ToString() { Calls++; return "override(" + base.ToString() + ")"; }
        public string CallBase() => base.ToString();
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

    private sealed class PoisonTag : IDisposable
    {
        public int Reads { get; private set; }
        public int Disposals { get; private set; }
        public override string ToString() { Reads++; throw new InvalidOperationException("ListViewItem.ToString must not format Tag."); }
        public void Dispose() { Disposals++; throw new InvalidOperationException("ListViewItem.ToString must not dispose Tag."); }
    }
}
