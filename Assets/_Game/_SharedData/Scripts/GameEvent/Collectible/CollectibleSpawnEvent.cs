using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectible/Spawn Event")]
public class CollectibleSpawnEvent : ScriptableObject
{
    private readonly List<CollectibleSpawnListener> listeners = new List<CollectibleSpawnListener>(10);
    
    private Vector3 spawnPosition;

    public void SetSpawnPosition(Transform transform)
    {
        this.spawnPosition = transform.position;
    }

    public void Raise(CollectibleTable table)
    {
        foreach (CollectibleSpawnListener listener in this.listeners)
        {
            listener.OnEventRaised(table, this.spawnPosition);
        }
    }

    public void RegisterListener(CollectibleSpawnListener listener)
    {
        if (this.listeners.Contains(listener)) return;
        this.listeners.Add(listener);
    }

    public void UnregisterListener(CollectibleSpawnListener listener)
    {
        if (!this.listeners.Contains(listener)) return;
        this.listeners.Remove(listener);
    }
}