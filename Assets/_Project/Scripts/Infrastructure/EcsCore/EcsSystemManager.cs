using _Project.Scripts.Configs;
using _Project.Scripts.Services.Save;
using _Project.Scripts.Systems;
using _Project.Scripts.UI.Gameplay.Balance;
using _Project.Scripts.UI.Gameplay.BarIncome;
using Leopotam.EcsLite;

namespace _Project.Scripts.Infrastructure.EcsCore
{
    public class EcsSystemManager
    {
        private readonly EcsWorld _world;
        private readonly SaveServices _saveServices;
        private readonly PriceLevelUpConfig _priceLevelUpConfig;
        private readonly PriceUpgradesConfig _priceUpgradesConfig;

        private EcsSystems _systems;

        public EcsSystemManager(EcsWorld world, SaveServices saveServices,PriceLevelUpConfig priceLevelUpConfig,
            PriceUpgradesConfig priceUpgradesConfig)
        {
            _world = world;
            _saveServices = saveServices;
            _priceLevelUpConfig = priceLevelUpConfig;
            _priceUpgradesConfig = priceUpgradesConfig;
        }

        public void Init()
        {
            _systems = new EcsSystems(_world);

            _systems
                .Add(new PlayerInitSystem(_saveServices))
                .Add(new BuyLevelUpSystem(_priceLevelUpConfig))
                .Add(new BuyUpgradeSystem(_priceUpgradesConfig))
                .Add(new BusinessEconomySystem())
                .Add(new GenerateIncomeSystem())
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