using System.Collections.Generic;
using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability Upgrade/Data")]
public class AbilityUpgradeData : ScriptableObject
{
    [SpritePreview]
    public Sprite icon;
    public string skillName;
    public int currentLevel;
    public int MaxLevel => this.updateComponents.Count;
    public List<AbilityUpgradeComponent> updateComponents;

    private void OnEnable()
    {
        this.currentLevel = 0;
    }
}