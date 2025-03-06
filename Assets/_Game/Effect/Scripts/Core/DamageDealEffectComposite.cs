using Dt.Attribute;
using UnityEngine;

public class DamageDealEffectComposite : DamageDealEffectComponent
{
    [SerializeField]
    private DamageDealEffectComponent[] components;

    public override void ApplyEffect(DamageReceiver receiver, float damage)
    {
        foreach (DamageDealEffectComponent component in this.components)
        {
            if (component.gameObject.activeSelf)
            {
                component.ApplyEffect(receiver, damage);
            }
            else
            {
                Debug.LogWarning($"{component.name}: components are disabled", component.gameObject);
            }
        }
    }

    [Button]
    private void GetEffects()
    {
        this.components = new DamageDealEffectComponent[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            this.components[i] = transform.GetChild(i).GetComponent<DamageDealEffectComponent>();
        }
    }
}