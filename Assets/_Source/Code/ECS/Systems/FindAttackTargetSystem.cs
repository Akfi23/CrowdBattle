using _Client_.Scripts.Utils.Extensions;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Source.Code.ECS.Systems
{
    public class FindAttackTargetSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerUnitsFilter;
        private EcsFilter _enemyUnitsFilter;

        private EcsPool<Die> _diePool;
        private EcsPool<AttackTarget> _attackTargetPool;
        private EcsPool<TransformRef> _transformPool;

        private EcsPackedEntity _target;
        private float _minDistance = Mathf.Infinity;
        private float _distance = 0f;
        
        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _playerUnitsFilter = _world.Filter<Unit>().Inc<Spawned>().Inc<Init>().Inc<Player>().Exc<Enemy>().Exc<Die>().Exc<DestroyImmediateSelfRequest>().End();
            _enemyUnitsFilter = _world.Filter<Unit>().Inc<Spawned>().Inc<Init>().Inc<Enemy>().Exc<Player>().Exc<Die>().Exc<DestroyImmediateSelfRequest>().End();

            _diePool = _world.GetPool<Die>();
            _attackTargetPool = _world.GetPool<AttackTarget>();
            _transformPool = _world.GetPool<TransformRef>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            if (Time.frameCount % 2 != 0) return;
            
            foreach (var entity in _playerUnitsFilter)
            {
                if (!_attackTargetPool.Has(entity))
                {
                    TryFindAttackTarget(in entity, _enemyUnitsFilter);
                    continue;
                }

                ref var attackTarget = ref _attackTargetPool.Get(entity).target;

                if (!attackTarget.Unpack(_world, out var targetEntity) || _diePool.Has(targetEntity))
                {
                    TryFindAttackTarget(in entity, _enemyUnitsFilter);
                }
                else
                {
                    _attackTargetPool.Del(entity);
                }
            }

            foreach (var entity in _enemyUnitsFilter)
            {
                if (!_attackTargetPool.Has(entity))
                {
                    TryFindAttackTarget(in entity, _playerUnitsFilter);
                    continue;
                }

                ref var attackTarget = ref _attackTargetPool.Get(entity).target;

                if (!attackTarget.Unpack(_world, out var targetEntity) || _diePool.Has(targetEntity))
                {
                    TryFindAttackTarget(in entity, _playerUnitsFilter);
                }
                else
                {
                    _attackTargetPool.Del(entity);
                }
            }
        }

        private void TryFindAttackTarget(in int entity,EcsFilter targetsFilter)
        {
            ref var transform = ref _transformPool.Get(entity).instance;
            FindClosestTarget(transform.position,targetsFilter);
            
            if(_target.EqualsTo(default)) return;

            _attackTargetPool.SafeAdd(entity).target = _target;
        }
        
        private void FindClosestTarget(in Vector3 position,EcsFilter targetFilter)
        {
            _minDistance = Mathf.Infinity;
            _target = default;

            foreach (var entity in targetFilter)
            {
                ref var transform = ref _transformPool.Get(entity).instance;
                _distance = position.DistanceXZ(transform.position);

                if (_distance > _minDistance) continue;

                _minDistance = _distance;
                _target = _world.PackEntity(entity);
            }
        }
    }
}