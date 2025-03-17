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
    
    public override float Value => Random.Range(this.min, this.max);
    public override event Action OnValueChanged;
}