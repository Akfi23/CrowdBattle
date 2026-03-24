using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using AKFramework.Generated;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Systems
{
    public class DeathAnimSystem : AKEcsSystem
    {
        private EcsWorld _world;

        private EcsFilter _destroyFilter;
        
        private EcsPool<DeathRequest> _deathRequestPool;
        private EcsPool<Unit> _unitPool;
        private EcsPool<AnimatorRef> _animatorPool;


        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _destroyFilter = _world.Filter<DeathRequest>().End();

            _deathRequestPool = _world.GetPool<DeathRequest>();
            _unitPool = _world.GetPool<Unit>();
            _animatorPool = _world.GetPool<AnimatorRef>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _destroyFilter)
            {
                ref var destroyRequest = ref _deathRequestPool.Get(entity);
                if (!destroyRequest.TargetPackedEntity.Unpack(_world, out var destroyedEntity)) continue;
                if (!_unitPool.Has(destroyedEntity)) continue;

                ref var animator = ref _animatorPool.Get(destroyedEntity).instance;
                animator.SetTrigger(AnimatorHashStrings.IsDeath);
            }
        }
    }
}