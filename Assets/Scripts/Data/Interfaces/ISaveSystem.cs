using ProjectFM.Data.Interfaces;
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
} 