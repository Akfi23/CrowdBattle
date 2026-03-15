using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using AKFramework.Generated;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Systems
{
    public class SetupValueParametersSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        private EcsPool<SetupValueParametersRequest> _setupParametersRequestPool;
        private EcsPool<HealthRef> _healthPool;
        private EcsPool<Damage> _damagePool;
        private EcsPool<AttackSpeed> _attackSpeedPool;
        private EcsPool<MovementSpeedRef> _moveSpeedPool;
        private EcsPool<DestroyRequest> _destroyPool;
        private EcsPool<Die> _diePool;
        

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<SetupValueParametersRequest>().End();
            _healthPool = _world.GetPool<HealthRef>();
            _damagePool = _world.GetPool<Damage>();
            _attackSpeedPool = _world.GetPool<AttackSpeed>();
            _moveSpeedPool = _world.GetPool<MovementSpeedRef>();
            _setupParametersRequestPool = _world.GetPool<SetupValueParametersRequest>();
            _destroyPool = _world.GetPool<DestroyRequest>();
            _diePool = _world.GetPool<Die>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _setupParametersRequestPool.Get(entity);
                if(!request.TargetPackedEntity.Unpack(_world,out var unitEntity)) continue;
                if(request.ValueParameters==null) continue;
                if(request.ValueParameters.Length<1) continue;
                
                foreach (var parameter in request.ValueParameters)
                {
                    if (parameter.GetParameterTag().Equals(AKTags.Parameters__Health))
                    {
                        if (_healthPool.Has(unitEntity))
                        {
                            ref var health = ref _healthPool.Get(unitEntity);

                            health.baseValue += parameter.GetValueParameterValue();
                            health.value = health.baseValue;
                        }
                    }

                    if (parameter.GetParameterTag().Equals(AKTags.Parameters__Damage))
                    {
                        if (_damagePool.Has(unitEntity))
                        {
                            ref var damage = ref _damagePool.Get(unitEntity);
                            damage.value += parameter.GetValueParameterValue();
                        }
                    }

                    if (parameter.GetParameterTag().Equals(AKTags.Parameters__AttackSpeed))
                    {
                        if (_attackSpeedPool.Has(unitEntity))
                        {
                            ref var attackSpeed = ref _attackSpeedPool.Get(unitEntity);
                            attackSpeed.value += parameter.GetValueParameterValue();
                        }
                    }

                    if (parameter.GetParameterTag().Equals(AKTags.Parameters__MoveSpeed))
                    {
                        if (_moveSpeedPool.Has(unitEntity))
                        {
                            ref var moveSpeed = ref _moveSpeedPool.Get(unitEntity);
                            moveSpeed.value += parameter.GetValueParameterValue();
                        }
                    }
                }
            }
        }
    }
}