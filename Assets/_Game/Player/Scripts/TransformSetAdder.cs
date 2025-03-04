using Dt.Attribute;
using UnityEngine;

public class TransformSetAdder : MonoBehaviour
{
    [SerializeField, Required]
    private TransformSet transformSet;

    private void OnEnable()
    {
        Add();
    }

    public void Add()
    {
        this.transformSet.Add(transform);
    }

    private void OnDisable()
    {
        Remove();
    }

    public void Remove()
    {
        this.transformSet.Remove(transform);
    }
}