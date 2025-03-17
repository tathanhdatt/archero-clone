using UnityEngine;
using UnityEngine.Events;

public class CollectibleDespawnListener : MonoBehaviour
{
    public CollectibleDespawnEvent collectibleDespawnEvent;
    public UnityEvent<CollectibleObject> response;

    public void OnEventRaised(CollectibleObject collectibleObject)
    {
        this.response?.Invoke(collectibleObject);
    }

    private void OnEnable()
    {
        this.collectibleDespawnEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        this.collectibleDespawnEvent.UnregisterListener(this);
    }
}