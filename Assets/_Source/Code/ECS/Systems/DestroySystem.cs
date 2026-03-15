using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKPools.Runtime;
using _Source.Code.ECS.Components;
using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Source.Code.ECS.Systems
{
    public class DestroySystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private EcsPool<GameObjectRef> _gameObjectPool;
        private EcsPool<TransformRef> _transformPool;
        private EcsPool<DestroyImmediateSelfRequest> _destroyPool;

        private IAKPoolsService _poolsService;

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DestroyImmediateSelfRequest>().Inc<Spawned>().End();

            _gameObjectPool = _world.GetPool<GameObjectRef>();
            _transformPool = _world.GetPool<TransformRef>();
            _destroyPool = _world.GetPool<DestroyImmediateSelfRequest>();

            _poolsService = container.Resolve<IAKPoolsService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var gameObject = ref _gameObjectPool.Get(entity).instance;
                ref var transform = ref _transformPool.Get(entity).instance;

                transform.DOKill();

                _destroyPool.Del(entity);
                
                if (_poolsService.Despawn(gameObject)) continue;

                Object.DestroyImmediate(gameObject);
                _world.DelEntity(entity);
            }
        }
    }
}