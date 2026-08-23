using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.Balance
{
    public class BalancePresenter
    {
        private readonly EcsWorld _world;
        private readonly BalanceView _balanceView;

        private EcsFilter _playerFilter;
        private EcsPool<PlayerBalanceComponent> _playerBalanceComponent;

        public BalancePresenter(EcsWorld world, BalanceView balanceView)
        {
            _world = world;
            _balanceView = balanceView;

            _playerFilter = _world.Filter<PlayerBalanceComponent>().End();
            _playerBalanceComponent = _world.GetPool<PlayerBalanceComponent>();
        }

        public void UpdateBalancePlayer()
        {
            if (_playerFilter.GetEntitiesCount() <= 0)
                return;

            int playerEntity = _playerFilter.GetRawEntities()[0];
            
            ref PlayerBalanceComponent playerBalanceComponent = ref _playerBalanceComponent.Get(playerEntity);
            
            _balanceView.SetBalanceText(playerBalanceComponent.Amount);
        }
    }
}