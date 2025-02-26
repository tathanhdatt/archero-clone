using System.Linq;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class AnimationTrigger : StateDecorator
{
    [SerializeField, Required]
    private Animator animator;

    [SerializeField, ValueDropdown(nameof(GetTriggerNames))]
    private string triggerVar;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        this.animator.SetTrigger(this.triggerVar);
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;
    }

    [Button]
    private void Rename()
    {
        gameObject.name = $"Trigger: \"{this.triggerVar}\"";
    }

#if UNITY_EDITOR
    private string[] GetTriggerNames()
    {
        return this.animator.parameters.Where(parameter => parameter.type == 
            AnimatorControllerParameterType.Trigger).Select(parameter => parameter.name).ToArray();
    }
#endif
}