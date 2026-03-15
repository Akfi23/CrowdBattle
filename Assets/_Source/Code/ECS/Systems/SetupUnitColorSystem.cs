using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKPools.Runtime;
using _Source.Code.ECS.Components;
using _Source.Code.Services;
using _Source.Code.Utils.Extensions.EcsEvents;
using AKFramework.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Source.Code.ECS.Systems
{
    public class SetupUnitColorSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _unitsFilter;
        
        private EcsPool<MeshRendererRef> _meshRendererPool;
        private EcsPool<SetupValueParametersRequest> _setupParametersRequestPool;
        
        private UnitColorService _unitColorService;
        
        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _unitsFilter = _world.Filter<Unit>().Inc<Spawned>().Inc<Graphic>().Inc<MeshRendererRef>().Inc<Init>().End();

            _meshRendererPool = _world.GetPool<MeshRendererRef>();
            _setupParametersRequestPool = _world.GetPool<SetupValueParametersRequest>();

            _unitColorService = container.Resolve<UnitColorService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Spawn_Units))
            {
                SetupColor();
            }
            
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Remix_Units))
            {
                SetupColor();
            }
        }

        private void SetupColor()
        {
            foreach (var entity in _unitsFilter)
            {
                var data = _unitColorService.GetRandomData();
                
                ref var setupRequest = ref _setupParametersRequestPool.Add(_world.NewEntity());
                setupRequest.TargetPackedEntity = _world.PackEntity(entity);
                setupRequest.ValueParameters = data.ValueParameters;

                ref var meshRenderer = ref _meshRendererPool.Get(entity).instance;
                meshRenderer.material = data.GetMainParameterValue();
                
            }
        }
    }
}