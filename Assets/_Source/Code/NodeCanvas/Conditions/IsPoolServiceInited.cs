using System;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKNodeCanvas;
using _Source.Code._AKFramework.AKPools.Runtime;
using ParadoxNotion.Design;

[Category("AKFramework/Pools")]
[Name("Is Services Inited")]
[Serializable]
public class IsPoolServiceInited : AKConditionTask
{
    private IAKPoolsService _poolService;

    protected override void Init(IAKContainer injectionContainer)
    {
        _poolService = injectionContainer.Resolve<IAKPoolsService>();
    }
    

    protected override bool OnCheck()
    {
        if (_poolService == null) return false;
        return _poolService.IsInitialized;
    }

    protected override string info => $"Is services inited";
}