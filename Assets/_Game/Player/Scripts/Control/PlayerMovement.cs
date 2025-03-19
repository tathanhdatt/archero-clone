using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : InitializableMono
{
    [SerializeField, Required]
    private Vector2Variable movementDirection;

    [SerializeField, Required]
    private FloatVariable maxSpeed;

    [SerializeField, Required]
    private Transform character;

    [SerializeField, Required]
    private Rigidbody rb;
    
    [SerializeField, Required]
    private BoolVariable canMove;

    [Title("Read Only Variable")]
    [SerializeField, ReadOnly]
    private bool isMoving;

    [SerializeField, ReadOnly]
    private bool isStopped;

    [SerializeField, ReadOnly]
    private bool isChangedHorizontal;

    [SerializeField, ReadOnly]
    private bool isChangedVertical;

    [SerializeField, ReadOnly]
    private Vector3 velocity;

    [Line]
    public UnityEvent onStartMoved;

    public UnityEvent onStop;

    public override async UniTask Initialize()
    {
        this.isMoving = false;
        this.isStopped = true;
        await UniTask.CompletedTask;
    }


    private void Update()
    {
        this.velocity = this.rb.linearVelocity;
        this.isChangedHorizontal = !Mathf.Approximately(
            this.movementDirection.Value.x, Mathf.Epsilon);
        this.isChangedVertical = !Mathf.Approximately(
            this.movementDirection.Value.y, Mathf.Epsilon);
        RaiseEvents();
    }

    private void FixedUpdate()
    {
        if (!this.canMove.Value) return;
        if (this.isChangedHorizontal)
        {
            HandleHorizontalInput();
        }

        if (this.isChangedVertical)
        {
            HandleVerticalInput();
        }
    }

    private void RaiseEvents()
    {
        if (this.isChangedVertical || this.isChangedHorizontal)
        {
            this.isMoving = true;
            if (this.isStopped)
            {
                this.isStopped = false;
                this.onStartMoved?.Invoke();
            }
        }
        else
        {
            this.isStopped = true;
            if (this.isMoving)
            {
                this.isMoving = false;
                this.rb.linearVelocity = Vector3.zero;
                this.onStop?.Invoke();
            }
        }
    }


    private void HandleHorizontalInput()
    {
        float speed = this.movementDirection.Value.x * this.maxSpeed.Value;
        this.rb.linearVelocity = this.rb.linearVelocity.ReplaceX(speed);
    }

    private void HandleVerticalInput()
    {
        float speed = this.movementDirection.Value.y * this.maxSpeed.Value;
        this.rb.linearVelocity = this.rb.linearVelocity.ReplaceZ(speed);
    }

    public override async UniTask Terminate()
    {
        await UniTask.CompletedTask;
    }
}