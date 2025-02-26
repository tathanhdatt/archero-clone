using DG.Tweening;
using Dt.Attribute;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField, Required]
    private CollectibleTable table;

    [SerializeField]
    private float jumpForce = 5;

    [SerializeField]
    private Vector2 durationRange;

    [SerializeField]
    private float radius;

    [Button]
    public void Spawn()
    {
        foreach (CollectibleItem item in this.table.items)
        {
            if (Random.Range(0f, 1f) >= item.rate) return;
            int number = Random.Range(item.rangeAmount.min, item.rangeAmount.max);
            Spawn(number, item.blueprint.prefab);
        }
    }

    private void Spawn(int number, CollectibleObject prefab)
    {
        for (int i = 0; i < number; i++)
        {
            CollectibleObject collectible = Instantiate(prefab);
            collectible.type = prefab.type;
            collectible.gameObject.SetActive(true);
            Vector3 target = Random.insideUnitSphere * this.radius;
            target.y = 1;
            float duration = Random.Range(this.durationRange.x, this.durationRange.y);
            target.x += transform.position.x;
            target.z += transform.position.z;
            collectible.transform
                .DOJump(target, this.jumpForce, 1, duration)
                .SetEase(Ease.OutBounce);
        }
    }
}