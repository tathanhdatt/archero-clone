using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;

public class EmptyCoreGun : CoreGun
{
    [SerializeField, Required]
    private Bullet bulletPrefab;

    public override Bullet BulletPrefab => this.bulletPrefab;
    public override event Action OnShot;

    public override void Shoot()
    {
    }
}