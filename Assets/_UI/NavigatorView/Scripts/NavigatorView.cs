using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;
using UnityEngine.UI;

public class NavigatorView : BaseView
{
    [SerializeField, Required]
    private Button gearButton;

    [SerializeField, Required]
    private Button homeButton;

    [SerializeField, Required]
    private Button gachaButton;

    [SerializeField, Required]
    private Camera uiCamera;

    public event Action OnClickedHome;
    public event Action OnClickedGear;
    public event Action OnClickedGacha;

    public override async UniTask Initialize()
    {
        await base.Initialize();
        this.homeButton.onClick.AddListener(() => { OnClickedHome?.Invoke(); });
        this.gearButton.onClick.AddListener(() => { OnClickedGear?.Invoke(); });
        this.gachaButton.onClick.AddListener(() => { OnClickedGacha?.Invoke(); });
    }

    public override async UniTask Show()
    {
        ActivateUICam();
        await base.Show();
    }

    public override async UniTask Hide()
    {
        await base.Hide();
        DeactivateUICam();
    }

    private void ActivateUICam()
    {
        this.uiCamera.gameObject.SetActive(true);
    }

    private void DeactivateUICam()
    {
        this.uiCamera.gameObject.SetActive(false);
    }
}