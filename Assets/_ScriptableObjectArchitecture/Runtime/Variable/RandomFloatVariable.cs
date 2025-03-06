using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "ScriptableObject Architecture/Variable/Random Float")]
public class RandomFloatVariable : FloatVariable
{
    [SerializeField]
    private FloatRange range;
    
    [Serializable]
    private struct FloatRange
    {
        public float min;
        public float max;
    }

    public override float Value => Random.Range(this.range.min, this.range.max);
}