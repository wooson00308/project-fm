using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.UI.Interfaces
{
    /// <summary>
    /// 텍스트 로그 뷰의 설정을 정의하는 인터페이스입니다.
    /// </summary>
    public interface IUITextLogViewSettings
    {
        /// <summary>
        /// 글꼴
        /// </summary>
        Font Font { get; set; }
        
        /// <summary>
        /// 폰트 크기
        /// </summary>
        int FontSize { get; set; }
        
        /// <summary>
        /// 텍스트 색상
        /// </summary>
        Color TextColor { get; set; }
        
        /// <summary>
        /// 배경 색상
        /// </summary>
        Color BackgroundColor { get; set; }
        
        /// <summary>
        /// 스크롤 속도
        /// </summary>
        float ScrollSpeed { get; set; }
        
        /// <summary>
        /// 행간
        /// </summary>
        float LineSpacing { get; set; }
        
        /// <summary>
        /// 엔트리당 최대 표시 시간 (초)
        /// </summary>
        float EntryDisplayTime { get; set; }
        
        /// <summary>
        /// 페이드 아웃 시간 (초)
        /// </summary>
        float FadeOutTime { get; set; }
    }
} 