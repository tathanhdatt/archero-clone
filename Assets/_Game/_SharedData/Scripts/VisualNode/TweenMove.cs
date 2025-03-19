using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class TweenMove : VisualNode, ILeafNode
{
    [Header("Preference")]
    [SerializeField, Required]
    private Transform source;

    [SerializeField, Required]
    private Transform target;

    [Header("Config")]
    [SerializeField]
    private float duration;

    [SerializeField]
    private Ease ease;

    private Tween tweener;

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        this.tweener?.Kill();
        this.tweener = this.source.DOMove(this.target.position, this.duration);
        this.tweener.SetEase(this.ease);
        await this.tweener.ToUniTask1(cancellationToken);
    }
}