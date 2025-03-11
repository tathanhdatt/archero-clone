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
}