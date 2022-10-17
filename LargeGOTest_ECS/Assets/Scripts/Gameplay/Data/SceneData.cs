using UnityEngine;

namespace Data
{
    public class SceneData : MonoBehaviour
    {
        public Bounds worldBounds;
        public Bounds[] spawnAreas;
        
        public Transform[] towerShootPoints;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.41f, 0.58f, 0.27f, 0.43f);
            
            foreach (var spawnArea in spawnAreas)
            {
                Gizmos.DrawCube(spawnArea.center, spawnArea.size);    
            }
            
            Gizmos.color = new Color(1f, 0.69f, 0f, 0.5f);
            Gizmos.DrawCube(worldBounds.center, worldBounds.size);
        }
    }
}