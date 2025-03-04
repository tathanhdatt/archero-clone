using UnityEngine;
using UnityEngine.Events;

public class AnimationEventListener : MonoBehaviour
{
    public string eventName;
    public UnityEvent response;

    public void OnEventRaised()
    {
        this.response.Invoke();
    }
}