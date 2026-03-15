using System.Collections.Generic;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKPools.Runtime;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Databases;
using _Source.Code.Objects;
using UnityEngine;

namespace _Source.Code.Services
{
    public class UnitShapeService : IAKService
    {
        [AKInject] private ShapeDatabase _database;
        
        private Dictionary<AKTag, ShapeParameterData> _parametersMapping = new Dictionary<AKTag, ShapeParameterData>();
        private List<AKTag> keysArray = new();
        
        [AKInject]
        private void Init()
        {
            foreach (var data in _database.ShapeParameterDatas)
            {
                _parametersMapping.Add(data.GetParameterTag(),data);
                keysArray.Add(data.GetParameterTag());
            }
        }

        public ShapeParameterData GetRandomData() => _parametersMapping[keysArray[Random.Range(0, keysArray.Count)]];
        public AKPrefab GetShapeByTag(AKTag tag) => _parametersMapping[tag].GetMainParameterValue();
    }
}