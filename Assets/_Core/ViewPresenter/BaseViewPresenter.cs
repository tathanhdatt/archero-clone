using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseViewPresenter
{
    private readonly Dictionary<Type, BaseView> views = new Dictionary<Type, BaseView>(4);
    protected GamePresenter Presenter { get; private set; }
    protected Transform Transform { get; private set; }
    public bool IsShowing { get; private set; }

    protected BaseViewPresenter(GamePresenter presenter, Transform transform)
    {
        Presenter = presenter;
        Transform = transform;
    }

    public async UniTask Initialize()
    {
        AddViews();
        foreach (BaseView view in this.views.Values)
        {
            await view.Initialize();
        }
    }

    protected abstract void AddViews();

    protected T AddView<T>(bool canShowWithPresenter = true) where T : BaseView
    {
        T view = Transform.GetComponentInChildren<T>();
        view.CanShowWithPresenter = canShowWithPresenter;
        this.views.Add(typeof(T), view);
        return view;
    }

    public T GetView<T>() where T : BaseView
    {
        if (this.views.TryGetValue(typeof(T), out BaseView view))
        {
            return (T)view;
        }
        throw new KeyNotFoundException($"View of type {typeof(T)} not found");
    }

    public async UniTask Show()
    {
        if (IsShowing) return;
        IsShowing = true;
        foreach (BaseView view in this.views.Values)
        {
            if (view.CanShowWithPresenter)
            {
                await view.Show();
            }
        }

        OnShow();
    }

    protected virtual void OnShow()
    {
    }

    public async UniTask Hide()
    {
        IsShowing = false;
        foreach (BaseView view in this.views.Values)
        {
            await view.Hide();
        }

        OnHide();
    }

    protected virtual void OnHide()
    {
    }
}