using ProjectFM.Data.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Battle.Interfaces
{
    /// <summary>
    /// 텍스트 로그 생성기를 정의하는 인터페이스입니다.
    /// </summary>
    public interface ITextLogGenerator
    {
        /// <summary>
        /// 액션 로그를 생성합니다.
        /// </summary>
        /// <param name="action">수행된 액션</param>
        /// <param name="result">액션 결과</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateActionLog(IAction action, IActionResult result);
        
        /// <summary>
        /// 이동 로그를 생성합니다.
        /// </summary>
        /// <param name="unit">이동한 유닛</param>
        /// <param name="fromPosition">시작 위치</param>
        /// <param name="toPosition">도착 위치</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateMovementLog(IUnit unit, Vector2Int fromPosition, Vector2Int toPosition);
        
        /// <summary>
        /// 데미지 로그를 생성합니다.
        /// </summary>
        /// <param name="attacker">공격자</param>
        /// <param name="target">대상</param>
        /// <param name="damage">데미지</param>
        /// <param name="targetPart">대상 파트</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateDamageLog(IUnit attacker, IUnit target, int damage, PartType targetPart);
        
        /// <summary>
        /// 파츠 파괴 로그를 생성합니다.
        /// </summary>
        /// <param name="unit">해당 유닛</param>
        /// <param name="destroyedPart">파괴된 파트</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateDestructionLog(IUnit unit, PartType destroyedPart);
        
        /// <summary>
        /// 상태 효과 로그를 생성합니다.
        /// </summary>
        /// <param name="unit">대상 유닛</param>
        /// <param name="effect">적용된 효과</param>
        /// <returns>생성된 로그 엔트리</returns>
        ITextLogEntry CreateStatusEffectLog(IUnit unit, IStatusEffect effect);
    }
} 