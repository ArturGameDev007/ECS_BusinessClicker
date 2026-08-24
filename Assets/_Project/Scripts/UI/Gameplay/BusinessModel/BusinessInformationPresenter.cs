using _Project.Scripts._Configs;
using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.BusinessModel
{
    public class BusinessInformationPresenter
    {
        private readonly EcsWorld _world;
        private readonly BusinessModelView _businessModelView;

        private EcsFilter _businessFilter;

        private EcsPool<BusinessIdComponent> _idPool;
        private EcsPool<BusinessEconomyComponent> _economyPool;

        public BusinessInformationPresenter(EcsWorld world, BusinessModelView businessModelView)
        {
            _world = world;
            _businessModelView = businessModelView;

            _businessFilter = _world.Filter<BusinessIdComponent>().Inc<BusinessEconomyComponent>().End();

            _idPool = _world.GetPool<BusinessIdComponent>();
            _economyPool = _world.GetPool<BusinessEconomyComponent>();
        }

        public void RefreshLevel()
        {
            if (_world == null)
                return;

            foreach (var entity in _businessFilter)
            {
                ref BusinessIdComponent idComponent = ref _idPool.Get(entity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);

                _businessModelView?.SetLevel(idComponent.ID, economyComponent.Level);
            }
        }

        public void RefreshIncome()
        {
            if (_world == null)
                return;

            foreach (var entity in _businessFilter)
            {
                ref BusinessIdComponent idComponent = ref _idPool.Get(entity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);

                _businessModelView?.SetIncome(idComponent.ID, economyComponent.BaseIncome);
            }
        }
    }
}