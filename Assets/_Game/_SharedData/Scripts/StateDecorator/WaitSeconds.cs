using Cysharp.Threading.Tasks;
using Dt.StateMachine.Core;
using UnityEngine;

public class WaitSeconds : StateDecorator
{
    [SerializeField]
    private float duration;
    protected override async UniTask OnStateEnter()
    {
        await UniTask.WaitForSeconds(this.duration);
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
    }

    public override void ResetName()
    {
        gameObject.name = $"Wait {this.duration} seconds";
    }
}