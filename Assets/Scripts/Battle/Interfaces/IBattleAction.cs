using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;
using ProjectFM.Battle.Enums;

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
} 