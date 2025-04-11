using System;
using System.Collections.Generic;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 전투에서 수행 가능한 액션을 정의하는 인터페이스입니다.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// 액션의 고유 식별자
        /// </summary>
        string Id { get; }
        
        /// <summary>
        /// 액션 이름
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 액션 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 액션 수행자
        /// </summary>
        IUnit Actor { get; }
        
        /// <summary>
        /// 액션 대상
        /// </summary>
        IUnit Target { get; }
        
        /// <summary>
        /// 액션 실행
        /// </summary>
        /// <returns>액션 결과</returns>
        IActionResult Execute();
        
        /// <summary>
        /// 실행 가능 여부 확인
        /// </summary>
        /// <returns>실행 가능 여부</returns>
        bool CanExecute();
        
        /// <summary>
        /// 액션 취소
        /// </summary>
        void Cancel();
    }
} 