using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;

public class ChangeMaterialDecorator : StateDecorator
{
    [SerializeField, Required]
    private Material material;

    [SerializeField]
    private bool recoverOnExit;
    
    [SerializeField, Required]
    private Renderer ren;
    
    private Material originalMaterial;

    protected override async UniTask OnStateEnter()
    {
        this.originalMaterial = this.ren.material;
        this.ren.material = this.material;
        await UniTask.CompletedTask;;
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;;
    }

    protected override async UniTask OnStateExit()
    {
        await UniTask.CompletedTask;;
        if (this.recoverOnExit)
        {
            this.ren.material = this.originalMaterial;
        }
    }

    [Button]
    private void Rename()
    {
        gameObject.name = $"Change material to {this.material.name}";
    }
}