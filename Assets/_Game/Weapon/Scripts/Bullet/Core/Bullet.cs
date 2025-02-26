using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField, Required]
    private MovementStrategy movementStrategy;

    [SerializeField, Required]
    private TargetProviderStrategy targetProviderStrategy;

    public UnityEvent onDespawn;

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