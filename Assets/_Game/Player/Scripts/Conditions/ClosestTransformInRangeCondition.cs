using Dt.Attribute;
using Dt.Condition.Core;
using UnityEngine;

public class ClosestTransformInRangeCondition : Condition
{
    [SerializeField]
    private float range;
    
    [SerializeField, Required]
    private TransformSet transformSet;

    [SerializeField, Required]
    private Transform root;

    public override bool IsMet => IsInRange();

    private bool IsInRange()
    {
        Transform closest = this.transformSet.GetClosestFrom(this.root);
        if (Vector3.Distance(closest.position, this.root.position) < this.range)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.root.position, this.range);
    }
}