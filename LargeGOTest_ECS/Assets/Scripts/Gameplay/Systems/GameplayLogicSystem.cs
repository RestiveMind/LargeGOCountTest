using System;
using Data;
using Gameplay.Components;
using Gameplay.Data;
using Leopotam.Ecs;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Gameplay.Systems
{
    public class GameplayLogicSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        
        private float _spawnTimer;
        private GameplayStaticData _gameplayData;
        private SceneData _sceneData;
        
        
        public void Init()
        {
            _spawnTimer = _gameplayData.spawnInterval;
            CreateTowerEntities();
        }

        private void CreateTowerEntities()
        {
            foreach (var towerShootPoint in _sceneData.towerShootPoints)
            {
                ref var tower = ref _ecsWorld.NewEntity().Get<Tower>();
                tower.ShootPoint = towerShootPoint.position;
                tower.ShootInterval = _gameplayData.towerFireRate;
            }
        }

        public void Run()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0 && Enemy.Count < _gameplayData.maxEnemiesCount)
            {
                int i = _gameplayData.spawnCountInTime;
                while (--i > -1)
                {
                    SpawnUnit();    
                }
                
                _spawnTimer += _gameplayData.spawnInterval;
            }
        }

        private void SpawnUnit()
        {
            ref var enemyComponent = ref _ecsWorld.NewEntity().Get<Enemy>();
            
            var randomAreaFrom = _sceneData.spawnAreas.GetRandom();
            var randomAreaTo = _sceneData.spawnAreas.GetRandomExcept(randomAreaFrom);

            var startPoint = randomAreaFrom.GetRandomPointInsideBounds();
            var endPoint = randomAreaTo.GetRandomPointInsideBounds();
            
            var enemyObject = Object.Instantiate(_gameplayData.enemyPrefab, startPoint, Quaternion.identity);
            
            enemyComponent.EnemyTransform = enemyObject.transform;
            enemyComponent.TargetPoint = endPoint;

            Enemy.Count++;
        }
    }
}