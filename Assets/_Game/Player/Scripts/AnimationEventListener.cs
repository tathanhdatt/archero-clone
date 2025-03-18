using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventListener : MonoBehaviour
{
    public List<UnityEvent> responses;

    public void OnEventRaised(int eventId)
    {
        this.responses[eventId]?.Invoke();
    }
}