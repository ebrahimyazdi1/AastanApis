using AasanApis.Models;

namespace AasanApis.Services
{
    public interface IAastanClient
    {
        Task<TokenRes> GetTokenAsync();
        Task<RefreshTokenRes> GetRefreshTokenAsync(RefreshTokenReq refreshTokenReq);
        Task<MatchingEncryptRes> GetMatchingEncryptedAsync(MatchingEncryptReq matchingEncryptReq);
    }
}
