using UnityEngine;

public abstract class DamageDealEffectComponent : MonoBehaviour
{
    public abstract void ApplyEffect(DamageReceiver receiver, float damage);
}