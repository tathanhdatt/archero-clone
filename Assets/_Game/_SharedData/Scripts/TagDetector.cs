using UnityEngine;
using UnityEngine.Events;

public class TagDetector : MonoBehaviour
{
    [SerializeField]
    private Tag destroyTag;
    
    public UnityEvent onTagDetected;

    private void OnTriggerEnter(Collider other)
    {
        Tags tags = other.GetComponent<Tags>();
        if (tags == null) return;
        if (tags.GetTags().Contains(this.destroyTag))
        {
            this.onTagDetected?.Invoke();
        }
    }
}