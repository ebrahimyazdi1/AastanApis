using AasanApis.Models;


namespace AasanApis.Data.Repositories
{
    public interface IAastanRepository
    {
        Task<string> InsertAastanResponseLog(AastanResponseLogDTO satnaResponseLogDTO);
        Task<string> InsertAastanRequestLog(AastanRequestLogDTO satnaRequestLog);
    }
}
