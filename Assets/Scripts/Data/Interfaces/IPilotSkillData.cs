using System;

namespace ProjectFM.Data.Interfaces
{
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