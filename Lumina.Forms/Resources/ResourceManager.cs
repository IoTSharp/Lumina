using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Lumina.Forms.Resources;

/// <summary>
/// Reads standard resources and the Bitmap byte resources emitted by the LuminaForms build path.
/// </summary>
/// <remarks>
/// Bitmap conversion uses a statically referenced decoder. Other resource objects retain the
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
/// Reads designer resources with the standard ComponentResourceManager API and direct Bitmap decoding.
/// </summary>
/// <remarks>
/// This first resource path covers GetObject for Bitmap resources. ApplyResources and other
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

    internal static object GetObject(byte[] bytes, string? resourceType)
    {
        if (resourceType is null)
        {
            return bytes;
        }

        if (!string.Equals(resourceType, "System.Drawing.Bitmap", StringComparison.Ordinal))
        {
            throw new NotSupportedException($"The resource object type '{resourceType}' is not supported by the Bitmap resource path.");
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
}
