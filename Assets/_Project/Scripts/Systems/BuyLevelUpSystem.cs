using _Project.Scripts.Components;
using _Project.Scripts.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Systems
{
    public sealed class BuyLevelUpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly PriceLevelUpConfig _levelUpConfig;

        private EcsWorld _world;

        private EcsFilter _requestFilter;
        private EcsFilter _playerFilter;

        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _playerBalancePool;
        private EcsPool<BuyLevelRequestComponent> _buyLevelRequestPool;

        public BuyLevelUpSystem(PriceLevelUpConfig levelUpConfig)
        {
            _levelUpConfig = levelUpConfig;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _requestFilter = _world.Filter<BuyLevelRequestComponent>().Inc<BusinessEconomyComponent>().End();
            _playerFilter = _world.Filter<PlayerBalanceComponent>().End();

            _economyPool = _world.GetPool<BusinessEconomyComponent>();
            _playerBalancePool = _world.GetPool<PlayerBalanceComponent>();
            _buyLevelRequestPool = _world.GetPool<BuyLevelRequestComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_playerFilter.GetEntitiesCount() <= 0)
                return;

            int playerEntity = _playerFilter.GetRawEntities()[0];
            ref PlayerBalanceComponent balance = ref _playerBalancePool.Get(playerEntity);

            foreach (var entity in _requestFilter)
            {
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);

                double priceLevel = _levelUpConfig.Price[economyComponent.ID];

                if (balance.Amount >= priceLevel)
                {
                    balance.Amount -= priceLevel;
                    economyComponent.Level++;
                }

                _buyLevelRequestPool.Del(entity);
            }
        }
    }
}