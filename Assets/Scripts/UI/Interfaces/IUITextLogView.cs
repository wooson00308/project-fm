using System;
using System.Collections.Generic;
using ProjectFM.Battle.Interfaces;

namespace ProjectFM.UI.Interfaces
{
    /// <summary>
    /// 텍스트 로그 UI 뷰를 정의하는 인터페이스입니다.
    /// 전투 로그를 UI에 표시하는 역할을 합니다.
    /// </summary>
    public interface IUITextLogView
    {
        /// <summary>
        /// 로그 엔트리를 UI에 추가합니다.
        /// </summary>
        /// <param name="logEntry">추가할 로그 엔트리</param>
        void AddLogEntry(ITextLogEntry logEntry);
        
        /// <summary>
        /// 여러 로그 엔트리를 한 번에 UI에 추가합니다.
        /// </summary>
        /// <param name="logEntries">추가할 로그 엔트리 목록</param>
        void AddLogEntries(IEnumerable<ITextLogEntry> logEntries);
        
        /// <summary>
        /// 로그 UI를 초기화합니다.
        /// </summary>
        void ClearLogs();
        
        /// <summary>
        /// 로그 뷰를 활성화합니다.
        /// </summary>
        void Show();
        
        /// <summary>
        /// 로그 뷰를 비활성화합니다.
        /// </summary>
        void Hide();
        
        /// <summary>
        /// 로그 뷰의 가시성을 설정합니다.
        /// </summary>
        /// <param name="visible">가시성 여부</param>
        void SetVisible(bool visible);
        
        /// <summary>
        /// 로그 엔트리를 UI에 표시할 때 발생하는 이벤트
        /// </summary>
        event Action<ITextLogEntry> OnLogDisplayed;
    }
} 