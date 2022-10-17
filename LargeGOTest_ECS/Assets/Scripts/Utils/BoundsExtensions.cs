using UnityEngine;

namespace Utils
{
    public static class BoundsExtensions
    {
        public static Vector3 GetRandomPointInsideBounds(this Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                0,
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}