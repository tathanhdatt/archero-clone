using Dt.Attribute;
using UnityEngine;

public class PushBackDamageReceiver : DamageDealEffectComponent
{
    [SerializeField]
    private float pushBackForce;
    
    [SerializeField]
    private ForceMode forceMode;

    [SerializeField, ReadOnly]
    private Rigidbody rb;

    [SerializeField, ReadOnly]
    private DamageReceiver prevReceiver;

    public override void ApplyEffect(DamageReceiver receiver, float damage)
    {
        if (this.prevReceiver != receiver)
        {
            this.rb = receiver.transform.parent.GetComponent<Rigidbody>();
        }

        this.rb.AddForce(-this.rb.transform.forward * this.pushBackForce, this.forceMode);
    }
}