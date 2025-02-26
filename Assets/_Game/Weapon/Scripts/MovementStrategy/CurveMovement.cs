using DG.Tweening;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;
using UnityEngine.Events;

public class CurveMovement : MovementStrategy
{
    [SerializeField, Required]
    private FloatVariable speed;

    [SerializeField, Required]
    private float jumpPower;

    [SerializeField, Required]
    private int numberOfJumps;

    [SerializeField]
    private bool speedBase;

    [SerializeField]
    private Ease ease;

    public UnityEvent onCompleted;

    private Sequence sequence;

    public override void Move(Transform source, Vector3 target)
    {
        this.sequence?.Kill();
        this.sequence = source.DOJump(target, this.jumpPower, this.numberOfJumps,
            this.speed.Value);
        this.sequence.SetSpeedBased(this.speedBase);
        this.sequence.SetEase(this.ease);
        this.sequence.OnComplete(() => this.onCompleted?.Invoke());
    }

    private void OnDisable()
    {
        this.sequence?.Kill();
    }
}