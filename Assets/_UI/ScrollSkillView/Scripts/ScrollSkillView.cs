using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

public class ScrollSkillView : BaseView
{
    [Title("Scroll Skill Animations")]
    [SerializeField]
    private List<Animator> scrollSkinAnimators;

    [SerializeField, ValueDropdown(nameof(GetAnimNames))]
    private string startScrollAnimName;

    [SerializeField]
    private float scrollDuration;

    [Title("Canvas Groups Animations")]
    [SerializeField, Required]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private float fadeInDuration = 0.2f;

    [SerializeField]
    private float fadeOutDuration = 0.2f;

    [SerializeField]
    private Ease fadeInEase;

    [SerializeField]
    private Ease fadeOutEase;

    private Tweener fadeInTweener;
    private Tweener fadeOutTweener;

    [Title("Skill UI")]
    [SerializeField]
    private List<SkillUI> skillUIs;

    public List<SkillUI> SkillUIs => this.skillUIs;
    
    public event Action OnSkillUpdated;

    public override async UniTask Initialize()
    {
        await base.Initialize();
        ListenUpdatedSkill();
    }

    private void ListenUpdatedSkill()
    {
        this.skillUIs.ForEach(skillUI=>skillUI.OnSkillUpdated += OnSkillUpdatedHandler);
    }

    private void OnSkillUpdatedHandler()
    {
        OnSkillUpdated?.Invoke();
    }

    public override async UniTask Show()
    {
        EnableAnimators();
        SetCanvasTransparency(0);
        await base.Show();
        await FadeInCanvasGroup();
        await StartScrollAnimation();
    }

    public override async UniTask Hide()
    {
        await FadeOutCanvasGroup();
        await base.Hide();
    }

    private void SetCanvasTransparency(float value)
    {
        this.canvasGroup.alpha = value;
    }

    private async UniTask FadeInCanvasGroup()
    {
        this.fadeInTweener = this.canvasGroup.DOFade(1, this.fadeInDuration);
        this.fadeInTweener.SetUpdate(true);
        this.fadeInTweener.SetEase(this.fadeInEase);
        await this.fadeInTweener.AsyncWaitForCompletion();
    }

    private async UniTask StartScrollAnimation()
    {
        this.scrollSkinAnimators.ForEach(animator => animator.Play(this.startScrollAnimName));
        await UniTask.WaitForSeconds(this.scrollDuration, true);
        DisableAnimators();
    }

    private void EnableAnimators()
    {
        this.scrollSkinAnimators.ForEach(animator => animator.enabled = true);
    }

    private void DisableAnimators()
    {
        this.scrollSkinAnimators.ForEach(animator => animator.enabled = false);
    }

    private async UniTask FadeOutCanvasGroup()
    {
        this.fadeOutTweener = this.canvasGroup.DOFade(0, this.fadeOutDuration);
        this.fadeOutTweener.SetUpdate(true);
        this.fadeOutTweener.SetEase(this.fadeOutEase);
        await this.fadeOutTweener.AsyncWaitForCompletion();
    }

    public void SetSkillData(SkillUpdaterData[] data)
    {
        for (int i = 0; i < this.skillUIs.Count; i++)
        {
            this.skillUIs[i].Init(data[i]);
        }
    }

    private string[] GetAnimNames()
    {
        return this.scrollSkinAnimators.First().GetAnimationNames();
    }
}