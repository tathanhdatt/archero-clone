using System.Collections.Generic;
using UnityEngine;

public abstract class EventGeneric<T> : ScriptableObject
{
    private readonly List<EventListener<T>> listeners = new List<EventListener<T>>();

    public void Raise(T value)
    {
        foreach (EventListener<T> listener in this.listeners)
        {
            listener.OnEventRaised(value);
        }
    }

    public void RegisterListener(EventListener<T> listener)
    {
        if (this.listeners.Contains(listener)) return;
        this.listeners.Add(listener);
    }

    public void UnregisterListener(EventListener<T> listener)
    {
        if (!this.listeners.Contains(listener)) return;
        this.listeners.Remove(listener);
    }
}