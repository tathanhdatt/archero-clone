using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Dt.StateMachine.Core;
using UnityEngine;

public class EnableGameObjectsStateDecorator : StateDecorator
{
    [SerializeField]
    private List<GameObject> gameObjects;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        this.gameObjects.ForEach(go => go.SetActive(true));
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
    }

    public override void ResetName()
    {
        base.ResetName();
        gameObject.name = "Enable GameObjects";
    }
}