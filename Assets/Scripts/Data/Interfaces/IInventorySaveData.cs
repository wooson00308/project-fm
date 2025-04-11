using System.Collections.Generic;

namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 인벤토리 저장 데이터에 대한 인터페이스입니다.
    /// </summary>
    public interface IInventorySaveData
    {
        /// <summary>
        /// 보유한 파츠 ID 배열
        /// </summary>
        string[] OwnedParts { get; }
        
        /// <summary>
        /// 자원 사전
        /// </summary>
        IDictionary<string, int> Resources { get; }
    }
} 