using ProjectFM.Mecha.Enums;
using ProjectFM.Mecha.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Mecha.Abstract
{
    /// <summary>
    /// 메카닉 파트의 기본 추상 클래스
    /// </summary>
    public abstract class MechaPartBase : MonoBehaviour, IMechaPart
    {
        [SerializeField] protected string _partName;
        [SerializeField] protected PartType _type;
        [SerializeField] protected int _maxDurability = 100;
        [SerializeField] protected List<StatModifierData> _statModifiers = new List<StatModifierData>();
        
        protected int _currentDurability;
        protected bool _isDestroyed = false;
        protected Dictionary<StatType, int> _statModifiersDict = new Dictionary<StatType, int>();
        
        // IMechaPart 인터페이스 구현
        public string PartName => _partName;
        public PartType Type => _type;
        public int Durability => _currentDurability;
        public int MaxDurability => _maxDurability;
        public bool IsDestroyed => _isDestroyed;
        public IDictionary<StatType, int> StatModifiers => _statModifiersDict;
        
        public virtual void Initialize()
        {
            _currentDurability = _maxDurability;
            _isDestroyed = false;
            
            // 스탯 수정자 딕셔너리 초기화
            _statModifiersDict.Clear();
            foreach (var mod in _statModifiers)
            {
                _statModifiersDict[mod.StatType] = mod.Value;
            }
            
            OnInitialized();
        }
        
        public virtual void TakeDamage(int damage)
        {
            if (_isDestroyed)
                return;
                
            _currentDurability = Mathf.Max(0, _currentDurability - damage);
            
            if (_currentDurability <= 0)
            {
                _isDestroyed = true;
                OnDestruction();
            }
            
            OnDamaged(damage);
        }
        
        public virtual void OnDestruction()
        {
            // 파트 파괴 처리
            Debug.Log($"{_partName} 파트가 파괴되었습니다!");
            // 이벤트 발생 등 추가 로직
        }
        
        public virtual void Repair(int amount)
        {
            if (_isDestroyed)
            {
                // 완전히 파괴된 파트는 수리할 수 없음
                Debug.Log($"{_partName} 파트는 완전히 파괴되어 수리할 수 없습니다.");
                return;
            }
            
            _currentDurability = Mathf.Min(_maxDurability, _currentDurability + amount);
            
            Debug.Log($"{_partName} 파트가 수리되었습니다. 현재 내구도: {_currentDurability}/{_maxDurability}");
            OnRepaired(amount);
        }
        
        // 추가 가상 메서드
        protected virtual void OnInitialized()
        {
            // 초기화 완료 후 호출되는 메서드
        }
        
        protected virtual void OnDamaged(int damageAmount)
        {
            // 데미지를 받은 후 호출되는 메서드
        }
        
        protected virtual void OnRepaired(int repairAmount)
        {
            // 수리 후 호출되는 메서드
        }
    }
    
    /// <summary>
    /// 스탯 수정자 데이터 구조체
    /// </summary>
    [System.Serializable]
    public struct StatModifierData
    {
        public StatType StatType;
        public int Value;
    }
} 