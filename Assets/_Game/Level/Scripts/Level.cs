using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.BehaviourTree;
using Dt.StateMachine.Core;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField, Required]
    private VisualNode initPlayerAttributesNode;

    [SerializeField, Required]
    private VisualNode startingNode;

    [SerializeField]
    private List<InitializableMono> initializables;

    [SerializeField]
    private StateMachine stateMachine;

    public async UniTask Initialize()
    {
        await UniTask.CompletedTask;
        await this.initPlayerAttributesNode.Initialize();
        await this.initPlayerAttributesNode.Run(destroyCancellationToken);
        await this.startingNode.Initialize();
        foreach (InitializableMono mono in this.initializables)
        {
            await mono.Initialize();
        }

        await this.stateMachine.Initialize();
    }

    public async UniTask Play()
    {
        await UniTask.CompletedTask;
        await this.startingNode.Run(destroyCancellationToken);
    }

    public async UniTask Terminate()
    {
        await UniTask.CompletedTask;
        foreach (InitializableMono mono in this.initializables)
        {
            await mono.Terminate();
        }

        await this.stateMachine.Terminate();
    }
}