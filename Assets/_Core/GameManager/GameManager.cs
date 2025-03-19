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

        [SerializeField, Required]
        private DialogManager dialogManager;
        
        [SerializeField, Required]
        private Camera uiCamera;

        [SerializeField]
        private AbilityUpgradeData[] abilityUpgradeDatas;

        private Level currentLevel;

        public AbilityUpgradeData[] AbilityUpgradeDatas => this.abilityUpgradeDatas;

        private async void Awake()
        {
            await Initialize();
        }

        private async UniTask Initialize()
        {
            InitUICamera();
            await UniTask.CompletedTask;
            Application.targetFrameRate = 60;
        }

        private void InitUICamera()
        {
            ActivateUICamera();
        }

        private void ActivateUICamera()
        {
            this.uiCamera.gameObject.SetActive(true);
        }

        private void DeactivateUICamera()
        {
            this.uiCamera.gameObject.SetActive(false);
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
            // Messenger.AddListener(Message.LevelWin, LevelWinHandler);
            Messenger.AddListener(Message.ResetUpgradeData, ResetUpgradeDataHandler);
            // await this.presenter.GetViewPresenter<NavigatorViewPresenter>().Show();
            PlayHandler(1);
        }

        private void ResetUpgradeDataHandler()
        {
            foreach (AbilityUpgradeData data in this.abilityUpgradeDatas)
            {
                data.ResetData();
            }
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
            ActivateUICamera();
            await ClearLevel();
            await this.presenter.GetViewPresenter<WinViewPresenter>().Show();
        }

        private async void PlayHandler(int levelId)
        {
            await ClearLevel();
            GameObject newLevel =
                await Addressables.LoadAssetAsync<GameObject>($"Level_{levelId:D3}");
            this.currentLevel = Instantiate(newLevel).GetComponent<Level>();
            DeactivateUICamera();
            await this.currentLevel.Initialize();
            await this.presenter.GetViewPresenter<HomeViewPresenter>().Hide();
            await this.presenter.GetViewPresenter<NavigatorViewPresenter>().Hide();
            await this.currentLevel.Play();
        }

        private async void CombatLevelUpHandler()
        {
            await this.presenter.GetViewPresenter<ScrollSkillViewPresenter>().Show();
        }
    }
}