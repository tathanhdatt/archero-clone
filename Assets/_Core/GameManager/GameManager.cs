using Core.AudioService;
using Core.Service;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;

namespace Core.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Required]
        private GamePresenter presenter;

        [Line]
        [SerializeField, Required]
        private DialogManager dialogManager;

        public IAudioService AudioService { get; private set; }
        public IPoolService PoolService { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        private async UniTask Initialize()
        {
            await UniTask.CompletedTask;
            Application.targetFrameRate = 60;
            InitPoolService();
            InitAudioService();
        }

        private void InitAudioService()
        {
            AudioService = FindAnyObjectByType<NativeAudioService>();
            ServiceLocator.Register(AudioService);
        }

        private void InitPoolService()
        {
            PoolService = FindAnyObjectByType<NativePoolService>();
            ServiceLocator.Register(PoolService);
        }



        private async void Start()
        {
            await OnEnter();
        }

        private async UniTask OnEnter()
        {

        }

        private void ClearLevelHandler()
        {
            ClearLevel();
        }

        private async void PlayLevel(int levelId)
        {

        }

        private void ClearLevel()
        {

        }

#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            SaveData();
        }
#else
        private void OnApplicationPause(bool pauseStatus)
        {
            SaveData();
        }
#endif
        private void SaveData()
        {
            SaveLevels();
            PlayerPrefs.Save();
        }

        private void SaveLevels()
        {

        }
    }
}