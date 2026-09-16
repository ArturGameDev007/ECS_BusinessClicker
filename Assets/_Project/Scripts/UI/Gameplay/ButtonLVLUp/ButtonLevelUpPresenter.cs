using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.UI.Gameplay.ButtonLVLUp
{
    public class ButtonLevelUpPresenter
    {
        private readonly ButtonLevelUpView _view;
        private readonly EcsWorld _world;

        private EcsFilter _businessFilter;

        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<BuyLevelRequestComponent> _buyLevelRequestPool;

        public ButtonLevelUpPresenter(ButtonLevelUpView view, EcsWorld world)
        {
            _view = view;
            _world = world;

            _businessFilter = _world.Filter<BusinessEconomyComponent>().End();

            _economyPool = _world.GetPool<BusinessEconomyComponent>();
            _buyLevelRequestPool = _world.GetPool<BuyLevelRequestComponent>();
        }

        public void Init()
        {
            if (_view == null)
                return;

            for (int i = 0; i < _view.ButtonBuy.Length; i++)
            {
                if (_view.ButtonBuy[i] != null)
                {
                    int index = i;
                    _view.ButtonBuy[i].onClick.AddListener(() => OnButtonClickBuyLevelUp(index));
                }
            }
        }

        public void Destroy()
        {
            if (_view == null)
                return;

            for (int i = 0; i < _view.ButtonBuy.Length; i++)
            {
                if (_view.ButtonBuy[i] != null)
                {
                    int index = i;
                    _view.ButtonBuy[i].onClick.RemoveListener(() => OnButtonClickBuyLevelUp(index));
                }
            }
        }

        private void OnButtonClickBuyLevelUp(int index)
        {
            CheckBuyClick(index);
        }

        private void CheckBuyClick(int index)
        {
            if (_world == null)
                return;

            foreach (var entity in _businessFilter)
            {
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);

                if (economyComponent.ID != index)
                    continue;

                if (!_buyLevelRequestPool.Has(entity))
                    _buyLevelRequestPool.Add(entity);
                
                break;
            }
        }
    }
}