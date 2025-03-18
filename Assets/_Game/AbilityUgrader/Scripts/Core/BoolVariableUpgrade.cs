using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability Upgrade/Bool Variable Upgrade")]
public class BoolVariableUpgrade : AbilityUpgradeComponent
{
    [SerializeField, Required]
    private BoolVariable variable;

    [SerializeField]
    private bool value;

    [SerializeField, ReadOnly]
    private bool originalValue;

    [TextArea(1, 10)]
    public string description;

    public override string Description => this.description;

    public override void Upgrade()
    {
        this.originalValue = this.variable.Value;
        this.variable.Value = this.value;
    }

    public override void Downgrade()
    {
        this.variable.Value = this.originalValue;
    }
}