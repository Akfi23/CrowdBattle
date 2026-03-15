using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKEvents.Runtime;
using _Source.Code.ECS.Components;
using _Source.Code.Utils.Extensions.EcsEvents;
using AKFramework.Generated;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Systems
{
    public class ClearBattleFieldSystem : AKEcsSystem
    {
        private EcsWorld _world;

        private EcsFilter _playerFilter;
        private EcsFilter _enemyFilter;
        
        private EcsPool<DestroyRequest> _destroyPool;

        private IAKEventsService _eventsService;
        
        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _playerFilter = _world.Filter<Unit>().Inc<Player>().Inc<Spawned>().Inc<Init>().Exc<DestroyImmediateSelfRequest>().End();
            _enemyFilter = _world.Filter<Unit>().Inc<Enemy>().Inc<Spawned>().Inc<Init>().Exc<DestroyImmediateSelfRequest>().End();

            _destroyPool = _world.GetPool<DestroyRequest>();
            
            _eventsService = container.Resolve<IAKEventsService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            if (!_world.HasEventCallback<AKEventCallback>(AKEvents.Game__End_Battle)) return;
            
            foreach (var entity in _playerFilter)
            {
                _destroyPool.Add(_world.NewEntity()).TargetPackedEntity = _world.PackEntity(entity);
            }
            
            foreach (var entity in _enemyFilter)
            {
                _destroyPool.Add(_world.NewEntity()).TargetPackedEntity = _world.PackEntity(entity);
            }
            
            _eventsService.Broadcast(AKEvents.Game__Spawn_Units);
        }
    }
}