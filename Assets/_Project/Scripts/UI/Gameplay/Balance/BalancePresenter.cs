using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.Balance
{
    public class BalancePresenter
    {
        private readonly EcsWorld _world;
        private readonly BalanceView _balanceView;

        private EcsFilter _balanceFilter;
        
        private EcsPool<PlayerBalanceComponent> _playerBalanceComponent;
        private EcsPool<BalanceChangedEventComponent> _balanceChangedEventComponent;

        public BalancePresenter(EcsWorld world, BalanceView balanceView)
        {
            _world = world;
            _balanceView = balanceView;

            _balanceFilter = _world.Filter<PlayerBalanceComponent>().End();
            
            _playerBalanceComponent = _world.GetPool<PlayerBalanceComponent>();
            _balanceChangedEventComponent = _world.GetPool<BalanceChangedEventComponent>();
        }

        public void UpdateBalance()
        {
            if (_balanceView == null)
                return;
        
            foreach (var entity in _balanceFilter)
            {
                ref PlayerBalanceComponent playerBalanceComponent = ref _playerBalanceComponent.Get(entity);
                
                _balanceView.SetBalanceText(playerBalanceComponent.Amount);
                
                _balanceChangedEventComponent.Del(entity);
            }
        }

        public void ShowStartBalance()
        {
            if (_balanceView == null)
                return;

            foreach (var playerEntity in _balanceFilter)
            {
                ref PlayerBalanceComponent playerBalanceComponent = ref _playerBalanceComponent.Get(playerEntity);

                _balanceView.SetBalanceText(playerBalanceComponent.Amount);

                return;
            }
        }
    }
}