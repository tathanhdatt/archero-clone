using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun Decorators Trigger Table")]
public class GunDecoratorsTriggerTable : ScriptableObject
{
    public TypeGunDecoratorAndBoolDict triggersDict;
    public event Action<GunDecoratorType, bool> OnTriggerActiveChanged;

    public void UpdateTrigger(GunDecoratorType type, bool active)
    {
        if (!this.triggersDict.ContainsKey(type)) return;
        this.triggersDict[type] = active;
        OnTriggerActiveChanged?.Invoke(type, active);
    }

    private void OnEnable()
    {
        if (this.triggersDict.Count == 0) return;
        for (int i = 0; i < this.triggersDict.Count; i++)
        {
            this.triggersDict[this.triggersDict.Keys.ElementAt(i)] = false;
        }
        this.triggersDict[this.triggersDict.Keys.First()] = true;
    }
}