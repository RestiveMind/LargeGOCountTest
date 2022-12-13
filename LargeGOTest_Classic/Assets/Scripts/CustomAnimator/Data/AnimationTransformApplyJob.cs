using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace CustomAnimator
{
    [BurstCompile]
    public struct AnimationTransformApplyJob : IJobParallelForTransform
    {
        [ReadOnly] public NativeArray<float> Curves;
        [ReadOnly] public NativeArray<Vector2Int> CurvePositions;
        [ReadOnly] public int CurvesPerObject;
        
        public void Execute(int index, TransformAccess transform)
        {
            if (!transform.isValid)
            {
                return;
            }
            
            var curvePosIndex = index % CurvePositions.Length;
            var curveIndex = index / CurvePositions.Length * CurvesPerObject;
            curveIndex += CurvePositions[curvePosIndex].x;
            
            if ((CurvePositions[curvePosIndex].y & (int)TransformComponent.Position) != 0)
            {
                var pos = new Vector3(Curves[curveIndex++], Curves[curveIndex++], Curves[curveIndex++]);
                transform.localPosition = pos;
            }
            
            if ((CurvePositions[curvePosIndex].y & (int)TransformComponent.Rotation) != 0)
            {
                var rotation = new Quaternion(Curves[curveIndex++], Curves[curveIndex++], Curves[curveIndex++],
                    Curves[curveIndex++]);
                transform.localRotation = rotation;
            }
            
            if ((CurvePositions[curvePosIndex].y & (int)TransformComponent.Scale) != 0)
            {
                var scale = new Vector3(Curves[curveIndex++], Curves[curveIndex++], Curves[curveIndex]);
                transform.localScale = scale;
            }
        }
    }
}