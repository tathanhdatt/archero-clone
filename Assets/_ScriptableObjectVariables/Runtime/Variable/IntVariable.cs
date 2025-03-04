using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject Variable/Variable/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField]
    protected int initValue;

    [SerializeField]
    protected int value;

    private void OnEnable()
    {
        this.value = this.initValue;
    }

    public virtual int Value
    {
        get => this.value;
        set => this.value = value;
    }
}