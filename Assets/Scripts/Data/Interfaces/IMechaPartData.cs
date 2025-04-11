using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 메카닉 파트 타입을 정의하는 열거형입니다.
    /// </summary>
    public enum PartType
    {
        Body,
        LeftArm,
        RightArm,
        Legs,
        Backpack
    }
    
    /// <summary>
    /// 스탯 타입을 정의하는 열거형입니다.
    /// </summary>
    public enum StatType
    {
        Attack,
        Defense,
        Mobility,
        Energy,
        Accuracy,
        Evasion,
        CriticalRate
    }
    
    /// <summary>
    /// 메카닉 파트 데이터에 대한 인터페이스입니다.
    /// ScriptableObject에서 구현될 기본 데이터 구조를 정의합니다.
    /// </summary>
    public interface IMechaPartData
    {
        /// <summary>
        /// 파트의 고유 ID
        /// </summary>
        string PartID { get; }
        
        /// <summary>
        /// 파트의 이름
        /// </summary>
        string PartName { get; }
        
        /// <summary>
        /// 파트의 유형 (몸체, 팔, 다리 등)
        /// </summary>
        PartType Type { get; }
        
        /// <summary>
        /// 파트의 최대 내구도 기본값
        /// </summary>
        int BaseMaxDurability { get; }
        
        /// <summary>
        /// 파트가 제공하는 스탯 수정자
        /// </summary>
        IDictionary<StatType, int> StatModifiers { get; }
        
        /// <summary>
        /// 파트가 가진 특수 효과 배열
        /// </summary>
        ISpecialEffect[] SpecialEffects { get; }
        
        /// <summary>
        /// 파트의 카테고리 (무기, 방어, 이동 등)
        /// </summary>
        string Category { get; }
    }
    
    /// <summary>
    /// 특수 효과에 대한 인터페이스입니다.
    /// </summary>
    public interface ISpecialEffect
    {
        /// <summary>
        /// 특수 효과의 이름
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// 특수 효과의 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 효과가 적용되는 조건
        /// </summary>
        string Condition { get; }
    }
} 