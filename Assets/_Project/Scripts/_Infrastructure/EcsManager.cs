using _Project.Scripts._Configs;
using _Project.Scripts._Services.Save;
using _Project.Scripts.Components;
using _Project.Scripts.Systems;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BarIncome;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using _Project.Scripts.UI.Gameplay.ButtonLVLUp;
using _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome;
using _Project.Scripts.UI.Gameplay.IncomeUpgrades;
using _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades;
using Leopotam.EcsLite;

namespace _Project.Scripts._Infrastructure
{
    public class EcsManager
    {
        private readonly EcsWorld _world;
        private readonly BusinessFactory _businessFactory;
        private readonly SaveServices _saveServices;

        private readonly BusinessNamesConfig _businessNamesConfig;
        private readonly UpgradeNamesConfig _upgradeNamesConfig;

        private readonly BusinessNamePresenter _businessNamePresenter;
        private readonly ButtonLevelUpPresenter _buttonLevelUpPresenter;
        private readonly UpgradeNamesPresenter _upgradeNamesPresenter;
        private readonly BusinessInformationPresenter _businessInformationPresenter;
        private readonly PriceLevelUpPresenter _levelUpPresenter;
        private readonly BarIncomePresenter _barIncomePresenter;
        private readonly IncomeUpgradePresenter _incomeUpgradePresenter;
        private readonly ButtonUpIncomePresenter _buttonUpIncomePresenter;
        private readonly PriceUpgradePresenter _priceUpgradePresenter;
        // private readonly BarIncomeModel _barIncomeModel;

        private readonly BalanceModel _balanceModel;

        private readonly BalancePresenter _balancePresenter;

        private EcsSystems _systems;
        private EcsFilter _businessFilter;
        private EcsPool<BusinessComponents> _businessComponentsPool;

        public EcsManager(EcsWorld world, BusinessFactory businessFactory, SaveServices saveServices,
            BusinessNamesConfig businessNamesConfig,
            UpgradeNamesConfig upgradeNamesConfig,
            BusinessNamePresenter businessNamePresenter, ButtonLevelUpPresenter buttonLevelUpPresenter,
            UpgradeNamesPresenter upgradeNamesPresenter, BusinessInformationPresenter businessInformationPresenter,
            PriceLevelUpPresenter levelUpPresenter, BarIncomePresenter incomePresenter,
            IncomeUpgradePresenter incomeUpgradePresenter, ButtonUpIncomePresenter buttonUpIncomePresenter,
            PriceUpgradePresenter priceUpgradePresenter, BalanceModel balanceModel,
            BalancePresenter balancePresenter)
        {
            _world = world;
            _businessFactory = businessFactory;
            _saveServices = saveServices;
            _businessNamesConfig = businessNamesConfig;
            _upgradeNamesConfig = upgradeNamesConfig;
            _businessNamePresenter = businessNamePresenter;
            _buttonLevelUpPresenter = buttonLevelUpPresenter;
            _upgradeNamesPresenter = upgradeNamesPresenter;
            _businessInformationPresenter = businessInformationPresenter;
            _levelUpPresenter = levelUpPresenter;
            _barIncomePresenter = incomePresenter;
            _incomeUpgradePresenter = incomeUpgradePresenter;
            _buttonUpIncomePresenter = buttonUpIncomePresenter;
            _priceUpgradePresenter = priceUpgradePresenter;
            // _barIncomeModel = barIncomeModel;
            _balanceModel = balanceModel;
            _balancePresenter = balancePresenter;

            _businessFilter = _world.Filter<BusinessComponents>().End();
            _businessComponentsPool = world.GetPool<BusinessComponents>();
        }

        public void Init()
        {
            _systems = new EcsSystems(_world);

            BusinessNames();

            _systems
                .Add(new GenerateIncomeSystem(_barIncomePresenter, _balancePresenter, _balanceModel))
                .Init();

            CreateBusiness();
            RefreshUi();

            _buttonLevelUpPresenter?.Init();
            _buttonUpIncomePresenter?.Init();

            _buttonUpIncomePresenter?.UpdateUpgradesUI();
        }

        public void Tick()
        {
            _systems.Run();
        }

        public void Destroy()
        {
            SaveDataPlayer();

            _world.Destroy();
            _buttonLevelUpPresenter?.Destroy();
            _buttonUpIncomePresenter?.Destroy();
        }

        private void CreateBusiness()
        {
            PlayerSaveData loadedData = _saveServices.HasSave() ? _saveServices.Load() : null;

            _businessFactory.CreateBusiness(index: 0, startLevel: 1, baseIncome: 3d, loadedData);
            _businessFactory.CreateBusiness(index: 1, startLevel: 0, baseIncome: 40d, loadedData);
            _businessFactory.CreateBusiness(index: 2, startLevel: 0, baseIncome: 200d, loadedData);
            _businessFactory.CreateBusiness(index: 3, startLevel: 0, baseIncome: 1000d, loadedData);
            _businessFactory.CreateBusiness(index: 4, startLevel: 0, baseIncome: 5000d, loadedData);
        }

        private void RefreshUi()
        {
            _balancePresenter?.UpdateBalancePlayer();
            _levelUpPresenter?.ShowPriceLevelUp();

            _businessInformationPresenter?.RefreshLevel();
            _businessInformationPresenter?.RefreshIncome();

            _incomeUpgradePresenter?.ShowIncomeUpgrades();

            _priceUpgradePresenter?.ShowPriceFirstUpgrade();
            _priceUpgradePresenter?.ShowPriceSecondUpgrade();
        }

        private void BusinessNames()
        {
            _businessNamePresenter?.ShowBusinessName(_businessNamesConfig.Names);
            _upgradeNamesPresenter?.ShowUpgradeNames(_upgradeNamesConfig.FirstUpgradeName,
                _upgradeNamesConfig.SecondUpgradeName);
        }

        private void SaveDataPlayer()
        {
            PlayerSaveData playerSaveData = new PlayerSaveData(_balanceModel.Amount);

            foreach (var entity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponentsPool.Get(entity);

                BusinessSaveData businessSaveData = new BusinessSaveData
                {
                    ID = business.ID,
                    Level = business.Level,
                    CurrentIncome = business.BaseIncome,
                    FirstUpgradeIncome =  business.FirstUpgradeIncome,
                    SecondUpgradeIncome =  business.SecondUpgradeIncome
                };

                playerSaveData.BusinessSave.Add(businessSaveData);
            }

            _saveServices.Save(playerSaveData);
        }
    }
}