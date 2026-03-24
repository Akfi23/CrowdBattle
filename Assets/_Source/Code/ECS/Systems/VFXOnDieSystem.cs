using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKPools.Runtime;
using _Source.Code.ECS.Components;
using AKFramework.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Source.Code.ECS.Systems
{
    public class VFXOnDieSystem : AKEcsSystem
    {
        private EcsWorld _world;

        private EcsFilter _destroyFilter;
        
        private EcsPool<DeathRequest> _deathRequestPool;
        private EcsPool<Unit> _unitPool;
        private EcsPool<TransformRef> _transformPool;

        private IAKPoolsService _poolsService;

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _destroyFilter = _world.Filter<DeathRequest>().End();

            _deathRequestPool = _world.GetPool<DeathRequest>();
            _unitPool = _world.GetPool<Unit>();
            _transformPool = _world.GetPool<TransformRef>();

            _poolsService = container.Resolve<IAKPoolsService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _destroyFilter)
            {
                ref var destroyRequest = ref _deathRequestPool.Get(entity);
                if (!destroyRequest.TargetPackedEntity.Unpack(_world, out var destroyedEntity)) continue;
                if (!_unitPool.Has(destroyedEntity)) continue;

                ref var transform = ref _transformPool.Get(destroyedEntity).instance;

                _poolsService.Spawn(AKPrefabs.VFX__Death_splash, new AKPrefabSpawnSettings() // move into separated component logic 
                {
                    Position = transform.position + Vector3.up
                },out var fxObject);
            }
        }
    }
}