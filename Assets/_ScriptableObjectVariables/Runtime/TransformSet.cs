using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Variable/Runtime Set/Transform Set")]
public class TransformSet : RuntimeSets<Transform>
{
    public Transform GetClosestObject(Transform transform)
    {
        if (this.items.IsEmpty()) return null;
        float sqrtMag = Mathf.Infinity;
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