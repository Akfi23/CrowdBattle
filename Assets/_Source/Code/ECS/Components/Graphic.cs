using System;
using _Source.Code._AKFramework.AKECS.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Source.Code.ECS.Components
{
    [AKGenerateProvider]
    [Serializable]
    public struct Graphic
    {
        public Transform root;
        [HideInInspector]
        public GameObject instance;
    }
}