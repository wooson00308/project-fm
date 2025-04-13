using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 팔 파트 인터페이스
    /// </summary>
    public interface IArmPart : IMechaPart
    {
        // 추가 속성
        bool IsWeaponMounted { get; }
        bool IsRightArm { get; }
        float AimStability { get; }
        float WeaponMountingCapacity { get; }
        
        // 추가 메서드
        bool MountWeapon(IWeaponSystem weapon);
        void UnmountWeapon();
        IWeaponSystem GetMountedWeapon();
        float GetAccuracyBonus();
    }
} 