using _Project.Scripts.Components;
using Leopotam.EcsLite;

namespace _Project.Scripts.Systems
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        private readonly double _startBalance;

        public PlayerInitSystem(double startBalance)
        {
            _startBalance = startBalance;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            EcsPool<PlayerBalanceComponent> balance = world.GetPool<PlayerBalanceComponent>();

            int playerEntity = world.NewEntity();

            ref PlayerBalanceComponent balanceComponent = ref balance.Add(playerEntity);
            
            balanceComponent.Amount = _startBalance;
        }
    }
}