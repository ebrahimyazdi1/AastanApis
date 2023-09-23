

using AasanApis.Models;

namespace AasanApis.Services
{
    public interface IAastanService
    {
        Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLog);
        Task<OutputModel> GetRefreshTokenAsync(RefreshTokenReqDTO refreshTokenReq);
        Task<OutputModel> GetMatchingEncryptedAsync(MatchingEncryptReqDTO matchingEncryptReq);
    }
}
