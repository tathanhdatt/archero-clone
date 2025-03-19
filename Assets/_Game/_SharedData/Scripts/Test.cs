using Dt.Attribute;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Test prefab;

    [Button]
    private void Spawn()
    {
        Instantiate(this.prefab);
    }
}