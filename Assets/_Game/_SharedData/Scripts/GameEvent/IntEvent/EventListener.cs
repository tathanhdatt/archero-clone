using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T> : MonoBehaviour
{
    public EventGeneric<T> eventGeneric;
    public UnityEvent<T> response;

    public void OnEventRaised(T value)
    {
        this.response?.Invoke(value);
    }

    private void OnEnable()
    {
        this.eventGeneric.RegisterListener(this);
    }

    private void OnDisable()
    {
        this.eventGeneric.UnregisterListener(this);
    }
}