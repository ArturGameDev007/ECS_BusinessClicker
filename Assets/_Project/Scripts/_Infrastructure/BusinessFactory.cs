using _Project.Scripts._Configs;
using _Project.Scripts._Services.Save;
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

        public void CreateBusiness(int index, int startLevel, double baseIncome, PlayerSaveData loadedData)
        {
            int businessEntity = _world.NewEntity();
            
            EcsPool<BusinessComponents> businessPool = _world.GetPool<BusinessComponents>();
            
            ref BusinessComponents businessComponent = ref businessPool.Add(businessEntity);

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
                businessComponent.Level = savedBusiness.Level;
                businessComponent.BaseIncome = savedBusiness.CurrentIncome;
                businessComponent.FirstUpgradeIncome = savedBusiness.FirstUpgradeIncome;
                businessComponent.SecondUpgradeIncome = savedBusiness.SecondUpgradeIncome;
            }
            else
            {
                businessComponent.Level = startLevel;
                businessComponent.BaseIncome = baseIncome;
                businessComponent.FirstUpgradeIncome = 0d;
                businessComponent.SecondUpgradeIncome = 0d;
            }

            businessComponent.ID = index;

            businessComponent.FinalReward = 0f;

            businessComponent.CurrentValueSlider = 0f;
            businessComponent.MaxValueSlider = 100f;

            businessComponent.CountSliderStep = 20f;

            businessComponent.CurrentTime = 0f;

            if (index >= 0 && index < _barIncomeConfig.IncomeDuration.Length)
                businessComponent.IncomeDuration = _barIncomeConfig.IncomeDuration[index];
        }
    }
}