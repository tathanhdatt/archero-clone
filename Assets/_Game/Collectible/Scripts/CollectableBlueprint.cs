using UnityEngine;

[CreateAssetMenu(menuName = "Collectible/Blueprint")]
public class CollectableBlueprint : ScriptableObject
{
    public CollectibleType collectibleType;
    public CollectibleObject prefab;
}