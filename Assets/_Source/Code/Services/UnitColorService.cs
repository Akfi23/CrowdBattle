using System.Collections.Generic;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Databases;
using _Source.Code.Objects;
using UnityEngine;

namespace _Source.Code.Services
{
    public class UnitColorService : IAKService
    {
        [AKInject] private ColorDatabase _database;

        private Dictionary<AKTag, ColorParameterData> _parametersMapping = new Dictionary<AKTag, ColorParameterData>();
        private List<AKTag> keysArray = new();
        
        [AKInject]
        private void Init()
        {
            foreach (var data in _database.ColorParameterDatas)
            {
                _parametersMapping.Add(data.GetParameterTag(),data);
                keysArray.Add(data.GetParameterTag());
            }
            
        }

        public ColorParameterData GetRandomData() => _parametersMapping[keysArray[Random.Range(0, keysArray.Count)]];
        public Material GetMaterialByTag(AKTag tag) => _parametersMapping[tag].GetMainParameterValue();
    }
}