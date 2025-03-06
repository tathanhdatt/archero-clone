public class SingleBulletShootDecorator : GunDecorator
{
    protected override void OnShooting()
    {
        Bullet bullet = Instantiate(BulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.forward = transform.forward;
        bullet.gameObject.SetActive(true);
        bullet.Initialize();
        bullet.Fire();
        base.OnShooting();
    }
}