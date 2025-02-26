using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField, Required]
    private FloatVariable maxHealth;
    
    [SerializeField]
    private FloatVariable currentHealth;

    [SerializeField, Required]
    private Element element;

    public float CurrentHealth => this.currentHealth.Value;
    public float MaxHealth => this.maxHealth.Value;
    public Element Element => this.element;
    
    public UnityEvent<DamageType> onTakenDamage;
    public UnityEvent onDeath;

    private void OnEnable()
    {
        if (this.currentHealth == null)
        {
            this.currentHealth = ScriptableObject.CreateInstance<FloatVariable>();
        }
        this.currentHealth.Value = this.maxHealth.Value;
    }

    public void TakeDamage(float incomingDamage, DamageType type = DamageType.Normal)
    {
        this.currentHealth.Value -= incomingDamage;
        this.onTakenDamage?.Invoke(type);
        if (this.currentHealth.Value <= 0)
        {
            this.onDeath?.Invoke();
        }
    }
}