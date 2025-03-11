using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInfoView : BaseView
{
    [SerializeField, Required]
    private Image icon;

    [SerializeField, Required]
    private TMP_Text description;

    [SerializeField, Required]
    private Button equipButton;

    [SerializeField, Required]
    private Button exitButton;

    [SerializeField, ReadOnly]
    private EquipmentData data;

    public event Action<EquipmentData> OnClickEquip;

    public override async UniTask Initialize()
    {
        await base.Initialize();
        this.exitButton.onClick.AddListener(OnClickExitHandler);
        this.equipButton.onClick.AddListener(() => OnClickEquip?.Invoke(data));
    }

    private async void OnClickExitHandler()
    {
        await Hide();
    }

    public void SetData(EquipmentData data)
    {
        this.icon.sprite = data.icon;
        this.description.text = data.description;
        this.data = data;
    }
}