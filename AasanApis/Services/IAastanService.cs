using AastanApis.Models;

namespace AastanApis.Services
{
    public interface IAastanService
    {
        Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLog);
        Task<OutputModel> GetRefreshTokenAsync(RefreshTokenReqDTO refreshTokenReq);
        Task<OutputModel> GetMatchingEncryptedAsync(MatchingEncryptReqDTO matchingEncryptReq);
        Task<OutputModel> GetPgsbTokenAsync(BasePublicLogData basePublicLogData);
        Task<OutputModel> PostConsentInquiryAsync(ConsentInquiryReqDto request);
        Task<OutputModel> PostCriminalRecordAsync(CriminalRecordReqDto criminalRecordRequest);
    }
}
