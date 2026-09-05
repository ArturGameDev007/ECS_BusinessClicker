using _Project.Scripts.Components;
using _Project.Scripts.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Systems
{
    public sealed class BuyLevelUpSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly PriceLevelUpConfig _levelUpConfig;

        private EcsWorld _world;

        private EcsFilter _businessFilter;
        private EcsFilter _playerFilter;

        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _playerBalancePool;
        
        public BuyLevelUpSystem(PriceLevelUpConfig levelUpConfig)
        {
            _levelUpConfig = levelUpConfig;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _businessFilter = _world.Filter<BusinessEconomyComponent>().End();
            _playerFilter = _world.Filter<PlayerBalanceComponent>().End();

            _economyPool = _world.GetPool<BusinessEconomyComponent>();
            _playerBalancePool = _world.GetPool<PlayerBalanceComponent>();
            
            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent  economyComponent = ref _economyPool.Get(entity);

                economyComponent.OnBuyLevelSuccess += ProcessBuy;
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent  economyComponent = ref _economyPool.Get(entity);

                economyComponent.OnBuyLevelSuccess -= ProcessBuy;
            }
        }
        
        private void ProcessBuy(int index)
        {
            if (_playerFilter.GetEntitiesCount() <= 0)
                return;

            int playerEntity = _playerFilter.GetRawEntities()[0];
            ref PlayerBalanceComponent balance = ref _playerBalancePool.Get(playerEntity);

            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
                
                if (economyComponent.ID != index)
                    continue;
                
                double priceLevel = _levelUpConfig.Price[index];

                if (balance.Amount >= priceLevel)
                {
                    balance.Amount -= priceLevel;
                    economyComponent.Level++;
                    
                    balance.OnBalanceChanged?.Invoke(balance.Amount);
                }
            }
        }
    }
}