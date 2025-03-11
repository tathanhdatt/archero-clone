using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class InitializableMono : MonoBehaviour
{
    public abstract UniTask Initialize();
    public abstract UniTask Terminate();
}