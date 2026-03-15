using System;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Interfaces;
using AKFramework.Generated;
using UnityEngine;

namespace _Source.Code.Objects.ValueParameters
{
    [Serializable]
    public class DamageValueParameter : IValueParameter<float>
    {
        [SerializeField]
        private float damageValue;
        
        private AKTag tag = AKTags.Parameters__Damage;
        
        public AKTag GetParameterTag() => tag;
        public float GetValueParameterValue() => damageValue;
        
    }
}