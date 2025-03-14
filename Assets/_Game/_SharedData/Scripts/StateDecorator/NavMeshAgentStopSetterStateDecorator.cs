using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentStopSetterStateDecorator : StateDecorator
{
    [SerializeField, Required]
    private NavMeshAgent agent;

    [SerializeField]
    private bool isStopped;

    protected override async UniTask OnStateEnter()
    {
        this.agent.isStopped = this.isStopped;
        await UniTask.CompletedTask;
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
        gameObject.name =
            $"Set NMAgent [{this.agent.name}] [{(this.isStopped ? "Stop" : "Continue")}]";
        base.ResetName();
    }
}