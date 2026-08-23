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

        private EcsPool<BusinessComponents> _businessComponentsPool;
        private EcsPool<PlayerBalanceComponent> _playerBalancePool;

        public SaveData(EcsWorld world, SaveServices saveServices)
        {
            _world = world;
            _saveServices = saveServices;

            _businessFilter = _world.Filter<BusinessComponents>().End();
            _businessComponentsPool = _world.GetPool<BusinessComponents>();

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
                ref BusinessComponents business = ref _businessComponentsPool.Get(entity);

                BusinessSaveData businessSaveData = new BusinessSaveData
                (
                    business.ID,
                    business.Level,
                    business.BaseIncome,
                    business.FirstUpgradeIncome,
                    business.SecondUpgradeIncome
                );

                playerSaveData.BusinessSave.Add(businessSaveData);
            }

            _saveServices.Save(playerSaveData);
        }
    }
}