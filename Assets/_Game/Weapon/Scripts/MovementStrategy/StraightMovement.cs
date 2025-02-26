using DG.Tweening;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;
using UnityEngine.Events;

public class StraightMovement : MovementStrategy
{
    [SerializeField, Required]
    private FloatVariable speed;

    [SerializeField]
    private bool speedBase;

    [SerializeField]
    private Ease ease;

    public UnityEvent onCompleted;

    private Tweener tweener;

    public override void Move(Transform source, Vector3 target)
    {
        this.tweener?.Kill();
        this.tweener = source.DOMove(target, this.speed.Value);
        this.tweener.SetSpeedBased(this.speedBase);
        this.tweener.SetEase(this.ease);
        this.tweener.OnComplete(() => this.onCompleted?.Invoke());
    }

    private void OnDisable()
    {
        this.tweener?.Kill();
    }
}