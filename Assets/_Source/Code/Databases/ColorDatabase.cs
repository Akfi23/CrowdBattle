using _Source.Code.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Source.Code.Databases
{
    [CreateAssetMenu(fileName = "db_color", menuName = "Game/Databases/Color Database", order = 1)]
    public class ColorDatabase :AKDatabase
    {
        public override string Title => "Color Database";

        [TabGroup("Color", "Color", SdfIconType.BrushFill,TextColor = "yellow")]
        [SerializeField] private ColorParameterData[] colorParameterDatas;
        public ColorParameterData[] ColorParameterDatas => colorParameterDatas;
    }
}