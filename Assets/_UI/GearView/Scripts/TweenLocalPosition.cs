using DG.Tweening;
using Dt.Attribute;
using UnityEngine;

public class TweenLocalPosition : MonoBehaviour
{
    [SerializeField, Required]
    private Transform transformStart;

    [SerializeField, Required]
    private Transform transformEnd;

    [SerializeField]
    private float duration;

    [SerializeField]
    private Ease ease;

    [SerializeField, ReadOnly]
    private Vector3 startPosition;

    [SerializeField, ReadOnly]
    private Vector3 endPosition;

    private Tweener tweener;

    public void Initialize()
    {
        this.startPosition = this.transformStart.localPosition;
        this.endPosition = this.transformEnd.localPosition;
        ResetTween();
    }

    public void Play()
    {
        this.tweener?.Kill();
        this.tweener = transform.DOLocalMove(this.endPosition, this.duration);
        this.tweener.SetEase(this.ease);
    }

    public void ResetTween()
    {
        this.tweener?.Kill();
        transform.localPosition = this.startPosition;
    }
}