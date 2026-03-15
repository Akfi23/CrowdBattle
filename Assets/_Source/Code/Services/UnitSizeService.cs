using System.Collections.Generic;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Databases;
using _Source.Code.Objects;
using UnityEngine;

namespace _Source.Code.Services
{
    public class UnitSizeService : IAKService
    {
        [AKInject] private SizeDatabase _database;
        
        private Dictionary<AKTag, SizeParameterData> _parametersMapping = new Dictionary<AKTag, SizeParameterData>();
        private List<AKTag> keysArray = new();
        
        [AKInject]
        private void Init()
        {
            foreach (var data in _database.SizeParameterDatas)
            {
                _parametersMapping.Add(data.GetParameterTag(),data);
                keysArray.Add(data.GetParameterTag());
            }
        }

        public SizeParameterData GetRandomData() => _parametersMapping[keysArray[Random.Range(0, keysArray.Count)]];
    }
}