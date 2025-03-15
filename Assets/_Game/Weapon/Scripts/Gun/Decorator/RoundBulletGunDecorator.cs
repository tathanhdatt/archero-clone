using UnityEngine;

public class RoundBulletGunDecorator : GunDecorator
{
    [SerializeField, Range(1, 12)]
    private int numberOfBullets;
    
    [SerializeField]
    private Bullet bulletPrefab;

    protected override void OnShooting()
    {
        base.OnShooting();
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        Bullet prefab = this.bulletPrefab== null ? BulletPrefab : this.bulletPrefab;
        float eulerAngleBetweenBullet = 360f / this.numberOfBullets;
        float angle = -eulerAngleBetweenBullet * (this.numberOfBullets - 1) / 2f;
        for (int i = 0; i < this.numberOfBullets; i++)
        {
            Bullet bullet = Instantiate(prefab, 
                transform.position, 
                Quaternion.Euler(0, angle, 0));
            bullet.transform.position = transform.position;
            bullet.Initialize();
            bullet.Fire();
            angle += eulerAngleBetweenBullet;
        }
    }
}