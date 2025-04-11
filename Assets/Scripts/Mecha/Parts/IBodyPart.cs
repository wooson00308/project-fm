using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 바디 파트 인터페이스
    /// </summary>
    public interface IBodyPart : IMechaPart
    {
        // 추가 속성
        int CoreHitPoints { get; }
        int MaxCoreHitPoints { get; }
        float WeightCapacity { get; }
        bool HasLifeSupportSystems { get; }
        
        // 추가 메서드
        void ApplyCoreSystemDamage(int damage);
        bool CheckSystemStatus();
        float GetPowerOutput();
    }
} 