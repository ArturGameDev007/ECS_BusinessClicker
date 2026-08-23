using _Project.Scripts._Configs;
using _Project.Scripts.Components;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.ButtonLVLUp
{
    public class ButtonLevelUpPresenter
    {
        private readonly BalancePresenter _balancePresenter;
        private readonly BusinessInformationPresenter _businessInformationPresenter;
        private readonly ButtonLevelUpView _view;
        private readonly PriceLevelUpConfig _priceLevelUpConfig;
        private readonly EcsWorld _world;

        private EcsFilter _businessFilter;
        private EcsFilter _balanceFilter;

        private EcsPool<BusinessComponents> _businessComponents;
        private EcsPool<PlayerBalanceComponent> _playerBalanceComponents;

        public ButtonLevelUpPresenter(BalancePresenter balancePresenter,
            BusinessInformationPresenter informationPresenter, ButtonLevelUpView view,
            PriceLevelUpConfig priceLevelUpConfig, EcsWorld world)
        {
            _balancePresenter = balancePresenter;
            _businessInformationPresenter = informationPresenter;
            _view = view;
            _priceLevelUpConfig = priceLevelUpConfig;

            _world = world;
            
            _businessFilter = _world.Filter<BusinessComponents>().End();
            _businessComponents = _world.GetPool<BusinessComponents>();
            
            _balanceFilter = _world.Filter<PlayerBalanceComponent>().End();
            _playerBalanceComponents = _world.GetPool<PlayerBalanceComponent>();
        }

        public void Init()
        {
            if (_view == null)
                return;

            for (int i = 0; i < _view.ButtonBuy.Length; i++)
            {
                if (_view.ButtonBuy != null)
                {
                    int index = i;
                    _view.ButtonBuy[i].onClick.AddListener(() => OnButtonClickBuyLevelUp(index));
                }
            }
        }

        public void Destroy()
        {
            if (_view == null)
                return;

            for (int i = 0; i < _view.ButtonBuy.Length; i++)
            {
                if (_view.ButtonBuy != null)
                {
                    int index = i;
                    _view.ButtonBuy[i].onClick.RemoveListener(() => OnButtonClickBuyLevelUp(index));
                }
            }
        }

        private void OnButtonClickBuyLevelUp(int index)
        {
            CheckBuyClick(index);
        }

        private void CheckBuyClick(int index)
        {
            if (_world == null)
                return;
            
            if (_balanceFilter.GetEntitiesCount() <= 0)
                return;
            
            int playerEntity = _balanceFilter.GetRawEntities()[0];
            ref PlayerBalanceComponent balance = ref _playerBalanceComponents.Get(playerEntity);

            foreach (var entity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponents.Get(entity);

                if (business.ID == index)
                {
                    double priceLevel = _priceLevelUpConfig.Price[index];
                    
                    if (balance.Amount >= priceLevel)
                    {
                        balance.Amount -= priceLevel;
                        business.Level++;

                        _businessInformationPresenter?.RefreshLevel();
                        _businessInformationPresenter?.RefreshIncome();
                        _balancePresenter?.UpdateBalancePlayer();
                    }
                }
            }
        }
    }
}