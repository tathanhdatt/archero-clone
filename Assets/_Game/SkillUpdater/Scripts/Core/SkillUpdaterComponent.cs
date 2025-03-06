using Dt.Attribute;
using UnityEngine;

public abstract class SkillUpdaterComponent : ScriptableObject
{
    public abstract string Description { get; }
    [Button]
    public abstract void Update();
}