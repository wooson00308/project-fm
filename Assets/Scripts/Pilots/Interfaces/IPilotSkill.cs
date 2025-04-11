using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Pilots.Enums;

namespace ProjectFM.Pilots.Interfaces
{
    /// <summary>
    /// 파일럿 스킬 인터페이스
    /// </summary>
    public interface IPilotSkill
    {
        // 기본 속성
        string SkillName { get; }
        string Description { get; }
        int Level { get; }
        bool IsActive { get; }
        float CooldownTime { get; }
        SkillType Type { get; }
        
        // 메서드
        void Initialize();
        void Activate(IPilot pilot);
        void LevelUp();
        bool CanUse();
        void StartCooldown();
    }
} 