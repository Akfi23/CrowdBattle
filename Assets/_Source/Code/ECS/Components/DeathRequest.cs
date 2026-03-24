using System;
using _Source.Code._AKFramework.AKECS.Runtime;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Components
{
    [Serializable]
    public struct DeathRequest : IAKEcsRequest
    {
        public EcsPackedEntity TargetPackedEntity { get; set; }
    }
}