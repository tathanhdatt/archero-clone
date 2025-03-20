using UnityEngine;

[CreateAssetMenu(menuName = "Damage Type Variable")]
public class DamageTypeVariable : ScriptableObject
{
    [SerializeField]
    private DamageType initValue;

    [SerializeField]
    private DamageType value;

    public DamageType Value
    {
        get => this.value;
        set => this.value = value;
    }

    private void OnEnable()
    {
        this.value = this.initValue;
    }
}