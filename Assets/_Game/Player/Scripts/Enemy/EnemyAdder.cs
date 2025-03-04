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
        enemy.transform.position = new Vector3(Random.Range(-4f, 4f), 1, Random.Range(-4f, 4f));
    }
}