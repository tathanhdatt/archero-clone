using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class SetPosition : VisualNode, ILeafNode
{
    [SerializeField, Required]
    private Transform source;

    [SerializeField]
    private bool useVector3;

    [SerializeField, ShowIf(nameof(useVector3))]
    private Vector3 position;

    [SerializeField, HideIf(nameof(useVector3))]
    private Transform target;

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        Vector3 pos = this.useVector3 ? this.position : this.target.position;
        this.source.position = pos;
        await UniTask.CompletedTask;
    }
}