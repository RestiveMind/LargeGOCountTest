using Data;
using Gameplay.Data;
using Gameplay.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameplayStaticData configuration;
        [SerializeField] private SceneData sceneData;
    
        private EcsWorld _ecsWorld;
        private EcsSystems _updateSystems;

        private void Start()
        {
            _ecsWorld = new EcsWorld();
            _updateSystems = new EcsSystems(_ecsWorld);
        
            _updateSystems.Add(new DetectAndShootSystem())
                .Add(new EnemyMovementSystem())
                .Add(new GameplayLogicSystem())
                .Add(new ProjectileDamageSystem())
                .Add(new ProjectileMovementSystem())
                .Inject(configuration)
                .Inject(sceneData)
                .Init();

        }

        private void Update()
        {
            _updateSystems.Run();
        }

        private void OnDestroy()
        {
            _updateSystems.Destroy();
            _ecsWorld.Destroy();
        }
    }
}
