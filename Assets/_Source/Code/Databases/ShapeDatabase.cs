using _Source.Code.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Source.Code.Databases
{
    [CreateAssetMenu(fileName = "db_shape", menuName = "Game/Databases/Shape Database", order = 1)]
    public class ShapeDatabase:AKDatabase
    {
        public override string Title => "Shape Database";

        [TabGroup("Shape", "Shape", SdfIconType.CircleSquare,TextColor = "blue")]
        [SerializeField] private ShapeParameterData[] shapeParameterDatas;
        public ShapeParameterData[] ShapeParameterDatas => shapeParameterDatas;
    }
}