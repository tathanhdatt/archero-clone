using DG.Tweening;
using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class StraightMovement : MovementStrategy
{
    [SerializeField]
    private bool useSOVariable;

    [SerializeField, ShowIf(nameof(useSOVariable))]
    private FloatVariable speedVariable;

    [SerializeField, HideIf(nameof(useSOVariable))]
    private float speedValue;

    [SerializeField]
    private bool speedBase;

    [SerializeField]
    private Ease ease;

    public UnityEvent onCompleted;

    private Tweener tweener;

    public override void Move(Transform source, Vector3 target)
    {
        this.tweener?.Kill();
        float speed = this.useSOVariable ? this.speedVariable.Value : this.speedValue;
        this.tweener = source.DOMove(target, speed);
        this.tweener.SetSpeedBased(this.speedBase);
        this.tweener.SetEase(this.ease);
        this.tweener.OnComplete(() => this.onCompleted?.Invoke());
    }

    private void OnDisable()
    {
        this.tweener?.Kill();
    }
}