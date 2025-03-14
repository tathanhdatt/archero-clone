using Dt.Attribute;
using UnityEngine;

public class EffectCollection : MonoBehaviour
{
    [SerializeField]
    private TypeEffectComponentDict effectsDict;

    [SerializeField]
    private EffectTriggerTable triggerTable;

    [Button]
    public void UpdateEffect()
    {
        if (this.triggerTable == null) return;
        foreach (EffectType type in this.triggerTable.effectsDict.Keys)
        {
            this.effectsDict[type].gameObject.SetActive(this.triggerTable.effectsDict[type]);
        }
    }
}

[System.Serializable]
public class EffectItem
{
    public EffectType type;
    public DamageDealEffectComponent effect;
}