using AasanApis.Models;

namespace AasanApis.Data.Repositories
{
    public interface IAasanRepository
    {
        Task<string> InsertAasanResponseLog(AasanResponseLogDTO satnaResponseLogDTO);
        Task<string> InsertAasanRequestLog(AasanRequestLogDTO satnaRequestLog);
    }
}
