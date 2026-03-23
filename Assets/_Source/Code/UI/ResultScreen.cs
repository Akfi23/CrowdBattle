using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKUI.Runtime;
using _Source.Code.Services;
using TMPro;
using UnityEngine;

public class ResultScreen : ScreenAnimationView
{
    [AKInject] private BattleRoundService _battleRoundService;
    [SerializeField] private TMP_Text resultText;


    protected override void OnScreenShown()
    {
        base.OnScreenShown();
        UpdateView();
    }

    protected override void OnShowScreen()
    {
        base.OnShowScreen();
        UpdateView();
    }

    private void UpdateView()
    {
        if (_battleRoundService.GetIsPlayerWin())
        {
            resultText.SetText("<color=green>WIN</color>");
        }
        else
        {
            resultText.SetText("<color=red>LOSE</color>");
        }
    }
}
