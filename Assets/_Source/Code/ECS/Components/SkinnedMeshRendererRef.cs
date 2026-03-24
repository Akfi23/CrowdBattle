using System;
using _Source.Code._AKFramework.AKECS.Runtime;
using UnityEngine;

namespace _Source.Code.ECS.Components
{
    [AKGenerateProvider]
    [Serializable]
    public struct SkinnedMeshRendererRef
    {
        public SkinnedMeshRenderer instance;
    }
}