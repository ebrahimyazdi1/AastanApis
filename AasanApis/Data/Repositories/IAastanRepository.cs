using AasanApis.Data.Entities;
using AasanApis.Models;


namespace AasanApis.Data.Repositories
{
    public interface IAastanRepository
    {
        Task<string> InsertAastanResponseLog(AastanResponseLogDTO satnaResponseLogDTO);
        Task<string> InsertAastanRequestLog(AastanRequestLogDTO satnaRequestLog);
        Task AddOrUpdateTokenAsync(string? accessToken);
        Task InsertShahkarRequestsLog(ShahkarRequestsLogDTO shahkarRequestsLogDTO);
        Task UpdateShahkarRequestsLog(UpdateShahkarRequestsLogDTO updateShahkarRequests);
        Task<string> FindRefreshToken();
        Task<string> FindToken();
        Task<string> FindRefreshTokenById(string accessToken);
        Task<ShahkarRequestsLogEntity> FindAccessToken();
        Task<ShahkarRequestsLogEntity> UpdateShahkarRequestLogTokenAsync(TokenRes tokenRes, ShahkarRequestsLogEntity shahkarRequestsLogEntity);
    }
}
