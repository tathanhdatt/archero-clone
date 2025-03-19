using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField, Required]
    private MovementStrategy movementStrategy;

    [SerializeField, Required]
    private TargetProviderStrategy targetProviderStrategy;
    
    [SerializeField, Required]
    private EffectCollection effectCollection;

    private Bullet originalBullet;
    public UnityEvent onDespawn;

    public void Initialize(Bullet originalBullet)
    {
        this.originalBullet = originalBullet;
        this.effectCollection.UpdateEffect();
    }

    public void Fire()
    {
        this.movementStrategy.Move(transform, this.targetProviderStrategy.GetTargetPosition());
    }

    public void Despawn()
    {
        this.onDespawn?.Invoke();
        NativeObjectPooling.Despawn(this.originalBullet, this);
    }
}