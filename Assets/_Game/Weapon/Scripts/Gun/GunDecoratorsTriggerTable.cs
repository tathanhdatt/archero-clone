using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun Decorators Trigger Table")]
public class GunDecoratorsTriggerTable : TriggerTable
{
    public TypeGunDecoratorAndBoolDict triggersDict;
    public event Action<GunDecoratorType, bool> OnTriggerActiveChanged;

    public void UpdateTrigger(GunDecoratorType type, bool active)
    {
        if (!this.triggersDict.ContainsKey(type)) return;
        this.triggersDict[type] = active;
        OnTriggerActiveChanged?.Invoke(type, active);
    }


    public override void ResetTable()
    {
        for (int i = 0; i < this.triggersDict.Count; i++)
        {
            this.triggersDict[this.triggersDict.Keys.ElementAt(i)] = false;
        }
    }
}