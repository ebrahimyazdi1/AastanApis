using AasanApis.Data.Entities;
using AasanApis.ErrorHandling;
using AasanApis.Exceptions;
using AasanApis.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Oracle.ManagedDataAccess.Client;

namespace AasanApis.Data.Repositories
{
    public class AastanRepository : IAastanRepository
    {
        public IConfiguration _configuration { get; set; }
        private readonly ILogger<AastanRepository> _logger;
        private readonly AastanDbContext _dbContext;
        private readonly IMapper _mapper;
        public AastanRepository(AastanDbContext dbContext, ILogger<AastanRepository> logger, IConfiguration configuration,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
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

        public async Task AddOrUpdateTokenAsync(string? accessToken)
        {
            var query = _dbContext.AccessTokens.SingleOrDefault(i => i.Id == "7");
            if (query is null)
            {
                query = new AccessTokenEntity();
                query.Id = "7";
                query.TokenName = "AastanToken";
                await _dbContext.AccessTokens.AddAsync(query).ConfigureAwait(false);
            }
            query.AccessToken = accessToken;
            query.TokenDateTime = DateTime.UtcNow;
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,
                    $"{nameof(AddOrUpdateTokenAsync)} -> applyUpdateToken in AddOrUpdateTokenAsync couldn't update.");
                throw new RamzNegarException(ErrorCode.AastanTokenApiError,
                    $"Exception occurred while: {nameof(AddOrUpdateTokenAsync)}  => {ErrorCode.AastanTokenApiError.GetDisplayName()}");
            }

            //return query;
        }

        public async Task InsertShahkarRequestsLog(ShahkarRequestsLogDTO shahkarRequestsLogDTO)
        {

            var test = _dbContext.ChangeTracker.DebugView.LongView;

            var mappedEntity= _mapper.Map<ShahkarRequestsLogEntity>(shahkarRequestsLogDTO);
            var entity=_dbContext.ShahkarRequestsLog.Add(mappedEntity);
            try
            {
               await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Exception occurred while {nameof(InsertShahkarRequestsLog)}");
                throw new ApplicationException($"Exception occurred while: {nameof(InsertShahkarRequestsLog)}  => {ex.Message}");
            }
        }

        public async Task UpdateShahkarRequestsLog(UpdateShahkarRequestsLogDTO updateShahkarRequests)
        {
            var test = _dbContext.ChangeTracker.DebugView.LongView;
            ShahkarRequestsLogDTO updateShahkar = new ShahkarRequestsLogDTO()
            { 
                RequestId= updateShahkarRequests?.RequestId,
            };
            var mappedEntity = _mapper.Map<ShahkarRequestsLogEntity>(updateShahkar);
            var entity = _dbContext.ShahkarRequestsLog.Add(mappedEntity);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Exception occurred while {nameof(UpdateShahkarRequestsLog)}");
                throw new ApplicationException($"Exception occurred while: {nameof(UpdateShahkarRequestsLog)}  => {ex.Message}");
            }
        }
    }
}
