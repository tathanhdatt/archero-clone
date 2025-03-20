using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Architecture/Variable/Bool")]
public class BoolVariable : ScriptableObjectVariable
{
    [SerializeField]
    private bool initValue;

    [SerializeField]
    private bool value;

    [SerializeField]
    private bool logValueChanged;

    public override event Action OnValueChanged;

    public bool Value
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

    private void OnEnable()
    {
        ResetValue();
    }

    public void ResetValue()
    {
        Value = this.initValue;
    }
}