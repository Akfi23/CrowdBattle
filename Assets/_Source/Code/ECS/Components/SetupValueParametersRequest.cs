using _Client_.Scripts.Interfaces;
using _Source.Code.Interfaces;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Components
{
    public struct SetupValueParametersRequest:IAKEcsRequest
    {
        public EcsPackedEntity TargetPackedEntity { get; set; }
        public IValueParameter<float>[] ValueParameters;
    }
}