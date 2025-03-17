using UnityEngine;
using UnityEngine.Events;

public class CollectibleSpawnListener : MonoBehaviour
{
    public CollectibleSpawnEvent collectibleSpawnEvent;
    public UnityEvent<CollectibleTable, Vector3> onCollectibleSpawned; 
    public void OnEventRaised(CollectibleTable table, Vector3 position)
    {
        this.onCollectibleSpawned?.Invoke(table, position);
    }

    private void OnEnable()
    {
        this.collectibleSpawnEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        this.collectibleSpawnEvent.UnregisterListener(this);
    }
}