using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;

[RequireComponent(typeof(CoreGun))]
public class GunWrapper : InitializableMono
{
    [SerializeField, Required]
    private CoreGun coreGun;

    [SerializeField]
    private GunDecorator[] decorators;
    
    [SerializeField, ReadOnly]
    private Gun wrapper;

    [SerializeField, ReadOnly]
    private bool isInitialized;

    public event Action OnShot;

    public override async UniTask Initialize()
    {
        await UniTask.CompletedTask;
        if (this.isInitialized) return;
        this.isInitialized = true;
        await Decorate();
    }

    [Button]
    public void Shoot()
    {
        this.wrapper?.Shoot();
    }

    private async UniTask Decorate()
    {
        if (this.decorators.Length == 0)
        {
            this.wrapper = this.coreGun;
            return;
        }

        if (this.decorators.Length == 1)
        {
            await this.decorators.First().Initialize(this.coreGun);
            this.wrapper = this.decorators.First();
            return;
        }

        for (int i = 0; i < this.decorators.Length - 1; i++)
        {
            await this.decorators[i].Initialize(this.decorators[i + 1]);
        }

        await this.decorators.Last().Initialize(this.coreGun);
        this.wrapper = this.decorators.First();
    }

    public override async UniTask Terminate()
    {
        await UniTask.CompletedTask;
    }

    [Button]
    private void GetDecorators()
    {
        this.decorators = GetComponentsInChildren<GunDecorator>(true);
    }
}