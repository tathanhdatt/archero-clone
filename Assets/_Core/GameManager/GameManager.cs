using Core.AudioService;
using Core.Service;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, Required]
        private GamePresenter presenter;

        [Line]
        [SerializeField, Required]
        private DialogManager dialogManager;

        [Line]
        [SerializeField]
        private AbilityUpgradeData[] skillUpdaterData;

        private Level currentLevel;

        public AbilityUpgradeData[] SkillUpdaterData => this.skillUpdaterData;
        public IAudioService AudioService { get; private set; }
        public IPoolService PoolService { get; private set; }

        private async void Awake()
        {
            await Initialize();
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
            this.presenter.Initialize(this);
            await this.presenter.InitialViewPresenters();
            await OnEnter();
        }

        private async UniTask OnEnter()
        {
            await UniTask.CompletedTask;
            Messenger.AddListener(Message.CombatLevelUp, CombatLevelUpHandler);
            Messenger.AddListener<int>(Message.Play, PlayHandler);
            Messenger.AddListener(Message.LevelWin, LevelWinHandler);
            await this.presenter.GetViewPresenter<NavigatorViewPresenter>().Show();
        }

        private async UniTask ClearLevel()
        {
            if (this.currentLevel != null)
            {
                await this.currentLevel.Terminate();
                Destroy(this.currentLevel.gameObject);
            }
        }
        private async void LevelWinHandler()
        {
            await ClearLevel();
            await this.presenter.GetViewPresenter<WinViewPresenter>().Show();
        }

        private async void PlayHandler(int levelId)
        {
            await ClearLevel();
            GameObject newLevel =
                await Addressables.LoadAssetAsync<GameObject>($"Level_{levelId:D3}");
            this.currentLevel = Instantiate(newLevel).GetComponent<Level>();
            await this.currentLevel.Initialize();
            await this.presenter.GetViewPresenter<HomeViewPresenter>().Hide();
            await this.presenter.GetViewPresenter<NavigatorViewPresenter>().Hide();
            this.currentLevel.Play();
        }

        private async void CombatLevelUpHandler()
        {
            await this.presenter.GetViewPresenter<ScrollSkillViewPresenter>().Show();
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