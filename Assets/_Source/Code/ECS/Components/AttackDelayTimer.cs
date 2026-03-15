using System;
using _Source.Code._AKFramework.AKECS.Runtime;

namespace _Source.Code.ECS.Components
{
    [Serializable]
    public struct AttackDelayTimer : IAKEcsTimer
    {
        public float Timer { get; set; }
    }
}