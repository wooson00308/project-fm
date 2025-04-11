using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;
using System;

namespace ProjectFM.Battle.Interfaces
{
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
} 