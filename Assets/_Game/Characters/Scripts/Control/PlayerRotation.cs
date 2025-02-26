using Dt.Attribute;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField, Required]
    private Transform player;

    [SerializeField, Required]
    private Joystick joystick;

    [SerializeField]
    private float offset = 90;

    private void Update()
    {
        bool changedHorizontal = !Mathf.Approximately(this.joystick.Horizontal, Mathf.Epsilon);
        bool changedVertical = !Mathf.Approximately(this.joystick.Vertical, Mathf.Epsilon);
        if (changedHorizontal || changedVertical)
        {
            float angle = Mathf.Atan2(-this.joystick.Vertical, this.joystick.Horizontal);
            angle = Mathf.Rad2Deg * angle + this.offset;
            this.player.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}