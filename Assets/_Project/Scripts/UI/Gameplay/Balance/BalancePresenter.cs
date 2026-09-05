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

        public void Enable()
        {
            if (_world == null)
                return;

            foreach (var playerEntity in _playerFilter)
            {
                ref PlayerBalanceComponent playerBalanceComponent = ref _playerBalanceComponent.Get(playerEntity);

                playerBalanceComponent.OnBalanceChanged += OnBalanceChange;
                return;
            }
        }

        public void Disable()
        {
            if (_world != null)
            {
                foreach (var playerEntity in _playerFilter)
                {
                    ref PlayerBalanceComponent playerBalanceComponent = ref _playerBalanceComponent.Get(playerEntity);

                    playerBalanceComponent.OnBalanceChanged -= OnBalanceChange;
                    return;
                }
            }
        }

        public void ShowStartBalance()
        {
            if (_balanceView == null)
                return;

            foreach (var playerEntity in _playerFilter)
            {
                ref PlayerBalanceComponent playerBalanceComponent = ref _playerBalanceComponent.Get(playerEntity);

                _balanceView.SetBalanceText(playerBalanceComponent.Amount);

                return;
            }
        }

        private void OnBalanceChange(double balance)
        {
            _balanceView?.SetBalanceText(balance);
        }
    }
}