using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Abstract;
using ProjectFM.Mecha.Parts;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 백팩 파트 기본 클래스
    /// </summary>
    public class BackpackPartBase : MechaPartBase, IBackpackPart
    {
        [Header("Backpack Part Properties")]
        [SerializeField] protected float _energyCapacity = 1000f;
        [SerializeField] protected float _energyRechargeRate = 10f;
        [SerializeField] protected bool _hasFlightSystem = false;
        [SerializeField] protected bool _hasSpecialEquipment = false;
        [SerializeField] protected string[] _specialAbilities = new string[0];
        
        // 내부 상태 변수
        protected float _currentEnergy;
        protected Dictionary<string, bool> _abilityCooldowns = new Dictionary<string, bool>();
        
        // IBackpackPart 인터페이스 구현
        public float EnergyCapacity => _energyCapacity;
        public float EnergyRechargeRate => _energyRechargeRate;
        public bool HasFlightSystem => _hasFlightSystem;
        public bool HasSpecialEquipment => _hasSpecialEquipment;
        public string[] SpecialAbilities => _specialAbilities;
        
        // 초기화 시 추가 작업
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _currentEnergy = _energyCapacity;
            
            // 능력치 쿨다운 초기화
            _abilityCooldowns.Clear();
            foreach (var ability in _specialAbilities)
            {
                _abilityCooldowns[ability] = false;
            }
            
            Debug.Log($"백팩 파트 '{PartName}' 초기화 완료. 에너지 용량: {EnergyCapacity}, 재충전 속도: {EnergyRechargeRate}");
        }
        
        // 데미지 받았을 때 추가 작업
        protected override void OnDamaged(int damageAmount)
        {
            base.OnDamaged(damageAmount);
            
            // 데미지에 따른 에너지 용량 및 충전 효율 감소 시뮬레이션
            float damageRatio = 1.0f - ((float)Durability / MaxDurability);
            float energyLoss = _currentEnergy * (damageRatio * 0.2f);
            _currentEnergy = Mathf.Max(0, _currentEnergy - energyLoss);
            
            Debug.Log($"백팩 파트 '{PartName}'의 에너지 시스템이 손상되어, {energyLoss:F2} 에너지가 손실되었습니다.");
        }
        
        // 파괴 시 추가 작업
        public override void OnDestruction()
        {
            base.OnDestruction();
            _currentEnergy = 0;
            Debug.Log($"백팩 파트 '{PartName}'가 파괴되었습니다. 에너지 시스템이 중단되었습니다.");
        }
        
        // IBackpackPart 메서드 구현
        public float GetCurrentEnergy()
        {
            return _currentEnergy;
        }
        
        public void ConsumeEnergy(float amount)
        {
            if (IsDestroyed)
            {
                Debug.Log("백팩이 파괴되어 에너지를 소모할 수 없습니다.");
                return;
            }
            
            float actualConsumption = Mathf.Min(_currentEnergy, amount);
            _currentEnergy -= actualConsumption;
            
            Debug.Log($"에너지 소모: {actualConsumption:F2}/{amount:F2}, 남은 에너지: {_currentEnergy:F2}");
        }
        
        public void RechargeEnergy(float amount)
        {
            if (IsDestroyed)
            {
                Debug.Log("백팩이 파괴되어 에너지를 충전할 수 없습니다.");
                return;
            }
            
            float previousEnergy = _currentEnergy;
            _currentEnergy = Mathf.Min(_energyCapacity, _currentEnergy + amount);
            
            Debug.Log($"에너지 충전: {_currentEnergy - previousEnergy:F2}/{amount:F2}, 현재 에너지: {_currentEnergy:F2}");
        }
        
        public bool ActivateSpecialAbility(string abilityName)
        {
            if (IsDestroyed)
            {
                Debug.Log("백팩이 파괴되어 특수 능력을 사용할 수 없습니다.");
                return false;
            }
            
            // 능력 존재 및 쿨다운 확인
            if (!CanUseAbility(abilityName))
                return false;
                
            // 에너지 충분한지 확인 (각 능력마다 다른 에너지 소모량을 가정)
            float energyCost = GetAbilityEnergyCost(abilityName);
            if (_currentEnergy < energyCost)
            {
                Debug.Log($"특수 능력 '{abilityName}' 사용 실패: 에너지 부족 ({_currentEnergy:F2}/{energyCost:F2})");
                return false;
            }
            
            // 에너지 소모
            _currentEnergy -= energyCost;
            
            // 쿨다운 설정
            _abilityCooldowns[abilityName] = true;
            StartCoroutine(ResetCooldown(abilityName));
            
            Debug.Log($"특수 능력 '{abilityName}' 활성화 성공! 남은 에너지: {_currentEnergy:F2}");
            return true;
        }
        
        public bool CanUseAbility(string abilityName)
        {
            if (IsDestroyed)
                return false;
                
            // 해당 능력이 존재하는지 확인
            bool hasAbility = System.Array.IndexOf(_specialAbilities, abilityName) >= 0;
            if (!hasAbility)
            {
                Debug.Log($"특수 능력 '{abilityName}'이 존재하지 않습니다.");
                return false;
            }
            
            // 쿨다운 상태인지 확인
            if (_abilityCooldowns.TryGetValue(abilityName, out bool isOnCooldown) && isOnCooldown)
            {
                Debug.Log($"특수 능력 '{abilityName}'은 현재 쿨다운 중입니다.");
                return false;
            }
            
            return true;
        }
        
        // 자동 에너지 충전 처리
        private void Update()
        {
            if (!IsDestroyed && _currentEnergy < _energyCapacity)
            {
                // 매 프레임마다 에너지 자동 충전
                float rechargeAmount = _energyRechargeRate * Time.deltaTime;
                _currentEnergy = Mathf.Min(_energyCapacity, _currentEnergy + rechargeAmount);
            }
        }
        
        // 내부 헬퍼 메서드
        private float GetAbilityEnergyCost(string abilityName)
        {
            // 실제 구현에서는 능력별로 다른 비용을 반환할 수 있음
            // 이 예제에서는 단순화를 위해 고정 비용을 사용
            return 50f;
        }
        
        private IEnumerator ResetCooldown(string abilityName)
        {
            // 일정 시간 대기 (이 예제에서는 모든 능력의 쿨다운을 5초로 설정)
            yield return new WaitForSeconds(5f);
            
            if (_abilityCooldowns.ContainsKey(abilityName))
            {
                _abilityCooldowns[abilityName] = false;
                Debug.Log($"특수 능력 '{abilityName}'의 쿨다운이 완료되었습니다.");
            }
        }
    }
} 