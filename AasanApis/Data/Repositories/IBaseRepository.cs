using AasanApis.Data.Entities;

namespace AasanApis.Data.Repositories
{
    public interface IBaseRepository
    {
        Task<string> FindAccessToken();
        Task<AccessTokenEntity> AddOrUpdateTokenAsync(string? accessToken);
    }
}
