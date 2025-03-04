using Dt.Attribute;
using UnityEngine;

public class GameEventInvoker : MonoBehaviour
{
    [SerializeField, Required]
    private GameEvent @event;

    [Button]
    public void RaiseEvent()
    {
        this.@event.Raise();
    }
}