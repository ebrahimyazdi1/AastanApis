using AasanApis.Data.Entities;
using AasanApis.Models;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace AasanApis.Data.Repositories
{
    public class AastanRepository : IAastanRepository
    {

        public IConfiguration _configuration { get; set; }
        private readonly ILogger<AastanRepository> _logger;
        private readonly AastanDbContext _dbContext;
        public AastanRepository(AastanDbContext dbContext, ILogger<AastanRepository> logger, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<string> InsertAastanRequestLog(AastanRequestLogDTO RequestLog)
        {
            string requestId = Guid.NewGuid().ToString();
            AastanReqLog ReqLog = new AastanReqLog
            {
                Id = requestId,
                LogDateTime = DateTime.Now,
                JsonReq = RequestLog.jsonRequest,
                UserId = RequestLog.userId,
                PublicAppId = RequestLog.publicAppId,
                ServiceId = RequestLog.serviceId,
                PublicReqId = RequestLog.publicRequestId
            };
            _dbContext.AastanReqLogs.Add(ReqLog);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return requestId;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(InsertAastanRequestLog)}");
                throw new ApplicationException($"Exception occurred while: {nameof(InsertAastanRequestLog)}  => {ex.Message}");
            }
        }

        public async Task<string> InsertAastanResponseLog(AastanResponseLogDTO ResponseLogDTO)
        {
            string responseId = Guid.NewGuid().ToString();
            AastanResLog resLog = new AastanResLog
            {
                Id = responseId,
                HTTPStatusCode = ResponseLogDTO.asanHttpResponseCode,
                JsonRes = ResponseLogDTO.jsonResponse,
                PublicReqId = ResponseLogDTO.publicRequestId,
                ReqLogId = ResponseLogDTO.asanRequestId,
                ResCode = ResponseLogDTO.asanResCode
            };
            _dbContext.AastanResLogs.Add(resLog);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return responseId;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(InsertAastanResponseLog)}");
                throw new ApplicationException($"Exception occurred while: {nameof(InsertAastanResponseLog)}  => {ex.Message}");
            }
        }
    }
}
