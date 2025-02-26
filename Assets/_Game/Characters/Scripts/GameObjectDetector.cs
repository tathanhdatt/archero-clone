using Dt.Attribute;
using Dt.Extension;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectDetector : MonoBehaviour
{
    [SerializeField, ValueDropdown(ValueDropdownField.Tag)]
    private string objectTag;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float magnitude;

    [SerializeField, ReadOnly]
    private bool isDetected;

    [SerializeField, ReadOnly]
    private bool isLost;

    public UnityEvent onObjectDetected;
    public UnityEvent onObjectLost;

    private readonly RaycastHit[] hits = new RaycastHit[2];

    private void Awake()
    {
        this.isDetected = false;
        this.isLost = true;
    }

    private void Update()
    {
        this.hits.Clear();
        int numberHits = Physics.RaycastNonAlloc(transform.position + this.offset,
            transform.forward, this.hits, this.magnitude);
        if (numberHits == 0)
        {
            HandleOnLost();
            return;
        }

        HandleHasHits();
    }

    private void HandleHasHits()
    {
        foreach (RaycastHit hit in this.hits)
        {
            if (hit.collider == null)
            {
                continue;
            }
            if (hit.collider.CompareTag(this.objectTag))
            {
                HandleOnDetected();
                return;
            }
        }

        HandleOnLost();
    }

    private void HandleOnDetected()
    {
        if (this.isLost)
        {
            this.isDetected = true;
            this.isLost = false;
            this.onObjectDetected?.Invoke();
        }
    }

    private void HandleOnLost()
    {
        if (this.isDetected)
        {
            this.isDetected = false;
            this.isLost = true;
            this.onObjectLost?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 start = transform.position + this.offset;
        Vector3 end = transform.position + this.offset + transform.forward * this.magnitude;
        Debug.DrawLine(start, end, Color.red);
    }
}