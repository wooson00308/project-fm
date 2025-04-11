using System.Collections.Generic;
using UnityEngine;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// ScriptableObject 기반 데이터 에셋 저장소에 대한 인터페이스입니다.
    /// 특정 타입의 ScriptableObject 데이터를 관리하기 위한 기본 구조를 정의합니다.
    /// </summary>
    /// <typeparam name="T">관리할 ScriptableObject 타입</typeparam>
    public interface IDataAssetRepository<T> where T : ScriptableObject
    {
        /// <summary>
        /// 모든 데이터 에셋을 로드합니다.
        /// </summary>
        void LoadAllAssets();
        
        /// <summary>
        /// ID로 특정 데이터 에셋을 가져옵니다.
        /// </summary>
        /// <param name="id">데이터 에셋의 고유 ID</param>
        /// <returns>해당 ID를 가진 데이터 에셋</returns>
        T GetById(string id);
        
        /// <summary>
        /// 모든 데이터 에셋을 가져옵니다.
        /// </summary>
        /// <returns>모든 데이터 에셋 목록</returns>
        IReadOnlyList<T> GetAll();
        
        /// <summary>
        /// 특정 카테고리에 속한 데이터 에셋을 가져옵니다.
        /// </summary>
        /// <param name="category">검색할 카테고리</param>
        /// <returns>해당 카테고리의 데이터 에셋 목록</returns>
        IReadOnlyList<T> FindByCategory(string category);
        
        /// <summary>
        /// 특정 데이터 에셋이 존재하는지 확인합니다.
        /// </summary>
        /// <param name="id">확인할 데이터 에셋의 ID</param>
        /// <returns>존재 여부</returns>
        bool Exists(string id);
        
        /// <summary>
        /// 현재 로드된 에셋의 수를 반환합니다.
        /// </summary>
        int Count { get; }
    }
} 