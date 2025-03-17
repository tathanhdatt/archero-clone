using System;
using UnityEngine;

[Serializable]
public class CollectibleItem
{
    [Range(0f, 1f)]
    public float rate;
    public IntRange amountRange;
    public IntRange valueRange;
    public CollectibleObject prefab;
}

[Serializable]
public class IntRange
{
    public int min;
    public int max;
}