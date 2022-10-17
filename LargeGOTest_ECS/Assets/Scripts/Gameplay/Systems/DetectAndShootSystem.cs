using Gameplay.Components;
using Gameplay.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems
{
    public class DetectAndShootSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<Tower> _filter;
        private GameplayStaticData _gameplayData;
        
        private Collider[] _colliderResults;
        private int _layerMask;
        
        public void Init()
        {
            _layerMask = LayerMask.GetMask("Enemy");
            _colliderResults = new Collider[1];
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var tower = ref _filter.Get1(i);

                tower.ShootInterval -= Time.deltaTime;
                while (tower.ShootInterval <= 0)
                {
                    Shoot(ref tower);
                    tower.ShootInterval += _gameplayData.towerFireRate;
                }
            }
        }

        private void Shoot(ref Tower tower)
        {
            var shootingPoint = tower.ShootPoint;
            var count = Physics.OverlapSphereNonAlloc(shootingPoint, _gameplayData.towerShootingRange, _colliderResults, _layerMask);
            if (count > 0)
            {
                var targetTransform = _colliderResults[0].transform.parent;
                var enemyDirection = targetTransform.position - shootingPoint;
                var projectileSpawnPoint = enemyDirection.normalized * 0.5f + shootingPoint;
                var projectile = Object.Instantiate(_gameplayData.projectilePrefab, projectileSpawnPoint, Quaternion.identity);
                
                ref var projectileEntity = ref _ecsWorld.NewEntity().Get<Projectile>();
                projectileEntity.Transform = projectile.transform;
                projectileEntity.TargetTransform = targetTransform;
                projectileEntity.Lifetime = 10;
            }
        }

        
    }
}