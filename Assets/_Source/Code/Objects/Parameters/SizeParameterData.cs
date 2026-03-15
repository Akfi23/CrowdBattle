using System;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Interfaces;
using UnityEngine;

namespace _Source.Code.Objects
{
    [Serializable]
    public class SizeParameterData:IMainParameter<Vector3>
    {
        [SerializeField][AKTagsGroup("Size Type")] private AKTag tag;
        [SerializeField] private Vector3 size;
        [SerializeReference] private IValueParameter<float>[] valueParameters; 

        public AKTag GetParameterTag() => tag;
        public Vector3 GetMainParameterValue() => size;
        public IValueParameter<float>[] ValueParameters => valueParameters;
    }
}