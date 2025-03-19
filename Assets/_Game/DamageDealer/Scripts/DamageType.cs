using System;

[Flags]
public enum DamageType
{
    Lightning = 1 << 1,
    Poison = 1 << 2,
    Critical = 1 << 3,
    Collide = 1 << 4,
    Ice = 1 << 5,
    Force = 1 << 6,
}