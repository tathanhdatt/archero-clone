using Dt.Attribute;
using UnityEngine;

public class BulletScarSpawner : MonoBehaviour
{
    [SerializeField, Required]
    private GameObject scarPrefab;

    public void Spawn()
    {
        Instantiate(this.scarPrefab, transform.position, Quaternion.identity);
    }
}