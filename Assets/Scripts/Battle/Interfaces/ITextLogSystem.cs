using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 로그 엔트리 타입을 정의하는 열거형입니다.
    /// </summary>
    public enum LogEntryType
    {
        Movement,
        Attack,
        Damage,
        PartDestruction,
        StatusEffect,
        TurnChange,
        BattleEvent
    }
    
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
    
    /// <summary>
    /// 텍스트 로그 생성기를 정의하는 인터페이스입니다.
    /// </summary>
    public interface ITextLogGenerator
    {
        /// <summary>
        /// 액션 로그를 생성합니다.
        /// </summary>
        /// <param name="action">수행된 액션</param>
        /// <param name="result">액션 결과</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateActionLog(IAction action, IActionResult result);
        
        /// <summary>
        /// 이동 로그를 생성합니다.
        /// </summary>
        /// <param name="unit">이동한 유닛</param>
        /// <param name="fromPosition">시작 위치</param>
        /// <param name="toPosition">도착 위치</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateMovementLog(IUnit unit, Vector2Int fromPosition, Vector2Int toPosition);
        
        /// <summary>
        /// 데미지 로그를 생성합니다.
        /// </summary>
        /// <param name="attacker">공격자</param>
        /// <param name="target">대상</param>
        /// <param name="damage">데미지</param>
        /// <param name="targetPart">대상 파트</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateDamageLog(IUnit attacker, IUnit target, int damage, PartType targetPart);
        
        /// <summary>
        /// 파츠 파괴 로그를 생성합니다.
        /// </summary>
        /// <param name="unit">해당 유닛</param>
        /// <param name="destroyedPart">파괴된 파트</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateDestructionLog(IUnit unit, PartType destroyedPart);
        
        /// <summary>
        /// 상태 효과 로그를 생성합니다.
        /// </summary>
        /// <param name="unit">대상 유닛</param>
        /// <param name="effect">적용된 효과</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateStatusEffectLog(IUnit unit, IStatusEffect effect);
    }
    
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
    
    /// <summary>
    /// 액션을 정의하는 인터페이스입니다.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// 액션의 고유 ID
        /// </summary>
        string ActionID { get; }
        
        /// <summary>
        /// 액션의 이름
        /// </summary>
        string ActionName { get; }
        
        /// <summary>
        /// 액션 수행자
        /// </summary>
        IUnit Actor { get; }
        
        /// <summary>
        /// 액션 대상
        /// </summary>
        IUnit Target { get; }
        
        /// <summary>
        /// 액션을 실행합니다.
        /// </summary>
        /// <returns>액션 실행 결과</returns>
        IActionResult Execute();
    }
    
    /// <summary>
    /// 액션 결과를 정의하는 인터페이스입니다.
    /// </summary>
    public interface IActionResult
    {
        /// <summary>
        /// 액션 성공 여부
        /// </summary>
        bool IsSuccess { get; }
        
        /// <summary>
        /// 액션 결과 메시지
        /// </summary>
        string ResultMessage { get; }
        
        /// <summary>
        /// 액션 관련 효과 목록
        /// </summary>
        IReadOnlyList<IStatusEffect> AppliedEffects { get; }
    }
    
    /// <summary>
    /// 상태 효과를 정의하는 인터페이스입니다.
    /// </summary>
    public interface IStatusEffect
    {
        /// <summary>
        /// 상태 효과의 고유 ID
        /// </summary>
        string EffectID { get; }
        
        /// <summary>
        /// 상태 효과의 이름
        /// </summary>
        string EffectName { get; }
        
        /// <summary>
        /// 효과 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 효과 지속 턴 수
        /// </summary>
        int DurationTurns { get; }
        
        /// <summary>
        /// 효과가 적용된 유닛
        /// </summary>
        IUnit AffectedUnit { get; }
    }
} 