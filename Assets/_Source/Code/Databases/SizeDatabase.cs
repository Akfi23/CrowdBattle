using _Source.Code.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Source.Code.Databases
{
    [CreateAssetMenu(fileName = "db_size", menuName = "Game/Databases/Size Database", order = 1)]
    public class SizeDatabase :AKDatabase
    {
        public override string Title => "Size Database";
        
        [TabGroup("Size", "Size", SdfIconType.BoundingBoxCircles,TextColor = "green")]
        [SerializeField] private SizeParameterData[] sizeParameterDatas;
        public SizeParameterData[] SizeParameterDatas => sizeParameterDatas;
    }
}