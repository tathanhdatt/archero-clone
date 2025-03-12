using Dt.Attribute;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField, Required]
    private Transform player;

    [SerializeField, Required]
    private Vector2Variable joystickAxis;

    [SerializeField]
    private float offset = 90;

    private void Update()
    {
        bool changedHorizontal = !Mathf.Approximately(this.joystickAxis.Value.x, Mathf.Epsilon);
        bool changedVertical = !Mathf.Approximately(this.joystickAxis.Value.y, Mathf.Epsilon);
        if (changedHorizontal || changedVertical)
        {
            float angle = Mathf.Atan2(-this.joystickAxis.Value.y, this.joystickAxis.Value.x);
            angle = Mathf.Rad2Deg * angle + this.offset;
            this.player.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}