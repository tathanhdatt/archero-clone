using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "ScriptableObject Architecture/Variable/Random Float")]
public class RandomFloatVariable : FloatVariable, IRandomVariable
{
    [SerializeField]
    private float min;

    [SerializeField]
    private float max;

    public override float Value
    {
        get => this.value + Random.Range(this.min, this.max);
        set
        {
            if (!Mathf.Approximately(this.value, value))
            {
                OnValueChanged?.Invoke();
            }

            this.value = value;
        }
    }

    public override event Action OnValueChanged;
}