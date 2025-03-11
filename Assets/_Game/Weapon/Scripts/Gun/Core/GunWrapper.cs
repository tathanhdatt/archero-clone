using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;

public class GunWrapper : Gun
{
    [SerializeField, Required]
    private Gun coreGun;

    [SerializeField]
    private GunDecorator[] decorators;
    
    [SerializeField, ReadOnly]
    private Gun wrapper;

    [SerializeField, ReadOnly]
    private bool isInitialized;

    public override Bullet BulletPrefab => this.wrapper.BulletPrefab;

    public override event Action OnShot;

    public override async UniTask Initialize()
    {
        await UniTask.CompletedTask;
        if (this.isInitialized) return;
        this.isInitialized = true;
        await Decorate();
    }

    [Button]
    public override void Shoot()
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