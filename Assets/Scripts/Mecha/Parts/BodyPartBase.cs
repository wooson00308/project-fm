using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Abstract;
using ProjectFM.Mecha.Parts;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 바디 파트 기본 클래스
    /// </summary>
    public class BodyPartBase : MechaPartBase, IBodyPart
    {
        [Header("Body Part Properties")]
        [SerializeField] protected int _maxCoreHitPoints = 200;
        [SerializeField] protected float _weightCapacity = 1000f;
        [SerializeField] protected bool _hasLifeSupportSystems = true;
        
        // 내부 상태 변수
        protected int _coreHitPoints;
        protected bool _coreSystemsOnline = true;
        protected float _powerOutputEfficiency = 1.0f;
        
        // IBodyPart 인터페이스 구현
        public int CoreHitPoints => _coreHitPoints;
        public int MaxCoreHitPoints => _maxCoreHitPoints;
        public float WeightCapacity => _weightCapacity;
        public bool HasLifeSupportSystems => _hasLifeSupportSystems;
        
        // 초기화 시 추가 작업
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _coreHitPoints = _maxCoreHitPoints;
            _coreSystemsOnline = true;
            _powerOutputEfficiency = 1.0f;
            
            Debug.Log($"바디 파트 '{PartName}' 초기화 완료. 코어 HP: {CoreHitPoints}, 무게 용량: {WeightCapacity}");
        }
        
        // 데미지 받았을 때 추가 작업
        protected override void OnDamaged(int damageAmount)
        {
            base.OnDamaged(damageAmount);
            
            // 일부 데미지가 코어 시스템에도 영향을 줌
            float coreSystemDamage = damageAmount * 0.5f;
            ApplyCoreSystemDamage(Mathf.RoundToInt(coreSystemDamage));
        }
        
        // 파괴 시 추가 작업
        public override void OnDestruction()
        {
            base.OnDestruction();
            _coreSystemsOnline = false;
            _coreHitPoints = 0;
            
            Debug.Log($"바디 파트 '{PartName}'가 파괴되었습니다. 모든 코어 시스템이 오프라인 상태가 되었습니다.");
        }
        
        // IBodyPart 메서드 구현
        public void ApplyCoreSystemDamage(int damage)
        {
            if (!_coreSystemsOnline)
                return;
                
            _coreHitPoints = Mathf.Max(0, _coreHitPoints - damage);
            float coreIntegrity = (float)_coreHitPoints / _maxCoreHitPoints;
            _powerOutputEfficiency = Mathf.Max(0.3f, coreIntegrity);
            
            Debug.Log($"코어 시스템 데미지: {damage}, 남은 코어 HP: {_coreHitPoints}/{_maxCoreHitPoints}");
            
            if (_coreHitPoints <= 0)
            {
                _coreSystemsOnline = false;
                Debug.Log("코어 시스템이 치명적 손상을 입어 오프라인 상태가 되었습니다!");
            }
        }
        
        public bool CheckSystemStatus()
        {
            string status = _coreSystemsOnline ? "정상 작동 중" : "오프라인";
            float integrityPercent = (float)_coreHitPoints / _maxCoreHitPoints * 100f;
            
            Debug.Log($"시스템 상태: {status}, 코어 무결성: {integrityPercent:F1}%, 전력 출력: {_powerOutputEfficiency * 100:F1}%");
            
            // 라이프 서포트 시스템 상태 확인
            if (HasLifeSupportSystems)
            {
                bool lifeSupportOnline = _coreSystemsOnline && _coreHitPoints > _maxCoreHitPoints * 0.2f;
                Debug.Log($"생명 유지 시스템: {(lifeSupportOnline ? "작동 중" : "위험")}");
            }
            
            return _coreSystemsOnline;
        }
        
        public float GetPowerOutput()
        {
            float baseOutput = 100f;
            
            // 코어 시스템 손상에 따른 출력 조정
            float actualOutput = baseOutput * _powerOutputEfficiency;
            
            // 심각한 데미지 시 출력 불안정
            if (_coreHitPoints < _maxCoreHitPoints * 0.3f && _coreSystemsOnline)
            {
                // 출력 변동 시뮬레이션
                actualOutput *= Random.Range(0.8f, 1.1f);
                Debug.Log("경고: 전력 출력 불안정!");
            }
            
            Debug.Log($"현재 전력 출력: {actualOutput:F1} / {baseOutput}");
            return actualOutput;
        }
    }
} 