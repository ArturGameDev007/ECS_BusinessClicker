using _Project.Scripts.Components;
using _Project.Scripts.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Systems
{
    public sealed class BuyUpgradeSystem : IEcsInitSystem,IEcsRunSystem
    {
        private readonly PriceUpgradesConfig _priceUpgradesConfig;

        private EcsWorld _world;

        private EcsFilter _firstRequestFilter;
        private EcsFilter _secondRequestFilter;
        private EcsFilter _playerFilter;

        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _playerBalancePool;
        private EcsPool<BuyFirstUpgradeRequestComponent> _buyFirstUpgradePool;
        private EcsPool<BuySecondUpgradeRequestComponent> _buySecondUpgradePool;

        public BuyUpgradeSystem(PriceUpgradesConfig priceUpgradesConfig)
        {
            _priceUpgradesConfig = priceUpgradesConfig;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _firstRequestFilter = _world.Filter<BuyFirstUpgradeRequestComponent>().Inc<BusinessEconomyComponent>().End();
            _secondRequestFilter = _world.Filter<BuySecondUpgradeRequestComponent>().Inc<BusinessEconomyComponent>().End();
            _playerFilter = _world.Filter<PlayerBalanceComponent>().End();

            _economyPool = _world.GetPool<BusinessEconomyComponent>();
            _playerBalancePool = _world.GetPool<PlayerBalanceComponent>();
            _buyFirstUpgradePool = _world.GetPool<BuyFirstUpgradeRequestComponent>();
            _buySecondUpgradePool = _world.GetPool<BuySecondUpgradeRequestComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _firstRequestFilter)
            {
                ProcessBuy(entity,true);
                _buyFirstUpgradePool.Del(entity);
            }
            
            foreach (var entity in _secondRequestFilter)
            {
                ProcessBuy(entity,false);
                _buySecondUpgradePool.Del(entity);
            }
        }

        private void ProcessBuy(int entity, bool isFirstUpgrade)
        {
            if (_playerFilter.GetEntitiesCount() <= 0)
                return;

            int playerEntity = _playerFilter.GetRawEntities()[0];
            ref PlayerBalanceComponent balance = ref _playerBalancePool.Get(playerEntity);
            
            ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
            
            bool purchased = isFirstUpgrade
                ? economyComponent.FirstUpgradeIncome > 0
                : economyComponent.SecondUpgradeIncome > 0;

            if (purchased)
                return;

            int index = economyComponent.ID;
                
            double priceUpgrade = isFirstUpgrade
                ? _priceUpgradesConfig.PriceFirstUpgrade[index]
                : _priceUpgradesConfig.PriceSecondUpgrade[index];

            double upgradePercent = isFirstUpgrade
                ? _priceUpgradesConfig.PercentFirstUpgrade[index]
                : _priceUpgradesConfig.PercentSecondUpgrade[index];

            if (balance.Amount < priceUpgrade)
                return;

            balance.Amount -= priceUpgrade;
            economyComponent.BaseIncome *= upgradePercent + 1;
            
            if (isFirstUpgrade)
                economyComponent.FirstUpgradeIncome = upgradePercent;
            else
                economyComponent.SecondUpgradeIncome = upgradePercent;
        }
    }
}