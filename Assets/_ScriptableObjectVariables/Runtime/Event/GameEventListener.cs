using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Required]
    public GameEvent @event;
    public UnityEvent response;

    public void OnEventRaised()
    {
        this.response?.Invoke();
    }

    private void OnEnable()
    {
        this.@event.RegisterListener(this);
    }

    private void OnDisable()
    {
        this.@event.UnregisterListener(this);
    }
}