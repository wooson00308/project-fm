namespace ProjectFM.Data.Interfaces
{
    /// <summary>
    /// 게임 진행 저장 데이터에 대한 인터페이스입니다.
    /// </summary>
    public interface IProgressSaveData
    {
        /// <summary>
        /// 완료한 미션 ID 배열
        /// </summary>
        string[] CompletedMissions { get; }
        
        /// <summary>
        /// 현재 활성화된 미션 ID 배열
        /// </summary>
        string[] ActiveMissions { get; }
        
        /// <summary>
        /// 게임 플레이 시간 (초)
        /// </summary>
        float PlayTimeSeconds { get; }
    }
} 