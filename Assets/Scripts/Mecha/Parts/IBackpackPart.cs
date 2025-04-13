using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 메카의 백팩 파트 인터페이스
    /// </summary>
    public interface IBackpackPart : IMechaPart
    {
        // 속성
        float EnergyCapacity { get; }
        float EnergyRechargeRate { get; }
        bool HasFlightSystem { get; }
        bool HasSpecialEquipment { get; }
        string[] SpecialAbilities { get; }
        
        // 메서드
        float GetCurrentEnergy();
        void ConsumeEnergy(float amount);
        void RechargeEnergy(float amount);
        bool ActivateSpecialAbility(string abilityName);
        bool CanUseAbility(string abilityName);
    }
} 