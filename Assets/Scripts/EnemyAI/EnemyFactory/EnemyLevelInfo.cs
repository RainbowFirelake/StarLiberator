using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StarLiberator.Enemies
{
    [System.Serializable]
    public class EnemyLevelInfo
    {
        [field: SerializeField]
        public EnemyController Enemy { get; private set; }


        [field: SerializeField]
        public int MinCount { get; private set; }

        [field: SerializeField]
        public int MaxCount { get; private set; }
    }
}

