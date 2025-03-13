using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class ColliderTriggerSetter : StateDecorator
{
    [SerializeField, Required]
    private Collider col;

    [SerializeField]
    private bool trigger;

    [SerializeField]
    private bool reverseOnExit;

    [SerializeField, ReadOnly]
    private bool originalTrigger;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        this.originalTrigger = this.col.isTrigger;
        this.col.isTrigger = this.trigger;
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
        if (this.reverseOnExit)
        {
            this.col.isTrigger = this.originalTrigger;
        }
    }

    public override void ResetName()
    {
        base.ResetName();
        gameObject.name = $"Set \"{this.trigger}\" trigger for \"{this.col.name}\"";
    }
}