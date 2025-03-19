using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class EnableComponentSetter : VisualNode, ILeafNode
{
    [SerializeField, Component]
    private MonoBehaviour component;
    
    [SerializeField]
    private bool enable;

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        this.component.enabled = this.enable;
        await UniTask.CompletedTask;
    }

    [Button]
    private void Clear()
    {
        this.component = null;
    }
}