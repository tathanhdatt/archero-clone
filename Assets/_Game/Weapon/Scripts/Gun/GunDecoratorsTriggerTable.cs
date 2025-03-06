using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun Decorators Trigger Table")]
public class GunDecoratorsTriggerTable : ScriptableObject
{
    public GunDecoratorTrigger[] triggers;
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
}

[Serializable]
public class GunDecoratorTrigger
{
    public GunDecoratorType type;
    public bool active;
    public GunDecoratorType[] conflictTypes;
}