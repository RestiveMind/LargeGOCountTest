using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu]
    public class GameplayStaticData : ScriptableObject
    {
        public GameObject enemyPrefab;
        public GameObject projectilePrefab;

        public float spawnInterval;
        public int spawnCountInTime;
        public int maxEnemiesCount;
        public int enemySpeed;
        public int projectileSpeed;
        public float projectileColliderRadius;
        
        public float towerShootingRange;
        public float towerFireRate;
        
    }
}