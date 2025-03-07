using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Editor;
using Dt.BehaviourTree.Leaf;
using UnityEditor;
using UnityEngine;

public class TimeScaleSetter : VisualNode, ILeafNode
{
    [SerializeField]
    private float timeScale = 1f;
    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.CompletedTask;
        Time.timeScale = this.timeScale;
    }
#if UNITY_EDITOR
    [MenuItem("GameObject/Behaviour Tree/Leaf/Time Scale Setter", false, -10000)]
    public static void Create(MenuCommand menuCommand)
    {
        NodeHierarchyCreator.CreateSingleGameObject<TimeScaleSetter>(menuCommand);
    }
#endif
}