using _Project.Scripts._Configs;
using _Project.Scripts.Components;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome
{
    public class ButtonUpIncomePresenter
    {
        private readonly EcsWorld _world;
        private readonly ButtonUpIncomeView _buttonUpIncomeView;
        private readonly BalanceModel _balanceModel;
        private readonly PriceUpgradesConfig _priceUpgradesConfig;
        private readonly BalancePresenter _balancePresenter;
        private readonly PriceUpgradePresenter _priceUpgradePresenter;
        private readonly BusinessInformationPresenter _businessInformationPresenter;

        private EcsFilter _businessFilter;
        private EcsPool<BusinessComponents> _businessComponents;

        private bool _isPurchased;

        public ButtonUpIncomePresenter(EcsWorld world, ButtonUpIncomeView buttonUpIncomeView, BalanceModel balanceModel,
            PriceUpgradesConfig priceUpgradesConfig, BalancePresenter balancePresenter,
            PriceUpgradePresenter priceUpgradePresenter,
            BusinessInformationPresenter businessInformationPresenter)
        {
            _world = world;
            _buttonUpIncomeView = buttonUpIncomeView;
            _balanceModel = balanceModel;
            _priceUpgradesConfig = priceUpgradesConfig;
            _balancePresenter = balancePresenter;
            _priceUpgradePresenter = priceUpgradePresenter;
            _businessInformationPresenter = businessInformationPresenter;

            _businessFilter = _world.Filter<BusinessComponents>().End();
            _businessComponents = _world.GetPool<BusinessComponents>();
        }

        public void Init()
        {
            if (_buttonUpIncomeView == null)
                return;

            for (int i = 0; i < _buttonUpIncomeView.ButtonFirstUpgrade.Length; i++)
            {
                int index = i;
                _buttonUpIncomeView.ButtonFirstUpgrade[i].onClick.AddListener(() => OnButtonFirstUpgradeClick(index));
            }

            for (int i = 0; i < _buttonUpIncomeView.ButtonSecondUpgrade.Length; i++)
            {
                int index = i;
                _buttonUpIncomeView.ButtonSecondUpgrade[i].onClick.AddListener(() => OnButtonSecondUpgradeClick(index));
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < _buttonUpIncomeView.ButtonFirstUpgrade.Length; i++)
            {
                if (_buttonUpIncomeView != null)
                {
                    int index = i;
                    _buttonUpIncomeView.ButtonFirstUpgrade[i].onClick
                        .RemoveListener(() => OnButtonFirstUpgradeClick(index));
                }
            }

            for (int i = 0; i < _buttonUpIncomeView.ButtonSecondUpgrade.Length; i++)
            {
                if (_buttonUpIncomeView != null)
                {
                    int index = i;
                    _buttonUpIncomeView.ButtonSecondUpgrade[i].onClick
                        .RemoveListener(() => OnButtonSecondUpgradeClick(index));
                }
            }
        }

        public void UpdateUpgradesUI()
        {
            if (_world == null || _buttonUpIncomeView == null)
                return;

            foreach (var entity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponents.Get(entity);

                if (business.FirstUpgradeIncome > 0)
                {
                    _buttonUpIncomeView.ButtonFirstUpgrade[business.ID].interactable = false;
                    _priceUpgradePresenter?.ShowMessageForFirstUpgrade(business.ID);
                }

                if (business.SecondUpgradeIncome > 0)
                {
                    _buttonUpIncomeView.ButtonSecondUpgrade[business.ID].interactable = false;
                    _priceUpgradePresenter?.ShowMessageForSecondUpgrade(business.ID);
                }
            }
        }

        private void OnButtonFirstUpgradeClick(int index)
        {
            if (_world == null)
                return;

            foreach (var entity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponents.Get(entity);

                if (business.ID == index)
                {
                    double priceUpgrade = _priceUpgradesConfig.PriceFirstUpgrade[index];
                    double upgradePercent = _priceUpgradesConfig.PercentFirstUpgrade[index];

                    if (_balanceModel.Amount >= priceUpgrade)
                    {
                        _isPurchased = true;

                        _balanceModel.Amount -= priceUpgrade;

                        business.FirstUpgradeIncome = upgradePercent;
                        business.BaseIncome *= upgradePercent + 1;

                        _businessInformationPresenter?.RefreshIncome();
                        _balancePresenter?.UpdateBalancePlayer();
                        _priceUpgradePresenter?.ShowMessageForFirstUpgrade(business.ID);

                        _buttonUpIncomeView.ButtonFirstUpgrade[index].interactable = false;
                    }
                    else
                    {
                        _isPurchased = false;
                        Debug.Log("У вас недостаточно средств для улучшения бизнеса.");
                    }
                }
            }
        }

        private void OnButtonSecondUpgradeClick(int index)
        {
            if (_world == null)
                return;

            foreach (var entity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponents.Get(entity);

                if (business.ID == index)
                {
                    double priceUpgrade = _priceUpgradesConfig.PriceSecondUpgrade[index];
                    double upgradePercent = _priceUpgradesConfig.PercentSecondUpgrade[index];

                    if (_balanceModel.Amount >= priceUpgrade)
                    {
                        _isPurchased = true;

                        _balanceModel.Amount -= priceUpgrade;

                        business.SecondUpgradeIncome = upgradePercent;
                        business.BaseIncome *= upgradePercent + 1;

                        _businessInformationPresenter?.RefreshIncome();
                        _balancePresenter?.UpdateBalancePlayer();
                        _priceUpgradePresenter?.ShowMessageForSecondUpgrade(business.ID);

                        _buttonUpIncomeView.ButtonSecondUpgrade[index].interactable = false;
                    }
                    else
                    {
                        Debug.Log("У вас недостаточно средств для улучшения бизнеса.");
                    }
                }
            }
        }
    }
}