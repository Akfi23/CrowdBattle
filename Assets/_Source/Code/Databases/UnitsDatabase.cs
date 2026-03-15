using _Source.Code.Interfaces;
using _Source.Code.Objects;
using UnityEngine;

namespace _Source.Code.Databases
{
    [CreateAssetMenu(fileName = "db_units", menuName = "Game/Databases/Units Database", order = 1)]
    public class UnitsDatabase:AKDatabase
    {
        public override string Title => "Units";

        [SerializeReference] private IValueParameter<float>[] unitsInitialValueParameters;
        public IValueParameter<float>[] UnitsInitialValueParameters => unitsInitialValueParameters;
    }
}