using System;
using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Updater/Float Variable Updater")]
public class FloatVariableUpdater : SkillUpdaterLeaf
{
    [SerializeField]
    private NumberModifiedType modifiedType;

    [SerializeField, Required]
    private FloatVariable variable;

    [ShowIf(nameof(modifiedType), NumberModifiedType.AddWithPercentage)]
    [SerializeField, Range(-1f, 1f)]
    private float percentage;

    [SerializeField]
    [HideIf(nameof(modifiedType), NumberModifiedType.AddWithPercentage)]
    private float value;
    
    [TextArea(1, 10)]
    public string description;
    
    public override string Description => this.description;

    public override void Update()
    {
        switch (this.modifiedType)
        {
            case NumberModifiedType.ChangeValue:
                this.variable.Value = this.value;
                break;
            case NumberModifiedType.AddWithValue:
                this.variable.Value += this.value;
                break;
            case NumberModifiedType.AddWithPercentage:
                this.variable.Value += this.variable.Value * this.percentage;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}