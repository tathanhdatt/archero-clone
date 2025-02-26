using UnityEngine;

public abstract class TargetProviderStrategy : MonoBehaviour
{
    public abstract Vector3 GetTargetPosition();
}