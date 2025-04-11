using ProjectFM.Battle.Enums;
using System;
using System.Collections.Generic;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 텍스트 로그 관리자를 정의하는 인터페이스입니다.
    /// </summary>
    public interface ITextLogManager
    {
        /// <summary>
        /// 현재 전투의 모든 로그 목록
        /// </summary>
        IReadOnlyList<ITextLogEntry> CurrentBattleLogs { get; }
        
        /// <summary>
        /// 로그 개수
        /// </summary>
        int LogCount { get; }
        
        /// <summary>
        /// 로그를 추가합니다.
        /// </summary>
        /// <param name="logEntry">추가할 로그 엔트리</param>
        void AddLog(ITextLogEntry logEntry);
        
        /// <summary>
        /// 모든 로그를 삭제합니다.
        /// </summary>
        void ClearLogs();
        
        /// <summary>
        /// 최근 로그를 가져옵니다.
        /// </summary>
        /// <param name="count">가져올 로그 개수</param>
        /// <returns>최근 로그 배열</returns>
        ITextLogEntry[] GetLatestLogs(int count);
        
        /// <summary>
        /// 특정 타입의 로그를 가져옵니다.
        /// </summary>
        /// <param name="type">가져올 로그 타입</param>
        /// <returns>해당 타입의 로그 배열</returns>
        ITextLogEntry[] GetLogsByType(LogEntryType type);
        
        /// <summary>
        /// 로그가 추가될 때 발생하는 이벤트
        /// </summary>
        event Action<ITextLogEntry> OnLogAdded;
        
        /// <summary>
        /// 로그가 모두 지워질 때 발생하는 이벤트
        /// </summary>
        event Action OnLogsCleared;
    }
} 