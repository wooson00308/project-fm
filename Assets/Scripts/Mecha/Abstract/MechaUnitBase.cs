using ProjectFM.Mecha.Enums;
using ProjectFM.Mecha.Interfaces;
using ProjectFM.Pilots.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Mecha.Abstract
{
    /// <summary>
    /// 메카닉 유닛의 기본 추상 클래스
    /// </summary>
    public abstract class MechaUnitBase : MonoBehaviour, IMechaUnit
    {
        [SerializeField] protected string _unitName;
        [SerializeField] protected int _level = 1;
        
        // 파트 필드
        protected IMechaPart _headPart;
        protected IMechaPart _bodyPart;
        protected IMechaPart _leftArmPart;
        protected IMechaPart _rightArmPart;
        protected IMechaPart _legsPart;
        protected IMechaPart _backpackPart;
        
        // 파일럿 필드
        protected IPilot _currentPilot;
        
        // 상태 필드
        protected bool _isDestroyed = false;
        protected Dictionary<StatType, int> _baseStats = new Dictionary<StatType, int>();
        protected Dictionary<StatType, int> _calculatedStats = new Dictionary<StatType, int>();
        
        // IMechaUnit 구현
        public string UnitName => _unitName;
        public int Level => _level;
        public bool IsDestroyed => _isDestroyed;
        
        public IMechaPart Head => _headPart;
        public IMechaPart Body => _bodyPart;
        public IMechaPart LeftArm => _leftArmPart;
        public IMechaPart RightArm => _rightArmPart;
        public IMechaPart Legs => _legsPart;
        public IMechaPart Backpack => _backpackPart;
        
        public IPilot CurrentPilot => _currentPilot;
        
        public virtual void Initialize()
        {
            // 초기화 로직
            InitializeBaseStats();
            CalculateStats();
        }
        
        public virtual void EquipPart(IMechaPart part)
        {
            // 파트 타입에 따라 적절한 슬롯에 장착
            switch (part.Type)
            {
                case PartType.Head:
                    _headPart = part;
                    break;
                case PartType.Body:
                    _bodyPart = part;
                    break;
                case PartType.LeftArm:
                    _leftArmPart = part;
                    break;
                case PartType.RightArm:
                    _rightArmPart = part;
                    break;
                case PartType.Legs:
                    _legsPart = part;
                    break;
                case PartType.Backpack:
                    _backpackPart = part;
                    break;
            }
            
            CalculateStats();
        }
        
        public virtual void AssignPilot(IPilot pilot)
        {
            _currentPilot = pilot;
            CalculateStats();
        }
        
        public virtual void CalculateStats()
        {
            // 스탯 계산 로직
            _calculatedStats = new Dictionary<StatType, int>(_baseStats);
            
            // 각 파트에서 스탯 수정자 적용
            ApplyPartStatModifiers(_headPart);
            ApplyPartStatModifiers(_bodyPart);
            ApplyPartStatModifiers(_leftArmPart);
            ApplyPartStatModifiers(_rightArmPart);
            ApplyPartStatModifiers(_legsPart);
            ApplyPartStatModifiers(_backpackPart);
            
            // 파일럿 스탯 적용
            if (_currentPilot != null)
            {
                // 파일럿 스탯 적용 로직
            }
        }
        
        public virtual void TakeDamage(int damage, PartType targetPart)
        {
            IMechaPart targetedPart = null;
            
            // 타겟 파트 확인
            switch (targetPart)
            {
                case PartType.Head:
                    targetedPart = _headPart;
                    break;
                case PartType.Body:
                    targetedPart = _bodyPart;
                    break;
                case PartType.LeftArm:
                    targetedPart = _leftArmPart;
                    break;
                case PartType.RightArm:
                    targetedPart = _rightArmPart;
                    break;
                case PartType.Legs:
                    targetedPart = _legsPart;
                    break;
                case PartType.Backpack:
                    targetedPart = _backpackPart;
                    break;
            }
            
            // 데미지 적용
            if (targetedPart != null)
            {
                targetedPart.TakeDamage(damage);
                OnPartDamaged(targetedPart);
            }
            
            CheckDestructionStatus();
        }
        
        public virtual void CheckDestructionStatus()
        {
            // 바디 파트 파괴 확인
            if (_bodyPart != null && _bodyPart.IsDestroyed)
            {
                _isDestroyed = true;
                OnUnitDestroyed();
            }
        }
        
        // 헬퍼 메서드
        protected virtual void InitializeBaseStats()
        {
            // 기본 스탯 초기화
            _baseStats[StatType.Attack] = 10;
            _baseStats[StatType.Defense] = 10;
            _baseStats[StatType.Mobility] = 10;
            _baseStats[StatType.Energy] = 100;
            _baseStats[StatType.Accuracy] = 70;
            _baseStats[StatType.Evasion] = 30;
            _baseStats[StatType.CriticalRate] = 5;
        }
        
        protected virtual void ApplyPartStatModifiers(IMechaPart part)
        {
            if (part != null && !part.IsDestroyed)
            {
                foreach (var statMod in part.StatModifiers)
                {
                    if (_calculatedStats.ContainsKey(statMod.Key))
                    {
                        _calculatedStats[statMod.Key] += statMod.Value;
                    }
                    else
                    {
                        _calculatedStats[statMod.Key] = statMod.Value;
                    }
                }
            }
        }
        
        protected virtual void OnPartDamaged(IMechaPart part)
        {
            // 파트 데미지 이벤트 처리
            Debug.Log($"{UnitName}의 {part.PartName} 파트가 데미지를 입었습니다. 남은 내구도: {part.Durability}/{part.MaxDurability}");
            
            if (part.IsDestroyed)
            {
                OnPartDestroyed(part);
            }
            
            CalculateStats();
        }
        
        protected virtual void OnPartDestroyed(IMechaPart part)
        {
            // 파트 파괴 이벤트 처리
            Debug.Log($"{UnitName}의 {part.PartName} 파트가 파괴되었습니다!");
            
            // 파트 타입별 특수 처리
            if (part == _headPart)
            {
                Debug.Log($"{UnitName}의 센서 시스템이 오프라인 상태가 되었습니다. 정확도가 크게 감소합니다.");
                // 헤드 파트 파괴에 따른 추가 효과 처리
            }
        }
        
        protected virtual void OnUnitDestroyed()
        {
            // 유닛 파괴 이벤트 처리
            Debug.Log($"{UnitName} 유닛이 파괴되었습니다!");
        }
    }
} 