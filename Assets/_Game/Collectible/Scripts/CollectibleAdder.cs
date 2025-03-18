using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleAdder : MonoBehaviour
{
    [SerializeField, Required]
    private CollectibleType expectedType;

    public UnityEvent<float> onAdded;

    public void Add(CollectibleType type, float amount)
    {
        if (type != this.expectedType) return;
        this.onAdded?.Invoke(amount);
        Debug.Log($"Collect {amount} {type.name}");
    }
}