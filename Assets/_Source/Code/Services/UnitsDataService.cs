using System.Collections.Generic;
using _Source.Code._AKFramework.AKCore.Runtime;
using _Source.Code._AKFramework.AKTags.Runtime;
using _Source.Code.Databases;
using _Source.Code.Interfaces;

namespace _Source.Code.Services
{
    public class UnitsDataService : IAKService
    {
        [AKInject] private UnitsDatabase _database;
        
        private Dictionary<AKTag, IValueParameter<float>> _dataMapping = new Dictionary<AKTag, IValueParameter<float>>();

        [AKInject]
        private void Init()
        {
            foreach (var data in _database.UnitsInitialValueParameters)
            {
                _dataMapping.Add(data.GetParameterTag(),data);
            }
        }

        public IValueParameter<float> GetInitialDataByTag(AKTag tag) => _dataMapping[tag];
        public Dictionary<AKTag, IValueParameter<float>> GetInitialParameters => _dataMapping;
    }
}