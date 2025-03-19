using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class SetBoolVariable : VisualNode, ILeafNode
{
    [SerializeField, Required]
    private BoolVariable variable;
    
    [SerializeField]
    private bool value;
    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        this.variable.Value = this.value;
        await UniTask.CompletedTask;
    }
}