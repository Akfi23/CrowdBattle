using System;
using _Source.Code._AKFramework.AKPools.Runtime;
using _Source.Code._AKFramework.AKTags.Runtime;
using UnityEngine;

namespace _Source.Code.Objects
{
    [Serializable]
    public struct UnitBaseData
    {
        [SerializeField] private AKTag unitTag;
        [SerializeField] private AKPrefab prefab;
        [SerializeField] private float health;
        [SerializeField] private float damage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float moveSpeed;

        public AKTag UnitTag => unitTag;
        public AKPrefab Prefab => prefab;
        public float Health => health;
        public float Damage => damage;
        public float AttackSpeed => attackSpeed;
        public float MoveSpeed => moveSpeed;
    }
}