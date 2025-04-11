using ProjectFM.Battle.Enums;
using System;
using System.Collections.Generic;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 텍스트 로그 엔트리를 정의하는 인터페이스입니다.
    /// </summary>
    public interface ITextLogEntry
    {
        /// <summary>
        /// 로그 엔트리의 타입
        /// </summary>
        LogEntryType Type { get; }
        
        /// <summary>
        /// 로그 메시지
        /// </summary>
        string Message { get; }
        
        /// <summary>
        /// 로그 생성 시간
        /// </summary>
        DateTime Timestamp { get; }
        
        /// <summary>
        /// 로그와 관련된 추가 파라미터
        /// </summary>
        object[] Parameters { get; }
    }
} 