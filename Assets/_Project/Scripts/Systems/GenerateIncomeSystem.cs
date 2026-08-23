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

        private EcsFilter _playerFilter;
        private EcsFilter _businessFilter;

        private EcsPool<BusinessComponents> _businessComponentsPool;
        private EcsPool<PlayerBalanceComponent> _balanceComponentsPool;

        public GenerateIncomeSystem(BarIncomePresenter barIncomePresenter, BalancePresenter balancePresenter)
        {
            _barIncomePresenter = barIncomePresenter;
            _balancePresenter = balancePresenter;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _businessFilter = world.Filter<BusinessComponents>().End();
            _businessComponentsPool = world.GetPool<BusinessComponents>();

            _playerFilter = world.Filter<PlayerBalanceComponent>().End();
            _balanceComponentsPool = world.GetPool<PlayerBalanceComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_playerFilter.GetEntitiesCount() <=0)
                return;

            int playerEntity = _playerFilter.GetRawEntities()[0];
            ref PlayerBalanceComponent balance =  ref _balanceComponentsPool.Get(playerEntity);
            
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
                        business.CurrentValueSlider = 0f;
                        balance.Amount += business.FinalReward;
                        
                        _balancePresenter?.UpdateBalancePlayer();
                    }

                    _barIncomePresenter?.RefreshBarSlider(business.ID, business.CurrentValueSlider,
                        business.MaxValueSlider);

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