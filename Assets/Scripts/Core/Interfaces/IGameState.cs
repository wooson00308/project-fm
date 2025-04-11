using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Core.Interfaces
{
    /// <summary>
    /// 게임 상태 인터페이스
    /// </summary>
    public interface IGameState
    {
        // 기본 속성
        string StateName { get; }
        bool IsActive { get; }
        
        // 메서드
        void Enter();
        void Exit();
        void Update();
        void FixedUpdate();
        void HandleInput();
    }
} 