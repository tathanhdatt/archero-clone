using UnityEngine;

public class HomeViewPresenter : BaseViewPresenter
{
    private HomeView homeView;

    public HomeViewPresenter(GamePresenter presenter, Transform transform) : base(presenter,
        transform)
    {
    }

    protected override void AddViews()
    {
        this.homeView = AddView<HomeView>();
    }

    protected override void OnShow()
    {
        base.OnShow();
        this.homeView.OnClickedPlay += OnClickedPlayHandler;
    }


    protected override void OnHide()
    {
        base.OnHide();
        this.homeView.OnClickedPlay -= OnClickedPlayHandler;
    }

    private void OnClickedPlayHandler()
    {
        Messenger.Broadcast(Message.Play, 1);
    }
}