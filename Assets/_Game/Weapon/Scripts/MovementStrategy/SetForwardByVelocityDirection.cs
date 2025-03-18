using Dt.Attribute;
using UnityEngine;

public class SetForwardByVelocityDirection : MonoBehaviour
{
    [SerializeField, Required]
    private Rigidbody rb;

    private void FixedUpdate()
    {
        transform.forward = this.rb.linearVelocity.normalized;
    }
}