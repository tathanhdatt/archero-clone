using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class LookAtClosestYAxisDecorator : StateDecorator
{
    [SerializeField, Required]
    private TransformSet targetTransformSet;

    [SerializeField, Required]
    private float rotationDuration;

    [SerializeField, Required]
    private Ease ease;

    [SerializeField, ReadOnly]
    private Transform closest;
    
    private Tweener rotationTweener;

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
        Transform closestObject = this.targetTransformSet.GetClosestObject(RootTransform);
        if (closestObject == null) return;
        this.closest = closestObject;
        this.rotationTweener?.Kill();
        RotateToClosest();
    }

    private void RotateToClosest()
    {
        if (this.closest == null) return;

        Vector3 direction = this.closest.position - RootTransform.position;
        direction.y = 0;

        if (direction == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        this.rotationTweener = RootTransform.DORotate(targetRotation.eulerAngles, this.rotationDuration);
        this.rotationTweener.SetEase(this.ease);
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
        this.rotationTweener?.Kill();
    }
}