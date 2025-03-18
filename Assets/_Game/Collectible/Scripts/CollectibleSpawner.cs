using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 5;

    [SerializeField]
    private Vector2 durationRange;

    [SerializeField]
    private float radius;

    [SerializeField]
    private List<Pool> prewarmPools;

    private readonly Dictionary<CollectibleType, Stack<CollectibleObject>> pools
        = new Dictionary<CollectibleType, Stack<CollectibleObject>>();

    [Serializable]
    private struct Pool
    {
        public CollectibleObject prefab;
        public int amount;
    }

    private void Awake()
    {
        foreach (Pool pool in this.prewarmPools)
        {
            this.pools.TryAdd(pool.prefab.type,
                new Stack<CollectibleObject>(20));
            for (int i = 0; i < pool.amount; i++)
            {
                CollectibleObject collectibleObject = Instantiate(pool.prefab, transform);
                collectibleObject.gameObject.SetActive(false);
                this.pools[pool.prefab.type].Push(collectibleObject);
            }
        }
    }

    public void Spawn(CollectibleTable table, Vector3 position)
    {
        foreach (CollectibleItem item in table.items)
        {
            if (Random.Range(0f, 1f) >= item.rate) continue;
            Spawn(item, position);
        }
    }

    private void Spawn(CollectibleItem item, Vector3 position)
    {
        int number = Random.Range(item.amountRange.min, item.amountRange.max);
        for (int i = 0; i < number; i++)
        {
            CollectibleObject collectible = GetObjectFromPools(item.prefab);
            collectible.transform.position = position;
            collectible.type = item.prefab.type;
            collectible.gameObject.SetActive(true);
            Vector3 target = Random.insideUnitSphere * this.radius;
            target.y = 1;
            target.x += position.x;
            target.z += position.z;
            collectible.value = Random.Range(item.valueRange.min, item.valueRange.max);
            float duration = Random.Range(this.durationRange.x, this.durationRange.y);
            collectible.transform
                .DOJump(target, this.jumpForce, 1, duration)
                .SetEase(Ease.OutBounce);
        }
    }

    private CollectibleObject GetObjectFromPools(CollectibleObject prefab)
    {
        bool containKey = this.pools.ContainsKey(prefab.type);
        if (!containKey)
        {
            CreatePool(prefab);
        }

        if (this.pools[prefab.type].Count <= 0)
        {
            SpawnNewInstance(prefab);
        }

        CollectibleObject newObject = this.pools[prefab.type].Pop();
        newObject.gameObject.SetActive(true);
        return newObject;
    }

    public void Despawn(CollectibleObject collectibleObject)
    {
        bool containKey = this.pools.ContainsKey(collectibleObject.type);
        if (containKey)
        {
            this.pools[collectibleObject.type].Push(collectibleObject);
        }
        else
        {
            this.pools.Add(collectibleObject.type, new Stack<CollectibleObject>(20));
        }

        collectibleObject.transform.SetParent(transform);
        collectibleObject.gameObject.SetActive(false);
    }

    private void CreatePool(CollectibleObject prefab)
    {
        this.pools.Add(prefab.type, new Stack<CollectibleObject>(20));
        SpawnNewInstance(prefab);
    }

    private void SpawnNewInstance(CollectibleObject prefab)
    {
        CollectibleObject collectibleObject = Instantiate(prefab, transform);
        collectibleObject.gameObject.SetActive(false);
        this.pools[prefab.type].Push(collectibleObject);
    }
}