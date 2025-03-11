using System;
using UnityEngine;

public abstract class Gun : InitializableMono
{
    public abstract Bullet BulletPrefab { get; }
    public abstract event Action OnShot;
    public abstract void Shoot();
}