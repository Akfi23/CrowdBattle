using System;
using _Source.Code._AKFramework.AKECS.Runtime;
using UnityEngine;

namespace _Source.Code.ECS.Components
{
    [AKGenerateProvider]
    [Serializable]
    public struct AnimatorRef
    {
        public Animator instance;
    }
}