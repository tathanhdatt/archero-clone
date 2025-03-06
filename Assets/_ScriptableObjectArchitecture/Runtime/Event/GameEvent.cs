using System.Collections.Generic;
using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Architecture/Event/Game Event")]
public class GameEvent : ScriptableObjectArchitecture
{
    [SerializeField]
    private bool logEventRaised;

    private readonly List<GameEventListener> listeners = new List<GameEventListener>(10);

    [Button]
    public void Raise()
    {
        foreach (GameEventListener listener in this.listeners)
        {
            listener.OnEventRaised();
        }

        if (this.logEventRaised)
        {
            Debug.Log($"Event: [{name}] Raised", this);
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