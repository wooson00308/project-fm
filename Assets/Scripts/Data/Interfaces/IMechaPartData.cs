using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 메카 파트 데이터를 정의하는 인터페이스입니다.
    /// </summary>
    public interface IMechaPartData
    {
        /// <summary>
        /// 파트 ID
        /// </summary>
        string Id { get; }
        
        /// <summary>
        /// 파트 이름
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 파트 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 파트 타입
        /// </summary>
        PartType Type { get; }
        
        /// <summary>
        /// 에너지 소모량
        /// </summary>
        int EnergyCost { get; }
        
        /// <summary>
        /// 내구도
        /// </summary>
        int Durability { get; }
        
        /// <summary>
        /// 최대 내구도
        /// </summary>
        int MaxDurability { get; }
        
        /// <summary>
        /// 파트 스탯
        /// </summary>
        IReadOnlyDictionary<StatType, int> Stats { get; }
        
        /// <summary>
        /// 특수 효과 목록
        /// </summary>
        ISpecialEffect[] SpecialEffects { get; }
        
        /// <summary>
        /// 파트 아이콘
        /// </summary>
        Sprite Icon { get; }
        
        /// <summary>
        /// 모델 프리팹 경로
        /// </summary>
        string ModelPrefabPath { get; }
    }
}