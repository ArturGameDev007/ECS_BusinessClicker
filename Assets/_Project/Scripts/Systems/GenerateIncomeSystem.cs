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

        private EcsPool<BusinessIdComponent> _idPool;
        private EcsPool<BusinessProgressComponent> _progressPool;
        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _balanceComponentsPool;

        public GenerateIncomeSystem(BarIncomePresenter barIncomePresenter, BalancePresenter balancePresenter)
        {
            _barIncomePresenter = barIncomePresenter;
            _balancePresenter = balancePresenter;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _businessFilter = world.Filter<BusinessIdComponent>()
                .Inc<BusinessProgressComponent>()
                .Inc<BusinessEconomyComponent>().End();

            _idPool = world.GetPool<BusinessIdComponent>();
            _progressPool = world.GetPool<BusinessProgressComponent>();
            _economyPool = world.GetPool<BusinessEconomyComponent>();

            _playerFilter = world.Filter<PlayerBalanceComponent>().End();
            _balanceComponentsPool = world.GetPool<PlayerBalanceComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_playerFilter.GetEntitiesCount() <= 0)
                return;

            int playerEntity = _playerFilter.GetRawEntities()[0];
            ref PlayerBalanceComponent balance = ref _balanceComponentsPool.Get(playerEntity);

            foreach (var businessEntity in _businessFilter)
            {
                ref BusinessIdComponent idComponent = ref _idPool.Get(businessEntity);
                ref BusinessProgressComponent progressComponent = ref _progressPool.Get(businessEntity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(businessEntity);

                if (economyComponent.Level <= 0)
                {
                    _barIncomePresenter?.RefreshBarSlider(idComponent.ID, 0f, progressComponent.MaxValueSlider);
                    continue;
                }

                CalculateBusinessIncome(ref economyComponent);

                progressComponent.CurrentTime += Time.deltaTime;

                float currentTimeProgress = progressComponent.CurrentTime;
                float timer = progressComponent.IncomeDuration;

                if (currentTimeProgress >= timer)
                {
                    progressComponent.CurrentValueSlider += progressComponent.CountSliderStep;

                    if (progressComponent.CurrentValueSlider >= progressComponent.MaxValueSlider)
                    {
                        progressComponent.CurrentValueSlider = 0f;
                        balance.Amount += economyComponent.FinalReward;

                        _balancePresenter?.UpdateBalancePlayer();
                    }

                    _barIncomePresenter?.RefreshBarSlider(idComponent.ID, progressComponent.CurrentValueSlider,
                        progressComponent.MaxValueSlider);

                    progressComponent.CurrentTime -= timer;
                }
            }
        }

        private void CalculateBusinessIncome(ref BusinessEconomyComponent economyComponent)
        {
            economyComponent.FinalReward =
                economyComponent.Level * economyComponent.BaseIncome * (
                    1.0d + economyComponent.FirstUpgradeIncome + economyComponent.SecondUpgradeIncome);
        }
    }
}