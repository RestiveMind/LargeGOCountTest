using System;
using UnityEngine;

namespace CustomAnimator
{
    [Flags]
    public enum TransformComponent
    {
        None = 0,
        Rotation = 1,
        Position = 2,
        Scale = 4
    }
    
    public struct AnimatedTransform
    {
        private readonly Transform _transform;
        private TransformComponent _components;
        private int _dataCapacity;
        
        public int PositionCurveIndex { get; set; }
        public int RotationCurveIndex { get; set; }
        public int ScaleCurveIndex { get; set; }

        public int DataCapacity => _dataCapacity;
        public int Components => (int)_components;

        public Transform Transform => _transform;
        
        public AnimatedTransform(Transform transform)
        {
            _transform = transform;
            _components = TransformComponent.None;
            _dataCapacity = 0;
            PositionCurveIndex = 0;
            RotationCurveIndex = 0;
            ScaleCurveIndex = 0;
        }

        public void SetComponent(TransformComponent component)
        {
            switch (component)
            {
                case TransformComponent.Position:
                case TransformComponent.Scale:
                    _dataCapacity += 3;
                    break;
                case TransformComponent.Rotation:
                    _dataCapacity += 4;
                    break;
            }
            _components |= component;
        }

        public bool IsComponentSet(TransformComponent component)
        {
            return (_components & component) != TransformComponent.None;
        }

        public void UpdateFrom(NativeArrayStream stream)
        {
            if ((_components & TransformComponent.Position) != TransformComponent.None)
            {
                _transform.localPosition = new Vector3(stream.Read(), stream.Read(), stream.Read());
            }
            
            if ((_components & TransformComponent.Rotation) != TransformComponent.None)
            {
                _transform.localRotation = new Quaternion(stream.Read(), stream.Read(), stream.Read(), stream.Read());
            }
            
            if ((_components & TransformComponent.Scale) != TransformComponent.None)
            {
                _transform.localScale = new Vector3(stream.Read(), stream.Read(), stream.Read());
            }
        }
    }
}