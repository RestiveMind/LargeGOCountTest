using System.Collections.Generic;
using UnityEngine;

namespace CustomAnimator
{
    public static class AnimationCurveExt
    {
        public static void WriteSamples(this AnimationCurve ac, int samplesCount, NativeArrayStream nativeArrayStream)
        {
            var timeStep = 1f / (samplesCount - 1);
 
            for (var i = 0; i < samplesCount; i++)
            {
                nativeArrayStream.Write(ac.Evaluate(i * timeStep));
            }
        }

        public static void WriteVector3Samples(this IEnumerable<KeyframeData> keyframes, int samplesCount,
            NativeArrayStream stream)
        {
            var xCurve = new AnimationCurve();
            var yCurve = new AnimationCurve();
            var zCurve = new AnimationCurve();
            foreach (var curve in keyframes)
            {
                xCurve.AddKey(new Keyframe(curve.Time, curve.Value.x, curve.InSlope.x, curve.OutSlope.x,
                    curve.InWeight.x, curve.OutWeight.x));

                yCurve.AddKey(new Keyframe(curve.Time, curve.Value.y, curve.InSlope.y, curve.OutSlope.y,
                    curve.InWeight.y, curve.OutWeight.y));

                zCurve.AddKey(new Keyframe(curve.Time, curve.Value.z, curve.InSlope.z, curve.OutSlope.z,
                    curve.InWeight.z, curve.OutWeight.z));
            }

            xCurve.WriteSamples(samplesCount, stream);
            yCurve.WriteSamples(samplesCount, stream);
            zCurve.WriteSamples(samplesCount, stream);
        }

        public static void WriteVector4Samples(this IEnumerable<KeyframeData> keyframes, int samplesCount,
            NativeArrayStream stream)
        {
            var xCurve = new AnimationCurve();
            var yCurve = new AnimationCurve();
            var zCurve = new AnimationCurve();
            var wCurve = new AnimationCurve();
            foreach (var curve in keyframes)
            {
                xCurve.AddKey(new Keyframe(curve.Time, curve.Value.x, curve.InSlope.x, curve.OutSlope.x,
                    curve.InWeight.x, curve.OutWeight.x));

                yCurve.AddKey(new Keyframe(curve.Time, curve.Value.y, curve.InSlope.y, curve.OutSlope.y,
                    curve.InWeight.y, curve.OutWeight.y));

                zCurve.AddKey(new Keyframe(curve.Time, curve.Value.z, curve.InSlope.z, curve.OutSlope.z,
                    curve.InWeight.z, curve.OutWeight.z));
                
                wCurve.AddKey(new Keyframe(curve.Time, curve.Value.w, curve.InSlope.w, curve.OutSlope.w,
                    curve.InWeight.w, curve.OutWeight.w));
            }

            xCurve.WriteSamples(samplesCount, stream);
            yCurve.WriteSamples(samplesCount, stream);
            zCurve.WriteSamples(samplesCount, stream);
            wCurve.WriteSamples(samplesCount, stream);
        }

    }
}