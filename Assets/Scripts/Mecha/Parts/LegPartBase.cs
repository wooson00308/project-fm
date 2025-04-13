using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProjectFM.Mecha.Abstract;
using ProjectFM.Mecha.Parts;
using ProjectFM.Mecha.Interfaces;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 다리 파트 기본 클래스
    /// </summary>
    public class LegPartBase : MechaPartBase, ILegPart
    {
        [Header("Leg Part Properties")]
        [SerializeField] protected int _movementPoints = 5;
        [SerializeField] protected float _stabilityFactor = 80f;
        [SerializeField] protected MovementType _movementMode = MovementType.Biped;
        [SerializeField] protected TerrainType[] _supportedTerrains = { TerrainType.Flat, TerrainType.Rough, TerrainType.Urban };
        
        // 기존 필드들은 유지하되 private로 변경
        [Header("Additional Properties")]
        [SerializeField] private float _baseMovementSpeed = 5f;
        [SerializeField] private float _jumpPower = 10f;
        [SerializeField] private bool _isHoverType = false;
        
        // ILegPart 인터페이스 속성 구현
        public int MovementPoints => _movementPoints;
        public float StabilityFactor => _stabilityFactor;
        public TerrainType[] SupportedTerrains => _supportedTerrains;
        public MovementType MovementMode => _movementMode;
        
        // 내부 상태 변수
        protected float _currentStability;
        protected bool _isGrounded = true;
        
        // 초기화 시 추가 작업
        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            // 초기값 설정
            _currentStability = _stabilityFactor;
            
            Debug.Log($"{MovementMode} 타입 다리 파트 '{PartName}' 초기화 완료. 이동력: {MovementPoints}, 안정성: {StabilityFactor}");
        }
        
        // 데미지 받았을 때 추가 작업
        protected override void OnDamaged(int damageAmount)
        {
            base.OnDamaged(damageAmount);
            
            // 데미지에 따른 이동 관련 능력치 감소
            float damageRatio = 1.0f - ((float)Durability / MaxDurability);
            
            // 안정성 감소
            _currentStability = _stabilityFactor * (1.0f - damageRatio * 0.8f);
            
            Debug.Log($"다리 파트 '{PartName}'의 성능이 저하되었습니다. 현재 안정성: {_currentStability:F1}");
            
            // 심각한 손상 시 넘어질 가능성 체크
            if (damageRatio > 0.7f && Random.value < damageRatio * 0.3f)
            {
                Debug.Log("다리 파트의 심각한 손상으로 인해 균형을 잃었습니다!");
                _isGrounded = false;
            }
        }
        
        // 파괴 시 추가 작업
        public override void OnDestruction()
        {
            base.OnDestruction();
            
            // 다리 파트 파괴 시 움직임 불가
            _currentStability = 0f;
            _isGrounded = false;
            
            Debug.Log($"다리 파트 '{PartName}'가 파괴되었습니다. 이동이 불가능합니다.");
        }
        
        // ILegPart 메서드 구현
        public float GetMovementCostModifier(TerrainType terrain)
        {
            if (IsDestroyed)
                return 5.0f; // 파괴된 경우 모든 지형에서 매우 높은 코스트
                
            // 지원하는 지형인지 확인
            if (!CanTraverseTerrainType(terrain))
                return 3.0f; // 지원하지 않는 지형은 높은 코스트
                
            // 이동 타입과 지형에 따른 코스트 조정
            float modifier = 1.0f;
            
            switch (terrain)
            {
                case TerrainType.Flat:
                    modifier = 1.0f;
                    break;
                    
                case TerrainType.Rough:
                    modifier = MovementMode == MovementType.Hover || MovementMode == MovementType.Flight ? 1.2f : 1.5f;
                    break;
                    
                case TerrainType.Water:
                    modifier = MovementMode == MovementType.Hover || MovementMode == MovementType.Flight ? 1.0f : 2.5f;
                    break;
                    
                case TerrainType.Mountain:
                    modifier = MovementMode == MovementType.Flight ? 1.2f : 2.0f;
                    break;
                    
                case TerrainType.Urban:
                    modifier = MovementMode == MovementType.Biped ? 1.0f : 1.3f;
                    break;
                    
                case TerrainType.Desert:
                    modifier = MovementMode == MovementType.Hover || MovementMode == MovementType.Flight ? 1.0f : 1.7f;
                    break;
                    
                case TerrainType.Snow:
                    modifier = MovementMode == MovementType.Hover || MovementMode == MovementType.Flight ? 1.0f : 1.8f;
                    break;
            }
            
            // 내구도에 따른 추가 보정
            float durabilityFactor = (float)Durability / MaxDurability;
            modifier *= (1.0f + (1.0f - durabilityFactor));
            
            return Mathf.Clamp(modifier, 1.0f, 5.0f);
        }
        
        public bool CanTraverseTerrainType(TerrainType terrain)
        {
            if (IsDestroyed)
                return false;
                
            return SupportedTerrains.Contains(terrain) || MovementMode == MovementType.Flight;
        }
        
        public float GetStabilityBonus()
        {
            if (IsDestroyed)
                return 0f;
                
            // 기본 안정성 보너스 계산
            float bonus = _currentStability * 0.2f;
            
            // 이동 타입에 따른 안정성 보정
            switch (MovementMode)
            {
                case MovementType.Biped:
                    bonus *= 1.0f;
                    break;
                    
                case MovementType.Quadruped:
                    bonus *= 1.3f;
                    break;
                    
                case MovementType.Tracks:
                    bonus *= 1.5f;
                    break;
                    
                case MovementType.Wheels:
                    bonus *= 1.2f;
                    break;
                    
                case MovementType.Hover:
                    bonus *= 0.9f;
                    break;
                    
                case MovementType.Flight:
                    bonus *= 0.7f;
                    break;
            }
            
            // 지면 상태에 따른 보너스 조정
            if (!_isGrounded)
            {
                bonus *= 0.5f;
            }
            
            return Mathf.Max(0, bonus);
        }
        
        public int GetJumpDistance()
        {
            if (IsDestroyed || !_isGrounded)
                return 0;
                
            // 기본 점프 거리 계산
            int distance = Mathf.FloorToInt(MovementPoints * 0.5f);
            
            // 이동 타입에 따른 점프 거리 조정
            switch (MovementMode)
            {
                case MovementType.Biped:
                    distance = Mathf.FloorToInt(MovementPoints * 0.6f);
                    break;
                    
                case MovementType.Quadruped:
                    distance = Mathf.FloorToInt(MovementPoints * 0.7f);
                    break;
                    
                case MovementType.Tracks:
                    distance = 0; // 궤도 타입은 점프 불가
                    break;
                    
                case MovementType.Wheels:
                    distance = 1; // 바퀴 타입은 미미한 점프만 가능
                    break;
                    
                case MovementType.Hover:
                    distance = Mathf.FloorToInt(MovementPoints * 0.5f);
                    break;
                    
                case MovementType.Flight:
                    distance = MovementPoints; // 비행 타입은 전체 이동력으로 점프 가능
                    break;
            }
            
            // 내구도에 따른 조정
            float durabilityRatio = (float)Durability / MaxDurability;
            distance = Mathf.FloorToInt(distance * durabilityRatio);
            
            return Mathf.Max(0, distance);
        }
        
        // 기존 메서드들은 유지하되 private 또는 protected로 변경
        
        protected float GetCurrentMovementSpeed()
        {
            if (IsDestroyed)
                return 0f;
                
            float speed = _baseMovementSpeed;
            
            // 지면 상태에 따른 속도 조정
            if (!_isGrounded)
            {
                return speed * 0.2f;
            }
            
            // 내구도에 따른 속도 조정
            float durabilityRatio = (float)Durability / MaxDurability;
            speed *= durabilityRatio;
            
            return speed;
        }
        
        protected float GetCurrentJumpPower()
        {
            if (IsDestroyed || !_isGrounded)
                return 0f;
                
            return _jumpPower * ((float)Durability / MaxDurability);
        }
        
        protected bool CanJump()
        {
            return !IsDestroyed && _isGrounded && GetJumpDistance() > 0;
        }
        
        protected void SetGroundState(bool isGrounded)
        {
            _isGrounded = isGrounded;
            
            if (isGrounded && !IsDestroyed)
            {
                Debug.Log($"다리 파트 '{PartName}'가 지면에 착지했습니다.");
            }
        }
        
        protected float CalculateEvasionBonus()
        {
            if (IsDestroyed)
                return -30f; // 파괴된 경우 큰 회피 페널티
                
            // 기본 회피 보너스 계산
            float bonus = 0f;
            
            // 이동 타입에 따른 회피 보너스
            switch (MovementMode)
            {
                case MovementType.Biped:
                    bonus += 10f;
                    break;
                    
                case MovementType.Quadruped:
                    bonus += 5f;
                    break;
                    
                case MovementType.Tracks:
                    bonus += 0f;
                    break;
                    
                case MovementType.Wheels:
                    bonus += 15f;
                    break;
                    
                case MovementType.Hover:
                    bonus += 20f;
                    break;
                    
                case MovementType.Flight:
                    bonus += 25f;
                    break;
            }
            
            // 이동 포인트에 따른 회피 보너스
            bonus += MovementPoints * 2;
            
            // 안정성에 따른 회피 보너스 조정
            bonus *= (1.0f - (_currentStability / 100f) * 0.3f);
            
            // 다리 상태에 따른 보너스 조정
            float damageRatio = 1.0f - ((float)Durability / MaxDurability);
            bonus *= (1.0f - damageRatio * 0.9f);
            
            // 지면 상태에 따른 보너스 조정
            if (!_isGrounded)
            {
                bonus *= 0.5f;
            }
            
            return Mathf.Clamp(bonus, -30f, 30f);
        }
    }
} 