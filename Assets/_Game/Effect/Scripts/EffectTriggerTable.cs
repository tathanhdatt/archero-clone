using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect Trigger Table")]
public class EffectTriggerTable : TriggerTable
{
    public TypeEffectAndBoolDict effectsDict;

    public void UpdateActive(EffectType type, bool active)
    {
        this.effectsDict[type] = active;
    }

    public override void ResetTable()
    {
        for (int i = 0; i < this.effectsDict.Count; i++)
        {
            this.effectsDict[this.effectsDict.Keys.ElementAt(i)] = false;
        }
    }
}