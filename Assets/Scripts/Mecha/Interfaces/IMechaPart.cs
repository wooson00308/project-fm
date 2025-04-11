using ProjectFM.Data.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Mecha.Interfaces
{
    /// <summary>
    /// 메카닉 파트의 기본 인터페이스
    /// </summary>
    public interface IMechaPart
    {
        // 기본 속성
        string PartName { get; }
        PartType Type { get; }
        int Durability { get; }
        int MaxDurability { get; }
        bool IsDestroyed { get; }
        
        // 스탯 관련
        IDictionary<StatType, int> StatModifiers { get; }
        
        // 메서드
        void Initialize();
        void TakeDamage(int damage);
        void OnDestruction();
        void Repair(int amount);
    }
} 