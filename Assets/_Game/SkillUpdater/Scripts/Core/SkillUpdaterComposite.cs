using System.Collections.Generic;
using System.Text;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Updater/Composite")]
public class SkillUpdaterComposite : SkillUpdaterComponent
{
    public List<SkillUpdaterComponent> components;
    
    [SerializeField, TextArea(1, 10)]
    private string cachedDescription;

    public override string Description
    {
        get
        {
            if (this.cachedDescription.IsNullOrEmpty())
            {
                CombineDescription();
            }
            return this.cachedDescription;
        }
    }

    public override void Update()
    {
        foreach (SkillUpdaterComponent component in this.components)
        {
            component.Update();
        }
    }

    [Button]
    private void CombineDescription()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (SkillUpdaterComponent component in this.components)
        {
            stringBuilder.Append(component.Description);
            stringBuilder.Append("\n");
        }
        this.cachedDescription = stringBuilder.ToString();
    }
}