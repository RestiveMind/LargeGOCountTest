using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CustomAnimator
{
    public class CustomAnimator : MonoBehaviour
    {
        private const int SamplesCount = 10;
        private const int ObjectsCurvesCache = 20000;
        
        private List<CustomAnimation> _animations;
        private int _curvesCountPerObject;

        private Dictionary<string, CustomAnimationData> _animationsData;

        private NativeArray<float> _animationKeySequence;
        private NativeArrayStream _resultStream;
        private NativeArray<float> _timesArray;
        private float[] _timesArrayManaged;
        private NativeArray<Vector2Int> _transformCurvesPositions;

        private TransformAccessArray _accessArray;

        private void Awake()
        {
            _accessArray = new TransformAccessArray(ObjectsCurvesCache);
            _animationsData = new Dictionary<string, CustomAnimationData>();
            
            CustomAnimation.AnimationObjectCreated += OnAnimationCreated;
            CustomAnimation.AnimationObjectDestroyed += OnAnimationDestroyed;
            _animations = new List<CustomAnimation>();
            
        }

        private void Update()
        {
            //var times = _animations.Select(t => t.Time).ToArray();
            //_timesArray.CopyFrom(times);
            var deltaTime = Time.deltaTime;
            var count = _animations.Count;
            for (var i = 0 ; i < count; i++)
            {
                var t = _timesArrayManaged[i];
                if (t > -1)
                {
                    t += deltaTime;
                    while(t > 1)
                    {
                        t -= 1;
                    }

                    _timesArrayManaged[i] = t; // = _animations[i].Time;    
                }
            }

            _timesArray.CopyFrom(_timesArrayManaged);
            RunEvaluationJob();
        }

        private void RunEvaluationJob()
        {
            if (!_animationKeySequence.IsCreated)
            {
                return;
            }
            
            var jobData = new CurveEvaluateJob
            {
                Curves = _animationKeySequence,
                Times = _timesArray,
                Result = _resultStream.TargetArray,
                CurvesCountPerObject = _curvesCountPerObject,
                SamplesCount = SamplesCount
            };

            JobHandle handle = jobData.Schedule(_animations.Count * _animationKeySequence.Length / SamplesCount, 1500);
            handle.Complete();

            ProcessResult();
        }

        private void ProcessResult()
        {
            _resultStream.Reset();
            
            var job = new AnimationTransformApplyJob
            {
                Curves = _resultStream.TargetArray,
                CurvePositions = _transformCurvesPositions,
                CurvesPerObject = _curvesCountPerObject
            };
            
            JobHandle jobHandle = job.Schedule(_accessArray);
            jobHandle.Complete();

            //DebugJobTransformApply();

            // for (var i = 0 ; i < _animations.Count; i++)
            // {
            //     var customAnimation = _animations[i];
            //     customAnimation.EvaluateState(_resultStream);
            // }
        }

        private void DebugJobTransformApply()
        {
            for (int i = 0; i < _accessArray.length; i++)
            {
                DebugTransformApply(i, _accessArray[i], _resultStream.TargetArray, _transformCurvesPositions, _curvesCountPerObject);
            }
        }

        private void DebugTransformApply(int index, Transform targetTransform, NativeArray<float> curves, NativeArray<Vector2Int> curvePositions, int curvesPerObject)
        {
            var curvePosIndex = index % curvePositions.Length;
            var curveIndex = index / curvePositions.Length * curvesPerObject;
            curveIndex += curvePositions[curvePosIndex].x;
            
            if ((curvePositions[curvePosIndex].y & (int)TransformComponent.Position) != 0)
            {
                var pos = new Vector3(curves[curveIndex++], curves[curveIndex++], curves[curveIndex++]);
                targetTransform.localPosition = pos;
            }
            
            if ((curvePositions[curvePosIndex].y & (int)TransformComponent.Rotation) != 0)
            {
                var rotation = new Quaternion(curves[curveIndex++], curves[curveIndex++], curves[curveIndex++],
                    curves[curveIndex++]);
                targetTransform.localRotation = rotation;
            }
            
            if ((curvePositions[curvePosIndex].y & (int)TransformComponent.Scale) != 0)
            {
                var scale = new Vector3(curves[curveIndex++], curves[curveIndex++], curves[curveIndex]);
                targetTransform.localScale = scale;
            }
        }
        
        private void OnAnimationDestroyed(CustomAnimation customAnimation)
        {
            int index = _animations.IndexOf(customAnimation);
            _animations[index].IsActive = false;
            _timesArrayManaged[index] = -1;
            _timesArray[index] = -1;

            var childTransformsCount = customAnimation.AnimatedTransforms.Count();
            var startIndex = index * childTransformsCount;
            var endIndex = startIndex + childTransformsCount;
            for (var i = startIndex; i < endIndex; i++)
            {
                _accessArray[i] = null;
            }
        }

        private void OnAnimationCreated(CustomAnimation customAnimation)
        {
            var animationName = customAnimation.AnimationName;
            if (!_animationsData.ContainsKey(animationName))
            {
                _animationsData[animationName] = CreateAnimationData(animationName);
            }
            
            customAnimation.SearchAnimatedTransforms(_animationsData[animationName]);

            var childTransformsCount = customAnimation.AnimatedTransforms.Count();
            if (!_animationKeySequence.IsCreated)
            {
                _animationKeySequence = customAnimation.Create(_animationsData[animationName], SamplesCount);
                _curvesCountPerObject = GetCurvesCount(_animationsData[animationName]);
                _resultStream = new NativeArrayStream(new NativeArray<float>(_curvesCountPerObject * ObjectsCurvesCache, Allocator.Persistent));
                _timesArray = new NativeArray<float>(ObjectsCurvesCache, Allocator.Persistent);
                _timesArrayManaged = new float[ObjectsCurvesCache];
                _transformCurvesPositions =
                    new NativeArray<Vector2Int>(childTransformsCount, Allocator.Persistent);
                
                int i = 0;
                int currentPos = 0;
                foreach (var animatedTransform in customAnimation.AnimatedTransforms)
                {
                    _transformCurvesPositions[i] =
                        new Vector2Int(currentPos, animatedTransform.Components);
                    
                    currentPos += animatedTransform.DataCapacity;
                    i++;
                }
            }

            var index = GetFreeIndex();
            _animations[index] = customAnimation;
            _timesArrayManaged[index] = 0;
            _timesArray[index] = 0;

            var accessArrayIndex = index * childTransformsCount;
            foreach (var animatedTransform in customAnimation.AnimatedTransforms)
            {
                if (accessArrayIndex >= _accessArray.length)
                {
                    _accessArray.Add(animatedTransform.Transform);
                }
                else
                {
                    _accessArray[accessArrayIndex] = animatedTransform.Transform;
                }

                accessArrayIndex++;
            }
        }

        private int GetFreeIndex()
        {
            for (var i = 0; i < _animations.Count; i++)
            {
                if (_animations[i] == null) return i;
            }
            _animations.Add(null);
            return _animations.Count - 1;
        }

        private int GetCurvesCount(CustomAnimationData data)
        {
            var curvesCount = data.AnimationClip.PositionCurves.Length * 3;
            curvesCount += data.AnimationClip.RotationCurves.Length * 4;
            curvesCount += data.AnimationClip.ScaleCurves.Length * 3;

            return curvesCount;
        }

        private CustomAnimationData CreateAnimationData(string animationName)
        {
            var myAnimation = (TextAsset)Resources.Load(animationName);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(NullNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<CustomAnimationData>(myAnimation.text);
        }
    }
}