using System;
using _Project.Scripts._Configs;
using _Project.Scripts._Services.Save;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BarIncome;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using _Project.Scripts.UI.Gameplay.ButtonLVLUp;
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
        [SerializeField] private BusinessInformationConfig _businessInformationConfig;
        [SerializeField] private PriceLevelUpConfig _levelUpConfig;

        [Header("Other")] 
        [SerializeField] private BusinessNameView _businessNameView;
        [SerializeField] private BarIncomeView _barIncomeView;
        [SerializeField] private UpgradeNamesView _upgradeNamesView;
        [SerializeField] private BalanceView _balanceView;
        [SerializeField] private BusinessModelView _businessModelView;
        [SerializeField] private PriceView _priceView;

        public EcsManager Compose()
        {
            EcsWorld world = new EcsWorld();
            BusinessFactory businessFactory = new BusinessFactory(world, _barIncomeConfig);
            
            SaveServices saveServices = new SaveServices();
            var startBalance = SaveServices(saveServices);
            
            BusinessNamePresenter namePresenter = new BusinessNamePresenter(_businessNameView);
            UpgradeNamesPresenter upgradeNamesPresenter = new UpgradeNamesPresenter(_upgradeNamesView);
            BalanceModel balanceModel = new BalanceModel(startBalance);
            BalancePresenter balancePresenter = new BalancePresenter(_balanceView, balanceModel);
            BusinessInformationPresenter informationPresenter = new BusinessInformationPresenter(_businessModelView, _businessInformationConfig);
            PriceLevelUpPresenter levelUpPresenter = new PriceLevelUpPresenter(_priceView, _levelUpConfig);
            BarIncomeModel incomeModel = new BarIncomeModel();
            BarIncomePresenter incomePresenter = new BarIncomePresenter(_barIncomeView);
            // GenerationIncome generationIncome = new GenerationIncome(_barIncomeConfig, balanceModel, balancePresenter, incomePresenter, incomeModel);

            return new EcsManager(world, businessFactory, _businessNamesConfig, _upgradeNamesConfig, _barIncomeConfig, namePresenter, upgradeNamesPresenter, informationPresenter, levelUpPresenter, incomePresenter, incomeModel, saveServices,
                balancePresenter, balanceModel);
        }

        private float SaveServices(SaveServices saveServices)
        {
            float minValueBalance = 0f;

            var loadBalance = saveServices.HasSave() ? saveServices.Load().Balance : minValueBalance;

            return loadBalance;
        }
    }
}