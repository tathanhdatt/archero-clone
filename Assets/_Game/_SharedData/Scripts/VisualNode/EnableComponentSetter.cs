using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class EnableComponentSetter : VisualNode, ILeafNode
{
    [SerializeField, ComponentReference]
    private MonoBehaviour monoBehaviour;

    [SerializeField]
    private bool enable;

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        this.monoBehaviour.enabled = this.enable;
        await UniTask.CompletedTask;
    }
}