using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>
    /// 파일럿 클래스 열거형
    /// </summary>
    public enum PilotClass
    {
        Rookie,
        Veteran,
        Ace,
        Legend
    }

    /// <summary>
    /// 파일럿 스탯 열거형
    /// </summary>
    public enum PilotStat
    {
        Reflexes,
        Precision,
        Endurance,
        Adaptation,
        Leadership,
        Willpower
    }
} 