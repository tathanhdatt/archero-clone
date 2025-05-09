﻿using UnityEngine;
using UnityEngine.Events;

public class IceDamageHandler : MonoBehaviour
{
    [SerializeField]
    private FloatReference iceHealthAmount;

    public UnityEvent onIced;

    public void ReceiveIceDamage(DamageType type, float incomingDamage)
    {
        if (!type.HasFlag(DamageType.Ice)) return;
        this.iceHealthAmount.Value -= incomingDamage;
        if (this.iceHealthAmount.Value > 0) return;
        this.onIced?.Invoke();
        this.iceHealthAmount.ResetValue();
    }
}