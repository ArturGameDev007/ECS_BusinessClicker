using System;
using _Project.Scripts._Configs;
using _Project.Scripts._Infrastructure.EcsCore;
using _Project.Scripts._Services.Save;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BarIncome;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using _Project.Scripts.UI.Gameplay.ButtonLVLUp;
using _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome;
using _Project.Scripts.UI.Gameplay.IncomeUpgrades;
using _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts._Infrastructure
{
    [Serializable]
    public class GameplayCompositionRoot
    {
        [Header("Configs")] 
        [SerializeField] private BusinessNamesConfig _businessNamesConfig;
        [SerializeField] private BarIncomeConfig _barIncomeConfig;
        [SerializeField] private UpgradeNamesConfig _upgradeNamesConfig;
        [SerializeField] private PriceLevelUpConfig _levelUpConfig;
        [SerializeField] private IncomeUpgradesConfig _incomeUpgradesConfig;
        [SerializeField] private PriceUpgradesConfig _priceUpgradesConfig;

        [Header("Views")] 
        [SerializeField] private BusinessNameView _businessNameView;
        [SerializeField] private ButtonLevelUpView _buttonLevelUpView;
        [SerializeField] private BarIncomeView _barIncomeView;
        [SerializeField] private UpgradeNamesView _upgradeNamesView;
        [SerializeField] private BalanceView _balanceView;
        [SerializeField] private BusinessModelView _businessModelView;
        [SerializeField] private PriceView _priceView;
        [SerializeField] private IncomeUpgradeView _incomeUpgradeView;
        [SerializeField] private ButtonUpIncomeView _buttonUpIncomeView;
        [SerializeField] private PriceUpgradesView _priceUpgradesView;

        public GameManager Compose()
        {
            EcsWorld world = new EcsWorld();
            BusinessFactory businessFactory = new BusinessFactory(world, _barIncomeConfig);
            SaveServices saveServices = new SaveServices();
            
            double startBalance = SaveServices(saveServices);
            
            BusinessNamePresenter namePresenter = new BusinessNamePresenter(_businessNameView);
            BalancePresenter balancePresenter = new BalancePresenter(world, _balanceView);
            BusinessInformationPresenter informationPresenter = new BusinessInformationPresenter(world, _businessModelView);
            ButtonLevelUpPresenter buttonLevelUpPresenter = new ButtonLevelUpPresenter(balancePresenter, informationPresenter, _buttonLevelUpView, _levelUpConfig, world);
            UpgradeNamesPresenter upgradeNamesPresenter = new UpgradeNamesPresenter(_upgradeNamesView);
            IncomeUpgradePresenter incomeUpgradePresenter = new IncomeUpgradePresenter(_incomeUpgradeView, _incomeUpgradesConfig);
            PriceLevelUpPresenter levelUpPresenter = new PriceLevelUpPresenter(_priceView, _levelUpConfig);
            PriceUpgradePresenter priceUpgradePresenter = new PriceUpgradePresenter(_priceUpgradesView, _priceUpgradesConfig);
            ButtonUpIncomePresenter upIncomePresenter = new ButtonUpIncomePresenter(world, _buttonUpIncomeView, _priceUpgradesConfig, balancePresenter, priceUpgradePresenter, informationPresenter);
            BarIncomePresenter incomePresenter = new BarIncomePresenter(_barIncomeView);

            EcsSystemManager ecsSystemManager = new EcsSystemManager(world, balancePresenter, incomePresenter, startBalance);
            SaveData saveData = new SaveData(world, saveServices);
            
            return new GameManager(world, businessFactory, saveServices, _businessNamesConfig, _upgradeNamesConfig, 
                namePresenter, buttonLevelUpPresenter, upgradeNamesPresenter, informationPresenter, levelUpPresenter, 
                incomeUpgradePresenter, upIncomePresenter, priceUpgradePresenter, balancePresenter, ecsSystemManager, saveData);
        }

        private double SaveServices(SaveServices saveServices)
        {
            double minValueBalance = 0f;
            double loadBalance = saveServices.HasSave() ? saveServices.Load().Balance : minValueBalance;
        
            return loadBalance;
        }
    }
}