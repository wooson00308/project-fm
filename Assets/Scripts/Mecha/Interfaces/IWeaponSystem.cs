using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Interfaces
{
    /// <summary>
    /// 무기 시스템 인터페이스
    /// </summary>
    public interface IWeaponSystem
    {
        // 기본 속성
        string WeaponName { get; }
        WeaponType Type { get; }
        int Damage { get; }
        int Range { get; }
        float Weight { get; }
        
        // 메서드
        void Initialize();
        void Fire(Vector3 targetPosition);
        bool CanFire();
        void Reload();
        int GetAmmoRemaining();
    }
} 