using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;

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
    
    /// <summary>
    /// 무기 타입 열거형
    /// </summary>
    public enum WeaponType
    {
        MachineGun,
        Cannon,
        Missile,
        Laser,
        Melee,
        Flamethrower
    }
} 