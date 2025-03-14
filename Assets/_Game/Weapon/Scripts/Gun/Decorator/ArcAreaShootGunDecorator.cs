using UnityEngine;

public class ArcAreaShootGunDecorator : GunDecorator
{
    [SerializeField]
    private float startEulerAngle;

    [SerializeField]
    private float endEulerAngle;

    [SerializeField]
    private float angleOffset;

    [SerializeField, Min(1)]
    private int numberOfBullets;

    protected override void OnShooting()
    {
        base.OnShooting();
        SpawnBullets();
    }

    private void SpawnBullets()
    {
        for (int i = 0; i < this.numberOfBullets; i++)
        {
            Bullet bullet = Instantiate(BulletPrefab);
            bullet.transform.position = transform.position;
            // bullet.transform.forward = transform.forward;
            float eulerAngle = Random.Range(this.startEulerAngle, this.endEulerAngle);
            eulerAngle += transform.eulerAngles.y;
            bullet.transform.rotation = Quaternion.Euler(0, eulerAngle + this.angleOffset, 0);
            bullet.Initialize();
            bullet.Fire();
        }
    }

    private void OnDrawGizmosSelected()
    {
        DebugExtension.DrawArc(transform.position, 1,
            this.startEulerAngle, this.endEulerAngle,
            transform.right, transform.forward);
    }
}