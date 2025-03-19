using Dt.Attribute;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class RaiseGameEventOnTriggerEnter : MonoBehaviour
{
    [SerializeField, Required]
    private GameEvent gameEvent;

    private void OnTriggerEnter(Collider other)
    {
        this.gameEvent.Raise();
    }
}