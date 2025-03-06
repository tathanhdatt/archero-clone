using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObjectArchitecture
{
    public List<T> items = new List<T>();

    public virtual void Add(T item)
    {
        if (this.items.Contains(item)) return;
        this.items.Add(item);
    }

    public virtual void Remove(T item)
    {
        if (this.items.Contains(item))
        {
            this.items.Remove(item);
        }
    }

    private void OnEnable()
    {
        this.items.Clear();
    }
}