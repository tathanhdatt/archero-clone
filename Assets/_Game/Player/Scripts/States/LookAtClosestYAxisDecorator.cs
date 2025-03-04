using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class LookAtClosestYAxisDecorator : StateDecorator
{
    [SerializeField, Required]
    private TransformSet targetTransformSet;

    [SerializeField, ReadOnly]
    private Transform closest;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        this.closest = null;
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
        LookAtClosest();
    }

    private void LookAtClosest()
    {
        Transform closestObject = this.targetTransformSet.GetClosestFrom(RootTransform);
        if (closestObject == null) return;
        this.closest = closestObject;
        RotateToClosest();
    }

    private void RotateToClosest()
    {
        if (this.closest == null) return;
        RootTransform.LookAt(this.closest);
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
    }
}