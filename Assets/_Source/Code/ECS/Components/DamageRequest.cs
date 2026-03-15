using _Source.Code._AKFramework.AKECS.Runtime;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Components
{
    public struct DamageRequest : IAKEcsRequest
    {
        public float value;
        public EcsPackedEntity OwnerPackedEntity;
        public EcsPackedEntity TargetPackedEntity { get; set; }
    }
}