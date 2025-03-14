using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class MoveForward : MovementStrategy
{
    [SerializeField]
    private bool useSOVariable;

    [SerializeField, ShowIf(nameof(useSOVariable))]
    private FloatVariable forceVariable;

    [SerializeField, HideIf(nameof(useSOVariable))]
    private float forceValue;

    [SerializeField]
    private float duration;

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
        this.canMove = true;
        float speed = this.useSOVariable ? this.forceVariable.Value : this.forceValue;
        this.rb.AddForce(source.forward * speed);
    }

    private void Update()
    {
        if (!this.canMove) return;
        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime < this.duration) return;
        this.canMove = false;
        this.onMoveEnded?.Invoke();
    }
}