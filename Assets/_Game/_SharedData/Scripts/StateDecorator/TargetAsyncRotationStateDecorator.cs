using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class TargetAsyncRotationStateDecorator : StateDecorator
{
    [SerializeField, Required]
    private TransformSet transformSet;

    [SerializeField]
    private float duration;

    private Tweener tweener;

    protected override async UniTask OnStateEnter()
    {
        this.tweener?.Kill();
        await UniTask.CompletedTask;
        Quaternion target = Quaternion.LookRotation(CalculateDirection());
        this.tweener = RootTransform.DORotateQuaternion(target, this.duration);
        await WaitForTween(this.tweener);
    }

    private UniTask WaitForTween(Tween tween)
    {
        AutoResetUniTaskCompletionSource source 
            = AutoResetUniTaskCompletionSource.Create();
        tween.OnComplete(() => source.TrySetResult());
        return source.Task;
    }

    private Vector3 CalculateDirection()
    {
        if (this.transformSet.IsEmptyOrNull()) return Vector3.zero;
        return this.transformSet.GetClosestFrom(RootTransform).position - RootTransform.position;
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
        this.tweener.Complete(true);
        this.tweener?.Kill();
    }

    public override void ResetName()
    {
        base.ResetName();
        gameObject.name = "Rotate to Target Async";
    }
}