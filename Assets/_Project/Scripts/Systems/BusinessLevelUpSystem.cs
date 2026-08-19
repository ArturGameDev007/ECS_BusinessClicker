// using _Project.Scripts._Configs;
// using _Project.Scripts.Components;
// using _Project.Scripts.UI.Gameplay.Balance;
// using Leopotam.EcsLite;
//
// namespace _Project.Scripts.Systems
// {
//     public sealed class BusinessLevelUpSystem : IEcsRunSystem, IEcsInitSystem
//     {
//         private EcsFilter _requestFilter;
//         private EcsFilter _businessFilter;
//         private EcsFilter _playerFilter;
//
//         private EcsPool<BusinessComponents> _businessPool;
//         private EcsPool<TryLevelUpEventComponent> _requestPool;
//         private EcsPool<PlayerBalanceComponent> _playerPool;
//
//         private readonly PriceLevelUpConfig _config;
//         private readonly BalancePresenter _balancePresenter;
//
//         public BusinessLevelUpSystem(PriceLevelUpConfig config, BalancePresenter balancePresenter)
//         {
//             _config = config;
//             _balancePresenter = balancePresenter;
//         }
//
//         public void Init(IEcsSystems systems)
//         {
//             EcsWorld world = systems.GetWorld();
//
//             _requestFilter = world.Filter<TryLevelUpEventComponent>().End();
//             _businessFilter = world.Filter<BusinessComponents>().End();
//             _playerFilter = world.Filter<PlayerBalanceComponent>().End();
//
//             _businessPool = world.GetPool<BusinessComponents>();
//             _requestPool = world.GetPool<TryLevelUpEventComponent>();
//             _playerPool = world.GetPool<PlayerBalanceComponent>();
//         }
//
//
//         public void Run(IEcsSystems systems)
//         {
//             if (_requestFilter.GetEntitiesCount() <= 0)
//                 return;
//             
//             int playerEntity = -1;
//
//             foreach (int playerE in _playerFilter)
//             {
//                 playerEntity = playerE;
//                 break;
//             }
//
//             ref PlayerBalanceComponent player = ref _playerPool.Get(playerEntity);
//
//             foreach (var requestEntity in _requestFilter)
//             {
//                 ref TryLevelUpEventComponent request = ref _requestPool.Get(requestEntity);
//                 var clickedBusinessID = request.BusinessIndex;
//                 
//                 foreach (var businessEntity in _businessFilter)
//                 {
//                     ref BusinessComponents business = ref _businessPool.Get(businessEntity);
//                     var businessID = business.ID;
//
//                     if (business.ID ==  clickedBusinessID)
//                     {
//                         if (clickedBusinessID >= 0 && clickedBusinessID < _config.Price.Length)
//                         {
//                             double cost = (business.Level + 1) * _config.Price[businessID];
//
//                             if (player.Amount >= cost)
//                             {
//                                 player.Amount -= cost;
//                                 business.Level++;
//
//                                 _balancePresenter?.UpdateBalancePlayer(player.Amount);
//                             }
//                         }
//                     }
//
//                 
//                     _requestPool.Del(playerEntity);
//                 }
//             }
//             
//             
//             // foreach (var businessEntity in _businessFilter)
//             // {
//             //     ref BusinessComponents business = ref _businessPool.Get(businessEntity);
//             //     var businessID = business.ID;
//             //
//             //     if (businessID >= 0 && businessID < _config.Price.Length)
//             //     {
//             //         double cost = (business.Level + 1) * _config.Price[businessID];
//             //
//             //         if (player.Amount >= cost)
//             //         {
//             //             player.Amount -= cost;
//             //             business.Level++;
//             //             
//             //             _balancePresenter?.UpdateBalancePlayer(player.Amount);
//             //         }
//             //     }
//             //     
//             //     _requestPool.Del(playerEntity);
//             // }
//         }
//     }
// }