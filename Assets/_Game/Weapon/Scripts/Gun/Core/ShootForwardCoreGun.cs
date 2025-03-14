using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;

public class ShootForwardCoreGun : CoreGun
{
    [SerializeField, Required]
    private Bullet bulletPrefab;

    [SerializeField, Required]
    private IntVariable numberBullets;

    [SerializeField, Required]
    private float spaceBetweenBullets;

    public override Bullet BulletPrefab => this.bulletPrefab;

    public override event Action OnShot;

    [Button]
    public override void Shoot()
    {
        Vector3 position = -this.spaceBetweenBullets *
            (this.numberBullets.Value - 1) * transform.right / 2;
        for (int i = 0; i < this.numberBullets.Value; i++)
        {
            Bullet bullet = Instantiate(this.bulletPrefab);
            Vector3 offset = position + this.spaceBetweenBullets * i * transform.right;
            bullet.transform.position = transform.position + offset;
            bullet.transform.forward = transform.forward;
            bullet.gameObject.SetActive(true);
            bullet.Initialize();
            bullet.Fire();
        }
    }
}