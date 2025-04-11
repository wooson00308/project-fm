using ProjectFM.Battle.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 텍스트 로그 시스템을 정의하는 인터페이스입니다.
    /// </summary>
    public interface ITextLogSystem
    {
        /// <summary>
        /// 텍스트 로그 매니저
        /// </summary>
        ITextLogManager LogManager { get; }
        
        /// <summary>
        /// 텍스트 로그 생성기
        /// </summary>
        ITextLogGenerator LogGenerator { get; }
        
        /// <summary>
        /// 시스템 초기화
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// 배틀 시작 시 호출
        /// </summary>
        void OnBattleStart();
        
        /// <summary>
        /// 배틀 종료 시 호출
        /// </summary>
        void OnBattleEnd();
        
        /// <summary>
        /// 턴 시작 시 호출
        /// </summary>
        /// <param name="turnNumber">턴 번호</param>
        /// <param name="activeUnit">활성화된 유닛</param>
        void OnTurnStart(int turnNumber, IUnit activeUnit);
        
        /// <summary>
        /// 턴 종료 시 호출
        /// </summary>
        /// <param name="turnNumber">턴 번호</param>
        /// <param name="activeUnit">활성화된 유닛</param>
        void OnTurnEnd(int turnNumber, IUnit activeUnit);
    }
}