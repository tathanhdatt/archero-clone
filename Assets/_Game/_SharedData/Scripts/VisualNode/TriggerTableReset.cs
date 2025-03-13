using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class TriggerTableReset : VisualNode, ILeafNode
{
    [SerializeField, Required]
    private List<TriggerTable> triggerTables;

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.CompletedTask;
        this.triggerTables.ForEach(table=>table.ResetTable());
    }
}