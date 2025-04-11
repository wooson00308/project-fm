using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.UI.Interfaces
{
    /// <summary>
    /// UI 패널 인터페이스
    /// </summary>
    public interface IUIPanel
    {
        // 기본 속성
        string PanelName { get; }
        bool IsActive { get; }
        Canvas ParentCanvas { get; }
        List<IUIElement> Elements { get; }
        
        // 메서드
        void Initialize();
        void Show();
        void Hide();
        void Update();
        void AddElement(IUIElement element);
        void RemoveElement(IUIElement element);
        IUIElement GetElement(string elementName);
    }
} 