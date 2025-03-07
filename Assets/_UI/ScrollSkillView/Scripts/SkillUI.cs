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
    private SkillUpdaterData updaterData;

    [SerializeField, Required]
    private Button button;

    public event Action OnSkillUpdated;

    public void UpdateSkill()
    {
        if (this.updaterData != null)
        {
            this.updaterData.updateComponents[this.updaterData.currentLevel].Update();
            this.updaterData.currentLevel++;
        }

        OnSkillUpdated?.Invoke();
    }


    public void Init(SkillUpdaterData skillUpdaterData)
    {
        this.icon.sprite = skillUpdaterData.icon;
        int currentLevelSkill = skillUpdaterData.currentLevel;
        string content = skillUpdaterData.updateComponents[currentLevelSkill].Description;
        this.description.SetText(content);
    }
}