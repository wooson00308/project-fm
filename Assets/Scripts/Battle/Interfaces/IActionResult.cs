using System;
using System.Collections.Generic;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 액션 실행 결과를 정의하는 인터페이스입니다.
    /// </summary>
    public interface IActionResult
    {
        /// <summary>
        /// 성공 여부
        /// </summary>
        bool IsSuccessful { get; }
        
        /// <summary>
        /// 결과 메시지
        /// </summary>
        string ResultMessage { get; }
        
        /// <summary>
        /// 액션으로 인한 데미지
        /// </summary>
        int Damage { get; }
        
        /// <summary>
        /// 액션으로 인한 효과들
        /// </summary>
        IStatusEffect[] AppliedEffects { get; }
        
        /// <summary>
        /// 타임스탬프
        /// </summary>
        DateTime Timestamp { get; }
    }
} 