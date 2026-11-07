using System.Reflection;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class TextBoxWordBoundaryTests
{
    // Bind the internal helper without adding a public API or friend assembly.
    private static readonly Type s_clientUtils = typeof(Control).Assembly
        .GetType("Lumina.Forms.ClientUtils", throwOnError: true)!;
    private static readonly Func<string, int, int> s_boundaryStart = s_clientUtils
        .GetMethod("GetWordBoundaryStart", BindingFlags.Static | BindingFlags.NonPublic)!
        .CreateDelegate<Func<string, int, int>>();

    [Theory]
    [InlineData("", 0, 0)]
    [InlineData("abc", 0, 0)]
    [InlineData("abc", 1, 0)]
    [InlineData("abc", 2, 0)]
    [InlineData("abc", 3, 0)]
    [InlineData("abc def", 7, 4)]
    [InlineData("abc def", 4, 0)]
    [InlineData("abc  def!", 9, 5)]
    [InlineData("abc  def", 5, 0)]
    [InlineData(" abc", 4, 0)]
    [InlineData("  abc", 5, 2)]
    [InlineData(".abc", 4, 0)]
    [InlineData("..abc", 5, 2)]
    [InlineData("a.b", 3, 2)]
    [InlineData("ab_c", 4, 3)]
    [InlineData("a_bc", 4, 2)]
    [InlineData("_abc", 4, 0)]
    [InlineData("ab__", 4, 0)]
    [InlineData("ab__cd", 6, 4)]
    [InlineData("one,two", 7, 4)]
    [InlineData("one,two!", 8, 4)]
    [InlineData("one!!! two", 10, 7)]
    [InlineData("one!!!", 6, 0)]
    [InlineData("!!!", 3, 0)]
    [InlineData(" ", 1, 0)]
    [InlineData("\t\r\n", 3, 0)]
    [InlineData("ab\r\ncd", 6, 4)]
    [InlineData("ab\r\n", 4, 0)]
    [InlineData("ab\u00A0cd", 5, 3)]
    [InlineData("ab\u200Bcd", 5, 3)]
    [InlineData("你好 世界", 5, 3)]
    [InlineData("你好世界", 4, 0)]
    [InlineData("e\u0301x", 3, 0)]
    [InlineData("a \u0301", 3, 2)]
    [InlineData("\u0301ab", 3, 0)]
    [InlineData("ab\u0903cd", 5, 3)]
    [InlineData("ab\u20DDcd", 5, 3)]
    [InlineData("ab\u0661cd", 5, 0)]
    [InlineData("ab\u203Fcd", 5, 3)]
    [InlineData("a\uD83D\uDE00b", 4, 3)]
    [InlineData("a\uD83D\uDE00b", 3, 3)]
    [InlineData("a\uD83D\uDE00b", 2, 2)]
    [InlineData("\uD83D\uDE00abc", 5, 2)]
    [InlineData("abc\uD83D\uDE00", 5, 5)]
    public void BoundaryMatchesFixedWordPunctuationWhitespaceAndUtf16Rules(string text, int endIndex, int expected)
        => Assert.Equal(expected, s_boundaryStart(text, endIndex));

    [Fact]
    public void UnpairedSurrogatesAlsoTerminateTheScan()
    {
        string highSurrogateAtEnd = new(new[] { 'a', 'b', 'c', '\uD800' });
        string lowSurrogateInside = new(new[] { 'a', 'b', 'c', '\uDC00', 'd', 'e', 'f' });
        Assert.Equal(4, s_boundaryStart(highSurrogateAtEnd, 4));
        Assert.Equal(4, s_boundaryStart(lowSurrogateInside, 7));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-3)]
    public void NegativeEndIndexReturnsTheUnnormalizedInput(int endIndex)
        => Assert.Equal(endIndex, s_boundaryStart("abc", endIndex));

    [Theory]
    [InlineData(4)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void OutOfRangePositiveScanOrSubtractionOverflowKeepsOriginalIndexException(int endIndex)
        => Assert.Throws<IndexOutOfRangeException>(() => s_boundaryStart("abc", endIndex));

    [Fact]
    public void NullWithNoScanReturnsZeroButPositiveScanKeepsOriginalNullException()
    {
        Assert.Equal(0, s_boundaryStart(null!, 0));
        Assert.Throws<NullReferenceException>(() => s_boundaryStart(null!, 1));
    }

    [Fact]
    public void HelperDoesNotExpandThePublicWinFormsSurface()
    {
        Assert.False(s_clientUtils.IsPublic);
        Assert.Empty(s_clientUtils.GetMethods(BindingFlags.Static | BindingFlags.Public));
    }
}
