using Cysharp.Threading.Tasks;
using UnityEngine;

public class NavigatorViewPresenter : BaseViewPresenter
{
    private NavigatorView navigatorView;
    private BaseViewPresenter showingViewPresenter;

    public NavigatorViewPresenter(GamePresenter presenter, Transform transform) : base(presenter,
        transform)
    {
    }

    protected override void AddViews()
    {
        this.navigatorView = AddView<NavigatorView>();
    }

    protected override void OnShow()
    {
        base.OnShow();
        ShowViewPresenter(Presenter.GetViewPresenter<HomeViewPresenter>());
        this.navigatorView.OnClickedHome += OnClickedHomeHandler;
        this.navigatorView.OnClickedGear += OnClickedGearHandler;
        this.navigatorView.OnClickedGacha += OnClickedGachaHandler;
    }


    protected override void OnHide()
    {
        base.OnHide();
        this.navigatorView.OnClickedHome -= OnClickedHomeHandler;
        this.navigatorView.OnClickedGear -= OnClickedGearHandler;
        this.navigatorView.OnClickedGacha -= OnClickedGachaHandler;
    }

    private async void OnClickedHomeHandler()
    {
        await ShowViewPresenter(Presenter.GetViewPresenter<HomeViewPresenter>());
    }

    private async void OnClickedGearHandler()
    {
        await ShowViewPresenter(Presenter.GetViewPresenter<GearViewPresenter>());
    }

    private void OnClickedGachaHandler()
    {
        Debug.Log("Show Gacha View");
    }

    private async UniTask ShowViewPresenter(BaseViewPresenter viewPresenter)
    {
        if (this.showingViewPresenter != null)
        {
            await this.showingViewPresenter.Hide();
        }

        this.showingViewPresenter = viewPresenter;
        await this.showingViewPresenter.Show();
    }
}