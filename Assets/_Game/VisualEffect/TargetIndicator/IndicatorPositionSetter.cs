using Dt.Attribute;
using UnityEngine;

public class IndicatorPositionSetter : MonoBehaviour
{
    [SerializeField, Required]
    private TransformSet transformSet;

    [SerializeField, Required]
    private Transform indicatorTransform;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private bool keepX;

    [SerializeField]
    private bool keepY;

    [SerializeField]
    private bool keepZ;

    [SerializeField, ReadOnly]
    private Vector3 originalPosition;

    private void Awake()
    {
        this.originalPosition = this.indicatorTransform.position;
    }

    private void LateUpdate()
    {
        if (this.transformSet.items.IsEmpty())
        {
            this.indicatorTransform.gameObject.SetActive(false);
            return;
        }

        if (!this.indicatorTransform.gameObject.activeSelf)
        {
            this.indicatorTransform.gameObject.SetActive(true);
        }

        Vector3 position = this.transformSet.GetClosestFrom(transform).position;
        if (this.keepX)
        {
            position.x = this.originalPosition.x;
        }

        if (this.keepY)
        {
            position.y = this.originalPosition.y;
        }

        if (this.keepZ)
        {
            position.z = this.originalPosition.z;
        }

        this.indicatorTransform.position = position + this.offset;
    }
}