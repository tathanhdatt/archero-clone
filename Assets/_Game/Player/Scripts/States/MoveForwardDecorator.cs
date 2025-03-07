using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class MoveForwardDecorator : StateDecorator
{
    [SerializeField]
    private float speed;

    [SerializeField, Required]
    private Rigidbody rb;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
        this.rb.linearVelocity = this.rb.transform.forward * this.speed;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
    }

    [Button]
    public override void ResetName()
    {
        gameObject.name = "Move Forward";
    }
}