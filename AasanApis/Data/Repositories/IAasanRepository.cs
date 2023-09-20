using AastanApis.Models;

namespace AastanApis.Data.Repositories
{
    public interface IAastanRepository
    {
        Task<string> InsertAastanResponseLog(AastanResponseLogDTO satnaResponseLogDTO);
        Task<string> InsertAastanRequestLog(AastanRequestLogDTO satnaRequestLog);
    }
}
