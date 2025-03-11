using UnityEngine;

public class GearViewPresenter : BaseViewPresenter
{
    private GearView gearView;
    private EquipmentInfoView equipmentInfoView;

    public GearViewPresenter(GamePresenter presenter,
        Transform transform) : base(presenter, transform)
    {
    }

    protected override void AddViews()
    {
        this.gearView = AddView<GearView>();
        this.equipmentInfoView = AddView<EquipmentInfoView>(false);
    }

    protected override void OnShow()
    {
        this.gearView.OnClickEquipment += OnClickEquipmentHandler;
        this.equipmentInfoView.OnClickEquip += OnClickEquipHandler;
        base.OnShow();
    }

    protected override void OnHide()
    {
        this.equipmentInfoView.OnClickEquip -= OnClickEquipHandler;
        this.gearView.OnClickEquipment -= OnClickEquipmentHandler;
        base.OnHide();
    }

    private void OnClickEquipmentHandler(EquipmentData data)
    {
        this.equipmentInfoView.SetData(data);
        this.equipmentInfoView.Show();
    }

    private async void OnClickEquipHandler(EquipmentData data)
    {
        this.gearView.SetEquipment(data);
        await this.equipmentInfoView.Hide();
    }
}