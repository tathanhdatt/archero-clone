using Dt.Attribute;
using UnityEngine;

public class DamageDealerInvoker : MonoBehaviour
{
    [SerializeField, Required]
    private DamageDealer damageDealer;

    public void Invoke()
    {
        this.damageDealer.DealDamage();
    }
}