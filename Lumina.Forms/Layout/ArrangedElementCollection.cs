using System.Collections;
using System.ComponentModel;

namespace Lumina.Forms.Layout;

/// <summary>
/// Provides the collection surface shared by elements participating in layout.
/// </summary>
public class ArrangedElementCollection : IList
{
    internal ArrangedElementCollection() { }

    private protected List<Control> InnerList { get; } = new(4);

    /// <summary>Gets the number of elements in the collection.</summary>
    public virtual int Count => InnerList.Count;

    /// <summary>Gets whether collection mutations are prohibited.</summary>
    public virtual bool IsReadOnly => ((IList)InnerList).IsReadOnly;

    /// <summary>Copies the elements into the supplied array.</summary>
    public void CopyTo(Array array, int index)
    {
        if (array is not null && array.Rank != 1)
        {
            throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
        }

        // The reference collection copies from interface-typed storage. This
        // preserves Array.Copy's per-element casts and partial writes, rather
        // than the upfront array-type rejection of a concrete Control[] source.
        IComponent[] elements = new IComponent[InnerList.Count];
        for (int item = 0; item < elements.Length; item++)
        {
            elements[item] = InnerList[item];
        }

        try
        {
            Array.Copy(elements, 0, array!, index, elements.Length);
        }
        catch (ArrayTypeMismatchException)
        {
            throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
        }
    }

    /// <summary>Returns an enumerator over the elements.</summary>
    public virtual IEnumerator GetEnumerator() => InnerList.GetEnumerator();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is not ArrangedElementCollection collection || Count != collection.Count)
        {
            return false;
        }

        for (int index = 0; index < Count; index++)
        {
            if (!ReferenceEquals(InnerList[index], collection.InnerList[index]))
            {
                return false;
            }
        }

        return true;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var result = new HashCode();
        foreach (Control element in InnerList)
        {
            result.Add(element);
        }

        return result.ToHashCode();
    }

    bool IList.IsFixedSize => ((IList)InnerList).IsFixedSize;
    object ICollection.SyncRoot => ((ICollection)InnerList).SyncRoot;
    bool ICollection.IsSynchronized => ((ICollection)InnerList).IsSynchronized;
    bool IList.Contains(object? value) => ((IList)InnerList).Contains(value);
    int IList.IndexOf(object? value) => ((IList)InnerList).IndexOf(value);
    int IList.Add(object? value) => ((IList)InnerList).Add(value);
    void IList.Remove(object? value) => ((IList)InnerList).Remove(value);
    void IList.RemoveAt(int index) => InnerList.RemoveAt(index);
    void IList.Clear() => InnerList.Clear();
    void IList.Insert(int index, object? value) => throw new NotSupportedException();
    object? IList.this[int index]
    {
        get => InnerList[index];
        set => throw new NotSupportedException();
    }
}
