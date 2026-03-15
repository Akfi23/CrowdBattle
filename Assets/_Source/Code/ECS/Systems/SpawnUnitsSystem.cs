using _Client_.Scripts.Utils.Extensions;
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
    public class SpawnUnitsSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerSpawnerFilter;
        private EcsFilter _enemySpawnerFilter;
        
        private EcsPool<SpawnArea> _spawnAreaPool;
        private EcsPool<TransformRef> _transformPool;
        private EcsPool<NavMeshAgentRef> _navMeshAgentPool;
        
        private IAKPoolsService _poolsService;
        private BattleRoundService _battleRoundService;
        
        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _playerSpawnerFilter = _world.Filter<SpawnArea>().End();

            _spawnAreaPool = _world.GetPool<SpawnArea>();
            _transformPool = _world.GetPool<TransformRef>();
            _navMeshAgentPool = _world.GetPool<NavMeshAgentRef>();
            
            _poolsService = container.Resolve<IAKPoolsService>();
            _battleRoundService = container.Resolve<BattleRoundService>();
        }
        
        public override void Tick(ref IEcsSystems systems)
        {
            if (!_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Spawn_Units))
                return;
            
            foreach(var entity in _playerSpawnerFilter)
            {
                ref var spawnArea = ref _spawnAreaPool.Get(entity);
                ref var spawnAreaTransform = ref _transformPool.Get(entity).instance;
                
                for (int i = 0; i < _battleRoundService.GetUnitsSpawnCount(); i++)
                {
                    var spawnPos = Extensions.GetRandomPointInArea(in spawnArea.size, spawnAreaTransform);
                    
                    if(!_poolsService.Spawn(spawnArea.prefabToSpawn, new AKPrefabSpawnSettings()
                       {
                           Parent = null,
                           Position = spawnPos,
                           Rotation = Quaternion.identity
                       },out var spawnedUnitObject)) continue;
                    
                    if(!AKEntityMappingService.GetEntity(spawnedUnitObject,in _world,out var unitEntity)) continue;
                    
                    if(!_navMeshAgentPool.Has(unitEntity)) continue;

                    ref var navMeshAgent = ref _navMeshAgentPool.Get(unitEntity).instance;
                    navMeshAgent.Warp(spawnPos);
                }
            }
        }
    }
}