using System;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Interfaces;
using UnityEngine;

namespace _Source.Code.Objects
{
    [Serializable]
    public class ColorParameterData:IMainParameter<Material>
    {
        [SerializeField][AKTagsGroup("Color Type")] private AKTag tag;
        [SerializeField] private Material material;
        [SerializeReference] private IValueParameter<float>[] valueParameters; 

        public AKTag GetParameterTag() => tag;
        public Material GetMainParameterValue() => material;
        public IValueParameter<float>[] ValueParameters => valueParameters;

    }
}