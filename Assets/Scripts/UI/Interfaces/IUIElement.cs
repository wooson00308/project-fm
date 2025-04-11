using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.UI.Interfaces
{
    /// <summary>
    /// UI 요소 인터페이스
    /// </summary>
    public interface IUIElement
    {
        // 기본 속성
        string ElementName { get; }
        IUIPanel ParentPanel { get; }
        bool IsVisible { get; }
        RectTransform RectTransform { get; }
        
        // 메서드
        void Initialize();
        void Show();
        void Hide();
        void SetData(object data);
        void OnParentPanelShow();
        void OnParentPanelHide();
    }
} 