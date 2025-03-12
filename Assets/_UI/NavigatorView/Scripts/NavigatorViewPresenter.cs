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

    protected override async void OnShow()
    {
        this.navigatorView.OnClickedHome += OnClickedHomeHandler;
        this.navigatorView.OnClickedGear += OnClickedGearHandler;
        this.navigatorView.OnClickedGacha += OnClickedGachaHandler;
        base.OnShow();
        await Presenter.GetViewPresenter<HomeViewPresenter>().Show();
        await Presenter.GetViewPresenter<GearViewPresenter>().Show();
    }


    protected override async void OnHide()
    {
        base.OnHide();
        this.navigatorView.OnClickedHome -= OnClickedHomeHandler;
        this.navigatorView.OnClickedGear -= OnClickedGearHandler;
        this.navigatorView.OnClickedGacha -= OnClickedGachaHandler;
        await Presenter.GetViewPresenter<HomeViewPresenter>().Show();
        await Presenter.GetViewPresenter<GearViewPresenter>().Show();
    }

    private void OnClickedHomeHandler()
    {
        this.showingViewPresenter = Presenter.GetViewPresenter<HomeViewPresenter>();
        int index = this.showingViewPresenter.GetView<HomeView>().transform.GetSiblingIndex();
        this.navigatorView.SnapToView(index);
    }

    private void OnClickedGearHandler()
    {
        this.showingViewPresenter = Presenter.GetViewPresenter<GearViewPresenter>();
        int index = this.showingViewPresenter.GetView<GearView>().transform.GetSiblingIndex();
        this.navigatorView.SnapToView(index);
    }

    private void OnClickedGachaHandler()
    {
        Debug.Log("Show Gacha View");
    }
}