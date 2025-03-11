using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class WaitGameEventRaised : VisualNode, ILeafNode
{
    [SerializeField, ReadOnly]
    private bool isRaised;

    public void OnEventRaised()
    {
        this.isRaised = true;
    }

    protected override async UniTask OnStartRunning(CancellationToken cancellationToken)
    {
        await base.OnStartRunning(cancellationToken);
        this.isRaised = false;
    }

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.WaitUntil(() => this.isRaised, cancellationToken: cancellationToken);
    }
#if UNITY_EDITOR
    [MenuItem("GameObject/Behaviour Tree/Leaf/Wait GameEvent Raised", false, -10000)]
    public static void Create(MenuCommand menuCommand)
    {
        NodeHierarchyCreator.CreateSingleGameObject<WaitGameEventRaised>(menuCommand);
    }
#endif
}