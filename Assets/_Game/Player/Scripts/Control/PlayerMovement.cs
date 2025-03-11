using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.Extension;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : InitializableMono
{
    [SerializeField, Required]
    private Joystick joystick;

    [SerializeField, Required]
    private FloatVariable maxSpeed;

    [SerializeField, Required]
    private Transform character;

    [SerializeField, Required]
    private Rigidbody rb;

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
        this.isChangedHorizontal = !Mathf.Approximately(this.joystick.Horizontal, Mathf.Epsilon);
        this.isChangedVertical = !Mathf.Approximately(this.joystick.Vertical, Mathf.Epsilon);
        RaiseEvents();
    }

    private void FixedUpdate()
    {
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
                this.rb.isKinematic = false;
            }
        }
        else
        {
            this.isStopped = true;
            if (this.isMoving)
            {
                this.isMoving = false;
                this.onStop?.Invoke();
                this.rb.isKinematic = true;
            }
        }
    }


    private void HandleHorizontalInput()
    {
        float speed = this.joystick.Horizontal * this.maxSpeed.Value;
        this.rb.linearVelocity = this.rb.linearVelocity.ReplaceX(speed);
    }

    private void HandleVerticalInput()
    {
        float speed = this.joystick.Vertical * this.maxSpeed.Value;
        this.rb.linearVelocity = this.rb.linearVelocity.ReplaceZ(speed);
    }
    
    public override async UniTask Terminate()
    {
        await UniTask.CompletedTask;
    }
}