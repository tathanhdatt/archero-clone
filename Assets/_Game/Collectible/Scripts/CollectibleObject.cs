using Dt.Attribute;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    [ReadOnly]
    public int value;
    public CollectibleType type;
}