using System;
using _Source.Code._AKFramework.AKECS.Runtime;
using UnityEngine;

namespace _Source.Code.ECS.Components
{
    [Serializable]
    [AKGenerateProvider]
    public struct ShootFXRoot
    {
        public ParticleSystem fx;
    }
}