using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Required]
    private Joystick joystick;

    [SerializeField, Required]
    private FloatVariable maxSpeed;

    [SerializeField, Required]
    private Transform character;

    [SerializeField, ReadOnly]
    private bool isMoving;

    [SerializeField, ReadOnly]
    private bool isStopped;

    [SerializeField, ReadOnly]
    private bool isChangedHorizontal;

    [SerializeField, ReadOnly]
    private bool isChangedVertical;

    public UnityEvent onStartMoved;
    public UnityEvent onStop;

    private void Awake()
    {
        this.isMoving = false;
        this.isStopped = true;
    }

    private void Update()
    {
        this.isChangedHorizontal = !Mathf.Approximately(this.joystick.Horizontal, Mathf.Epsilon);
        this.isChangedVertical = !Mathf.Approximately(this.joystick.Vertical, Mathf.Epsilon);
        if (this.isChangedHorizontal)
        {
            HandleHorizontalInput();
        }

        if (this.isChangedVertical)
        {
            HandleVerticalInput();
        }

        RaiseEvents();
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
                this.onStop?.Invoke();
            }
        }
    }

    private void HandleHorizontalInput()
    {
        float speed = this.joystick.Vertical * this.maxSpeed.Value * Time.deltaTime;
        this.character.position += Vector3.forward * speed;
    }

    private void HandleVerticalInput()
    {
        float speed = this.joystick.Horizontal * this.maxSpeed.Value * Time.deltaTime;
        this.character.position += Vector3.right * speed;
    }
}