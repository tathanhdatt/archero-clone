using System.Collections.Generic;
using Dt.Attribute;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField, Required]
    private FloatVariable maxHealth;

    [SerializeField, Required]
    private FloatVariable currentHealth;

    public override float CurrentHealth
    {
        get => this.currentHealth.Value;
        protected set => this.currentHealth.Value = value;
    }

    public override float MaxHealth => this.maxHealth.Value;

    public override void Initialize()
    {
        base.Initialize();
        this.currentHealth.OnValueChanged += CurrentHealthOnOnValueChanged;
    }

    private void CurrentHealthOnOnValueChanged()
    {
        if (this.currentHealth.Value > this.maxHealth.Value)
        {
            this.currentHealth.Value = this.maxHealth.Value;
        }
    }

    public override void Terminate()
    {
        this.currentHealth.OnValueChanged -= CurrentHealthOnOnValueChanged;
    }
}