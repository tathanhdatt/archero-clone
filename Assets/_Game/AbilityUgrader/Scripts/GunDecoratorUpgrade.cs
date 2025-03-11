using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability Upgrade/Gun Upgrade")]
public class GunDecoratorUpgrade : AbilityUpgradeComponent
{
    [SerializeField, TextArea(1, 10)]
    private string description;

    [SerializeField]
    private GunDecoratorType type;

    [SerializeField]
    private bool active;

    [SerializeField, Required]
    private GunDecoratorsTriggerTable triggerTable;

    public override string Description => this.description;

    public override void Upgrade()
    {
        this.triggerTable.UpdateTrigger(this.type, this.active);
    }

    public override void Downgrade()
    {
        this.triggerTable.UpdateTrigger(this.type, !this.active);
    }
}