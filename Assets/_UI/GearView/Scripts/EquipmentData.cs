using Dt.Attribute;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment Data")]
public class EquipmentData : ScriptableObject
{
    [SpritePreview]
    public Sprite icon;
    [TextArea(2, 10)]
    public string description;
    public EquipmentType type;
    public AbilityUpgradeComponent updaterComponents;
}