using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using AKFramework.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Source.Code.ECS.Systems
{
    public class FollowAttackTargetSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _unitsFilter;

        private EcsPool<AttackTarget> _attackTargetPool;
        private EcsPool<TransformRef> _transformPool;
        private EcsPool<NavMeshAgentRef> _agentPool;
        private EcsPool<MovementSpeedRef> _movementSpeedPool;
        private EcsPool<AttackDistance> _attackDistancePool;
        private EcsPool<AnimatorRef> _animatorPool;

        private EcsPackedEntity _target;
        private float _minDistance = Mathf.Infinity;
        private float _distance = 0f;
        
        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _unitsFilter = _world.Filter<Unit>().Inc<Spawned>().Inc<AttackTarget>().Inc<AttackDistance>().Inc<MovementSpeedRef>().Inc<NavMeshAgentRef>().Inc<Init>().Exc<Die>().Exc<DestroyImmediateSelfRequest>().End();

            _attackTargetPool = _world.GetPool<AttackTarget>();
            _transformPool = _world.GetPool<TransformRef>();
            _agentPool = _world.GetPool<NavMeshAgentRef>();
            _movementSpeedPool = _world.GetPool<MovementSpeedRef>();
            _attackDistancePool = _world.GetPool<AttackDistance>();
            _animatorPool = _world.GetPool<AnimatorRef>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _unitsFilter)
            {
                ref var attackTarget = ref _attackTargetPool.Get(entity).target;
                if(!attackTarget.Unpack(_world,out var targetEntity)) continue;

                ref var transform = ref _transformPool.Get(entity).instance;
                ref var targetTransform = ref _transformPool.Get(targetEntity).instance;
                ref var agent = ref _agentPool.Get(entity).instance;
                ref var attackDistance = ref _attackDistancePool.Get(entity).value;
                ref var animator = ref _animatorPool.Get(entity).instance;

                if (transform.position.DistanceXZ(targetTransform.position) < attackDistance)
                {
                    if (agent.hasPath)
                    {
                        agent.ResetPath();
                        // animator.SetTrigger(AnimatorHashStrings.IsIdle);
                    }
                    
                    continue;
                }

                ref var moveSpeed = ref _movementSpeedPool.Get(entity).value;

                agent.speed = moveSpeed;
                agent.SetDestination(targetTransform.position);
                animator.SetTrigger(AnimatorHashStrings.IsRun);
            }
        }
    }
}