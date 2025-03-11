using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Data")]
public class InventoryData : ScriptableObject
{
    public List<EquipmentData> equippedEquipment;
    public List<EquipmentData> availableEquipments;

    public void UnequipEquipment(EquipmentData equipmentData)
    {
        equipmentData.updaterComponents?.Downgrade();
        this.equippedEquipment.Remove(equipmentData);
        this.availableEquipments.Add(equipmentData);
    }

    public void EquipEquipment(EquipmentData equipmentData)
    {
        equipmentData.updaterComponents?.Upgrade();
        this.equippedEquipment.Add(equipmentData);
        this.availableEquipments.Remove(equipmentData);
    }
}