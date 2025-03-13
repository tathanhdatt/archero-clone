using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;

public class GearView : BaseView
{
    [SerializeField, Required]
    private InventoryData inventoryData;

    [SerializeField, Required]
    private EquipmentUI equipmentUIPrefab;

    [SerializeField, Required]
    private Transform content;

    [SerializeField, Required]
    private List<TweenLocalPosition> transitions;

    [SerializeField]
    private TypeAndUIEquipmentDict typeAndUIEquipmentDict;

    private readonly Dictionary<EquipmentData, EquipmentUI> equipmentUIs =
        new Dictionary<EquipmentData, EquipmentUI>(10);

    private readonly Dictionary<EquipmentType, EquipmentData> equippedEquipment =
        new Dictionary<EquipmentType, EquipmentData>(2);

    public event Action<EquipmentData> OnClickEquipment;

    public override async UniTask Initialize()
    {
        await base.Initialize();
        SpawnEquipmentUI();
        DeactivateEquipmentUI();
        InitEquippedEquipment();
        InitTransitions();
    }

    private void SpawnEquipmentUI()
    {
        foreach (EquipmentData data in this.inventoryData.availableEquipments)
        {
            EquipmentUI equipmentUI = Instantiate(this.equipmentUIPrefab, this.content);
            equipmentUI.Initialize(data);
            this.equipmentUIs.Add(data, equipmentUI);
        }
    }

    private void InitEquippedEquipment()
    {
        foreach (EquipmentData data in this.inventoryData.equippedEquipment)
        {
            this.typeAndUIEquipmentDict[data.type].Initialize(data);
            this.equippedEquipment.Add(data.type, data);
            data.updaterComponents?.Upgrade();
        }
    }

    private void InitTransitions()
    {
        this.transitions.ForEach(transition=>transition.Initialize());
    }

    public override async UniTask Show()
    {
        ActivateEquipmentUI();
        await base.Show();
    }

    private void ActivateEquipmentUI()
    {
        foreach (EquipmentData data in this.inventoryData.availableEquipments)
        {
            this.equipmentUIs[data].Initialize(data);
            this.equipmentUIs[data].OnClick += OnClickHandler;
            this.equipmentUIs[data].gameObject.SetActive(true);
        }
    }

    public override async UniTask Hide()
    {
        await base.Hide();
        DeactivateEquipmentUI();
    }

    private void DeactivateEquipmentUI()
    {
        foreach (EquipmentUI ui in this.equipmentUIs.Values)
        {
            ui.OnClick -= OnClickHandler;
            ui.gameObject.SetActive(false);
        }
    }

    private void OnClickHandler(EquipmentData data)
    {
        OnClickEquipment?.Invoke(data);
    }

    public void SetEquipment(EquipmentData data)
    {
        this.typeAndUIEquipmentDict[data.type].Initialize(data);
        EquipmentData lastEquippedData = this.equippedEquipment[data.type];
        this.equippedEquipment[data.type] = data;
        SwapDataInInventory(lastEquippedData, data);
        EquipmentUI ui = this.equipmentUIs[data];
        ui.Initialize(lastEquippedData);
        this.equipmentUIs.Remove(data);
        this.equipmentUIs.Add(lastEquippedData, ui);
    }

    private void SwapDataInInventory(EquipmentData lastEquippedData, EquipmentData currentData)
    {
        this.inventoryData.UnequipEquipment(lastEquippedData);
        this.inventoryData.EquipEquipment(currentData);
    }
}