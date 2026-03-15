using System;
using _Source.Code._AKFramework.AKPools.Runtime;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Source.Code.Objects
{
    [Serializable]
    public class ShapeParameterData:IMainParameter<AKPrefab>
    {
        [SerializeField][AKTagsGroup("Shape Type")] private AKTag tag;
        [SerializeField] private AKPrefab shapePrefab;
        [SerializeReference] private IValueParameter<float>[] valueParameters; 

        public AKTag GetParameterTag() => tag;
        public AKPrefab GetMainParameterValue() => shapePrefab;
        public IValueParameter<float>[] ValueParameters => valueParameters;

    }
}