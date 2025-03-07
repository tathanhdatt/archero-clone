using System.Collections.Generic;
using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Updater/Data")]
public class SkillUpdaterData : ScriptableObject
{
    [SpritePreview]
    public Sprite icon;
    public string skillName;
    public int currentLevel;
    public int MaxLevel => this.updateComponents.Count;
    public List<SkillUpdaterComponent> updateComponents;

    private void OnEnable()
    {
        this.currentLevel = 0;
    }
}