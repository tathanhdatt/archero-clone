using Dt.Attribute;
using UnityEngine;

public class ClosestTransformSetProvider : TargetProviderStrategy
{
    [SerializeField, Required]
    private TransformSet transformSet;

    [SerializeField, Required]
    private Transform source;
    public override Vector3 GetTargetPosition()
    {
        Transform closest = this.transformSet.GetClosestFrom(this.source);
        if (closest == null)
        {
            return Vector3.zero;
        }
        return this.transformSet.GetClosestFrom(this.source).position;
    }
}