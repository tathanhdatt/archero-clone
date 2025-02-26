using Dt.Attribute;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Required]
    private EnemyAliveSet enemyAliveSet;
    
    private void OnEnable()
    {
        this.enemyAliveSet.Add(this);
    }

    private void OnDisable()
    {
        this.enemyAliveSet.Remove(this);
    }
}