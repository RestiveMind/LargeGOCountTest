using System;
using Unity.Collections;

namespace CustomAnimator
{
    public class NativeArrayStream : IDisposable
    {
        private NativeArray<float> _targetArray;
        private int _position;

        public NativeArray<float> TargetArray => _targetArray;

        public NativeArrayStream(NativeArray<float> targetArray)
        {
            _targetArray = targetArray;
        }

        public void Write(float value)
        {
            _targetArray[_position++] = value;
        }

        public float Read()
        {
            return _targetArray[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }

        public void Dispose()
        {
            _targetArray.Dispose();
        }
    }
}