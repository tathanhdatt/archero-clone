using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;
using UnityEngine.Events;

public class MoveRandomInRange : StateDecorator
{
    [SerializeField]
    private float maxRange;

    [SerializeField]
    private float minRange;

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
        this.target = GetRandomTarget();
        this.target += RootTransform.position;
        this.target.y = RootTransform.position.y;
        await RotateToTarget();
        this.tweener = RootTransform.DOMove(this.target, this.speed);
        this.tweener.SetSpeedBased(true);
        this.tweener.OnComplete(OnMoveCompleted);
    }

    private Vector3 GetRandomTarget()
    {
        Vector3 randomTarget = Random.insideUnitSphere * this.maxRange;
        if (Mathf.Abs(randomTarget.x) < this.minRange)
        {
            randomTarget.x = Mathf.Sign(randomTarget.x) * this.minRange;
            float noise = Random.Range(0f, 1f);
            randomTarget.x += noise;
        }

        if (Mathf.Abs(randomTarget.z) < this.minRange)
        {
            randomTarget.z = Mathf.Sign(randomTarget.z) * this.minRange;
            float noise = Random.Range(0f, 1f);
            randomTarget.z += noise;
        }

        return randomTarget;
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
            this.maxRange);
    }
}