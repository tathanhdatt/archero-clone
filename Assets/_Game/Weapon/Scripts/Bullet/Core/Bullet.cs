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

    public UnityEvent onDespawn;

    public void Initialize()
    {
        this.effectCollection.UpdateEffect();
    }

    public void Fire()
    {
        this.movementStrategy.Move(transform, this.targetProviderStrategy.GetTargetPosition());
    }

    public void Despawn()
    {
        this.onDespawn?.Invoke();
        Destroy(gameObject);
    }
}