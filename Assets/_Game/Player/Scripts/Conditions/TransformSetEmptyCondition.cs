using Dt.Attribute;
using Dt.Condition.Core;
using UnityEngine;

public class TransformSetEmptyCondition : Condition
{
    [SerializeField, Required]
    private TransformSet set;

    public override bool IsMet => this.set.IsEmptyOrNull();

    [Button]
    private void Rename()
    {
        gameObject.name = $"Is [{this.set.name}] empty?";
    }
}