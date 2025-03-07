using System;
using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Architecture/Variable/Float")]
public class FloatVariable : ScriptableObjectVariable
{
    [SerializeField]
    protected float initValue;

    [SerializeField]
    protected float value;
    
    [SerializeField]
    private bool logValueChanged;

    public override event Action OnValueChanged;

    private void OnEnable()
    {
        this.value = this.initValue;
    }

    public virtual float Value
    {
        get => this.value;
        set
        {
            this.value = value;
            OnValueChanged?.Invoke();
            if (this.logValueChanged)
            {
                Debug.Log($"[{name}]'s value was modified to: {value}", this);
            }
        }
    }

    [Button]
    private void SetValue(float value)
    {
        Value = value;
    }
}