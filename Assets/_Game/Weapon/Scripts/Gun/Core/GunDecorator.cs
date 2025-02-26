using System;

public abstract class GunDecorator : Gun
{
    protected Gun Gun;
    public override Bullet BulletPrefab => this.Gun.BulletPrefab;
    public override event Action OnShot;

    public void Initialize(Gun gun)
    {
        this.Gun = gun;
        this.Gun.OnShot += OnShotHandler;
    }

    public override void Shoot()
    {
        if (isActiveAndEnabled)
        {
            OnShooting();
        }

        this.Gun.Shoot();
    }

    private void OnShotHandler()
    {
        OnShot?.Invoke();
    }

    protected virtual void OnShooting()
    {
    }
}