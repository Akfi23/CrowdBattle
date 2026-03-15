using System;
using _Source.Code._AKFramework.AKPools.Runtime;
using Leopotam.EcsLite;

namespace _Source.Code.ECS.Components
{
    [Serializable]
    public struct AttackTarget : IAKPoolRemove
    {
        public EcsPackedEntity target;
    }
}