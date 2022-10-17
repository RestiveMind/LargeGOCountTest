using UnityEngine;

namespace Gameplay.Entities
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float _range;

        [SerializeField] private float _fireRate;

        [SerializeField] private Projectile _projectilePrefab;

        [SerializeField] private Transform _projectileSpanwPoint;

        private float _shootInterval;

        private Collider[] _colliderResults;
        private int _layerMask;

        private void Start()
        {
            _layerMask = LayerMask.GetMask("Enemy");

            _colliderResults = new Collider[1];
            _shootInterval = _fireRate;
        }
        
        private void Update()
        {
            _shootInterval -= Time.deltaTime;
            while (_shootInterval <= 0)
            {
                Shoot();
                _shootInterval += _fireRate;
            }
        }

        private void Shoot()
        {
            var shootingPoint = _projectileSpanwPoint.position;
            var count = Physics.OverlapSphereNonAlloc(shootingPoint, _range, _colliderResults, _layerMask);
            if (count > 0)
            {
                
                var enemy = _colliderResults[0].GetComponentInParent<Enemy>();
                var enemyDirection = enemy.transform.position - shootingPoint;
                var projectileSpawnPoint = enemyDirection.normalized * 0.5f + shootingPoint;
                var projectile = Instantiate(_projectilePrefab, projectileSpawnPoint, Quaternion.identity);
                projectile.Init(enemy);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0f, 0.24f, 1f, 0.5f);
            Gizmos.DrawSphere(transform.position, _range);
        }
    }
}

