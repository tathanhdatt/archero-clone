using System;
using System.Collections.Generic;
using Dt.Attribute;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageReceiverPopupSpawner : MonoBehaviour
{
    [Title("Config")]
    [SerializeField]
    private TextPopup prefab;

    [SerializeField]
    private float randomRadius;

    [Title("Colors")]
    [SerializeField]
    private List<DamageTypeColor> colors;

    [Title("Font size")]
    [SerializeField]
    private float normalFontSize;

    [SerializeField]
    private float criticalFontSize;

    public void Create(DamageType type, float incomingDamage)
    {
        TextPopup textPopup = Instantiate(this.prefab, transform);
        Vector3 randomPos = Random.insideUnitCircle * this.randomRadius;
        randomPos.z = 0;
        textPopup.transform.localPosition = randomPos;
        float fontSize = this.normalFontSize;
        foreach (DamageTypeColor typeColor in this.colors)
        {
            if (!type.HasFlag(typeColor.type)) continue;
            textPopup.SetColor(typeColor.color);
            break;
        }

        string content = $"-{incomingDamage:F0}";
        textPopup.Initialize(content, fontSize);
    }
    [Serializable]
    private struct DamageTypeColor
    {
        public DamageType type;
        public Color color;
    }
}