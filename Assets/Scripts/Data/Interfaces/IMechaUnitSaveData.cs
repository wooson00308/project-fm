using System.Collections.Generic;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 메카닉 유닛 저장 데이터에 대한 인터페이스입니다.
    /// </summary>
    public interface IMechaUnitSaveData
    {
        /// <summary>
        /// 유닛 ID
        /// </summary>
        string UnitID { get; }
        
        /// <summary>
        /// 유닛 이름
        /// </summary>
        string UnitName { get; }
        
        /// <summary>
        /// 파일럿 ID
        /// </summary>
        string PilotID { get; }
        
        /// <summary>
        /// 장착된 파츠 ID 사전
        /// </summary>
        IDictionary<PartType, string> EquippedParts { get; }
        
        /// <summary>
        /// 커스텀 색상 데이터
        /// </summary>
        string ColorData { get; }
    }
} 