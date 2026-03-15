using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Source.Code.ECS.Systems
{
    public class AttackTargetSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _unitsFilter;

        private EcsPool<AttackTarget> _attackTargetPool;
        private EcsPool<TransformRef> _transformPool;
        private EcsPool<AttackDistance> _attackDistancePool;
        private EcsPool<AttackDelayTimer> _attackDelayTimerPool;
        private EcsPool<AttackSpeed> _attackSpeedPool;
        private EcsPool<Damage> _damagePool;
        private EcsPool<DamageRequest> _damageRequestPool;

        private EcsPackedEntity _target;
        private float _minDistance = Mathf.Infinity;
        private float _distance = 0f;

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _unitsFilter = _world.Filter<Unit>().Inc<Spawned>().Inc<AttackTarget>().Inc<AttackDistance>()
                .Inc<MovementSpeedRef>().Inc<NavMeshAgentRef>().Inc<Init>().Exc<Die>()
                .Exc<DestroyImmediateSelfRequest>().Exc<AttackDelayTimer>().End();

            _attackTargetPool = _world.GetPool<AttackTarget>();
            _transformPool = _world.GetPool<TransformRef>();
            _attackDistancePool = _world.GetPool<AttackDistance>();
            _attackDelayTimerPool = _world.GetPool<AttackDelayTimer>();
            _attackSpeedPool = _world.GetPool<AttackSpeed>();
            _damagePool = _world.GetPool<Damage>();
            _damageRequestPool = _world.GetPool<DamageRequest>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _unitsFilter)
            {
                ref var attackTarget = ref _attackTargetPool.Get(entity).target;
                if (!attackTarget.Unpack(_world, out var targetEntity)) continue;
                
                ref var transform = ref _transformPool.Get(entity).instance;
                ref var targetTransform = ref _transformPool.Get(targetEntity).instance;
                ref var attackDistance = ref _attackDistancePool.Get(entity).value;

                if (transform.position.DistanceXZ(targetTransform.position) >= attackDistance) continue;

                ref var damage = ref _damagePool.Get(entity).value;

                _damageRequestPool.Add(_world.NewEntity()) = new DamageRequest()
                {
                    value = damage,
                    OwnerPackedEntity = _world.PackEntity(entity),
                    TargetPackedEntity = attackTarget
                };

                ref var attackSpeed = ref _attackSpeedPool.Get(entity).value;
                _attackDelayTimerPool.Add(entity).Timer = attackSpeed;
            }
        }
    }
}