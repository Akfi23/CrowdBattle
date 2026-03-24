using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKPools.Runtime;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.ECS.Components;
using _Source.Code.Services;
using _Source.Code.Utils.Extensions.EcsEvents;
using AKFramework.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Source.Code.ECS.Systems
{
    public class SetupUnitShapeSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _unitsFilter;
        
        private EcsPool<Graphic> _graphicPool;
        private EcsPool<SkinnedMeshRendererRef> _meshRendererPool;
        private EcsPool<SetupValueParametersRequest> _setupParametersRequestPool;
        private EcsPool<AnimatorRef> _animatorPool;
        private EcsPool<ShootFXRoot> _shootFXRootPool;

        private UnitShapeService _unitShapeService;
        private IAKPoolsService _poolsService;
        
        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _unitsFilter = _world.Filter<Unit>().Inc<Spawned>().Inc<Graphic>().Inc<AnimatorRef>().Inc<SkinnedMeshRendererRef>().Inc<Init>().End();

            _graphicPool = _world.GetPool<Graphic>();
            _meshRendererPool = _world.GetPool<SkinnedMeshRendererRef>();
            _setupParametersRequestPool = _world.GetPool<SetupValueParametersRequest>();
            _animatorPool = _world.GetPool<AnimatorRef>();
            _shootFXRootPool = _world.GetPool<ShootFXRoot>();
            
            _unitShapeService = container.Resolve<UnitShapeService>();
            _poolsService = container.Resolve<IAKPoolsService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Spawn_Units))
            {
                SetupShape();
            }
            
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Remix_Units))
            {
                SetupShape();
            }

        }

        private void SetupShape()
        {
            foreach (var entity in _unitsFilter)
            {
                var data = _unitShapeService.GetRandomData();
                ref var graphic = ref _graphicPool.Get(entity);

                ref var setupRequest = ref _setupParametersRequestPool.Add(_world.NewEntity());
                setupRequest.TargetPackedEntity = _world.PackEntity(entity);
                setupRequest.ValueParameters = data.ValueParameters;

                if (graphic.instance != null)
                {
                    _poolsService.Despawn(graphic.instance);
                    graphic.instance = null;
                }
                
                if(!_poolsService.Spawn(data.GetMainParameterValue(),new AKPrefabSpawnSettings()
                   {
                       Parent = graphic.root,
                       Position = graphic.root.position,
                       Rotation = Quaternion.identity
                   },out var spawnedShapeObject)) continue;
                
                graphic.instance = spawnedShapeObject;
                
                if(!AKEntityMappingService.GetEntity(spawnedShapeObject,_world,out var shapeEntity)) continue;
                if(!_meshRendererPool.Has(shapeEntity)) continue;
                
                ref var shapeRenderer = ref _meshRendererPool.Get(shapeEntity).instance;
                ref var unitRenderer = ref _meshRendererPool.Get(entity).instance;
                unitRenderer = shapeRenderer;

                ref var shapeAnimator = ref _animatorPool.Get(shapeEntity).instance;
                ref var unitAnimator = ref _animatorPool.Get(entity).instance;

                unitAnimator = shapeAnimator;

                ref var shootFX = ref _shootFXRootPool.Get(shapeEntity).fx;
                ref var unitFX = ref _shootFXRootPool.Get(entity).fx;

                unitFX = shootFX;
            }
        }
    }
}