using System;
using _Source.Code._AKFramework.AKECS.Runtime;

namespace _Source.Code.ECS.Components
{
    [Serializable]
    [AKGenerateProvider]
    public struct HealthRef
    {
        public float baseValue;
        public float value;
    }
}