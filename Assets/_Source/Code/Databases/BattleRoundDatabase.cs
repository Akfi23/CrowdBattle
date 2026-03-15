using UnityEngine;

namespace _Source.Code.Databases
{
    [CreateAssetMenu(fileName = "db_battle_round_setings", menuName = "Game/Databases/Battle Round Settings", order = 1)]
    public class BattleRoundDatabase : AKDatabase
    {
        public override string Title => "Battle Round Settings";

        [SerializeField][Range(1,200)] private int unitsCount;
        public int UnitsCount => unitsCount;
    }
}