using System;
using System.Linq;
using UnityEngine;

public class ScrollSkillViewPresenter : BaseViewPresenter
{
    private ScrollSkillView scrollSkillView;
    private readonly SkillUpdaterData[] skillUpdaterData;

    public ScrollSkillViewPresenter(GamePresenter presenter,
        Transform transform, SkillUpdaterData[] skillUpdaterData) : base(presenter, transform)
    {
        this.skillUpdaterData = skillUpdaterData;
    }

    protected override void AddViews()
    {
        this.scrollSkillView = AddView<ScrollSkillView>();
    }

    protected override void OnShow()
    {
        this.scrollSkillView.OnSkillUpdated += OnSkillUpdatedHandler;
        base.OnShow();
        this.skillUpdaterData.Shuffle();
        SetSkillImages();
    }

    protected override void OnHide()
    {
        base.OnHide();
        this.scrollSkillView.OnSkillUpdated -= OnSkillUpdatedHandler;
    }

    private async void OnSkillUpdatedHandler()
    {
        await Hide();
    }

    private void SetSkillImages()
    {
        int numberOfSkills = this.scrollSkillView.SkillUIs.Count;
        if (numberOfSkills > this.skillUpdaterData.Length)
        {
            throw new ArgumentException("Not enough skill images");
        }

        this.scrollSkillView.SetSkillData(
            this.skillUpdaterData
                .Where(data => data.currentLevel < data.updateComponents.Count)
                .ToArray());
    }
}