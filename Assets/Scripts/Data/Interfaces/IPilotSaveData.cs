using System.Collections.Generic;
using ProjectFM.Pilots.Enums;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 파일럿 저장 데이터에 대한 인터페이스입니다.
    /// </summary>
    public interface IPilotSaveData
    {
        /// <summary>
        /// 파일럿 ID
        /// </summary>
        string PilotID { get; }
        
        /// <summary>
        /// 현재 레벨
        /// </summary>
        int Level { get; }
        
        /// <summary>
        /// 경험치
        /// </summary>
        int Experience { get; }
        
        /// <summary>
        /// 스탯 경험치 사전
        /// </summary>
        IDictionary<PilotStat, int> StatExperience { get; }
        
        /// <summary>
        /// 잠금 해제된 스킬 ID 배열
        /// </summary>
        string[] UnlockedSkills { get; }
    }
} 