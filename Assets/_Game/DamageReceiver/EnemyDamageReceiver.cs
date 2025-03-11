using Dt.Attribute;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField, Required]
    private FloatVariable maxHealth;

    [SerializeField, ReadOnly]
    private float currentHealth;

    public override float CurrentHealth
    {
        get => this.currentHealth;
        protected set => this.currentHealth = value;
    }

    public override float MaxHealth => this.maxHealth.Value;
}