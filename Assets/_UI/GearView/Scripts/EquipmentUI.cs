using System;
using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField, Required]
    private Image icon;

    [SerializeField, Required]
    private Button selectButton;

    public UnityEvent<EquipmentData> onClickSelect;
    public event Action<EquipmentData> OnClick;

    public void Initialize(EquipmentData data)
    {
        this.icon.sprite = data.icon;
        this.selectButton.onClick.AddListener(() =>
        {
            this.onClickSelect?.Invoke(data);
            OnClick?.Invoke(data);
        });
    }
}