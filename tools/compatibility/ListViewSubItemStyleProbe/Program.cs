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
        using var sharedFont = new Font("Arial", 12.5f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
        using var otherFont = new Font("Consolas", 10f, FontStyle.Underline, GraphicsUnit.Point);
        var observations = new List<object>();
        var defaults = new ListViewItem.ListViewSubItem();
        Font defaultFont = defaults.Font;
        Observe("default", defaults, observations);
        Observe("owner-text-null", new ListViewItem.ListViewSubItem(null, null), observations);
        Observe("style-null-empty", new ListViewItem.ListViewSubItem(null, null, Color.Empty, Color.Empty, null), observations);
        Observe("style-null-custom", new ListViewItem.ListViewSubItem(null, "detail", Color.Crimson, Color.AliceBlue, sharedFont), observations, sharedFont);
        Observe("style-transparent", new ListViewItem.ListViewSubItem(null, "transparent", Color.Transparent, Color.FromArgb(0, 7, 8, 9), null), observations);

        var freshOwner = new ListViewItem();
        ObserveOwner("fresh-owner", freshOwner, sharedFont, observations);
        var populatedOwner = new ListViewItem(new[] { "row", "detail" });
        populatedOwner.ForeColor = Color.DarkMagenta;
        ObserveOwner("populated-owner", populatedOwner, sharedFont, observations);

        var item = new ListViewItem.ListViewSubItem { Text = "stable text", Name = "stable name", Tag = new object() };
        object tag = item.Tag;
        item.ForeColor = Color.FromArgb(123, 11, 22, 33);
        Observe("set-fore", item, observations);
        item.BackColor = Color.FromArgb(67, 44, 55, 66);
        Observe("set-back", item, observations);
        item.Font = sharedFont;
        Observe("set-font", item, observations, sharedFont);
        item.ForeColor = Color.Empty;
        Observe("empty-fore", item, observations, sharedFont);
        item.BackColor = Color.Empty;
        Observe("empty-back", item, observations, sharedFont);
        item.Font = null;
        Observe("null-font", item, observations);
        observations.Add(new { scenario = "null-font-default-identity", sameDefault = ReferenceEquals(defaultFont, item.Font) });
        item.ForeColor = Color.Green;
        item.BackColor = Color.Yellow;
        item.Font = sharedFont;
        item.ResetStyle();
        Observe("reset-style", item, observations);
        observations.Add(new { scenario = "reset-preserves-data", item.Text, item.Name, sameTag = ReferenceEquals(tag, item.Tag), sameDefault = ReferenceEquals(defaultFont, item.Font) });
        item.ResetStyle();
        Observe("reset-style-twice", item, observations);

        var first = new ListViewItem.ListViewSubItem(null, "first", Color.Red, Color.Blue, sharedFont);
        var second = new ListViewItem.ListViewSubItem(null, "second", Color.Green, Color.Yellow, sharedFont);
        first.ResetStyle();
        observations.Add(new { scenario = "reset-shared-font-lifetime", sameOtherFont = ReferenceEquals(sharedFont, second.Font), height = sharedFont.GetHeight(96f), otherHeight = second.Font.GetHeight(96f) });
        first.Font = sharedFont;
        first.Font = null;
        observations.Add(new { scenario = "null-shared-font-lifetime", sameOtherFont = ReferenceEquals(sharedFont, second.Font), height = sharedFont.GetHeight(96f) });
        first.Font = sharedFont;
        first.Font = otherFont;
        observations.Add(new { scenario = "replace-shared-font-lifetime", sameReplacement = ReferenceEquals(otherFont, first.Font), sameOtherFont = ReferenceEquals(sharedFont, second.Font), height = sharedFont.GetHeight(96f) });
        second.ResetStyle();
        observations.Add(new { scenario = "reset-last-shared-font-lifetime", height = sharedFont.GetHeight(96f), sameDefault = ReferenceEquals(defaultFont, second.Font) });

        var derived = new DerivedSubItem(populatedOwner, "derived", Color.Orange, Color.Navy, sharedFont);
        Observe("derived-style", derived, observations, sharedFont);
        derived.ResetStyle();
        Observe("derived-reset", derived, observations);

        object? referenceDefaults = null;
#if REFERENCE
        referenceDefaults = new
        {
            controlDefault = DescribeFont(Control.DefaultFont), systemDefault = DescribeFont(SystemFonts.DefaultFont),
            messageBox = DescribeFont(SystemFonts.MessageBoxFont!),
            sameControlDefault = ReferenceEquals(defaultFont, Control.DefaultFont),
            sameSystemDefault = ReferenceEquals(defaultFont, SystemFonts.DefaultFont),
            sameMessageBox = ReferenceEquals(defaultFont, SystemFonts.MessageBoxFont)
        };
#endif
        Console.WriteLine(JsonSerializer.Serialize(new { version, referenceDefaults, observations }));
    }

    private static void ObserveOwner(string scenario, ListViewItem owner, Font font, List<object> observations)
    {
        var subItems = owner.SubItems;
        var primary = subItems[0];
        string text = owner.Text;
        int count = subItems.Count;
        var implicitStyle = new ListViewItem.ListViewSubItem(owner, "implicit detail");
        Observe(scenario + "-implicit", implicitStyle, observations);
        var emptyStyle = new ListViewItem.ListViewSubItem(owner, "empty detail", Color.Empty, Color.Empty, null);
        Observe(scenario + "-empty", emptyStyle, observations);
        var styled = new ListViewItem.ListViewSubItem(owner, "styled detail", Color.IndianRed, Color.LightCyan, font);
        Observe(scenario + "-custom", styled, observations, font);
        styled.ResetStyle();
        Observe(scenario + "-reset", styled, observations);
        observations.Add(new
        {
            scenario = scenario + "-owner-stability", sameCollection = ReferenceEquals(subItems, owner.SubItems),
            samePrimary = ReferenceEquals(primary, owner.SubItems[0]), sameText = text == owner.Text,
            sameCount = count == owner.SubItems.Count, member = owner.SubItems.Contains(styled),
            styled.Text, styled.Name, tagNull = styled.Tag is null
        });
        owner.SubItems.Add(styled);
        styled.ForeColor = Color.Coral;
        styled.BackColor = Color.Beige;
        styled.Font = font;
        Observe(scenario + "-member-custom", styled, observations, font);
        styled.ResetStyle();
        Observe(scenario + "-member-reset", styled, observations);
        observations.Add(new { scenario = scenario + "-member-stability", samePrimary = ReferenceEquals(primary, owner.SubItems[0]), sameText = text == owner.Text, count = owner.SubItems.Count, sameMember = ReferenceEquals(styled, owner.SubItems[count]) });
    }

    private static void Observe(string scenario, ListViewItem.ListViewSubItem item, List<object> observations, Font? expectedFont = null)
    {
        observations.Add(new
        {
            scenario, item.Text, item.Name, tagNull = item.Tag is null,
            foreColor = DescribeColor(item.ForeColor), backColor = DescribeColor(item.BackColor),
            font = DescribeFont(item.Font), sameReadFont = ReferenceEquals(item.Font, item.Font),
            sameSuppliedFont = expectedFont is not null && ReferenceEquals(expectedFont, item.Font)
        });
    }

    private static object DescribeColor(Color color) => new { color.IsEmpty, color.IsNamedColor, color.IsKnownColor, color.Name, argb = color.ToArgb() };

    private static object DescribeFont(Font font) => new { font.Name, font.Size, style = (int)font.Style, unit = (int)font.Unit, font.GdiCharSet, font.GdiVerticalFont };

    private sealed class DerivedSubItem : ListViewItem.ListViewSubItem
    {
        internal DerivedSubItem(ListViewItem? owner, string? text, Color foreColor, Color backColor, Font? font) : base(owner, text, foreColor, backColor, font)
        {
        }
    }
}
