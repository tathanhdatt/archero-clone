using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Variable/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField]
    protected float initValue;

    [SerializeField]
    protected float value;

    private void OnEnable()
    {
        this.value = this.initValue;
    }

    public virtual float Value
    {
        get => this.value;
        set => this.value = value;
    }
}