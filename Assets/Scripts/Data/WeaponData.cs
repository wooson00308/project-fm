using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectFM.Mecha.Enums;
using ProjectFM.Data.Interfaces;

namespace ProjectFM.Data
{
    /// <summary>
    /// 무기 데이터를 정의하는 ScriptableObject입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "ProjectFM/Weapons/WeaponData")]
    public class WeaponData : ScriptableObject, IWeaponData
    {
        [Header("기본 정보")]
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [TextArea(3, 5)]
        [SerializeField] private string _description;
        [SerializeField] private WeaponType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _modelPrefabPath;
        
        [Header("무기 스탯")]
        [SerializeField] private int _baseDamage = 10;
        [SerializeField] private int _range = 5;
        [SerializeField] private float _weight = 50f;
        [SerializeField] private int _energyCost = 5;
        
        [Header("탄약 설정")]
        [SerializeField] private int _maxAmmo = 30;
        [SerializeField] private float _reloadTime = 2f;
        
        [Header("명중 및 크리티컬")]
        [SerializeField] private int _accuracyBonus = 0;
        [SerializeField, Range(0f, 1f)] private float _criticalChance = 0.05f;
        [SerializeField, Range(1f, 3f)] private float _criticalDamageMultiplier = 1.5f;
        
        [Header("장착 제한")]
        [SerializeField] private bool _canMountOnRightArm = true;
        [SerializeField] private bool _canMountOnLeftArm = true;
        
        [Header("특수 효과")]
        [SerializeReference] private ISpecialEffect[] _specialEffects;
        
        // IWeaponData 인터페이스 구현
        public string Id => _id;
        public string Name => _name;
        public string Description => _description;
        public WeaponType Type => _type;
        public int BaseDamage => _baseDamage;
        public int Range => _range;
        public float Weight => _weight;
        public int EnergyCost => _energyCost;
        public int MaxAmmo => _maxAmmo;
        public float ReloadTime => _reloadTime;
        public int AccuracyBonus => _accuracyBonus;
        public float CriticalChance => _criticalChance;
        public float CriticalDamageMultiplier => _criticalDamageMultiplier;
        public bool CanMountOnRightArm => _canMountOnRightArm;
        public bool CanMountOnLeftArm => _canMountOnLeftArm;
        public ISpecialEffect[] SpecialEffects => _specialEffects;
        public Sprite Icon => _icon;
        public string ModelPrefabPath => _modelPrefabPath;
        
        private void OnValidate()
        {
            // ID가 비어있는 경우 자동 생성
            if (string.IsNullOrEmpty(_id))
            {
                _id = Guid.NewGuid().ToString();
            }
            
            // 이름이 비어있는 경우 기본값 설정
            if (string.IsNullOrEmpty(_name))
            {
                _name = $"New {_type} Weapon";
            }
        }
    }
} 