using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;
using UnityEngine.Events;

public class MoveRandomInRange : StateDecorator
{
    [SerializeField]
    private float range;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotateDuration;

    [SerializeField, ReadOnly]
    private Vector3 target;

    private Tweener tweener;
    public UnityEvent onCompleted;

    protected override async UniTask OnStateEnter()
    {
        this.tweener?.Kill();
        this.target = RootTransform.position + Random.insideUnitSphere * this.range;
        this.target.y = RootTransform.position.y;
        await RotateToTarget();
        this.tweener = RootTransform.DOMove(this.target, this.speed);
        this.tweener.SetSpeedBased(true);
        this.tweener.OnComplete(OnMoveCompleted);
    }

    private void OnMoveCompleted()
    {
        this.onCompleted?.Invoke();
    }

    private async UniTask RotateToTarget()
    {
        Vector3 dir = this.target - RootTransform.position;
        Quaternion quaternion = Quaternion.LookRotation(dir);
        this.tweener = RootTransform.DORotateQuaternion(quaternion, this.rotateDuration);
        await this.tweener.AsyncWaitForCompletion();
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
        this.tweener?.Kill();
    }

    private void OnDrawGizmosSelected()
    {
        DebugExtension.DrawCircle(
            transform.position, transform.up, Color.red,
            this.range);
    }
}