using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Variable/Random Float")]
public class RandomFloatVariable : FloatVariable
{
    [SerializeField]
    private IntRange range;

    public override float Value
    {
        get => Random.Range(this.range.min, this.range.max);
        set => this.value = value;
    }
}