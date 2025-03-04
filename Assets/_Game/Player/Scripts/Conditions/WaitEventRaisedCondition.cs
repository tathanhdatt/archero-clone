using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.Condition.Core;

public class WaitEventRaisedCondition : Condition
{
    protected override async UniTask OnEntered()
    {
        await base.OnEntered();
        this.isMet = false;
    }

    public void MetEvent()
    {
        this.isMet = true;
    }

    [Button]
    private void Rename()
    {
        gameObject.name = "Wait Event";
    }
}