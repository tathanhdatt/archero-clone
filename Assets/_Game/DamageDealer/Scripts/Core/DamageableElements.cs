using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Variable/Damageable Elements")]
public class DamageableElements : ScriptableObject
{
    public bool exclude;
    public List<Tag> damageTag;
    public List<Element> excludeElements;
    public List<Element> includeElements;

    public bool CanDamageType(Element element)
    {
        if (this.exclude)
        {
            if (this.excludeElements.Contains(element))
            {
                return false;
            }

            return true;
        }

        if (this.includeElements.Contains(element))
        {
            return true;
        }

        return false;
    }
}