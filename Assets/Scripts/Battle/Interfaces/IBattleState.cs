using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 전투 단계를 정의하는 열거형입니다.
    /// </summary>
    public enum BattlePhase
    {
        Initialization,
        TurnStart,
        UnitSelection,
        ActionSelection,
        ActionExecution,
        TurnEnd,
        BattleEnd
    }
    
    /// <summary>
    /// 전투 결과를 정의하는 열거형입니다.
    /// </summary>
    public enum BattleResult
    {
        InProgress,
        PlayerVictory,
        PlayerDefeat,
        Draw
    }
    
    /// <summary>
    /// 전투 상태를 관리하는 인터페이스입니다.
    /// 현재 전투 진행 상황과 관련된 정보를 제공합니다.
    /// </summary>
    public interface IBattleState
    {
        /// <summary>
        /// 현재 전투 단계
        /// </summary>
        BattlePhase CurrentPhase { get; }
        
        /// <summary>
        /// 현재 턴 번호
        /// </summary>
        int CurrentTurn { get; }
        
        /// <summary>
        /// 현재 행동 중인 유닛
        /// </summary>
        IUnit CurrentActor { get; }
        
        /// <summary>
        /// 플레이어 유닛 목록
        /// </summary>
        IReadOnlyList<IUnit> PlayerUnits { get; }
        
        /// <summary>
        /// 적 유닛 목록
        /// </summary>
        IReadOnlyList<IUnit> EnemyUnits { get; }
        
        /// <summary>
        /// 전투 그리드
        /// </summary>
        IBattleGrid BattleGrid { get; }
        
        /// <summary>
        /// 텍스트 로그 관리자
        /// </summary>
        ITextLogManager LogManager { get; }
        
        /// <summary>
        /// 현재 플레이어 턴인지 여부
        /// </summary>
        bool IsPlayerTurn { get; }
        
        /// <summary>
        /// 전투가 종료되었는지 여부
        /// </summary>
        bool IsBattleOver { get; }
        
        /// <summary>
        /// 현재 전투 결과를 가져옵니다.
        /// </summary>
        /// <returns>전투 결과</returns>
        BattleResult GetBattleResult();
        
        /// <summary>
        /// 전투 단계 변경 시 발생하는 이벤트
        /// </summary>
        event Action<BattlePhase> OnPhaseChanged;
        
        /// <summary>
        /// 전투 종료 시 발생하는 이벤트
        /// </summary>
        event Action<BattleResult> OnBattleEnded;
    }
    
    /// <summary>
    /// 턴 관리를 위한 인터페이스입니다.
    /// 턴 기반 전투에서 턴 순서와 현재 행동자를 관리합니다.
    /// </summary>
    public interface ITurnManager
    {
        /// <summary>
        /// 현재 턴 번호
        /// </summary>
        int CurrentTurn { get; }
        
        /// <summary>
        /// 현재 행동 중인 유닛
        /// </summary>
        IUnit CurrentActor { get; }
        
        /// <summary>
        /// 턴 순서 목록
        /// </summary>
        IReadOnlyList<IUnit> TurnOrder { get; }
        
        /// <summary>
        /// 전투를 시작합니다.
        /// </summary>
        void StartBattle();
        
        /// <summary>
        /// 전투를 종료합니다.
        /// </summary>
        void EndBattle();
        
        /// <summary>
        /// 다음 턴으로 진행합니다.
        /// </summary>
        void NextTurn();
        
        /// <summary>
        /// 현재 턴을 종료합니다.
        /// </summary>
        void EndCurrentTurn();
        
        /// <summary>
        /// 유닛을 턴 순서에 등록합니다.
        /// </summary>
        /// <param name="unit">등록할 유닛</param>
        void RegisterUnit(IUnit unit);
        
        /// <summary>
        /// 유닛을 턴 순서에서 제거합니다.
        /// </summary>
        /// <param name="unit">제거할 유닛</param>
        void UnregisterUnit(IUnit unit);
        
        /// <summary>
        /// 행동자 변경 시 발생하는 이벤트
        /// </summary>
        event Action<IUnit> OnActorChanged;
        
        /// <summary>
        /// 턴 변경 시 발생하는 이벤트
        /// </summary>
        event Action<int> OnTurnChanged;
    }
    
    /// <summary>
    /// 전투 그리드를 정의하는 인터페이스입니다.
    /// </summary>
    public interface IBattleGrid
    {
        /// <summary>
        /// 그리드의 너비
        /// </summary>
        int Width { get; }
        
        /// <summary>
        /// 그리드의 높이
        /// </summary>
        int Height { get; }
        
        /// <summary>
        /// 특정 위치의 유닛을 가져옵니다.
        /// </summary>
        /// <param name="position">위치</param>
        /// <returns>해당 위치의 유닛 (없으면 null)</returns>
        IUnit GetUnitAt(Vector2Int position);
        
        /// <summary>
        /// 유닛을 특정 위치에 배치합니다.
        /// </summary>
        /// <param name="unit">배치할 유닛</param>
        /// <param name="position">배치할 위치</param>
        /// <returns>배치 성공 여부</returns>
        bool PlaceUnit(IUnit unit, Vector2Int position);
        
        /// <summary>
        /// 유닛의 위치를 가져옵니다.
        /// </summary>
        /// <param name="unit">위치를 확인할 유닛</param>
        /// <returns>유닛의 위치 (그리드에 없으면 null)</returns>
        Vector2Int? GetUnitPosition(IUnit unit);
        
        /// <summary>
        /// 유닛을 그리드에서 제거합니다.
        /// </summary>
        /// <param name="unit">제거할 유닛</param>
        /// <returns>제거 성공 여부</returns>
        bool RemoveUnit(IUnit unit);
        
        /// <summary>
        /// 두 위치 간의 거리를 계산합니다.
        /// </summary>
        /// <param name="from">시작 위치</param>
        /// <param name="to">도착 위치</param>
        /// <returns>두 위치 간의 거리</returns>
        int CalculateDistance(Vector2Int from, Vector2Int to);
        
        /// <summary>
        /// 유닛 배치 시 발생하는 이벤트
        /// </summary>
        event Action<IUnit, Vector2Int> OnUnitPlaced;
        
        /// <summary>
        /// 유닛 이동 시 발생하는 이벤트
        /// </summary>
        event Action<IUnit, Vector2Int, Vector2Int> OnUnitMoved;
        
        /// <summary>
        /// 유닛 제거 시 발생하는 이벤트
        /// </summary>
        event Action<IUnit, Vector2Int> OnUnitRemoved;
    }
    
    /// <summary>
    /// 유닛을 정의하는 인터페이스입니다.
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// 유닛의 고유 ID
        /// </summary>
        string UnitID { get; }
        
        /// <summary>
        /// 유닛의 이름
        /// </summary>
        string UnitName { get; }
        
        /// <summary>
        /// 유닛이 플레이어 소속인지 여부
        /// </summary>
        bool IsPlayerUnit { get; }
        
        /// <summary>
        /// 유닛이 행동 가능한지 여부
        /// </summary>
        bool CanAct { get; }
        
        /// <summary>
        /// 유닛이 파괴되었는지 여부
        /// </summary>
        bool IsDestroyed { get; }
    }
} 