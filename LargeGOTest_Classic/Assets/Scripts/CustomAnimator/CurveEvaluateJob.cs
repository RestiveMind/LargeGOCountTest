using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace CustomAnimator
{
    [BurstCompile]
    public struct CurveEvaluateJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<float> Curves; //evaluated curves 2160 with 10 value in each
        [ReadOnly] public NativeArray<float> Times; //10 objects (for ex)
        [ReadOnly] public int SamplesCount;
        [ReadOnly] public int CurvesCountPerObject;
        public NativeArray<float> Result;

        public void Execute(int i)
        {
            var timeIndex = i / CurvesCountPerObject;

            if (Times[timeIndex] < 0)
            {
                return;
            }
            
            var curveIndex = i % CurvesCountPerObject;
            
            var startIndex = curveIndex * SamplesCount;
            var len = SamplesCount - 1;
            var position = len * Times[timeIndex];
            var index = (int)position;
        
            if (index == len)
            {
                Result[i] = Curves[startIndex + index];
            }
        
            var localTime = position - index;

            Result[i] = Mathf.Lerp(Curves[startIndex + index], Curves[startIndex + index + 1], localTime);
        }
    }
}