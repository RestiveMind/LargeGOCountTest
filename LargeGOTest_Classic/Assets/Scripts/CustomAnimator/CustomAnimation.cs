using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace CustomAnimator
{
    public class CustomAnimation : MonoBehaviour
    {
        public static event Action<CustomAnimation> AnimationObjectCreated;
        public static event Action<CustomAnimation> AnimationObjectDestroyed;
        
        private Transform _rootTransform;
        private float _t;
        private Dictionary<string, AnimatedTransform> _animatedTransforms;

        public string AnimationName => "run";
        
        public float Time => IsActive ? _t : -1;

        public IEnumerable<AnimatedTransform> AnimatedTransforms => _animatedTransforms.Select(t => t.Value);

        public bool IsActive { get; set; }

        private void Awake()
        {
            IsActive = true;
            AnimationObjectCreated?.Invoke(this);
        }

        public void SearchAnimatedTransforms(CustomAnimationData animationData)
        {
            _rootTransform = transform;
            _animatedTransforms = new Dictionary<string, AnimatedTransform>();

            var animationClip = animationData.AnimationClip;
            
            int i = animationClip.PositionCurves.Length;
            while (--i > -1)
            {
                InitAnimatedTransform(animationClip.PositionCurves[i], TransformComponent.Position, i);
            }
            
            i = animationClip.RotationCurves.Length;
            while (--i > -1)
            {
                InitAnimatedTransform(animationClip.RotationCurves[i], TransformComponent.Rotation, i);
            }
            
            i = animationClip.ScaleCurves.Length;
            while (--i > -1)
            {
                InitAnimatedTransform(animationClip.ScaleCurves[i], TransformComponent.Scale, i);
            }
        }

        private void InitAnimatedTransform(CurveData data, TransformComponent component, int dataIndex)
        {
            GetPathAndTarget(data, out var path, out var target);
            if (!_animatedTransforms.ContainsKey(path))
            {
                _animatedTransforms[path] = new AnimatedTransform(target);
            }

            var animatedTransform = _animatedTransforms[path];
            animatedTransform.SetComponent(component);

            switch (component)
            {
                case TransformComponent.Position:
                    animatedTransform.PositionCurveIndex = dataIndex;
                    break;
                case TransformComponent.Rotation:
                    animatedTransform.RotationCurveIndex = dataIndex;
                    break;
                case TransformComponent.Scale:
                    animatedTransform.ScaleCurveIndex = dataIndex;
                    break;
            }
            
            _animatedTransforms[path] = animatedTransform;
        }
        
        private void GetPathAndTarget(CurveData curveData, out string path, out Transform target)
        {
            path = string.IsNullOrEmpty(curveData.Path) ? "root" : curveData.Path;
            target = string.IsNullOrEmpty(curveData.Path) ? _rootTransform : _rootTransform.Find(curveData.Path);
        }

        public NativeArray<float> Create(CustomAnimationData data, int samplesCount)
        {
            var curvesCount = data.AnimationClip.PositionCurves.Length * 3;
            curvesCount += data.AnimationClip.RotationCurves.Length * 4;
            curvesCount += data.AnimationClip.ScaleCurves.Length * 3;

            var arrayLen = curvesCount * samplesCount;
            var animationKeySequence = new NativeArray<float>(arrayLen, Allocator.Persistent);
            var stream = new NativeArrayStream(animationKeySequence);

            foreach (var animatedTransformKeyValue in _animatedTransforms)
            {
                var animatedTransform = animatedTransformKeyValue.Value;
                if (animatedTransform.IsComponentSet(TransformComponent.Position))
                {
                    data.AnimationClip.PositionCurves[animatedTransform.PositionCurveIndex].CurveClass.KeyFrames
                        .WriteVector3Samples(samplesCount, stream);
                }

                if (animatedTransform.IsComponentSet(TransformComponent.Rotation))
                {
                    data.AnimationClip.RotationCurves[animatedTransform.RotationCurveIndex].CurveClass.KeyFrames
                        .WriteVector4Samples(samplesCount, stream);
                }

                if (animatedTransform.IsComponentSet(TransformComponent.Scale))
                {
                    data.AnimationClip.ScaleCurves[animatedTransform.ScaleCurveIndex].CurveClass.KeyFrames
                        .WriteVector3Samples(samplesCount, stream);
                }
            }

            return animationKeySequence;
        }

        private void Update()
        {
            // _t += UnityEngine.Time.deltaTime;
            // while(_t > 1)
            // {
            //     _t -= 1;
            // }
        }

        public void EvaluateState(NativeArrayStream stream)
        {
            foreach (var animatedTransformKeyValue in _animatedTransforms)
            {
                var animatedTransform = animatedTransformKeyValue.Value;
                animatedTransform.UpdateFrom(stream);
            }
        }

        private void OnDestroy()
        {
            AnimationObjectDestroyed?.Invoke(this);
        }
    }
}