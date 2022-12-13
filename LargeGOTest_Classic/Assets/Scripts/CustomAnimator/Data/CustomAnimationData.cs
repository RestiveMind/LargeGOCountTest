// <auto-generated/>

using UnityEditor;
using UnityEngine;
using YamlDotNet.Serialization;

namespace CustomAnimator
{
    public class CustomAnimationData
    {
        public AnimationClip AnimationClip { get; set; }
    }

    public class AnimationClip
    {
        [YamlMember(Alias="m_ObjectHideFlags")]
        public long ObjectHideFlags { get; set; }

        [YamlMember(Alias="m_CorrespondingSourceObject")]
        public AssetLink CorrespondingSourceObject { get; set; }

        [YamlMember(Alias="m_PrefabInstance")]
        public AssetLink PrefabInstance { get; set; }

        [YamlMember(Alias="m_PrefabAsset")]
        public AssetLink PrefabAsset { get; set; }

        [YamlMember(Alias="m_Name")]
        public string Name { get; set; }

        [YamlMember(Alias="serializedVersion")]
        public long SerializedVersion { get; set; }

        [YamlMember(Alias="m_Legacy")]
        public bool Legacy { get; set; }

        [YamlMember(Alias="m_Compressed")]
        public bool Compressed { get; set; }

        [YamlMember(Alias="m_UseHighQualityCurve")]
        public bool UseHighQualityCurve { get; set; }

        [YamlMember(Alias="m_RotationCurves")]
        public CurveData[] RotationCurves { get; set; }

        [YamlMember(Alias="m_CompressedRotationCurves")]
        public CurveData[] CompressedRotationCurves { get; set; }
        
        [YamlMember(Alias="m_EulerCurves")]
        public CurveData[] EulerCurves { get; set; }

        [YamlMember(Alias="m_PositionCurves")]
        public CurveData[] PositionCurves { get; set; }

        [YamlMember(Alias="m_ScaleCurves")]
        public CurveData[] ScaleCurves { get; set; }

        [YamlMember(Alias="m_FloatCurves")]
        public CurveData[] FloatCurves { get; set; }

        [YamlMember(Alias="m_PPtrCurves")]
        public CurveData[] PPtrCurves { get; set; }

        [YamlMember(Alias="m_SampleRate")]
        public int SampleRate { get; set; }

        [YamlMember(Alias="m_WrapMode")]
        public WrapMode WrapMode { get; set; }

        [YamlMember(Alias="m_Bounds")]
        public MBounds Bounds { get; set; }

        [YamlMember(Alias="m_ClipBindingConstant")]
        public MClipBindingConstant MClipBindingConstant { get; set; }

        [YamlMember(Alias="m_AnimationClipSettings")]
        public MAnimationClipSettings MAnimationClipSettings { get; set; }

        [YamlMember(Alias="m_EditorCurves")]
        public object[] MEditorCurves { get; set; }

        [YamlMember(Alias="m_EulerEditorCurves")]
        public object[] MEulerEditorCurves { get; set; }

        [YamlMember(Alias="m_HasGenericRootTransform")]
        public long MHasGenericRootTransform { get; set; }

        [YamlMember(Alias="m_HasMotionFloatCurves")]
        public long MHasMotionFloatCurves { get; set; }

        [YamlMember(Alias="m_Events")]
        public object[] MEvents { get; set; }
    }

    public class MBounds
    {
        [YamlMember(Alias="m_Center")]
        public Vector3 Center { get; set; }
        
        [YamlMember(Alias="m_Extent")]
        public Vector3 Extents { get; set; }
    }

    public class MAnimationClipSettings
    {
        [YamlMember(Alias="serializedVersion")]
        public long SerializedVersion { get; set; }

        [YamlMember(Alias="m_AdditiveReferencePoseClip")]
        public AssetLink MAdditiveReferencePoseClip { get; set; }

        [YamlMember(Alias="m_AdditiveReferencePoseTime")]
        public long MAdditiveReferencePoseTime { get; set; }

        [YamlMember(Alias="m_StartTime")]
        public long MStartTime { get; set; }

        [YamlMember(Alias="m_StopTime")]
        public double MStopTime { get; set; }

