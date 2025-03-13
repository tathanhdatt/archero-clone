using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;

public class CombatLevelUp : VisualNode, ILeafNode
{
    [Button]
    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.CompletedTask;
        Messenger.Broadcast(Message.CombatLevelUp);
    }
}