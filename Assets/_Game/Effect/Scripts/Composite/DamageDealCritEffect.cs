using Dt.Attribute;
using UnityEngine;

public class DamageDealCritEffect : DamageDealEffectComponent
{
    [SerializeField, Required]
    private FloatVariable critRate;

    public override void ApplyEffect(DamageReceiver receiver, float damage)
    {
        if (Random.Range(0f, 1f) > this.critRate.Value) return;
        receiver.TakeDamage(damage);
    }

    [Button]
    private void Rename()
    {
        gameObject.name = $"Crit {this.critRate.Value * 100}%";
    }
}