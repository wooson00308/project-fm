using System;

namespace ProjectFM.Data.Interfaces
{
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
} 