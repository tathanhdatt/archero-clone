using Dt.Attribute;
using UnityEngine;

public class JoystickValueTracker : MonoBehaviour
{
    [SerializeField, Required]
    private Joystick joystick;
    
    [SerializeField, Required]
    private Vector2Variable joystickValue;

    private void Update()
    {
        this.joystickValue.Value = this.joystick.Direction;
    }
}