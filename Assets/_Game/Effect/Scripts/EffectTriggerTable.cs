using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect Trigger Table")]
public class EffectTriggerTable : ScriptableObject
{
    public TypeEffectAndBoolDict effectsDict;

    public void UpdateActive(EffectType type, bool active)
    {
        this.effectsDict[type] = active;
    }

    private void OnEnable()
    {
        for (int i = 0; i < this.effectsDict.Count; i++)
        {
            this.effectsDict[this.effectsDict.Keys.ElementAt(i)] = false;
        }
    }
}

[Serializable]
public class EffectTrigger
{
    public EffectType type;

    public bool active;
}