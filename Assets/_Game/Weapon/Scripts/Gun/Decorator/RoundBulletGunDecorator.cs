using UnityEngine;

public class RoundBulletGunDecorator : GunDecorator
{
    [SerializeField, Range(1, 12)]
    private int numberOfBullets;

    protected override void OnShooting()
    {
        base.OnShooting();
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        float eulerAngleBetweenBullet = 360f / this.numberOfBullets;
        float angle = -eulerAngleBetweenBullet * (this.numberOfBullets - 1) / 2f;
        for (int i = 0; i < this.numberOfBullets; i++)
        {
            Bullet bullet = Instantiate(BulletPrefab, 
                transform.position, 
                Quaternion.Euler(0, angle, 0));
            bullet.transform.position = transform.position;
            // bullet.transform.rotation = Quaternion.Euler(0, angle, 0);
            bullet.Initialize();
            bullet.Fire();
            angle += eulerAngleBetweenBullet;
        }
    }
}