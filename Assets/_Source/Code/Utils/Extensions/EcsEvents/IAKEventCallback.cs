using _Source.Code._AKFramework.AKEvents.Runtime;

namespace _Source.Code.Utils.Extensions.EcsEvents
{
    public interface IAKEventCallback
    {
        AKEvent AKEvent { get; set; }
    }
}