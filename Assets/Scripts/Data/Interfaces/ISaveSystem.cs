using System;
using System.Collections.Generic;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 게임 저장 시스템에 대한 인터페이스입니다.
    /// 게임 데이터의 저장 및 로드 기능을 제공합니다.
    /// </summary>
    public interface ISaveSystem
    {
        /// <summary>
        /// 특정 슬롯에 저장 데이터가 존재하는지 확인합니다.
        /// </summary>
        /// <param name="saveSlot">확인할 저장 슬롯</param>
        /// <returns>저장 데이터 존재 여부</returns>
        bool SaveExists(string saveSlot);
        
        /// <summary>
        /// 게임 데이터를 저장합니다.
        /// </summary>
        /// <param name="saveSlot">저장할 슬롯</param>
        /// <param name="saveData">저장할 게임 데이터</param>
        /// <returns>저장 성공 여부</returns>
        bool SaveGame(string saveSlot, IGameSaveData saveData);
        
        /// <summary>
        /// 게임 데이터를 로드합니다.
        /// </summary>
        /// <param name="saveSlot">로드할 슬롯</param>
        /// <returns>로드된 게임 데이터</returns>
        IGameSaveData LoadGame(string saveSlot);
        
        /// <summary>
        /// 저장 데이터를 삭제합니다.
        /// </summary>
        /// <param name="saveSlot">삭제할 저장 슬롯</param>
        /// <returns>삭제 성공 여부</returns>
        bool DeleteSave(string saveSlot);
        
        /// <summary>
        /// 모든 저장 슬롯 목록을 가져옵니다.
        /// </summary>
        /// <returns>저장 슬롯 배열</returns>
        string[] GetAllSaveSlots();
    }
    
    /// <summary>
    /// 게임 저장 데이터에 대한 인터페이스입니다.
    /// </summary>
    public interface IGameSaveData
    {
        /// <summary>
        /// 저장 버전
        /// </summary>
        string SaveVersion { get; }
        
        /// <summary>
        /// 저장 날짜
        /// </summary>
        DateTime SaveDate { get; }
        
        /// <summary>
        /// 플레이어 메카닉 유닛 저장 데이터 배열
        /// </summary>
        IMechaUnitSaveData[] PlayerMechs { get; }
        
        /// <summary>
        /// 플레이어 파일럿 저장 데이터 배열
        /// </summary>
        IPilotSaveData[] PlayerPilots { get; }
        
        /// <summary>
        /// 인벤토리 저장 데이터
        /// </summary>
        IInventorySaveData Inventory { get; }
        
        /// <summary>
        /// 게임 진행 저장 데이터
        /// </summary>
        IProgressSaveData Progress { get; }
    }
    
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
    
    /// <summary>
    /// 인벤토리 저장 데이터에 대한 인터페이스입니다.
    /// </summary>
    public interface IInventorySaveData
    {
        /// <summary>
        /// 보유한 파츠 ID 배열
        /// </summary>
        string[] OwnedParts { get; }
        
        /// <summary>
        /// 자원 사전
        /// </summary>
        IDictionary<string, int> Resources { get; }
    }
    
    /// <summary>
    /// 게임 진행 저장 데이터에 대한 인터페이스입니다.
    /// </summary>
    public interface IProgressSaveData
    {
        /// <summary>
        /// 완료한 미션 ID 배열
        /// </summary>
        string[] CompletedMissions { get; }
        
        /// <summary>
        /// 현재 활성화된 미션 ID 배열
        /// </summary>
        string[] ActiveMissions { get; }
        
        /// <summary>
        /// 게임 플레이 시간 (초)
        /// </summary>
        float PlayTimeSeconds { get; }
    }
} 