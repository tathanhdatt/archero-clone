using UnityEngine;

public class DestroyOnTriggerEnter : MonoBehaviour
{
    [SerializeField]
    private Tag destroyTag;

    private void OnTriggerEnter(Collider other)
    {
        Tags tags = other.GetComponent<Tags>();
        if (tags == null) return;
        if (tags.GetTags().Contains(this.destroyTag))
        {
            Destroy(gameObject);
        }
    }
}