        [YamlMember(Alias="m_OrientationOffsetY")]
        public long MOrientationOffsetY { get; set; }

        [YamlMember(Alias="m_Level")]
        public long MLevel { get; set; }

        [YamlMember(Alias="m_CycleOffset")]
        public long MCycleOffset { get; set; }

        [YamlMember(Alias="m_HasAdditiveReferencePose")]
        public long MHasAdditiveReferencePose { get; set; }

        [YamlMember(Alias="m_LoopTime")]
        public long MLoopTime { get; set; }

        [YamlMember(Alias="m_LoopBlend")]
        public long MLoopBlend { get; set; }

        [YamlMember(Alias="m_LoopBlendOrientation")]
        public long MLoopBlendOrientation { get; set; }

        [YamlMember(Alias="m_LoopBlendPositionY")]
        public long MLoopBlendPositionY { get; set; }

        [YamlMember(Alias="m_LoopBlendPositionXZ")]
        public long MLoopBlendPositionXz { get; set; }

        [YamlMember(Alias="m_KeepOriginalOrientation")]
        public long MKeepOriginalOrientation { get; set; }

        [YamlMember(Alias="m_KeepOriginalPositionY")]
        public long MKeepOriginalPositionY { get; set; }

        [YamlMember(Alias="m_KeepOriginalPositionXZ")]
        public long MKeepOriginalPositionXz { get; set; }

        [YamlMember(Alias="m_HeightFromFeet")]
        public long MHeightFromFeet { get; set; }

        [YamlMember(Alias="m_Mirror")]
        public long MMirror { get; set; }
    }

    public class AssetLink
    {
        [YamlMember(Alias="fileID")]
        public long FileId { get; set; }
    }

    public class MClipBindingConstant
    {
        [YamlMember(Alias="genericBindings")]
        public GenericBinding[] GenericBindings { get; set; }

        [YamlMember(Alias="pptrCurveMapping")]
        public object[] PptrCurveMapping { get; set; }
    }

    public class GenericBinding
    {
        [YamlMember(Alias="serializedVersion")]
        public long SerializedVersion { get; set; }

        [YamlMember(Alias="path")]
        public long Path { get; set; }

        [YamlMember(Alias="attribute")]
        public long Attribute { get; set; }

        [YamlMember(Alias="script")]
        public AssetLink Script { get; set; }

        [YamlMember(Alias="typeID")]
        public long TypeId { get; set; }

        [YamlMember(Alias="customType")]
        public long CustomType { get; set; }

        [YamlMember(Alias="isPPtrCurve")]
        public long IsPPtrCurve { get; set; }
    }

    public class CurveData
    {
        [YamlMember(Alias="curve")]
        public CurveClass CurveClass { get; set; }

        [YamlMember(Alias="path")]
        public string Path { get; set; }
    }

    public class CurveClass
    {
        [YamlMember(Alias="serializedVersion")]
        public long SerializedVersion { get; set; }

        [YamlMember(Alias="m_Curve")]
        public KeyframeData[] KeyFrames { get; set; }

        [YamlMember(Alias="m_PreInfinity")]
        public long MPreInfinity { get; set; }

        [YamlMember(Alias="m_PostInfinity")]
        public long MPostInfinity { get; set; }

        [YamlMember(Alias="m_RotationOrder")]
        public long MRotationOrder { get; set; }
    }

    public struct KeyframeData
    {
        [YamlMember(Alias="serializedVersion")]
        public long SerializedVersion { get; set; }

        [YamlMember(Alias="time")]
        public float Time { get; set; }

        [YamlMember(Alias="value")]
        public Vector4 Value { get; set; }

        [YamlMember(Alias="inSlope")]
        public Vector4 InSlope { get; set; }

        [YamlMember(Alias="outSlope")]
        public Vector4 OutSlope { get; set; }

        [YamlMember(Alias="tangentMode")]
        public AnimationUtility.TangentMode TangentMode { get; set; }

        [YamlMember(Alias="weightedMode")]
        public WeightedMode WeightedMode { get; set; }

        [YamlMember(Alias="inWeight")]
        public Vector4 InWeight { get; set; }

        [YamlMember(Alias="outWeight")]
        public Vector4 OutWeight { get; set; }
    }
}