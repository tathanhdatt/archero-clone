using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class DamageReceiver : InitializableMono
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

    public override void Initialize()
    {
        if (this.currentHealth == null)
        {
            this.currentHealth = ScriptableObject.CreateInstance<FloatVariable>();
        }
        else
        {
            this.currentHealth.OnValueChanged += CurrentHealthOnOnValueChanged;
        }

        this.currentHealth.Value = this.maxHealth.Value;
    }

    private void CurrentHealthOnOnValueChanged()
    {
        if (this.currentHealth.Value > this.maxHealth.Value)
        {
            this.currentHealth.Value = this.maxHealth.Value;
        }
    }

    public void TakeDamage(float incomingDamage, DamageType type = DamageType.Normal)
    {
        this.currentHealth.Value -= incomingDamage;
        if (this.currentHealth.Value <= 0)
        {
            this.onDeath?.Invoke();
        }
        else
        {
            this.onTakenDamage?.Invoke(type);
        }
    }

    public override void Terminate()
    {
        if (this.currentHealth != null)
        {
            this.currentHealth.OnValueChanged -= CurrentHealthOnOnValueChanged;
        }
    }
}