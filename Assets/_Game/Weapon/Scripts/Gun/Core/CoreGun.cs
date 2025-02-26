using System;
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
}