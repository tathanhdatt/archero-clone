using System;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : BaseView
{
    [SerializeField, Required]
    private Button playButton;
    
    public event Action OnClickedPlay;
    public override async UniTask Initialize()
    {
        await base.Initialize();
        this.playButton.onClick.AddListener(() => OnClickedPlay?.Invoke());
    }
}