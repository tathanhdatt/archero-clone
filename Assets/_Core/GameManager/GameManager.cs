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
        
        [Line]
        [SerializeField]
        private AbilityUpgradeData[] skillUpdaterData;

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
            // this.presenter.GetViewPresenter<GearViewPresenter>().Show();
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