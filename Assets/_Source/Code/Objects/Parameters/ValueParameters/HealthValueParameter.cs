using System;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Interfaces;
using AKFramework.Generated;
using UnityEngine;

namespace _Source.Code.Objects.ValueParameters
{
    [Serializable]
    public class HealthValueParameter:IValueParameter<float>
    {
        [SerializeField]
        private float healthValue;
        
        private AKTag tag = AKTags.Parameters__Health;


        public AKTag GetParameterTag() => tag;
        public float GetValueParameterValue() => healthValue;
        
    }
}