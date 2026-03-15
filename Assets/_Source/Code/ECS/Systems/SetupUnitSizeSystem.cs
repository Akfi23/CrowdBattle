using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using _Source.Code.Services;
using _Source.Code.Utils.Extensions.EcsEvents;
using AKFramework.Generated;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Systems
{
    public class SetupUnitSizeSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _unitsFilter;

        private EcsPool<Graphic> _graphicPool;
        private EcsPool<SetupValueParametersRequest> _setupParametersRequestPool;
        
        private UnitSizeService _unitSizeService;

        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _unitsFilter = _world.Filter<Unit>().Inc<Spawned>().Inc<Graphic>().Inc<Init>().End();

            _graphicPool = _world.GetPool<Graphic>();
            _setupParametersRequestPool = _world.GetPool<SetupValueParametersRequest>();
            
            _unitSizeService = container.Resolve<UnitSizeService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Spawn_Units))
            {
                SetupSize();
            }
            
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Remix_Units))
            {
                SetupSize();
            }
        }

        private void SetupSize()
        {
            foreach (var entity in _unitsFilter)
            {
                var data = _unitSizeService.GetRandomData();
                
                ref var setupRequest = ref _setupParametersRequestPool.Add(_world.NewEntity());
                setupRequest.TargetPackedEntity = _world.PackEntity(entity);
                setupRequest.ValueParameters = data.ValueParameters;

                ref var graphic = ref _graphicPool.Get(entity).instance;

                if (graphic != null)
                {
                    graphic.transform.localScale = data.GetMainParameterValue();
                }
            }
        }
    }
}