using _Project.Scripts._Configs;
using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.BusinessModel
{
    public class BusinessInformationPresenter
    {
        private readonly EcsWorld  _world;
        private readonly EcsFilter _businessFilter;
        private readonly EcsPool<BusinessComponents> _businessComponents;
        
        private readonly BusinessModelView  _businessModelView;

        public BusinessInformationPresenter(EcsWorld world, BusinessModelView businessModelView)
        {
            _world = world;
            _businessFilter = _world.Filter<BusinessComponents>().End();
            _businessComponents = _world.GetPool<BusinessComponents>();
            
            _businessModelView = businessModelView;
        }

        public void RefreshLevel()
        {
            if (_world == null)
                return;

            foreach (var entity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponents.Get(entity);
                
                _businessModelView?.SetLevel(business.ID, business.Level);
            }
        }

        public void RefreshIncome()
        {
            if (_world == null)
                return;
            
            foreach (var entity in _businessFilter)
            {
                ref BusinessComponents business = ref _businessComponents.Get(entity);
                
                _businessModelView?.SetIncome(business.ID, business.BaseIncome);
            }
        }
    }
}