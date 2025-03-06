using Dt.Attribute;
using UnityEngine;

public class EffectCollection : MonoBehaviour
{
    [SerializeField]
    private EffectItem[] effects;
    
    [SerializeField]
    private EffectTriggerTable triggerTable;

    [Button]
    public void UpdateEffect()
    {
        if (this.triggerTable == null) return;
        foreach (EffectTrigger trigger in this.triggerTable.effectTriggers)
        {
            DamageDealEffectComponent component = GetEffect(trigger.type);
            component.gameObject.SetActive(trigger.active);
        }
    }

    public DamageDealEffectComponent GetEffect(EffectType type)
    {
        foreach (EffectItem item in this.effects)
        {
            if (item.type == type)
            {
                return item.effect;
            }
        }
        return null;
    }
}

[System.Serializable]
public class EffectItem
{
    public EffectType type;
    public DamageDealEffectComponent effect;
}