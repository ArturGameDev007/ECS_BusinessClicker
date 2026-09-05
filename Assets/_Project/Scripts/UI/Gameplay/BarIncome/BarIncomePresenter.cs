using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.BarIncome
{
    public class BarIncomePresenter
    {
        private readonly EcsWorld _world;
        private readonly BarIncomeView _barIncomeView;

        private EcsFilter _businessFilter;
        private EcsFilter _playerFilter;

        // private EcsPool<BusinessIdComponent> _idPool;
        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<BusinessProgressComponent> _progressPool;

        public BarIncomePresenter(EcsWorld world, BarIncomeView barIncomeView)
        {
            _world = world;
            _barIncomeView = barIncomeView;

            // _businessFilter = _world.Filter<BusinessIdComponent>().Inc<BusinessProgressComponent>().End();
            _businessFilter = _world.Filter<BusinessEconomyComponent>().Inc<BusinessProgressComponent>().End();

            // _idPool = _world.GetPool<BusinessIdComponent>();
            _economyPool = _world.GetPool<BusinessEconomyComponent>();
            _progressPool = _world.GetPool<BusinessProgressComponent>();
        }

        public void Tick()
        {
            if (_barIncomeView == null)
                return;
        
            foreach (var entity in _businessFilter)
            {
                // ref BusinessIdComponent idComponent = ref _idPool.Get(entity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
                ref BusinessProgressComponent progressComponent = ref _progressPool.Get(entity);
        
                if (economyComponent.Level <= 0)
                {
                    _barIncomeView?.UpdateBarIncome(economyComponent.ID, 0f, progressComponent.MaxValueSlider);
                    continue;
                }
        
                _barIncomeView?.UpdateBarIncome(economyComponent.ID, progressComponent.CurrentValueSlider,
                    progressComponent.MaxValueSlider);
            }
        }
    }
}