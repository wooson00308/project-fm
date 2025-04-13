using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Abstract;
using ProjectFM.Mecha.Parts;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 헤드 파트 기본 클래스
    /// </summary>
    public class HeadPartBase : MechaPartBase, IHeadPart
    {
        [Header("Head Part Properties")]
        [SerializeField] protected int _sensorRange = 5;
        [SerializeField] protected float _targetingAccuracy = 75f;
        [SerializeField] protected bool _hasNightVision = false;
        [SerializeField] protected int _detectionLevel = 2;
        
        // IHeadPart 인터페이스 구현
        public int SensorRange => _sensorRange;
        public float TargetingAccuracy => _targetingAccuracy;
        public bool HasNightVision => _hasNightVision;
        public int DetectionLevel => _detectionLevel;
        
        // 초기화 시 추가 작업
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Debug.Log($"헤드 파트 '{PartName}' 초기화 완료. 센서 범위: {SensorRange}, 정확도: {TargetingAccuracy}");
        }
        
        // 데미지 받았을 때 추가 작업
        protected override void OnDamaged(int damageAmount)
        {
            base.OnDamaged(damageAmount);
            
            // 데미지에 따른 센서 기능 감소 시뮬레이션
            float damageRatio = 1.0f - ((float)Durability / MaxDurability);
            Debug.Log($"헤드 파트 '{PartName}'의 센서 성능이 {damageRatio * 100}% 감소했습니다.");
        }
        
        // 파괴 시 추가 작업
        public override void OnDestruction()
        {
            base.OnDestruction();
            Debug.Log($"헤드 파트 '{PartName}'가 파괴되었습니다. 센서 시스템이 오프라인 상태가 되었습니다.");
        }
        
        // IHeadPart 메서드 구현
        public bool ScanArea(Vector3 position, float radius)
        {
            if (IsDestroyed)
            {
                Debug.Log("센서가 파괴되어 스캔할 수 없습니다.");
                return false;
            }
            
            // 센서 범위 내에 있는지 확인
            float distance = Vector3.Distance(transform.position, position);
            bool inRange = distance <= SensorRange * radius;
            
            Debug.Log($"영역 스캔 결과: {(inRange ? "감지됨" : "범위 초과")}");
            return inRange;
        }
        
        public float CalculateAccuracyModifier(float distance)
        {
            if (IsDestroyed)
                return -50f; // 파괴된 경우 큰 정확도 페널티
                
            // 거리에 따른 정확도 수정자 계산
            float modifier = Mathf.Clamp(TargetingAccuracy - (distance / SensorRange * 25f), -25f, TargetingAccuracy);
            
            // 센서 상태에 따라 수정자 조정
            float damageRatio = 1.0f - ((float)Durability / MaxDurability);
            modifier *= (1.0f - damageRatio * 0.5f);
            
            return modifier;
        }
        
        public bool DetectHiddenTarget(int stealthLevel)
        {
            if (IsDestroyed)
                return false;
                
            // 은신 대상 감지 확률 계산
            int detectionChance = DetectionLevel * 20 - stealthLevel * 15;
            
            // 나이트 비전 보유 시 어두운 환경에서 보너스
            if (HasNightVision)
                detectionChance += 25;
                
            // 손상에 따른 감지 능력 감소
            float damageRatio = 1.0f - ((float)Durability / MaxDurability);
            detectionChance = Mathf.RoundToInt(detectionChance * (1.0f - damageRatio * 0.7f));
            
            // 최종 감지 여부 결정 (0-100 사이의 랜덤값과 비교)
            bool detected = Random.Range(0, 100) < Mathf.Clamp(detectionChance, 5, 95);
            return detected;
        }
    }
} 