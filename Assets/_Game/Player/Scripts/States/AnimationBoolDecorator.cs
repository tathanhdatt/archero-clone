using System.Linq;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class AnimationBoolDecorator : StateDecorator
{
    [SerializeField, Required]
    private Animator animator;

    [SerializeField, ValueDropdown(nameof(GetVarNames))]
    private string boolVarName;

    [SerializeField]
    private bool enterValue;

    [SerializeField]
    private bool exitValue;

    [Line]
    [SerializeField]
    private bool waitTransition;

    [SerializeField, ShowIf(nameof(waitTransition))]
    private int layer;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        this.animator.SetBool(this.boolVarName, this.enterValue);
        if (this.waitTransition)
        {
            await UniTask.WaitUntil(() => !this.animator.IsInTransition(this.layer));
        }
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
        this.animator.SetBool(this.boolVarName, this.exitValue);
    }

    [Button]
    public override void ResetName()
    {
        gameObject.name =
            $"Bool [{this.boolVarName}]: \"{this.enterValue}\" <-> \"{this.exitValue}\"";
    }

    private string[] GetVarNames()
    {
        return this.animator.parameters
            .Where(parameter => parameter.type == AnimatorControllerParameterType.Bool)
            .Select(parameter => parameter.name).ToArray();
    }
}