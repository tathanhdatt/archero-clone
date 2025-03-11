using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEditor;
using UnityEngine;

public class WaitForAllGameObjectDisabled : VisualNode, ILeafNode
{
    [SerializeField]
    private List<GameObject> gameObjects;

    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.WaitUntil(IsAllGameObjectsDisabled, cancellationToken: cancellationToken);
    }

    private bool IsAllGameObjectsDisabled()
    {
        return this.gameObjects.All(go => !go.activeSelf);
    }
#if UNITY_EDITOR
    [MenuItem("GameObject/Behaviour Tree/Leaf/Wait All GameObjects Disabled", false, -10000)]
    public static void Create(MenuCommand menuCommand)
    {
        NodeHierarchyCreator.CreateSingleGameObject<WaitForAllGameObjectDisabled>(menuCommand);
    }
#endif
}