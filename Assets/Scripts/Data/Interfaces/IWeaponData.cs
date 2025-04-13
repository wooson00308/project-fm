using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 무기 데이터를 정의하는 인터페이스입니다.
    /// </summary>
    public interface IWeaponData
    {
        /// <summary>
        /// 무기 ID
        /// </summary>
        string Id { get; }
        
        /// <summary>
        /// 무기 이름
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 무기 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 무기 타입
        /// </summary>
        WeaponType Type { get; }
        
        /// <summary>
        /// 기본 데미지
        /// </summary>
        int BaseDamage { get; }
        
        /// <summary>
        /// 사거리
        /// </summary>
        int Range { get; }
        
        /// <summary>
        /// 무게
        /// </summary>
        float Weight { get; }
        
        /// <summary>
        /// 에너지 소모량
        /// </summary>
        int EnergyCost { get; }
        
        /// <summary>
        /// 최대 탄약
        /// </summary>
        int MaxAmmo { get; }
        
        /// <summary>
        /// 재장전 시간
        /// </summary>
        float ReloadTime { get; }
        
        /// <summary>
        /// 명중률 보너스
        /// </summary>
        int AccuracyBonus { get; }
        
        /// <summary>
        /// 크리티컬 확률
        /// </summary>
        float CriticalChance { get; }
        
        /// <summary>
        /// 크리티컬 데미지 배율
        /// </summary>
        float CriticalDamageMultiplier { get; }
        
        /// <summary>
        /// 장착 가능한 팔 타입
        /// </summary>
        bool CanMountOnRightArm { get; }
        bool CanMountOnLeftArm { get; }
        
        /// <summary>
        /// 무기 특수 효과
        /// </summary>
        ISpecialEffect[] SpecialEffects { get; }
        
        /// <summary>
        /// 무기 아이콘
        /// </summary>
        Sprite Icon { get; }
        
        /// <summary>
        /// 모델 프리팹 경로
        /// </summary>
        string ModelPrefabPath { get; }
    }
} 