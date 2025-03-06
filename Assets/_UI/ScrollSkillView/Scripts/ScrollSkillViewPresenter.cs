using UnityEngine;

public class ScrollSkillViewPresenter : BaseViewPresenter
{
    private ScrollSkillView scrollSkillView;
    public ScrollSkillViewPresenter(GamePresenter presenter, Transform transform) : base(presenter, transform)
    {
    }

    protected override void AddViews()
    {
        this.scrollSkillView = AddView<ScrollSkillView>();
    }
}