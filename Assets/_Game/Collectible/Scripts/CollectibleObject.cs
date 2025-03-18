using Dt.Attribute;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    [ReadOnly]
    public float value;
    public CollectibleType type;
}