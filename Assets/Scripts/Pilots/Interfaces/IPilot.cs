using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Pilots.Enums;

namespace ProjectFM.Pilots.Interfaces
{
    /// <summary>
    /// 파일럿의 기본 인터페이스
    /// </summary>
    public interface IPilot
    {
        // 기본 속성
        string PilotName { get; }
        int Level { get; }
        PilotClass Class { get; }
        
        // 스탯 및 스킬
        IReadOnlyDictionary<PilotStat, int> Stats { get; }
        IReadOnlyCollection<IPilotSkill> Skills { get; }
        
        // 메서드
        void Initialize();
        void GainExperience(int amount);
        void LevelUp();
        void LearnSkill(IPilotSkill skill);
        int GetStatValue(PilotStat stat);
    }
} 