using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Architecture/Variable/Int")]
public class IntVariable : ScriptableObjectVariable
{
    [SerializeField]
    protected int initValue;

    [SerializeField]
    protected int value;

    [SerializeField]
    private bool logValueChanged;

    private void OnEnable()
    {
        this.value = this.initValue;
    }

    public virtual int Value
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

    public override event Action OnValueChanged;
}