using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._Core.View;
using _Source.Code.Services;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Code.UI
{
    public class EnemyUnitsCountBar:AKView
    {
        [AKInject] private BattleRoundService _battleRoundService;
        [SerializeField] private Image progressImage;

        protected override void PostInit()
        {
            _battleRoundService.OnUnitCountUpdated += UpdateFillAmount;
        }

        private void UpdateFillAmount()
        {
            progressImage.fillAmount = (float)_battleRoundService.GetEnemyUnitCount() / _battleRoundService.GetUnitsSpawnCount();
        }
    }
}