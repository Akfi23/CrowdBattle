using System;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code.Databases;
using UnityEngine;

namespace _Source.Code.Services
{
    public class BattleRoundService : IAKService
    {
        [AKInject] private BattleRoundDatabase _database;
        public Action OnUnitCountUpdated { get; set; } = delegate {  };

        private int _playersUnitCount;
        private int _enemyUnitCount;

        public int GetUnitsSpawnCount() => _database.UnitsCount;

        public void SetPlayersUnitCount(int count)
        {
            _playersUnitCount = count;
            OnUnitCountUpdated?.Invoke();
        }

        public void SetEnemyUnitCount(int count)
        {
            _enemyUnitCount = count;
            OnUnitCountUpdated?.Invoke();
        }

        public int GetPlayersUnitCount() => _playersUnitCount;
        public int GetEnemyUnitCount() => _enemyUnitCount;

        public void ChangeTimeScale()
        {
            Time.timeScale = Time.timeScale == 1 ? 2 : 1;
        }
    }
}