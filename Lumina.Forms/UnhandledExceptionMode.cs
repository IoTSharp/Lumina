namespace Lumina.Forms;

/// <summary>Specifies how unhandled UI thread exceptions are processed.</summary>
public enum UnhandledExceptionMode
{
    Automatic = 0,
    ThrowException = 1,
    CatchException = 2,
}
