using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Updater/Effect Updater")]
public class EffectUpdater : SkillUpdaterLeaf
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

    public override void Update()
    {
        this.triggerTable.UpdateActive(this.effectType, this.value);
    }
}