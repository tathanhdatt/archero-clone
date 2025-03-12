using System;
using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Architecture/Variable/Vector2")]
public class Vector2Variable : ScriptableObjectVariable
{
    [SerializeField]
    private Vector2 initialValue;

    [SerializeField]
    private Vector2 value;

    public override event Action OnValueChanged;

    public Vector2 Value
    {
        get => this.value;
        set
        {
            if (this.value == value) return;
            this.value = value;
            OnValueChanged?.Invoke();
        }
    }

    [Button]
    private void SetValue(Vector2 value)
    {
        Value = value;
    }
}