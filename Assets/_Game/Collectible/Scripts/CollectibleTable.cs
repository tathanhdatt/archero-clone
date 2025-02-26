using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectible/Table")]
public class CollectibleTable : ScriptableObject
{
    public List<CollectibleItem> items;
}