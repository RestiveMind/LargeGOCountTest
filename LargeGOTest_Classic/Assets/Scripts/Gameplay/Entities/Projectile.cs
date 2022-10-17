using System;
using UnityEngine;

namespace Gameplay.Entities
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Enemy _target;
        private SphereCollider _collider;
        private LayerMask _layerMask;
        private Collider[] _collidedResults;
        private Vector3 _velocity;
        private float _lifetime = 10;
        
        public void Init(Enemy target)
        {
            _target = target;
            _collider = GetComponent<SphereCollider>();
            _layerMask = LayerMask.GetMask("Enemy");
            _collidedResults = new Collider[5];
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime < 0)
            {
                Destroy(gameObject);
                return;
            }
            
            if (_target)
            {
                _velocity = (_target.transform.position - transform.position + Vector3.up).normalized * _speed;    
            }
            
            transform.Translate(_velocity * Time.deltaTime, Space.World);

            var radius = _collider.radius * transform.localScale.x;
            var count = Physics.OverlapSphereNonAlloc(transform.position, radius, _collidedResults, _layerMask);
            if (count > 0)
            {
                int i = Math.Min(count, _collidedResults.Length);
                while (--i > -1)
                {
                    var enemy = _collidedResults[i].GetComponentInParent<Enemy>();
                    enemy.Destroy();
                }
                Destroy(gameObject);
            }
        }
    }
}