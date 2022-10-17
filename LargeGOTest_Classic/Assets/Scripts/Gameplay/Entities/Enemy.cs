using UnityEngine;

namespace Gameplay.Entities
{
    public class Enemy : MonoBehaviour
    {
        public static int Count;
        
        [SerializeField] private float _speed;
        
        private Vector3 _velocity;

        private Bounds _worldBounds;
        
        public void Initialize(Vector3 targetPoint, Bounds worldBounds)
        {
            Count++;
            _worldBounds = worldBounds;
            _velocity = (targetPoint - transform.position).normalized * _speed;
            transform.rotation = Quaternion.LookRotation(_velocity, Vector3.up);
        }

        private void Update()
        {
            transform.Translate(_velocity * Time.deltaTime, Space.World);
            if (!_worldBounds.Contains(transform.position))
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            Count--;
            Destroy(gameObject);
        }
    }
}
