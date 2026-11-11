namespace Lumina.Forms;

/// <summary>Filters queued Windows messages before they are dispatched.</summary>
public interface IMessageFilter
{
    /// <summary>Returns true to consume the supplied message.</summary>
    bool PreFilterMessage(ref Message m);
}
