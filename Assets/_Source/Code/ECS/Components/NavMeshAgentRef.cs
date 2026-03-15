using System;
using _Source.Code._AKFramework.AKECS.Runtime;
using UnityEngine.AI;

namespace _Source.Code.ECS.Components
{
    [Serializable]
    [AKGenerateProvider]
    public struct NavMeshAgentRef
    {
        public NavMeshAgent instance;
    }
}