using System;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Interfaces;
using AKFramework.Generated;
using UnityEngine;

namespace _Source.Code.Objects.ValueParameters
{
    [Serializable]
    public class AttackSpeedValueParameter:IValueParameter<float>
    {
        [SerializeField]
        private float attackSpeedValue;
        
        private AKTag tag = AKTags.Parameters__AttackSpeed;
        
        public AKTag GetParameterTag() => tag;
        public float GetValueParameterValue() => attackSpeedValue;
        
    }
}