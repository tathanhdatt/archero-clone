using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun Decorators Trigger Table")]
public class GunDecoratorsTriggerTable : ScriptableObject
{
    public List<GunDecoratorTrigger> triggers;
    public event Action<GunDecoratorTrigger> OnTriggerChanged;

    public void UpdateTrigger(GunDecoratorType type, bool active)
    {
        foreach (GunDecoratorTrigger trigger in this.triggers)
        {
            if (trigger.type != type) continue;
            trigger.active = active;
            OnTriggerChanged?.Invoke(trigger);
            break;
        }
    }

    private void OnEnable()
    {
        if (this.triggers.Count == 0) return;
        this.triggers.ForEach(trigger => trigger.active = false);
        this.triggers[0].active = true;
    }
}

[Serializable]
public class GunDecoratorTrigger
{
    public GunDecoratorType type;
    public bool active;
    public GunDecoratorType[] conflictTypes;
}