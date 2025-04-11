using System;
using System.Collections.Generic;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 상태 효과를 정의하는 인터페이스입니다.
    /// </summary>
    public interface IStatusEffect
    {
        /// <summary>
        /// 상태 효과 이름
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 상태 효과 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 지속 시간 (턴)
        /// </summary>
        int Duration { get; }
        
        /// <summary>
        /// 스택 개수
        /// </summary>
        int StackCount { get; }
        
        /// <summary>
        /// 효과 적용
        /// </summary>
        /// <param name="target">적용 대상</param>
        void Apply(IUnit target);
        
        /// <summary>
        /// 효과 제거
        /// </summary>
        /// <param name="target">제거 대상</param>
        void Remove(IUnit target);
        
        /// <summary>
        /// 턴 종료 시 발동
        /// </summary>
        /// <param name="target">대상</param>
        void OnTurnEnd(IUnit target);
    }
} 