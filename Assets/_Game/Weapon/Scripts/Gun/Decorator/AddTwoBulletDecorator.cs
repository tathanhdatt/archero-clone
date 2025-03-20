using UnityEngine;

public class AddTwoBulletDecorator : GunDecorator
{
    [SerializeField, Range(0f, 90f)]
    private float angle;

    protected override void OnShooting()
    {
        Bullet bullet = NativeObjectPooling.Spawn(BulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.forward = transform.forward;
        bullet.transform.Rotate(new Vector3(0, this.angle, 0), Space.World);
        bullet.Initialize(BulletPrefab);
        bullet.gameObject.SetActive(true);
        bullet.Fire();
        bullet = NativeObjectPooling.Spawn(BulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.forward = transform.forward;
        bullet.transform.Rotate(new Vector3(0, -this.angle, 0), Space.World);
        bullet.Initialize(BulletPrefab);
        bullet.gameObject.SetActive(true);
        bullet.Fire();
        base.OnShooting();
    }
}