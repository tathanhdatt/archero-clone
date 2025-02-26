using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

public class DistanceProvider : TargetProviderStrategy
{
    [SerializeField, Required]
    private FloatVariable distance;

    [SerializeField, Required]
    private Transform source;

    public override Vector3 GetTargetPosition()
    {
        float angle = this.source.rotation.eulerAngles.y * Mathf.Deg2Rad;
        return this.source.position.GetPoint(this.distance.Value, angle);
    }
}