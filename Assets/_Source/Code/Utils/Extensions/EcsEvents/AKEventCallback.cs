using _Source.Code._AKFramework.AKEvents.Runtime;

namespace _Source.Code.Utils.Extensions.EcsEvents
{
    public struct AKEventCallback : IAKEventCallback
    {
        public AKEvent AKEvent { get; set; }
        
        public AKEventCallback(AKEvent akEvent)
        {
            AKEvent = akEvent;
        }
    }
    
    public struct AKEventCallback<T1> : IAKEventCallback
    {
        public T1 Arg1;
        public AKEvent AKEvent { get; set; }
        
        public AKEventCallback(AKEvent akEvent, T1 arg1)
        {
            AKEvent = akEvent;
            Arg1 = arg1;
        }
    }
    
    public struct AKEventCallback<T1, T2> : IAKEventCallback
    {
        public T1 Arg1;
        public T2 Arg2;
        public AKEvent AKEvent { get; set; }
        
        public AKEventCallback(AKEvent akEvent, T1 arg1, T2 arg2)
        {
            AKEvent = akEvent;
            Arg1 = arg1;
            Arg2 = arg2;
        }
    }
    
    public struct AKEventCallback<T1, T2, T3> : IAKEventCallback
    {
        public T1 Arg1;
        public T2 Arg2;
        public T3 Arg3;
        public AKEvent AKEvent { get; set; }
        
        public AKEventCallback(AKEvent akEvent, T1 arg1, T2 arg2, T3 arg3)
        {
            AKEvent = akEvent;
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
        }
    }
    
    public struct AKEventCallback<T1, T2, T3, T4> : IAKEventCallback
    {
        public T1 Arg1;
        public T2 Arg2;
        public T3 Arg3;
        public T4 Arg4;
        public AKEvent AKEvent { get; set; }
        
        public AKEventCallback(AKEvent akEvent, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            AKEvent = akEvent;
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
            Arg4 = arg4;
        }
    }
}