using Dt.Attribute;
using UnityEngine;

public class BulletScarSpawner : MonoBehaviour
{
    [SerializeField, Required]
    private DamageDealer prefab;

    public void Spawn()
    {
        Instantiate(this.prefab, transform.position, Quaternion.identity);
    }
}