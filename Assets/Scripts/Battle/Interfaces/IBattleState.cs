using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Battle.Enums;

namespace ProjectFM.Battle.Interfaces
{
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
} 