using System.Collections.Generic;
using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Variable/Event/Game Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> listeners = new List<GameEventListener>(10);

    [Button]
    public void Raise()
    {
        foreach (GameEventListener listener in this.listeners)
        {
            listener.OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (this.listeners.Contains(listener)) return;
        this.listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (this.listeners.Contains(listener))
        {
            this.listeners.Remove(listener);
        }
    }
}