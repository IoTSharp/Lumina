using System.Collections;
using System.Drawing;
using System.Reflection;
using Lumina.Forms;
using Xunit;
using SubItem = Lumina.Forms.ListViewItem.ListViewSubItem;
using SubItems = Lumina.Forms.ListViewItem.ListViewSubItemCollection;

namespace Lumina.Tests;

public class ListViewSubItemToStringTests
{
    [Fact]
    public void ToString_OverridesTheObjectSlotWithTheStandardSignature()
    {
        MethodInfo method = Assert.Single(typeof(SubItem).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly), entry => entry.Name == nameof(ToString));

        Assert.Equal(typeof(string), method.ReturnType);
        Assert.Empty(method.GetParameters());
        Assert.Empty(method.GetGenericArguments());
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
        string?[] texts = [null, "", "plain", "中文子项", "emoji 😀", "two\nlines", "two\r\nlines", "\t spaced \t", "{braces}: text", "nul\0tail", "אבג العربية", "e\u0301"];
        foreach (string creation in new[] { "constructor", "setter", "inherited" })
        foreach (string? text in texts)
            yield return [creation, text];
    }

    [Theory]
    [MemberData(nameof(TextCases))]
    public void ToString_FormatsNormalizedTextWithoutEscapingOrUsingTheDerivedTypeName(string creation, string? text)
    {
        SubItem item = creation switch
        {
            "constructor" => new SubItem(null, text),
            "setter" => new SubItem { Text = text },
            _ => new InheritedSubItem(text)
        };
        string expected = "ListViewSubItem: {" + text + "}";

        Assert.Equal(expected, item.ToString());
        Assert.Equal(expected, ((object)item).ToString());
        Assert.Equal(expected, $"{item}");
        Assert.Equal(expected, string.Concat(item));
        Assert.Equal(text ?? string.Empty, item.Text);
    }

    [Fact]
    public void ToString_FormatsTheParameterlessDefaultAsEmptyBraces()
    {
        Assert.Equal("ListViewSubItem: {}", new SubItem().ToString());
    }

    [Fact]
    public void ToString_ReadsTheLatestTextAndDoesNotCacheEarlierResults()
    {
        var item = new SubItem(null, "original");
        Assert.Equal("ListViewSubItem: {original}", item.ToString());

        item.Text = "updated 中文\r\ntext";
        Assert.Equal("ListViewSubItem: {updated 中文\r\ntext}", item.ToString());
        item.Text = null;
        Assert.Equal("ListViewSubItem: {}", item.ToString());
        item.Text = string.Empty;
        Assert.Equal("ListViewSubItem: {}", item.ToString());
        item.Text = "original";
        Assert.Equal("ListViewSubItem: {original}", item.ToString());
    }

    [Fact]
    public void ToString_DoesNotReadOrChangeNameAndTag()
    {
        string text = new string("text".ToCharArray());
        string name = new string("name 中文".ToCharArray());
        var tag = new PoisonTag();
        var item = new SubItem(null, text) { Name = name, Tag = tag };

        Assert.Equal("ListViewSubItem: {text}", item.ToString());
        Assert.Equal("ListViewSubItem: {text}", item.ToString());
        Assert.Same(text, item.Text);
        Assert.Same(name, item.Name);
        Assert.Same(tag, item.Tag);

        item.Name = null;
        item.Tag = null;
        Assert.Equal("ListViewSubItem: {text}", item.ToString());
        Assert.Equal(string.Empty, item.Name);
        Assert.Null(item.Tag);
    }

    [Fact]
    public void ToString_PreservesStylesAndTheBorrowedFontLifetime()
    {
        using var font = new Font("Arial", 11.5f, FontStyle.Bold, GraphicsUnit.Point);
        float height = font.GetHeight(96f);
        var item = new SubItem(null, "styled", Color.Crimson, Color.AliceBlue, font);

        Assert.Equal("ListViewSubItem: {styled}", item.ToString());
        Assert.Equal(Color.Crimson, item.ForeColor);
        Assert.Equal(Color.AliceBlue, item.BackColor);
        Assert.Same(font, item.Font);
        Assert.Equal(height, font.GetHeight(96f));

        item.ForeColor = Color.Empty;
        item.BackColor = Color.Transparent;
        Assert.Equal("ListViewSubItem: {styled}", item.ToString());
        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Equal(Color.Transparent, item.BackColor);
        Assert.Same(font, item.Font);

        item.ResetStyle();
        Assert.Equal("ListViewSubItem: {styled}", item.ToString());
        Assert.Equal(SystemColors.WindowText, item.ForeColor);
        Assert.Equal(SystemColors.Window, item.BackColor);
        Assert.Equal(height, font.GetHeight(96f));

        item.Font = font;
        Assert.Equal("ListViewSubItem: {styled}", item.ToString());
        Assert.Same(font, item.Font);
        Assert.Equal(height, font.GetHeight(96f));
    }

    [Fact]
    public void ToString_DoesNotConsumeADisposedBorrowedFont()
    {
        using var font = new Font("Arial", 10f);
        var item = new SubItem(null, "text", Color.Red, Color.Blue, font);
        font.Dispose();

        Assert.Equal("ListViewSubItem: {text}", item.ToString());
        Assert.Same(font, item.Font);
    }

    [Theory]
    [InlineData("fresh")]
    [InlineData("property-cleared")]
    [InlineData("last-removed")]
    public void ToString_DoesNotRestoreThePrimarySubitemOrInvalidateAnEmptyEnumerator(string shape)
    {
        var owner = new ListViewItem();
        var first = new SubItems(owner);
        var second = new SubItems(owner);
        var item = new SubItem(owner, "detached");
        if (shape == "property-cleared") owner.SubItems.Clear();
        if (shape == "last-removed") { first.Add(item); first.RemoveAt(0); }
        IEnumerator enumerator = first.GetEnumerator();

        Assert.Equal("ListViewSubItem: {detached}", item.ToString());
        Assert.Equal("ListViewSubItem: {detached}", ((object)item).ToString());

        Assert.Equal(0, first.Count);
        Assert.Equal(0, second.Count);
        Assert.Equal(string.Empty, owner.Text);
        Assert.False(enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => _ = enumerator.Current);
        enumerator.Reset();
        Assert.False(enumerator.MoveNext());
        Assert.Equal(0, first.Count);
    }

    public static IEnumerable<object[]> OwnerCases()
    {
        foreach (string shape in new[] { "populated", "duplicates", "removed", "cross-owner" })
        foreach (string consumer in new[] { "public", "interface", "generic" })
            yield return [shape, consumer];
    }

    [Theory]
    [MemberData(nameof(OwnerCases))]
    public void ToString_PreservesSharedViewsDuplicateIdentityAndExistingEnumeration(string shape, string consumer)
    {
        var owner = new ListViewItem();
        var first = new SubItems(owner);
        var second = new SubItems(owner);
        var item = new InheritedSubItem("detail");
        first.Add(new SubItem(null, "primary"));
        first.Add(item);
        first.Add(new SubItem(null, "other"));
        if (shape == "duplicates") first.Add(item);
        if (shape == "removed") first.Remove(item);
        if (shape == "cross-owner") new SubItems(new ListViewItem()).Add(item);
        SubItem[] before = Enumerable.Range(0, first.Count).Select(index => first[index]).ToArray();
        IEnumerator enumerator = consumer switch
        {
            "interface" => ((IEnumerable)first).GetEnumerator(),
            "generic" => ((IEnumerable<SubItem>)first).GetEnumerator(),
            _ => first.GetEnumerator()
        };
        Assert.True(enumerator.MoveNext());
        Assert.Same(before[0], enumerator.Current);

        Assert.Equal("ListViewSubItem: {detail}", item.ToString());
        foreach (SubItem entry in before)
            Assert.Equal("ListViewSubItem: {" + entry.Text + "}", entry.ToString());

        Assert.Equal("primary", owner.Text);
        Assert.Equal(before.Length, first.Count);
        Assert.Equal(before.Length, second.Count);
        for (int index = 0; index < before.Length; index++)
        {
            Assert.Same(before[index], first[index]);
            Assert.Same(before[index], second[index]);
        }
        Assert.Same(before[0], enumerator.Current);
        for (int index = 1; index < before.Length; index++)
        {
            Assert.True(enumerator.MoveNext());
            Assert.Same(before[index], enumerator.Current);
        }
        Assert.False(enumerator.MoveNext());
        enumerator.Reset();
        Assert.True(enumerator.MoveNext());
        Assert.Same(before[0], enumerator.Current);
    }

    [Fact]
    public void ToString_PreservesEachViewsIndependentKeyCache()
    {
        var owner = new ListViewItem(new[] { "primary", "detail", "other" });
        var first = owner.SubItems;
        var second = new SubItems(owner);
        first[0].Name = "primary";
        first[1].Name = "target";
        first[2].Name = "other";
        Assert.Equal(1, first.IndexOfKey("target"));
        Assert.Equal(2, second.IndexOfKey("other"));
        first[0].Name = "target";
        first[1].Name = "other";

        foreach (SubItem entry in first) _ = entry.ToString();

        Assert.Equal(1, first.IndexOfKey("other"));
        Assert.Equal(2, second.IndexOfKey("other"));
        Assert.Equal(0, first.IndexOfKey("target"));
        Assert.Equal(2, second.IndexOfKey("other"));
    }

    [Fact]
    public void ToString_UsesBaseTextWithoutReadingHiddenDerivedMembers()
    {
        var item = new ShadowedSubItem();
        ((SubItem)item).Text = "base text";

        Assert.Equal("ListViewSubItem: {base text}", item.ToString());
        Assert.Equal(0, item.HiddenReads);
        Assert.Equal("base text", ((SubItem)item).Text);
    }

    [Fact]
    public void ToString_DispatchesDerivedOverridesThroughBaseObjectAndCollectionReferences()
    {
        var item = new OverridingSubItem();
        var items = new SubItems(new ListViewItem());
        items.Add(item);
        items.Add(item);

        Assert.Equal("override(ListViewSubItem: {derived text})", item.ToString());
        Assert.Equal("override(ListViewSubItem: {derived text})", ((SubItem)item).ToString());
        Assert.Equal("override(ListViewSubItem: {derived text})", ((object)item).ToString());
        Assert.Equal("override(ListViewSubItem: {derived text})", items[0].ToString());
        Assert.Equal("override(ListViewSubItem: {derived text})", items[1].ToString());
        Assert.Equal(5, item.Calls);
        Assert.Equal("ListViewSubItem: {derived text}", item.CallBase());
        Assert.Equal(5, item.Calls);
        Assert.Equal(2, items.Count);
        Assert.Same(items[0], items[1]);
    }

    [Theory]
    [InlineData("base")]
    [InlineData("object")]
    [InlineData("interpolation")]
    public void ToString_PropagatesTheOriginalDerivedException(string consumer)
    {
        var expected = new InvalidOperationException("original derived failure");
        var item = new ThrowingSubItem(expected);

        InvalidOperationException actual = Assert.Throws<InvalidOperationException>(() =>
        {
            _ = consumer switch
            {
                "base" => ((SubItem)item).ToString(),
                "object" => ((object)item).ToString(),
                _ => $"{item}"
            };
        });

        Assert.Same(expected, actual);
        Assert.Equal(1, item.Calls);
    }

    private sealed class InheritedSubItem(string? text) : SubItem(null, text)
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("ToString must not call Equals.");
        public override int GetHashCode() => throw new InvalidOperationException("ToString must not call GetHashCode.");
    }

    private sealed class OverridingSubItem() : SubItem(null, "derived text")
    {
        public int Calls { get; private set; }
        public override string ToString() { Calls++; return "override(" + base.ToString() + ")"; }
        public string CallBase() => base.ToString();
    }

    private sealed class ThrowingSubItem(Exception expected) : SubItem
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
