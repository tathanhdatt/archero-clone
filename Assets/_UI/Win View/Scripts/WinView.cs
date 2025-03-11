using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;
using UnityEngine.UI;

public class WinView : BaseView
{
    [SerializeField, Required]
    private Button homeButton;

    public event Action OnClickedWin;

    public override async UniTask Initialize()
    {
        await base.Initialize();
        this.homeButton.onClick.AddListener(() => OnClickedWin?.Invoke());
    }
}