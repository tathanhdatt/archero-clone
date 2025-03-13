using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class FloatVariableSetter : VisualNode, ILeafNode
{
    [SerializeField, Required]
    private FloatVariable source;

    [SerializeField, Required]
    private FloatVariable target;

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.CompletedTask;
        this.target.Value = this.source.Value;
    }
}