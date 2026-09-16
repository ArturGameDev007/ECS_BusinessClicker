using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.BusinessModel
{
    public class BusinessInformationPresenter
    {
        private readonly EcsWorld _world;
        private readonly BusinessModelView _businessModelView;

        public BusinessInformationPresenter(EcsWorld world, BusinessModelView businessModelView)
        {
            _world = world;
            _businessModelView = businessModelView;
        }

        public void RefreshLevel(int id, int level)
        {
            if (_world == null)
                return;

            _businessModelView?.SetLevel(id, level);
        }

        public void RefreshIncome(int id, double income)
        {
            if (_world == null)
                return;

            _businessModelView?.SetIncome(id, income);
        }
    }
}