using System;
using _Client_.Scripts.Interfaces;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Components
{
    [Serializable]
    public struct DestroyRequest : IAKEcsRequest
    {
        public EcsPackedEntity TargetPackedEntity { get; set; }
        public float Delay;
    }
}