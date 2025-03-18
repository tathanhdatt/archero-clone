using Dt.Attribute;
using UnityEngine;

public class SetIsTriggerByBoolVariable : MonoBehaviour
{
    [SerializeField, Required]
    private BoolVariable variable;
    
    [SerializeField, Required]
    private Collider triggerCollider;

    private void OnEnable()
    {
        this.triggerCollider.isTrigger = this.variable.Value;
        this.variable.OnValueChanged += OnTriggerChangedHandler;
    }

    private void OnTriggerChangedHandler()
    {
        this.triggerCollider.isTrigger = this.variable.Value;
    }
}