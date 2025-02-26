using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.Condition.Core;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class WaitEventRaised : Condition
{
    protected override async UniTask OnInitialized()
    {
        await base.OnInitialized();
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