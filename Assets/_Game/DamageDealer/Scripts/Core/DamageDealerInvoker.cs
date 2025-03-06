using UnityEngine;

public class DamageDealerInvoker : MonoBehaviour
{
    [SerializeField]
    private DamageDealer[] damageDealers;

    public void Invoke(int damageDealerIndex)
    {
        if (damageDealerIndex < 0 || damageDealerIndex >= this.damageDealers.Length)
        {
            Debug.LogWarning("DamageDealer index is out of range.", gameObject);
            return;
        }
        this.damageDealers[damageDealerIndex].DealDamage();
    }
}