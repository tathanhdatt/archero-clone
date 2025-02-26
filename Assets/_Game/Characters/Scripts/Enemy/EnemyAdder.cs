using Dt.Attribute;
using Dt.Extension;
using UnityEngine;

public class EnemyAdder : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;


    [Button]
    private void Add()
    {
        GameObject enemy = Instantiate(this.prefab, transform);
        enemy.transform.position =
            Vector3.up.Assign(x: Random.Range(-4f, 4f), z: Random.Range(-4f, 4f));
    }
}