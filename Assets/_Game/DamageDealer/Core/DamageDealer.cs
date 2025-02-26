using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    [SerializeField, Required]
    private FloatVariable damage;

    [SerializeField, Required]
    private BoolVariable piercing;

    [SerializeField, Required]
    private DamageableElements damageableElements;

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
        if (this.damageableElements.CanDamageType(this.cachedDamageReceiver.Element))
        {
            DealDamage();
        }
    }

    private void DealDamage()
    {
        this.onBeforeDamage?.Invoke(this.cachedDamageReceiver, this.damage.Value);
        this.cachedDamageReceiver.TakeDamage(this.damage.Value);
        if (this.piercing.value) return;
        this.onAfterDamage?.Invoke(this.cachedDamageReceiver, this.damage.Value);
    }
}