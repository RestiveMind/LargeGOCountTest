using UnityEngine;

namespace Gameplay
{
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField]
        private Bounds _spawnBounds;

        public Vector3 GetRandomPointInsideArea()
        {
            var transformPos = transform.position;
            return new Vector3(
                Random.Range(_spawnBounds.min.x, _spawnBounds.max.x) + transformPos.x,
                0,
                Random.Range(_spawnBounds.min.z, _spawnBounds.max.z) + transformPos.z
                );
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.41f, 0.58f, 0.27f, 0.43f);
            Gizmos.DrawCube(_spawnBounds.center + transform.position, _spawnBounds.size);
        }
    }
}