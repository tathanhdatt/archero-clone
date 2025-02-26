using Dt.Attribute;
using UnityEngine;

public class TransformSetAdder : MonoBehaviour
{
    [SerializeField, Required]
    private TransformSet transformSet;

    private void OnEnable()
    {
        this.transformSet.Add(transform);
    }

    private void OnDisable()
    {
        this.transformSet.Remove(transform);
    }
}