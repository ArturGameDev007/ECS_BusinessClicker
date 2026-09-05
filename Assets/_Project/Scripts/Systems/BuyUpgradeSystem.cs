using _Project.Scripts.Components;
using _Project.Scripts.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Systems
{
    public sealed class BuyUpgradeSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly PriceUpgradesConfig _priceUpgradesConfig;

        private EcsWorld _world;

        private EcsFilter _businessFilter;
        private EcsFilter _playerFilter;

        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _playerBalancePool;

        public BuyUpgradeSystem(PriceUpgradesConfig priceUpgradesConfig)
        {
            _priceUpgradesConfig = priceUpgradesConfig;
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
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);

                economyComponent.OnBuyFirstUpgradeSuccess += ProcessFirstUpgrade;
                economyComponent.OnBuySecondUpgradeSuccess += ProcessSecondUpgrade;
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);

                economyComponent.OnBuyFirstUpgradeSuccess -= ProcessFirstUpgrade;
                economyComponent.OnBuySecondUpgradeSuccess -= ProcessSecondUpgrade;
            }
        }

        private void ProcessFirstUpgrade(int index)
        {
            ProcessBuy(index, true);
        }
        
        private void ProcessSecondUpgrade(int index)
        {
            ProcessBuy(index, false);
        }

        private void ProcessBuy(int index, bool isFirstUpgrade)
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
                
                double priceUpgrade = isFirstUpgrade
                    ? _priceUpgradesConfig.PriceFirstUpgrade[index]
                    : _priceUpgradesConfig.PriceSecondUpgrade[index];

                double upgradePercent = isFirstUpgrade
                    ? _priceUpgradesConfig.PercentFirstUpgrade[index]
                    : _priceUpgradesConfig.PercentSecondUpgrade[index];

                if (balance.Amount >= priceUpgrade)
                {
                    balance.Amount -= priceUpgrade;
                    economyComponent.BaseIncome *= upgradePercent + 1;
                    
                    balance.OnBalanceChanged?.Invoke(balance.Amount);

                    if (isFirstUpgrade)
                        economyComponent.FirstUpgradeIncome = upgradePercent;
                    else
                        economyComponent.SecondUpgradeIncome = upgradePercent;
                }

                break;
            }
        }
    }
}