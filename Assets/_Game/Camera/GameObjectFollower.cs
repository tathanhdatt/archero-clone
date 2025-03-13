using System;
using Dt.Attribute;
using UnityEngine;

public class GameObjectFollower : MonoBehaviour
{
    [SerializeField, Required]
    private Transform target;

    [SerializeField]
    private Bound bound;
    
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private bool ignoreX;
    
    [SerializeField]
    private bool ignoreY;
    
    [SerializeField]
    
    private bool ignoreZ;

    [Button("Update Position")]
    private void LateUpdate()
    {
        Vector3 position = this.target.position + this.offset;
        position.z = Mathf.Clamp(position.z, this.bound.backward, this.bound.forward);
        position.x = Mathf.Clamp(position.x, this.bound.left, this.bound.right);
        if (this.ignoreX)
        {
            position.x = transform.position.x;
        }

        if (this.ignoreY)
        {
            position.y = transform.position.y;
        }

        if (this.ignoreZ)
        {
            position.z = transform.position.z;
        }
        transform.position = position;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 forwardLeft = new Vector3(
            this.bound.left, transform.position.y, this.bound.forward);
        Vector3 forwardRight = new Vector3(
            this.bound.right, transform.position.y, this.bound.forward);
        Vector3 backwardLeft =
            new Vector3(this.bound.left, transform.position.y, this.bound.backward);
        Vector3 backwardRight =
            new Vector3(this.bound.right, transform.position.y, this.bound.backward);
        Gizmos.DrawLine(forwardLeft, forwardRight);
        Gizmos.DrawLine(forwardRight, backwardRight);
        Gizmos.DrawLine(backwardRight, backwardLeft);
        Gizmos.DrawLine(backwardLeft, forwardLeft);
    }
}

[Serializable]
public struct Bound
{
    public float forward;
    public float backward;
    public float right;
    public float left;
}