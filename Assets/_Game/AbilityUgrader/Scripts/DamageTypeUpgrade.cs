using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability Upgrade/Damage Type Upgrade")]
public class DamageTypeUpgrade : AbilityUpgradeComponent
{
    [SerializeField, Required]
    private DamageTypeVariable damageType;

    [SerializeField]
    private DamageType expectedDamageType;

    [SerializeField, TextArea(2, 10)]
    private string description;

    public override string Description => this.description;

    public override void Upgrade()
    {
        this.damageType.Value |= this.expectedDamageType;
    }

    public override void Downgrade()
    {
        this.damageType.Value &= ~this.expectedDamageType;
    }
}