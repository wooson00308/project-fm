using System;
using System.Collections.Generic;

namespace ProjectFM.Core.Interfaces
{
    /// <summary>
    /// 서비스 로케이터 패턴을 구현하는 인터페이스입니다.
    /// 프로젝트 전반에 걸쳐 사용되는 서비스의 종속성을 관리합니다.
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// 서비스를 등록합니다.
        /// </summary>
        /// <typeparam name="T">등록할 서비스 타입</typeparam>
        /// <param name="service">서비스 인스턴스</param>
        void RegisterService<T>(T service) where T : class;
        
        /// <summary>
        /// 등록된 서비스를 가져옵니다.
        /// </summary>
        /// <typeparam name="T">가져올 서비스 타입</typeparam>
        /// <returns>서비스 인스턴스</returns>
        /// <exception cref="InvalidOperationException">서비스가 등록되지 않은 경우 발생</exception>
        T GetService<T>() where T : class;
        
        /// <summary>
        /// 특정 타입의 서비스가 등록되어 있는지 확인합니다.
        /// </summary>
        /// <typeparam name="T">확인할 서비스 타입</typeparam>
        /// <returns>서비스 등록 여부</returns>
        bool HasService<T>() where T : class;
    }
} 