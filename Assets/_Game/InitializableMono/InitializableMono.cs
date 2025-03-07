using UnityEngine;

public abstract class InitializableMono : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Terminate();
}