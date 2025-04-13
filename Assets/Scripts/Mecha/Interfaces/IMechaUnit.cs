using ProjectFM.Mecha.Enums;
using ProjectFM.Pilots.Interfaces;


namespace ProjectFM.Mecha.Interfaces
{
    /// <summary>
    /// 메카닉 유닛의 기본 인터페이스
    /// </summary>
    public interface IMechaUnit
    {
        // 기본 속성
        string UnitName { get; }
        int Level { get; }
        bool IsDestroyed { get; }
        
        // 파트 참조
        IMechaPart Head { get; }
        IMechaPart Body { get; }
        IMechaPart LeftArm { get; }
        IMechaPart RightArm { get; }
        IMechaPart Legs { get; }
        IMechaPart Backpack { get; }
        
        // 파일럿 참조
        IPilot CurrentPilot { get; }
        
        // 메서드
        void Initialize();
        void EquipPart(IMechaPart part);
        void AssignPilot(IPilot pilot);
        void CalculateStats();
        void TakeDamage(int damage, PartType targetPart);
        void CheckDestructionStatus();
    }
} 