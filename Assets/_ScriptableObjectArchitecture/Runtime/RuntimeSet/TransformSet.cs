using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Architecture/Runtime Set/Transform Set")]
public class TransformSet : RuntimeSet<Transform>
{
    private const float MaxSqrMagnitude = 10000f;

    public Transform GetClosestFrom(Transform transform)
    {
        if (this.items.IsEmpty()) return null;
        float sqrtMag = MaxSqrMagnitude;
        Transform closest = this.items[0];
        foreach (Transform item in this.items)
        {
            float tempSqrtMagnitude = Vector3.SqrMagnitude(
                item.position - transform.position);
            if (tempSqrtMagnitude >= sqrtMag) continue;
            sqrtMag = tempSqrtMagnitude;
            closest = item;
        }

        return closest;
    }
}