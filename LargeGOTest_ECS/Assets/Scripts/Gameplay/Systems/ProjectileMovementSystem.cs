using Gameplay.Components;
using Gameplay.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems
{
    public class ProjectileMovementSystem : IEcsRunSystem
    {
        private GameplayStaticData _gameplayData;
        private EcsFilter<Projectile> _filter;
        private EcsWorld _ecsWorld;

        private Vector3 _velocity;
        
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var projectile = ref _filter.Get1(i);

                projectile.Lifetime -= Time.deltaTime;
                
                if (projectile.Lifetime < 0)
                {
                    Object.Destroy(projectile.Transform.gameObject);
                    _filter.GetEntity(i).Destroy();
                    return;
                }

                if (projectile.TargetTransform)
                {
                    _velocity = (projectile.TargetTransform.position - projectile.Transform.position + Vector3.up).normalized * _gameplayData.projectileSpeed;    
                }
                
                projectile.Transform.Translate(_velocity * Time.deltaTime, Space.World);
            }
        }
    }
}