using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;


namespace _Client_.Scripts.ECS.Systems
{
    public class DamageSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _selfFilter;

        private EcsPool<DamageRequest> _damagePool;
        private EcsPool<HealthRef> _healthPool;
        private EcsPool<Die> _diePool;
        private EcsPool<DestroyRequest> _destroyRequestPool;
        private EcsPool<DeathRequest> _deathRequestPool;

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DamageRequest>().End();

            _damagePool = _world.GetPool<DamageRequest>();
            _healthPool = _world.GetPool<HealthRef>();
            _diePool = _world.GetPool<Die>();
            _destroyRequestPool = _world.GetPool<DestroyRequest>();
            _deathRequestPool = _world.GetPool<DeathRequest>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var damage = ref _damagePool.Get(entity);

                if (!damage.TargetPackedEntity.Unpack(_world, out var targetEntity))continue;
                
                Damage(in targetEntity,in damage.value);
            }
        }

        private void Damage(in int entity,in float value)
        {
            if(_diePool.Has(entity)) return;
            if(!_healthPool.Has(entity)) return;
            
            ref var health = ref _healthPool.Get(entity);
            health.value -= value;
            health.value = Mathf.Clamp(health.value, 0, health.value);
            
            if(health.value>0) return;

            _diePool.Add(entity);
            ref var destroy = ref _destroyRequestPool.Add(_world.NewEntity());
            destroy.TargetPackedEntity = _world.PackEntity(entity);
            destroy.Delay = 2f;

            _deathRequestPool.Add(_world.NewEntity()).TargetPackedEntity = destroy.TargetPackedEntity;
        }
    }
}