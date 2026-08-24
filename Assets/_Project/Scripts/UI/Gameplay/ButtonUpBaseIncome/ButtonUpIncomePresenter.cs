using _Project.Scripts._Configs;
using _Project.Scripts.Components;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome
{
    public class ButtonUpIncomePresenter
    {
        private readonly EcsWorld _world;
        private readonly ButtonUpIncomeView _buttonUpIncomeView;
        private readonly PriceUpgradesConfig _priceUpgradesConfig;
        private readonly BalancePresenter _balancePresenter;
        private readonly PriceUpgradePresenter _priceUpgradePresenter;
        private readonly BusinessInformationPresenter _businessInformationPresenter;

        private EcsFilter _businessFilter;
        private EcsFilter _balanceFilter;

        private EcsPool<BusinessIdComponent> _idPool;
        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _playerBalanceComponents;

        private bool _isPurchased;

        public ButtonUpIncomePresenter(EcsWorld world, ButtonUpIncomeView buttonUpIncomeView,
            PriceUpgradesConfig priceUpgradesConfig, BalancePresenter balancePresenter,
            PriceUpgradePresenter priceUpgradePresenter,
            BusinessInformationPresenter businessInformationPresenter)
        {
            _world = world;
            _buttonUpIncomeView = buttonUpIncomeView;
            _priceUpgradesConfig = priceUpgradesConfig;
            _balancePresenter = balancePresenter;
            _priceUpgradePresenter = priceUpgradePresenter;
            _businessInformationPresenter = businessInformationPresenter;

            _businessFilter = _world.Filter<BusinessIdComponent>().Inc<BusinessEconomyComponent>().End();

            _idPool = _world.GetPool<BusinessIdComponent>();
            _economyPool = _world.GetPool<BusinessEconomyComponent>();

            _balanceFilter = _world.Filter<PlayerBalanceComponent>().End();
            _playerBalanceComponents = _world.GetPool<PlayerBalanceComponent>();
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
                ref BusinessIdComponent idComponent = ref _idPool.Get(entity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);

                if (economyComponent.FirstUpgradeIncome > 0)
                {
                    _buttonUpIncomeView.ButtonFirstUpgrade[idComponent.ID].interactable = false;
                    _priceUpgradePresenter?.ShowMessageForFirstUpgrade(idComponent.ID);
                }

                if (economyComponent.SecondUpgradeIncome > 0)
                {
                    _buttonUpIncomeView.ButtonSecondUpgrade[idComponent.ID].interactable = false;
                    _priceUpgradePresenter?.ShowMessageForSecondUpgrade(idComponent.ID);
                }
            }
        }

        private void OnButtonFirstUpgradeClick(int index)
        {
            if (_world == null)
                return;
            
            if (_balanceFilter.GetEntitiesCount() <= 0)
                return;
            
            int playerEntity = _balanceFilter.GetRawEntities()[0];
            ref PlayerBalanceComponent balance = ref _playerBalanceComponents.Get(playerEntity);
            
            foreach (var entity in _businessFilter)
            {
                ref BusinessIdComponent idComponent = ref _idPool.Get(entity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
                
                if (idComponent.ID == index)
                {
                    double priceUpgrade = _priceUpgradesConfig.PriceFirstUpgrade[index];
                    double upgradePercent = _priceUpgradesConfig.PercentFirstUpgrade[index];
            
                    if (balance.Amount >= priceUpgrade)
                        BuyIncomeUpgrades(ref idComponent, ref economyComponent, ref balance, priceUpgrade, upgradePercent, index, isFirstUpgrade: true);
                    else
                        _isPurchased = false;
                }
            }
        }

        private void OnButtonSecondUpgradeClick(int index)
        {
            if (_world == null)
                return;
            
            if (_balanceFilter.GetEntitiesCount() <= 0)
                return;
            
            int playerEntity = _balanceFilter.GetRawEntities()[0];
            ref PlayerBalanceComponent balance = ref _playerBalanceComponents.Get(playerEntity);
            
            foreach (var entity in _businessFilter)
            {
                ref BusinessIdComponent idComponent = ref _idPool.Get(entity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
            
                if (idComponent.ID == index)
                {
                    double priceUpgrade = _priceUpgradesConfig.PriceSecondUpgrade[index];
                    double upgradePercent = _priceUpgradesConfig.PercentSecondUpgrade[index];
            
                    if (balance.Amount >= priceUpgrade)
                        BuyIncomeUpgrades(ref idComponent, ref economyComponent, ref balance, priceUpgrade, upgradePercent, index, isFirstUpgrade: false);
                    else
                        _isPurchased = false;
                }
            }
        }
        
        private void BuyIncomeUpgrades(ref BusinessIdComponent idComponent, ref BusinessEconomyComponent economyComponent, ref PlayerBalanceComponent balance, double priceUpgrade, double upgradePercent, int index, bool isFirstUpgrade)
        {
            if (_isPurchased)
                return;

            balance.Amount -= priceUpgrade;
            economyComponent.BaseIncome *= upgradePercent + 1;

            if (isFirstUpgrade)
            {
                economyComponent.FirstUpgradeIncome = upgradePercent;
                _priceUpgradePresenter?.ShowMessageForFirstUpgrade(idComponent.ID);
                _buttonUpIncomeView.ButtonFirstUpgrade[index].interactable = false;
            }
            else
            {
                economyComponent.SecondUpgradeIncome = upgradePercent;
                _priceUpgradePresenter?.ShowMessageForSecondUpgrade(idComponent.ID);
                _buttonUpIncomeView.ButtonSecondUpgrade[index].interactable = false;
            }

            _businessInformationPresenter?.RefreshIncome();
            _balancePresenter?.UpdateBalancePlayer();
        }
    }
}