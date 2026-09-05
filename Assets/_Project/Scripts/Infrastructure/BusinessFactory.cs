using _Project.Scripts.Components;
using _Project.Scripts.Configs;
using _Project.Scripts.Services.Save;
using Leopotam.EcsLite;

namespace _Project.Scripts.Infrastructure
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

        public void CreateBusiness(int index, int startLevel, double baseIncome, PlayerSaveData loadedData)
        {
            int businessEntity = _world.NewEntity();

            EcsPool<BusinessProgressComponent> progressPool = _world.GetPool<BusinessProgressComponent>();
            EcsPool<BusinessEconomyComponent> economyPool = _world.GetPool<BusinessEconomyComponent>();

            ref BusinessProgressComponent progressComponent = ref progressPool.Add(businessEntity);
            ref BusinessEconomyComponent economyComponent = ref economyPool.Add(businessEntity);

            BusinessSaveData savedBusiness = null;

            if (loadedData != null)
            {
                foreach (var business in loadedData.BusinessSave)
                {
                    if (business != null && business.ID == index)
                    {
                        savedBusiness = business;
                    }
                }
            }

            if (savedBusiness != null)
            {
                economyComponent.Level = savedBusiness.Level;
                economyComponent.BaseIncome = savedBusiness.CurrentIncome;
                economyComponent.FirstUpgradeIncome = savedBusiness.FirstUpgradeIncome;
                economyComponent.SecondUpgradeIncome = savedBusiness.SecondUpgradeIncome;
            }
            else
            {
                economyComponent.Level = startLevel;
                economyComponent.BaseIncome = baseIncome;
                economyComponent.FirstUpgradeIncome = 0d;
                economyComponent.SecondUpgradeIncome = 0d;
            }

            economyComponent.ID = index;

            progressComponent.CurrentValueSlider = 0f;
            progressComponent.MaxValueSlider = 100f;
            progressComponent.CountSliderStep = 20f;
            progressComponent.CurrentTime = 0f;

            if (index >= 0 && index < _barIncomeConfig.IncomeDuration.Length)
                progressComponent.IncomeDuration = _barIncomeConfig.IncomeDuration[index];
        }
    }
}