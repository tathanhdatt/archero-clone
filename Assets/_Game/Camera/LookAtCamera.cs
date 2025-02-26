using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private Camera cam;

    private void Awake()
    {
        this.cam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.rotation = transform.rotation.Replace(
            x: this.cam.transform.rotation.x, y: 0f, z: 0f, 
            w: this.cam.transform.rotation.w);
    }
}