using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class PiercingBulletEffect : DamageDealEffectComponent
{
    [SerializeField, Required]
    private BoolVariable pierce;
    
    public UnityEvent onHit;

    public override void ApplyEffect(DamageReceiver receiver, float damage)
    {
        if (this.pierce.Value) return;
        this.onHit?.Invoke();
    }
}