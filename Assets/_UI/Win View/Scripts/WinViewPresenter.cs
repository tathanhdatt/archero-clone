using UnityEngine;

public class WinViewPresenter : BaseViewPresenter
{
    private WinView winView;
    public WinViewPresenter(GamePresenter presenter, Transform transform) : base(presenter, transform)
    {
    }

    protected override void AddViews()
    {
        this.winView = AddView<WinView>();
    }

    protected override void OnShow()
    {
        base.OnShow();
        this.winView.OnClickedWin += OnClickedWin;
    }

    protected override void OnHide()
    {
        base.OnHide();
        this.winView.OnClickedWin -= OnClickedWin;
    }

    private async void OnClickedWin()
    {
        await Hide();
        await Presenter.GetViewPresenter<NavigatorViewPresenter>().Show();
    }
}