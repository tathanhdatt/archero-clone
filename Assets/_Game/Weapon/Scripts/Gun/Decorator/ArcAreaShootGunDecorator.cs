using UnityEngine;

public class ArcAreaShootGunDecorator : GunDecorator
{
    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private float startEulerAngle;

    [SerializeField]
    private float endEulerAngle;

    private const float AngleOffset = -90f;

    [SerializeField, Min(1)]
    private int numberOfBullets;

    protected override void OnShooting()
    {
        base.OnShooting();
        SpawnBullets();
    }

    private void SpawnBullets()
    {
        Bullet prefab = this.bulletPrefab == null ? BulletPrefab : this.bulletPrefab;
        for (int i = 0; i < this.numberOfBullets; i++)
        {
            Bullet bullet = NativeObjectPooling.Spawn(prefab);
            bullet.transform.position = transform.position;
            float eulerAngle = Random.Range(this.startEulerAngle, this.endEulerAngle);
            eulerAngle += transform.eulerAngles.y;
            bullet.transform.rotation = Quaternion.Euler(0, eulerAngle + AngleOffset, 0);
            bullet.Initialize(prefab);
            bullet.Fire();
        }
    }

    private void OnDrawGizmosSelected()
    {
        DebugExtension.DrawArc(transform.position, 1,
            this.startEulerAngle , 
            this.endEulerAngle,
            transform.right, transform.forward);
    }
}