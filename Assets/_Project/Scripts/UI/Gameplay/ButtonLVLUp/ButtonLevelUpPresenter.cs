using _Project.Scripts._Configs;
using _Project.Scripts.Components;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BusinessModel;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.ButtonLVLUp
{
    public class ButtonLevelUpPresenter
    {
        private readonly BalancePresenter _balancePresenter;
        private readonly BusinessInformationPresenter _businessInformationPresenter;
        private readonly ButtonLevelUpView _view;
        private readonly BalanceModel _balanceModel;
        private readonly PriceLevelUpConfig _priceLevelUpConfig;
        private readonly EcsWorld _world;

        private EcsFilter _businessFilter;
        private EcsPool<BusinessComponents> _businessComponents;

        public ButtonLevelUpPresenter(BalancePresenter balancePresenter,
            BusinessInformationPresenter informationPresenter, ButtonLevelUpView view, BalanceModel balanceModel,
            PriceLevelUpConfig priceLevelUpConfig, EcsWorld world)
        {
            _balancePresenter = balancePresenter;
            _businessInformationPresenter = informationPresenter;
            _view = view;
            _balanceModel = balanceModel;
            _priceLevelUpConfig = priceLevelUpConfig;

            _world = world;
            _businessFilter = _world.Filter<BusinessComponents>().End();
            _businessComponents = _world.GetPool<BusinessComponents>();
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

        // private void CheckBuyClick(int index)
        // {
        //     if (_world == null || _businessFilter.GetEntitiesCount() <= 0)
        //         return;
        //     
        //     if (index < 0 || index >= _businessFilter.GetEntitiesCount())
        //         return;
        //
        //     if (_businessFilter.GetEntitiesCount() <= 0)
        //         return;
        //
        //     int entityId = _businessFilter.GetRawEntities()[index];
        //
        //     ref BusinessComponents business = ref _businessComponents.Get(entityId);
        //
        //
        //     double priceLevel = _priceLevelUpConfig.Price[index];
        //
        //     if (_balanceModel.Amount >= priceLevel)
        //     {
        //         _balanceModel.Amount -= priceLevel;
        //         business.Level++;
        //
        //         _businessInformationPresenter?.RefreshLevel();
        //         _balancePresenter?.UpdateBalancePlayer();
        //
        //         Debug.Log($"Куплен новый уровень. Списано {priceLevel}");
        //     }
        //     else
        //     {
        //         Debug.Log("У вас недостаточно средст для покупки.");
        //     }
        // }

        private void CheckBuyClick(int index)
        {
            if (_world == null)
                return;
        
            foreach (var entity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponents.Get(entity);
        
                if (business.ID == index)
                {
                    double priceLevel = _priceLevelUpConfig.Price[index];
        
                    if (_balanceModel.Amount >= priceLevel)
                    {
                        _balanceModel.Amount -= priceLevel;
                        business.Level++;
        
                        _businessInformationPresenter?.RefreshLevel();
                        _businessInformationPresenter?.RefreshIncome();
                        _balancePresenter?.UpdateBalancePlayer();
        
                        Debug.Log($"Куплен новый уровень. Списано {priceLevel}");
                    }
                    else
                    {
                        Debug.Log("У вас недостаточно средст для покупки.");
                    }
                }
            }
        }
    }
}