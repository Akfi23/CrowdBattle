using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Systems
{
    public class AutoDestroySystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<AutoDestroy> _autoDestroyPool;
        private EcsPool<DestroySelfRequest> _destroyPool;

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<AutoDestroy>().Inc<Spawned>().Exc<DestroySelfRequest>().End();

            _autoDestroyPool = _world.GetPool<AutoDestroy>();
            _destroyPool = _world.GetPool<DestroySelfRequest>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var autoDestroy = ref _autoDestroyPool.Get(entity);

                _destroyPool.Add(entity) = new DestroySelfRequest
                {
                    Delay = autoDestroy.delay
                };
            }
        }
    }
}