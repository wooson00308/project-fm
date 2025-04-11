using System;
using System.Collections.Generic;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 특수 효과를 정의하는 인터페이스입니다.
    /// </summary>
    public interface ISpecialEffect
    {
        /// <summary>
        /// 효과 이름
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 효과 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 효과 발동 조건
        /// </summary>
        string ActivationCondition { get; }
        
        /// <summary>
        /// 효과 값
        /// </summary>
        float Value { get; }
        
        /// <summary>
        /// 효과 발동
        /// </summary>
        /// <param name="context">효과 컨텍스트</param>
        /// <returns>발동 성공 여부</returns>
        bool Activate(object context);
    }
} 