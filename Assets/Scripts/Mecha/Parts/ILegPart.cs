using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 다리 파트 인터페이스
    /// </summary>
    public interface ILegPart : IMechaPart
    {
        // 추가 속성
        int MovementPoints { get; }
        float StabilityFactor { get; }
        TerrainType[] SupportedTerrains { get; }
        MovementType MovementMode { get; }
        
        // 추가 메서드
        float GetMovementCostModifier(TerrainType terrain);
        bool CanTraverseTerrainType(TerrainType terrain);
        float GetStabilityBonus();
        int GetJumpDistance();
    }
    
    /// <summary>
    /// 지형 타입 열거형
    /// </summary>
    public enum TerrainType
    {
        Flat,
        Rough,
        Water,
        Mountain,
        Urban,
        Desert,
        Snow
    }
    
    /// <summary>
    /// 이동 타입 열거형
    /// </summary>
    public enum MovementType
    {
        Biped,
        Quadruped,
        Tracks,
        Wheels,
        Hover,
        Flight
    }
} 