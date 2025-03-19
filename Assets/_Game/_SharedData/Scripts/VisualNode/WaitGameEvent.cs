using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class WaitGameEvent : VisualNode, ILeafNode
{
    [SerializeField, Required]
    private GameEvent gameEvent;

    [SerializeField, ReadOnly]
    private bool isEventRaised;

    protected override async UniTask OnStartRunning(CancellationToken cancellationToken)
    {
        this.isEventRaised = false;
        await base.OnStartRunning(cancellationToken);
        this.gameEvent.OnRaised += OnEventRaised;
    }

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.WaitUntil(IsEventRaised, cancellationToken: cancellationToken);
    }

    protected override async UniTask OnEndRunning(CancellationToken cancellationToken)
    {
        await base.OnEndRunning(cancellationToken);
        this.gameEvent.OnRaised -= OnEventRaised;
    }

    private void OnEventRaised()
    {
        this.isEventRaised = true;
    }

    private bool IsEventRaised()
    {
        return this.isEventRaised;
    }
}