using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Lumina.Forms.Resources;

/// <summary>
/// Reads standard resources and the Drawing byte resources emitted by the LuminaForms build path.
/// </summary>
/// <remarks>
/// Bitmap, Icon and Font conversion uses statically referenced decoders. Other resource objects retain the
/// behavior of <see cref="System.Resources.ResourceManager"/>.
/// </remarks>
public class ResourceManager : System.Resources.ResourceManager
{
    /// <summary>Creates a resource manager for the specified assembly and resource base name.</summary>
    public ResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
    {
    }

    /// <summary>Creates a resource manager for the resource represented by a type.</summary>
    public ResourceManager(Type resourceSource) : base(resourceSource)
    {
    }

    /// <inheritdoc/>
    public override object? GetObject(string name) => GetObject(name, null);

    /// <inheritdoc/>
    public override object? GetObject(string name, CultureInfo? culture)
    {
        object? value = base.GetObject(name, culture);
        return value is byte[] bytes
            ? BitmapResourceDecoder.GetObject(bytes, base.GetString(name + BitmapResourceDecoder.TypeSuffix, culture))
            : value;
    }
}

/// <summary>
/// Reads designer resources with the standard ComponentResourceManager API and direct Drawing decoding.
/// </summary>
/// <remarks>
/// This resource path covers GetObject for Bitmap, Icon and invariant-string Font resources. ApplyResources and other
/// reflection based component conversion continue to follow the base implementation.
/// </remarks>
public class ComponentResourceManager : System.ComponentModel.ComponentResourceManager
{
    /// <summary>Creates a component resource manager for the resource represented by a type.</summary>
    public ComponentResourceManager(Type t) : base(t)
    {
    }

    /// <inheritdoc/>
    public override object? GetObject(string name) => GetObject(name, null);

    /// <inheritdoc/>
    public override object? GetObject(string name, CultureInfo? culture)
    {
        object? value = base.GetObject(name, culture);
        return value is byte[] bytes
            ? BitmapResourceDecoder.GetObject(bytes, base.GetString(name + BitmapResourceDecoder.TypeSuffix, culture))
            : value;
    }
}

internal static class BitmapResourceDecoder
{
    internal const string TypeSuffix = ".__LuminaResourceType";

    // System.Drawing requires the input stream to remain open for the lifetime of the Bitmap.
    // MemoryStream has no native handle; the association expires when the Bitmap is collected.
    private static readonly ConditionalWeakTable<Bitmap, MemoryStream> s_streams = new();
    private static readonly ConditionalWeakTable<Icon, MemoryStream> s_iconStreams = new();

    internal static object GetObject(byte[] bytes, string? resourceType)
    {
        if (resourceType is null)
        {
            return bytes;
        }

        if (string.Equals(resourceType, "System.Drawing.Icon", StringComparison.Ordinal))
        {
            return CreateIcon(bytes);
        }

        if (string.Equals(resourceType, "System.Drawing.Font", StringComparison.Ordinal))
        {
            return CreateFont(bytes);
        }

        if (!string.Equals(resourceType, "System.Drawing.Bitmap", StringComparison.Ordinal))
        {
            throw new NotSupportedException($"The resource object type '{resourceType}' is not supported by the Drawing resource path.");
        }

        // The preserialized Bitmap path used by the WinForms baseline decodes each lookup.
        // Do not change image identity or share disposable Bitmaps between callers.
        var stream = new MemoryStream(bytes, writable: false);
        Bitmap? bitmap = null;
        try
        {
            bitmap = new Bitmap(stream);
            s_streams.Add(bitmap, stream);
            return bitmap;
        }
        catch
        {
            bitmap?.Dispose();
            stream.Dispose();
            throw;
        }
    }

    private static Icon CreateIcon(byte[] bytes)
    {
        var stream = new MemoryStream(bytes, writable: false);
        Icon? icon = null;
        try
        {
            icon = new Icon(stream);
            s_iconStreams.Add(icon, stream);
            return icon;
        }
        catch
        {
            icon?.Dispose();
            stream.Dispose();
            throw;
        }
    }

    private static Font CreateFont(byte[] bytes)
    {
        using var stream = new MemoryStream(bytes, writable: false);
        using var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: false);
        if (reader.ReadInt32() != 0x3152464C) // LFR1: versioned, primitive-only font record.
        {
            throw new InvalidDataException("The Font resource record has an unsupported format.");
        }

        string name = reader.ReadString();
        float size = reader.ReadSingle();
        FontStyle style = (FontStyle)reader.ReadInt32();
        GraphicsUnit unit = (GraphicsUnit)reader.ReadInt32();
        byte charSet = reader.ReadByte();
        bool vertical = reader.ReadBoolean();
        if (stream.Position != stream.Length)
        {
            throw new InvalidDataException("The Font resource record has trailing data.");
        }

        // Keep the original family name until the target OS resolves it. Building
        // must not bake a build-machine font substitution into a deployed resource.
        return new Font(name, size, style, unit, charSet, vertical);
    }
}
