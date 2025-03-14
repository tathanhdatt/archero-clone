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

    [SerializeField, ReadOnly]
    private Vector3 target;

    private Tweener tweener;

    public UnityEvent onCompleted;

    protected override async UniTask OnStateEnter()
    {
        this.tweener?.Kill();
        await UniTask.CompletedTask;
        this.target = RootTransform.position + Random.insideUnitSphere * this.range;
        this.target.y = RootTransform.position.y;
        this.tweener = RootTransform.DOMove(this.target, this.speed);
        this.tweener.SetSpeedBased(true);
        this.tweener.OnComplete(() => this.onCompleted?.Invoke());
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
        RootTransform.LookAt(this.target);
    }

    protected override async UniTask OnStateExit()
    {
        this.tweener?.Kill();
        await UniTask.CompletedTask;
    }

    private void OnDrawGizmosSelected()
    {
        DebugExtension.DrawCircle(
            transform.position, transform.up, Color.red,
            this.range);
    }
}