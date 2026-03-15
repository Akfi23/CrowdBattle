using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code.ECS.Components;
using _Source.Code.Services;
using _Source.Code.Utils.Extensions.EcsEvents;
using AKFramework.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace _Source.Code.ECS.Systems
{
    public class BattleStatusHandlerSystem : AKEcsSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _enemyFilter;
        
        private BattleRoundService _battleRoundService;
        
        protected override void Setup(ref IEcsSystems systems, ref IAKContainer container)
        {
            _world = systems.GetWorld();

            _playerFilter = _world.Filter<Unit>().Inc<Player>().Inc<Spawned>().Inc<Init>().Exc<Die>().End();
            _enemyFilter = _world.Filter<Unit>().Inc<Enemy>().Inc<Spawned>().Inc<Init>().Exc<Die>().End();

            _battleRoundService = container.Resolve<BattleRoundService>();
        }

        public override void Tick(ref IEcsSystems systems)
        {
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__Start_Battle))
            {
                SendBattleStatus(true);
                
                _battleRoundService.SetPlayersUnitCount(_playerFilter.GetEntitiesCount());
                _battleRoundService.SetEnemyUnitCount(_enemyFilter.GetEntitiesCount());
            }
            
            if (_world.HasEventCallback<AKEventCallback>(AKEvents.Game__End_Battle))
            {
                SendBattleStatus(false);
            }
        }

        private void SendBattleStatus(bool status)
        {
            var entity = _world.NewEntity ();
            ref var evt = ref _world.GetPool<EcsGroupSystemState> ().Add (entity);
            evt.Name = "Battle Systems";
            evt.State = status;
        }

        private void InitBattleRound()
        {
            
        }
    }
}