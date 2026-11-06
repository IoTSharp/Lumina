// Adapted from dotnet/winforms v10.0.11 InputLanguageCollection.cs.
// Licensed to the .NET Foundation under the MIT license.

using System.Collections;

namespace Lumina.Forms;

/// <summary>Provides a read-only collection of installed input languages.</summary>
public class InputLanguageCollection : ReadOnlyCollectionBase
{
    internal InputLanguageCollection(InputLanguage[] value) => InnerList.AddRange(value);

    /// <summary>Gets the input language at the specified index.</summary>
    public InputLanguage this[int index] => (InputLanguage)InnerList[index]!;

    /// <summary>Returns whether the specified input language belongs to this collection.</summary>
    public bool Contains(InputLanguage? value) => InnerList.Contains(value);

    /// <summary>Copies the input languages to an array at the specified index.</summary>
    public void CopyTo(InputLanguage[] array, int index) => InnerList.CopyTo(array, index);

    /// <summary>Gets the index of an input language, or -1 when it is absent.</summary>
    public int IndexOf(InputLanguage? value) => InnerList.IndexOf(value);
}
