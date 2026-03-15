using System;
using _Source.Code._AKFramework.AKPools.Runtime;

namespace _Source.Code.ECS.Components
{
    [Serializable]
    public struct DestroySelfRequest : IAKPoolRemove
    {
        public float Delay;
    }
}