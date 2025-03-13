using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class TransformScaler : StateDecorator
{
    [SerializeField, Required]
    private Transform target;

    [SerializeField]
    private Vector3 targetScale;

    [SerializeField, Tooltip("Seconds")]
    private float duration;

    [SerializeField]
    private Ease ease;

    [SerializeField]
    private bool async;

    private Tweener tweener;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        this.tweener?.Kill();
        this.tweener = this.target.DOScale(this.targetScale, this.duration);
        if (this.async)
        {
            await this.tweener.AsyncWaitForCompletion();
        }

        this.tweener.SetEase(this.ease);
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
    }
}