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
        private readonly BarIncomeModel _barIncomeModel;
        private readonly BalanceModel _balanceModel;
        private readonly BalancePresenter _balancePresenter;

        private EcsFilter _playerFilter;
        private EcsFilter _businessFilter;

        private EcsPool<PlayerBalanceComponent> _playerBalancePool;
        private EcsPool<BusinessComponents> _businessComponentsPool;

        public GenerateIncomeSystem(BarIncomePresenter barIncomePresenter, BarIncomeModel barIncomeModel,
            BalanceModel balanceModel, BalancePresenter balancePresenter)
        {
            _barIncomePresenter = barIncomePresenter;
            _barIncomeModel = barIncomeModel;
            _balanceModel = balanceModel;
            _balancePresenter = balancePresenter;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _playerFilter = world.Filter<PlayerBalanceComponent>().End();
            _businessFilter = world.Filter<BusinessComponents>().End();

            _playerBalancePool = world.GetPool<PlayerBalanceComponent>();
            _businessComponentsPool = world.GetPool<BusinessComponents>();
        }

        public void Run(IEcsSystems systems)
        {
            int playerEntity = -1;

            foreach (int entity in _playerFilter)
            {
                playerEntity = entity;
                break;
            }

            if (playerEntity == -1)
                return;

            ref PlayerBalanceComponent playerBalance = ref _playerBalancePool.Get(playerEntity);

            foreach (var businessEntity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponentsPool.Get(businessEntity);

                if (business.Level <= 0)
                {
                    _barIncomePresenter?.RefreshBarSlider(business.ID, 0f,  business.MaxValueSlider);
                    continue;
                }

                CalculateBusinessIncome(ref business);

                business.CurrentTime += Time.deltaTime;

                float currentTimeProgress = business.CurrentTime;
                float timer = business.IncomeDuration;

                if (currentTimeProgress >= timer)
                {
                    bool isBarFull = _barIncomeModel.AddBarIncomeSlider(
                        business.CurrentValueSlider, business.CountSliderStep, business.MaxValueSlider);

                    business.CurrentValueSlider = _barIncomeModel.CurrentValue;

                    playerBalance.Amount += business.FinalReward;
                    _barIncomePresenter?.RefreshBarSlider(business.ID, business.CurrentValueSlider,  business.MaxValueSlider);

                    business.CurrentTime -= timer;

                    if (isBarFull)
                    {
                        _balancePresenter?.UpdateBalancePlayer(playerBalance.Amount);
                        business.CurrentValueSlider = 0f;
                    }
                }

                // _barIncomePresenter?.RefreshBar();
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