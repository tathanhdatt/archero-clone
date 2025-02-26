using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.Condition.Core;
using UnityEngine;

public class WaitSpecificDamageType : Condition
{
    [SerializeField]
    private DamageType expectedDamageType;

    [SerializeField, ReadOnly]
    private DamageType actualDamageType;

    public override bool IsMet => this.actualDamageType != DamageType.NoType &&
                                  this.actualDamageType == this.expectedDamageType;

    protected override async UniTask OnInitialized()
    {
        await base.OnInitialized();
        this.actualDamageType = DamageType.NoType;
    }

    public void SetType(DamageType damageType)
    {
        this.actualDamageType = damageType;
    }

    [Button]
    private void Rename()
    {
        gameObject.name = $"Wait for \"{this.expectedDamageType}\" damage type";
    }
}