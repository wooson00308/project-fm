using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Abstract;
using ProjectFM.Mecha.Parts;
using ProjectFM.Mecha.Interfaces;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Parts
{
    /// <summary>
    /// 팔 파트 기본 클래스
    /// </summary>
    public class ArmPartBase : MechaPartBase, IArmPart
    {
        [Header("Arm Part Properties")]
        [SerializeField] protected bool _isRightArm = true;
        [SerializeField] protected float _aimStability = 70f;
        [SerializeField] protected float _weaponMountingCapacity = 100f;
        
        // 무기 참조
        protected IWeaponSystem _mountedWeapon;
        
        // IArmPart 인터페이스 구현
        public bool IsWeaponMounted => _mountedWeapon != null;
        public bool IsRightArm => _isRightArm;
        public float AimStability => _aimStability;
        public float WeaponMountingCapacity => _weaponMountingCapacity;
        
        // 초기화 시 추가 작업
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _mountedWeapon = null;
            
            string armType = IsRightArm ? "오른팔" : "왼팔";
            Debug.Log($"{armType} 파트 '{PartName}' 초기화 완료. 안정성: {AimStability}, 무기 장착 용량: {WeaponMountingCapacity}");
        }
        
        // 데미지 받았을 때 추가 작업
        protected override void OnDamaged(int damageAmount)
        {
            base.OnDamaged(damageAmount);
            
            // 데미지에 따른 조준 안정성 감소 시뮬레이션
            float damageRatio = 1.0f - ((float)Durability / MaxDurability);
            float stabilityLoss = _aimStability * damageRatio * 0.6f;
            
            Debug.Log($"{(IsRightArm ? "오른팔" : "왼팔")} 파트 '{PartName}'의 조준 안정성이 {stabilityLoss:F1} 감소했습니다.");
        }
        
        // 파괴 시 추가 작업
        public override void OnDestruction()
        {
            base.OnDestruction();
            
            string armType = IsRightArm ? "오른팔" : "왼팔";
            Debug.Log($"{armType} 파트 '{PartName}'가 파괴되었습니다. 무기 사용이 불가능합니다.");
            
            // 무기가 장착되어 있었다면 파괴 시 탈락 시뮬레이션
            if (IsWeaponMounted)
            {
                string weaponName = _mountedWeapon != null ? _mountedWeapon.WeaponName : "무기";
                Debug.Log($"파괴로 인해 장착된 {weaponName}이(가) 분리되었습니다!");
                UnmountWeapon();
            }
        }
        
        // IArmPart 메서드 구현
        public bool MountWeapon(IWeaponSystem weapon)
        {
            if (IsDestroyed)
            {
                Debug.Log($"{(IsRightArm ? "오른팔" : "왼팔")}이 파괴되어 무기를 장착할 수 없습니다.");
                return false;
            }
            
            if (weapon == null)
            {
                Debug.Log("장착할 무기가 없습니다.");
                return false;
            }
            
            if (IsWeaponMounted)
            {
                Debug.Log($"이미 {_mountedWeapon.WeaponName}이(가) 장착되어 있습니다. 먼저 제거해주세요.");
                return false;
            }
            
            // 무기 무게가 장착 용량을 초과하는지 확인
            if (weapon.Weight > WeaponMountingCapacity)
            {
                Debug.Log($"무기 {weapon.WeaponName}의 무게({weapon.Weight})가 장착 용량({WeaponMountingCapacity})을 초과합니다.");
                return false;
            }
            
            // 무기 장착
            _mountedWeapon = weapon;
            Debug.Log($"{(IsRightArm ? "오른팔" : "왼팔")}에 {weapon.WeaponName}을(를) 장착했습니다.");
            
            return true;
        }
        
        public void UnmountWeapon()
        {
            if (!IsWeaponMounted)
            {
                Debug.Log($"{(IsRightArm ? "오른팔" : "왼팔")}에 장착된 무기가 없습니다.");
                return;
            }
            
            string weaponName = _mountedWeapon.WeaponName;
            _mountedWeapon = null;
            
            Debug.Log($"{(IsRightArm ? "오른팔" : "왼팔")}에서 {weaponName}을(를) 제거했습니다.");
        }
        
        public IWeaponSystem GetMountedWeapon()
        {
            return _mountedWeapon;
        }
        
        public float GetAccuracyBonus()
        {
            if (IsDestroyed)
                return -30f; // 파괴된 경우 큰 정확도 페널티
                
            // 기본 정확도 보너스 계산
            float bonus = AimStability * 0.2f;
            
            // 팔 상태에 따른 보너스 조정
            float damageRatio = 1.0f - ((float)Durability / MaxDurability);
            bonus *= (1.0f - damageRatio * 0.8f);
            
            // 무기 장착 여부에 따른 보너스 조정
            if (IsWeaponMounted)
            {
                // 무기 무게에 따른 보너스 감소
                float weightFactor = _mountedWeapon.Weight / WeaponMountingCapacity;
                bonus -= weightFactor * 10f;
                
                // 장시간 사용에 따른 피로 시뮬레이션
                if (Random.value < 0.1f)
                {
                    bonus -= Random.Range(0f, 5f);
                    Debug.Log("팔의 피로로 인해 정확도가 약간 감소했습니다.");
                }
            }
            
            return Mathf.Clamp(bonus, -30f, 30f);
        }
    }
} 