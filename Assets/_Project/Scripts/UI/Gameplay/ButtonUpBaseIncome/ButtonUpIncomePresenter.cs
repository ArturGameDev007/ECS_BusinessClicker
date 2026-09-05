using _Project.Scripts.Components;
using _Project.Scripts.Configs;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome
{
    public class ButtonUpIncomePresenter
    {
        private readonly EcsWorld _world;

        private readonly ButtonUpIncomeView _buttonUpIncomeView;

        private readonly PriceUpgradePresenter _priceUpgradePresenter;
        private readonly BusinessInformationPresenter _businessInformationPresenter;

        private EcsFilter _businessFilter;

        private EcsPool<BusinessEconomyComponent> _economyPool;

        public ButtonUpIncomePresenter(EcsWorld world, ButtonUpIncomeView buttonUpIncomeView,
            PriceUpgradePresenter priceUpgradePresenter, BusinessInformationPresenter businessInformationPresenter)
        {
            _world = world;
            _buttonUpIncomeView = buttonUpIncomeView;
            _priceUpgradePresenter = priceUpgradePresenter;
            _businessInformationPresenter = businessInformationPresenter;

            _businessFilter = _world.Filter<BusinessEconomyComponent>().End();

            _economyPool = _world.GetPool<BusinessEconomyComponent>();
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

        private void OnButtonFirstUpgradeClick(int index)
        {
            TryBuyUpgrade(index, isFirstUpgrade: true);
        }

        private void OnButtonSecondUpgradeClick(int index)
        {
            TryBuyUpgrade(index, isFirstUpgrade: false);
        }

        private void TryBuyUpgrade(int index, bool isFirstUpgrade)
        {
            if (_world == null)
                return;

            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
                
                if (economyComponent.ID != index)
                    continue;

                if (isFirstUpgrade)
                    economyComponent.OnBuyFirstUpgradeSuccess?.Invoke(index);
                else
                    economyComponent.OnBuySecondUpgradeSuccess?.Invoke(index);

                bool purchased = isFirstUpgrade
                    ? economyComponent.FirstUpgradeIncome > 0
                    : economyComponent.SecondUpgradeIncome > 0;

                if (purchased)
                {
                    if (isFirstUpgrade)
                    {
                        _priceUpgradePresenter?.ShowMessageForFirstUpgrade(economyComponent.ID);
                        _buttonUpIncomeView.ButtonFirstUpgrade[index].interactable = false;
                    }
                    else
                    {
                        _priceUpgradePresenter?.ShowMessageForSecondUpgrade(economyComponent.ID);
                        _buttonUpIncomeView.ButtonSecondUpgrade[index].interactable = false;
                    }

                    _businessInformationPresenter?.RefreshIncome();
                }
                
                break;
            }
        }
    }
}