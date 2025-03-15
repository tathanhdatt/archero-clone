using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDamageDealer : DamageDealer
{
    [SerializeField, Required]
    private FloatVariable damage;

    [SerializeField, Required]
    private Tag damageableTag;

    [SerializeField]
    private DamageType damageType;

    [SerializeField, ShowIf(nameof(damageType), DamageType.Collide)]
    private float delayDealDamageCollide;
    
    [SerializeField, ShowIf(nameof(damageType), DamageType.Collide), ReadOnly]
    private float elapsedTime;

    private Collider cachedCollider;
    private DamageReceiver cachedDamageReceiver;

    public UnityEvent<DamageReceiver, float> onBeforeDamage;
    public UnityEvent<DamageReceiver, float> onAfterDamage;

    private void OnTriggerEnter(Collider other)
    {
        DetectAndDealDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.damageType != DamageType.Collide) return;
        DealDamageCollide(other);
    }

    private void DealDamageCollide(Collider other)
    {
        this.elapsedTime += Time.fixedDeltaTime;
        if (this.elapsedTime < this.delayDealDamageCollide) return;
        this.elapsedTime = 0;
        DetectAndDealDamage(other);
    }

    private void DetectAndDealDamage(Collider other)
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
        this.cachedDamageReceiver.TakeDamage(this.damage.Value, this.damageType);
        this.onAfterDamage?.Invoke(this.cachedDamageReceiver, this.damage.Value);
    }
}