using System;

namespace ProjectFM.Battle.Enums
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
} 