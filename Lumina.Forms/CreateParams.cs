namespace Lumina.Forms;

/// <summary>Specifies the native window class, style, position and creation data.</summary>
public class CreateParams
{
    public string? ClassName { get; set; }
    public string? Caption { get; set; }
    public int Style { get; set; }
    public int ExStyle { get; set; }
    public int ClassStyle { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public nint Parent { get; set; }
    public object? Param { get; set; }

    public override string ToString()
        => $"CreateParams {{'{ClassName}', '{Caption}', 0x{Style:x}, 0x{ExStyle:x}, {{{X}, {Y}, {Width}, {Height}}}}}";
}
