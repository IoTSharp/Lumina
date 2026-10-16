using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Lumina.Forms;

/// <summary>Owns the temporary native representation passed to CreateWindowEx.</summary>
internal sealed class NativeCreateParameter : IDisposable
{
    private readonly Action<nint>? _destroy;
    internal nint Pointer { get; private set; }

    private NativeCreateParameter(nint pointer, Action<nint>? destroy = null)
    {
        Pointer = pointer;
        _destroy = destroy;
    }

    internal static NativeCreateParameter Create(object? value)
    {
        if (value is null) { return new(0); }
        if (value is StringBuilder builder) { value = builder.ToString(); }
        if (value is string text) { return new(Marshal.StringToCoTaskMemUni(text)); }

        // Root the standard value marshallers explicitly so these parameter
        // values also have their native representation in a trimmed AOT image.
        switch (value)
        {
            case bool item: return CreateValue(item);
            case byte item: return CreateValue(item);
            case sbyte item: return CreateValue(item);
            case short item: return CreateValue(item);
            case ushort item: return CreateValue(item);
            case int item: return CreateValue(item);
            case uint item: return CreateValue(item);
            case long item: return CreateValue(item);
            case ulong item: return CreateValue(item);
            case nint item: return CreateValue(item);
            case nuint item: return CreateValue(item);
            case char item: return CreateValue(item);
            case float item: return CreateValue(item);
            case double item: return CreateValue(item);
            case decimal item: return CreateValue(item);
            case Guid item: return CreateValue(item);
        }

        if (RuntimeFeature.IsDynamicCodeSupported) { return CreateDynamic(value); }
        throw new NotSupportedException("Native AOT window creation parameters currently support strings, StringBuilder, primitive values, decimal and Guid. Custom object marshalling requires a statically available marshaller.");
    }

    private static NativeCreateParameter CreateValue<T>(T value) where T : struct
    {
        nint pointer = Marshal.AllocCoTaskMem(Marshal.SizeOf<T>());
        try
        {
            Marshal.StructureToPtr(value, pointer, false);
            return new(pointer, Marshal.DestroyStructure<T>);
        }
        catch
        {
            Marshal.FreeCoTaskMem(pointer);
            throw;
        }
    }

    [RequiresDynamicCode("Custom creation parameter objects can require a generated native marshaller.")]
    private static NativeCreateParameter CreateDynamic(object value)
    {
        nint pointer = Marshal.AllocCoTaskMem(Marshal.SizeOf(value));
        try
        {
            Marshal.StructureToPtr(value, pointer, false);
            return new(pointer, p => Marshal.DestroyStructure(p, value.GetType()));
        }
        catch
        {
            Marshal.FreeCoTaskMem(pointer);
            throw;
        }
    }

    public void Dispose()
    {
        nint pointer = Pointer;
        Pointer = 0;
        if (pointer == 0) { return; }
        try { _destroy?.Invoke(pointer); }
        finally { Marshal.FreeCoTaskMem(pointer); }
    }
}
