using System.Collections.Generic;
using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public abstract class DamageReceiver : InitializableMono
{
    [SerializeField, Required]
    private List<Tag> tags;

    public abstract float CurrentHealth { get; protected set; }
    public abstract float MaxHealth { get; }

    public UnityEvent<DamageType> onTakenDamage;
    public UnityEvent onDeath;

    public override void Initialize()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float incomingDamage, DamageType type = DamageType.Normal)
    {
        CurrentHealth -= incomingDamage;
        if (CurrentHealth <= 0)
        {
            this.onDeath?.Invoke();
        }
        else
        {
            this.onTakenDamage?.Invoke(type);
        }
    }

    public bool ContainsTag(Tag tag)
    {
        return this.tags.Contains(tag);
    }

    public override void Terminate()
    {
    }
}