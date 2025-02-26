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
        return this.transformSet.GetClosestObject(this.source).position;
    }
}