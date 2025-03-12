using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigatorView : BaseView, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField, Required]
    private Button gearButton;

    [SerializeField, Required]
    private Button homeButton;

    [SerializeField, Required]
    private Button gachaButton;

    [SerializeField, Required]
    private RectTransform viewHolder;

    [SerializeField, Required]
    private int defaultViewIndex;

    [SerializeField, ReadOnly]
    private float widthPerView;

    [SerializeField, ReadOnly]
    private float minPositionX;

    [SerializeField, ReadOnly]
    private float maxPositionX;

    private Tweener snapTweener;

    public event Action OnClickedHome;
    public event Action OnClickedGear;
    public event Action OnClickedGacha;

    public override async UniTask Initialize()
    {
        await base.Initialize();
        this.homeButton.onClick.AddListener(() => { OnClickedHome?.Invoke(); });
        this.gearButton.onClick.AddListener(() => { OnClickedGear?.Invoke(); });
        this.gachaButton.onClick.AddListener(() => { OnClickedGacha?.Invoke(); });
        CalculateWidthPerView();
        CalculateMinLocalPositionX();
    }

    private void CalculateWidthPerView()
    {
        this.widthPerView = this.viewHolder.rect.width / this.viewHolder.childCount;
    }

    private void CalculateMinLocalPositionX()
    {
        float minLocalPositionX = -this.widthPerView * (this.viewHolder.childCount - 1);
        this.minPositionX = this.viewHolder.TransformPoint(minLocalPositionX, 0, 0).x;
        this.maxPositionX = this.viewHolder.TransformPoint(Vector3.zero).x;
    }

    public override async UniTask Show()
    {
        ResetViewHolderPosition();
        await base.Show();
    }

    private void ResetViewHolderPosition()
    {
        Vector2 position = this.viewHolder.anchoredPosition;
        position = position.ReplaceX(-this.widthPerView * this.defaultViewIndex);
        this.viewHolder.anchoredPosition = position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.snapTweener?.Kill();
    }

    public void OnDrag(PointerEventData eventData)
    {
        CalculateViewHolderPosition(eventData.delta);
    }

    private void CalculateViewHolderPosition(Vector2 delta)
    {
        Vector3 position = this.viewHolder.position;
        position += new Vector3(delta.x, 0, 0);
        position.x = Mathf.Clamp(position.x, this.minPositionX, this.maxPositionX);
        this.viewHolder.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float x = this.viewHolder.anchoredPosition.x;
        x = SnapToClosest(x, this.widthPerView);
        this.snapTweener = this.viewHolder.DOAnchorPosX(x, 0.2f);
    }

    private float SnapToClosest(float value, float snapValue)
    {
        float redundantValue = value % snapValue;
        bool isClosePrev = redundantValue < -snapValue / 2;
        if (isClosePrev)
        {
            return value - snapValue - redundantValue;
        }

        bool isCloseNext = redundantValue > snapValue / 2;
        if (isCloseNext)
        {
            return value + snapValue - redundantValue;
        }

        return value - redundantValue;
    }

    public void SnapToView(int index)
    {
        this.snapTweener?.Kill();
        this.snapTweener = this.viewHolder.DOAnchorPosX(-index * this.widthPerView, 0.2f);
    }
}