using _Project.Scripts.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public sealed class GenerateIncomeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _playerFilter;
        private EcsFilter _businessFilter;

        private EcsPool<BusinessProgressComponent> _progressPool;
        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _balanceComponentsPool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _businessFilter = world.Filter<BusinessEconomyComponent>().Inc<BusinessProgressComponent>().End();

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
                ref BusinessProgressComponent progressComponent = ref _progressPool.Get(businessEntity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(businessEntity);

                if (economyComponent.Level <= 0)
                {
                    continue;
                }

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
                        
                        balance.OnBalanceChanged?.Invoke(balance.Amount);
                    }

                    progressComponent.CurrentTime -= timer;
                }
            }
        }
    }
}