using UnityEngine;

public class LevelAutoPlay : MonoBehaviour
{
    [SerializeField]
    private Level level;
    private async void Awake()
    {
        await this.level.Initialize();
        await this.level.Play();
    }
}