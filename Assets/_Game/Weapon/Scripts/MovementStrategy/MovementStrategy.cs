using UnityEngine;

public abstract class MovementStrategy : MonoBehaviour
{
    public abstract void Move(Transform source, Vector3 target);
}