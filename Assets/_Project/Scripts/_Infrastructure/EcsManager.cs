using _Project.Scripts._Configs;
using _Project.Scripts._Services.Save;
using _Project.Scripts.Components;
using _Project.Scripts.Systems;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BarIncome;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using _Project.Scripts.UI.Gameplay.ButtonLVLUp;
using _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades;
using Leopotam.EcsLite;

namespace _Project.Scripts._Infrastructure
{
    public class EcsManager
    {
        private readonly EcsWorld _world;
        private readonly BusinessFactory _businessFactory;
        
        private readonly BusinessNamesConfig _businessNamesConfig;
        private readonly UpgradeNamesConfig _upgradeNamesConfig;
        private readonly BarIncomeConfig _barIncomeConfig;
        
        private readonly BusinessNamePresenter _businessNamePresenter;
        private readonly UpgradeNamesPresenter _upgradeNamesPresenter;
        private readonly BusinessInformationPresenter _businessInformationPresenter;
        private readonly PriceLevelUpPresenter _levelUpPresenter;
        private readonly BarIncomePresenter _barIncomePresenter;
        private readonly BarIncomeModel _barIncomeModel;
        private readonly SaveServices _saveServices;
        private readonly BalancePresenter _balancePresenter;
        private readonly BalanceModel _balance;

        private EcsSystems _systems;

        public EcsManager(EcsWorld world, BusinessFactory businessFactory, BusinessNamesConfig businessNamesConfig, UpgradeNamesConfig upgradeNamesConfig,
            BarIncomeConfig incomeConfig, BusinessNamePresenter businessNamePresenter,
            UpgradeNamesPresenter upgradeNamesPresenter, BusinessInformationPresenter businessInformationPresenter,
            PriceLevelUpPresenter levelUpPresenter, BarIncomePresenter incomePresenter, BarIncomeModel barIncomeModel,
            SaveServices saveServices,
            BalancePresenter balancePresenter, BalanceModel balanceModel)
        {
            _world = world;
            _businessFactory = businessFactory;
            _businessNamesConfig = businessNamesConfig;
            _upgradeNamesConfig = upgradeNamesConfig;
            _barIncomeConfig = incomeConfig;
            _businessNamePresenter = businessNamePresenter;
            _upgradeNamesPresenter = upgradeNamesPresenter;
            _businessInformationPresenter = businessInformationPresenter;
            _levelUpPresenter = levelUpPresenter;
            _barIncomePresenter = incomePresenter;
            _barIncomeModel = barIncomeModel;
            _saveServices = saveServices;
            _balancePresenter = balancePresenter;
            _balance = balanceModel;
        }

        public void Init()
        {
            _systems = new EcsSystems(_world);
            
            BusinessNames();

            _systems
                .Add(new GenerateIncomeSystem(_barIncomePresenter, _barIncomeModel, _balance, _balancePresenter))
                .Init();
            
            CreateBusiness();
            
            _balancePresenter?.UpdateBalancePlayer(_balance.Amount);
            
            RefreshUi();
        }

        public void Tick()
        {
            _systems.Run();

            RefreshUi();
        }

        public void Destroy()
        {
            SaveDataPlayer();

            _world.Destroy();
        }

        private void CreateBusiness()
        {
            _businessFactory.CreatePlayer(_balance.Amount);
            
            _businessFactory.CreateBusiness(index: 0, startLevel: 1, baseIncome: 3d);
            _businessFactory.CreateBusiness(index: 1, startLevel: 0, baseIncome: 40d);
            _businessFactory.CreateBusiness(index: 2, startLevel: 0, baseIncome: 200d);
            _businessFactory.CreateBusiness(index: 3, startLevel: 0, baseIncome: 1000d);
            _businessFactory.CreateBusiness(index: 4, startLevel: 0, baseIncome: 5000d);
        }

        private void RefreshUi()
        {
            _businessInformationPresenter?.RefreshData();
            _levelUpPresenter?.ShowPriceLevelUp();
        }

        private void BusinessNames()
        {
            _businessNamePresenter?.ShowBusinessName(_businessNamesConfig.Names);
            _upgradeNamesPresenter?.ShowUpgradeNames(_upgradeNamesConfig.FirstUpgradeName,
                _upgradeNamesConfig.SecondUpgradeName);
        }

        private void SaveDataPlayer()
        {
            PlayerSaveData playerSaveData = new PlayerSaveData(_balance.Amount);
            _saveServices.Save(playerSaveData);
        }
    }
}