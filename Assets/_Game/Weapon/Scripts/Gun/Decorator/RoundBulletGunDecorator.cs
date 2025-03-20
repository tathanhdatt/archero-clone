using System.Collections.Generic;
using MEC;
using UnityEngine;

public class RoundBulletGunDecorator : GunDecorator
{
    [SerializeField, Range(1, 12)]
    private int numberOfBullets;

    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private float delay;

    [SerializeField]
    private float offset;

    private CoroutineHandle coroutineHandle;

    protected override void OnShooting()
    {
        base.OnShooting();
        if (this.coroutineHandle != default)
        {
            Timing.KillCoroutines(this.coroutineHandle);
        }

        this.coroutineHandle = Timing.RunCoroutine(SpawnBulletDelay());
    }

    private IEnumerator<float> SpawnBulletDelay()
    {
        yield return Timing.WaitForSeconds(this.delay);
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        Bullet prefab = this.bulletPrefab == null ? BulletPrefab : this.bulletPrefab;
        float eulerAngleBetweenBullet = 360f / this.numberOfBullets;
        float angle = -eulerAngleBetweenBullet * (this.numberOfBullets - 1) / 2f;
        angle += this.offset;
        float yAngle = transform.eulerAngles.y;
        for (int i = 0; i < this.numberOfBullets; i++)
        {
            Bullet bullet = NativeObjectPooling.Spawn(prefab);
            bullet.transform.rotation = Quaternion.Euler(0, yAngle + angle, 0);
            bullet.transform.position = transform.position;
            bullet.Initialize(prefab);
            bullet.gameObject.SetActive(true);
            bullet.Fire();
            angle += eulerAngleBetweenBullet;
        }
    }
}