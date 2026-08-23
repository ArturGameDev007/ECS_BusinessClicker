using _Project.Scripts.Systems;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BarIncome;
using Leopotam.EcsLite;

namespace _Project.Scripts._Infrastructure.EcsCore
{
    public class EcsSystemManager
    {
        private readonly EcsWorld _world;
        private readonly BalancePresenter _balancePresenter;
        private readonly BarIncomePresenter _barIncomePresenter;
        private readonly double _startBalance;

        private EcsSystems _systems;

        public EcsSystemManager(EcsWorld world, BalancePresenter balancePresenter, BarIncomePresenter barIncomePresenter,  double startBalance)
        {
            _world = world;
            _balancePresenter = balancePresenter;
            _barIncomePresenter = barIncomePresenter;
            _startBalance = startBalance;
        }

        public void Init()
        {
            _systems = new EcsSystems(_world);

            _systems
                .Add(new PlayerInitSystem(_startBalance))
                .Add(new GenerateIncomeSystem(_barIncomePresenter, _balancePresenter))
                .Init();
        }

        public void Tick()
        {
            _systems?.Run();
        }

        public void Destroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }
        }
    }
}