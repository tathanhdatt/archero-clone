using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    public UnityEvent<CollectibleType, int> onCollect;

    private void OnTriggerEnter(Collider other)
    {
        CollectibleObject collectibleObject = other.GetComponent<CollectibleObject>();
        if (collectibleObject == null) return;
        this.onCollect?.Invoke(collectibleObject.type, collectibleObject.value);
        Destroy(collectibleObject.gameObject);
    }
}