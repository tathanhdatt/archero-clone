using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Updater/Gun Updater")]
public class GunDecoratorUpdater : SkillUpdaterLeaf
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

    public override void Update()
    {
        this.triggerTable.UpdateTrigger(this.type, this.active);
    }
}