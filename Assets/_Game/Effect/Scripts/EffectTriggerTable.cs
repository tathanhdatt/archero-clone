using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect Trigger Table")]
public class EffectTriggerTable : ScriptableObject
{
    public EffectTrigger[] effectTriggers;

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
}

[Serializable]
public class EffectTrigger
{
    public EffectType type;

    public bool active;
}