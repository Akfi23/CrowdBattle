using System;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Interfaces;
using AKFramework.Generated;
using UnityEngine;

namespace _Source.Code.Objects.ValueParameters
{
    [Serializable]
    public class MoveSpeedValueParameter : IValueParameter<float>
    {
        [SerializeField]
        private float moveSpeedValue;
        
        private AKTag tag = AKTags.Parameters__MoveSpeed;
        
        public AKTag GetParameterTag() => tag;
        public float GetValueParameterValue() => moveSpeedValue;

    }
}