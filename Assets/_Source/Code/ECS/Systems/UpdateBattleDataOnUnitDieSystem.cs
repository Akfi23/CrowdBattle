using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKEvents.Runtime;
using _Source.Code.ECS.Components;
using _Source.Code.Services;
using AKFramework.Generated;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Systems
{
    public class UpdateBattleDataOnUnitDieSystem: AKEcsSystem
    {
        private EcsWorld _world;

        private EcsFilter _destroyFilter;
        private EcsFilter _playerFilter;
        private EcsFilter _enemyFilter;
        
        private EcsPool<DestroyRequest> _destroyPool;
        private EcsPool<Unit> _unitPool;
        
        private BattleRoundService _battleRoundService;
        private IAKEventsService _eventsService;
        
        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _destroyFilter = _world.Filter<DestroyRequest>().End();
            _playerFilter = _world.Filter<Unit>().Inc<Player>().Inc<Spawned>().Inc<Init>().Exc<Die>().End();
            _enemyFilter = _world.Filter<Unit>().Inc<Enemy>().Inc<Spawned>().Inc<Init>().Exc<Die>().End();

            _destroyPool = _world.GetPool<DestroyRequest>();
            _unitPool = _world.GetPool<Unit>();
            
            _battleRoundService = container.Resolve<BattleRoundService>();
            _eventsService = container.Resolve<IAKEventsService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _destroyFilter)
            {
                ref var destroyRequest = ref _destroyPool.Get(entity);
                if(!destroyRequest.TargetPackedEntity.Unpack(_world,out var destroyedEntity)) continue;
                if(!_unitPool.Has(destroyedEntity)) continue;

                _battleRoundService.SetEnemyUnitCount(_enemyFilter.GetEntitiesCount());
                _battleRoundService.SetPlayersUnitCount(_playerFilter.GetEntitiesCount());
                
                if (_enemyFilter.GetEntitiesCount() < 1)
                {
                    _eventsService.Broadcast(AKEvents.Game__End_Battle);
                    break;
                }
                
                if (_playerFilter.GetEntitiesCount() < 1)
                {
                    _eventsService.Broadcast(AKEvents.Game__End_Battle);
                    break;
                }
            }   
        }
    }
}