using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 전투 액션 인터페이스
    /// </summary>
    public interface IBattleAction
    {
        // 기본 속성
        string ActionName { get; }
        string Description { get; }
        ActionType Type { get; }
        int ActionPointCost { get; }
        int Range { get; }
        bool IsTargeted { get; }
        bool RequiresLineOfSight { get; }
        
        // 메서드
        void Initialize();
        bool CanExecute(IMechaUnit executor);
        bool IsValidTarget(IMechaUnit executor, IMechaUnit target, Vector2Int targetPosition);
        void Execute(IMechaUnit executor, IMechaUnit target, Vector2Int targetPosition);
        List<Vector2Int> GetValidTargetPositions(IMechaUnit executor, Vector2Int executorPosition);
    }

    /// <summary>
    /// 액션 타입 열거형
    /// </summary>
    public enum ActionType
    {
        Movement,
        Attack,
        Support,
        Special,
        Overwatch,
        Interact
    }
} 