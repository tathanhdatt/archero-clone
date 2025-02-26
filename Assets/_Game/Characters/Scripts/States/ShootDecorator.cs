using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.StateMachine.Core;
using UnityEngine;
using UnityEngine.Events;

public class ShootDecorator : StateDecorator
{
    [SerializeField, Required]
    private FloatVariable delayTime;

    [SerializeField, Required]
    private FloatVariable reloadTime;

    [SerializeField, Required]
    private GunWrapper gun;

    [SerializeField]
    private bool resetOnEnter;

    private float remainReloadTime;

    private bool canShoot;
    public UnityEvent onShot;

    protected override async UniTask OnStateEnter()
    {
        await UniTask.CompletedTask;
        this.gun.Initialize();
        if (this.resetOnEnter)
        {
            this.remainReloadTime = 0;
        }
        await UniTask.WaitForSeconds(this.delayTime.Value);
        this.canShoot = true;
    }

    private void Shoot()
    {
        this.gun.Shoot();
        this.onShot?.Invoke();
    }

    protected override async UniTask OnStateUpdate()
    {
        await UniTask.CompletedTask;
        if (!this.canShoot) return;
        this.remainReloadTime -= Time.deltaTime;
        if (this.remainReloadTime > 0) return;
        Shoot();
        this.remainReloadTime = this.reloadTime.Value;
    }

    protected override async UniTask OnStateExit()
    {
        this.canShoot = false;
        await UniTask.CompletedTask;
    }
}