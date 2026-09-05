using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Systems
{
    public sealed class BusinessEconomySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _businessFilter;
        private EcsPool<BusinessEconomyComponent> _economyPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _businessFilter = _world.Filter<BusinessEconomyComponent>().End();
            _economyPool = _world.GetPool<BusinessEconomyComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);

                economyComponent.FinalReward = CalculateBusinessIncome(economyComponent);
            }
        }

        private double CalculateBusinessIncome(BusinessEconomyComponent economyComponent)
        {
            double result = economyComponent.FinalReward =
                economyComponent.Level * economyComponent.BaseIncome * (
                    1.0d + economyComponent.FirstUpgradeIncome + economyComponent.SecondUpgradeIncome);

            return result;
        }
    }
}