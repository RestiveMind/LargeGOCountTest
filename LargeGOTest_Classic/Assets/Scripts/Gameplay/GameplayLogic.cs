using CustomAnimator;
using Gameplay.Entities;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class GameplayLogic : MonoBehaviour
    {
        [SerializeField] private SpawnArea[] _spawnAreas;

        [SerializeField] private float _firstSpawnDelay;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private int _spawnCountInTime;
        [SerializeField] private int _maxEnemiesCount;

        [SerializeField] private Enemy _unitPrefab;

        [SerializeField] private Bounds _worldBounds;

        [SerializeField] private CustomAnimation _sphereAnimation;

        private float _spawnTimer;

        private void Start()
        {
            _spawnTimer = _firstSpawnDelay;
        }


        private void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0 && Enemy.Count < _maxEnemiesCount)
            {
                int i = _spawnCountInTime;
                while (--i > -1)
                {
                    SpawnUnit();    
                }
                
                _spawnTimer += _spawnInterval;
            }
        }

        private void SpawnUnit()
        {
            var randomAreaFrom = _spawnAreas.GetRandom();

            var startPoint = randomAreaFrom.GetRandomPointInsideArea();

            var randomAreaTo = _spawnAreas.GetRandomExcept(randomAreaFrom);

            var endPoint = randomAreaTo.GetRandomPointInsideArea();

            var unit = Instantiate(_unitPrefab, startPoint, Quaternion.identity);
            unit.Initialize(endPoint, _worldBounds);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0.69f, 0f, 0.5f);
            Gizmos.DrawCube(_worldBounds.center + transform.position, _worldBounds.size);
        }
    }
}
