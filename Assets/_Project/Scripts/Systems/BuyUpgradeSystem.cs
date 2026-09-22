using _Project.Scripts.Components;
using _Project.Scripts.Configs;
using Leopotam.EcsLite;

namespace _Project.Scripts.Systems
{
    public sealed class BuyUpgradeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly BusinessConfig _businessConfig;

        private EcsWorld _world;

        private EcsFilter _firstRequestFilter;
        private EcsFilter _secondRequestFilter;
        private EcsFilter _playerFilter;

        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _playerBalancePool;
        private EcsPool<BuyFirstUpgradeRequestComponent> _buyFirstUpgradePool;
        private EcsPool<BuySecondUpgradeRequestComponent> _buySecondUpgradePool;

        private EcsPool<BalanceChangedEventComponent> _balanceChangedEventPool;
        private EcsPool<BusinessIncomeChangedEventComponent> _firstUpgradeChangedEventPool;

        public BuyUpgradeSystem(BusinessConfig businessConfig)
        {
            _businessConfig = businessConfig;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _firstRequestFilter =
                _world.Filter<BusinessEconomyComponent>().Inc<BuyFirstUpgradeRequestComponent>().End();
            _secondRequestFilter =
                _world.Filter<BusinessEconomyComponent>().Inc<BuySecondUpgradeRequestComponent>().End();
            _playerFilter = _world.Filter<PlayerBalanceComponent>().End();

            _economyPool = _world.GetPool<BusinessEconomyComponent>();
            _playerBalancePool = _world.GetPool<PlayerBalanceComponent>();
            _buyFirstUpgradePool = _world.GetPool<BuyFirstUpgradeRequestComponent>();
            _buySecondUpgradePool = _world.GetPool<BuySecondUpgradeRequestComponent>();

            _balanceChangedEventPool = _world.GetPool<BalanceChangedEventComponent>();
            _firstUpgradeChangedEventPool = _world.GetPool<BusinessIncomeChangedEventComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _firstRequestFilter)
            {
                ProcessBuy(entity, true);
                _buyFirstUpgradePool.Del(entity);
            }

            foreach (var entity in _secondRequestFilter)
            {
                ProcessBuy(entity, false);
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

            BusinessBaseInformation business = _businessConfig.GetById(economyComponent.ID);
            UpgradeInformation upgrade = isFirstUpgrade ? business.Upgrade[0] : business.Upgrade[1];

            double priceUpgrade = upgrade.Price;
            double upgradePercent = upgrade.Percent;

            if (balance.Amount < priceUpgrade)
                return;

            balance.Amount -= priceUpgrade;
            economyComponent.BaseIncome *= upgradePercent + 1;

            if (isFirstUpgrade)
                economyComponent.FirstUpgradeIncome = upgradePercent;
            else
                economyComponent.SecondUpgradeIncome = upgradePercent;

            if (!_balanceChangedEventPool.Has(playerEntity))
                _balanceChangedEventPool.Add(playerEntity);

            if (!_firstUpgradeChangedEventPool.Has(entity))
                _firstUpgradeChangedEventPool.Add(entity);
        }
    }
}