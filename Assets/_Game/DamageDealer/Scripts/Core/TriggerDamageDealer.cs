using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDamageDealer : DamageDealer
{
    [SerializeField, Required]
    private FloatVariable damage;

    [SerializeField, Required]
    private FloatVariable criticalHitChance;

    [SerializeField, Required]
    private Tag damageableTag;

    [SerializeField, Required]
    private DamageTypeVariable damageType;

    [SerializeField, ShowIf(nameof(HasCollideDamage))]
    private float delayDealDamageCollide;

    [SerializeField, ShowIf(nameof(HasCollideDamage)), ReadOnly]
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
        if (!this.damageType.Value.HasFlag(DamageType.Collide)) return;
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
        bool criticalHit = Random.Range(0f, 1f) < Mathf.Clamp01(this.criticalHitChance.Value);
        DamageType type = this.damageType.Value;
        float damageValue = this.damage.Value;
        if (criticalHit)
        {
            type |= DamageType.Critical;
            damageValue = this.damage.Value * 2f;
        }
        this.cachedDamageReceiver.TakeDamage(damageValue, type);
        this.onAfterDamage?.Invoke(this.cachedDamageReceiver, this.damage.Value);
    }

    private bool HasCollideDamage()
    {
        return this.damageType.Value.HasFlag(DamageType.Collide);
    }
}