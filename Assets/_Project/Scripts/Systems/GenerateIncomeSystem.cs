using _Project.Scripts.Components;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BarIncome;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public sealed class GenerateIncomeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly BarIncomePresenter _barIncomePresenter;
        private readonly BalancePresenter _balancePresenter;
        private readonly BalanceModel _balanceModel;

        private EcsFilter _playerFilter;
        private EcsFilter _businessFilter;

        private EcsPool<BusinessComponents> _businessComponentsPool;

        public GenerateIncomeSystem(BarIncomePresenter barIncomePresenter, BalancePresenter balancePresenter, BalanceModel balanceModel)
        {
            _barIncomePresenter = barIncomePresenter;
            _balancePresenter = balancePresenter;
            _balanceModel = balanceModel;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _businessFilter = world.Filter<BusinessComponents>().End();
            _businessComponentsPool = world.GetPool<BusinessComponents>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var businessEntity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponentsPool.Get(businessEntity);

                if (business.Level <= 0)
                {
                    _barIncomePresenter?.RefreshBarSlider(business.ID, 0f, business.MaxValueSlider);
                    continue;
                }

                CalculateBusinessIncome(ref business);

                business.CurrentTime += Time.deltaTime;

                float currentTimeProgress = business.CurrentTime;
                float timer = business.IncomeDuration;

                if (currentTimeProgress >= timer)
                {
                    business.CurrentValueSlider += business.CountSliderStep;

                    if (business.CurrentValueSlider >= business.MaxValueSlider)
                    {
                        _balanceModel.Amount += business.FinalReward;
                        
                        business.CurrentValueSlider = 0f;
                        
                        _balancePresenter?.UpdateBalancePlayer();
                    }

                    _barIncomePresenter?.RefreshBarSlider(business.ID, business.CurrentValueSlider, business.MaxValueSlider);
                    
                    business.CurrentTime -= timer;
                }
            }
        }

        private void CalculateBusinessIncome(ref BusinessComponents businessComponents)
        {
            businessComponents.FinalReward =
                businessComponents.Level * businessComponents.BaseIncome * (
                    1.0d + businessComponents.FirstUpgradeIncome + businessComponents.SecondUpgradeIncome);
        }
    }
}