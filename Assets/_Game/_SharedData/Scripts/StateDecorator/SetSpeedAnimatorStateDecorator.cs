using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class SetSpeedAnimatorStateDecorator : StateDecorator
{
    [SerializeField, Required]
    private Animator animator;

    [SerializeField]
    private float speed;
    
    protected override async UniTask OnStateEnter()
    {
        this.animator.speed = this.speed;
        await UniTask.CompletedTask;
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
        base.ResetName();
        gameObject.name = $"Set speed of [{this.animator.name}] = {this.speed}";
    }
}