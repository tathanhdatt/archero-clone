using UnityEngine;
using UnityEngine.Events;

public class PushBackDamageHandler : MonoBehaviour
{
    public UnityEvent onTakeDamage;
    public void OnTakeDamage(DamageType damageType, float damage)
    {
        if (!damageType.HasFlag(DamageType.Force)) return;
        this.onTakeDamage?.Invoke();
    }
}