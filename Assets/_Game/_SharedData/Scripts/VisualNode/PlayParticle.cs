using System.Threading;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.BehaviourTree.Leaf;
using UnityEngine;

public class PlayParticle : VisualNode, ILeafNode
{
    [SerializeField, Required]
    private ParticleSystem particle;

    [SerializeField]
    private bool withChildren;
    protected override async UniTask OnRunning(CancellationToken cancellationToken)
    {
        await UniTask.CompletedTask;
        this.particle.Play(this.withChildren);
    }
}