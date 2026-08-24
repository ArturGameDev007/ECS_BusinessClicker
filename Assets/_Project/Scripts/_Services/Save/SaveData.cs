using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts._Services.Save
{
    public class SaveData
    {
        private readonly EcsWorld _world;

        private readonly SaveServices _saveServices;

        private EcsFilter _businessFilter;
        private EcsFilter _balanceFilter;

        private EcsPool<BusinessIdComponent> _idPool;
        private EcsPool<BusinessEconomyComponent> _economyPool;
        private EcsPool<PlayerBalanceComponent> _playerBalancePool;

        public SaveData(EcsWorld world, SaveServices saveServices)
        {
            _world = world;
            _saveServices = saveServices;

            _businessFilter = world.Filter<BusinessIdComponent>().Inc<BusinessEconomyComponent>().End();
            
            _idPool = world.GetPool<BusinessIdComponent>();
            _economyPool = world.GetPool<BusinessEconomyComponent>();

            _balanceFilter = _world.Filter<PlayerBalanceComponent>().End();
            _playerBalancePool = _world.GetPool<PlayerBalanceComponent>();
        }

        public void SaveDataPlayer()
        {
            if (_balanceFilter.GetEntitiesCount() <= 0)
                return;

            int players = _balanceFilter.GetRawEntities()[0];
            int playerEntity = players;

            ref PlayerBalanceComponent balance = ref _playerBalancePool.Get(playerEntity);

            PlayerSaveData playerSaveData = new PlayerSaveData(balance.Amount);

            foreach (var entity in _businessFilter)
            {
                ref BusinessIdComponent idComponent = ref _idPool.Get(entity);
                ref BusinessEconomyComponent economyComponent = ref _economyPool.Get(entity);
                
                BusinessSaveData businessSaveData = new BusinessSaveData
                (
                    idComponent.ID,
                    economyComponent.Level,
                    economyComponent.BaseIncome,
                    economyComponent.FirstUpgradeIncome,
                    economyComponent.SecondUpgradeIncome
                );

                playerSaveData.BusinessSave.Add(businessSaveData);
            }

            _saveServices.Save(playerSaveData);
        }
    }
}