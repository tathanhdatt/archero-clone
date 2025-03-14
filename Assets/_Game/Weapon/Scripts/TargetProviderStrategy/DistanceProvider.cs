using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

public class DistanceProvider : TargetProviderStrategy
{
    [SerializeField]
    private bool useSOVariable;

    [SerializeField, ShowIf(nameof(useSOVariable))]
    private FloatVariable distanceVariable;

    [SerializeField, HideIf(nameof(useSOVariable))]
    private float distanceValue;

    public override Vector3 GetTargetPosition()
    {
        float angle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        float distance = this.useSOVariable ? this.distanceVariable.Value : this.distanceValue;
        return transform.position.GetPoint(distance, angle);
    }
}