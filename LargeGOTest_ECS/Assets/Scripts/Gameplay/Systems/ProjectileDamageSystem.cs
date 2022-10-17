using System;
using Gameplay.Components;
using Gameplay.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems
{
    public class ProjectileDamageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameplayStaticData _gameplayData;
        
        private LayerMask _layerMask;
        private Collider[] _collidedResults;
        
        private EcsFilter<Projectile> _filter;
        
        public void Init()
        {
            _collidedResults = new Collider[5];
            _layerMask = LayerMask.GetMask("Enemy");
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var projectile = ref _filter.Get1(i);

                var radius = _gameplayData.projectileColliderRadius * projectile.Transform.localScale.x;
                var count = Physics.OverlapSphereNonAlloc(projectile.Transform.position, radius, _collidedResults, _layerMask);
                if (count > 0)
                {
                    int index = Math.Min(count, _collidedResults.Length);
                    while (--index > -1)
                    {
                        var enemyGo = _collidedResults[index].transform.parent.gameObject;
                        UnityEngine.Object.Destroy(enemyGo);
                        Enemy.Count--;
                    }
                    UnityEngine.Object.Destroy(projectile.Transform.gameObject);
                    _filter.GetEntity(i).Destroy();
                }
            }
        }
    }
}