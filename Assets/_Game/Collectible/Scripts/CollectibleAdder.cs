using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleAdder : MonoBehaviour
{
    [SerializeField, Required]
    private CollectibleType expectedType;

    public UnityEvent<int> onAdded;

    public void Add(CollectibleType type, int amount)
    {
        if (type != this.expectedType) return;
        this.onAdded?.Invoke(amount);
        Debug.Log($"Collect {amount} {type.name}");
    }
}