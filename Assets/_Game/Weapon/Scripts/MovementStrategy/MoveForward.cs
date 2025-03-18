using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class MoveForward : MovementStrategy
{
    [SerializeField]
    private bool useForceVariable;

    [SerializeField, ShowIf(nameof(useForceVariable))]
    private FloatVariable forceVariable;

    [SerializeField, HideIf(nameof(useForceVariable))]
    private float forceValue;

    [SerializeField]
    private bool useDurationVariable;

    [SerializeField, ShowIf(nameof(useDurationVariable))]
    private FloatVariable durationVariable;

    [SerializeField, HideIf(nameof(useDurationVariable))]
    private float durationValue;

    [SerializeField, ReadOnly]
    private bool canMove;

    [SerializeField, ReadOnly]
    private float elapsedTime;

    [SerializeField, Required]
    private Rigidbody rb;

    public UnityEvent onMoveEnded;

    private void OnEnable()
    {
        this.canMove = false;
    }

    public override void Move(Transform source, Vector3 target)
    {
        this.elapsedTime =
            this.useDurationVariable ? this.durationVariable.Value : this.durationValue;
        this.canMove = true;
        float speed = this.useForceVariable ? this.forceVariable.Value : this.forceValue;
        this.rb.AddForce(source.forward * speed);
    }

    private void Update()
    {
        if (!this.canMove) return;
        this.elapsedTime -= Time.deltaTime;
        if (this.elapsedTime > 0) return;
        this.canMove = false;
        this.onMoveEnded?.Invoke();
    }
}