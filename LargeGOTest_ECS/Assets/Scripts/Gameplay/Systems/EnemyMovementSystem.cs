using Data;
using Gameplay.Components;
using Gameplay.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy> _filter;

        private SceneData _sceneData;
        private GameplayStaticData _gameplayData;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var enemy = ref _filter.Get1(i);

                if (enemy.EnemyTransform == null)
                {
                    _filter.GetEntity(i).Destroy();
                    continue;
                }
                
                if (enemy.Velocity.Equals(Vector3.zero))
                {
                    enemy.Velocity = (enemy.TargetPoint - enemy.EnemyTransform.position).normalized * _gameplayData.enemySpeed;
                    enemy.EnemyTransform.rotation = Quaternion.LookRotation(enemy.Velocity, Vector3.up);
                }
                
                enemy.EnemyTransform.Translate(enemy.Velocity * Time.deltaTime, Space.World);
                if (!_sceneData.worldBounds.Contains(enemy.EnemyTransform.position))
                {
                    Object.Destroy(enemy.EnemyTransform.gameObject);
                    _filter.GetEntity(i).Destroy();
                    Enemy.Count--;
                }
            }
        }
    }
}