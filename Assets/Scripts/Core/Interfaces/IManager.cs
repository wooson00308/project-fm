using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Core.Interfaces
{
    /// <summary>
    /// 매니저 인터페이스
    /// </summary>
    public interface IManager
    {
        // 기본 속성
        string ManagerName { get; }
        bool IsInitialized { get; }
        
        // 메서드
        void Initialize();
        void Shutdown();
    }
} 