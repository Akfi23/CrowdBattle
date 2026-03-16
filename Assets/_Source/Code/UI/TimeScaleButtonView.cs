using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKUI.Runtime;
using _Source.Code._Core.View;
using _Source.Code.Services;
using TMPro;
using UnityEngine;

namespace _Source.Code.UI
{
    public class TimeScaleButtonView : ButtonView
    {
        [AKInject] private BattleRoundService _battleRoundService;
        
        [SerializeField] private TMP_Text text;


        protected override void PostInit()
        {
            text.SetText($"x{Time.timeScale:0}");
        }

        protected override void ButtonClick()
        {
            _battleRoundService.ChangeTimeScale();
            text.SetText($"x{Time.timeScale:0}");

        }
    }
}
