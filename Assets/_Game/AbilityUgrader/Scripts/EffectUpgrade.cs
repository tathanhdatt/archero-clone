using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability Upgrade/Effect Upgrade")]
public class EffectUpgrade : AbilityUpgradeComponent
{
    [SerializeField]
    private EffectType effectType;

    [SerializeField, Required]
    private EffectTriggerTable triggerTable;

    [SerializeField]
    private bool value;

    [SerializeField]
    private string description;

    public override string Description => this.description;

    public override void Upgrade()
    {
        this.triggerTable.UpdateActive(this.effectType, this.value);
    }

    public override void Downgrade()
    {
        this.triggerTable.UpdateActive(this.effectType, !this.value);
    }
}