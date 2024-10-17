using AasanApis.Models;
using AastanApis.Models;

namespace AastanApis.Services
{
    public interface IAastanClient
    {
        Task<TokenRes> GetTokenAsync();
        Task<RefreshTokenRes> GetRefreshTokenAsync(RefreshTokenReq refreshTokenReq);
        Task<MatchingEncryptRes> GetMatchingEncryptedAsync(MatchingEncryptReq matchingEncryptReq);
        Task<PgsbTokenRes> GetPgsbTokenAsync();
        Task<ConsentInquiryResDto> PostConsentInquiryAsync(ConsentInquiryReqDto consentInquiryRequest);
        Task<CriminalRecordResDto> PostCriminalRecordAsync(CriminalRecordReqDto criminalRecordRequest);
    }
}
