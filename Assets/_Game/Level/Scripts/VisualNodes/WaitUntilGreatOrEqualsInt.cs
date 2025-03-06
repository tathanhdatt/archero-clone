using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using UnityEngine;

public class WaitUntilGreatOrEqualsInt : VisualNode
{
    [SerializeField]
    private int expectedInt;

    [SerializeField, ReadOnly]
    private int actualInt;

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.WaitUntil(() => this.actualInt == this.expectedInt,
            cancellationToken: cancellationToken);
    }
}