using System;
using _Source.Code._AKFramework.AKECS.Runtime;
using _Source.Code._AKFramework.AKPools.Runtime;
using UnityEngine;

namespace _Source.Code.ECS.Components
{
    [AKGenerateProvider]
    [Serializable]
    public struct SpawnArea
    {
        public Vector3 size;
        public AKPrefab prefabToSpawn;
    }
}