using _Client_.Scripts.Utils.Extensions;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKPools.Runtime;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.ECS.Components;
using _Source.Code.Services;
using _Source.Code.Utils.Extensions.EcsEvents;
using AKFramework.Generated;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Systems
{
    public class InitUnitsSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _unitsFilter;

        private EcsPool<HealthRef> _healthPool;
        private EcsPool<Damage> _damagePool;
        private EcsPool<AttackSpeed> _attackSpeedPool;
        private EcsPool<MovementSpeedRef> _moveSpeedPool;
        private EcsPool<Init> _initPool;

        private UnitsDataService _unitsDataService;

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _unitsFilter = _world.Filter<Unit>().Inc<Spawned>().Inc<AKTagRef>().End();

            _healthPool = _world.GetPool<HealthRef>();
            _damagePool = _world.GetPool<Damage>();
            _attackSpeedPool = _world.GetPool<AttackSpeed>();
            _moveSpeedPool = _world.GetPool<MovementSpeedRef>();
            _initPool = _world.GetPool<Init>();

            _unitsDataService = container.Resolve<UnitsDataService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Spawn_Units))
            {
                InitUnits();
            }

            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Remix_Units))
            {
                InitUnits();
            }
        }

        private void InitUnits()
        {
            foreach (var entity in _unitsFilter)
            {
                var initialParameters = _unitsDataService.GetInitialParameters;

                foreach (var parameter in initialParameters)
                {
                    var value = parameter.Value;

                    if (value.GetParameterTag().Equals(AKTags.Parameters__Health))
                    {
                        if (_healthPool.Has(entity))
                        {
                            ref var health = ref _healthPool.Get(entity);

                            health.baseValue = value.GetValueParameterValue();
                            health.value = health.baseValue;
                        }

                        continue;
                    }

                    if (value.GetParameterTag().Equals(AKTags.Parameters__Damage))
                    {
                        if (_damagePool.Has(entity))
                        {
                            ref var damage = ref _damagePool.Get(entity);
                            damage.value = value.GetValueParameterValue();
                        }

                        continue;
                    }

                    if (value.GetParameterTag().Equals(AKTags.Parameters__AttackSpeed))
                    {
                        if (_attackSpeedPool.Has(entity))
                        {
                            ref var attackSpeed = ref _attackSpeedPool.Get(entity);
                            attackSpeed.value = value.GetValueParameterValue();
                        }

                        continue;
                    }

                    if (value.GetParameterTag().Equals(AKTags.Parameters__MoveSpeed))
                    {
                        if (_moveSpeedPool.Has(entity))
                        {
                            ref var moveSpeed = ref _moveSpeedPool.Get(entity);
                            moveSpeed.value = value.GetValueParameterValue();
                        }

                        continue;
                    }
                }

                _initPool.SafeAdd(entity);
            }

        }
    }
}