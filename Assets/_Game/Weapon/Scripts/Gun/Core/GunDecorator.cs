using System;
using Cysharp.Threading.Tasks;

public abstract class GunDecorator : Gun
{
    protected Gun Gun;
    public override Bullet BulletPrefab => this.Gun.BulletPrefab;
    public override event Action OnShot;

    public override async UniTask Initialize()
    {
        await UniTask.CompletedTask;
    }
    
    public async UniTask Initialize(Gun gun)
    {
        this.Gun = gun;
        this.Gun.OnShot += OnShotHandler;
        await UniTask.CompletedTask;
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

    public override async UniTask Terminate()
    {
        await UniTask.CompletedTask;
    }
}