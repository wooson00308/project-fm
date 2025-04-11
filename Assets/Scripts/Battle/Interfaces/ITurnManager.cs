using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;
using System;

namespace ProjectFM.Battle.Interfaces
{
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
} 