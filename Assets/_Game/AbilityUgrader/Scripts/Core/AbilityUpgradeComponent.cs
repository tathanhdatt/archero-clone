using Dt.Attribute;
using UnityEngine;

public abstract class AbilityUpgradeComponent : ScriptableObject
{
    public abstract string Description { get; }
    [Button]
    public abstract void Upgrade();
    [Button]
    public abstract void Downgrade();
}