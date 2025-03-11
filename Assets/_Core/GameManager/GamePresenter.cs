using System;
using System.Collections.Generic;
using Core.Game;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    private readonly Dictionary<Type, BaseViewPresenter> presenters =
        new Dictionary<Type, BaseViewPresenter>();

    private GameManager manager;

    public void Initialize(GameManager gameManager)
    {
        this.manager = gameManager;
    }

    private void AddPresenters()
    {
        ScrollSkillViewPresenter scrollSkillViewPresenter =
            new ScrollSkillViewPresenter(this, transform, this.manager.SkillUpdaterData);
        AddPresenter(scrollSkillViewPresenter);
        
        GearViewPresenter gearViewPresenter = new GearViewPresenter(this, transform);
        AddPresenter(gearViewPresenter);
    }

    public async UniTask InitialViewPresenters()
    {
        AddPresenters();
        foreach (BaseViewPresenter presenter in this.presenters.Values)
        {
            await presenter.Initialize();
        }
    }

    public void AddPresenter(BaseViewPresenter presenter)
    {
        this.presenters.Add(presenter.GetType(), presenter);
    }

    public T GetViewPresenter<T>() where T : BaseViewPresenter
    {
        Type type = typeof(T);
        return (T)this.presenters[type];
    }

    public async UniTask HideViewPresenters()
    {
        foreach (BaseViewPresenter viewPresenter in this.presenters.Values)
        {
            await viewPresenter.Hide();
        }
    }
}