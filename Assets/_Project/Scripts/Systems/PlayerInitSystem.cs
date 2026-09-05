using _Project.Scripts.Components;
using _Project.Scripts.Services.Save;
using Leopotam.EcsLite;

namespace _Project.Scripts.Systems
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly SaveServices _saveServices;

        public PlayerInitSystem(SaveServices saveServices)
        {
            _saveServices = saveServices;
        }

        private EcsPool<PlayerBalanceComponent> _playerBalancePool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            double loadBalance = _saveServices.HasSave() ? _saveServices.Load().Balance : 0d;
            
            int playerEntity = world.NewEntity();

            _playerBalancePool = world.GetPool<PlayerBalanceComponent>();
            
            ref PlayerBalanceComponent balanceComponent = ref _playerBalancePool.Add(playerEntity);

            balanceComponent.Amount = loadBalance;
        }
    }
}