using Dt.Attribute;
using Dt.Condition.Core;
using UnityEngine;

public class EnemySetEmptyTracker : Condition
{
    [SerializeField, Required]
    private EnemyAliveSet set;

    public override bool IsMet => this.set.IsEmptyOrNull();
}