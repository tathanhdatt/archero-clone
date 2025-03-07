using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect Trigger Table")]
public class EffectTriggerTable : ScriptableObject
{
    public List<EffectTrigger> effectTriggers;

    public void UpdateActive(EffectType type, bool active)
    {
        EffectTrigger trigger = GetEffectTrigger(type);
        trigger.active = active;
    }

    private EffectTrigger GetEffectTrigger(EffectType effectType)
    {
        foreach (EffectTrigger trigger in this.effectTriggers)
        {
            if (trigger.type == effectType)
            {
                return trigger;
            }
        }

        return null;
    }

    private void OnEnable()
    {
        this.effectTriggers.ForEach(trigger => trigger.active = false);
    }
}

[Serializable]
public class EffectTrigger
{
    public EffectType type;

    public bool active;
}