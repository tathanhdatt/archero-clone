using System;
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

    public void Initialize()
    {
        if (this.isInitialized) return;
        this.isInitialized = true;
        Decorate();
    }

    [Button]
    public override void Shoot()
    {
        this.wrapper?.Shoot();
    }

    private void Decorate()
    {
        if (this.decorators.Length == 0)
        {
            this.wrapper = this.coreGun;
            return;
        }

        if (this.decorators.Length == 1)
        {
            this.decorators.First().Initialize(this.coreGun);
            this.wrapper = this.decorators.First();
            return;
        }

        for (int i = 0; i < this.decorators.Length - 1; i++)
        {
            this.decorators[i].Initialize(this.decorators[i + 1]);
        }

        this.decorators.Last().Initialize(this.coreGun);
        this.wrapper = this.decorators.First();
    }

    [Button]
    private void GetDecorators()
    {
        this.decorators = GetComponentsInChildren<GunDecorator>(true);
    }
}