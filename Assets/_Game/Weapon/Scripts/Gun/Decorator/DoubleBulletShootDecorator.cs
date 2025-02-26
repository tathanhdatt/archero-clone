using UnityEngine;

public class DoubleBulletShootDecorator : GunDecorator
{
    [SerializeField, Range(0f, 1f)]
    private float spaceBetweenBullets;

    protected override void OnShooting()
    {
        Bullet bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.forward = transform.forward;
        bullet.transform.Translate(
            bullet.transform.right * -this.spaceBetweenBullets / 2, 
            Space.World);
        bullet.gameObject.SetActive(true);
        bullet.Fire();
        bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.forward = transform.forward;
        bullet.transform.Translate(
            bullet.transform.right * this.spaceBetweenBullets / 2,
            Space.World);
        bullet.gameObject.SetActive(true);
        bullet.Fire();
        base.OnShooting();
    }
}