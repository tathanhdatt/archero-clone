using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDamageDealer : DamageDealer
{
    [SerializeField, Required]
    private FloatVariable damage;

    [SerializeField, Required]
    private Tag damageableTag;

    private Collider cachedCollider;
    private DamageReceiver cachedDamageReceiver;

    public UnityEvent<DamageReceiver, float> onBeforeDamage;
    public UnityEvent<DamageReceiver, float> onAfterDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other != this.cachedCollider)
        {
            DamageReceiver damageReceiver = other.GetComponent<DamageReceiver>();
            if (damageReceiver == null) return;
            this.cachedDamageReceiver = damageReceiver;
            this.cachedCollider = other;
        }

        DealDamageIfDamageable();
    }

    private void DealDamageIfDamageable()
    {
        if (this.cachedDamageReceiver.ContainsTag(this.damageableTag))
        {
            DealDamage();
        }
    }

    public override void DealDamage()
    {
        this.onBeforeDamage?.Invoke(this.cachedDamageReceiver, this.damage.Value);
        this.cachedDamageReceiver.TakeDamage(this.damage.Value);
        this.onAfterDamage?.Invoke(this.cachedDamageReceiver, this.damage.Value);
    }
}