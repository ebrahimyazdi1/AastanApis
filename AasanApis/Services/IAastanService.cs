using AastanApis.Models;

namespace AasanApis.Services
{
    public interface IAastanService
    {
        Task<OutputModel> GetTokenAsync();
        Task<OutputModel> GetRefreshToken();
        Task<OutputModel> GetMatchingEncrypted();
    }
}
