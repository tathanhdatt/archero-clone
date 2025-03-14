using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMovementStateDecorator : StateDecorator
{
    [SerializeField, Required]
    private NavMeshAgent navMeshAgent;

    [SerializeField, Required]
    private TransformSet targetTransformSet;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
        if (this.targetTransformSet.IsEmptyOrNull())
        {
            this.navMeshAgent.isStopped = true;
        }
        else
        {
            this.navMeshAgent.SetDestination(
                this.targetTransformSet
                    .GetClosestFrom(RootTransform).position);
        }
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
    }

    public override void ResetName()
    {
        base.ResetName();
        gameObject.name = "NavMeshAgent Destination Setter";
    }
}