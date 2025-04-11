using System;

namespace ProjectFM.Battle.Interfaces
{
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