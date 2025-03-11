using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;

public class CoreGun : Gun
{
    [SerializeField, Required]
    private Bullet bulletPrefab;

    public override Bullet BulletPrefab=> this.bulletPrefab;

    public override event Action OnShot;

    public override void Shoot()
    {
    }

    public override async UniTask Initialize()
    {
        await UniTask.CompletedTask;
    }

    public override async UniTask Terminate()
    {
        await UniTask.CompletedTask;
    }
}