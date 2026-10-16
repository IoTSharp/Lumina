using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>Represents a Windows message and the result of processing it.</summary>
public struct Message : IEquatable<Message>
{
    public nint HWnd { get; set; }
    public int Msg { get; set; }
    public nint WParam { get; set; }
    public nint LParam { get; set; }
    public nint Result { get; set; }

    public readonly object? GetLParam(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)]
        Type cls) => Marshal.PtrToStructure(LParam, cls);

    public static Message Create(nint hWnd, int msg, nint wparam, nint lparam)
        => new() { HWnd = hWnd, Msg = msg, WParam = wparam, LParam = lparam };

    public override readonly bool Equals(object? o) => o is Message message && Equals(message);

    public readonly bool Equals(Message other)
        => HWnd == other.HWnd && Msg == other.Msg && WParam == other.WParam
            && LParam == other.LParam && Result == other.Result;

    public static bool operator ==(Message a, Message b) => a.Equals(b);
    public static bool operator !=(Message a, Message b) => !a.Equals(b);
    public override readonly int GetHashCode() => HashCode.Combine(HWnd, Msg);
    public override readonly string ToString() => MessageDecoder.ToString(this);
}
