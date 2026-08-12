using _Project.Scripts._Configs;
using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts._Infrastructure
{
    public class BusinessFactory
    {
        private readonly EcsWorld _world;
        private readonly BarIncomeConfig _barIncomeConfig;

        public BusinessFactory(EcsWorld world, BarIncomeConfig barIncomeConfig)
        {
            _world = world;
            _barIncomeConfig = barIncomeConfig;
        }

        public void CreatePlayer(float startBalance)
        {
            int playerEntity = _world.NewEntity();

            EcsPool<PlayerBalanceComponent> playerPool = _world.GetPool<PlayerBalanceComponent>();

            ref PlayerBalanceComponent playerComp = ref playerPool.Add(playerEntity);

            playerComp.Amount = startBalance;
        }

        public void CreateBusiness(int index, int startLevel, double baseIncome)
        {
            int businessEntity = _world.NewEntity();
            EcsPool<BusinessComponents> businessPool = _world.GetPool<BusinessComponents>();
            ref BusinessComponents businessComponent = ref businessPool.Add(businessEntity);

            businessComponent.ID = index;

            businessComponent.FinalReward = 0f;

            businessComponent.CurrentValueSlider = 0f;
            businessComponent.MaxValueSlider = 100f;

            businessComponent.Level = startLevel;
            businessComponent.BaseIncome = baseIncome;
            businessComponent.CountSliderStep = 5f;

            businessComponent.CurrentTime = 0f;

            if (index >= 0 && index < _barIncomeConfig.IncomeDuration.Length)
                businessComponent.IncomeDuration = _barIncomeConfig.IncomeDuration[index];
            
            businessComponent.FirstUpgradeIncome = 0f;
            businessComponent.SecondUpgradeIncome = 0f;
        }
    }
}