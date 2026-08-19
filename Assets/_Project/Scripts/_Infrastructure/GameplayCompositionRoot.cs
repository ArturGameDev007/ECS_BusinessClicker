using System;
using _Project.Scripts._Configs;
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
using UnityEngine.Serialization;

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

        [Header("Other")] 
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

        public EcsManager Compose()
        {
            EcsWorld world = new EcsWorld();
            BusinessFactory businessFactory = new BusinessFactory(world, _barIncomeConfig);
            
            SaveServices saveServices = new SaveServices();
            
            double startBalance = SaveServices(saveServices);
            
            BalanceModel balanceModel = new BalanceModel(startBalance);
            
            BusinessNamePresenter namePresenter = new BusinessNamePresenter(_businessNameView);
            BalancePresenter balancePresenter = new BalancePresenter(_balanceView, balanceModel);
            BusinessInformationPresenter informationPresenter = new BusinessInformationPresenter(world, _businessModelView);
            ButtonLevelUpPresenter buttonLevelUpPresenter = new ButtonLevelUpPresenter(balancePresenter, informationPresenter, _buttonLevelUpView, balanceModel, _levelUpConfig, world);
            UpgradeNamesPresenter upgradeNamesPresenter = new UpgradeNamesPresenter(_upgradeNamesView);
            IncomeUpgradePresenter incomeUpgradePresenter = new IncomeUpgradePresenter(_incomeUpgradeView, _incomeUpgradesConfig);
            PriceLevelUpPresenter levelUpPresenter = new PriceLevelUpPresenter(_priceView, _levelUpConfig);
            PriceUpgradePresenter priceUpgradePresenter = new PriceUpgradePresenter(_priceUpgradesView, _priceUpgradesConfig);
            ButtonUpIncomePresenter upIncomePresenter = new ButtonUpIncomePresenter(world, _buttonUpIncomeView, balanceModel, _priceUpgradesConfig, balancePresenter, priceUpgradePresenter, informationPresenter);
            // BarIncomeModel incomeModel = new BarIncomeModel();
            BarIncomePresenter incomePresenter = new BarIncomePresenter(_barIncomeView);

            return new EcsManager(world, businessFactory, saveServices, _businessNamesConfig, _upgradeNamesConfig, namePresenter, buttonLevelUpPresenter, upgradeNamesPresenter, informationPresenter, levelUpPresenter, incomePresenter, incomeUpgradePresenter, upIncomePresenter, priceUpgradePresenter, balanceModel, balancePresenter);
        }

        private double SaveServices(SaveServices saveServices)
        {
            double minValueBalance = 0f;
            double loadBalance = saveServices.HasSave() ? saveServices.Load().Balance : minValueBalance;
        
            return loadBalance;
        }
    }
}