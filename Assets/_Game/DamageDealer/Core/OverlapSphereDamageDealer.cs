using Dt.Attribute;
using UnityEngine;

public class OverlapSphereDamageDealer : DamageDealer
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private Vector3 offset;

    [SerializeField, Required]
    private FloatVariable incomingDamage;

    [SerializeField, Required]
    private DamageableElements damageableElements;

    private readonly Collider[] colliders = new Collider[10];
    private Collider cachedCollider;
    private DamageReceiver cachedDamageReceiver;

    public override void DealDamage()
    {
        int numberCols = Physics.OverlapSphereNonAlloc(
            transform.position + this.offset,
            this.radius,
            this.colliders);
        if (numberCols <= 0) return;
        for (int i = 0; i < numberCols; i++)
        {
            if (this.colliders[i] == null) continue;
            if (this.colliders[i] == this.cachedCollider) break;
            DamageReceiver receiver = this.colliders[i].GetComponent<DamageReceiver>();
            if (receiver == null) continue;
            bool canTakeDamage = this.damageableElements.CanDamageType(receiver.Element);
            if (!canTakeDamage) continue;
            this.cachedDamageReceiver = receiver;
            break;
        }

        if (this.cachedDamageReceiver == null) return;
        this.cachedDamageReceiver.TakeDamage(this.incomingDamage.Value);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + this.offset, this.radius);
    }
}