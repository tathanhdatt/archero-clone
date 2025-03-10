using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class KinematicRigidBodySetter : StateDecorator
{
    [SerializeField, Required]
    private Rigidbody rb;

    [SerializeField]
    private bool isKinematic;

    [SerializeField]
    private bool reverseOnExit;

    [SerializeField, ReadOnly]
    private bool originalIsKinematic;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        this.originalIsKinematic = this.rb.isKinematic;
        this.rb.isKinematic = this.isKinematic;
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
            this.rb.isKinematic = this.originalIsKinematic;
        }
    }

    public override void ResetName()
    {
        base.ResetName();
        gameObject.name = $"Set \"{this.rb.name}\" to \"{this.isKinematic}\" kinematic";
    }
}