using System.Collections.Generic;
using System.Text;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability Upgrade/Composite")]
public class AbilityUpgradeComposite : AbilityUpgradeComponent
{
    public List<AbilityUpgradeComponent> components;

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

    public override void Upgrade()
    {
        foreach (AbilityUpgradeComponent component in this.components)
        {
            component.Upgrade();
        }
    }

    public override void Downgrade()
    {
        foreach (AbilityUpgradeComponent component in this.components)
        {
            component.Downgrade();
        }
    }

    [Button]
    private void CombineDescription()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (AbilityUpgradeComponent component in this.components)
        {
            stringBuilder.Append(component.Description);
            stringBuilder.Append("\n");
        }

        this.cachedDescription = stringBuilder.ToString();
    }
}