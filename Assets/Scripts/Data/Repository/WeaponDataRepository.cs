using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ProjectFM.Data.Interfaces;
using ProjectFM.Mecha.Enums;

namespace ProjectFM.Data.Repository
{
    /// <summary>
    /// 무기 데이터 저장소를 정의하는 클래스입니다.
    /// 모든 무기 데이터에 접근하는 중앙 지점입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponDataRepository", menuName = "ProjectFM/Repositories/WeaponDataRepository")]
    public class WeaponDataRepository : ScriptableObject, IDataAssetRepository<WeaponData>
    {
        [SerializeField] private List<WeaponData> _weapons = new List<WeaponData>();
        
        private Dictionary<string, WeaponData> _weaponsById = new Dictionary<string, WeaponData>();
        private Dictionary<WeaponType, List<WeaponData>> _weaponsByType = new Dictionary<WeaponType, List<WeaponData>>();
        private Dictionary<string, List<WeaponData>> _weaponsByCategory = new Dictionary<string, List<WeaponData>>();
        private bool _isInitialized = false;
        
        /// <summary>
        /// 모든 무기 데이터를 가져옵니다.
        /// </summary>
        public IEnumerable<IWeaponData> GetAllWeapons()
        {
            Initialize();
            return _weapons;
        }
        
        /// <summary>
        /// ID로 무기 데이터를 가져옵니다.
        /// </summary>
        public IWeaponData GetWeaponById(string id)
        {
            Initialize();
            if (_weaponsById.TryGetValue(id, out WeaponData weapon))
            {
                return weapon;
            }
            
            Debug.LogWarning($"무기 ID '{id}'를 찾을 수 없습니다.");
            return null;
        }
        
        /// <summary>
        /// 무기 타입별로 무기 데이터를 가져옵니다.
        /// </summary>
        public IEnumerable<IWeaponData> GetWeaponsByType(WeaponType type)
        {
            Initialize();
            if (_weaponsByType.TryGetValue(type, out List<WeaponData> weapons))
            {
                return weapons;
            }
            
            return new List<IWeaponData>();
        }
        
        /// <summary>
        /// 리포지토리를 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized) return;
            
            _weaponsById.Clear();
            _weaponsByType.Clear();
            _weaponsByCategory.Clear();
            
            foreach (var weapon in _weapons)
            {
                if (weapon == null) continue;
                
                // ID별 매핑
                if (!string.IsNullOrEmpty(weapon.Id))
                {
                    _weaponsById[weapon.Id] = weapon;
                }
                else
                {
                    Debug.LogWarning($"무기 '{weapon.name}'에 ID가 없습니다.");
                }
                
                // 타입별 매핑
                if (!_weaponsByType.ContainsKey(weapon.Type))
                {
                    _weaponsByType[weapon.Type] = new List<WeaponData>();
                }
                
                _weaponsByType[weapon.Type].Add(weapon);
                
                // 카테고리별 매핑 (타입을 문자열로 사용)
                string category = weapon.Type.ToString();
                if (!_weaponsByCategory.ContainsKey(category))
                {
                    _weaponsByCategory[category] = new List<WeaponData>();
                }
                
                _weaponsByCategory[category].Add(weapon);
            }
            
            _isInitialized = true;
            Debug.Log($"무기 데이터 저장소 초기화 완료. 총 {_weapons.Count}개의 무기가 로드되었습니다.");
        }
        
        // 런타임에 무기 추가
        public void AddWeapon(WeaponData weapon)
        {
            if (weapon == null) return;
            
            Initialize();
            
            if (!_weapons.Contains(weapon))
            {
                _weapons.Add(weapon);
                
                // ID별 매핑 업데이트
                if (!string.IsNullOrEmpty(weapon.Id))
                {
                    _weaponsById[weapon.Id] = weapon;
                }
                
                // 타입별 매핑 업데이트
                if (!_weaponsByType.ContainsKey(weapon.Type))
                {
                    _weaponsByType[weapon.Type] = new List<WeaponData>();
                }
                
                _weaponsByType[weapon.Type].Add(weapon);
                
                // 카테고리별 매핑 업데이트
                string category = weapon.Type.ToString();
                if (!_weaponsByCategory.ContainsKey(category))
                {
                    _weaponsByCategory[category] = new List<WeaponData>();
                }
                
                _weaponsByCategory[category].Add(weapon);
                
                Debug.Log($"무기 '{weapon.Name}'이(가) 저장소에 추가되었습니다.");
            }
        }
        
        #region IDataAssetRepository 인터페이스 구현
        
        /// <summary>
        /// 모든 데이터 에셋을 로드합니다.
        /// </summary>
        public void LoadAllAssets()
        {
            Initialize();
        }
        
        /// <summary>
        /// ID로 특정 데이터 에셋을 가져옵니다.
        /// </summary>
        public WeaponData GetById(string id)
        {
            Initialize();
            if (_weaponsById.TryGetValue(id, out WeaponData weapon))
            {
                return weapon;
            }
            return null;
        }
        
        /// <summary>
        /// 모든 데이터 에셋을 가져옵니다.
        /// </summary>
        public IReadOnlyList<WeaponData> GetAll()
        {
            Initialize();
            return _weapons.AsReadOnly();
        }
        
        /// <summary>
        /// 특정 카테고리에 속한 데이터 에셋을 가져옵니다.
        /// </summary>
        public IReadOnlyList<WeaponData> FindByCategory(string category)
        {
            Initialize();
            if (_weaponsByCategory.TryGetValue(category, out List<WeaponData> weapons))
            {
                return weapons.AsReadOnly();
            }
            return new List<WeaponData>().AsReadOnly();
        }
        
        /// <summary>
        /// 특정 데이터 에셋이 존재하는지 확인합니다.
        /// </summary>
        public bool Exists(string id)
        {
            Initialize();
            return _weaponsById.ContainsKey(id);
        }
        
        /// <summary>
        /// 현재 로드된 에셋의 수를 반환합니다.
        /// </summary>
        public int Count 
        { 
            get 
            {
                Initialize();
                return _weapons.Count;
            }
        }
        
        #endregion
    }
} 