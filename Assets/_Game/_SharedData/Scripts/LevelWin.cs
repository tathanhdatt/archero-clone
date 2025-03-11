using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;

public class LevelWin : VisualNode, ILeafNode
{
    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.CompletedTask;
        Messenger.Broadcast(Message.LevelWin);
    }
}