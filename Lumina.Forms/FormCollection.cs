// FormCollection contract adapted from dotnet/winforms v10.0.11.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses those portions under the MIT license.

using System.Collections;

namespace Lumina.Forms;

/// <summary>Provides read-only indexed and name access to the application's open forms.</summary>
public class FormCollection : ReadOnlyCollectionBase
{
    internal static Lock CollectionSyncRoot { get; } = new();
    internal int AddVersion { get; private set; }

    /// <summary>Gets a form by its registration index.</summary>
    public virtual Form? this[int index]
    {
        get { lock (CollectionSyncRoot) { return (Form?)InnerList[index]; } }
    }

    /// <summary>Gets the first form with the supplied name, ignoring case.</summary>
    public virtual Form? this[string? name]
    {
        get
        {
            if (name is null) { return null; }
            lock (CollectionSyncRoot)
            {
                foreach (Form form in InnerList)
                {
                    if (string.Equals(form.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        return form;
                    }
                }
            }

            return null;
        }
    }

    internal void Add(Form form)
    {
        lock (CollectionSyncRoot)
        {
            InnerList.Add(form);
            AddVersion++;
        }
    }

    internal void Remove(Form form)
    {
        lock (CollectionSyncRoot) { InnerList.Remove(form); }
    }
}
