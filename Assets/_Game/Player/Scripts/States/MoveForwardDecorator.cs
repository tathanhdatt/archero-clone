using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class MoveForwardDecorator : StateDecorator
{
    [SerializeField]
    private float speed;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
        RootTransform.Translate(Vector3.forward * (this.speed * Time.deltaTime));
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