using Dt.Attribute;
using UnityEngine;

public class ClosestDistanceCalculator : MonoBehaviour
{
    [SerializeField, Required]
    private FloatVariable storageDistance;
    
    [SerializeField, Required]
    private TransformSet playerTransform;

    private void Update()
    {
        if (this.playerTransform.items.IsEmpty()) return;
        Transform closest = this.playerTransform.GetClosestObject(transform);
        this.storageDistance.Value = Vector3.Distance(closest.position, transform.position);
    }
}