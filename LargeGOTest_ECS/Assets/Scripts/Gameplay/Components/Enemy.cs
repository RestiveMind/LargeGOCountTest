using UnityEngine;

namespace Gameplay.Components
{
    public struct Enemy
    {
        public static int Count;
        
        public Transform EnemyTransform;
        public Vector3 TargetPoint;
        public Vector3 Velocity;
    }
}