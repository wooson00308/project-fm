using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 헤드 파트 인터페이스
    /// </summary>
    public interface IHeadPart : IMechaPart
    {
        // 추가 속성
        int SensorRange { get; }
        float TargetingAccuracy { get; }
        bool HasNightVision { get; }
        int DetectionLevel { get; }
        
        // 추가 메서드
        bool ScanArea(Vector3 position, float radius);
        float CalculateAccuracyModifier(float distance);
        bool DetectHiddenTarget(int stealthLevel);
    }
} 