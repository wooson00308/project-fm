using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 파일럿 스탯 타입을 정의하는 열거형입니다.
    /// </summary>
    public enum PilotStat
    {
        Reflexes,
        Gunnery,
        Melee,
        Tactics,
        Engineering,
        Leadership
    }
    
    /// <summary>
    /// 파일럿 클래스를 정의하는 열거형입니다.
    /// </summary>
    public enum PilotClass
    {
        Striker,
        Defender,
        Support,
        Commander
    }
    
    /// <summary>
    /// 파일럿 데이터에 대한 인터페이스입니다.
    /// ScriptableObject에서 구현될 기본 데이터 구조를 정의합니다.
    /// </summary>
    public interface IPilotData
    {
        /// <summary>
        /// 파일럿의 고유 ID
        /// </summary>
        string PilotID { get; }
        
        /// <summary>
        /// 파일럿의 이름
        /// </summary>
        string PilotName { get; }
        
        /// <summary>
        /// 파일럿의 클래스
        /// </summary>
        PilotClass Class { get; }
        
        /// <summary>
        /// 파일럿의 기본 레벨
        /// </summary>
        int BaseLevel { get; }
        
        /// <summary>
        /// 파일럿의 기본 스탯
        /// </summary>
        IDictionary<PilotStat, int> BaseStats { get; }
        
        /// <summary>
        /// 파일럿이 보유한 스킬 배열
        /// </summary>
        IPilotSkillData[] Skills { get; }
    }
    
    /// <summary>
    /// 파일럿 스킬 데이터에 대한 인터페이스입니다.
    /// </summary>
    public interface IPilotSkillData
    {
        /// <summary>
        /// 스킬의 고유 ID
        /// </summary>
        string SkillID { get; }
        
        /// <summary>
        /// 스킬의 이름
        /// </summary>
        string SkillName { get; }
        
        /// <summary>
        /// 스킬의 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 스킬의 쿨다운 턴 수
        /// </summary>
        int CooldownTurns { get; }
        
        /// <summary>
        /// 스킬 사용을 위한 최소 에너지
        /// </summary>
        int EnergyCost { get; }
    }
} 