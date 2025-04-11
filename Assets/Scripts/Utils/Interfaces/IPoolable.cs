using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Utils.Interfaces
{
    /// <summary>
    /// 오브젝트 풀링을 위한 풀링 가능 인터페이스
    /// </summary>
    public interface IPoolable
    {
        // 기본 속성
        bool IsActive { get; }
        string PoolableId { get; }
        
        // 메서드
        void OnSpawn();
        void OnDespawn();
        void ReturnToPool();
    }
} 