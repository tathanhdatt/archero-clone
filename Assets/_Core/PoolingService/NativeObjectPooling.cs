using System;
using System.Collections.Generic;
using UnityEngine;

public class NativeObjectPooling : MonoBehaviour
{
    private const string DespawnExceptionMessage = "The go cannot be null.";

    private static readonly Dictionary<MonoBehaviour, Stack<MonoBehaviour>> Pools =
        new Dictionary<MonoBehaviour, Stack<MonoBehaviour>>();

    private static Transform poolTransform;

    [SerializeField]
    private List<Pool> prewarmPools;

    [Serializable]
    public struct Pool
    {
        public MonoBehaviour prefab;
        public int amount;
    }

    private void Awake()
    {
        poolTransform = transform;
        foreach (Pool pool in this.prewarmPools)
        {
            WarmUp(pool.prefab, pool.amount);
        }
    }

    public static T Spawn<T>(T prefab, Transform parent = null) where T : MonoBehaviour
    {
        if (prefab == null)
        {
            throw new ArgumentNullException(nameof(prefab));
        }

        bool hasPool = Pools.ContainsKey(prefab);
        if (!hasPool)
        {
            WarmUp(prefab);
        }

        T go = Pools[prefab].Pop() as T;
        go?.transform.SetParent(parent);
        return go;
    }


    public static void Despawn<T>(T prefabKey, T instance) where T : MonoBehaviour
    {
        if (instance == null)
        {
            throw new ArgumentNullException(DespawnExceptionMessage);
        }

        bool hasPool = Pools.ContainsKey(prefabKey);
        if (!hasPool)
        {
            WarmUp(prefabKey);
        }

        PushIntoPool(prefabKey, instance);
        instance.gameObject.SetActive(false);
    }

    private static void WarmUp<T>(T prefab, int amount = 1) where T : MonoBehaviour
    {
        Stack<MonoBehaviour> newPool = new Stack<MonoBehaviour>(10);
        Pools.Add(prefab, newPool);
        for (int i = 0; i < amount; i++)
        {
            T newInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            Despawn(prefab, newInstance);
        }
    }

    private static void PushIntoPool<T>(T prefab, T instance) where T : MonoBehaviour
    {
        Pools[prefab].Push(instance);
        instance.transform.SetParent(poolTransform);
    }
}