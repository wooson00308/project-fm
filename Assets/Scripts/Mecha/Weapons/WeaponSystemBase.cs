using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Interfaces;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Mecha.Weapons
{
    /// <summary>
    /// 무기 시스템 기본 클래스
    /// </summary>
    public class WeaponSystemBase : MonoBehaviour, IWeaponSystem
    {
        [Header("Weapon Basic Properties")]
        [SerializeField] protected string _weaponName;
        [SerializeField] protected WeaponType _type;
        [SerializeField] protected int _baseDamage = 10;
        [SerializeField] protected int _range = 5;
        [SerializeField] protected float _weight = 50f;
        
        [Header("Ammo Properties")]
        [SerializeField] protected int _maxAmmo = 30;
        [SerializeField] protected float _reloadTime = 2f;
        [SerializeField] protected bool _isInfiniteAmmo = false;
        
        // 내부 상태 변수
        protected int _currentAmmo;
        protected bool _isReloading = false;
        protected float _reloadTimer = 0f;
        protected bool _isInitialized = false;
        
        // IWeaponSystem 인터페이스 구현
        public string WeaponName => _weaponName;
        public WeaponType Type => _type;
        public int Damage => CalculateFinalDamage();
        public int Range => _range;
        public float Weight => _weight;
        
        // Unity 라이프사이클 메서드
        protected virtual void Awake()
        {
            Initialize();
        }
        
        protected virtual void Update()
        {
            // 재장전 처리
            if (_isReloading)
            {
                _reloadTimer += Time.deltaTime;
                if (_reloadTimer >= _reloadTime)
                {
                    CompleteReload();
                }
            }
        }
        
        // IWeaponSystem 메서드 구현
        public virtual void Initialize()
        {
            _currentAmmo = _maxAmmo;
            _isReloading = false;
            _reloadTimer = 0f;
            _isInitialized = true;
            
            Debug.Log($"무기 시스템 '{_weaponName}' 초기화 완료. 타입: {_type}, 데미지: {_baseDamage}, 사거리: {_range}");
        }
        
        public virtual void Fire(Vector3 targetPosition)
        {
            if (!CanFire())
            {
                Debug.Log($"무기 '{_weaponName}'을(를) 발사할 수 없습니다.");
                return;
            }
            
            // 발사 로직
            Debug.Log($"무기 '{_weaponName}'을(를) 발사했습니다. 목표 위치: {targetPosition}");
            OnFire(targetPosition);
            
            // 탄약 소모 처리
            if (!_isInfiniteAmmo)
            {
                _currentAmmo--;
                if (_currentAmmo <= 0)
                {
                    Debug.Log($"무기 '{_weaponName}'의 탄약이 소진되었습니다. 재장전이 필요합니다.");
                }
            }
        }
        
        public virtual bool CanFire()
        {
            // 발사 가능 여부 확인
            if (!_isInitialized)
            {
                Debug.LogWarning($"무기 '{_weaponName}'이(가) 초기화되지 않았습니다.");
                return false;
            }
            
            if (_isReloading)
            {
                Debug.Log($"무기 '{_weaponName}'이(가) 재장전 중입니다.");
                return false;
            }
            
            if (!_isInfiniteAmmo && _currentAmmo <= 0)
            {
                Debug.Log($"무기 '{_weaponName}'의 탄약이 부족합니다.");
                return false;
            }
            
            return true;
        }
        
        public virtual void Reload()
        {
            if (_isReloading)
            {
                Debug.Log($"무기 '{_weaponName}'이(가) 이미 재장전 중입니다.");
                return;
            }
            
            if (_currentAmmo == _maxAmmo)
            {
                Debug.Log($"무기 '{_weaponName}'의 탄약이 이미 최대치입니다.");
                return;
            }
            
            _isReloading = true;
            _reloadTimer = 0f;
            Debug.Log($"무기 '{_weaponName}'의 재장전을 시작합니다. 재장전 시간: {_reloadTime}초");
            
            OnReloadStart();
        }
        
        public virtual int GetAmmoRemaining()
        {
            return _isInfiniteAmmo ? int.MaxValue : _currentAmmo;
        }
        
        // 보조 메서드
        protected virtual void CompleteReload()
        {
            _isReloading = false;
            _currentAmmo = _maxAmmo;
            Debug.Log($"무기 '{_weaponName}'의 재장전이 완료되었습니다. 현재 탄약: {_currentAmmo}/{_maxAmmo}");
            
            OnReloadComplete();
        }
        
        protected virtual int CalculateFinalDamage()
        {
            // 기본 데미지 계산 로직 (확장 가능)
            return _baseDamage;
        }
        
        // 이벤트 처리 메서드 (상속 클래스에서 오버라이드)
        protected virtual void OnFire(Vector3 targetPosition)
        {
            // 발사 시 추가 효과
        }
        
        protected virtual void OnReloadStart()
        {
            // 재장전 시작 시 추가 효과
        }
        
        protected virtual void OnReloadComplete()
        {
            // 재장전 완료 시 추가 효과
        }
    }
} 