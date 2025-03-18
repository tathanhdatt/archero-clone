using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    public UnityEvent<CollectibleType, float> onCollect;
    public UnityEvent<CollectibleObject> onCollectObject;

    private void OnTriggerEnter(Collider other)
    {
        CollectibleObject collectibleObject = other.GetComponent<CollectibleObject>();
        if (collectibleObject == null) return;
        this.onCollect?.Invoke(collectibleObject.type, collectibleObject.value);
        this.onCollectObject?.Invoke(collectibleObject);
    }
}