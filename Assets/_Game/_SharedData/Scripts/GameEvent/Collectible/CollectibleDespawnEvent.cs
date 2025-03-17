using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectible/Despawn Event")]
public class CollectibleDespawnEvent : ScriptableObject
{
    private readonly List<CollectibleDespawnListener> listeners =
        new List<CollectibleDespawnListener>(10);

    public void Raise(CollectibleObject collectibleObject)
    {
        foreach (CollectibleDespawnListener listener in this.listeners)
        {
            listener.OnEventRaised(collectibleObject);
        }
    }

    public void RegisterListener(CollectibleDespawnListener listener)
    {
        if (this.listeners.Contains(listener)) return;
        this.listeners.Add(listener);
    }

    public void UnregisterListener(CollectibleDespawnListener listener)
    {
        if (!this.listeners.Contains(listener)) return;
        this.listeners.Remove(listener);
    }
}