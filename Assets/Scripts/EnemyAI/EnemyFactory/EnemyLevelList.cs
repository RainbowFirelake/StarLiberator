using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace StarLiberator.Enemies
{
    [CreateAssetMenu()]
    public class EnemyLevelList : SerializedScriptableObject
    {
        [DictionaryDrawerSettings(KeyLabel = "Level", ValueLabel = "Preset")]
        public Dictionary<int, List<EnemyLevelInfo>> LevelLists = new Dictionary<int, List<EnemyLevelInfo>>();

        [ShowInInspector]
        public int CurrentMaxLevel
        {
            get { return this._maxLevel; }
        }
        private int _maxLevel = 0;

        private void OnValidate()
        {
            _maxLevel = LevelLists.Count;
        }

        public EnemyController GetRandomEnemyAtLevel(int level)
        {
            if (level > _maxLevel)
            {
                level = _maxLevel - 1;
            }

            var levelList = LevelLists[level];
            return levelList[Random.Range(0, levelList.Count)].Enemy;
        }

        public List<EnemyLevelInfo> GetInfoByLevel(int level)
        {
            if (level > _maxLevel) return LevelLists[_maxLevel - 1];

            return LevelLists[level];
        }
    }
}