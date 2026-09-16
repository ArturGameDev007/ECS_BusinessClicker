using _Project.Scripts.Components;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome
{
    public class ButtonUpIncomePresenter
    {
        private readonly EcsWorld _world;
        private readonly ButtonUpIncomeView _buttonUpIncomeView;
        private readonly BusinessInformationPresenter _businessInformationPresenter;
        private readonly PriceUpgradePresenter _priceUpgradePresenter;

        private EcsFilter _businessFilter;

        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<BuyFirstUpgradeRequestComponent> _firstRequestPool;
        private EcsPool<BuySecondUpgradeRequestComponent> _secondRequestPool;

        private EcsPool<BusinessIncomeChangedEventComponent> _firstUpgradeChangedEventPool;

        public ButtonUpIncomePresenter(EcsWorld world, ButtonUpIncomeView buttonUpIncomeView,
            BusinessInformationPresenter businessInformationPresenter,
            PriceUpgradePresenter priceUpgradePresenter)
        {
            _world = world;
            _buttonUpIncomeView = buttonUpIncomeView;
            _businessInformationPresenter = businessInformationPresenter;
            _priceUpgradePresenter = priceUpgradePresenter;

            _businessFilter = _world.Filter<BusinessEconomyComponent>().End();

            _economyPool = _world.GetPool<BusinessEconomyComponent>();
            _firstRequestPool = _world.GetPool<BuyFirstUpgradeRequestComponent>();
            _secondRequestPool = _world.GetPool<BuySecondUpgradeRequestComponent>();

            _firstUpgradeChangedEventPool = _world.GetPool<BusinessIncomeChangedEventComponent>();
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
                if (_buttonUpIncomeView.ButtonFirstUpgrade[i] != null)
                {
                    int index = i;
                    _buttonUpIncomeView.ButtonFirstUpgrade[i].onClick
                        .RemoveListener(() => OnButtonFirstUpgradeClick(index));
                }
            }

            for (int i = 0; i < _buttonUpIncomeView.ButtonSecondUpgrade.Length; i++)
            {
                if (_buttonUpIncomeView.ButtonSecondUpgrade[i] != null)
                {
                    int index = i;
                    _buttonUpIncomeView.ButtonSecondUpgrade[i].onClick
                        .RemoveListener(() => OnButtonSecondUpgradeClick(index));
                }
            }
        }

        public void UpdateIncome()
        {
            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
                
                _businessInformationPresenter?.RefreshIncome(economyComponent.ID, economyComponent.BaseIncome);
                
                _firstUpgradeChangedEventPool.Del(entity);
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
                {
                    if (!_firstRequestPool.Has(entity))
                    {
                        _firstRequestPool.Add(entity);
                    }
                }
                else
                {
                    if (!_secondRequestPool.Has(entity))
                    {
                        _secondRequestPool.Add(entity);
                    }
                }

                break;
            }
        }

        public void StateButton()
        {
            if (_buttonUpIncomeView == null)
                return;
        
            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
                int index = economyComponent.ID;
        
                if (economyComponent.FirstUpgradeIncome > 0)
                {
                    _priceUpgradePresenter?.ShowMessageForFirstUpgrade(index);
                    _buttonUpIncomeView.ButtonFirstUpgrade[index].interactable = false;
                }
        
                if (economyComponent.SecondUpgradeIncome > 0)
                {
                    _priceUpgradePresenter?.ShowMessageForSecondUpgrade(index);
                    _buttonUpIncomeView.ButtonSecondUpgrade[index].interactable = false;
                }
            }
        }
    }
}