using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class SetBoolVariableStateDecorator : StateDecorator
{
    [SerializeField, Required]
    private BoolVariable variable;

    [SerializeField]
    private bool value;

    [SerializeField]
    private bool setOnEnter;

    [SerializeField]
    private bool setOnExit;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        if (this.setOnEnter)
        {
            this.variable.Value = this.value;
        }
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
        if (this.setOnExit)
        {
            this.variable.Value = this.value;
        }
    }
}