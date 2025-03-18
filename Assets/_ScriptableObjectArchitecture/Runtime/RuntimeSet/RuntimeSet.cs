using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObjectArchitecture
{
    [SerializeField]
    protected List<T> items = new List<T>();

    public bool IsEmptyOrNull()
    {
        return this.items == null || this.items.IsEmpty();
    }

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