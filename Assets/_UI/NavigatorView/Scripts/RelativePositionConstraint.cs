using Dt.Attribute;
using UnityEngine;

public class RelativePositionConstraint : MonoBehaviour
{
    [SerializeField, Required]
    private Transform source;

    [SerializeField, Required]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float patio;

    [SerializeField, ReadOnly]
    private Vector3 lastPosition;

    [SerializeField, ReadOnly]
    private Vector3 currentPosition;

    [SerializeField, Required]
    private Camera cam;

    private void Awake()
    {
        this.lastPosition = this.currentPosition = this.source.position;
    }

    private void LateUpdate()
    {
        this.lastPosition = this.currentPosition;
        this.currentPosition = this.source.position;
        Vector3 delta = this.currentPosition - this.lastPosition;
        this.target.position += delta / (Screen.height / (this.cam.orthographicSize * 2));
    }
}