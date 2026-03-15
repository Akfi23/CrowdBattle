using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Client_.Scripts.ECS.Systems
{
    public class DestroyTimerSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _selfFilter;
        private EcsFilter _filter;

        private EcsPool<DestroyRequest> _destroyPool;
        private EcsPool<DestroySelfRequest> _destroySelfPool;
        private EcsPool<Spawned> _spawnedPool;
        private EcsPool<DestroyImmediateSelfRequest> _destroyImmediatePool;

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DestroyRequest>().End();
            _selfFilter = _world.Filter<DestroySelfRequest>().Exc<DestroyImmediateSelfRequest>().End();

            _destroyPool = _world.GetPool<DestroyRequest>();
            _destroySelfPool = _world.GetPool<DestroySelfRequest>();
            _spawnedPool = _world.GetPool<Spawned>();
            _destroyImmediatePool = _world.GetPool<DestroyImmediateSelfRequest>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _selfFilter)
            {
                ref var destroy = ref _destroySelfPool.Get(entity);

                destroy.Delay -= Time.deltaTime;
                if(destroy.Delay > 0) continue;

                _destroyImmediatePool.Add(entity);
            }

            foreach (var entity in _filter)
            {
                ref var destroy = ref _destroyPool.Get(entity);

                destroy.Delay -= Time.deltaTime;

                if(destroy.Delay > 0) continue;
                
                if (!destroy.TargetPackedEntity.Unpack(_world, out var targetEntity))
                {
                    _world.DelEntity(entity);
                    continue;
                }

                if (!_spawnedPool.Has(targetEntity))
                {
                    _world.DelEntity(entity);
                    continue;
                }

                if (_destroyImmediatePool.Has(targetEntity))
                {
                    _world.DelEntity(entity);
                    continue;
                }

                _destroyImmediatePool.Add(targetEntity);
                _world.DelEntity(entity);
            }
        }
    }
}