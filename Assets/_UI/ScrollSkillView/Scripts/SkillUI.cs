using System;
using Dt.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField, Required]
    private Image icon;

    [SerializeField, Required]
    private TMP_Text description;

    [SerializeField, ReadOnly]
    private AbilityUpgradeData upgradeData;

    [SerializeField, Required]
    private Button button;

    public event Action OnSkillUpdated;

    public void UpdateSkill()
    {
        if (this.upgradeData != null)
        {
            this.upgradeData.updateComponents[this.upgradeData.currentLevel].Upgrade();
            this.upgradeData.currentLevel++;
        }

        OnSkillUpdated?.Invoke();
    }


    public void Init(AbilityUpgradeData abilityUpgradeData)
    {
        this.icon.sprite = abilityUpgradeData.icon;
        int currentLevelSkill = abilityUpgradeData.currentLevel;
        string content = abilityUpgradeData.updateComponents[currentLevelSkill].Description;
        this.description.SetText(content);
    }
}