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

    [SerializeField]
    private bool setOnEnter;

    [SerializeField]
    private bool setOnExit;

    protected override async UniTask OnStateEnter()
    {
        if (this.setOnEnter)
        {
            this.animator.speed = this.speed;
        }

        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
        if (this.setOnExit)
        {
            this.animator.speed = this.speed;
        }
    }

    public override void ResetName()
    {
        base.ResetName();
        string setOnEnterStr = this.setOnEnter ? "On Enter" : string.Empty;
        string andStr = this.setOnEnter && this.setOnExit ? "&" : string.Empty;
        string setOnExitStr = this.setOnExit ? "On Exit" : string.Empty;
        string settingTime = $"{setOnEnterStr} {andStr} {setOnExitStr}";
        gameObject.name = $"Set [{this.animator.name}]'s speed = {this.speed} {settingTime}";
    }
}